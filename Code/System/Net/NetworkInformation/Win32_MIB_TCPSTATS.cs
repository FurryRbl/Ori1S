using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C1 RID: 961
	internal struct Win32_MIB_TCPSTATS
	{
		// Token: 0x04001454 RID: 5204
		public uint RtoAlgorithm;

		// Token: 0x04001455 RID: 5205
		public uint RtoMin;

		// Token: 0x04001456 RID: 5206
		public uint RtoMax;

		// Token: 0x04001457 RID: 5207
		public uint MaxConn;

		// Token: 0x04001458 RID: 5208
		public uint ActiveOpens;

		// Token: 0x04001459 RID: 5209
		public uint PassiveOpens;

		// Token: 0x0400145A RID: 5210
		public uint AttemptFails;

		// Token: 0x0400145B RID: 5211
		public uint EstabResets;

		// Token: 0x0400145C RID: 5212
		public uint CurrEstab;

		// Token: 0x0400145D RID: 5213
		public uint InSegs;

		// Token: 0x0400145E RID: 5214
		public uint OutSegs;

		// Token: 0x0400145F RID: 5215
		public uint RetransSegs;

		// Token: 0x04001460 RID: 5216
		public uint InErrs;

		// Token: 0x04001461 RID: 5217
		public uint OutRsts;

		// Token: 0x04001462 RID: 5218
		public uint NumConns;
	}
}
