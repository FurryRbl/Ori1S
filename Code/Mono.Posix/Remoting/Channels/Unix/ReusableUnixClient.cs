using System;
using System.Net.Sockets;
using Mono.Unix;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000088 RID: 136
	internal class ReusableUnixClient : UnixClient
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0000F808 File Offset: 0x0000DA08
		public ReusableUnixClient(string path) : base(path)
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000F814 File Offset: 0x0000DA14
		public bool IsAlive
		{
			get
			{
				return !base.Client.Poll(0, SelectMode.SelectRead);
			}
		}
	}
}
