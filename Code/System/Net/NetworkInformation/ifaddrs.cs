using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000390 RID: 912
	internal struct ifaddrs
	{
		// Token: 0x0400137E RID: 4990
		public IntPtr ifa_next;

		// Token: 0x0400137F RID: 4991
		public string ifa_name;

		// Token: 0x04001380 RID: 4992
		public uint ifa_flags;

		// Token: 0x04001381 RID: 4993
		public IntPtr ifa_addr;

		// Token: 0x04001382 RID: 4994
		public IntPtr ifa_netmask;

		// Token: 0x04001383 RID: 4995
		public ifa_ifu ifa_ifu;

		// Token: 0x04001384 RID: 4996
		public IntPtr ifa_data;
	}
}
