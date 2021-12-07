using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D6 RID: 982
	internal struct Win32_SOCKADDR
	{
		// Token: 0x040014E1 RID: 5345
		public ushort AddressFamily;

		// Token: 0x040014E2 RID: 5346
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
		public byte[] AddressData;
	}
}
