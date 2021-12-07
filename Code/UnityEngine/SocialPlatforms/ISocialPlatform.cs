using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002E9 RID: 745
	public interface ISocialPlatform
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600268C RID: 9868
		ILocalUser localUser { get; }

		// Token: 0x0600268D RID: 9869
		void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback);

		// Token: 0x0600268E RID: 9870
		void ReportProgress(string achievementID, double progress, Action<bool> callback);

		// Token: 0x0600268F RID: 9871
		void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback);

		// Token: 0x06002690 RID: 9872
		void LoadAchievements(Action<IAchievement[]> callback);

		// Token: 0x06002691 RID: 9873
		IAchievement CreateAchievement();

		// Token: 0x06002692 RID: 9874
		void ReportScore(long score, string board, Action<bool> callback);

		// Token: 0x06002693 RID: 9875
		void LoadScores(string leaderboardID, Action<IScore[]> callback);

		// Token: 0x06002694 RID: 9876
		ILeaderboard CreateLeaderboard();

		// Token: 0x06002695 RID: 9877
		void ShowAchievementsUI();

		// Token: 0x06002696 RID: 9878
		void ShowLeaderboardUI();

		// Token: 0x06002697 RID: 9879
		void Authenticate(ILocalUser user, Action<bool> callback);

		// Token: 0x06002698 RID: 9880
		void LoadFriends(ILocalUser user, Action<bool> callback);

		// Token: 0x06002699 RID: 9881
		void LoadScores(ILeaderboard board, Action<bool> callback);

		// Token: 0x0600269A RID: 9882
		bool GetLoading(ILeaderboard board);
	}
}
