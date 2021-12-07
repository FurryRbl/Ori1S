using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000138 RID: 312
	internal class Stats : SteamService, IStats
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x0000D230 File Offset: 0x0000B430
		internal Stats()
		{
			this.userStatsReceived = new List<UserStatsReceived>();
			this.userStatsStored = new List<UserStatsStored>();
			this.userAchievementStored = new List<UserAchievementStored>();
			this.leaderboardFindResult = new List<SteamService.Result<LeaderboardFindResult>>();
			this.leaderboardScoresDownloaded = new List<SteamService.Result<LeaderboardScoresDownloaded>>();
			this.leaderboardScoresUploaded = new List<SteamService.Result<LeaderboardScoreUploaded>>();
			this.leaderboardUGCSet = new List<SteamService.Result<LeaderboardUGCSet>>();
			this.numberOfCurrentPlayers = new List<SteamService.Result<NumberOfCurrentPlayers>>();
			this.userStatsUnloaded = new List<UserStatsUnloaded>();
			this.userAchievementIconFetched = new List<UserAchievementIconFetched>();
			this.globalAchievementPercentages = new List<SteamService.Result<GlobalAchievementPercentagesReady>>();
			this.globalStatsReceived = new List<SteamService.Result<GlobalStatsReceived>>();
			SteamService.Callbacks[CallbackID.UserStatsReceived] = delegate(IntPtr data, int size)
			{
				this.userStatsReceived.Add(ManagedSteam.CallbackStructures.UserStatsReceived.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.UserStatsStored] = delegate(IntPtr data, int size)
			{
				this.userStatsStored.Add(ManagedSteam.CallbackStructures.UserStatsStored.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.UserAchievementStored] = delegate(IntPtr data, int size)
			{
				this.userAchievementStored.Add(ManagedSteam.CallbackStructures.UserAchievementStored.Create(data, size));
			};
			SteamService.Results[ResultID.LeaderboardFindResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.leaderboardFindResult.Add(new SteamService.Result<LeaderboardFindResult>(ManagedSteam.CallbackStructures.LeaderboardFindResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.LeaderboardScoresDownloaded] = delegate(IntPtr data, int size, bool flag)
			{
				this.leaderboardScoresDownloaded.Add(new SteamService.Result<LeaderboardScoresDownloaded>(ManagedSteam.CallbackStructures.LeaderboardScoresDownloaded.Create(data, size), flag));
			};
			SteamService.Results[ResultID.LeaderboardScoreUploaded] = delegate(IntPtr data, int size, bool flag)
			{
				this.leaderboardScoresUploaded.Add(new SteamService.Result<LeaderboardScoreUploaded>(ManagedSteam.CallbackStructures.LeaderboardScoreUploaded.Create(data, size), flag));
			};
			SteamService.Results[ResultID.LeaderboardUGCSet] = delegate(IntPtr data, int size, bool flag)
			{
				this.leaderboardUGCSet.Add(new SteamService.Result<LeaderboardUGCSet>(ManagedSteam.CallbackStructures.LeaderboardUGCSet.Create(data, size), flag));
			};
			SteamService.Results[ResultID.NumberOfCurrentPlayers] = delegate(IntPtr data, int size, bool flag)
			{
				this.numberOfCurrentPlayers.Add(new SteamService.Result<NumberOfCurrentPlayers>(ManagedSteam.CallbackStructures.NumberOfCurrentPlayers.Create(data, size), flag));
			};
			SteamService.Callbacks[CallbackID.UserStatsUnloaded] = delegate(IntPtr data, int size)
			{
				this.userStatsUnloaded.Add(ManagedSteam.CallbackStructures.UserStatsUnloaded.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.UserAchievementIconFetched] = delegate(IntPtr data, int size)
			{
				this.userAchievementIconFetched.Add(ManagedSteam.CallbackStructures.UserAchievementIconFetched.Create(data, size));
			};
			SteamService.Results[ResultID.GlobalAchievementPercentagesReady] = delegate(IntPtr data, int size, bool flag)
			{
				this.globalAchievementPercentages.Add(new SteamService.Result<GlobalAchievementPercentagesReady>(ManagedSteam.CallbackStructures.GlobalAchievementPercentagesReady.Create(data, size), flag));
			};
			SteamService.Results[ResultID.GlobalStatsReceived] = delegate(IntPtr data, int size, bool flag)
			{
				this.globalStatsReceived.Add(new SteamService.Result<GlobalStatsReceived>(ManagedSteam.CallbackStructures.GlobalStatsReceived.Create(data, size), flag));
			};
		}

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06000A9F RID: 2719 RVA: 0x0000D458 File Offset: 0x0000B658
		// (remove) Token: 0x06000AA0 RID: 2720 RVA: 0x0000D490 File Offset: 0x0000B690
		public event CallbackEvent<UserStatsReceived> UserStatsReceived;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06000AA1 RID: 2721 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		// (remove) Token: 0x06000AA2 RID: 2722 RVA: 0x0000D500 File Offset: 0x0000B700
		public event CallbackEvent<UserStatsStored> UserStatsStored;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06000AA3 RID: 2723 RVA: 0x0000D538 File Offset: 0x0000B738
		// (remove) Token: 0x06000AA4 RID: 2724 RVA: 0x0000D570 File Offset: 0x0000B770
		public event CallbackEvent<UserAchievementStored> UserAchievementStored;

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06000AA5 RID: 2725 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		// (remove) Token: 0x06000AA6 RID: 2726 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		public event ResultEvent<LeaderboardFindResult> LeaderboardFindResult;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06000AA7 RID: 2727 RVA: 0x0000D618 File Offset: 0x0000B818
		// (remove) Token: 0x06000AA8 RID: 2728 RVA: 0x0000D650 File Offset: 0x0000B850
		public event ResultEvent<LeaderboardScoresDownloaded> LeaderboardScoresDownloaded;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06000AA9 RID: 2729 RVA: 0x0000D688 File Offset: 0x0000B888
		// (remove) Token: 0x06000AAA RID: 2730 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public event ResultEvent<LeaderboardScoreUploaded> LeaderboardScoreUploaded;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06000AAB RID: 2731 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		// (remove) Token: 0x06000AAC RID: 2732 RVA: 0x0000D730 File Offset: 0x0000B930
		public event ResultEvent<LeaderboardUGCSet> LeaderboardUGCSet;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06000AAD RID: 2733 RVA: 0x0000D768 File Offset: 0x0000B968
		// (remove) Token: 0x06000AAE RID: 2734 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public event ResultEvent<NumberOfCurrentPlayers> NumberOfCurrentPlayers;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06000AAF RID: 2735 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		// (remove) Token: 0x06000AB0 RID: 2736 RVA: 0x0000D810 File Offset: 0x0000BA10
		public event CallbackEvent<UserStatsUnloaded> UserStatsUnloaded;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06000AB1 RID: 2737 RVA: 0x0000D848 File Offset: 0x0000BA48
		// (remove) Token: 0x06000AB2 RID: 2738 RVA: 0x0000D880 File Offset: 0x0000BA80
		public event CallbackEvent<UserAchievementIconFetched> UserAchievementIconFetched;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06000AB3 RID: 2739 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		// (remove) Token: 0x06000AB4 RID: 2740 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		public event ResultEvent<GlobalAchievementPercentagesReady> GlobalAchievementPercentagesReady;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06000AB5 RID: 2741 RVA: 0x0000D928 File Offset: 0x0000BB28
		// (remove) Token: 0x06000AB6 RID: 2742 RVA: 0x0000D960 File Offset: 0x0000BB60
		public event ResultEvent<GlobalStatsReceived> GlobalStatsReceived;

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000D995 File Offset: 0x0000BB95
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0000D998 File Offset: 0x0000BB98
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<UserStatsReceived>(this.userStatsReceived, this.UserStatsReceived);
			SteamService.InvokeEvents<UserStatsStored>(this.userStatsStored, this.UserStatsStored);
			SteamService.InvokeEvents<UserAchievementStored>(this.userAchievementStored, this.UserAchievementStored);
			SteamService.InvokeEvents<LeaderboardFindResult>(this.leaderboardFindResult, this.LeaderboardFindResult);
			SteamService.InvokeEvents<LeaderboardScoresDownloaded>(this.leaderboardScoresDownloaded, this.LeaderboardScoresDownloaded);
			SteamService.InvokeEvents<LeaderboardScoreUploaded>(this.leaderboardScoresUploaded, this.LeaderboardScoreUploaded);
			SteamService.InvokeEvents<LeaderboardUGCSet>(this.leaderboardUGCSet, this.LeaderboardUGCSet);
			SteamService.InvokeEvents<NumberOfCurrentPlayers>(this.numberOfCurrentPlayers, this.NumberOfCurrentPlayers);
			SteamService.InvokeEvents<UserStatsUnloaded>(this.userStatsUnloaded, this.UserStatsUnloaded);
			SteamService.InvokeEvents<UserAchievementIconFetched>(this.userAchievementIconFetched, this.UserAchievementIconFetched);
			SteamService.InvokeEvents<GlobalAchievementPercentagesReady>(this.globalAchievementPercentages, this.GlobalAchievementPercentagesReady);
			SteamService.InvokeEvents<GlobalStatsReceived>(this.globalStatsReceived, this.GlobalStatsReceived);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000DA74 File Offset: 0x0000BC74
		internal override void ReleaseManagedResources()
		{
			this.userStatsReceived = null;
			this.UserStatsReceived = null;
			this.userStatsStored = null;
			this.UserStatsStored = null;
			this.userAchievementStored = null;
			this.UserAchievementStored = null;
			this.leaderboardFindResult = null;
			this.LeaderboardFindResult = null;
			this.leaderboardScoresDownloaded = null;
			this.LeaderboardScoresDownloaded = null;
			this.leaderboardScoresUploaded = null;
			this.LeaderboardScoreUploaded = null;
			this.leaderboardUGCSet = null;
			this.LeaderboardUGCSet = null;
			this.numberOfCurrentPlayers = null;
			this.NumberOfCurrentPlayers = null;
			this.userStatsUnloaded = null;
			this.UserStatsUnloaded = null;
			this.userAchievementIconFetched = null;
			this.UserAchievementIconFetched = null;
			this.globalAchievementPercentages = null;
			this.GlobalAchievementPercentagesReady = null;
			this.globalStatsReceived = null;
			this.GlobalStatsReceived = null;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000DB29 File Offset: 0x0000BD29
		public bool RequestCurrentStats()
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_RequestCurrentStats();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0000DB36 File Offset: 0x0000BD36
		public bool GetStat(string name, out int data)
		{
			base.CheckIfUsable();
			data = 0;
			return NativeMethods.Stats_GetStatInt(name, ref data);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public StatsGetStatIntResult GetStatInt(string name)
		{
			StatsGetStatIntResult result = default(StatsGetStatIntResult);
			result.Result = this.GetStat(name, out result.IntValue);
			return result;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0000DB73 File Offset: 0x0000BD73
		public bool GetStat(string name, out float data)
		{
			base.CheckIfUsable();
			data = 0f;
			return NativeMethods.Stats_GetStatFloat(name, ref data);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public StatsGetStatFloatResult GetStatFloat(string name)
		{
			StatsGetStatFloatResult result = default(StatsGetStatFloatResult);
			result.Result = this.GetStat(name, out result.FloatValue);
			return result;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0000DBB7 File Offset: 0x0000BDB7
		public bool SetStat(string name, int data)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_SetStatInt(name, data);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		public bool SetStat(string name, float data)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_SetStatFloat(name, data);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0000DBD5 File Offset: 0x0000BDD5
		public bool UpdateAverageRateStat(string name, float countThisSession, double sessionLength)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_UpdateAverageRateStat(name, countThisSession, sessionLength);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0000DBE5 File Offset: 0x0000BDE5
		public bool GetAchievement(string name, out bool achieved)
		{
			base.CheckIfUsable();
			achieved = false;
			return NativeMethods.Stats_GetAchievement(name, ref achieved);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		public StatsGetAchievementResult GetAchievement(string name)
		{
			StatsGetAchievementResult result = default(StatsGetAchievementResult);
			result.result = this.GetAchievement(name, out result.sender);
			return result;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0000DC23 File Offset: 0x0000BE23
		public bool SetAchievement(string name)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_SetAchievement(name);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0000DC31 File Offset: 0x0000BE31
		public bool ClearAchievement(string name)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_ClearAchievement(name);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000DC3F File Offset: 0x0000BE3F
		public bool GetAchievementAndUnlockTime(string name, out bool achieved, out uint unlockTime)
		{
			base.CheckIfUsable();
			achieved = false;
			unlockTime = 0U;
			return NativeMethods.Stats_GetAchievementAndUnlockTime(name, ref achieved, ref unlockTime);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public StatsGetAchievementAndUnlockTimeResult GetAchievementAndUnlockTime(SteamID steamID, string name)
		{
			StatsGetAchievementAndUnlockTimeResult result = default(StatsGetAchievementAndUnlockTimeResult);
			result.result = this.GetUserAchievementAndUnlockTime(steamID, name, out result.achievedSender, out result.unlockTimeSender);
			return result;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0000DC8B File Offset: 0x0000BE8B
		public bool StoreStats()
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_StoreStats();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000DC98 File Offset: 0x0000BE98
		public string GetAchievementDisplayAttribute(string name, string key)
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Stats_GetAchievementDisplayAttribute(name, key);
			return NativeHelpers.ToStringAnsi(pointer);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0000DCB9 File Offset: 0x0000BEB9
		public bool IndicateAchievementProgress(string name, uint currentProgress, uint maxProgress)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_IndicateAchievementProgress(name, currentProgress, maxProgress);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0000DCC9 File Offset: 0x0000BEC9
		public void RequestUserStats(SteamID steamID)
		{
			base.CheckIfUsable();
			NativeMethods.Stats_RequestUserStats(steamID.AsUInt64);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0000DCDD File Offset: 0x0000BEDD
		public bool GetUserStat(SteamID steamID, string name, out int data)
		{
			base.CheckIfUsable();
			data = 0;
			return NativeMethods.Stats_GetUserStatInt(steamID.AsUInt64, name, ref data);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public StatsGetUserStatResult GetUserStatInt(SteamID steamID, string name)
		{
			StatsGetUserStatResult result = default(StatsGetUserStatResult);
			result.result = this.GetUserStat(steamID, name, out result.intDataSender);
			return result;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0000DD24 File Offset: 0x0000BF24
		public bool GetUserStat(SteamID steamID, string name, out float data)
		{
			base.CheckIfUsable();
			data = 0f;
			return NativeMethods.Stats_GetUserStatFloat(steamID.AsUInt64, name, ref data);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public StatsGetUserStatResult GetUserStatFloat(SteamID steamID, string name)
		{
			StatsGetUserStatResult result = default(StatsGetUserStatResult);
			result.result = this.GetUserStat(steamID, name, out result.floatDataSender);
			return result;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public bool GetUserAchievement(SteamID steamID, string name, out bool achieved)
		{
			base.CheckIfUsable();
			achieved = false;
			return NativeMethods.Stats_GetUserAchievement(steamID.AsUInt64, name, ref achieved);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		public StatsGetUserAchievementResult GetUserAchievement(SteamID steamID, string name)
		{
			StatsGetUserAchievementResult result = default(StatsGetUserAchievementResult);
			result.result = this.GetUserAchievement(steamID, name, out result.sender);
			return result;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		public bool GetUserAchievementAndUnlockTime(SteamID steamID, string name, out bool achieved, out uint unlockTime)
		{
			base.CheckIfUsable();
			achieved = false;
			unlockTime = 0U;
			return NativeMethods.Stats_GetUserAchievementAndUnlockTime(steamID.AsUInt64, name, ref achieved, ref unlockTime);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public StatsGetUserAchievementAndUnlockTimeResult GetUserAchievementAndUnlockTime(SteamID steamID, string name)
		{
			StatsGetUserAchievementAndUnlockTimeResult result = default(StatsGetUserAchievementAndUnlockTimeResult);
			result.result = this.GetUserAchievementAndUnlockTime(steamID, name, out result.achievedSender, out result.unlockTimeSender);
			return result;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000DE0B File Offset: 0x0000C00B
		public bool ResetAllStats(bool achievementsToo)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_ResetAllStats(achievementsToo);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000DE19 File Offset: 0x0000C019
		public void FindOrCreateLeaderboard(string name, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType)
		{
			base.CheckIfUsable();
			NativeMethods.Stats_FindOrCreateLeaderboard(name, (int)sortMethod, (int)displayType);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000DE29 File Offset: 0x0000C029
		public void FindLeaderboard(string name)
		{
			base.CheckIfUsable();
			NativeMethods.Stats_FindLeaderboard(name);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000DE38 File Offset: 0x0000C038
		public string GetLeaderboardName(LeaderboardHandle handle)
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Stats_GetLeaderboardName(handle.AsUInt64);
			return NativeHelpers.ToStringAnsi(pointer);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000DE5E File Offset: 0x0000C05E
		public int GetLeaderboardEntryCount(LeaderboardHandle handle)
		{
			base.CheckIfUsable();
			return NativeMethods.Stats_GetLeaderboardEntryCount(handle.AsUInt64);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000DE72 File Offset: 0x0000C072
		public LeaderboardSortMethod GetLeaderboardSortMethod(LeaderboardHandle handle)
		{
			base.CheckIfUsable();
			return (LeaderboardSortMethod)NativeMethods.Stats_GetLeaderboardSortMethod(handle.AsUInt64);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0000DE86 File Offset: 0x0000C086
		public LeaderboardDisplayType GetLeaderboardDisplayType(LeaderboardHandle handle)
		{
			base.CheckIfUsable();
			return (LeaderboardDisplayType)NativeMethods.Stats_GetLeaderboardDisplayType(handle.AsUInt64);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000DE9A File Offset: 0x0000C09A
		public void DownloadLeaderboardEntries(LeaderboardHandle handle, LeaderboardDataRequest dataRequest, int rangeStart, int rangeEnd)
		{
			base.CheckIfUsable();
			NativeMethods.Stats_DownloadLeaderboardEntries(handle.AsUInt64, (int)dataRequest, rangeStart, rangeEnd);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		public void DownloadLeaderboardEntriesForUsers(LeaderboardHandle handle, SteamID[] users)
		{
			base.CheckIfUsable();
			byte[] managedData = NativeBuffer.ToBytes(users);
			using (NativeBuffer nativeBuffer = new NativeBuffer(managedData))
			{
				nativeBuffer.WriteToUnmanagedMemory();
				NativeMethods.Stats_DownloadLeaderboardEntriesForUsers(handle.AsUInt64, nativeBuffer.UnmanagedMemory, users.Length);
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000DF0C File Offset: 0x0000C10C
		public bool GetDownloadedLeaderboardEntry(LeaderboardEntriesHandle entries, int index, out LeaderboardEntry entry, int[] details)
		{
			base.CheckIfUsable();
			int num = (details == null) ? 0 : details.Length;
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(LeaderboardEntry))))
			{
				using (NativeBuffer nativeBuffer2 = new NativeBuffer(num * 4))
				{
					bool flag = NativeMethods.Stats_GetDownloadedLeaderboardEntry(entries.AsUInt64, index, nativeBuffer.UnmanagedMemory, nativeBuffer2.UnmanagedMemory, num);
					entry = LeaderboardEntry.Create(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
					for (int i = 0; i < num; i++)
					{
						details[i] = Marshal.ReadInt32(nativeBuffer2.UnmanagedMemory, 4 * i);
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public void UploadLeaderboardScore(LeaderboardHandle handle, LeaderboardUploadScoreMethod scoreMethod, int score, int[] details)
		{
			base.CheckIfUsable();
			using (NativeBuffer nativeBuffer = new NativeBuffer(NativeBuffer.ToBytes(details)))
			{
				nativeBuffer.WriteToUnmanagedMemory();
				NativeMethods.Stats_UploadLeaderboardScore(handle.AsUInt64, (int)scoreMethod, score, nativeBuffer.UnmanagedMemory, details.Length);
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0000E034 File Offset: 0x0000C234
		public void AttachLeaderboardUGC(LeaderboardHandle handle, UGCHandle ugcHandle)
		{
			base.CheckIfUsable();
			NativeMethods.Stats_AttachLeaderboardUGC(handle.AsUInt64, ugcHandle.AsUInt64);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0000E04F File Offset: 0x0000C24F
		public void GetNumberOfCurrentPlayers()
		{
			base.CheckIfUsable();
			NativeMethods.Stats_GetNumberOfCurrentPlayers();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0000E05C File Offset: 0x0000C25C
		public void RequestGlobalAchievementPercentages()
		{
			base.CheckIfUsable();
			NativeMethods.Stats_RequestGlobalAchievementPercentages();
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000E06C File Offset: 0x0000C26C
		public int GetMostAchievedAchievementInfo(out string name, out float percent, out bool achieved)
		{
			base.CheckIfUsable();
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(128))
			{
				percent = 0f;
				achieved = false;
				int num = NativeMethods.Stats_GetMostAchievedAchievementInfo(nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize, ref percent, ref achieved);
				name = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = num;
			}
			return result;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		public StatsGetMostAchievedAchievementInfo GetMostAchievedAchievementInfo()
		{
			StatsGetMostAchievedAchievementInfo result = default(StatsGetMostAchievedAchievementInfo);
			result.result = this.GetMostAchievedAchievementInfo(out result.nameSender, out result.percentSender, out result.achievedSender);
			return result;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000E10C File Offset: 0x0000C30C
		public int GetNextMostAchievedAchievementInfo(int iterator, out string name, out float percent, out bool achieved)
		{
			base.CheckIfUsable();
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(128))
			{
				percent = 0f;
				achieved = false;
				iterator = NativeMethods.Stats_GetNextMostAchievedAchievementInfo(iterator, nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize, ref percent, ref achieved);
				name = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = iterator;
			}
			return result;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000E178 File Offset: 0x0000C378
		public StatsGetNextMostAchievedAchievementInfo GetNextMostAchievedAchievementInfo(int iterator)
		{
			StatsGetNextMostAchievedAchievementInfo result = default(StatsGetNextMostAchievedAchievementInfo);
			result.result = this.GetNextMostAchievedAchievementInfo(iterator, out result.nameSender, out result.percentSender, out result.achievedSender);
			return result;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
		public bool GetAchievementAchievedPercent(string name, out float percent)
		{
			base.CheckIfUsable();
			percent = 0f;
			return NativeMethods.Stats_GetAchievementAchievedPercent(name, ref percent);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		public StatsGetAchievmentAchievedPercent GetAchievementAchievedPercent(string name)
		{
			StatsGetAchievmentAchievedPercent result = default(StatsGetAchievmentAchievedPercent);
			result.result = this.GetAchievementAchievedPercent(name, out result.sender);
			return result;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000E1F3 File Offset: 0x0000C3F3
		public void RequestGlobalStats(int historyDays)
		{
			base.CheckIfUsable();
			NativeMethods.Stats_RequestGlobalStats(historyDays);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000E201 File Offset: 0x0000C401
		public bool GetGlobalStat(string name, out long data)
		{
			base.CheckIfUsable();
			data = 0L;
			return NativeMethods.Stats_GetGlobalStatInt(name, ref data);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000E214 File Offset: 0x0000C414
		public StatsGetGlobalStat GetGlobalStatLong(string name)
		{
			StatsGetGlobalStat result = default(StatsGetGlobalStat);
			result.result = this.GetGlobalStat(name, out result.longSender);
			return result;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000E23F File Offset: 0x0000C43F
		public bool GetGlobalStat(string name, out double data)
		{
			base.CheckIfUsable();
			data = 0.0;
			return NativeMethods.Stats_GetGlobalStatDouble(name, ref data);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0000E25C File Offset: 0x0000C45C
		public StatsGetGlobalStat GetGlobalStatDouble(string name)
		{
			StatsGetGlobalStat result = default(StatsGetGlobalStat);
			result.result = this.GetGlobalStat(name, out result.doubleSender);
			return result;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000E288 File Offset: 0x0000C488
		public int GetGlobalStatHistory(string name, out long[] data, int historyDays)
		{
			base.CheckIfUsable();
			byte[] array = new byte[historyDays * 8];
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(array))
			{
				int num = NativeMethods.Stats_GetGlobalStatHistoryInt(name, nativeBuffer.UnmanagedMemory, (uint)historyDays);
				nativeBuffer.ReadFromUnmanagedMemory(num);
				data = NativeBuffer.ToLong(array);
				result = num;
			}
			return result;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		public StatsGetGlobalStatHistory GetGlobalStatHistoryLong(string name, int historyDays)
		{
			StatsGetGlobalStatHistory result = default(StatsGetGlobalStatHistory);
			result.result = this.GetGlobalStatHistory(name, out result.longSender, historyDays);
			return result;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0000E314 File Offset: 0x0000C514
		public int GetGlobalStatHistory(string name, out double[] data, int historyDays)
		{
			base.CheckIfUsable();
			byte[] array = new byte[historyDays * 8];
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(array))
			{
				int num = NativeMethods.Stats_GetGlobalStatHistoryDouble(name, nativeBuffer.UnmanagedMemory, (uint)historyDays);
				nativeBuffer.ReadFromUnmanagedMemory(num);
				data = NativeBuffer.ToDouble(array);
				result = num;
			}
			return result;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000E374 File Offset: 0x0000C574
		public StatsGetGlobalStatHistory GetGlobalStatHistoryDouble(string name, int historyDays)
		{
			StatsGetGlobalStatHistory result = default(StatsGetGlobalStatHistory);
			result.result = this.GetGlobalStatHistory(name, out result.doubleSender, historyDays);
			return result;
		}

		// Token: 0x04000573 RID: 1395
		private List<UserStatsReceived> userStatsReceived;

		// Token: 0x04000574 RID: 1396
		private List<UserStatsStored> userStatsStored;

		// Token: 0x04000575 RID: 1397
		private List<UserAchievementStored> userAchievementStored;

		// Token: 0x04000576 RID: 1398
		private List<SteamService.Result<LeaderboardFindResult>> leaderboardFindResult;

		// Token: 0x04000577 RID: 1399
		private List<SteamService.Result<LeaderboardScoresDownloaded>> leaderboardScoresDownloaded;

		// Token: 0x04000578 RID: 1400
		private List<SteamService.Result<LeaderboardScoreUploaded>> leaderboardScoresUploaded;

		// Token: 0x04000579 RID: 1401
		private List<SteamService.Result<LeaderboardUGCSet>> leaderboardUGCSet;

		// Token: 0x0400057A RID: 1402
		private List<SteamService.Result<NumberOfCurrentPlayers>> numberOfCurrentPlayers;

		// Token: 0x0400057B RID: 1403
		private List<UserStatsUnloaded> userStatsUnloaded;

		// Token: 0x0400057C RID: 1404
		private List<UserAchievementIconFetched> userAchievementIconFetched;

		// Token: 0x0400057D RID: 1405
		private List<SteamService.Result<GlobalAchievementPercentagesReady>> globalAchievementPercentages;

		// Token: 0x0400057E RID: 1406
		private List<SteamService.Result<GlobalStatsReceived>> globalStatsReceived;
	}
}
