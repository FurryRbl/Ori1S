using System;

// Token: 0x020002C2 RID: 706
public class CacheBaseAnimatorAction : ActionMethod
{
	// Token: 0x06001600 RID: 5632 RVA: 0x000617E3 File Offset: 0x0005F9E3
	public override void Perform(IContext context)
	{
		if (this.Animator)
		{
			this.Animator.CacheOriginals();
		}
	}

	// Token: 0x040012F4 RID: 4852
	public BaseAnimator Animator;
}
