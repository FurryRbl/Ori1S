using System;
using Core;
using UnityEngine;

// Token: 0x020004B8 RID: 1208
internal class DebugMenuTogglerItem : IDebugMenuItem
{
	// Token: 0x060020B9 RID: 8377 RVA: 0x0008F737 File Offset: 0x0008D937
	public DebugMenuTogglerItem(IDebugMenuToggleable toggleable)
	{
		this.m_toggleable = toggleable;
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x0008F748 File Offset: 0x0008D948
	public void Draw(Rect rect, bool selected)
	{
		GUIStyle style = (!selected) ? DebugMenuB.Style : DebugMenuB.SelectedStyle;
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		string str = "Toggle " + this.m_toggleable.Name;
		int currentToggleOptionId = this.m_toggleable.CurrentToggleOptionId;
		string[] toggleOptions = this.m_toggleable.ToggleOptions;
		if (currentToggleOptionId != -1 && currentToggleOptionId < toggleOptions.Length)
		{
			str = toggleOptions[currentToggleOptionId];
		}
		GUI.Label(rect, " < " + str + " > ", style);
		GUILayout.EndVertical();
	}

	// Token: 0x17000591 RID: 1425
	// (get) Token: 0x060020BB RID: 8379 RVA: 0x0008F7D4 File Offset: 0x0008D9D4
	// (set) Token: 0x060020BC RID: 8380 RVA: 0x0008F7E1 File Offset: 0x0008D9E1
	public string Text
	{
		get
		{
			return this.m_toggleable.Name;
		}
		set
		{
		}
	}

	// Token: 0x17000592 RID: 1426
	// (get) Token: 0x060020BD RID: 8381 RVA: 0x0008F7E3 File Offset: 0x0008D9E3
	// (set) Token: 0x060020BE RID: 8382 RVA: 0x0008F7F0 File Offset: 0x0008D9F0
	public string HelpText
	{
		get
		{
			return this.m_toggleable.HelpText;
		}
		set
		{
		}
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x0008F7F2 File Offset: 0x0008D9F2
	public void OnSelected()
	{
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x0008F7F4 File Offset: 0x0008D9F4
	public virtual void OnSelectedUpdate()
	{
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x0008F7F8 File Offset: 0x0008D9F8
	public virtual void OnSelectedFixedUpdate()
	{
		if (Core.Input.LeftShoulder.OnPressed || Core.Input.SoulFlame.OnPressed)
		{
			this.m_toggleable.CurrentToggleOptionId--;
			DebugMenuB.ShouldShowOnlySelectedItem = true;
		}
		if (Core.Input.RightShoulder.OnPressed || Core.Input.SpiritFlame.OnPressed || Core.Input.Jump.OnPressed)
		{
			this.m_toggleable.CurrentToggleOptionId++;
			DebugMenuB.ShouldShowOnlySelectedItem = true;
		}
	}

	// Token: 0x04001BC3 RID: 7107
	private IDebugMenuToggleable m_toggleable;
}
