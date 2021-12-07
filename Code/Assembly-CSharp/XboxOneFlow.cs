using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Core;
using Game;
using SmartInput;
using UnityEngine;

// Token: 0x020008C2 RID: 2242
public class XboxOneFlow : MonoBehaviour
{
	// Token: 0x170007ED RID: 2029
	// (get) Token: 0x060031DC RID: 12764 RVA: 0x000D37B8 File Offset: 0x000D19B8
	private static XboxOneFlow Instance
	{
		get
		{
			return XboxOneFlow.s_instance;
		}
	}

	// Token: 0x170007EE RID: 2030
	// (get) Token: 0x060031DD RID: 12765 RVA: 0x000D37BF File Offset: 0x000D19BF
	public static bool Ready
	{
		get
		{
			return XboxOneFlow.Instance != null;
		}
	}

	// Token: 0x170007EF RID: 2031
	// (get) Token: 0x060031DE RID: 12766 RVA: 0x000D37CC File Offset: 0x000D19CC
	// (set) Token: 0x060031DF RID: 12767 RVA: 0x000D37E8 File Offset: 0x000D19E8
	public static bool Engage
	{
		get
		{
			return XboxOneFlow.Ready && XboxOneFlow.Instance.m_engaged;
		}
		set
		{
			if (XboxOneFlow.Ready)
			{
				if (!value || !XboxOneFlow.Instance.m_engaged)
				{
				}
				XboxOneFlow.Instance.m_engaged = value;
			}
		}
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x000D3820 File Offset: 0x000D1A20
	private void Awake()
	{
		XboxOneFlow.s_instance = this;
		bool flag = !GameController.Instance.IsTrial && !GameController.Instance.IsDemo;
		XboxOneSave.EnableSave = flag;
		flag = flag;
		XboxOneLeaderboards.EnableLeaderboards = flag;
		flag = flag;
		XboxOneAchievements.EnableAchievements = flag;
		flag = flag;
		XboxOneRichPresence.EnableRichPresence = flag;
		XboxOneDVR.EnableDVR = flag;
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x000D3877 File Offset: 0x000D1A77
	private void Start()
	{
		VisualLog.RegisterStatus(this);
	}

	// Token: 0x060031E2 RID: 12770 RVA: 0x000D3880 File Offset: 0x000D1A80
	private void OnEnable()
	{
		XboxOneLive.OnOnlineStateChanged = (Action)Delegate.Combine(XboxOneLive.OnOnlineStateChanged, new Action(this.OnOnlineStateChanged));
		XboxOneUsers.OnUserWillChange = (Action<int>)Delegate.Combine(XboxOneUsers.OnUserWillChange, new Action<int>(this.OnUserWillChange));
		XboxOneUsers.OnUserPicked = (Action)Delegate.Combine(XboxOneUsers.OnUserPicked, new Action(this.OnUserPicked));
		XboxOneUsers.OnUserSignedOut = (Action)Delegate.Combine(XboxOneUsers.OnUserSignedOut, new Action(this.OnUserSignedOut));
		XboxOneSession.OnSessionStarted = (Action)Delegate.Combine(XboxOneSession.OnSessionStarted, new Action(this.OnSessionStarted));
		XboxOneSession.OnSessionEnded = (Action)Delegate.Combine(XboxOneSession.OnSessionEnded, new Action(this.OnSessionEnded));
		XboxOneSession.OnWindowDeactivated = (Action)Delegate.Combine(XboxOneSession.OnWindowDeactivated, new Action(this.OnResourcesLow));
		XboxOneSession.OnWindowActivated = (Action)Delegate.Combine(XboxOneSession.OnWindowActivated, new Action(this.OnResourcesHigh));
		XboxOneUsers.OnLoginCancel = (Action)Delegate.Combine(XboxOneUsers.OnLoginCancel, new Action(this.OnLoginCancel));
		XboxOneSession.OnSuspend = (Action)Delegate.Combine(XboxOneSession.OnSuspend, new Action(this.OnSuspend));
		XboxOneSession.OnResume = (Action)Delegate.Combine(XboxOneSession.OnResume, new Action(this.OnResume));
		XboxOneController.OnActiveControllerDisconnect = (Action)Delegate.Combine(XboxOneController.OnActiveControllerDisconnect, new Action(this.OnActiveControllerDisconnect));
		XboxOneController.OnLastControllerDisconnect = (Action)Delegate.Combine(XboxOneController.OnLastControllerDisconnect, new Action(this.OnLastControllerDisconnected));
		XboxOneController.OnWillSwitchController = (Action<int>)Delegate.Combine(XboxOneController.OnWillSwitchController, new Action<int>(this.OnWillSwitchController));
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x000D3A50 File Offset: 0x000D1C50
	private void OnDisable()
	{
		XboxOneLive.OnOnlineStateChanged = (Action)Delegate.Remove(XboxOneLive.OnOnlineStateChanged, new Action(this.OnOnlineStateChanged));
		XboxOneUsers.OnUserWillChange = (Action<int>)Delegate.Remove(XboxOneUsers.OnUserWillChange, new Action<int>(this.OnUserWillChange));
		XboxOneUsers.OnUserPicked = (Action)Delegate.Remove(XboxOneUsers.OnUserPicked, new Action(this.OnUserPicked));
		XboxOneUsers.OnUserSignedOut = (Action)Delegate.Remove(XboxOneUsers.OnUserSignedOut, new Action(this.OnUserSignedOut));
		XboxOneSession.OnSessionStarted = (Action)Delegate.Remove(XboxOneSession.OnSessionStarted, new Action(this.OnSessionStarted));
		XboxOneSession.OnSessionEnded = (Action)Delegate.Remove(XboxOneSession.OnSessionEnded, new Action(this.OnSessionEnded));
		XboxOneSession.OnWindowDeactivated = (Action)Delegate.Remove(XboxOneSession.OnWindowDeactivated, new Action(this.OnResourcesLow));
		XboxOneSession.OnWindowActivated = (Action)Delegate.Remove(XboxOneSession.OnWindowActivated, new Action(this.OnResourcesHigh));
		XboxOneUsers.OnLoginCancel = (Action)Delegate.Remove(XboxOneUsers.OnLoginCancel, new Action(this.OnLoginCancel));
		XboxOneSession.OnSuspend = (Action)Delegate.Remove(XboxOneSession.OnSuspend, new Action(this.OnSuspend));
		XboxOneSession.OnResume = (Action)Delegate.Remove(XboxOneSession.OnResume, new Action(this.OnResume));
		XboxOneController.OnActiveControllerDisconnect = (Action)Delegate.Remove(XboxOneController.OnActiveControllerDisconnect, new Action(this.OnActiveControllerDisconnect));
		XboxOneController.OnLastControllerDisconnect = (Action)Delegate.Remove(XboxOneController.OnLastControllerDisconnect, new Action(this.OnLastControllerDisconnected));
		XboxOneController.OnWillSwitchController = (Action<int>)Delegate.Remove(XboxOneController.OnWillSwitchController, new Action<int>(this.OnWillSwitchController));
	}

	// Token: 0x060031E4 RID: 12772 RVA: 0x000D3C1D File Offset: 0x000D1E1D
	private void OnInstallCompleted()
	{
		Scenes.Manager.OnFinishedStreamingInstall();
	}

	// Token: 0x060031E5 RID: 12773 RVA: 0x000D3C2C File Offset: 0x000D1E2C
	private void OnOnlineStateChanged()
	{
		if (XboxOneLive.Online && XboxOneSession.SessionActive)
		{
			XboxOneSession.RestartSession(null, null);
		}
		if (!XboxOneLive.Online)
		{
			UI.Hints.Show(this.OfflineMessageProvider, HintLayer.GameSaved, 2f);
			AchievementsUI.Visible = false;
			LeaderboardsController.Visible = false;
		}
	}

	// Token: 0x060031E6 RID: 12774 RVA: 0x000D3C80 File Offset: 0x000D1E80
	private void OnUserPicked()
	{
		bool flag = this.m_lastUser >= 0;
		bool flag2 = this.m_lastUser != XboxOneUsers.CurrentUserLocalID;
		bool gameInTitleScreen = GameController.Instance.GameInTitleScreen;
		if (flag && flag2)
		{
			this.RestartGame();
		}
		this.m_lastUser = XboxOneUsers.CurrentUserLocalID;
		if (this.m_isSwitchingUser)
		{
			GameStateMachine.Instance.SetToStartScreen();
		}
		this.m_isSwitchingUser = (this.m_isRequestingUser = false);
	}

	// Token: 0x060031E7 RID: 12775 RVA: 0x000D3CF8 File Offset: 0x000D1EF8
	private void OnLoginCancel()
	{
		if (this.m_isSwitchingUser)
		{
			this.m_isSwitchingUser = false;
		}
		else
		{
			XboxOneController.ResetCurrentGamepad();
		}
	}

	// Token: 0x060031E8 RID: 12776 RVA: 0x000D3D16 File Offset: 0x000D1F16
	private void OnUserSignedOut()
	{
		if (!UI.Menu.ResumeScreenVisible)
		{
			this.m_lastUser = 0;
			this.RestartGame();
		}
	}

	// Token: 0x060031E9 RID: 12777 RVA: 0x000D3D34 File Offset: 0x000D1F34
	private void OnUserWillChange(int newUserID)
	{
	}

	// Token: 0x060031EA RID: 12778 RVA: 0x000D3D38 File Offset: 0x000D1F38
	private void FixedUpdate()
	{
		if (this.m_engaged)
		{
			XboxOneSession.RequireSession(null, null);
			if (XboxOneUsers.CurrentUserLocalID >= 0 && !XboxOneUsers.ResolvingUser)
			{
				bool flag = false;
				try
				{
					flag = OptionsScreen.Instance.Navigation.IsVisible;
				}
				catch (Exception ex)
				{
				}
				if (GameController.Instance.GameInTitleScreen && XboxOneIdentityUI.IsVisible && this.m_switchProfileButton.GetButton() && !flag)
				{
					this.m_isSwitchingUser = true;
					XboxOneUsers.RequestUser(null, null);
				}
			}
		}
		XboxOneRichPresence.UpdateRichPresence();
	}

	// Token: 0x060031EB RID: 12779 RVA: 0x000D3DE0 File Offset: 0x000D1FE0
	public void OnApplicationQuit()
	{
	}

	// Token: 0x060031EC RID: 12780 RVA: 0x000D3DE4 File Offset: 0x000D1FE4
	private void RestartGame()
	{
		UI.Menu.HideMenuScreen(true);
		UI.Menu.HideResumeScreen();
		bool gameInTitleScreen = GameController.Instance.GameInTitleScreen;
		if (gameInTitleScreen)
		{
			if (TitleScreenManager.CurrentScreen == TitleScreenManager.Screen.PressStart)
			{
				this.m_lastUser = XboxOneUsers.CurrentUserLocalID;
				XboxOneSession.RestartSession(null, null);
			}
			else
			{
				XboxLiveController.Instance.Reset();
				XboxOneController.ResetCurrentGamepad();
				XboxOneFlow.Engage = false;
				XboxOneSession.EndSession();
				TitleScreenManager.SetScreen(TitleScreenManager.Screen.PressStart);
			}
		}
		else
		{
			this.m_engaged = false;
			XboxOneUsers.SignOutCurrentUser();
			this.SkipToTitleScreen();
		}
	}

	// Token: 0x060031ED RID: 12781 RVA: 0x000D3E71 File Offset: 0x000D2071
	private void SkipToTitleScreen()
	{
		GameController.Instance.RestartGame();
	}

	// Token: 0x060031EE RID: 12782 RVA: 0x000D3E80 File Offset: 0x000D2080
	private void OnEngagementEnd(bool requestUser = false)
	{
		if (this.m_engaged)
		{
			if (GameController.Instance.GameInTitleScreen)
			{
				this.RestartGame();
			}
			else
			{
				UI.Menu.ShowResumeScreen();
				this.m_isRequestingUser = (this.m_isRequestingUser || requestUser);
			}
		}
	}

	// Token: 0x060031EF RID: 12783 RVA: 0x000D3ED4 File Offset: 0x000D20D4
	private void OnSessionStarted()
	{
		this.m_lastUser = XboxOneUsers.CurrentUserLocalID;
		XboxOneUsers.ClearUserCachedData();
		XboxOneAchievements.UpdateAchievements(null, null);
		this.m_saveSlotToUpdate = 0;
		this.StartSessionWithStorage();
		XboxOneUsers.UpdateUserPicture(null);
		Telemetry.SendSettings();
	}

	// Token: 0x060031F0 RID: 12784 RVA: 0x000D3F13 File Offset: 0x000D2113
	private void StartSessionWithStorage()
	{
		XboxOneSave.RequireStorage(new Action(this.UpdateNextSaveSlot), new Action(this.OnRequireStorageFailed));
	}

	// Token: 0x060031F1 RID: 12785 RVA: 0x000D3F33 File Offset: 0x000D2133
	private void OnRequireStorageFailed()
	{
		base.StartCoroutine(this.RequireStorageNextFrameRoutine());
	}

	// Token: 0x060031F2 RID: 12786 RVA: 0x000D3F44 File Offset: 0x000D2144
	private IEnumerator RequireStorageNextFrameRoutine()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		this.StartSessionWithStorage();
		yield break;
	}

	// Token: 0x060031F3 RID: 12787 RVA: 0x000D3F60 File Offset: 0x000D2160
	public void UpdateNextSaveSlot()
	{
		if (this.m_saveSlotToUpdate < 10)
		{
			XboxOneSave.UpdateSaveGame(this.m_saveSlotToUpdate, new Action(this.UpdateNextSaveSlot), new Action(this.UpdateNextSaveSlot));
		}
		else
		{
			XboxOneSave.OnSlotsAllQueried();
		}
		this.m_saveSlotToUpdate++;
	}

	// Token: 0x060031F4 RID: 12788 RVA: 0x000D3FB8 File Offset: 0x000D21B8
	private void StorageTest()
	{
		string path = Application.persistentDataPath + "/TestFile.txt";
		if (!File.Exists(path))
		{
			File.WriteAllText(path, string.Format("Hello, storage! Wrote this at {0}.", DateTime.Now));
		}
	}

	// Token: 0x060031F5 RID: 12789 RVA: 0x000D4000 File Offset: 0x000D2200
	private void StatisticsTest()
	{
		XboxOneStatistics.RequestStatistics(delegate(List<XboxOneStatistics.StatisticReading> result)
		{
			foreach (XboxOneStatistics.StatisticReading statisticReading in result)
			{
			}
		}, delegate
		{
		});
	}

	// Token: 0x060031F6 RID: 12790 RVA: 0x000D404D File Offset: 0x000D224D
	private void OnSessionEnded()
	{
	}

	// Token: 0x060031F7 RID: 12791 RVA: 0x000D404F File Offset: 0x000D224F
	private void OnResourcesLow()
	{
		if (!XboxOneUsers.ResolvingUser && !GameStateMachine.Instance.IsInExtendedTitleScreen())
		{
			UI.Menu.ShowResumeScreen();
		}
	}

	// Token: 0x060031F8 RID: 12792 RVA: 0x000D4074 File Offset: 0x000D2274
	private void OnResourcesHigh()
	{
		if (UI.Menu.ResumeScreenVisible)
		{
			return;
		}
		if (this.m_lastUser > 0 && !XboxOneUsers.HasCurrentUser)
		{
			this.m_lastUser = 0;
			XboxOneUsers.SignOutCurrentUser();
		}
		if (!XboxOneUsers.HasCurrentUser || this.m_isSwitchingUser || !XboxOneUsers.CurrentUserControllerMatch())
		{
		}
	}

	// Token: 0x060031F9 RID: 12793 RVA: 0x000D40D2 File Offset: 0x000D22D2
	private void OnResume()
	{
	}

	// Token: 0x060031FA RID: 12794 RVA: 0x000D40D4 File Offset: 0x000D22D4
	private void OnSuspend()
	{
		this.OnEngagementEnd(true);
	}

	// Token: 0x060031FB RID: 12795 RVA: 0x000D40DD File Offset: 0x000D22DD
	private void OnActiveControllerDisconnect()
	{
		this.OnEngagementEnd(false);
	}

	// Token: 0x060031FC RID: 12796 RVA: 0x000D40E6 File Offset: 0x000D22E6
	private void OnLastControllerDisconnected()
	{
		this.OnEngagementEnd(false);
	}

	// Token: 0x060031FD RID: 12797 RVA: 0x000D40F0 File Offset: 0x000D22F0
	private void OnWillSwitchController(int newController)
	{
		if (XboxOneController.ActiveController != 0UL)
		{
			XboxOneController.ResetControllerVibration();
		}
		else if (!GameController.Instance.GameInTitleScreen && this.m_isRequestingUser)
		{
			XboxOneUsers.RequestUser(null, null);
		}
	}

	// Token: 0x060031FE RID: 12798 RVA: 0x000D413C File Offset: 0x000D233C
	private void OnStatusGUI()
	{
		GUILayout.Label(string.Format("Active gamepad is {0} ({1})", XboxOneController.ActiveGamepad, XboxOneController.ActiveController), new GUILayoutOption[0]);
		GUILayout.Label(string.Concat(new object[]
		{
			"Current user: ",
			XboxOneUsers.CurrentUserHandle,
			"    UserID: ",
			XboxOneUsers.CurrentUserLocalID
		}), new GUILayoutOption[0]);
		GUILayout.Label("Session: " + XboxOneSession.SessionID, new GUILayoutOption[0]);
		GUILayout.Label(string.Format("Engaged: {0}, Request user: {1}", this.m_engaged, this.m_isRequestingUser), new GUILayoutOption[0]);
		GUILayout.Label("IsHighResources: " + XboxOneSession.IsHighResources, new GUILayoutOption[0]);
		GUILayout.Label("GameStateMachine.Instance.CurrentState: " + GameStateMachine.Instance.CurrentState, new GUILayoutOption[0]);
	}

	// Token: 0x04002CFA RID: 11514
	[NotNull]
	public MessageProvider OfflineMessageProvider;

	// Token: 0x04002CFB RID: 11515
	private static XboxOneFlow s_instance;

	// Token: 0x04002CFC RID: 11516
	private bool m_engaged;

	// Token: 0x04002CFD RID: 11517
	private bool m_isRequestingUser;

	// Token: 0x04002CFE RID: 11518
	private bool m_isSwitchingUser;

	// Token: 0x04002CFF RID: 11519
	private int m_lastUser = -1;

	// Token: 0x04002D00 RID: 11520
	private IButtonInput m_switchProfileButton = new XboxOneController.ButtonInput(XboxOneController.Button.GamepadButtonY, false);

	// Token: 0x04002D01 RID: 11521
	private int m_saveSlotToUpdate;
}
