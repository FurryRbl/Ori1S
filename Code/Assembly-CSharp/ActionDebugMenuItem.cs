using System;
using Core;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class ActionDebugMenuItem : IDebugMenuItem
{
	// Token: 0x06002045 RID: 8261 RVA: 0x0008CD7D File Offset: 0x0008AF7D
	public ActionDebugMenuItem()
	{
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x0008CD85 File Offset: 0x0008AF85
	public ActionDebugMenuItem(string s, Func<bool> func)
	{
		this.HelpText = "Press X or A to invoke the action";
		this.TitleText = s;
		this.Func = func;
	}

	// Token: 0x06002047 RID: 8263 RVA: 0x0008CDA8 File Offset: 0x0008AFA8
	public void Draw(Rect rect, bool selected)
	{
		GUIStyle style = (!selected) ? DebugMenuB.Style : DebugMenuB.SelectedStyle;
		if (selected && this.m_pressedTimeCountDown > 0f)
		{
			style = DebugMenuB.PressedStyle;
		}
		GUI.Label(rect, this.TitleText, style);
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x0008CDF4 File Offset: 0x0008AFF4
	public void OnSelected()
	{
		this.m_pressedTimeCountDown = 0f;
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x0008CE01 File Offset: 0x0008B001
	public virtual void OnSelectedUpdate()
	{
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x0008CE04 File Offset: 0x0008B004
	public void OnSelectedFixedUpdate()
	{
		this.m_pressedTimeCountDown -= Time.deltaTime;
		if (Core.Input.SpiritFlame.OnPressed || Core.Input.Jump.OnPressed)
		{
			this.m_pressedTimeCountDown = 0.1f;
			this.Func();
		}
	}

	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x0600204B RID: 8267 RVA: 0x0008CE58 File Offset: 0x0008B058
	// (set) Token: 0x0600204C RID: 8268 RVA: 0x0008CE60 File Offset: 0x0008B060
	public string Text
	{
		get
		{
			return this.TitleText;
		}
		set
		{
			this.TitleText = value;
		}
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x0600204D RID: 8269 RVA: 0x0008CE69 File Offset: 0x0008B069
	// (set) Token: 0x0600204E RID: 8270 RVA: 0x0008CE71 File Offset: 0x0008B071
	public string HelpText
	{
		get
		{
			return this.m_helpText;
		}
		set
		{
			this.m_helpText = value;
		}
	}

	// Token: 0x04001B87 RID: 7047
	public string TitleText;

	// Token: 0x04001B88 RID: 7048
	public Func<bool> Func;

	// Token: 0x04001B89 RID: 7049
	private float m_pressedTimeCountDown;

	// Token: 0x04001B8A RID: 7050
	private string m_helpText;
}
