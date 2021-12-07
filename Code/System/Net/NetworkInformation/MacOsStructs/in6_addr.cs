using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x02000399 RID: 921
	internal struct in6_addr
	{
		// Token: 0x040013AD RID: 5037
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] u6_addr8;
	}
}
