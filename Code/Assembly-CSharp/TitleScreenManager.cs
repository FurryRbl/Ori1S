using System;
using UnityEngine;

// Token: 0x020007A5 RID: 1957
public class TitleScreenManager : MonoBehaviour
{
	// Token: 0x06002D63 RID: 11619 RVA: 0x000C1F45 File Offset: 0x000C0145
	public void Awake()
	{
		TitleScreenManager.s_instance = this;
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x000C1F4D File Offset: 0x000C014D
	public void Start()
	{
		this.Startup();
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x000C1F58 File Offset: 0x000C0158
	public void Startup()
	{
		foreach (BaseAnimator baseAnimator in this.AnimatorsToReset)
		{
			baseAnimator.Initialize();
			baseAnimator.AnimatorDriver.GoToStart();
			baseAnimator.AnimatorDriver.Pause();
		}
		if (this.OnTitleScreenStartup)
		{
			this.OnTitleScreenStartup.Perform(null);
		}
		TitleScreenManager.SetScreenImmediate(TitleScreenManager.Screen.PressStart);
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x000C1FC2 File Offset: 0x000C01C2
	public static void OnReturnToTitleScreen()
	{
		TitleScreenManager.s_instance.Startup();
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x000C1FCE File Offset: 0x000C01CE
	public void OnDestroy()
	{
		if (TitleScreenManager.s_instance == this)
		{
			TitleScreenManager.s_instance = null;
		}
	}

	// Token: 0x17000745 RID: 1861
	// (get) Token: 0x06002D68 RID: 11624 RVA: 0x000C1FE6 File Offset: 0x000C01E6
	public static TitleScreenManager.Screen CurrentScreen
	{
		get
		{
			if (TitleScreenManager.s_instance)
			{
				return TitleScreenManager.s_instance.m_currentScreen;
			}
			return TitleScreenManager.Screen.Undefined;
		}
	}

	// Token: 0x06002D69 RID: 11625 RVA: 0x000C2004 File Offset: 0x000C0204
	public static void SetScreen(TitleScreenManager.Screen screen)
	{
		if (TitleScreenManager.s_instance == null)
		{
			return;
		}
		TitleScreenManager.s_instance.m_currentScreen = screen;
		TitleScreenManager.s_instance.PressStartScreen.SetActive(screen == TitleScreenManager.Screen.PressStart);
		TitleScreenManager.s_instance.WaitingForSaveGameScreen.SetActive(screen == TitleScreenManager.Screen.WaitingForSaveGame);
		TitleScreenManager.SetVisible(TitleScreenManager.s_instance.MainMenuScreen, screen == TitleScreenManager.Screen.MainMenu);
		TitleScreenManager.SetVisible(TitleScreenManager.s_instance.ExitGameScreen, screen == TitleScreenManager.Screen.ExitGame);
		TitleScreenManager.SetVisible(TitleScreenManager.s_instance.CutscenesScreen, screen == TitleScreenManager.Screen.Cutscenes);
		TitleScreenManager.SetVisible(TitleScreenManager.s_instance.DemoMenuScreen, screen == TitleScreenManager.Screen.DemoMenu);
		TitleScreenManager.s_instance.SaveSlotsScreen.SetVisible(screen == TitleScreenManager.Screen.SaveSlots);
		TitleScreenManager.s_instance.TrialHelpScreen.SetActive(screen == TitleScreenManager.Screen.TrialHelp);
	}

	// Token: 0x06002D6A RID: 11626 RVA: 0x000C20C8 File Offset: 0x000C02C8
	public static void SetScreenImmediate(TitleScreenManager.Screen screen)
	{
		if (TitleScreenManager.s_instance == null)
		{
			return;
		}
		TitleScreenManager.s_instance.gameObject.SetActive(true);
		TitleScreenManager.s_instance.m_currentScreen = screen;
		TitleScreenManager.s_instance.PressStartScreen.SetActive(screen == TitleScreenManager.Screen.PressStart);
		TitleScreenManager.s_instance.WaitingForSaveGameScreen.SetActive(screen == TitleScreenManager.Screen.WaitingForSaveGame);
		TitleScreenManager.SetVisibleImmediate(TitleScreenManager.s_instance.MainMenuScreen, screen == TitleScreenManager.Screen.MainMenu);
		TitleScreenManager.SetVisibleImmediate(TitleScreenManager.s_instance.ExitGameScreen, screen == TitleScreenManager.Screen.ExitGame);
		TitleScreenManager.SetVisibleImmediate(TitleScreenManager.s_instance.CutscenesScreen, screen == TitleScreenManager.Screen.Cutscenes);
		TitleScreenManager.SetVisibleImmediate(TitleScreenManager.s_instance.DemoMenuScreen, screen == TitleScreenManager.Screen.DemoMenu);
		TitleScreenManager.s_instance.SaveSlotsScreen.SetVisibleImmediate(screen == TitleScreenManager.Screen.SaveSlots);
		TitleScreenManager.s_instance.TrialHelpScreen.SetActive(screen == TitleScreenManager.Screen.TrialHelp);
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x000C2199 File Offset: 0x000C0399
	private static void SetVisible(CleverMenuItemSelectionManager manager, bool visible)
	{
		manager.SetVisible(visible);
		manager.IsActive = visible;
		manager.IsLocked = false;
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x000C21B0 File Offset: 0x000C03B0
	private static void SetVisibleImmediate(CleverMenuItemSelectionManager manager, bool visible)
	{
		manager.SetVisibleImmediate(visible);
		manager.IsActive = visible;
		manager.IsLocked = false;
	}

	// Token: 0x040028EA RID: 10474
	private static TitleScreenManager s_instance;

	// Token: 0x040028EB RID: 10475
	public ActionMethod OnTitleScreenStartup;

	// Token: 0x040028EC RID: 10476
	public GameObject PressStartScreen;

	// Token: 0x040028ED RID: 10477
	public GameObject WaitingForSaveGameScreen;

	// Token: 0x040028EE RID: 10478
	public CleverMenuItemSelectionManager MainMenuScreen;

	// Token: 0x040028EF RID: 10479
	public CleverMenuItemSelectionManager ExitGameScreen;

	// Token: 0x040028F0 RID: 10480
	public CleverMenuItemSelectionManager CutscenesScreen;

	// Token: 0x040028F1 RID: 10481
	public CleverMenuItemSelectionManager DemoMenuScreen;

	// Token: 0x040028F2 RID: 10482
	public SaveSlotsUI SaveSlotsScreen;

	// Token: 0x040028F3 RID: 10483
	public GameObject TrialHelpScreen;

	// Token: 0x040028F4 RID: 10484
	public BaseAnimator[] AnimatorsToReset;

	// Token: 0x040028F5 RID: 10485
	private TitleScreenManager.Screen m_currentScreen;

	// Token: 0x020007A6 RID: 1958
	public enum Screen
	{
		// Token: 0x040028F7 RID: 10487
		PressStart,
		// Token: 0x040028F8 RID: 10488
		WaitingForSaveGame,
		// Token: 0x040028F9 RID: 10489
		MainMenu,
		// Token: 0x040028FA RID: 10490
		ExitGame,
		// Token: 0x040028FB RID: 10491
		Cutscenes,
		// Token: 0x040028FC RID: 10492
		DemoMenu,
		// Token: 0x040028FD RID: 10493
		TrialHelp,
		// Token: 0x040028FE RID: 10494
		SaveSlots,
		// Token: 0x040028FF RID: 10495
		Undefined
	}
}
