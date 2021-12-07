using System;

// Token: 0x02000332 RID: 818
[Category("Animator")]
public class SetAnimatorSpeedAction : ActionMethod
{
	// Token: 0x060017BF RID: 6079 RVA: 0x00066034 File Offset: 0x00064234
	public override void Perform(IContext context)
	{
		if (this.BaseAnimator)
		{
			this.BaseAnimator.AnimatorDriver.Speed = this.Speed;
		}
	}

	// Token: 0x0400146C RID: 5228
	public BaseAnimator BaseAnimator;

	// Token: 0x0400146D RID: 5229
	public float Speed;
}
