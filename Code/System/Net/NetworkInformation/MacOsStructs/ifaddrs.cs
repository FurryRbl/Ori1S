using System;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x02000396 RID: 918
	internal struct ifaddrs
	{
		// Token: 0x040013A0 RID: 5024
		public IntPtr ifa_next;

		// Token: 0x040013A1 RID: 5025
		public string ifa_name;

		// Token: 0x040013A2 RID: 5026
		public uint ifa_flags;

		// Token: 0x040013A3 RID: 5027
		public IntPtr ifa_addr;

		// Token: 0x040013A4 RID: 5028
		public IntPtr ifa_netmask;

		// Token: 0x040013A5 RID: 5029
		public IntPtr ifa_dstaddr;

		// Token: 0x040013A6 RID: 5030
		public IntPtr ifa_data;
	}
}
