using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class InventoryManager : MenuScreen
{
	// Token: 0x06000769 RID: 1897 RVA: 0x0001EBFC File Offset: 0x0001CDFC
	public override void Show()
	{
		this.NavigationManager.SetVisible(true);
		this.NavigationManager.SetIndexToFirst();
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0001EC15 File Offset: 0x0001CE15
	public override void Hide()
	{
		this.NavigationManager.SetVisible(false);
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x0001EC23 File Offset: 0x0001CE23
	public override void ShowImmediate()
	{
		this.NavigationManager.SetVisibleImmediate(true);
		this.NavigationManager.SetIndexToFirst();
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0001EC3C File Offset: 0x0001CE3C
	public override void HideImmediate()
	{
		this.NavigationManager.SetVisibleImmediate(false);
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0001EC4C File Offset: 0x0001CE4C
	public void Awake()
	{
		InventoryManager.Instance = this;
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OptionChangeCallback = (Action)Delegate.Combine(navigationManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
		CleverMenuItemSelectionManager navigationManager2 = this.NavigationManager;
		navigationManager2.OptionPressedCallback = (Action)Delegate.Combine(navigationManager2.OptionPressedCallback, new Action(this.OnMenuItemPressed));
		CleverMenuItemSelectionManager navigationManager3 = this.NavigationManager;
		navigationManager3.OnBackPressedCallback = (Action)Delegate.Combine(navigationManager3.OnBackPressedCallback, new Action(this.OnBackPressed));
		DifficultyController instance = DifficultyController.Instance;
		instance.OnDifficultyChanged = (Action)Delegate.Combine(instance.OnDifficultyChanged, new Action(this.OnDifficultyChanged));
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0001ECFA File Offset: 0x0001CEFA
	public void OnBackPressed()
	{
		UI.Menu.HideMenuScreen(false);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0001ED07 File Offset: 0x0001CF07
	public void OnMenuItemChange()
	{
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0001ED0C File Offset: 0x0001CF0C
	public void OnMenuItemPressed()
	{
		InventoryAbilityItem component = this.NavigationManager.CurrentMenuItem.GetComponent<InventoryAbilityItem>();
		if (component && !component.HasAbility)
		{
			if (this.PressUngainedAbilityOptionSound)
			{
				Sound.Play(this.PressUngainedAbilityOptionSound.GetSound(null), base.transform.position, null);
			}
			return;
		}
		InventoryItemHelpText component2 = this.NavigationManager.CurrentMenuItem.GetComponent<InventoryItemHelpText>();
		if (component2)
		{
			SuspensionManager.SuspendAll();
			MessageBox messageBox = UI.MessageController.ShowMessageBoxB(this.HelpMessageBox, component2.HelpMessage, Vector3.zero, float.PositiveInfinity);
			if (messageBox)
			{
				messageBox.SetAvatar(component2.Avatar);
				messageBox.OnMessageScreenHide += this.OnMessageScreenHide;
			}
			else
			{
				SuspensionManager.ResumeAll();
			}
			this.m_currentCloseMessageSound = ((!component) ? this.CloseStatisticsMessageSound : this.CloseAbilityMessageSound);
			if (component && this.PressAbilityOptionSound)
			{
				Sound.Play(this.PressAbilityOptionSound.GetSound(null), base.transform.position, null);
			}
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0001EE40 File Offset: 0x0001D040
	public void OnMessageScreenHide()
	{
		SuspensionManager.ResumeAll();
		if (this.m_currentCloseMessageSound && base.transform)
		{
			Sound.Play(this.m_currentCloseMessageSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0001EE90 File Offset: 0x0001D090
	public void OnDestroy()
	{
		if (InventoryManager.Instance == this)
		{
			InventoryManager.Instance = null;
		}
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OptionChangeCallback = (Action)Delegate.Remove(navigationManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
		CleverMenuItemSelectionManager navigationManager2 = this.NavigationManager;
		navigationManager2.OptionPressedCallback = (Action)Delegate.Remove(navigationManager2.OptionPressedCallback, new Action(this.OnMenuItemPressed));
		CleverMenuItemSelectionManager navigationManager3 = this.NavigationManager;
		navigationManager3.OnBackPressedCallback = (Action)Delegate.Remove(navigationManager3.OnBackPressedCallback, new Action(this.OnBackPressed));
		DifficultyController instance = DifficultyController.Instance;
		instance.OnDifficultyChanged = (Action)Delegate.Remove(instance.OnDifficultyChanged, new Action(this.OnDifficultyChanged));
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0001EF4E File Offset: 0x0001D14E
	public void OnDifficultyChanged()
	{
		if (this.Difficulty)
		{
			this.Difficulty.RefreshText();
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0001EF6C File Offset: 0x0001D16C
	public void UpdateItems()
	{
		SeinCharacter sein = Characters.Sein;
		if (sein == null)
		{
			return;
		}
		this.CompletionText.SetMessage(new MessageDescriptor(GameWorld.Instance.CompletionPercentage + "%"));
		this.DeathText.SetMessage(new MessageDescriptor(SeinDeathCounter.Count.ToString()));
		this.HealthUpgradesText.SetMessage(new MessageDescriptor(sein.Mortality.Health.HealthUpgradesCollected + " / " + 12));
		this.EnergyUpgradesText.SetMessage(new MessageDescriptor(sein.Energy.EnergyUpgradesCollected + " / " + 15));
		this.SkillPointUniquesText.SetMessage(new MessageDescriptor(sein.Inventory.SkillPointsCollected + " / " + 33));
		GameTimer timer = GameController.Instance.Timer;
		this.TimeText.SetMessage(new MessageDescriptor(string.Format("{0:D2}:{1:D2}:{2:D2}", timer.Hours, timer.Minutes, timer.Seconds)));
		CleverMenuItem currentMenuItem = this.NavigationManager.CurrentMenuItem;
		InventoryAbilityItem component = currentMenuItem.GetComponent<InventoryAbilityItem>();
		if (component)
		{
			this.AbilityNameText.gameObject.SetActive(true);
			this.AbilityItemHighlight.SetActive(true);
			this.AbilityItemHighlight.transform.position = component.transform.position;
			if (component.HasAbility)
			{
				this.AbilityNameText.SetMessageProvider(component.AbilityName);
			}
			else
			{
				this.AbilityNameText.SetMessageProvider(this.LockedMessageProvider);
			}
		}
		else
		{
			this.AbilityNameText.gameObject.SetActive(false);
			this.AbilityItemHighlight.SetActive(false);
		}
		if (this.Difficulty)
		{
			this.Difficulty.RefreshText();
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0001F171 File Offset: 0x0001D371
	public void FixedUpdate()
	{
		this.UpdateItems();
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0001F179 File Offset: 0x0001D379
	public void OnEnable()
	{
		this.UpdateItems();
	}

	// Token: 0x04000580 RID: 1408
	public const int TotalHealthUpgrades = 12;

	// Token: 0x04000581 RID: 1409
	public const int TotalEnergyUpgrades = 15;

	// Token: 0x04000582 RID: 1410
	public const int TotalSkillPoints = 33;

	// Token: 0x04000583 RID: 1411
	public const int MaxLevel = 20;

	// Token: 0x04000584 RID: 1412
	public static InventoryManager Instance;

	// Token: 0x04000585 RID: 1413
	public CleverMenuItemSelectionManager NavigationManager;

	// Token: 0x04000586 RID: 1414
	public SoundProvider OpenSound;

	// Token: 0x04000587 RID: 1415
	public SoundProvider CloseSound;

	// Token: 0x04000588 RID: 1416
	public SoundProvider PressAbilityOptionSound;

	// Token: 0x04000589 RID: 1417
	public SoundProvider PressUngainedAbilityOptionSound;

	// Token: 0x0400058A RID: 1418
	public SoundProvider CloseAbilityMessageSound;

	// Token: 0x0400058B RID: 1419
	public SoundProvider CloseStatisticsMessageSound;

	// Token: 0x0400058C RID: 1420
	private SoundProvider m_currentCloseMessageSound;

	// Token: 0x0400058D RID: 1421
	public GameObject AbilityItemHighlight;

	// Token: 0x0400058E RID: 1422
	public MessageBox AbilityNameText;

	// Token: 0x0400058F RID: 1423
	public MessageBox TimeText;

	// Token: 0x04000590 RID: 1424
	public MessageBox CompletionText;

	// Token: 0x04000591 RID: 1425
	public MessageBox DeathText;

	// Token: 0x04000592 RID: 1426
	public MessageBox HealthUpgradesText;

	// Token: 0x04000593 RID: 1427
	public MessageBox EnergyUpgradesText;

	// Token: 0x04000594 RID: 1428
	public MessageBox SkillPointUniquesText;

	// Token: 0x04000595 RID: 1429
	public GameObject GinsoTreeKey;

	// Token: 0x04000596 RID: 1430
	public GameObject ForlornRuinsKey;

	// Token: 0x04000597 RID: 1431
	public GameObject MountHoruKey;

	// Token: 0x04000598 RID: 1432
	public GameObject WorldEventsGroup;

	// Token: 0x04000599 RID: 1433
	public MessageBox Difficulty;

	// Token: 0x0400059A RID: 1434
	public MessageProvider LockedMessageProvider;

	// Token: 0x0400059B RID: 1435
	public MessageProvider NotAvailableYetMessageProvider;

	// Token: 0x0400059C RID: 1436
	public MessageProvider DiedZeroTimesMessageProvider;

	// Token: 0x0400059D RID: 1437
	public MessageProvider DiedOneTimeMessagProvider;

	// Token: 0x0400059E RID: 1438
	public MessageProvider DiedMultipleTimesMessageProvider;

	// Token: 0x0400059F RID: 1439
	public GameObject HelpMessageBox;
}
