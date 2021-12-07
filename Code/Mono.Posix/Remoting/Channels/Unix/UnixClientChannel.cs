using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007F RID: 127
	public class UnixClientChannel : IChannelSender, IChannel
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0000E394 File Offset: 0x0000C594
		public UnixClientChannel()
		{
			this._sinkProvider = new UnixBinaryClientFormatterSinkProvider();
			this._sinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		public UnixClientChannel(IDictionary properties, IClientChannelSinkProvider sinkProvider)
		{
			object obj = properties["name"];
			if (obj != null)
			{
				this.name = (obj as string);
			}
			obj = properties["priority"];
			if (obj != null)
			{
				this.priority = Convert.ToInt32(obj);
			}
			if (sinkProvider != null)
			{
				this._sinkProvider = sinkProvider;
				IClientChannelSinkProvider clientChannelSinkProvider = sinkProvider;
				while (clientChannelSinkProvider.Next != null)
				{
					clientChannelSinkProvider = clientChannelSinkProvider.Next;
				}
				clientChannelSinkProvider.Next = new UnixClientTransportSinkProvider();
			}
			else
			{
				this._sinkProvider = new UnixBinaryClientFormatterSinkProvider();
				this._sinkProvider.Next = new UnixClientTransportSinkProvider();
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000E480 File Offset: 0x0000C680
		public UnixClientChannel(string name, IClientChannelSinkProvider sinkProvider)
		{
			this.name = name;
			this._sinkProvider = sinkProvider;
			IClientChannelSinkProvider clientChannelSinkProvider = sinkProvider;
			while (clientChannelSinkProvider.Next != null)
			{
				clientChannelSinkProvider = clientChannelSinkProvider.Next;
			}
			clientChannelSinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		public string ChannelName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public int ChannelPriority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		public IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI)
		{
			if (url != null && this.Parse(url, out objectURI) != null)
			{
				return (IMessageSink)this._sinkProvider.CreateSink(this, url, remoteChannelData);
			}
			if (remoteChannelData != null)
			{
				IChannelDataStore channelDataStore = remoteChannelData as IChannelDataStore;
				if (channelDataStore == null || channelDataStore.ChannelUris.Length <= 0)
				{
					objectURI = null;
					return null;
				}
				url = channelDataStore.ChannelUris[0];
			}
			if (this.Parse(url, out objectURI) == null)
			{
				return null;
			}
			return (IMessageSink)this._sinkProvider.CreateSink(this, url, remoteChannelData);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000E574 File Offset: 0x0000C774
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x04000433 RID: 1075
		private int priority = 1;

		// Token: 0x04000434 RID: 1076
		private string name = "unix";

		// Token: 0x04000435 RID: 1077
		private IClientChannelSinkProvider _sinkProvider;
	}
}
