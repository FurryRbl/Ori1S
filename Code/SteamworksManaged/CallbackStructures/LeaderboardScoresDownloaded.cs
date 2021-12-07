using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000064 RID: 100
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoresDownloaded
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00006CB5 File Offset: 0x00004EB5
		internal static LeaderboardScoresDownloaded Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LeaderboardScoresDownloaded>(data, dataSize);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00006CBE File Offset: 0x00004EBE
		public LeaderboardHandle Leaderboard
		{
			get
			{
				return this.leaderboard;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00006CC6 File Offset: 0x00004EC6
		public LeaderboardEntriesHandle Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00006CCE File Offset: 0x00004ECE
		public int EntryCount
		{
			get
			{
				return this.entryCount;
			}
		}

		// Token: 0x040001CB RID: 459
		private LeaderboardHandle leaderboard;

		// Token: 0x040001CC RID: 460
		private LeaderboardEntriesHandle entries;

		// Token: 0x040001CD RID: 461
		private int entryCount;
	}
}
