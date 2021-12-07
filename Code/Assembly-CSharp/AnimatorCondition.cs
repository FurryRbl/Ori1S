using System;

// Token: 0x02000278 RID: 632
public class AnimatorCondition : Condition
{
	// Token: 0x060014FE RID: 5374 RVA: 0x0005E194 File Offset: 0x0005C394
	public override bool Validate(IContext context)
	{
		switch (this.Mode)
		{
		case AnimatorCondition.AnimatorMode.Stopped:
			return this.Animator.Stopped;
		case AnimatorCondition.AnimatorMode.Playing:
			return !this.Animator.Stopped;
		case AnimatorCondition.AnimatorMode.AtEnd:
			return this.Animator.AtEnd;
		case AnimatorCondition.AnimatorMode.AtStart:
			return this.Animator.AtStart;
		case AnimatorCondition.AnimatorMode.PlayingForward:
			return !this.Animator.Reversed && !this.Animator.Stopped;
		case AnimatorCondition.AnimatorMode.PlayingBackward:
			return this.Animator.Reversed && !this.Animator.Stopped;
		default:
			return this.Animator.Stopped;
		}
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x0005E250 File Offset: 0x0005C450
	public override string GetNiceName()
	{
		switch (this.Mode)
		{
		case AnimatorCondition.AnimatorMode.Stopped:
			return this.Animator.name + " animator is stopped";
		case AnimatorCondition.AnimatorMode.Playing:
			return this.Animator.name + " animator is playing";
		case AnimatorCondition.AnimatorMode.AtEnd:
			return this.Animator.name + " animator is at end";
		case AnimatorCondition.AnimatorMode.AtStart:
			return this.Animator.name + " animator is at start";
		case AnimatorCondition.AnimatorMode.PlayingForward:
			return this.Animator.name + " animator is playing forward";
		case AnimatorCondition.AnimatorMode.PlayingBackward:
			return this.Animator.name + " animator is playing backward";
		default:
			return "unknown";
		}
	}

	// Token: 0x0400123A RID: 4666
	public AnimatorCondition.AnimatorMode Mode;

	// Token: 0x0400123B RID: 4667
	public LegacyAnimator Animator;

	// Token: 0x02000279 RID: 633
	public enum AnimatorMode
	{
		// Token: 0x0400123D RID: 4669
		Stopped,
		// Token: 0x0400123E RID: 4670
		Playing,
		// Token: 0x0400123F RID: 4671
		AtEnd,
		// Token: 0x04001240 RID: 4672
		AtStart,
		// Token: 0x04001241 RID: 4673
		PlayingForward,
		// Token: 0x04001242 RID: 4674
		PlayingBackward
	}
}
