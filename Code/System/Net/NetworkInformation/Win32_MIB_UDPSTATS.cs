using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C5 RID: 965
	internal struct Win32_MIB_UDPSTATS
	{
		// Token: 0x04001465 RID: 5221
		public uint InDatagrams;

		// Token: 0x04001466 RID: 5222
		public uint NoPorts;

		// Token: 0x04001467 RID: 5223
		public uint InErrors;

		// Token: 0x04001468 RID: 5224
		public uint OutDatagrams;

		// Token: 0x04001469 RID: 5225
		public int NumAddrs;
	}
}
