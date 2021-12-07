using System;

// Token: 0x02000350 RID: 848
public class SpriteAnimatorAction : ActionMethod
{
	// Token: 0x0600184B RID: 6219 RVA: 0x000685B4 File Offset: 0x000667B4
	public override void Perform(IContext context)
	{
		switch (this.Command)
		{
		case SpriteAnimatorAction.PlayMode.Restart:
			this.Animator.AnimatorDriver.Restart();
			break;
		case SpriteAnimatorAction.PlayMode.Continue:
			this.Animator.AnimatorDriver.Resume();
			break;
		case SpriteAnimatorAction.PlayMode.Pause:
			this.Animator.AnimatorDriver.Pause();
			break;
		}
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x00068620 File Offset: 0x00066820
	public override string GetNiceName()
	{
		switch (this.Command)
		{
		case SpriteAnimatorAction.PlayMode.Restart:
			return "Restart " + ActionHelper.GetName(this.Animator) + " sprite animator";
		case SpriteAnimatorAction.PlayMode.Continue:
			return "Continue " + ActionHelper.GetName(this.Animator) + " sprite animator";
		case SpriteAnimatorAction.PlayMode.Pause:
			return "Pause " + ActionHelper.GetName(this.Animator) + " sprite animator";
		default:
			return base.GetNiceName();
		}
	}

	// Token: 0x040014DE RID: 5342
	public SpriteAnimatorAction.PlayMode Command;

	// Token: 0x040014DF RID: 5343
	[NotNull]
	public SpriteAnimator Animator;

	// Token: 0x02000351 RID: 849
	public enum PlayMode
	{
		// Token: 0x040014E1 RID: 5345
		Restart,
		// Token: 0x040014E2 RID: 5346
		Continue,
		// Token: 0x040014E3 RID: 5347
		Pause
	}
}
