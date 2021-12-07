using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Threading;
using Mono.Unix;
using Mono.Unix.Native;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000084 RID: 132
	public class UnixServerChannel : IChannel, IChannelReceiver
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		public UnixServerChannel(string path)
		{
			this.path = path;
			this.Init(null);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000EE34 File Offset: 0x0000D034
		public UnixServerChannel(IDictionary properties, IServerChannelSinkProvider serverSinkProvider)
		{
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				switch (text)
				{
				case "path":
					this.path = (dictionaryEntry.Value as string);
					break;
				case "priority":
					this.priority = Convert.ToInt32(dictionaryEntry.Value);
					break;
				case "supressChannelData":
					this.supressChannelData = Convert.ToBoolean(dictionaryEntry.Value);
					break;
				}
			}
			this.Init(serverSinkProvider);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000EF8C File Offset: 0x0000D18C
		public UnixServerChannel(string name, string path, IServerChannelSinkProvider serverSinkProvider)
		{
			this.name = name;
			this.path = path;
			this.Init(serverSinkProvider);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		public UnixServerChannel(string name, string path)
		{
			this.name = name;
			this.path = path;
			this.Init(null);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000F02C File Offset: 0x0000D22C
		private void Init(IServerChannelSinkProvider serverSinkProvider)
		{
			if (serverSinkProvider == null)
			{
				serverSinkProvider = new UnixBinaryServerFormatterSinkProvider();
			}
			this.channel_data = new ChannelDataStore(null);
			for (IServerChannelSinkProvider serverChannelSinkProvider = serverSinkProvider; serverChannelSinkProvider != null; serverChannelSinkProvider = serverChannelSinkProvider.Next)
			{
				serverChannelSinkProvider.GetChannelData(this.channel_data);
			}
			IServerChannelSink next = ChannelServices.CreateServerChannelSinkChain(serverSinkProvider, this);
			this.sink = new UnixServerTransportSink(next);
			this.StartListening(null);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000F090 File Offset: 0x0000D290
		public object ChannelData
		{
			get
			{
				if (this.supressChannelData)
				{
					return null;
				}
				return this.channel_data;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		public string ChannelName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		public int ChannelPriority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public string GetChannelUri()
		{
			return "unix://" + this.path;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public string[] GetUrlsForUri(string uri)
		{
			if (!uri.StartsWith("/"))
			{
				uri = "/" + uri;
			}
			string[] channelUris = this.channel_data.ChannelUris;
			string[] array = new string[channelUris.Length];
			for (int i = 0; i < channelUris.Length; i++)
			{
				array[i] = channelUris[i] + "?" + uri;
			}
			return array;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000F134 File Offset: 0x0000D334
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000F140 File Offset: 0x0000D340
		private void WaitForConnections()
		{
			try
			{
				for (;;)
				{
					Socket client = this.listener.AcceptSocket();
					this.CreateListenerConnection(client);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000F18C File Offset: 0x0000D38C
		internal void CreateListenerConnection(Socket client)
		{
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				if (this._activeConnections.Count >= this._maxConcurrentConnections)
				{
					Monitor.Wait(this._activeConnections);
				}
				if (this.server_thread != null)
				{
					ClientConnection @object = new ClientConnection(this, client, this.sink);
					Thread thread = new Thread(new ThreadStart(@object.ProcessMessages));
					thread.Start();
					thread.IsBackground = true;
					this._activeConnections.Add(thread);
				}
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000F23C File Offset: 0x0000D43C
		internal void ReleaseConnection(Thread thread)
		{
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				this._activeConnections.Remove(thread);
				Monitor.Pulse(this._activeConnections);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000F298 File Offset: 0x0000D498
		public void StartListening(object data)
		{
			this.listener = new UnixListener(this.path);
			Syscall.chmod(this.path, FilePermissions.DEFFILEMODE);
			if (this.server_thread == null)
			{
				this.listener.Start();
				string[] channelUris = new string[1];
				channelUris = new string[]
				{
					this.GetChannelUri()
				};
				this.channel_data.ChannelUris = channelUris;
				this.server_thread = new Thread(new ThreadStart(this.WaitForConnections));
				this.server_thread.IsBackground = true;
				this.server_thread.Start();
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000F330 File Offset: 0x0000D530
		public void StopListening(object data)
		{
			if (this.server_thread == null)
			{
				return;
			}
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				this.server_thread.Abort();
				this.server_thread = null;
				this.listener.Stop();
				foreach (object obj in this._activeConnections)
				{
					Thread thread = (Thread)obj;
					thread.Abort();
				}
				this._activeConnections.Clear();
				Monitor.PulseAll(this._activeConnections);
			}
		}

		// Token: 0x04000442 RID: 1090
		private string path;

		// Token: 0x04000443 RID: 1091
		private string name = "unix";

		// Token: 0x04000444 RID: 1092
		private int priority = 1;

		// Token: 0x04000445 RID: 1093
		private bool supressChannelData;

		// Token: 0x04000446 RID: 1094
		private Thread server_thread;

		// Token: 0x04000447 RID: 1095
		private UnixListener listener;

		// Token: 0x04000448 RID: 1096
		private UnixServerTransportSink sink;

		// Token: 0x04000449 RID: 1097
		private ChannelDataStore channel_data;

		// Token: 0x0400044A RID: 1098
		private int _maxConcurrentConnections = 100;

		// Token: 0x0400044B RID: 1099
		private ArrayList _activeConnections = new ArrayList();
	}
}
