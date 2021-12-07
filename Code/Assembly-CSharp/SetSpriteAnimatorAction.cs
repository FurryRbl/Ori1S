using System;

// Token: 0x0200038C RID: 908
public class SetSpriteAnimatorAction : ActionMethod
{
	// Token: 0x060019C5 RID: 6597 RVA: 0x0006E580 File Offset: 0x0006C780
	public override void Perform(IContext context)
	{
		if (!this.PerformIfSameAnimationIsPlaying && this.SpriteAnimator.TextureAnimator.Animation.name == this.AnimationToSet.name)
		{
			return;
		}
		this.SpriteAnimator.AnimationEndAction = this.OnAnimationEndedAction;
		this.SpriteAnimator.SetAnimation(this.AnimationToSet, true);
		this.SpriteAnimator.AnimatorDriver.Resume();
	}

	// Token: 0x04001619 RID: 5657
	[NotNull]
	public SpriteAnimator SpriteAnimator;

	// Token: 0x0400161A RID: 5658
	public TextureAnimation AnimationToSet;

	// Token: 0x0400161B RID: 5659
	public ActionMethod OnAnimationEndedAction;

	// Token: 0x0400161C RID: 5660
	public bool PerformIfSameAnimationIsPlaying = true;
}
