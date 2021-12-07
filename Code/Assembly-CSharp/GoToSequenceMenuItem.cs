using System;
using Core;
using UnityEngine;

// Token: 0x020004BA RID: 1210
public class GoToSequenceMenuItem : IDebugMenuItem
{
	// Token: 0x060020C5 RID: 8389 RVA: 0x0008F9DC File Offset: 0x0008DBDC
	public GoToSequenceMenuItem()
	{
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x0008F9E4 File Offset: 0x0008DBE4
	public GoToSequenceMenuItem(GoToSequenceData goToSequenceData)
	{
		this.HelpText = goToSequenceData.HelpText;
		this.TitleText = goToSequenceData.SequenceName;
		this.GoToSequenceData = goToSequenceData;
		this.GoToSequenceData.SceneName = goToSequenceData.SceneName;
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x0008FA28 File Offset: 0x0008DC28
	public void Draw(Rect rect, bool selected)
	{
		GUIStyle style = (!selected) ? DebugMenuB.Style : DebugMenuB.SelectedStyle;
		if (selected && this.m_pressedTimeCountDown > 0f)
		{
			style = DebugMenuB.PressedStyle;
		}
		GUI.Label(rect, this.TitleText, style);
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x0008FA74 File Offset: 0x0008DC74
	public void OnSelected()
	{
		this.m_pressedTimeCountDown = 0f;
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x0008FA81 File Offset: 0x0008DC81
	public virtual void OnSelectedUpdate()
	{
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x0008FA84 File Offset: 0x0008DC84
	public void OnSelectedFixedUpdate()
	{
		this.m_pressedTimeCountDown -= Time.deltaTime;
		if (Core.Input.SpiritFlame.OnPressed || Core.Input.Jump.OnPressed)
		{
			this.m_pressedTimeCountDown = 0.1f;
			this.Action();
		}
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x0008FAD4 File Offset: 0x0008DCD4
	public void Action()
	{
		foreach (string s in this.GoToSequenceData.TriggerStrings)
		{
			TriggerByString.Register(s);
		}
		GameController.Instance.RequireInitialValues = true;
		GoToSceneController.Instance.GoToScene(this.GoToSequenceData.SceneName);
	}

	// Token: 0x17000593 RID: 1427
	// (get) Token: 0x060020CC RID: 8396 RVA: 0x0008FB54 File Offset: 0x0008DD54
	// (set) Token: 0x060020CD RID: 8397 RVA: 0x0008FB5C File Offset: 0x0008DD5C
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

	// Token: 0x17000594 RID: 1428
	// (get) Token: 0x060020CE RID: 8398 RVA: 0x0008FB65 File Offset: 0x0008DD65
	// (set) Token: 0x060020CF RID: 8399 RVA: 0x0008FB6D File Offset: 0x0008DD6D
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

	// Token: 0x04001BC5 RID: 7109
	public string TitleText;

	// Token: 0x04001BC6 RID: 7110
	private float m_pressedTimeCountDown;

	// Token: 0x04001BC7 RID: 7111
	private GoToSequenceData GoToSequenceData;

	// Token: 0x04001BC8 RID: 7112
	private string m_helpText;
}
