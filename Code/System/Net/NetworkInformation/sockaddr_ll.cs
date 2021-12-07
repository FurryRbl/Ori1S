using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000394 RID: 916
	internal struct sockaddr_ll
	{
		// Token: 0x0400138E RID: 5006
		public ushort sll_family;

		// Token: 0x0400138F RID: 5007
		public ushort sll_protocol;

		// Token: 0x04001390 RID: 5008
		public int sll_ifindex;

		// Token: 0x04001391 RID: 5009
		public ushort sll_hatype;

		// Token: 0x04001392 RID: 5010
		public byte sll_pkttype;

		// Token: 0x04001393 RID: 5011
		public byte sll_halen;

		// Token: 0x04001394 RID: 5012
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] sll_addr;
	}
}
