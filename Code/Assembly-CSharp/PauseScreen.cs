using System;
using Game;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class PauseScreen : MenuScreen
{
	// Token: 0x06000D4B RID: 3403 RVA: 0x0003E08E File Offset: 0x0003C28E
	public void Awake()
	{
		PauseScreen.Instance = this;
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OnBackPressedCallback = (Action)Delegate.Combine(navigationManager.OnBackPressedCallback, new Action(this.OnBackPressed));
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0003E0BD File Offset: 0x0003C2BD
	public void OnDestroy()
	{
		if (PauseScreen.Instance == this)
		{
			PauseScreen.Instance = null;
		}
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OnBackPressedCallback = (Action)Delegate.Remove(navigationManager.OnBackPressedCallback, new Action(this.OnBackPressed));
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0003E0FC File Offset: 0x0003C2FC
	public void OnBackPressed()
	{
		UI.Menu.HideMenuScreen(false);
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0003E109 File Offset: 0x0003C309
	public override void Hide()
	{
		this.NavigationManager.SetVisible(false);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0003E117 File Offset: 0x0003C317
	public override void HideImmediate()
	{
		this.NavigationManager.SetVisibleImmediate(false);
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0003E125 File Offset: 0x0003C325
	public override void Show()
	{
		this.NavigationManager.SetVisible(true);
		this.OnShow();
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0003E139 File Offset: 0x0003C339
	public override void ShowImmediate()
	{
		this.NavigationManager.SetVisibleImmediate(true);
		this.OnShow();
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0003E150 File Offset: 0x0003C350
	public void OnShow()
	{
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.Prologue)
		{
			this.SkipText.SetMessage(new MessageDescriptor(this.SkipPrologueMessage.ToString()));
		}
		else
		{
			this.SkipText.SetMessage(new MessageDescriptor(this.SkipCutsceneMessage.ToString()));
		}
		this.NavigationManager.SetIndexToFirst();
		this.Fader.SetActive(false);
	}

	// Token: 0x04000AE1 RID: 2785
	public static PauseScreen Instance;

	// Token: 0x04000AE2 RID: 2786
	public CleverMenuItemSelectionManager NavigationManager;

	// Token: 0x04000AE3 RID: 2787
	public MessageBox SkipText;

	// Token: 0x04000AE4 RID: 2788
	public MessageProvider SkipCutsceneMessage;

	// Token: 0x04000AE5 RID: 2789
	public MessageProvider SkipPrologueMessage;

	// Token: 0x04000AE6 RID: 2790
	public GameObject Fader;
}
