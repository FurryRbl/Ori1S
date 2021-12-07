using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D3 RID: 979
	internal struct Win32_IP_ADAPTER_DNS_SERVER_ADDRESS
	{
		// Token: 0x040014D1 RID: 5329
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040014D2 RID: 5330
		public IntPtr Next;

		// Token: 0x040014D3 RID: 5331
		public Win32_SOCKET_ADDRESS Address;
	}
}
