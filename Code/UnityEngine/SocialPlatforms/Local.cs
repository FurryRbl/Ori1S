using System;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002E4 RID: 740
	public class Local : ISocialPlatform
	{
		// Token: 0x0600265F RID: 9823 RVA: 0x00035288 File Offset: 0x00033488
		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool> callback)
		{
			LocalUser localUser = (LocalUser)user;
			this.m_DefaultTexture = this.CreateDummyTexture(32, 32);
			this.PopulateStaticData();
			localUser.SetAuthenticated(true);
			localUser.SetUnderage(false);
			localUser.SetUserID("1000");
			localUser.SetUserName("Lerpz");
			localUser.SetImage(this.m_DefaultTexture);
			if (callback != null)
			{
				callback(true);
			}
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000352F0 File Offset: 0x000334F0
		void ISocialPlatform.LoadFriends(ILocalUser user, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			((LocalUser)user).SetFriends(this.m_Friends.ToArray());
			if (callback != null)
			{
				callback(true);
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x0003532C File Offset: 0x0003352C
		void ISocialPlatform.LoadScores(ILeaderboard board, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			Leaderboard leaderboard = (Leaderboard)board;
			foreach (Leaderboard leaderboard2 in this.m_Leaderboards)
			{
				if (leaderboard2.id == leaderboard.id)
				{
					leaderboard.SetTitle(leaderboard2.title);
					leaderboard.SetScores(leaderboard2.scores);
					leaderboard.SetMaxRange((uint)leaderboard2.scores.Length);
				}
			}
			this.SortScores(leaderboard);
			this.SetLocalPlayerScore(leaderboard);
			if (callback != null)
			{
				callback(true);
			}
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000353F8 File Offset: 0x000335F8
		bool ISocialPlatform.GetLoading(ILeaderboard board)
		{
			return this.VerifyUser() && ((Leaderboard)board).loading;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x00035414 File Offset: 0x00033614
		public ILocalUser localUser
		{
			get
			{
				if (Local.m_LocalUser == null)
				{
					Local.m_LocalUser = new LocalUser();
				}
				return Local.m_LocalUser;
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00035430 File Offset: 0x00033630
		public void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			List<UserProfile> list = new List<UserProfile>();
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (string b in userIDs)
			{
				foreach (UserProfile userProfile in this.m_Users)
				{
					if (userProfile.id == b)
					{
						list.Add(userProfile);
					}
				}
				foreach (UserProfile userProfile2 in this.m_Friends)
				{
					if (userProfile2.id == b)
					{
						list.Add(userProfile2);
					}
				}
			}
			callback(list.ToArray());
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x00035550 File Offset: 0x00033750
		public void ReportProgress(string id, double progress, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (Achievement achievement in this.m_Achievements)
			{
				if (achievement.id == id && achievement.percentCompleted <= progress)
				{
					if (progress >= 100.0)
					{
						achievement.SetCompleted(true);
					}
					achievement.SetHidden(false);
					achievement.SetLastReportedDate(DateTime.Now);
					achievement.percentCompleted = progress;
					if (callback != null)
					{
						callback(true);
					}
					return;
				}
			}
			foreach (AchievementDescription achievementDescription in this.m_AchievementDescriptions)
			{
				if (achievementDescription.id == id)
				{
					bool completed = progress >= 100.0;
					Achievement item = new Achievement(id, progress, completed, false, DateTime.Now);
					this.m_Achievements.Add(item);
					if (callback != null)
					{
						callback(true);
					}
					return;
				}
			}
			Debug.LogError("Achievement ID not found");
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000356D8 File Offset: 0x000338D8
		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			if (callback != null)
			{
				callback(this.m_AchievementDescriptions.ToArray());
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00035700 File Offset: 0x00033900
		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			if (callback != null)
			{
				callback(this.m_Achievements.ToArray());
			}
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00035728 File Offset: 0x00033928
		public void ReportScore(long score, string board, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (Leaderboard leaderboard in this.m_Leaderboards)
			{
				if (leaderboard.id == board)
				{
					leaderboard.SetScores(new List<Score>((Score[])leaderboard.scores)
					{
						new Score(board, score, this.localUser.id, DateTime.Now, score + " points", 0)
					}.ToArray());
					if (callback != null)
					{
						callback(true);
					}
					return;
				}
			}
			Debug.LogError("Leaderboard not found");
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00035818 File Offset: 0x00033A18
		public void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (Leaderboard leaderboard in this.m_Leaderboards)
			{
				if (leaderboard.id == leaderboardID)
				{
					this.SortScores(leaderboard);
					if (callback != null)
					{
						callback(leaderboard.scores);
					}
					return;
				}
			}
			Debug.LogError("Leaderboard not found");
			if (callback != null)
			{
				callback(new Score[0]);
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000358D0 File Offset: 0x00033AD0
		private void SortScores(Leaderboard board)
		{
			List<Score> list = new List<Score>((Score[])board.scores);
			list.Sort((Score s1, Score s2) => s2.value.CompareTo(s1.value));
			for (int i = 0; i < list.Count; i++)
			{
				list[i].SetRank(i + 1);
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x00035938 File Offset: 0x00033B38
		private void SetLocalPlayerScore(Leaderboard board)
		{
			foreach (Score score in board.scores)
			{
				if (score.userID == this.localUser.id)
				{
					board.SetLocalUserScore(score);
					break;
				}
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00035990 File Offset: 0x00033B90
		public void ShowAchievementsUI()
		{
			Debug.Log("ShowAchievementsUI not implemented");
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0003599C File Offset: 0x00033B9C
		public void ShowLeaderboardUI()
		{
			Debug.Log("ShowLeaderboardUI not implemented");
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000359A8 File Offset: 0x00033BA8
		public ILeaderboard CreateLeaderboard()
		{
			return new Leaderboard();
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000359BC File Offset: 0x00033BBC
		public IAchievement CreateAchievement()
		{
			return new Achievement();
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000359D0 File Offset: 0x00033BD0
		private bool VerifyUser()
		{
			if (!this.localUser.authenticated)
			{
				Debug.LogError("Must authenticate first");
				return false;
			}
			return true;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000359F0 File Offset: 0x00033BF0
		private void PopulateStaticData()
		{
			this.m_Friends.Add(new UserProfile("Fred", "1001", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Julia", "1002", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Jeff", "1003", true, UserState.Online, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Sam", "1004", false, UserState.Offline, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Max", "1005", false, UserState.Offline, this.m_DefaultTexture));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement01", "First achievement", this.m_DefaultTexture, "Get first achievement", "Received first achievement", false, 10));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement02", "Second achievement", this.m_DefaultTexture, "Get second achievement", "Received second achievement", false, 20));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement03", "Third achievement", this.m_DefaultTexture, "Get third achievement", "Received third achievement", false, 15));
			Leaderboard leaderboard = new Leaderboard();
			leaderboard.SetTitle("High Scores");
			leaderboard.id = "Leaderboard01";
			leaderboard.SetScores(new List<Score>
			{
				new Score("Leaderboard01", 300L, "1001", DateTime.Now.AddDays(-1.0), "300 points", 1),
				new Score("Leaderboard01", 255L, "1002", DateTime.Now.AddDays(-1.0), "255 points", 2),
				new Score("Leaderboard01", 55L, "1003", DateTime.Now.AddDays(-1.0), "55 points", 3),
				new Score("Leaderboard01", 10L, "1004", DateTime.Now.AddDays(-1.0), "10 points", 4)
			}.ToArray());
			this.m_Leaderboards.Add(leaderboard);
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00035C40 File Offset: 0x00033E40
		private Texture2D CreateDummyTexture(int width, int height)
		{
			Texture2D texture2D = new Texture2D(width, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color color = ((j & i) <= 0) ? Color.gray : Color.white;
					texture2D.SetPixel(j, i, color);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x04000BC9 RID: 3017
		private static LocalUser m_LocalUser;

		// Token: 0x04000BCA RID: 3018
		private List<UserProfile> m_Friends = new List<UserProfile>();

		// Token: 0x04000BCB RID: 3019
		private List<UserProfile> m_Users = new List<UserProfile>();

		// Token: 0x04000BCC RID: 3020
		private List<AchievementDescription> m_AchievementDescriptions = new List<AchievementDescription>();

		// Token: 0x04000BCD RID: 3021
		private List<Achievement> m_Achievements = new List<Achievement>();

		// Token: 0x04000BCE RID: 3022
		private List<Leaderboard> m_Leaderboards = new List<Leaderboard>();

		// Token: 0x04000BCF RID: 3023
		private Texture2D m_DefaultTexture;
	}
}
