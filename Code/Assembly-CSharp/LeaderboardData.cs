using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000640 RID: 1600
public class LeaderboardData : IEnumerable, IEnumerable<LeaderboardData.Entry>
{
	// Token: 0x06002729 RID: 10025 RVA: 0x000AAFF9 File Offset: 0x000A91F9
	public LeaderboardData(Leaderboard type, Leaderboards.Filter filter, string title, uint totalRowCount, IEnumerable<LeaderboardData.Entry> entries)
	{
		this.m_type = type;
		this.m_filter = filter;
		this.m_title = title;
		this.m_totalRowCount = totalRowCount;
		this.m_entries.AddRange(entries);
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x000AB036 File Offset: 0x000A9236
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x17000630 RID: 1584
	// (get) Token: 0x0600272B RID: 10027 RVA: 0x000AB03E File Offset: 0x000A923E
	public Leaderboard Type
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x17000631 RID: 1585
	// (get) Token: 0x0600272C RID: 10028 RVA: 0x000AB046 File Offset: 0x000A9246
	public Leaderboards.Filter Filter
	{
		get
		{
			return this.m_filter;
		}
	}

	// Token: 0x17000632 RID: 1586
	// (get) Token: 0x0600272D RID: 10029 RVA: 0x000AB04E File Offset: 0x000A924E
	public string Title
	{
		get
		{
			return this.m_title;
		}
	}

	// Token: 0x17000633 RID: 1587
	// (get) Token: 0x0600272E RID: 10030 RVA: 0x000AB056 File Offset: 0x000A9256
	public int Count
	{
		get
		{
			return this.m_entries.Count;
		}
	}

	// Token: 0x17000634 RID: 1588
	// (get) Token: 0x0600272F RID: 10031 RVA: 0x000AB063 File Offset: 0x000A9263
	public uint TotalRowCount
	{
		get
		{
			return this.m_totalRowCount;
		}
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x000AB06C File Offset: 0x000A926C
	public bool Update(LeaderboardData newData)
	{
		if (this.m_type != newData.m_type)
		{
			return false;
		}
		this.m_filter = newData.Filter;
		this.m_title = newData.m_title;
		this.m_entries.Clear();
		this.m_entries.AddRange(newData);
		return true;
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x000AB0BC File Offset: 0x000A92BC
	public IEnumerator<LeaderboardData.Entry> GetEnumerator()
	{
		return this.m_entries.GetEnumerator();
	}

	// Token: 0x17000635 RID: 1589
	public LeaderboardData.Entry this[int index]
	{
		get
		{
			return this.m_entries[index];
		}
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x000AB0DC File Offset: 0x000A92DC
	public override string ToString()
	{
		return string.Format("{0}: {1} ({2})", this.m_type, this.m_title, this.m_entries.Count);
	}

	// Token: 0x040021CF RID: 8655
	private Leaderboard m_type;

	// Token: 0x040021D0 RID: 8656
	private Leaderboards.Filter m_filter;

	// Token: 0x040021D1 RID: 8657
	private string m_title;

	// Token: 0x040021D2 RID: 8658
	private uint m_totalRowCount;

	// Token: 0x040021D3 RID: 8659
	private List<LeaderboardData.Entry> m_entries = new List<LeaderboardData.Entry>();

	// Token: 0x02000641 RID: 1601
	public class Entry
	{
		// Token: 0x06002734 RID: 10036 RVA: 0x000AB10C File Offset: 0x000A930C
		public Entry(Leaderboard leaderboard, uint rank, long score, string userID, string userHandle)
		{
			this.Rank = rank;
			this.UserID = userID;
			this.UserHandle = userHandle;
			LeaderboardEntryData leaderboardEntryData = new LeaderboardEntryData();
			switch (leaderboard)
			{
			case Leaderboard.SpeedRunner:
				leaderboardEntryData.DecodeSpeedRunner(score);
				break;
			case Leaderboard.Explorer:
				leaderboardEntryData.DecodeExplorer(score);
				break;
			case Leaderboard.Survivor:
				leaderboardEntryData.DecodeSurvivor(score);
				break;
			}
			this.m_time = leaderboardEntryData.Time;
			this.m_deathCount = leaderboardEntryData.DeathCount;
			this.m_completionPercentage = leaderboardEntryData.CompletionPercentage;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000AB19E File Offset: 0x000A939E
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x000AB1A6 File Offset: 0x000A93A6
		public uint Rank { get; protected set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x000AB1AF File Offset: 0x000A93AF
		// (set) Token: 0x06002738 RID: 10040 RVA: 0x000AB1B7 File Offset: 0x000A93B7
		public string UserID { get; protected set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x000AB1C0 File Offset: 0x000A93C0
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x000AB1C8 File Offset: 0x000A93C8
		public string UserHandle { get; protected set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000AB1D1 File Offset: 0x000A93D1
		public int Time
		{
			get
			{
				return this.m_time;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x000AB1D9 File Offset: 0x000A93D9
		public int DeathCount
		{
			get
			{
				return this.m_deathCount;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x000AB1E1 File Offset: 0x000A93E1
		public int CompletionPercentage
		{
			get
			{
				return this.m_completionPercentage;
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000AB1EC File Offset: 0x000A93EC
		public override string ToString()
		{
			return string.Format("{0} ({1:f2}): {2} ({3}) {4} {5}", new object[]
			{
				this.Rank,
				this.Time,
				this.DeathCount,
				this.CompletionPercentage,
				this.UserHandle,
				this.UserID
			});
		}

		// Token: 0x040021D4 RID: 8660
		private int m_time;

		// Token: 0x040021D5 RID: 8661
		private int m_completionPercentage;

		// Token: 0x040021D6 RID: 8662
		private int m_deathCount;
	}
}
