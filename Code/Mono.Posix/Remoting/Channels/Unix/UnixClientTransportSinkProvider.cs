using System;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000081 RID: 129
	internal class UnixClientTransportSinkProvider : IClientChannelSinkProvider
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000E800 File Offset: 0x0000CA00
		public IClientChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000E804 File Offset: 0x0000CA04
		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
		{
			return new UnixClientTransportSink(url);
		}
	}
}
