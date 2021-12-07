using System;
using Game;

// Token: 0x020002E5 RID: 741
[Category("Camera")]
public class FaderBCustomFadeAction : ActionWithDuration
{
	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x06001688 RID: 5768 RVA: 0x00062D41 File Offset: 0x00060F41
	// (set) Token: 0x06001689 RID: 5769 RVA: 0x00062D57 File Offset: 0x00060F57
	public override float Duration
	{
		get
		{
			return this.FadeInDuration + this.FadeStayDuration + this.FadeOutDuration;
		}
		set
		{
			throw new Exception("You shouldnt need this, are you using timed action sequence?");
		}
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x00062D63 File Offset: 0x00060F63
	public override void Perform(IContext context)
	{
		UI.Fader.Fade(this.FadeInDuration, this.FadeStayDuration, this.FadeOutDuration, null, null);
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x00062D83 File Offset: 0x00060F83
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x0600168C RID: 5772 RVA: 0x00062D8A File Offset: 0x00060F8A
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x0400136B RID: 4971
	public FaderB.State State;

	// Token: 0x0400136C RID: 4972
	public float FadeInDuration = 0.5f;

	// Token: 0x0400136D RID: 4973
	public float FadeStayDuration;

	// Token: 0x0400136E RID: 4974
	public float FadeOutDuration = 0.5f;
}
