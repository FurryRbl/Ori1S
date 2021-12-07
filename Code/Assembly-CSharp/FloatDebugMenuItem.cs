using System;
using UnityEngine;

// Token: 0x020004CD RID: 1229
internal class FloatDebugMenuItem : IDebugMenuItem
{
	// Token: 0x0600213E RID: 8510 RVA: 0x00091DAF File Offset: 0x0008FFAF
	public FloatDebugMenuItem()
	{
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x00091DC2 File Offset: 0x0008FFC2
	public FloatDebugMenuItem(string str)
	{
		this.m_text = str;
	}

	// Token: 0x170005A4 RID: 1444
	// (get) Token: 0x06002140 RID: 8512 RVA: 0x00091DDC File Offset: 0x0008FFDC
	// (set) Token: 0x06002141 RID: 8513 RVA: 0x00091DE4 File Offset: 0x0008FFE4
	public float Value
	{
		get
		{
			return this.m_value;
		}
		set
		{
			this.m_value = value;
		}
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x00091DF0 File Offset: 0x0008FFF0
	public void Draw(Rect rect, bool selected)
	{
		GUIStyle style = (!selected) ? DebugMenuB.Style : DebugMenuB.SelectedStyle;
		GUI.Label(rect, this.m_text + " " + this.m_value.ToString(), style);
	}

	// Token: 0x170005A5 RID: 1445
	// (get) Token: 0x06002143 RID: 8515 RVA: 0x00091E35 File Offset: 0x00090035
	// (set) Token: 0x06002144 RID: 8516 RVA: 0x00091E3D File Offset: 0x0009003D
	public string Text
	{
		get
		{
			return this.m_text;
		}
		set
		{
			this.m_text = value;
		}
	}

	// Token: 0x06002145 RID: 8517 RVA: 0x00091E46 File Offset: 0x00090046
	public void OnSelected()
	{
	}

	// Token: 0x06002146 RID: 8518 RVA: 0x00091E48 File Offset: 0x00090048
	public virtual void OnSelectedUpdate()
	{
	}

	// Token: 0x06002147 RID: 8519 RVA: 0x00091E4A File Offset: 0x0009004A
	public virtual void OnSelectedFixedUpdate()
	{
	}

	// Token: 0x170005A6 RID: 1446
	// (get) Token: 0x06002148 RID: 8520 RVA: 0x00091E4C File Offset: 0x0009004C
	// (set) Token: 0x06002149 RID: 8521 RVA: 0x00091E54 File Offset: 0x00090054
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

	// Token: 0x04001C21 RID: 7201
	private string m_text = string.Empty;

	// Token: 0x04001C22 RID: 7202
	private float m_value;

	// Token: 0x04001C23 RID: 7203
	private string m_helpText;
}
