using System;
using Game;

// Token: 0x020002E8 RID: 744
[Category("Camera")]
public class FaderBFadeOutAction : ActionWithDuration
{
	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x06001695 RID: 5781 RVA: 0x00062E04 File Offset: 0x00061004
	// (set) Token: 0x06001696 RID: 5782 RVA: 0x00062E0C File Offset: 0x0006100C
	public override float Duration
	{
		get
		{
			return this.FadeOutDuration;
		}
		set
		{
			this.FadeOutDuration = value;
		}
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x00062E15 File Offset: 0x00061015
	public override void Perform(IContext context)
	{
		UI.Fader.FadeOut(this.FadeOutDuration);
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x00062E27 File Offset: 0x00061027
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06001699 RID: 5785 RVA: 0x00062E2E File Offset: 0x0006102E
	public override bool IsPerforming
	{
		get
		{
			return UI.Fader.IsFadingOut();
		}
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x00062E3A File Offset: 0x0006103A
	public override string GetNiceName()
	{
		return "FadeFromBlack over " + this.Duration + " seconds";
	}

	// Token: 0x04001376 RID: 4982
	public float FadeOutDuration = 0.5f;
}
