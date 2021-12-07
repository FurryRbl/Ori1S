using System;

// Token: 0x020002A3 RID: 675
public class SpriteAnimatorCondition : Condition
{
	// Token: 0x060015A1 RID: 5537 RVA: 0x0005FFA4 File Offset: 0x0005E1A4
	public override bool Validate(IContext context)
	{
		SpriteAnimatorCondition.SpriteAnimatorMode mode = this.Mode;
		return mode == SpriteAnimatorCondition.SpriteAnimatorMode.AnimationEnded && this.Animator.TextureAnimator.AnimationEnded;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x0005FFD8 File Offset: 0x0005E1D8
	public override string GetNiceName()
	{
		return "sprite animator " + ActionHelper.GetName(this.Animator) + " " + ActionHelper.GetName(this.Mode.ToString());
	}

	// Token: 0x04001299 RID: 4761
	public SpriteAnimatorCondition.SpriteAnimatorMode Mode;

	// Token: 0x0400129A RID: 4762
	public SpriteAnimator Animator;

	// Token: 0x020002A4 RID: 676
	public enum SpriteAnimatorMode
	{
		// Token: 0x0400129C RID: 4764
		AnimationEnded
	}
}
