using System;
using Core;
using UnityEngine;

// Token: 0x020004A2 RID: 1186
internal class BoolDebugMenuItem : IDebugMenuItem
{
	// Token: 0x0600205E RID: 8286 RVA: 0x0008D018 File Offset: 0x0008B218
	public BoolDebugMenuItem(string str, Func<bool> getter, Action<bool> setter)
	{
		this.HelpText = "Press X or A to toggle value";
		this.m_text = str;
		this.m_setter = setter;
		this.m_getter = getter;
		this.Value = this.m_getter();
	}

	// Token: 0x17000585 RID: 1413
	// (get) Token: 0x0600205F RID: 8287 RVA: 0x0008D05C File Offset: 0x0008B25C
	// (set) Token: 0x06002060 RID: 8288 RVA: 0x0008D064 File Offset: 0x0008B264
	public bool Value
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

	// Token: 0x06002061 RID: 8289 RVA: 0x0008D070 File Offset: 0x0008B270
	public void Draw(Rect rect, bool selected)
	{
		if (selected)
		{
			Color color = GUI.color;
			GUIStyle style = DebugMenuB.SelectedStyle;
			GUI.color = ((!this.m_value) ? Color.red : Color.green);
			GUI.Label(rect, this.m_text, style);
			GUI.color = color;
		}
		else
		{
			GUIStyle style;
			if (this.m_value)
			{
				style = DebugMenuB.StyleEnabled;
			}
			else
			{
				style = DebugMenuB.StyleDisabled;
			}
			GUI.Label(rect, this.m_text, style);
		}
	}

	// Token: 0x17000586 RID: 1414
	// (get) Token: 0x06002062 RID: 8290 RVA: 0x0008D0EF File Offset: 0x0008B2EF
	// (set) Token: 0x06002063 RID: 8291 RVA: 0x0008D0F7 File Offset: 0x0008B2F7
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

	// Token: 0x06002064 RID: 8292 RVA: 0x0008D100 File Offset: 0x0008B300
	public void OnSelected()
	{
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x0008D102 File Offset: 0x0008B302
	public virtual void OnSelectedUpdate()
	{
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x0008D104 File Offset: 0x0008B304
	public void OnSelectedFixedUpdate()
	{
		if (Core.Input.SpiritFlame.OnPressed || Core.Input.Jump.OnPressed)
		{
			this.m_setter(!this.Value);
			this.Value = !this.Value;
		}
	}

	// Token: 0x17000587 RID: 1415
	// (get) Token: 0x06002067 RID: 8295 RVA: 0x0008D152 File Offset: 0x0008B352
	// (set) Token: 0x06002068 RID: 8296 RVA: 0x0008D15A File Offset: 0x0008B35A
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

	// Token: 0x04001B8E RID: 7054
	private string m_text;

	// Token: 0x04001B8F RID: 7055
	private Action<bool> m_setter;

	// Token: 0x04001B90 RID: 7056
	private Func<bool> m_getter;

	// Token: 0x04001B91 RID: 7057
	private bool m_value;

	// Token: 0x04001B92 RID: 7058
	private string m_helpText;
}
