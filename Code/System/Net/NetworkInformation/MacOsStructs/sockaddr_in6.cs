using System;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x0200039A RID: 922
	internal struct sockaddr_in6
	{
		// Token: 0x040013AE RID: 5038
		public byte sin6_len;

		// Token: 0x040013AF RID: 5039
		public byte sin6_family;

		// Token: 0x040013B0 RID: 5040
		public ushort sin6_port;

		// Token: 0x040013B1 RID: 5041
		public uint sin6_flowinfo;

		// Token: 0x040013B2 RID: 5042
		public in6_addr sin6_addr;

		// Token: 0x040013B3 RID: 5043
		public uint sin6_scope_id;
	}
}
