using System;

// Token: 0x0200063E RID: 1598
public static class LeaderboardUtility
{
	// Token: 0x06002727 RID: 10023 RVA: 0x000AAFED File Offset: 0x000A91ED
	public static Leaderboard LeaderboardBToLeaderboard(LeaderboardB table)
	{
		return (Leaderboard)(table % LeaderboardB.SpeedRunnerNormal);
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x000AAFF2 File Offset: 0x000A91F2
	public static LeaderboardB LeaderboardToLeaderboardB(Leaderboard table, DifficultyMode difficultyMode)
	{
		return (LeaderboardB)(table + (int)(DifficultyMode.OneLife * difficultyMode));
	}
}
