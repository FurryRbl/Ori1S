using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x0200009D RID: 157
	public interface IStats
	{
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060004DF RID: 1247
		// (remove) Token: 0x060004E0 RID: 1248
		event CallbackEvent<UserStatsReceived> UserStatsReceived;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060004E1 RID: 1249
		// (remove) Token: 0x060004E2 RID: 1250
		event CallbackEvent<UserStatsStored> UserStatsStored;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060004E3 RID: 1251
		// (remove) Token: 0x060004E4 RID: 1252
		event CallbackEvent<UserAchievementStored> UserAchievementStored;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060004E5 RID: 1253
		// (remove) Token: 0x060004E6 RID: 1254
		event ResultEvent<LeaderboardFindResult> LeaderboardFindResult;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060004E7 RID: 1255
		// (remove) Token: 0x060004E8 RID: 1256
		event ResultEvent<LeaderboardScoresDownloaded> LeaderboardScoresDownloaded;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060004E9 RID: 1257
		// (remove) Token: 0x060004EA RID: 1258
		event ResultEvent<LeaderboardScoreUploaded> LeaderboardScoreUploaded;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060004EB RID: 1259
		// (remove) Token: 0x060004EC RID: 1260
		event ResultEvent<LeaderboardUGCSet> LeaderboardUGCSet;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060004ED RID: 1261
		// (remove) Token: 0x060004EE RID: 1262
		event ResultEvent<NumberOfCurrentPlayers> NumberOfCurrentPlayers;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060004EF RID: 1263
		// (remove) Token: 0x060004F0 RID: 1264
		event CallbackEvent<UserStatsUnloaded> UserStatsUnloaded;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060004F1 RID: 1265
		// (remove) Token: 0x060004F2 RID: 1266
		event CallbackEvent<UserAchievementIconFetched> UserAchievementIconFetched;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060004F3 RID: 1267
		// (remove) Token: 0x060004F4 RID: 1268
		event ResultEvent<GlobalAchievementPercentagesReady> GlobalAchievementPercentagesReady;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060004F5 RID: 1269
		// (remove) Token: 0x060004F6 RID: 1270
		event ResultEvent<GlobalStatsReceived> GlobalStatsReceived;

		// Token: 0x060004F7 RID: 1271
		bool RequestCurrentStats();

		// Token: 0x060004F8 RID: 1272
		bool GetStat(string name, out int data);

		// Token: 0x060004F9 RID: 1273
		StatsGetStatIntResult GetStatInt(string name);

		// Token: 0x060004FA RID: 1274
		bool GetStat(string name, out float data);

		// Token: 0x060004FB RID: 1275
		StatsGetStatFloatResult GetStatFloat(string name);

		// Token: 0x060004FC RID: 1276
		bool SetStat(string name, int data);

		// Token: 0x060004FD RID: 1277
		bool SetStat(string name, float data);

		// Token: 0x060004FE RID: 1278
		bool UpdateAverageRateStat(string name, float countThisSession, double sessionLength);

		// Token: 0x060004FF RID: 1279
		bool GetAchievement(string name, out bool achieved);

		// Token: 0x06000500 RID: 1280
		StatsGetAchievementResult GetAchievement(string name);

		// Token: 0x06000501 RID: 1281
		bool SetAchievement(string name);

		// Token: 0x06000502 RID: 1282
		bool ClearAchievement(string name);

		// Token: 0x06000503 RID: 1283
		bool GetAchievementAndUnlockTime(string name, out bool achieved, out uint unlockTime);

		// Token: 0x06000504 RID: 1284
		StatsGetAchievementAndUnlockTimeResult GetAchievementAndUnlockTime(SteamID steamID, string name);

		// Token: 0x06000505 RID: 1285
		bool StoreStats();

		// Token: 0x06000506 RID: 1286
		string GetAchievementDisplayAttribute(string name, string key);

		// Token: 0x06000507 RID: 1287
		bool IndicateAchievementProgress(string name, uint currentProgress, uint maxProgress);

		// Token: 0x06000508 RID: 1288
		void RequestUserStats(SteamID steamID);

		// Token: 0x06000509 RID: 1289
		bool GetUserStat(SteamID steamID, string name, out int data);

		// Token: 0x0600050A RID: 1290
		StatsGetUserStatResult GetUserStatInt(SteamID steamID, string name);

		// Token: 0x0600050B RID: 1291
		bool GetUserStat(SteamID steamID, string name, out float data);

		// Token: 0x0600050C RID: 1292
		StatsGetUserStatResult GetUserStatFloat(SteamID steamID, string name);

		// Token: 0x0600050D RID: 1293
		bool GetUserAchievement(SteamID steamID, string name, out bool achieved);

		// Token: 0x0600050E RID: 1294
		StatsGetUserAchievementResult GetUserAchievement(SteamID steamID, string name);

		// Token: 0x0600050F RID: 1295
		bool GetUserAchievementAndUnlockTime(SteamID steamID, string name, out bool achieved, out uint unlockTime);

		// Token: 0x06000510 RID: 1296
		StatsGetUserAchievementAndUnlockTimeResult GetUserAchievementAndUnlockTime(SteamID steamID, string name);

		// Token: 0x06000511 RID: 1297
		bool ResetAllStats(bool achievementsToo);

		// Token: 0x06000512 RID: 1298
		void FindOrCreateLeaderboard(string name, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType);

		// Token: 0x06000513 RID: 1299
		void FindLeaderboard(string name);

		// Token: 0x06000514 RID: 1300
		string GetLeaderboardName(LeaderboardHandle handle);

		// Token: 0x06000515 RID: 1301
		int GetLeaderboardEntryCount(LeaderboardHandle handle);

		// Token: 0x06000516 RID: 1302
		LeaderboardSortMethod GetLeaderboardSortMethod(LeaderboardHandle handle);

		// Token: 0x06000517 RID: 1303
		LeaderboardDisplayType GetLeaderboardDisplayType(LeaderboardHandle handle);

		// Token: 0x06000518 RID: 1304
		void DownloadLeaderboardEntries(LeaderboardHandle handle, LeaderboardDataRequest dataRequest, int rangeStart, int rangeEnd);

		// Token: 0x06000519 RID: 1305
		void DownloadLeaderboardEntriesForUsers(LeaderboardHandle handle, SteamID[] users);

		// Token: 0x0600051A RID: 1306
		bool GetDownloadedLeaderboardEntry(LeaderboardEntriesHandle entries, int index, out LeaderboardEntry entry, int[] details);

		// Token: 0x0600051B RID: 1307
		void UploadLeaderboardScore(LeaderboardHandle handle, LeaderboardUploadScoreMethod scoreMethod, int score, int[] details);

		// Token: 0x0600051C RID: 1308
		void AttachLeaderboardUGC(LeaderboardHandle handle, UGCHandle ugcHandle);

		// Token: 0x0600051D RID: 1309
		void GetNumberOfCurrentPlayers();

		// Token: 0x0600051E RID: 1310
		void RequestGlobalAchievementPercentages();

		// Token: 0x0600051F RID: 1311
		int GetMostAchievedAchievementInfo(out string name, out float percent, out bool achieved);

		// Token: 0x06000520 RID: 1312
		StatsGetMostAchievedAchievementInfo GetMostAchievedAchievementInfo();

		// Token: 0x06000521 RID: 1313
		int GetNextMostAchievedAchievementInfo(int iterator, out string name, out float percent, out bool achieved);

		// Token: 0x06000522 RID: 1314
		StatsGetNextMostAchievedAchievementInfo GetNextMostAchievedAchievementInfo(int iterator);

		// Token: 0x06000523 RID: 1315
		bool GetAchievementAchievedPercent(string name, out float percent);

		// Token: 0x06000524 RID: 1316
		StatsGetAchievmentAchievedPercent GetAchievementAchievedPercent(string name);

		// Token: 0x06000525 RID: 1317
		void RequestGlobalStats(int historyDays);

		// Token: 0x06000526 RID: 1318
		bool GetGlobalStat(string name, out long data);

		// Token: 0x06000527 RID: 1319
		StatsGetGlobalStat GetGlobalStatLong(string name);

		// Token: 0x06000528 RID: 1320
		bool GetGlobalStat(string name, out double data);

		// Token: 0x06000529 RID: 1321
		StatsGetGlobalStat GetGlobalStatDouble(string name);

		// Token: 0x0600052A RID: 1322
		int GetGlobalStatHistory(string name, out long[] data, int historyDays);

		// Token: 0x0600052B RID: 1323
		StatsGetGlobalStatHistory GetGlobalStatHistoryLong(string name, int historyDays);

		// Token: 0x0600052C RID: 1324
		int GetGlobalStatHistory(string name, out double[] data, int historyDays);

		// Token: 0x0600052D RID: 1325
		StatsGetGlobalStatHistory GetGlobalStatHistoryDouble(string name, int historyDays);
	}
}
