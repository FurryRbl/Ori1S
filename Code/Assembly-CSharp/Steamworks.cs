using System;
using System.Collections;
using System.Collections.Generic;
using ManagedSteam;
using ManagedSteam.CallbackStructures;
using ManagedSteam.Exceptions;
using ManagedSteam.SteamTypes;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class Steamworks : MonoBehaviour
{
	// Token: 0x1700035D RID: 861
	// (get) Token: 0x0600133D RID: 4925 RVA: 0x00059549 File Offset: 0x00057749
	// (set) Token: 0x0600133E RID: 4926 RVA: 0x00059550 File Offset: 0x00057750
	public static Steam SteamInterface { get; private set; }

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x0600133F RID: 4927 RVA: 0x00059558 File Offset: 0x00057758
	public SteamID SteamId
	{
		get
		{
			return this.m_steamID;
		}
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x00059560 File Offset: 0x00057760
	public LeaderboardHandle GetLeaderboardHandle(LeaderboardB leaderboard)
	{
		LeaderboardHandle result;
		if (this.m_leaderboardHandles.TryGetValue(leaderboard, out result))
		{
			return result;
		}
		return default(LeaderboardHandle);
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06001341 RID: 4929 RVA: 0x0005958B File Offset: 0x0005778B
	public bool StatsReceived
	{
		get
		{
			return this.m_statsReceived;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06001342 RID: 4930 RVA: 0x00059593 File Offset: 0x00057793
	// (set) Token: 0x06001343 RID: 4931 RVA: 0x0005959A File Offset: 0x0005779A
	public static bool EnableAchievements { get; set; }

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06001344 RID: 4932 RVA: 0x000595A2 File Offset: 0x000577A2
	// (set) Token: 0x06001345 RID: 4933 RVA: 0x000595A9 File Offset: 0x000577A9
	public static bool EnableLeaderboards { get; set; }

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06001346 RID: 4934 RVA: 0x000595B1 File Offset: 0x000577B1
	public static bool IsLoggedIn
	{
		get
		{
			return Steamworks.Instance && Steamworks.Instance.m_isLoggedIn;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06001347 RID: 4935 RVA: 0x000595CE File Offset: 0x000577CE
	public static string Username
	{
		get
		{
			if (!Steamworks.Ready)
			{
				return string.Empty;
			}
			return Steam.Instance.Friends.GetPlayerNickname(Steamworks.Instance.m_steamID);
		}
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x000595FC File Offset: 0x000577FC
	private void Awake()
	{
		if (GameController.Instance.IsDemo)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		if (Steamworks.SteamInterface != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		bool flag = false;
		Steamworks.EnableLeaderboards = flag;
		Steamworks.EnableAchievements = flag;
		bool flag2 = false;
		try
		{
			Steamworks.SteamInterface = Steam.Initialize();
		}
		catch (AlreadyLoadedException ex)
		{
			flag2 = true;
		}
		catch (SteamInitializeFailedException ex2)
		{
			flag2 = true;
		}
		catch (SteamInterfaceInitializeFailedException ex3)
		{
			flag2 = true;
		}
		catch (DllNotFoundException ex4)
		{
			flag2 = true;
		}
		if (flag2)
		{
			Steamworks.SteamInterface = null;
			UnityEngine.Object.Destroy(this);
			return;
		}
		this.m_steamID = Steamworks.SteamInterface.User.GetSteamID();
		this.m_isLoggedIn = Steamworks.SteamInterface.User.IsLoggedOn();
		bool flag3 = Steamworks.SteamInterface.Apps.IsSubscribedApp(Steamworks.SteamInterface.AppID);
		if (Steamworks.SteamInterface.AppID == Steamworks.TrialDEAppID)
		{
			if (flag3)
			{
			}
		}
		else if (Steam.RestartAppIfNecessary(Steamworks.FullDEAppID.AsUInt32))
		{
			Application.Quit();
			return;
		}
		if (!flag3)
		{
			Steamworks.SteamInterface = null;
			UnityEngine.Object.Destroy(this);
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
		Steamworks.Instance = this;
		Steamworks.SteamInterface.ExceptionThrown += this.ExceptionThrown;
		Steamworks.SteamInterface.Friends.GameOverlayActivated += this.OverlayToggle;
		if (!this.m_isLoggedIn)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		Steamworks.EnableAchievements = !GameController.Instance.IsDemo;
		Steamworks.EnableLeaderboards = (!GameController.Instance.IsTrial && !GameController.Instance.IsDemo);
		this.m_stats = Steamworks.SteamInterface.Stats;
		this.m_stats.UserStatsReceived += this.UserStatsReceived;
		this.m_utils = Steamworks.SteamInterface.Utils;
		this.m_utils.SteamShutdown += this.SteamShutdownFunc;
		if (Steamworks.EnableLeaderboards)
		{
			Steamworks.SteamInterface.Stats.LeaderboardFindResult += this.LeaderboardFindResult;
			Steamworks.SteamInterface.Stats.LeaderboardScoreUploaded += this.LeaderboardScoreUploaded;
			Steamworks.SteamInterface.Stats.LeaderboardScoresDownloaded += this.LeaderboardScoresDownloaded;
			this.m_leaderboardToSteamLeaderboard["explorersLeaderboardV1Easy"] = LeaderboardB.ExplorerEasy;
			this.m_leaderboardToSteamLeaderboard["speedRunnersLeaderboardV1Easy"] = LeaderboardB.SpeedRunnerEasy;
			this.m_leaderboardToSteamLeaderboard["globalLeaderboardV1Easy"] = LeaderboardB.SurvivorEasy;
			this.m_leaderboardToSteamLeaderboard["explorersLeaderboardV1Normal"] = LeaderboardB.ExplorerNormal;
			this.m_leaderboardToSteamLeaderboard["speedRunnersLeaderboardV1Normal"] = LeaderboardB.SpeedRunnerNormal;
			this.m_leaderboardToSteamLeaderboard["globalLeaderboardV1Normal"] = LeaderboardB.SurvivorNormal;
			this.m_leaderboardToSteamLeaderboard["explorersLeaderboardV1Hard"] = LeaderboardB.ExplorerHard;
			this.m_leaderboardToSteamLeaderboard["speedRunnersLeaderboardV1Hard"] = LeaderboardB.SpeedRunnerHard;
			this.m_leaderboardToSteamLeaderboard["globalLeaderboardV1Hard"] = LeaderboardB.SurvivorHard;
			this.m_leaderboardToSteamLeaderboard["explorersLeaderboardV1OneLife"] = LeaderboardB.ExplorerOneLife;
			this.m_leaderboardToSteamLeaderboard["speedRunnersLeaderboardV1OneLife"] = LeaderboardB.SpeedRunnerOneLife;
			this.m_leaderboardToSteamLeaderboard["globalLeaderboardV1OneLife"] = LeaderboardB.SurvivorOneLife;
			base.StartCoroutine(this.FindLeaderboards());
		}
		Language currentGameLanguage = this.GetCurrentGameLanguage();
		if (currentGameLanguage != GameSettings.Instance.Language)
		{
			GameSettings.Instance.Language = currentGameLanguage;
		}
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x00059984 File Offset: 0x00057B84
	private Language GetCurrentGameLanguage()
	{
		Language result = Language.English;
		string currentGameLanguage = Steamworks.SteamInterface.Apps.GetCurrentGameLanguage();
		string text = currentGameLanguage;
		switch (text)
		{
		case "english":
			result = Language.English;
			break;
		case "german":
			result = Language.German;
			break;
		case "french":
			result = Language.French;
			break;
		case "italian":
			result = Language.Italian;
			break;
		case "spanish":
			result = Language.Spanish;
			break;
		case "simplified chinese":
			result = Language.Chinese;
			break;
		case "schinese":
			result = Language.Chinese;
			break;
		case "russian":
			result = Language.Russian;
			break;
		case "japanese":
			result = Language.Japanese;
			break;
		case "portuguese":
			result = Language.English;
			break;
		case "brazilian":
			result = Language.Portuguese;
			break;
		}
		return result;
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x00059AE4 File Offset: 0x00057CE4
	private IEnumerator FindLeaderboards()
	{
		float time = Time.realtimeSinceStartup;
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("explorersLeaderboardV1Easy");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("speedRunnersLeaderboardV1Easy");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("globalLeaderboardV1Easy");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("explorersLeaderboardV1Normal");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("speedRunnersLeaderboardV1Normal");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("globalLeaderboardV1Normal");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("explorersLeaderboardV1Hard");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("speedRunnersLeaderboardV1Hard");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("globalLeaderboardV1Hard");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("explorersLeaderboardV1OneLife");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("speedRunnersLeaderboardV1OneLife");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_findingLeaderboard = true;
		Steamworks.SteamInterface.Stats.FindLeaderboard("globalLeaderboardV1OneLife");
		while (this.m_findingLeaderboard)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return null;
		yield break;
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x0600134B RID: 4939 RVA: 0x00059AFF File Offset: 0x00057CFF
	public static bool Ready
	{
		get
		{
			return Steamworks.Instance != null;
		}
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x00059B0C File Offset: 0x00057D0C
	private void OverlayToggle(GameOverlayActivated value)
	{
		if (value.Active)
		{
			SuspensionManager.SuspendAll();
		}
		else
		{
			SuspensionManager.ResumeAll();
		}
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x00059B29 File Offset: 0x00057D29
	private void ExceptionThrown(Exception e)
	{
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x0600134E RID: 4942 RVA: 0x00059B2B File Offset: 0x00057D2B
	// (set) Token: 0x0600134F RID: 4943 RVA: 0x00059B32 File Offset: 0x00057D32
	public static float CurrentMapPedestalsFoundCount
	{
		get
		{
			return Steamworks.m_currentMapPedestalsFoundCount;
		}
		set
		{
			Steamworks.m_currentMapPedestalsFoundCount = value;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06001350 RID: 4944 RVA: 0x00059B3A File Offset: 0x00057D3A
	// (set) Token: 0x06001351 RID: 4945 RVA: 0x00059B41 File Offset: 0x00057D41
	public static float CurrentSecretsFoundCount
	{
		get
		{
			return Steamworks.m_currentSecretsFoundCount;
		}
		set
		{
			Steamworks.m_currentSecretsFoundCount = value;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06001352 RID: 4946 RVA: 0x00059B49 File Offset: 0x00057D49
	// (set) Token: 0x06001353 RID: 4947 RVA: 0x00059B50 File Offset: 0x00057D50
	public static float SoulLinkCaseCount
	{
		get
		{
			return Steamworks.m_soulLinkCaseCount;
		}
		set
		{
			Steamworks.m_soulLinkCaseCount = value;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06001354 RID: 4948 RVA: 0x00059B58 File Offset: 0x00057D58
	// (set) Token: 0x06001355 RID: 4949 RVA: 0x00059B5F File Offset: 0x00057D5F
	public static float EnergyCollectedCount
	{
		get
		{
			return Steamworks.m_energyCollectedCount;
		}
		set
		{
			Steamworks.m_energyCollectedCount = value;
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06001356 RID: 4950 RVA: 0x00059B67 File Offset: 0x00057D67
	// (set) Token: 0x06001357 RID: 4951 RVA: 0x00059B6E File Offset: 0x00057D6E
	public static float EnemiesKilledBySpiritFlameCount
	{
		get
		{
			return Steamworks.m_enemiesKilledBySpiritFlameCount;
		}
		set
		{
			Steamworks.m_enemiesKilledBySpiritFlameCount = value;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06001358 RID: 4952 RVA: 0x00059B76 File Offset: 0x00057D76
	// (set) Token: 0x06001359 RID: 4953 RVA: 0x00059B7D File Offset: 0x00057D7D
	public static float EnemiesKilledByChargeFlame
	{
		get
		{
			return Steamworks.m_enemiesKilledByChargeFlameCount;
		}
		set
		{
			Steamworks.m_enemiesKilledByChargeFlameCount = value;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x0600135A RID: 4954 RVA: 0x00059B85 File Offset: 0x00057D85
	// (set) Token: 0x0600135B RID: 4955 RVA: 0x00059B8C File Offset: 0x00057D8C
	public static float EnemiesKilledByStompCount
	{
		get
		{
			return Steamworks.m_enemiesKilledByStompCount;
		}
		set
		{
			Steamworks.m_enemiesKilledByStompCount = value;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x0600135C RID: 4956 RVA: 0x00059B94 File Offset: 0x00057D94
	// (set) Token: 0x0600135D RID: 4957 RVA: 0x00059B9B File Offset: 0x00057D9B
	public static float EnemiesKilledByReflectedBashProjectilesCount
	{
		get
		{
			return Steamworks.m_enemiesKilledByReflectedBashProjectilesCount;
		}
		set
		{
			Steamworks.m_enemiesKilledByReflectedBashProjectilesCount = value;
		}
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x00059BA4 File Offset: 0x00057DA4
	private void UserStatsReceived(UserStatsReceived value)
	{
		if (value.GameID != new GameID(Steamworks.SteamInterface.AppID.AsUInt64))
		{
			return;
		}
		if (value.Result != Result.OK)
		{
			return;
		}
		this.m_statsReceived = true;
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x00059BEF File Offset: 0x00057DEF
	private void SteamShutdownFunc(SteamShutdown value)
	{
		Application.Quit();
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06001360 RID: 4960 RVA: 0x00059BF6 File Offset: 0x00057DF6
	public static LeaderboardData LeaderboardData
	{
		get
		{
			if (!Steamworks.Instance)
			{
				return null;
			}
			return (Steamworks.EnableLeaderboards && Steamworks.Ready) ? Steamworks.Instance.m_leaderboardData : null;
		}
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x00059C30 File Offset: 0x00057E30
	private void LeaderboardFindResult(LeaderboardFindResult result, bool failed)
	{
		this.m_findingLeaderboard = false;
		if (failed)
		{
			return;
		}
		string leaderboardName = Steamworks.SteamInterface.Stats.GetLeaderboardName(result.Leaderboard);
		LeaderboardB key = this.m_leaderboardToSteamLeaderboard[leaderboardName];
		this.m_leaderboardHandles[key] = result.Leaderboard;
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x00059C82 File Offset: 0x00057E82
	private void LeaderboardScoreUploaded(LeaderboardScoreUploaded result, bool failed)
	{
		if (failed)
		{
			return;
		}
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x00059C8C File Offset: 0x00057E8C
	private void LeaderboardScoresDownloaded(LeaderboardScoresDownloaded result, bool failed)
	{
		if (failed && this.m_onLeaderboardUpdatedFailure != null)
		{
			this.m_onLeaderboardUpdatedFailure();
			this.m_onLeaderboardUpdatedFailure = null;
			return;
		}
		int leaderboardEntryCount = Steamworks.SteamInterface.Stats.GetLeaderboardEntryCount(this.GetLeaderboardHandle(this.m_loadingLeaderboard));
		List<LeaderboardData.Entry> list = new List<LeaderboardData.Entry>();
		Leaderboard leaderboard = LeaderboardUtility.LeaderboardBToLeaderboard(this.m_loadingLeaderboard);
		for (int i = 0; i < result.EntryCount; i++)
		{
			int[] array = new int[1];
			LeaderboardEntry leaderboardEntry;
			Steamworks.SteamInterface.Stats.GetDownloadedLeaderboardEntry(result.Entries, i, out leaderboardEntry, array);
			ulong num = (ulong)((long)leaderboardEntry.Score + -2147483648L << 32 | (long)((ulong)array[0]));
			num >>= 25;
			list.Add(new LeaderboardData.Entry(leaderboard, (uint)leaderboardEntry.Rank, (long)num, leaderboardEntry.User.ToString(), Steamworks.SteamInterface.Friends.GetFriendPersonaName(leaderboardEntry.User)));
		}
		this.m_leaderboardData = new LeaderboardData(leaderboard, this.m_loadingLeaderboardFilter, string.Empty, (uint)leaderboardEntryCount, list);
		this.m_updatingLeaderboard = false;
		if (this.m_onLeaderboardUpdatedSuccess != null)
		{
			this.m_onLeaderboardUpdatedSuccess();
			this.m_onLeaderboardUpdatedSuccess = null;
		}
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x00059DC8 File Offset: 0x00057FC8
	public static bool UpdateLeaderboard(LeaderboardB leaderboard, Leaderboards.Filter filter, Action success = null, Action failure = null)
	{
		if (!Steamworks.EnableLeaderboards || !Steamworks.Ready || Steamworks.Instance.m_updatingLeaderboard)
		{
			return false;
		}
		Steamworks.Instance.m_updatingLeaderboard = true;
		Steamworks.Instance.m_onLeaderboardUpdatedSuccess = success;
		Steamworks.Instance.m_onLeaderboardUpdatedFailure = failure;
		Steamworks.Instance.m_loadingLeaderboard = leaderboard;
		Steamworks.Instance.m_loadingLeaderboardFilter = filter;
		LeaderboardHandle leaderboardHandle = Steamworks.Instance.GetLeaderboardHandle(leaderboard);
		if (leaderboardHandle.AsUInt64 == 0UL)
		{
			Steamworks.Instance.m_updatingLeaderboard = false;
			return false;
		}
		switch (filter)
		{
		case Leaderboards.Filter.Overall:
			Steamworks.SteamInterface.Stats.DownloadLeaderboardEntries(leaderboardHandle, LeaderboardDataRequest.Global, 0, 10);
			break;
		case Leaderboards.Filter.Friends:
			Steamworks.SteamInterface.Stats.DownloadLeaderboardEntries(leaderboardHandle, LeaderboardDataRequest.Friends, -4, 5);
			break;
		case Leaderboards.Filter.MyScore:
			Steamworks.SteamInterface.Stats.DownloadLeaderboardEntries(leaderboardHandle, LeaderboardDataRequest.GlobalAroundUser, -4, 5);
			break;
		default:
			throw new ArgumentException("Unhandled leaderboard filter: " + filter);
		}
		return true;
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x00059ED4 File Offset: 0x000580D4
	public static void SendLeaderboardData(LeaderboardB leaderboard, ulong data)
	{
		data <<= 25;
		int data2 = (int)((data >> 32) - 18446744071562067968UL);
		int[] details = new int[]
		{
			(int)(data & (ulong)-1)
		};
		Steamworks.SendLeaderboardData(leaderboard, data2, details);
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x00059F0C File Offset: 0x0005810C
	public static void SendLeaderboardData(LeaderboardB leaderboard, int data, int[] details)
	{
		if (CheatsHandler.DebugWasEnabled)
		{
			return;
		}
		if (Steamworks.Ready)
		{
			LeaderboardHandle leaderboardHandle = Steamworks.Instance.GetLeaderboardHandle(leaderboard);
			if (leaderboardHandle.AsUInt64 == 0UL)
			{
				return;
			}
			Steamworks.SteamInterface.Stats.UploadLeaderboardScore(leaderboardHandle, LeaderboardUploadScoreMethod.KeepBest, data, details);
			Steamworks.SteamInterface.Stats.StoreStats();
		}
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x00059F6F File Offset: 0x0005816F
	private void Update()
	{
		if (Steamworks.SteamInterface != null)
		{
			Steamworks.SteamInterface.Update();
		}
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x00059F85 File Offset: 0x00058185
	private void OnDestroy()
	{
		if (Steamworks.Instance == this)
		{
			Steamworks.Instance = null;
			this.Cleanup();
		}
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x00059FA3 File Offset: 0x000581A3
	private void OnApplicationQuit()
	{
		this.Cleanup();
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x00059FAC File Offset: 0x000581AC
	private void Cleanup()
	{
		if (Steamworks.SteamInterface != null)
		{
			if (Application.isEditor)
			{
				Steamworks.SteamInterface.ReleaseManagedResources();
			}
			else
			{
				Steamworks.SteamInterface.Shutdown();
			}
			Steamworks.SteamInterface = null;
		}
	}

	// Token: 0x04001120 RID: 4384
	private const string m_explorersLeaderboardEasyString = "explorersLeaderboardV1Easy";

	// Token: 0x04001121 RID: 4385
	private const string m_speedRunnersLeaderboardEasyString = "speedRunnersLeaderboardV1Easy";

	// Token: 0x04001122 RID: 4386
	private const string m_survivorLeaderboardEasyString = "globalLeaderboardV1Easy";

	// Token: 0x04001123 RID: 4387
	private const string m_explorersLeaderboardNormalString = "explorersLeaderboardV1Normal";

	// Token: 0x04001124 RID: 4388
	private const string m_speedRunnersLeaderboardNormalString = "speedRunnersLeaderboardV1Normal";

	// Token: 0x04001125 RID: 4389
	private const string m_survivorLeaderboardNormalString = "globalLeaderboardV1Normal";

	// Token: 0x04001126 RID: 4390
	private const string m_explorersLeaderboardHardString = "explorersLeaderboardV1Hard";

	// Token: 0x04001127 RID: 4391
	private const string m_speedRunnersLeaderboardHardString = "speedRunnersLeaderboardV1Hard";

	// Token: 0x04001128 RID: 4392
	private const string m_survivorLeaderboardHardString = "globalLeaderboardV1Hard";

	// Token: 0x04001129 RID: 4393
	private const string m_explorersLeaderboardOneLifeString = "explorersLeaderboardV1OneLife";

	// Token: 0x0400112A RID: 4394
	private const string m_speedRunnersLeaderboardOneLifeString = "speedRunnersLeaderboardV1OneLife";

	// Token: 0x0400112B RID: 4395
	private const string m_survivorLeaderboardOneLifeString = "globalLeaderboardV1OneLife";

	// Token: 0x0400112C RID: 4396
	public static Steamworks Instance;

	// Token: 0x0400112D RID: 4397
	public static AppID TrialAppID = new AppID(424650U);

	// Token: 0x0400112E RID: 4398
	public static AppID FullAppID = new AppID(261570U);

	// Token: 0x0400112F RID: 4399
	public static AppID TrialDEAppID = new AppID(406380U);

	// Token: 0x04001130 RID: 4400
	public static AppID FullDEAppID = new AppID(387290U);

	// Token: 0x04001131 RID: 4401
	private IStats m_stats;

	// Token: 0x04001132 RID: 4402
	private IUser m_user;

	// Token: 0x04001133 RID: 4403
	private IUtils m_utils;

	// Token: 0x04001134 RID: 4404
	private SteamID m_steamID;

	// Token: 0x04001135 RID: 4405
	private bool m_isLoggedIn;

	// Token: 0x04001136 RID: 4406
	private bool m_gamepadTextInputDismissed;

	// Token: 0x04001137 RID: 4407
	private bool m_updatingLeaderboard;

	// Token: 0x04001138 RID: 4408
	private Dictionary<string, LeaderboardB> m_leaderboardToSteamLeaderboard = new Dictionary<string, LeaderboardB>();

	// Token: 0x04001139 RID: 4409
	private Dictionary<LeaderboardB, LeaderboardHandle> m_leaderboardHandles = new Dictionary<LeaderboardB, LeaderboardHandle>();

	// Token: 0x0400113A RID: 4410
	private Action m_onLeaderboardUpdatedSuccess;

	// Token: 0x0400113B RID: 4411
	private Action m_onLeaderboardUpdatedFailure;

	// Token: 0x0400113C RID: 4412
	private LeaderboardB m_loadingLeaderboard;

	// Token: 0x0400113D RID: 4413
	private Leaderboards.Filter m_loadingLeaderboardFilter;

	// Token: 0x0400113E RID: 4414
	private bool m_statsReceived;

	// Token: 0x0400113F RID: 4415
	private bool m_findingLeaderboard;

	// Token: 0x04001140 RID: 4416
	private static float m_currentMapPedestalsFoundCount = 2.1474836E+09f;

	// Token: 0x04001141 RID: 4417
	private static float m_currentSecretsFoundCount = 2.1474836E+09f;

	// Token: 0x04001142 RID: 4418
	private static float m_soulLinkCaseCount = 2.1474836E+09f;

	// Token: 0x04001143 RID: 4419
	private static float m_energyCollectedCount = 2.1474836E+09f;

	// Token: 0x04001144 RID: 4420
	private static float m_enemiesKilledBySpiritFlameCount = 2.1474836E+09f;

	// Token: 0x04001145 RID: 4421
	private static float m_enemiesKilledByChargeFlameCount = 2.1474836E+09f;

	// Token: 0x04001146 RID: 4422
	private static float m_enemiesKilledByStompCount = 2.1474836E+09f;

	// Token: 0x04001147 RID: 4423
	private static float m_enemiesKilledByReflectedBashProjectilesCount = 2.1474836E+09f;

	// Token: 0x04001148 RID: 4424
	private LeaderboardData m_leaderboardData;
}
