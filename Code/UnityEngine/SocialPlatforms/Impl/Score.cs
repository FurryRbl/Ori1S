using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020002E2 RID: 738
	public class Score : IScore
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x00034E58 File Offset: 0x00033058
		public Score() : this("unkown", -1L)
		{
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00034E68 File Offset: 0x00033068
		public Score(string leaderboardID, long value) : this(leaderboardID, value, "0", DateTime.Now, string.Empty, -1)
		{
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00034E90 File Offset: 0x00033090
		public Score(string leaderboardID, long value, string userID, DateTime date, string formattedValue, int rank)
		{
			this.leaderboardID = leaderboardID;
			this.value = value;
			this.m_UserID = userID;
			this.m_Date = date;
			this.m_FormattedValue = formattedValue;
			this.m_Rank = rank;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00034EC8 File Offset: 0x000330C8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Rank: '",
				this.m_Rank,
				"' Value: '",
				this.value,
				"' Category: '",
				this.leaderboardID,
				"' PlayerID: '",
				this.m_UserID,
				"' Date: '",
				this.m_Date
			});
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00034F48 File Offset: 0x00033148
		public void ReportScore(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportScore(this.value, this.leaderboardID, callback);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00034F6C File Offset: 0x0003316C
		public void SetDate(DateTime date)
		{
			this.m_Date = date;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x00034F78 File Offset: 0x00033178
		public void SetFormattedValue(string value)
		{
			this.m_FormattedValue = value;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00034F84 File Offset: 0x00033184
		public void SetUserID(string userID)
		{
			this.m_UserID = userID;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00034F90 File Offset: 0x00033190
		public void SetRank(int rank)
		{
			this.m_Rank = rank;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00034F9C File Offset: 0x0003319C
		// (set) Token: 0x06002640 RID: 9792 RVA: 0x00034FA4 File Offset: 0x000331A4
		public string leaderboardID { get; set; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00034FB0 File Offset: 0x000331B0
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x00034FB8 File Offset: 0x000331B8
		public long value { get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00034FC4 File Offset: 0x000331C4
		public DateTime date
		{
			get
			{
				return this.m_Date;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x00034FCC File Offset: 0x000331CC
		public string formattedValue
		{
			get
			{
				return this.m_FormattedValue;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x00034FD4 File Offset: 0x000331D4
		public string userID
		{
			get
			{
				return this.m_UserID;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x00034FDC File Offset: 0x000331DC
		public int rank
		{
			get
			{
				return this.m_Rank;
			}
		}

		// Token: 0x04000BB9 RID: 3001
		private DateTime m_Date;

		// Token: 0x04000BBA RID: 3002
		private string m_FormattedValue;

		// Token: 0x04000BBB RID: 3003
		private string m_UserID;

		// Token: 0x04000BBC RID: 3004
		private int m_Rank;
	}
}
