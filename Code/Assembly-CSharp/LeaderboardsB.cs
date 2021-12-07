using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class LeaderboardsB : CleverMenuItemGroupBase
{
	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0003525F File Offset: 0x0003345F
	public LeaderboardRowUI CurrentRowUI
	{
		get
		{
			return this.TableUI.GetRowByIndex(this.m_currentRowIndex);
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00035272 File Offset: 0x00033472
	// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0003527C File Offset: 0x0003347C
	public override bool IsVisible
	{
		get
		{
			return this.m_isVisible;
		}
		set
		{
			if (this.m_isVisible == value)
			{
				return;
			}
			if (value)
			{
				base.gameObject.SetActive(true);
			}
			else
			{
				this.m_isVisible = false;
				if (this.FadeAnimator)
				{
					if (this.FadeAnimator.FinalOpacity < 0.01f)
					{
						this.FadeAnimator.Initialize();
						this.FadeAnimator.AnimatorDriver.Stop();
						this.FadeAnimator.AnimatorDriver.ContinueBackwards();
					}
					else
					{
						this.FadeAnimator.Initialize();
						this.FadeAnimator.AnimatorDriver.ContinueBackwards();
					}
				}
				else
				{
					base.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00035335 File Offset: 0x00033535
	public override bool CanBeEntered
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00035338 File Offset: 0x00033538
	public void OnEnable()
	{
		this.m_isVisible = true;
		if (this.FadeAnimator)
		{
			this.FadeAnimator.Initialize();
			this.FadeAnimator.AnimatorDriver.ContinueForward();
		}
		LeaderboardsB.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty);
		LeaderboardsB.RefreshUIStrings();
		XboxOneLive.OnOnlineStateChanged = (Action)Delegate.Combine(XboxOneLive.OnOnlineStateChanged, new Action(this.OnOnlineStateChanged));
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000353B4 File Offset: 0x000335B4
	public void OnOnlineStateChanged()
	{
		LeaderboardsB.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty);
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x000353CE File Offset: 0x000335CE
	public void OnDisable()
	{
		this.m_isVisible = false;
		XboxOneLive.OnOnlineStateChanged = (Action)Delegate.Remove(XboxOneLive.OnOnlineStateChanged, new Action(this.OnOnlineStateChanged));
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000C07 RID: 3079 RVA: 0x000353F7 File Offset: 0x000335F7
	// (set) Token: 0x06000C08 RID: 3080 RVA: 0x000353FF File Offset: 0x000335FF
	public override bool IsActive
	{
		get
		{
			return this.m_isActive;
		}
		set
		{
			this.m_isActive = value;
			this.UpdateHighlight();
		}
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00035410 File Offset: 0x00033610
	public void UpdateHighlight()
	{
		if (this.HighlightAnimator == null)
		{
			return;
		}
		if (this.IsActive)
		{
			this.HighlightAnimator.Initialize();
			this.HighlightAnimator.AnimatorDriver.ContinueForward();
		}
		else
		{
			this.HighlightAnimator.Initialize();
			this.HighlightAnimator.AnimatorDriver.ContinueBackwards();
		}
		this.RefreshRowIndex();
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0003547B File Offset: 0x0003367B
	// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00035484 File Offset: 0x00033684
	public override bool IsHighlightVisible
	{
		get
		{
			return this.m_isHighlightVisible;
		}
		set
		{
			if (this.CurrentRowUI)
			{
				if (this.m_isActive)
				{
					this.CurrentRowUI.Highlight();
				}
				else
				{
					this.CurrentRowUI.Unhighlight();
				}
			}
		}
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x000354C7 File Offset: 0x000336C7
	public new void Awake()
	{
		LeaderboardsB.Instance = this;
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x000354CF File Offset: 0x000336CF
	public override void EnterInGroup()
	{
		this.IsActive = true;
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x000354D8 File Offset: 0x000336D8
	public override bool OnMenuItemChangedInGroup(CleverMenuItemGroup group)
	{
		if (group == this)
		{
			return true;
		}
		this.IsActive = false;
		return false;
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000C0F RID: 3087 RVA: 0x000354F0 File Offset: 0x000336F0
	public static bool Available
	{
		get
		{
			return LeaderboardsB.Instance != null;
		}
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x000354FD File Offset: 0x000336FD
	public static LeaderboardData GetLeaderboard(LeaderboardB leaderboard)
	{
		return LeaderboardsB.Available ? (LeaderboardsB.Instance.m_data.ContainsKey(leaderboard) ? LeaderboardsB.Instance.m_data[leaderboard] : null) : null;
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0003553C File Offset: 0x0003373C
	public static LeaderboardData CurrentLeaderboardData
	{
		get
		{
			LeaderboardB leaderboard = LeaderboardUtility.LeaderboardToLeaderboardB(LeaderboardsB.Instance.m_currentLeaderboard, LeaderboardsB.Instance.m_currentDifficulty);
			return LeaderboardsB.GetLeaderboard(leaderboard);
		}
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00035569 File Offset: 0x00033769
	public static void ClearTableUI()
	{
		LeaderboardsB.Instance.TableUI.ClearTable();
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0003557C File Offset: 0x0003377C
	public static bool UpdateLeaderboard(Leaderboard leaderboard, Leaderboards.Filter filter, DifficultyMode difficulty)
	{
		LeaderboardB leaderboardB = LeaderboardUtility.LeaderboardToLeaderboardB(leaderboard, difficulty);
		LeaderboardsB.ClearTableUI();
		return Steamworks.Ready && LeaderboardsB.Available && Steamworks.UpdateLeaderboard(leaderboardB, filter, delegate
		{
			if (!LeaderboardsB.Instance.m_data.ContainsKey(leaderboardB) || !LeaderboardsB.Instance.m_data[leaderboardB].Update(Steamworks.LeaderboardData))
			{
				LeaderboardsB.Instance.m_data[leaderboardB] = Steamworks.LeaderboardData;
			}
			LeaderboardsB.RefreshTableUI();
		}, null);
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x000355D1 File Offset: 0x000337D1
	public void ShowScoreCard()
	{
		XboxOneUsers.ShowProfileCard(LeaderboardsB.CurrentLeaderboardData[this.m_currentRowIndex].UserID);
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x000355F0 File Offset: 0x000337F0
	private void HandleMouseMove()
	{
		int num = -1;
		Vector2 cursorPositionUI = Core.Input.CursorPositionUI;
		for (int i = 0; i < LeaderboardsB.CurrentLeaderboardData.Count; i++)
		{
			if (this.TableUI.GetRowByIndex(i).Bounds.Contains(cursorPositionUI))
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return;
		}
		if (this.m_currentRowIndex != num)
		{
			if (this.ChangeSelectionSound)
			{
				Sound.Play(this.ChangeSelectionSound.GetSound(null), base.transform.position, null);
			}
			this.CurrentRowUI.Unhighlight();
			this.m_currentRowIndex = num;
			this.CurrentRowUI.Highlight();
			if (this.OnHighlightChangedAction)
			{
				this.OnHighlightChangedAction.Perform(null);
			}
		}
		if (Core.Input.LeftClick.OnPressed)
		{
			Core.Input.ActionButtonA.Used = true;
			Core.Input.LeftClick.Used = true;
			this.ShowScoreCard();
			if (this.ViewGamerSound)
			{
				Sound.Play(this.ViewGamerSound.GetSound(null), base.transform.position, null);
			}
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x0003571C File Offset: 0x0003391C
	public void FixedUpdate()
	{
		if (!this.IsVisible)
		{
			if (this.FadeAnimator && this.FadeAnimator.AnimatorDriver.IsReversed && !this.FadeAnimator.AnimatorDriver.IsPlaying)
			{
				base.gameObject.SetActive(false);
			}
			return;
		}
		if (!this.IsActive)
		{
			return;
		}
		if (this.m_inactiveTime > 0f)
		{
			this.m_inactiveTime -= Time.fixedDeltaTime;
			return;
		}
		if (Core.Input.Cancel.OnPressed)
		{
			this.IsActive = false;
			if (this.OnBackAction)
			{
				this.OnBackAction.Perform(null);
			}
			this.OnBackPressed();
		}
		else if (LeaderboardsB.CurrentLeaderboardData != null)
		{
			if (Core.Input.Filter.OnPressed)
			{
				this.m_inactiveTime = 0.7f;
				this.NextFilter();
				if (this.CycleFilterSound)
				{
					Sound.Play(this.CycleFilterSound.GetSound(null), base.transform.position, null);
				}
			}
			else if (Core.Input.MenuPageLeft.OnPressed)
			{
				this.m_inactiveTime = 0.7f;
				this.PreviousLeaderboard();
				if (this.CycleLeaderboardSound)
				{
					Sound.Play(this.CycleLeaderboardSound.GetSound(null), base.transform.position, null);
				}
			}
			else if (Core.Input.MenuPageRight.OnPressed)
			{
				this.m_inactiveTime = 0.7f;
				this.NextLeaderboard();
				if (this.CycleLeaderboardSound)
				{
					Sound.Play(this.CycleLeaderboardSound.GetSound(null), base.transform.position, null);
				}
			}
			else if (Core.Input.LeftShoulder.OnPressed)
			{
				this.m_inactiveTime = 0.7f;
				this.PreviousDifficulty();
				if (this.CycleLeaderboardSound)
				{
					Sound.Play(this.CycleLeaderboardSound.GetSound(null), base.transform.position, null);
				}
			}
			else if (Core.Input.RightShoulder.OnPressed)
			{
				this.m_inactiveTime = 0.7f;
				this.NextDifficulty();
				if (this.CycleLeaderboardSound)
				{
					Sound.Play(this.CycleLeaderboardSound.GetSound(null), base.transform.position, null);
				}
			}
			if (this.CurrentRowUI)
			{
				if (Core.Input.CursorMoved)
				{
					this.HandleMouseMove();
				}
				if (Core.Input.MenuUp.OnPressed)
				{
					this.RowUp();
				}
				else if (Core.Input.MenuDown.OnPressed)
				{
					this.RowDown();
				}
				else if (Core.Input.ActionButtonA.OnPressedNotUsed)
				{
					Core.Input.ActionButtonA.Used = true;
					this.ShowScoreCard();
					if (this.ViewGamerSound)
					{
						Sound.Play(this.ViewGamerSound.GetSound(null), base.transform.position, null);
					}
				}
			}
		}
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00035A34 File Offset: 0x00033C34
	private void NextLeaderboard()
	{
		int num = (int)(this.m_currentLeaderboard + 1);
		if (num >= Enum.GetValues(typeof(Leaderboard)).Length)
		{
			num = 0;
		}
		this.m_currentLeaderboard = (Leaderboard)num;
		LeaderboardsB.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty);
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00035A88 File Offset: 0x00033C88
	private void NextDifficulty()
	{
		int num = (int)(this.m_currentDifficulty + 1);
		if (num >= Enum.GetValues(typeof(DifficultyMode)).Length)
		{
			num = 0;
		}
		this.m_currentDifficulty = (DifficultyMode)num;
		LeaderboardsB.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty);
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00035ADC File Offset: 0x00033CDC
	private void PreviousDifficulty()
	{
		int num = this.m_currentDifficulty - DifficultyMode.Normal;
		if (num < 0)
		{
			num = Enum.GetValues(typeof(DifficultyMode)).Length - 1;
		}
		this.m_currentDifficulty = (DifficultyMode)num;
		LeaderboardsB.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty);
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00035B2F File Offset: 0x00033D2F
	public static void RefreshTableUI()
	{
		LeaderboardsB.Instance.TableUI.UpdateTable(LeaderboardsB.CurrentLeaderboardData);
		LeaderboardsB.RefreshUIStrings();
		LeaderboardsB.Instance.RefreshRowIndex();
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00035B54 File Offset: 0x00033D54
	public void RefreshRowIndex()
	{
		if (LeaderboardsB.Instance == null)
		{
			return;
		}
		if (this.CurrentRowUI)
		{
			this.CurrentRowUI.Unhighlight();
		}
		LeaderboardData currentLeaderboardData = LeaderboardsB.CurrentLeaderboardData;
		int max = (currentLeaderboardData == null) ? (this.TableUI.RowCount - 1) : (currentLeaderboardData.Count - 1);
		this.m_currentRowIndex = Mathf.Clamp(this.m_currentRowIndex, 0, max);
		if (this.CurrentRowUI && this.IsActive)
		{
			this.CurrentRowUI.Highlight();
		}
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00035BF0 File Offset: 0x00033DF0
	public static void RefreshUIStrings()
	{
		MessageDescriptor message = new MessageDescriptor(string.Empty);
		if (LeaderboardsB.Instance.m_currentDifficulty == DifficultyMode.Easy)
		{
			message = new MessageDescriptor(LeaderboardsB.Instance.DifficultyMessageProvider + ": " + LeaderboardsB.Instance.DifficultyEasyMessageProvider);
		}
		if (LeaderboardsB.Instance.m_currentDifficulty == DifficultyMode.Normal)
		{
			message = new MessageDescriptor(LeaderboardsB.Instance.DifficultyMessageProvider + ": " + LeaderboardsB.Instance.DifficultyNormalMessageProvider);
		}
		if (LeaderboardsB.Instance.m_currentDifficulty == DifficultyMode.Hard)
		{
			message = new MessageDescriptor(LeaderboardsB.Instance.DifficultyMessageProvider + ": " + LeaderboardsB.Instance.DifficultyHardMessageProvider);
		}
		if (LeaderboardsB.Instance.m_currentDifficulty == DifficultyMode.OneLife)
		{
			message = new MessageDescriptor(LeaderboardsB.Instance.DifficultyMessageProvider + ": " + LeaderboardsB.Instance.DifficultyOneLifeMessageProvider);
		}
		LeaderboardsB.Instance.DifficultyTextPC.SetMessage(message);
		LeaderboardsB.Instance.DifficultyTextXbox.SetMessage(message);
		MessageDescriptor message2 = new MessageDescriptor(string.Empty);
		if (LeaderboardsB.Instance.m_currentLeaderboard == Leaderboard.SpeedRunner)
		{
			message2 = new MessageDescriptor(LeaderboardsB.Instance.LeaderboardMessageProvider + ": " + LeaderboardsB.Instance.BoardSpeedRunnerMessageProvider);
		}
		if (LeaderboardsB.Instance.m_currentLeaderboard == Leaderboard.Explorer)
		{
			message2 = new MessageDescriptor(LeaderboardsB.Instance.LeaderboardMessageProvider + ": " + LeaderboardsB.Instance.BoardExplorerMessageProvider);
		}
		if (LeaderboardsB.Instance.m_currentLeaderboard == Leaderboard.Survivor)
		{
			message2 = new MessageDescriptor(LeaderboardsB.Instance.LeaderboardMessageProvider + ": " + LeaderboardsB.Instance.BoardGlobalMessageProvider);
		}
		LeaderboardsB.Instance.LeaderboardTitlePC.SetMessage(message2);
		LeaderboardsB.Instance.LeaderboardTitleXbox.SetMessage(message2);
		MessageDescriptor message3 = new MessageDescriptor(string.Empty);
		if (LeaderboardsB.Instance.m_currentFilter == Leaderboards.Filter.Overall)
		{
			message3 = new MessageDescriptor(LeaderboardsB.Instance.FilterMessageProvider + ": " + LeaderboardsB.Instance.FilterOverallMessageProvider);
		}
		if (LeaderboardsB.Instance.m_currentFilter == Leaderboards.Filter.Friends)
		{
			message3 = new MessageDescriptor(LeaderboardsB.Instance.FilterMessageProvider + ": " + LeaderboardsB.Instance.FilterMyFriendsMessageProvider);
		}
		if (LeaderboardsB.Instance.m_currentFilter == Leaderboards.Filter.MyScore)
		{
			message3 = new MessageDescriptor(LeaderboardsB.Instance.FilterMessageProvider + ": " + LeaderboardsB.Instance.FilterMyScoreMessageProvider);
		}
		LeaderboardsB.Instance.FilterTextPC.SetMessage(message3);
		LeaderboardsB.Instance.FilterTextXbox.SetMessage(message3);
		MessageDescriptor message4 = new MessageDescriptor(string.Empty);
		LeaderboardsB.Instance.EntriesTextXbox.SetMessage(message4);
		if (LeaderboardsB.CurrentLeaderboardData != null)
		{
			string message5 = LeaderboardsB.Instance.EntriesMessageProvider.ToString().Replace("[Count]", LeaderboardsB.CurrentLeaderboardData.TotalRowCount.ToString());
			message4 = new MessageDescriptor(message5);
		}
		else
		{
			message4 = new MessageDescriptor(string.Empty);
		}
		LeaderboardsB.Instance.EntriesTextPC.SetMessage(message4);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00035F10 File Offset: 0x00034110
	private void PreviousLeaderboard()
	{
		int num = this.m_currentLeaderboard - Leaderboard.Explorer;
		if (num < 0)
		{
			num = Enum.GetValues(typeof(Leaderboard)).Length - 1;
		}
		this.m_currentLeaderboard = (Leaderboard)num;
		LeaderboardsB.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty);
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00035F64 File Offset: 0x00034164
	private void RowDown()
	{
		this.CurrentRowUI.Unhighlight();
		this.m_currentRowIndex++;
		if (this.m_currentRowIndex >= LeaderboardsB.CurrentLeaderboardData.Count)
		{
			this.m_currentRowIndex = 0;
		}
		else if (this.ChangeSelectionSound)
		{
			Sound.Play(this.ChangeSelectionSound.GetSound(null), base.transform.position, null);
		}
		this.CurrentRowUI.Highlight();
		if (this.OnHighlightChangedAction)
		{
			this.OnHighlightChangedAction.Perform(null);
		}
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00036000 File Offset: 0x00034200
	private void RowUp()
	{
		this.CurrentRowUI.Unhighlight();
		this.m_currentRowIndex--;
		if (this.m_currentRowIndex < 0)
		{
			this.m_currentRowIndex = LeaderboardsB.CurrentLeaderboardData.Count - 1;
		}
		else if (this.ChangeSelectionSound)
		{
			Sound.Play(this.ChangeSelectionSound.GetSound(null), base.transform.position, null);
		}
		this.CurrentRowUI.Highlight();
		if (this.OnHighlightChangedAction)
		{
			this.OnHighlightChangedAction.Perform(null);
		}
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x000360A0 File Offset: 0x000342A0
	private void NextFilter()
	{
		int num = (int)(this.m_currentFilter + 1);
		if (num >= Enum.GetValues(typeof(Leaderboards.Filter)).Length)
		{
			num = 0;
		}
		this.m_currentFilter = (Leaderboards.Filter)num;
		LeaderboardsB.UpdateLeaderboard(this.m_currentLeaderboard, this.m_currentFilter, this.m_currentDifficulty);
	}

	// Token: 0x040009B0 RID: 2480
	private const string s_TotalEntriesCountIdentifier = "[Count]";

	// Token: 0x040009B1 RID: 2481
	public static LeaderboardsB Instance;

	// Token: 0x040009B2 RID: 2482
	public LeaderboardTableUI TableUI;

	// Token: 0x040009B3 RID: 2483
	public MessageBox LeaderboardTitlePC;

	// Token: 0x040009B4 RID: 2484
	public MessageBox FilterTextPC;

	// Token: 0x040009B5 RID: 2485
	public MessageBox EntriesTextPC;

	// Token: 0x040009B6 RID: 2486
	public MessageBox DifficultyTextPC;

	// Token: 0x040009B7 RID: 2487
	public MessageBox LeaderboardTitleXbox;

	// Token: 0x040009B8 RID: 2488
	public MessageBox FilterTextXbox;

	// Token: 0x040009B9 RID: 2489
	public MessageBox EntriesTextXbox;

	// Token: 0x040009BA RID: 2490
	public MessageBox DifficultyTextXbox;

	// Token: 0x040009BB RID: 2491
	public ActionMethod OnHighlightChangedAction;

	// Token: 0x040009BC RID: 2492
	private Leaderboard m_currentLeaderboard = Leaderboard.Explorer;

	// Token: 0x040009BD RID: 2493
	private Leaderboards.Filter m_currentFilter = Leaderboards.Filter.MyScore;

	// Token: 0x040009BE RID: 2494
	private DifficultyMode m_currentDifficulty = DifficultyMode.Normal;

	// Token: 0x040009BF RID: 2495
	private readonly Dictionary<LeaderboardB, LeaderboardData> m_data = new Dictionary<LeaderboardB, LeaderboardData>();

	// Token: 0x040009C0 RID: 2496
	private int m_currentRowIndex;

	// Token: 0x040009C1 RID: 2497
	private bool m_isVisible;

	// Token: 0x040009C2 RID: 2498
	public TranslatedMessageProvider LeaderboardMessageProvider;

	// Token: 0x040009C3 RID: 2499
	public TranslatedMessageProvider FilterMessageProvider;

	// Token: 0x040009C4 RID: 2500
	public TranslatedMessageProvider DifficultyMessageProvider;

	// Token: 0x040009C5 RID: 2501
	public TranslatedMessageProvider EntriesMessageProvider;

	// Token: 0x040009C6 RID: 2502
	public TranslatedMessageProvider BoardSpeedRunnerMessageProvider;

	// Token: 0x040009C7 RID: 2503
	public TranslatedMessageProvider BoardExplorerMessageProvider;

	// Token: 0x040009C8 RID: 2504
	public TranslatedMessageProvider BoardGlobalMessageProvider;

	// Token: 0x040009C9 RID: 2505
	public TranslatedMessageProvider FilterOverallMessageProvider;

	// Token: 0x040009CA RID: 2506
	public TranslatedMessageProvider FilterMyFriendsMessageProvider;

	// Token: 0x040009CB RID: 2507
	public TranslatedMessageProvider FilterMyScoreMessageProvider;

	// Token: 0x040009CC RID: 2508
	public TranslatedMessageProvider DifficultyEasyMessageProvider;

	// Token: 0x040009CD RID: 2509
	public TranslatedMessageProvider DifficultyNormalMessageProvider;

	// Token: 0x040009CE RID: 2510
	public TranslatedMessageProvider DifficultyHardMessageProvider;

	// Token: 0x040009CF RID: 2511
	public TranslatedMessageProvider DifficultyOneLifeMessageProvider;

	// Token: 0x040009D0 RID: 2512
	public SoundProvider OpenSound;

	// Token: 0x040009D1 RID: 2513
	public SoundProvider CloseSound;

	// Token: 0x040009D2 RID: 2514
	public SoundProvider CycleLeaderboardSound;

	// Token: 0x040009D3 RID: 2515
	public SoundProvider CycleFilterSound;

	// Token: 0x040009D4 RID: 2516
	public SoundProvider ViewGamerSound;

	// Token: 0x040009D5 RID: 2517
	public SoundProvider ChangeSelectionSound;

	// Token: 0x040009D6 RID: 2518
	public TransparencyAnimator FadeAnimator;

	// Token: 0x040009D7 RID: 2519
	public TransparencyAnimator HighlightAnimator;

	// Token: 0x040009D8 RID: 2520
	private bool m_isActive;

	// Token: 0x040009D9 RID: 2521
	public ActionSequence OnBackAction;

	// Token: 0x040009DA RID: 2522
	private bool m_isHighlightVisible;

	// Token: 0x040009DB RID: 2523
	private float m_inactiveTime;
}
