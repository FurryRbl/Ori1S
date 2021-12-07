using System;
using Core;
using UnityEngine;

// Token: 0x02000877 RID: 2167
public class AreaMapLegend : MonoBehaviour
{
	// Token: 0x060030F3 RID: 12531 RVA: 0x000D0A1F File Offset: 0x000CEC1F
	public void Awake()
	{
	}

	// Token: 0x060030F4 RID: 12532 RVA: 0x000D0A24 File Offset: 0x000CEC24
	public void FixedUpdate()
	{
		if (Core.Input.Cancel.OnPressed)
		{
			Core.Input.Cancel.Used = true;
			this.Hide();
		}
	}

	// Token: 0x060030F5 RID: 12533 RVA: 0x000D0A51 File Offset: 0x000CEC51
	public void Toggle()
	{
		if (this.m_visible)
		{
			this.Hide();
		}
		else
		{
			this.Show();
		}
	}

	// Token: 0x060030F6 RID: 12534 RVA: 0x000D0A70 File Offset: 0x000CEC70
	public void Show()
	{
		this.m_visible = true;
		base.gameObject.SetActive(true);
		if (this.AppearSound)
		{
			Sound.Play(this.AppearSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x060030F7 RID: 12535 RVA: 0x000D0AC0 File Offset: 0x000CECC0
	public void Hide()
	{
		this.m_visible = false;
		base.gameObject.SetActive(false);
		if (this.DisappearSound)
		{
			Sound.Play(this.DisappearSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x000D0B10 File Offset: 0x000CED10
	public void HideSilently()
	{
		if (this.DisappearSound)
		{
			SoundProvider disappearSound = this.DisappearSound;
			this.DisappearSound = null;
			this.Hide();
			this.DisappearSound = disappearSound;
			return;
		}
		this.Hide();
	}

	// Token: 0x04002C4C RID: 11340
	public SoundProvider AppearSound;

	// Token: 0x04002C4D RID: 11341
	public SoundProvider DisappearSound;

	// Token: 0x04002C4E RID: 11342
	private bool m_visible;
}
