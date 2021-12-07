using System;
using UnityEngine;

// Token: 0x02000655 RID: 1621
public class SpiritLightAffectorIntensityAnimator : BaseAnimator
{
	// Token: 0x17000648 RID: 1608
	// (get) Token: 0x06002796 RID: 10134 RVA: 0x000AC5A1 File Offset: 0x000AA7A1
	public override bool IsLooping
	{
		get
		{
			return this.IntensityAnimationCurve.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x000AC5B4 File Offset: 0x000AA7B4
	public override void CacheOriginals()
	{
		this.OriginalLightIntensity = this.Target.LightIntensity;
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x000AC5C7 File Offset: 0x000AA7C7
	public override void SampleValue(float time, bool forceSample)
	{
		time = base.TimeToAnimationCurveTime(time);
		this.Target.LightIntensity = this.OriginalLightIntensity * this.IntensityCurveMagnitude * this.IntensityAnimationCurve.Evaluate(time);
	}

	// Token: 0x17000649 RID: 1609
	// (get) Token: 0x06002799 RID: 10137 RVA: 0x000AC5F7 File Offset: 0x000AA7F7
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.IntensityAnimationCurve.CurveDuration());
		}
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x000AC60A File Offset: 0x000AA80A
	public override void RestoreToOriginalState()
	{
		this.Target.LightIntensity = this.OriginalLightIntensity;
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x000AC61D File Offset: 0x000AA81D
	private void Reset()
	{
		if (this.Target == null)
		{
			this.Target = base.GetComponent<SpiritLightRadialVisualAffector>();
		}
	}

	// Token: 0x04002239 RID: 8761
	public AnimationCurve IntensityAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400223A RID: 8762
	public float IntensityCurveMagnitude = 1f;

	// Token: 0x0400223B RID: 8763
	public SpiritLightRadialVisualAffector Target;

	// Token: 0x0400223C RID: 8764
	[HideInInspector]
	public float OriginalLightIntensity;
}
