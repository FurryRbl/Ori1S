using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200045E RID: 1118
	internal class ServerDispatchSinkProvider : IServerChannelSinkProvider, IServerFormatterSinkProvider
	{
		// Token: 0x06002E91 RID: 11921 RVA: 0x0009AA5C File Offset: 0x00098C5C
		public ServerDispatchSinkProvider()
		{
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0009AA64 File Offset: 0x00098C64
		public ServerDispatchSinkProvider(IDictionary properties, ICollection providerData)
		{
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x0009AA6C File Offset: 0x00098C6C
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x0009AA70 File Offset: 0x00098C70
		public IServerChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x0009AA78 File Offset: 0x00098C78
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new ServerDispatchSink();
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x0009AA80 File Offset: 0x00098C80
		public void GetChannelData(IChannelDataStore channelData)
		{
		}
	}
}
