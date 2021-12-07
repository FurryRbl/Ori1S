using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000086 RID: 134
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardEntry
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x00007CD9 File Offset: 0x00005ED9
		internal static LeaderboardEntry Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LeaderboardEntry>(data, dataSize);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00007CE2 File Offset: 0x00005EE2
		public SteamID User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00007CEA File Offset: 0x00005EEA
		public int Rank
		{
			get
			{
				return this.rank;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00007CF2 File Offset: 0x00005EF2
		public int Score
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00007CFA File Offset: 0x00005EFA
		public int NumberOfDetails
		{
			get
			{
				return this.numberOfDetails;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00007D02 File Offset: 0x00005F02
		public UGCHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0400026F RID: 623
		private SteamID user;

		// Token: 0x04000270 RID: 624
		private int rank;

		// Token: 0x04000271 RID: 625
		private int score;

		// Token: 0x04000272 RID: 626
		private int numberOfDetails;

		// Token: 0x04000273 RID: 627
		private UGCHandle handle;
	}
}
