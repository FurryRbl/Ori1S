using System;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x02000398 RID: 920
	internal struct sockaddr_in
	{
		// Token: 0x040013A9 RID: 5033
		public byte sin_len;

		// Token: 0x040013AA RID: 5034
		public byte sin_family;

		// Token: 0x040013AB RID: 5035
		public ushort sin_port;

		// Token: 0x040013AC RID: 5036
		public uint sin_addr;
	}
}
