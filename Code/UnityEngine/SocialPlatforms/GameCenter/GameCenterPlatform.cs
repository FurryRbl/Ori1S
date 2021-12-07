using System;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	// Token: 0x0200028C RID: 652
	public class GameCenterPlatform : Local
	{
		// Token: 0x060025AA RID: 9642 RVA: 0x00034418 File Offset: 0x00032618
		public static void ResetAllAchievements(Action<bool> callback)
		{
			Debug.Log("ResetAllAchievements - no effect in editor");
			callback(true);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0003442C File Offset: 0x0003262C
		public static void ShowDefaultAchievementCompletionBanner(bool value)
		{
			Debug.Log("ShowDefaultAchievementCompletionBanner - no effect in editor");
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00034438 File Offset: 0x00032638
		public static void ShowLeaderboardUI(string leaderboardID, TimeScope timeScope)
		{
			Debug.Log("ShowLeaderboardUI - no effect in editor");
		}
	}
}
