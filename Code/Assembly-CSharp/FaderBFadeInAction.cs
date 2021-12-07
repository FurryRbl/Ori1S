using System;
using Game;

// Token: 0x020002E7 RID: 743
[Category("Camera")]
public class FaderBFadeInAction : ActionWithDuration
{
	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x0600168E RID: 5774 RVA: 0x00062DA4 File Offset: 0x00060FA4
	// (set) Token: 0x0600168F RID: 5775 RVA: 0x00062DAC File Offset: 0x00060FAC
	public override float Duration
	{
		get
		{
			return this.FadeInDuration;
		}
		set
		{
			this.FadeInDuration = value;
		}
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x00062DB5 File Offset: 0x00060FB5
	public override void Perform(IContext context)
	{
		UI.Fader.FadeIn(this.FadeInDuration);
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x00062DC7 File Offset: 0x00060FC7
	public override void Stop()
	{
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x06001692 RID: 5778 RVA: 0x00062DC9 File Offset: 0x00060FC9
	public override bool IsPerforming
	{
		get
		{
			return UI.Fader.IsFadingIn();
		}
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x00062DD5 File Offset: 0x00060FD5
	public override string GetNiceName()
	{
		return "FadeToBlack over " + this.Duration + " seconds";
	}

	// Token: 0x04001375 RID: 4981
	public float FadeInDuration = 0.5f;
}
