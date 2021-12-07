using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D0 RID: 976
	internal struct Win32_IP_ADDR_STRING
	{
		// Token: 0x040014C6 RID: 5318
		public IntPtr Next;

		// Token: 0x040014C7 RID: 5319
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpAddress;

		// Token: 0x040014C8 RID: 5320
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpMask;

		// Token: 0x040014C9 RID: 5321
		public uint Context;
	}
}
