using System;
using UnityEngine;

// Token: 0x02000330 RID: 816
[Category("Animator")]
public class SetAnimatorPropertiesAction : ActionMethod
{
	// Token: 0x060017BC RID: 6076 RVA: 0x00065F84 File Offset: 0x00064184
	public new void Start()
	{
		base.Start();
		if (this.AnimatorsMode == SetAnimatorPropertiesAction.FindAnimatorsMode.GameObject)
		{
			this.Animators = this.Target.GetComponents<LegacyAnimator>();
		}
		if (this.AnimatorsMode == SetAnimatorPropertiesAction.FindAnimatorsMode.GameObjectAndChildren)
		{
			this.Animators = this.Target.GetComponentsInChildren<LegacyAnimator>();
		}
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x00065FD0 File Offset: 0x000641D0
	public override void Perform(IContext context)
	{
		for (int i = 0; i < this.Animators.Length; i++)
		{
			LegacyAnimator legacyAnimator = this.Animators[i];
			legacyAnimator.Speed = this.Speed;
			if (this.ShouldSetCurve)
			{
				legacyAnimator.AnimationCurve = this.Curve;
			}
			legacyAnimator.Stopped = false;
		}
	}

	// Token: 0x04001462 RID: 5218
	public GameObject Target;

	// Token: 0x04001463 RID: 5219
	public SetAnimatorPropertiesAction.FindAnimatorsMode AnimatorsMode;

	// Token: 0x04001464 RID: 5220
	public LegacyAnimator[] Animators;

	// Token: 0x04001465 RID: 5221
	public float Speed;

	// Token: 0x04001466 RID: 5222
	public bool ShouldSetCurve;

	// Token: 0x04001467 RID: 5223
	public AnimationCurve Curve;

	// Token: 0x02000331 RID: 817
	public enum FindAnimatorsMode
	{
		// Token: 0x04001469 RID: 5225
		GameObject,
		// Token: 0x0400146A RID: 5226
		GameObjectAndChildren,
		// Token: 0x0400146B RID: 5227
		List
	}
}
