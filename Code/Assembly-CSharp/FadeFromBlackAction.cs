using System;
using Game;
using UnityEngine;

// Token: 0x020002E3 RID: 739
[Category("Camera")]
public class FadeFromBlackAction : ActionWithDuration
{
	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x0600167A RID: 5754 RVA: 0x00062C23 File Offset: 0x00060E23
	// (set) Token: 0x0600167B RID: 5755 RVA: 0x00062C2B File Offset: 0x00060E2B
	public override float Duration
	{
		get
		{
			return this.DurationOfFade;
		}
		set
		{
			this.DurationOfFade = value;
		}
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x00062C34 File Offset: 0x00060E34
	public override void Perform(IContext context)
	{
		UI.Fader.FadeOut(this.Duration);
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x00062C46 File Offset: 0x00060E46
	public override void Stop()
	{
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x0600167E RID: 5758 RVA: 0x00062C48 File Offset: 0x00060E48
	public override bool IsPerforming
	{
		get
		{
			return UI.Fader.IsFadingOut();
		}
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x00062C54 File Offset: 0x00060E54
	public override string GetNiceName()
	{
		return "Fade from black over " + this.DurationOfFade + " seconds";
	}

	// Token: 0x04001364 RID: 4964
	[NotNull]
	public GameObject FaderToUse;

	// Token: 0x04001365 RID: 4965
	private Fader m_fader;

	// Token: 0x04001366 RID: 4966
	public float DurationOfFade;
}
