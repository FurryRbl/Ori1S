using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200037C RID: 892
	internal struct Win32_MIB_IPSTATS
	{
		// Token: 0x04001334 RID: 4916
		public int Forwarding;

		// Token: 0x04001335 RID: 4917
		public int DefaultTTL;

		// Token: 0x04001336 RID: 4918
		public uint InReceives;

		// Token: 0x04001337 RID: 4919
		public uint InHdrErrors;

		// Token: 0x04001338 RID: 4920
		public uint InAddrErrors;

		// Token: 0x04001339 RID: 4921
		public uint ForwDatagrams;

		// Token: 0x0400133A RID: 4922
		public uint InUnknownProtos;

		// Token: 0x0400133B RID: 4923
		public uint InDiscards;

		// Token: 0x0400133C RID: 4924
		public uint InDelivers;

		// Token: 0x0400133D RID: 4925
		public uint OutRequests;

		// Token: 0x0400133E RID: 4926
		public uint RoutingDiscards;

		// Token: 0x0400133F RID: 4927
		public uint OutDiscards;

		// Token: 0x04001340 RID: 4928
		public uint OutNoRoutes;

		// Token: 0x04001341 RID: 4929
		public uint ReasmTimeout;

		// Token: 0x04001342 RID: 4930
		public uint ReasmReqds;

		// Token: 0x04001343 RID: 4931
		public uint ReasmOks;

		// Token: 0x04001344 RID: 4932
		public uint ReasmFails;

		// Token: 0x04001345 RID: 4933
		public uint FragOks;

		// Token: 0x04001346 RID: 4934
		public uint FragFails;

		// Token: 0x04001347 RID: 4935
		public uint FragCreates;

		// Token: 0x04001348 RID: 4936
		public int NumIf;

		// Token: 0x04001349 RID: 4937
		public int NumAddr;

		// Token: 0x0400134A RID: 4938
		public int NumRoutes;
	}
}
