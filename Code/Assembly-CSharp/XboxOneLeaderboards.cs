using System;

// Token: 0x020008C6 RID: 2246
public class XboxOneLeaderboards
{
	// Token: 0x170007F2 RID: 2034
	// (get) Token: 0x0600320C RID: 12812 RVA: 0x000D4377 File Offset: 0x000D2577
	// (set) Token: 0x0600320D RID: 12813 RVA: 0x000D437E File Offset: 0x000D257E
	public static bool EnableLeaderboards { get; set; }

	// Token: 0x170007F3 RID: 2035
	// (get) Token: 0x0600320E RID: 12814 RVA: 0x000D4386 File Offset: 0x000D2586
	public static LeaderboardData LeaderboardData
	{
		get
		{
			return null;
		}
	}

	// Token: 0x0600320F RID: 12815 RVA: 0x000D4389 File Offset: 0x000D2589
	public static bool UpdateLeaderboard(LeaderboardB leaderboard, Leaderboards.Filter filter, Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06003210 RID: 12816 RVA: 0x000D438C File Offset: 0x000D258C
	public static bool SendLeaderboardData(LeaderboardB leaderboard, long data)
	{
		return CheatsHandler.DebugWasEnabled;
	}

	// Token: 0x06003211 RID: 12817 RVA: 0x000D439C File Offset: 0x000D259C
	private static string GetLeaderboardEventName(LeaderboardB leaderboard)
	{
		switch (leaderboard)
		{
		case LeaderboardB.SpeedRunnerEasy:
			return "SpeedRunnerEventAEasy";
		case LeaderboardB.ExplorerEasy:
			return "ExplorerEventAEasy";
		case LeaderboardB.SurvivorEasy:
			return "GlobalEventAEasy";
		case LeaderboardB.SpeedRunnerNormal:
			return "SpeedRunnerEventANormal";
		case LeaderboardB.ExplorerNormal:
			return "ExplorerEventANormal";
		case LeaderboardB.SurvivorNormal:
			return "GlobalEventANormal";
		case LeaderboardB.SpeedRunnerHard:
			return "SpeedRunnerEventAHard";
		case LeaderboardB.ExplorerHard:
			return "ExplorerEventAHard";
		case LeaderboardB.SurvivorHard:
			return "GlobalEventAHard";
		case LeaderboardB.SpeedRunnerOneLife:
			return "SpeedRunnerEventAOneLife";
		case LeaderboardB.ExplorerOneLife:
			return "ExplorerEventAOneLife";
		case LeaderboardB.SurvivorOneLife:
			return "GlobalEventAOneLife";
		default:
			throw new ArgumentOutOfRangeException("leaderboard");
		}
	}
}
