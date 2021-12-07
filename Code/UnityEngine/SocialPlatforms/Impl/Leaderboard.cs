using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020002E3 RID: 739
	public class Leaderboard : ILeaderboard
	{
		// Token: 0x06002647 RID: 9799 RVA: 0x00034FE4 File Offset: 0x000331E4
		public Leaderboard()
		{
			this.id = "Invalid";
			this.range = new Range(1, 10);
			this.userScope = UserScope.Global;
			this.timeScope = TimeScope.AllTime;
			this.m_Loading = false;
			this.m_LocalUserScore = new Score("Invalid", 0L);
			this.m_MaxRange = 0U;
			this.m_Scores = new Score[0];
			this.m_Title = "Invalid";
			this.m_UserIDs = new string[0];
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00035064 File Offset: 0x00033264
		public void SetUserFilter(string[] userIDs)
		{
			this.m_UserIDs = userIDs;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00035070 File Offset: 0x00033270
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ID: '",
				this.id,
				"' Title: '",
				this.m_Title,
				"' Loading: '",
				this.m_Loading,
				"' Range: [",
				this.range.from,
				",",
				this.range.count,
				"] MaxRange: '",
				this.m_MaxRange,
				"' Scores: '",
				this.m_Scores.Length,
				"' UserScope: '",
				this.userScope,
				"' TimeScope: '",
				this.timeScope,
				"' UserFilter: '",
				this.m_UserIDs.Length
			});
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0003517C File Offset: 0x0003337C
		public void LoadScores(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadScores(this, callback);
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x0003518C File Offset: 0x0003338C
		public bool loading
		{
			get
			{
				return ActivePlatform.Instance.GetLoading(this);
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0003519C File Offset: 0x0003339C
		public void SetLocalUserScore(IScore score)
		{
			this.m_LocalUserScore = score;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000351A8 File Offset: 0x000333A8
		public void SetMaxRange(uint maxRange)
		{
			this.m_MaxRange = maxRange;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000351B4 File Offset: 0x000333B4
		public void SetScores(IScore[] scores)
		{
			this.m_Scores = scores;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000351C0 File Offset: 0x000333C0
		public void SetTitle(string title)
		{
			this.m_Title = title;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000351CC File Offset: 0x000333CC
		public string[] GetUserFilter()
		{
			return this.m_UserIDs;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000351D4 File Offset: 0x000333D4
		// (set) Token: 0x06002652 RID: 9810 RVA: 0x000351DC File Offset: 0x000333DC
		public string id { get; set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x000351E8 File Offset: 0x000333E8
		// (set) Token: 0x06002654 RID: 9812 RVA: 0x000351F0 File Offset: 0x000333F0
		public UserScope userScope { get; set; }

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000351FC File Offset: 0x000333FC
		// (set) Token: 0x06002656 RID: 9814 RVA: 0x00035204 File Offset: 0x00033404
		public Range range { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x00035210 File Offset: 0x00033410
		// (set) Token: 0x06002658 RID: 9816 RVA: 0x00035218 File Offset: 0x00033418
		public TimeScope timeScope { get; set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00035224 File Offset: 0x00033424
		public IScore localUserScore
		{
			get
			{
				return this.m_LocalUserScore;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x0003522C File Offset: 0x0003342C
		public uint maxRange
		{
			get
			{
				return this.m_MaxRange;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x00035234 File Offset: 0x00033434
		public IScore[] scores
		{
			get
			{
				return this.m_Scores;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x0003523C File Offset: 0x0003343C
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x04000BBF RID: 3007
		private bool m_Loading;

		// Token: 0x04000BC0 RID: 3008
		private IScore m_LocalUserScore;

		// Token: 0x04000BC1 RID: 3009
		private uint m_MaxRange;

		// Token: 0x04000BC2 RID: 3010
		private IScore[] m_Scores;

		// Token: 0x04000BC3 RID: 3011
		private string m_Title;

		// Token: 0x04000BC4 RID: 3012
		private string[] m_UserIDs;
	}
}
