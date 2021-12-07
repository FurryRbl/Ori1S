using System;
using Game;

// Token: 0x02000311 RID: 785
[Category("Sein")]
internal class PlayCharacterAnimation : PerformingAction
{
	// Token: 0x06001745 RID: 5957 RVA: 0x000649A4 File Offset: 0x00062BA4
	public override void Perform(IContext context)
	{
		if (this.Condition == null || this.Condition.Validate(null))
		{
			Characters.Sein.Controller.PlayAnimation(this.Animation);
		}
		if (this.Animation.name == "idleListen")
		{
			Characters.Ori.StartListening();
		}
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x00064A0C File Offset: 0x00062C0C
	public override void Stop()
	{
		Characters.Sein.Controller.StopAnimation();
	}

	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x06001747 RID: 5959 RVA: 0x00064A1D File Offset: 0x00062C1D
	public override bool IsPerforming
	{
		get
		{
			return Characters.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x04001402 RID: 5122
	[NotNull]
	public TextureAnimationWithTransitions Animation;

	// Token: 0x04001403 RID: 5123
	public Condition Condition;
}
