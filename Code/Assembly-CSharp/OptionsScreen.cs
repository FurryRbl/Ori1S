using System;
using Core;
using Game;

// Token: 0x02000127 RID: 295
public class OptionsScreen : MenuScreen, ISuspendable
{
	// Token: 0x06000BEC RID: 3052 RVA: 0x0003501E File Offset: 0x0003321E
	public void Awake()
	{
		OptionsScreen.Instance = this;
		SuspensionManager.Register(this);
		CleverMenuItemSelectionManager navigation = this.Navigation;
		navigation.OnBackPressedCallback = (Action)Delegate.Combine(navigation.OnBackPressedCallback, new Action(this.OnBackPressed));
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00035053 File Offset: 0x00033253
	public void OnDestroy()
	{
		CleverMenuItemSelectionManager navigation = this.Navigation;
		navigation.OnBackPressedCallback = (Action)Delegate.Remove(navigation.OnBackPressedCallback, new Action(this.OnBackPressed));
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0003507C File Offset: 0x0003327C
	public void FixedUpdate()
	{
		if (Input.Bash.OnPressed)
		{
			XboxOne.Help();
		}
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00035094 File Offset: 0x00033294
	public override void Hide()
	{
		this.Navigation.SetVisible(false);
		CleverMenuItemGroup component = base.GetComponent<CleverMenuItemGroup>();
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in component.Options)
		{
			if (cleverMenuItemGroupItem.ItemGroup)
			{
				cleverMenuItemGroupItem.ItemGroup.IsActive = false;
			}
		}
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00035118 File Offset: 0x00033318
	public override void ShowImmediate()
	{
		this.Navigation.SetVisibleImmediate(true);
		this.Navigation.SetIndexToFirst();
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00035131 File Offset: 0x00033331
	public override void HideImmediate()
	{
		this.Navigation.SetVisibleImmediate(false);
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00035140 File Offset: 0x00033340
	public override void Show()
	{
		this.Navigation.RefreshVisible();
		this.Navigation.SetVisible(true);
		this.Navigation.SetIndexToFirst();
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x0003516F File Offset: 0x0003336F
	public void OnBackPressed()
	{
		if (GameController.Instance.GameInTitleScreen)
		{
			UI.Menu.HideMenuScreen(false);
		}
		else
		{
			UI.Menu.ShowInventoryOrPauseMenu();
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0003519A File Offset: 0x0003339A
	// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x000351A2 File Offset: 0x000333A2
	public bool IsSuspended { get; set; }

	// Token: 0x040009A9 RID: 2473
	public static OptionsScreen Instance;

	// Token: 0x040009AA RID: 2474
	public SoundProvider OpenSound;

	// Token: 0x040009AB RID: 2475
	public SoundProvider CloseSound;

	// Token: 0x040009AC RID: 2476
	public CleverMenuItemSelectionManager Navigation;
}
