using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007E RID: 126
	public class UnixChannel : IChannelSender, IChannel, IChannelReceiver
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public UnixChannel() : this(null)
		{
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public UnixChannel(string path)
		{
			this._name = "unix";
			this._priority = 1;
			base..ctor();
			Hashtable hashtable = new Hashtable();
			hashtable["path"] = path;
			this.Init(hashtable, null, null);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000E200 File Offset: 0x0000C400
		public UnixChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider, IServerChannelSinkProvider serverSinkProvider)
		{
			this._name = "unix";
			this._priority = 1;
			base..ctor();
			this.Init(properties, clientSinkProvider, serverSinkProvider);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000E224 File Offset: 0x0000C424
		private void Init(IDictionary properties, IClientChannelSinkProvider clientSink, IServerChannelSinkProvider serverSink)
		{
			this._clientChannel = new UnixClientChannel(properties, clientSink);
			if (properties["path"] != null)
			{
				this._serverChannel = new UnixServerChannel(properties, serverSink);
			}
			object obj = properties["name"];
			if (obj != null)
			{
				this._name = (obj as string);
			}
			obj = properties["priority"];
			if (obj != null)
			{
				this._priority = Convert.ToInt32(obj);
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000E298 File Offset: 0x0000C498
		public IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI)
		{
			return this._clientChannel.CreateMessageSink(url, remoteChannelData, out objectURI);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		public string ChannelName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public int ChannelPriority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public void StartListening(object data)
		{
			if (this._serverChannel != null)
			{
				this._serverChannel.StartListening(data);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		public void StopListening(object data)
		{
			if (this._serverChannel != null)
			{
				this._serverChannel.StopListening(data);
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		public string[] GetUrlsForUri(string uri)
		{
			if (this._serverChannel != null)
			{
				return this._serverChannel.GetUrlsForUri(uri);
			}
			return null;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000E30C File Offset: 0x0000C50C
		public object ChannelData
		{
			get
			{
				if (this._serverChannel != null)
				{
					return this._serverChannel.ChannelData;
				}
				return null;
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000E328 File Offset: 0x0000C528
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000E334 File Offset: 0x0000C534
		internal static string ParseUnixURL(string url, out string objectURI)
		{
			objectURI = null;
			if (!url.StartsWith("unix://"))
			{
				return null;
			}
			int num = url.IndexOf('?');
			if (num == -1)
			{
				return url.Substring(7);
			}
			objectURI = url.Substring(num + 1);
			if (objectURI.Length == 0)
			{
				objectURI = null;
			}
			return url.Substring(7, num - 7);
		}

		// Token: 0x0400042F RID: 1071
		private UnixClientChannel _clientChannel;

		// Token: 0x04000430 RID: 1072
		private UnixServerChannel _serverChannel;

		// Token: 0x04000431 RID: 1073
		private string _name;

		// Token: 0x04000432 RID: 1074
		private int _priority;
	}
}
