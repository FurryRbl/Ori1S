using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000CE RID: 206
	public interface IGameServerStats
	{
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060005C0 RID: 1472
		// (remove) Token: 0x060005C1 RID: 1473
		event ResultEvent<GSStatsReceived> GSStatsReceived;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060005C2 RID: 1474
		// (remove) Token: 0x060005C3 RID: 1475
		event ResultEvent<GSStatsStored> GSStatsStored;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060005C4 RID: 1476
		// (remove) Token: 0x060005C5 RID: 1477
		event CallbackEvent<GSStatsUnloaded> GSStatsUnloaded;

		// Token: 0x060005C6 RID: 1478
		void RequestUserStats(SteamID steamIDUser);

		// Token: 0x060005C7 RID: 1479
		bool GetUserStat(SteamID steamIDUser, string name, out int data);

		// Token: 0x060005C8 RID: 1480
		GameServerStatsGetUserStatIntResult GetUserStatInt(SteamID steamIDUser, string name);

		// Token: 0x060005C9 RID: 1481
		bool GetUserStat(SteamID steamIDUser, string name, out float data);

		// Token: 0x060005CA RID: 1482
		GameServerStatsGetUserStatFloatResult GetUserStatFloat(SteamID steamIDUser, string name);

		// Token: 0x060005CB RID: 1483
		bool GetUserAchievement(SteamID steamIDUser, string name, out bool achieved);

		// Token: 0x060005CC RID: 1484
		GameServerGetUserAchievementResult GetUserAchievement(SteamID steamIDUser, string name);

		// Token: 0x060005CD RID: 1485
		bool SetUserStat(SteamID steamIDUser, string name, int data);

		// Token: 0x060005CE RID: 1486
		bool SetUserStat(SteamID steamIDUser, string name, float data);

		// Token: 0x060005CF RID: 1487
		bool UpdateUserAvgRateStat(SteamID steamIDUser, string name, float countThisSession, double sessionLength);

		// Token: 0x060005D0 RID: 1488
		bool SetUserAchievement(SteamID steamIDUser, string name);

		// Token: 0x060005D1 RID: 1489
		bool ClearUserAchievement(SteamID steamIDUser, string name);

		// Token: 0x060005D2 RID: 1490
		void StoreUserStats(SteamID steamIDUser);
	}
}
