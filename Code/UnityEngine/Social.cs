using System;
using UnityEngine.SocialPlatforms;

namespace UnityEngine
{
	// Token: 0x020002E7 RID: 743
	public static class Social
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000362FC File Offset: 0x000344FC
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x00036304 File Offset: 0x00034504
		public static ISocialPlatform Active
		{
			get
			{
				return ActivePlatform.Instance;
			}
			set
			{
				ActivePlatform.Instance = value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x0003630C File Offset: 0x0003450C
		public static ILocalUser localUser
		{
			get
			{
				return Social.Active.localUser;
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00036318 File Offset: 0x00034518
		public static void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			Social.Active.LoadUsers(userIDs, callback);
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00036328 File Offset: 0x00034528
		public static void ReportProgress(string achievementID, double progress, Action<bool> callback)
		{
			Social.Active.ReportProgress(achievementID, progress, callback);
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00036338 File Offset: 0x00034538
		public static void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			Social.Active.LoadAchievementDescriptions(callback);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00036348 File Offset: 0x00034548
		public static void LoadAchievements(Action<IAchievement[]> callback)
		{
			Social.Active.LoadAchievements(callback);
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00036358 File Offset: 0x00034558
		public static void ReportScore(long score, string board, Action<bool> callback)
		{
			Social.Active.ReportScore(score, board, callback);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00036368 File Offset: 0x00034568
		public static void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			Social.Active.LoadScores(leaderboardID, callback);
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x00036378 File Offset: 0x00034578
		public static ILeaderboard CreateLeaderboard()
		{
			return Social.Active.CreateLeaderboard();
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x00036384 File Offset: 0x00034584
		public static IAchievement CreateAchievement()
		{
			return Social.Active.CreateAchievement();
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x00036390 File Offset: 0x00034590
		public static void ShowAchievementsUI()
		{
			Social.Active.ShowAchievementsUI();
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0003639C File Offset: 0x0003459C
		public static void ShowLeaderboardUI()
		{
			Social.Active.ShowLeaderboardUI();
		}
	}
}
