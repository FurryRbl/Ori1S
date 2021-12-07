using System;
using Game;
using UnityEngine;

// Token: 0x02000775 RID: 1909
public class CameraMultiplyMotionBlurAnimator : BaseAnimator
{
	// Token: 0x17000702 RID: 1794
	// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000BE8A0 File Offset: 0x000BCAA0
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x06002C4D RID: 11341 RVA: 0x000BE8B3 File Offset: 0x000BCAB3
	public override void CacheOriginals()
	{
	}

	// Token: 0x06002C4E RID: 11342 RVA: 0x000BE8B8 File Offset: 0x000BCAB8
	public override void SampleValue(float value, bool forceSample)
	{
		value = this.AnimationCurve.Evaluate(base.TimeToAnimationCurveTime(value));
		UI.Cameras.Current.CameraPostProcessing.UberPostProcess.MotionBlurMultiplier = this.AnimationCurve.Evaluate(value);
	}

	// Token: 0x17000703 RID: 1795
	// (get) Token: 0x06002C4F RID: 11343 RVA: 0x000BE8F9 File Offset: 0x000BCAF9
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x06002C50 RID: 11344 RVA: 0x000BE90C File Offset: 0x000BCB0C
	public override void RestoreToOriginalState()
	{
		if (UI.Cameras.Current && UI.Cameras.Current.CameraPostProcessing.UberPostProcess != null)
		{
			UI.Cameras.Current.CameraPostProcessing.UberPostProcess.MotionBlurMultiplier = 1f;
		}
	}

	// Token: 0x06002C51 RID: 11345 RVA: 0x000BE95B File Offset: 0x000BCB5B
	public void OnDisable()
	{
		this.RestoreToOriginalState();
	}

	// Token: 0x04002834 RID: 10292
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
}
