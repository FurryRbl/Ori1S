using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Game;

// Token: 0x020000CA RID: 202
public class CutsceneButtonPressedTransition : CutsceneTransition
{
	// Token: 0x060008A6 RID: 2214 RVA: 0x00025468 File Offset: 0x00023668
	public override bool ShouldTransition()
	{
		if (!base.enabled || Characters.Sein.Controller.InputLocked)
		{
			return false;
		}
		if (this.ButtonsPressed.Any((Input.Button b) => Input.GetButton(b).Pressed))
		{
			return true;
		}
		if (this.ButtonsReleased.Any((Input.Button b) => Input.GetButton(b).Released))
		{
			return true;
		}
		if (this.ButtonsOnPressed.Any((Input.Button b) => Input.GetButton(b).OnPressed))
		{
			return true;
		}
		return this.ButtonsOnRelease.Any((Input.Button b) => Input.GetButton(b).OnReleased);
	}

	// Token: 0x040006E8 RID: 1768
	public List<Input.Button> ButtonsOnPressed;

	// Token: 0x040006E9 RID: 1769
	public List<Input.Button> ButtonsOnRelease;

	// Token: 0x040006EA RID: 1770
	public List<Input.Button> ButtonsPressed;

	// Token: 0x040006EB RID: 1771
	public List<Input.Button> ButtonsReleased;
}
