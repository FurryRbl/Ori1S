using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D5 RID: 981
	internal struct Win32_IP_ADAPTER_UNICAST_ADDRESS
	{
		// Token: 0x040014D7 RID: 5335
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040014D8 RID: 5336
		public IntPtr Next;

		// Token: 0x040014D9 RID: 5337
		public Win32_SOCKET_ADDRESS Address;

		// Token: 0x040014DA RID: 5338
		public PrefixOrigin PrefixOrigin;

		// Token: 0x040014DB RID: 5339
		public SuffixOrigin SuffixOrigin;

		// Token: 0x040014DC RID: 5340
		public DuplicateAddressDetectionState DadState;

		// Token: 0x040014DD RID: 5341
		public uint ValidLifetime;

		// Token: 0x040014DE RID: 5342
		public uint PreferredLifetime;

		// Token: 0x040014DF RID: 5343
		public uint LeaseLifetime;

		// Token: 0x040014E0 RID: 5344
		public byte OnLinkPrefixLength;
	}
}
