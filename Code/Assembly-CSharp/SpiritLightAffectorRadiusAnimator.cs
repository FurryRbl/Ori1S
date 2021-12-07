using System;
using UnityEngine;

// Token: 0x02000656 RID: 1622
public class SpiritLightAffectorRadiusAnimator : BaseAnimator
{
	// Token: 0x1700064A RID: 1610
	// (get) Token: 0x0600279D RID: 10141 RVA: 0x000AC6A1 File Offset: 0x000AA8A1
	public override bool IsLooping
	{
		get
		{
			return this.RadiusAnimationCurve.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x000AC6B4 File Offset: 0x000AA8B4
	public override void CacheOriginals()
	{
		this.OriginalLightRadius = this.Target.Radius;
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x000AC6C7 File Offset: 0x000AA8C7
	public override void SampleValue(float time, bool forceSample)
	{
		time = base.TimeToAnimationCurveTime(time);
		this.Target.Radius = this.OriginalLightRadius * this.RadiusCurveMagnitude * this.RadiusAnimationCurve.Evaluate(time);
	}

	// Token: 0x1700064B RID: 1611
	// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000AC6F7 File Offset: 0x000AA8F7
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.RadiusAnimationCurve.CurveDuration());
		}
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x000AC70A File Offset: 0x000AA90A
	public override void RestoreToOriginalState()
	{
		this.Target.Radius = this.OriginalLightRadius;
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x000AC71D File Offset: 0x000AA91D
	private void Reset()
	{
		if (this.Target == null)
		{
			this.Target = base.GetComponent<SpiritLightRadialVisualAffector>();
		}
	}

	// Token: 0x0400223D RID: 8765
	public AnimationCurve RadiusAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400223E RID: 8766
	public float RadiusCurveMagnitude = 1f;

	// Token: 0x0400223F RID: 8767
	public SpiritLightRadialVisualAffector Target;

	// Token: 0x04002240 RID: 8768
	[HideInInspector]
	public float OriginalLightRadius;
}
