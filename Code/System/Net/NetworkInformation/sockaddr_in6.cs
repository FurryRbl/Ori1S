using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000392 RID: 914
	internal struct sockaddr_in6
	{
		// Token: 0x04001388 RID: 5000
		public ushort sin6_family;

		// Token: 0x04001389 RID: 5001
		public ushort sin6_port;

		// Token: 0x0400138A RID: 5002
		public uint sin6_flowinfo;

		// Token: 0x0400138B RID: 5003
		public in6_addr sin6_addr;

		// Token: 0x0400138C RID: 5004
		public uint sin6_scope_id;
	}
}
