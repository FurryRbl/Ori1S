using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000364 RID: 868
	internal struct Win32_MIBICMPSTATS
	{
		// Token: 0x040012E8 RID: 4840
		public uint Msgs;

		// Token: 0x040012E9 RID: 4841
		public uint Errors;

		// Token: 0x040012EA RID: 4842
		public uint DestUnreachs;

		// Token: 0x040012EB RID: 4843
		public uint TimeExcds;

		// Token: 0x040012EC RID: 4844
		public uint ParmProbs;

		// Token: 0x040012ED RID: 4845
		public uint SrcQuenchs;

		// Token: 0x040012EE RID: 4846
		public uint Redirects;

		// Token: 0x040012EF RID: 4847
		public uint Echos;

		// Token: 0x040012F0 RID: 4848
		public uint EchoReps;

		// Token: 0x040012F1 RID: 4849
		public uint Timestamps;

		// Token: 0x040012F2 RID: 4850
		public uint TimestampReps;

		// Token: 0x040012F3 RID: 4851
		public uint AddrMasks;

		// Token: 0x040012F4 RID: 4852
		public uint AddrMaskReps;
	}
}
