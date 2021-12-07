using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public class LeftTriggerRightTriggerCycle : MonoBehaviour
{
	// Token: 0x06001061 RID: 4193 RVA: 0x0004B114 File Offset: 0x00049314
	private void FixedUpdate()
	{
		if (Core.Input.Glide.OnPressed)
		{
			this.CurrentPanel.Hide();
			this.CurrentPanelIndex++;
			if (this.CurrentPanelIndex >= this.Panels.Count)
			{
				this.CurrentPanelIndex = this.Panels.Count;
			}
			this.CurrentPanel.Show();
		}
		if (Core.Input.ChargeJump.OnPressed)
		{
			this.CurrentPanel.Hide();
			this.CurrentPanelIndex--;
			if (this.CurrentPanelIndex < 0)
			{
				this.CurrentPanelIndex = 0;
			}
			this.CurrentPanel.Show();
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06001062 RID: 4194 RVA: 0x0004B1C1 File Offset: 0x000493C1
	public Panel CurrentPanel
	{
		get
		{
			return this.Panels[this.CurrentPanelIndex];
		}
	}

	// Token: 0x04000DB4 RID: 3508
	public List<Panel> Panels;

	// Token: 0x04000DB5 RID: 3509
	public int CurrentPanelIndex;
}
