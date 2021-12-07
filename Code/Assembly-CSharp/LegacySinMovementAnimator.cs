using System;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class LegacySinMovementAnimator : LegacyAnimator
{
	// Token: 0x06001A17 RID: 6679 RVA: 0x00070510 File Offset: 0x0006E710
	protected override void AnimateIt(float value)
	{
		if (this.m_target == null)
		{
			foreach (SinMovement.Affect affect in this.Target.Affectors)
			{
				if (affect.Type == this.AffectToTarget)
				{
					this.m_target = affect;
					break;
				}
			}
		}
		if (this.m_target == null)
		{
			return;
		}
		this.m_target.Range = this.RangeAnimationCurve.Evaluate(value);
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x000705B4 File Offset: 0x0006E7B4
	public override void RestoreToOriginalState()
	{
		base.Sample(0f);
	}

	// Token: 0x04001686 RID: 5766
	public SinMovement Target;

	// Token: 0x04001687 RID: 5767
	public SinMovement.Affect.AffectType AffectToTarget = SinMovement.Affect.AffectType.Angle;

	// Token: 0x04001688 RID: 5768
	public AnimationCurve RangeAnimationCurve;

	// Token: 0x04001689 RID: 5769
	private SinMovement.Affect m_target;
}
