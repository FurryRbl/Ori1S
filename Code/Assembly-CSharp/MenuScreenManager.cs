using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class MenuScreenManager : Suspendable
{
	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001D0F9 File Offset: 0x0001B2F9
	public static bool MenuOpenKeyPressed
	{
		get
		{
			return Core.Input.Start.OnPressed && !Core.Input.Start.Used;
		}
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0001D11C File Offset: 0x0001B31C
	public new void Awake()
	{
		base.Awake();
		UI.Menu = this;
		this.SetupOptionsScreen(this.OptionsPrefab);
		this.SetupPauseScreen(this.PausePrefab);
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		UberPostProcess.Instance.SetDoBlur(false);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0001D173 File Offset: 0x0001B373
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0001D196 File Offset: 0x0001B396
	public void OnGameReset()
	{
		this.LockClosingMenu = false;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0001D19F File Offset: 0x0001B39F
	private bool CanOpenMenus()
	{
		return GameController.Instance.MainMenuCanBeOpened && !UI.Fader.IsFadingInOrStay();
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
	public bool IsInventoryVisible()
	{
		return this.MainMenuVisible && this.CurrentScreen == MenuScreenManager.Screens.Inventory;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (MenuScreenManager.MenuOpenKeyPressed)
		{
			this.DoMenuKeyPress();
		}
		if (Characters.Sein && Characters.Sein.Controller.CanMove && !Core.Input.Select.Used && Core.Input.Select.OnPressed)
		{
			Core.Input.Select.Used = true;
			Core.Input.Cancel.Used = true;
			if (this.MainMenuVisible)
			{
				this.HideMenuScreen(false);
			}
			else if (this.CanOpenMenus() && Characters.Sein && Characters.Sein.Controller.CanMove && Characters.Sein.Active && GameMapUI.Instance != null && WorldMapLogic.Instance != null && WorldMapLogic.Instance.MapEnabledArea.FindFaceAtPositionFaster(Characters.Sein.Position) != null && World.CurrentArea != null && !GameMapUI.Instance.ShowingTeleporters && WorldMapUI.IsReady)
			{
				this.ShowAreaMap();
			}
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0001D31C File Offset: 0x0001B51C
	public void DoMenuKeyPress()
	{
		if (this.MainMenuVisible)
		{
			this.HideMenuScreen(false);
			Core.Input.Cancel.Used = true;
			Core.Input.Start.Used = true;
		}
		else if (this.CanOpenMenus())
		{
			Core.Input.Cancel.Used = true;
			Core.Input.Start.Used = true;
			this.ShowInventoryOrPauseMenu();
		}
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0001D37D File Offset: 0x0001B57D
	public void ShowInventoryOrPauseMenu()
	{
		if (InventoryManager.Instance)
		{
			this.ShowInventory();
		}
		else
		{
			this.ShowMenuScreen(false);
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
	private void Show(MenuScreen menuScreen, MenuScreenManager.Screens screen)
	{
		if (menuScreen)
		{
			if (this.CurrentScreen == screen)
			{
				menuScreen.Show();
			}
			else
			{
				menuScreen.Hide();
			}
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
	public void ChangeScreen(MenuScreenManager.Screens screen)
	{
		this.CurrentScreen = screen;
		this.Show(SkillTreeManager.Instance, MenuScreenManager.Screens.SkillTree);
		this.Show(GameMapUI.Instance, MenuScreenManager.Screens.WorldMap);
		this.Show(OptionsScreen.Instance, MenuScreenManager.Screens.Options);
		this.Show(InventoryManager.Instance, MenuScreenManager.Screens.Inventory);
		this.Show(PauseScreen.Instance, MenuScreenManager.Screens.Pause);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0001D428 File Offset: 0x0001B628
	public void ShowMenuScreen(bool immediate = false)
	{
		UI.Hints.HideExistingHint();
		this.ShowMenuScreen(MenuScreenManager.Screens.Pause, true);
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001D437 File Offset: 0x0001B637
	public bool ResumeScreenVisible
	{
		get
		{
			return this.m_resumeScreen != null;
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0001D448 File Offset: 0x0001B648
	public void ShowResumeScreen()
	{
		if (this.ResumeScreenVisible)
		{
			return;
		}
		UI.Hints.HideExistingHint();
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.ResumeScreen, Vector3.zero, Quaternion.identity);
		this.m_resumeScreen = gameObject.GetComponent<ResumeGameController>();
		XboxOneSession.PauseSession();
		if (!this.MainMenuVisible)
		{
			HideWhenMainMenuOpen.OnMenuShow();
		}
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
	public void HideResumeScreen()
	{
		if (this.m_resumeScreen)
		{
			XboxOneSession.ResumeSession();
			this.m_resumeScreen.Hide();
			this.m_resumeScreen = null;
			this.PlayOpenSound(this.CurrentScreen, false);
			if (!this.MainMenuVisible)
			{
				UberPostProcess.Instance.SetDoBlur(false);
				HideWhenMainMenuOpen.OnMenuHide();
			}
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0001D501 File Offset: 0x0001B701
	public void ShowInventory()
	{
		LeaderboardsController.UploadScore();
		UI.Hints.HideExistingHint();
		this.ShowMenuScreen(MenuScreenManager.Screens.Inventory, true);
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0001D515 File Offset: 0x0001B715
	public void ShowOptions()
	{
		UI.Hints.HideExistingHint();
		this.ShowMenuScreen(MenuScreenManager.Screens.Options, true);
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0001D524 File Offset: 0x0001B724
	public void ShowAreaMap()
	{
		UI.Hints.HideExistingHint();
		this.ShowMenuScreen(MenuScreenManager.Screens.WorldMap, true);
		AreaMapUI.Instance.Navigation.CenterMapOnWorldPosition(Characters.Sein.Position);
		GameMapTransitionManager.Instance.GoToAreaMapInstantly();
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0001D561 File Offset: 0x0001B761
	public void ShowWorldMap(bool playOpenSound = true)
	{
		UI.Hints.HideExistingHint();
		this.ShowMenuScreen(MenuScreenManager.Screens.WorldMap, playOpenSound);
		GameMapTransitionManager.Instance.GoToWorldMapInstantly();
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0001D57A File Offset: 0x0001B77A
	public void ShowObjective(Objective objective, Action method)
	{
		GameMapUI.Instance.ShowObjective.ShowObjective(objective, method);
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0001D58D File Offset: 0x0001B78D
	public void ShowSkillTree()
	{
		UI.Hints.HideExistingHint();
		this.ShowMenuScreen(MenuScreenManager.Screens.SkillTree, true);
		Core.Input.Cancel.Used = true;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
	private void ShowMenuScreen(MenuScreenManager.Screens screen, bool playOpenSound = true)
	{
		if (!this.MainMenuVisible)
		{
			UberPostProcess.Instance.SetDoBlur(true);
			this.MainMenuVisible = true;
			this.m_suspendables = SuspensionManager.GetSuspendables(this.m_suspendables, true, base.gameObject);
			SuspensionManager.SuspendExcluding(this.m_suspendables);
			Events.Scheduler.OnMenuOpen.Call();
			XboxOneSession.PauseSession();
		}
		this.ChangeScreen(screen);
		if (playOpenSound)
		{
			this.PlayOpenSound(this.CurrentScreen, true);
		}
		HideWhenMainMenuOpen.OnMenuShow();
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0001D62C File Offset: 0x0001B82C
	public void HideMenuScreen(bool immediate = false)
	{
		if (this.LockClosingMenu)
		{
			return;
		}
		if (CameraCrossFadeManager.CrossFadeMenuHack != MoonGuid.Empty)
		{
			if (!Scenes.Manager.SceneIsLoaded(CameraCrossFadeManager.CrossFadeMenuHack))
			{
				return;
			}
			CameraCrossFadeManager.CrossFadeMenuHack = MoonGuid.Empty;
		}
		if (!this.MainMenuVisible)
		{
			return;
		}
		this.MainMenuVisible = false;
		Events.Scheduler.OnMenuClose.Call();
		try
		{
			if (immediate)
			{
				switch (this.CurrentScreen)
				{
				case MenuScreenManager.Screens.Pause:
					PauseScreen.Instance.HideImmediate();
					break;
				case MenuScreenManager.Screens.SkillTree:
					SkillTreeManager.Instance.HideImmediate();
					break;
				case MenuScreenManager.Screens.WorldMap:
					GameMapUI.Instance.HideImmediate();
					break;
				case MenuScreenManager.Screens.Options:
					OptionsScreen.Instance.HideImmediate();
					break;
				case MenuScreenManager.Screens.Inventory:
					InventoryManager.Instance.HideImmediate();
					break;
				}
			}
			else
			{
				switch (this.CurrentScreen)
				{
				case MenuScreenManager.Screens.Pause:
					PauseScreen.Instance.Hide();
					break;
				case MenuScreenManager.Screens.SkillTree:
					SkillTreeManager.Instance.Hide();
					break;
				case MenuScreenManager.Screens.WorldMap:
					GameMapUI.Instance.Hide();
					break;
				case MenuScreenManager.Screens.Options:
					OptionsScreen.Instance.Hide();
					break;
				case MenuScreenManager.Screens.Inventory:
					InventoryManager.Instance.Hide();
					break;
				}
			}
			XboxOneSession.ResumeSession();
		}
		catch (Exception ex)
		{
		}
		if (!immediate)
		{
			this.PlayOpenSound(this.CurrentScreen, false);
		}
		SuspensionManager.ResumeExcluding(this.m_suspendables);
		this.m_suspendables.Clear();
		UberPostProcess.Instance.SetDoBlur(false);
		HideWhenMainMenuOpen.OnMenuHide();
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
	public void PlayOpenSound(MenuScreenManager.Screens screen, bool open)
	{
		switch (screen)
		{
		case MenuScreenManager.Screens.Pause:
			Sound.Play((!open) ? this.CloseSound.GetSound(null) : this.OpenSound.GetSound(null), base.transform.position, null);
			break;
		case MenuScreenManager.Screens.SkillTree:
			if (SkillTreeManager.Instance)
			{
				Sound.Play((!open) ? SkillTreeManager.Instance.CloseSound.GetSound(null) : SkillTreeManager.Instance.OpenSound.GetSound(null), base.transform.position, null);
			}
			break;
		case MenuScreenManager.Screens.WorldMap:
			if (AreaMapUI.Instance)
			{
				if (GameMapUI.Instance.ShowingTeleporters)
				{
					if (!open)
					{
						Sound.Play(GameMapUI.Instance.Teleporters.CloseWindowSound.GetSound(null), Vector3.zero, null);
					}
				}
				else
				{
					Sound.Play((!open) ? AreaMapUI.Instance.CloseSound.GetSound(null) : AreaMapUI.Instance.OpenSound.GetSound(null), base.transform.position, null);
				}
			}
			break;
		case MenuScreenManager.Screens.Options:
			if (OptionsScreen.Instance)
			{
				Sound.Play((!open) ? OptionsScreen.Instance.CloseSound.GetSound(null) : OptionsScreen.Instance.OpenSound.GetSound(null), base.transform.position, null);
			}
			break;
		case MenuScreenManager.Screens.Inventory:
			if (InventoryManager.Instance)
			{
				Sound.Play((!open) ? InventoryManager.Instance.CloseSound.GetSound(null) : InventoryManager.Instance.OpenSound.GetSound(null), base.transform.position, null);
			}
			break;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001D9CF File Offset: 0x0001BBCF
	// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001D9D7 File Offset: 0x0001BBD7
	public override bool IsSuspended { get; set; }

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
	// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
	public bool MainMenuVisible
	{
		get
		{
			return this.m_isPaused;
		}
		set
		{
			this.m_isPaused = value;
		}
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
	public GameObject Instantiate(GameObject prefab)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
		gameObject.name = prefab.name;
		SaveSceneManager.Master.RegisterGameObject(gameObject);
		gameObject.transform.parent = base.transform;
		gameObject.SetActive(false);
		gameObject.AddComponent<LoadFromMasterAtStart>();
		return gameObject;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0001DA50 File Offset: 0x0001BC50
	public void SetupWorldMapUI(GameObject worldMapUI)
	{
		if (GameMapUI.Instance == null)
		{
			GameObject go = this.Instantiate(worldMapUI);
			Utility.DontAssociateWithAnyScene(go);
		}
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0001DA7C File Offset: 0x0001BC7C
	public void SetupSkillTreeUI(GameObject skillTreeUIPrefab)
	{
		if (SkillTreeManager.Instance == null)
		{
			GameObject go = this.Instantiate(skillTreeUIPrefab);
			Utility.DontAssociateWithAnyScene(go);
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
	public void SetupOptionsScreen(GameObject optionsPrefab)
	{
		if (OptionsScreen.Instance == null)
		{
			GameObject go = this.Instantiate(optionsPrefab);
			Utility.DontAssociateWithAnyScene(go);
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
	public void SetupPauseScreen(GameObject pauseScreenPrefab)
	{
		if (PauseScreen.Instance == null)
		{
			GameObject go = this.Instantiate(pauseScreenPrefab);
			Utility.DontAssociateWithAnyScene(go);
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0001DB00 File Offset: 0x0001BD00
	public void SetupInventoryUI(GameObject inventoryManager)
	{
		if (InventoryManager.Instance == null)
		{
			GameObject go = this.Instantiate(inventoryManager);
			Utility.DontAssociateWithAnyScene(go);
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0001DB2C File Offset: 0x0001BD2C
	public void RemoveGameplayObjects()
	{
		if (InventoryManager.Instance)
		{
			InstantiateUtility.Destroy(InventoryManager.Instance.gameObject);
		}
		if (SkillTreeManager.Instance)
		{
			InstantiateUtility.Destroy(SkillTreeManager.Instance.gameObject);
		}
		if (GameMapUI.Instance)
		{
			InstantiateUtility.Destroy(GameMapUI.Instance.gameObject);
		}
	}

	// Token: 0x04000546 RID: 1350
	public GameObject ResumeScreen;

	// Token: 0x04000547 RID: 1351
	public GameObject OptionsPrefab;

	// Token: 0x04000548 RID: 1352
	public GameObject PausePrefab;

	// Token: 0x04000549 RID: 1353
	public MenuScreenManager.Screens CurrentScreen;

	// Token: 0x0400054A RID: 1354
	public SoundProvider OpenSound;

	// Token: 0x0400054B RID: 1355
	public SoundProvider CloseSound;

	// Token: 0x0400054C RID: 1356
	private HashSet<ISuspendable> m_suspendables = new HashSet<ISuspendable>();

	// Token: 0x0400054D RID: 1357
	private ResumeGameController m_resumeScreen;

	// Token: 0x0400054E RID: 1358
	public bool LockClosingMenu;

	// Token: 0x0400054F RID: 1359
	private bool m_isPaused;

	// Token: 0x02000143 RID: 323
	public enum Screens
	{
		// Token: 0x04000A65 RID: 2661
		Pause,
		// Token: 0x04000A66 RID: 2662
		SkillTree,
		// Token: 0x04000A67 RID: 2663
		WorldMap,
		// Token: 0x04000A68 RID: 2664
		Options,
		// Token: 0x04000A69 RID: 2665
		Inventory
	}
}
