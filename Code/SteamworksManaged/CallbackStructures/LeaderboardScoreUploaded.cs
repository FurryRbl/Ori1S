using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000065 RID: 101
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoreUploaded
	{
		// Token: 0x06000345 RID: 837 RVA: 0x00006CD6 File Offset: 0x00004ED6
		internal static LeaderboardScoreUploaded Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LeaderboardScoreUploaded>(data, dataSize);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00006CDF File Offset: 0x00004EDF
		public byte Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00006CE7 File Offset: 0x00004EE7
		public LeaderboardHandle Leaderboard
		{
			get
			{
				return this.leaderboard;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00006CEF File Offset: 0x00004EEF
		public int Score
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00006CF7 File Offset: 0x00004EF7
		public byte ScoreChanged
		{
			get
			{
				return this.scoreChanged;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00006CFF File Offset: 0x00004EFF
		public int RankNew
		{
			get
			{
				return this.rankNew;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00006D07 File Offset: 0x00004F07
		public int RankPrevious
		{
			get
			{
				return this.rankPrevious;
			}
		}

		// Token: 0x040001CE RID: 462
		private byte success;

		// Token: 0x040001CF RID: 463
		private LeaderboardHandle leaderboard;

		// Token: 0x040001D0 RID: 464
		private int score;

		// Token: 0x040001D1 RID: 465
		private byte scoreChanged;

		// Token: 0x040001D2 RID: 466
		private int rankNew;

		// Token: 0x040001D3 RID: 467
		private int rankPrevious;
	}
}
