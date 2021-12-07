using System;
using UnityEngine;

// Token: 0x0200077A RID: 1914
public class InputLockAnimator : BaseAnimator
{
	// Token: 0x06002C6D RID: 11373 RVA: 0x000BF01B File Offset: 0x000BD21B
	public override void CacheOriginals()
	{
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x000BF01D File Offset: 0x000BD21D
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.LastSampledValue = (this.AnimationCurve.Evaluate(value) > 0.5f);
		if (Application.isPlaying)
		{
			GameController.Instance.LockInputByAction = this.LastSampledValue;
		}
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x000BF05B File Offset: 0x000BD25B
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x1700070B RID: 1803
	// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000BF05D File Offset: 0x000BD25D
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x1700070C RID: 1804
	// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000BF070 File Offset: 0x000BD270
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x0400283F RID: 10303
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x04002840 RID: 10304
	public bool LastSampledValue;
}
