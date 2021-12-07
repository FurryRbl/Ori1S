using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D2 RID: 978
	internal struct Win32_IP_ADAPTER_ANYCAST_ADDRESS
	{
		// Token: 0x040014CE RID: 5326
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040014CF RID: 5327
		public IntPtr Next;

		// Token: 0x040014D0 RID: 5328
		public Win32_SOCKET_ADDRESS Address;
	}
}
