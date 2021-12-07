using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class LeaderboardsController : MonoBehaviour
{
	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0003E29D File Offset: 0x0003C49D
	public static LeaderboardsController Instance
	{
		get
		{
			if (LeaderboardsController.s_instance == null)
			{
				LeaderboardsController.s_instance = UnityEngine.Object.FindObjectOfType<LeaderboardsController>();
			}
			return LeaderboardsController.s_instance;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0003E2BE File Offset: 0x0003C4BE
	public static bool Available
	{
		get
		{
			return LeaderboardsController.Instance != null;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0003E2CB File Offset: 0x0003C4CB
	// (set) Token: 0x06000D71 RID: 3441 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
	public static bool Visible
	{
		get
		{
			return LeaderboardsController.Available && LeaderboardsController.Instance.m_placeholderGUIVisible;
		}
		set
		{
			if (!LeaderboardsController.Available)
			{
				return;
			}
			if (value && !LeaderboardsController.Instance.m_placeholderGUIVisible)
			{
				LeaderboardsController.UpdateLeaderboard(LeaderboardsController.Instance.m_currentLeaderboard, LeaderboardsController.Instance.m_currentFilter, LeaderboardsController.Instance.m_currentDifficulty, null);
			}
			if (LeaderboardsController.Instance.m_placeholderGUIVisible && !value)
			{
				SuspensionManager.ResumeAll();
			}
			else if (!LeaderboardsController.Instance.m_placeholderGUIVisible && value)
			{
				SuspensionManager.SuspendAll();
			}
			LeaderboardsController.Instance.m_placeholderGUIVisible = value;
		}
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0003E37C File Offset: 0x0003C57C
	public static LeaderboardData GetLeaderboard(Leaderboard leaderboard)
	{
		return LeaderboardsController.Available ? (LeaderboardsController.Instance.m_data.ContainsKey(leaderboard) ? LeaderboardsController.Instance.m_data[leaderboard] : null) : null;
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0003E3C4 File Offset: 0x0003C5C4
	public static bool Clear(Leaderboard leaderboard)
	{
		if (!LeaderboardsController.Available)
		{
			return false;
		}
		LeaderboardsController.Instance.m_data.Remove(leaderboard);
		return true;
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0003E3E4 File Offset: 0x0003C5E4
	public static bool UpdateLeaderboard(Leaderboard leaderboard, Leaderboards.Filter filter, DifficultyMode difficulty, LeaderboardsController.UpdateCallback callback = null)
	{
		LeaderboardB leaderboard2 = LeaderboardUtility.LeaderboardToLeaderboardB(leaderboard, difficulty);
		return Steamworks.Ready && LeaderboardsController.Available && Steamworks.UpdateLeaderboard(leaderboard2, filter, delegate
		{
			if (!LeaderboardsController.Instance.m_data.ContainsKey(leaderboard) || !LeaderboardsController.Instance.m_data[leaderboard].Update(Steamworks.LeaderboardData))
			{
				LeaderboardsController.Instance.m_data[leaderboard] = Steamworks.LeaderboardData;
			}
		}, null);
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0003E438 File Offset: 0x0003C638
	private void Update()
	{
		if (!this.m_placeholderGUIVisible || !this.m_previousPlaceholderGUIVisible)
		{
			this.m_previousPlaceholderGUIVisible = this.m_placeholderGUIVisible;
			this.m_lastInputTime = Time.time;
			return;
		}
		if (Core.Input.Cancel.IsPressed)
		{
			LeaderboardsController.Visible = false;
		}
		else if (Time.time - this.m_lastInputTime > 0.2f)
		{
			LeaderboardData leaderboard = LeaderboardsController.GetLeaderboard(this.m_currentLeaderboard);
			if (leaderboard != null)
			{
				if (Core.Input.MenuPageLeft.IsPressed)
				{
					this.m_lastInputTime = Time.time;
					int num = this.m_currentFilter - Leaderboards.Filter.Friends;
					if (num < 0)
					{
						num = Enum.GetValues(typeof(Leaderboards.Filter)).Length - 1;
					}
					this.m_currentFilter = (Leaderboards.Filter)num;
					LeaderboardsController.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty, null);
				}
				else if (Core.Input.MenuPageRight.IsPressed)
				{
					this.m_lastInputTime = Time.time;
					this.m_currentFilter = (this.m_currentFilter + 1) % (Leaderboards.Filter)Enum.GetValues(typeof(Leaderboards.Filter)).Length;
					LeaderboardsController.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty, null);
				}
				else if (Core.Input.MenuUp.IsPressed)
				{
					this.m_lastInputTime = Time.time;
					this.m_currentRow--;
					if (this.m_currentRow < 0)
					{
						this.m_currentRow = leaderboard.Count - 1;
					}
				}
				else if (Core.Input.MenuDown.IsPressed && leaderboard.Count > 0)
				{
					this.m_lastInputTime = Time.time;
					this.m_currentRow = (this.m_currentRow + 1) % leaderboard.Count;
				}
				else if (Core.Input.ActionButtonA.IsPressed)
				{
					XboxOneUsers.ShowProfileCard(leaderboard[this.m_currentRow].UserID);
				}
			}
		}
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0003E624 File Offset: 0x0003C824
	public static void UploadScores()
	{
		if (GameWorld.Instance == null)
		{
			return;
		}
		if (SeinDeathCounter.Instance == null)
		{
			return;
		}
		int count = SeinDeathCounter.Count;
		int completionPercentage = GameWorld.Instance.CompletionPercentage;
		int gameTimeInSeconds = GameController.Instance.GameTimeInSeconds;
		DifficultyMode lowestDifficulty = DifficultyController.Instance.LowestDifficulty;
		LeaderboardsController.m_lastScoreUploadTime = Time.realtimeSinceStartup;
		LeaderboardsController.Instance.StartCoroutine(LeaderboardsController.Instance.UploadScoresRoutine(gameTimeInSeconds, count, completionPercentage, lowestDifficulty));
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0003E69D File Offset: 0x0003C89D
	public static void UploadScore()
	{
		if (Time.realtimeSinceStartup - LeaderboardsController.m_lastScoreUploadTime > 1f && Time.realtimeSinceStartup > 10f && Characters.Sein != null)
		{
			LeaderboardsController.UploadScores();
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
	public void FixedUpdate()
	{
		if (this.m_updateLeaderboardsTime <= 0f)
		{
			this.m_updateLeaderboardsTime = 360f;
			if (Time.realtimeSinceStartup > 10f && Characters.Sein != null && LeaderboardsController.AutoUpload && !AchievementsLogic.Act3Ended)
			{
				LeaderboardsController.UploadScores();
			}
		}
		this.m_updateLeaderboardsTime -= Time.fixedDeltaTime;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0003E74C File Offset: 0x0003C94C
	public IEnumerator UploadScoresRoutine(int time, int deathCount, int completionPercentage, DifficultyMode difficulty)
	{
		LeaderboardsController.SendExplorerLeaderboardData(new LeaderboardEntryData(time, deathCount, completionPercentage), difficulty);
		yield return new WaitForSeconds(1f);
		yield break;
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0003E79A File Offset: 0x0003C99A
	public static void SendExplorerLeaderboardData(LeaderboardEntryData data, DifficultyMode difficultyMode)
	{
		if (DebugGUIText.Enabled)
		{
		}
		LeaderboardsController.SendLeaderboardData(Leaderboard.Explorer, data.EncodeExplorer(), difficultyMode);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0003E7B3 File Offset: 0x0003C9B3
	public static void SendSpeedRunnersLeaderboardData(LeaderboardEntryData data, DifficultyMode difficultyMode)
	{
		if (DebugGUIText.Enabled)
		{
		}
		LeaderboardsController.SendLeaderboardData(Leaderboard.SpeedRunner, data.EncodeSpeedRunner(), difficultyMode);
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0003E7CC File Offset: 0x0003C9CC
	public static void SendSurvivorLeaderboardData(LeaderboardEntryData data, DifficultyMode difficultyMode)
	{
		if (DebugGUIText.Enabled)
		{
		}
		LeaderboardsController.SendLeaderboardData(Leaderboard.Survivor, data.EncodeSurvivor(), difficultyMode);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0003E7E8 File Offset: 0x0003C9E8
	private static void SendLeaderboardData(Leaderboard leaderboard, long data, DifficultyMode difficultyMode)
	{
		if (!Steamworks.EnableLeaderboards)
		{
			return;
		}
		if (DebugGUIText.Enabled)
		{
		}
		LeaderboardB leaderboard2 = LeaderboardUtility.LeaderboardToLeaderboardB(leaderboard, difficultyMode);
		Steamworks.SendLeaderboardData(leaderboard2, (ulong)data);
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0003E81C File Offset: 0x0003CA1C
	public static Rect PushDown(ref Rect rect, float offset)
	{
		rect.Set(rect.x, rect.y + offset, rect.width, rect.height);
		return rect;
	}

	// Token: 0x04000AEE RID: 2798
	private const float kMinInputDelay = 0.2f;

	// Token: 0x04000AEF RID: 2799
	private static LeaderboardsController s_instance;

	// Token: 0x04000AF0 RID: 2800
	public static bool AutoUpload = true;

	// Token: 0x04000AF1 RID: 2801
	public static long Int52Max = 4503599627370495L;

	// Token: 0x04000AF2 RID: 2802
	private Dictionary<Leaderboard, LeaderboardData> m_data = new Dictionary<Leaderboard, LeaderboardData>();

	// Token: 0x04000AF3 RID: 2803
	private bool m_placeholderGUIVisible;

	// Token: 0x04000AF4 RID: 2804
	private Leaderboard m_currentLeaderboard = Leaderboard.Survivor;

	// Token: 0x04000AF5 RID: 2805
	private Leaderboards.Filter m_currentFilter = Leaderboards.Filter.MyScore;

	// Token: 0x04000AF6 RID: 2806
	private DifficultyMode m_currentDifficulty = DifficultyMode.Normal;

	// Token: 0x04000AF7 RID: 2807
	private int m_currentRow;

	// Token: 0x04000AF8 RID: 2808
	private float m_lastInputTime;

	// Token: 0x04000AF9 RID: 2809
	private bool m_previousPlaceholderGUIVisible;

	// Token: 0x04000AFA RID: 2810
	private float m_updateLeaderboardsTime;

	// Token: 0x04000AFB RID: 2811
	private static float m_lastScoreUploadTime;

	// Token: 0x02000644 RID: 1604
	// (Invoke) Token: 0x06002741 RID: 10049
	public delegate void UpdateCallback(Leaderboard leaderboard, LeaderboardData data);
}
