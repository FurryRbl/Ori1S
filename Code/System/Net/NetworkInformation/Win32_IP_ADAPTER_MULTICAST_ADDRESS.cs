using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D4 RID: 980
	internal struct Win32_IP_ADAPTER_MULTICAST_ADDRESS
	{
		// Token: 0x040014D4 RID: 5332
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040014D5 RID: 5333
		public IntPtr Next;

		// Token: 0x040014D6 RID: 5334
		public Win32_SOCKET_ADDRESS Address;
	}
}
