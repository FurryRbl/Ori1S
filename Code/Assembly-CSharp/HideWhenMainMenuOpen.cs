using System;
using Game;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class HideWhenMainMenuOpen : MonoBehaviour
{
	// Token: 0x06000C94 RID: 3220 RVA: 0x00039254 File Offset: 0x00037454
	public static void OnMenuShow()
	{
		for (int i = 0; i < HideWhenMainMenuOpen.m_all.Count; i++)
		{
			HideWhenMainMenuOpen hideWhenMainMenuOpen = HideWhenMainMenuOpen.m_all[i];
			hideWhenMainMenuOpen.MakeInvisible();
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00039290 File Offset: 0x00037490
	public static void OnMenuHide()
	{
		for (int i = 0; i < HideWhenMainMenuOpen.m_all.Count; i++)
		{
			HideWhenMainMenuOpen hideWhenMainMenuOpen = HideWhenMainMenuOpen.m_all[i];
			hideWhenMainMenuOpen.MakeVisible();
		}
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x000392CC File Offset: 0x000374CC
	public void Awake()
	{
		HideWhenMainMenuOpen.m_all.Add(this);
		if (this.Fader)
		{
			this.Fader.Initialize();
			this.Fader.AnimatorDriver.GoToEnd();
		}
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0003930F File Offset: 0x0003750F
	public void OnDestroy()
	{
		HideWhenMainMenuOpen.m_all.Remove(this);
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0003931C File Offset: 0x0003751C
	private void MakeVisible()
	{
		if (this.Fader)
		{
			this.Fader.AnimatorDriver.GoToEnd();
		}
		else
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0003935C File Offset: 0x0003755C
	private void MakeInvisible()
	{
		if (this.Fader)
		{
			this.Fader.AnimatorDriver.GoToStart();
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0003939A File Offset: 0x0003759A
	private bool MainMenuVisible
	{
		get
		{
			return UI.MainMenuVisible;
		}
	}

	// Token: 0x04000A62 RID: 2658
	private static readonly AllContainer<HideWhenMainMenuOpen> m_all = new AllContainer<HideWhenMainMenuOpen>();

	// Token: 0x04000A63 RID: 2659
	public TransparencyAnimator Fader;
}
