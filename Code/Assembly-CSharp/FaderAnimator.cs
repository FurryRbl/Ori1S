using System;
using Game;
using UnityEngine;

// Token: 0x02000777 RID: 1911
public class FaderAnimator : BaseAnimator
{
	// Token: 0x06002C5A RID: 11354 RVA: 0x000BEB0A File Offset: 0x000BCD0A
	public override void CacheOriginals()
	{
	}

	// Token: 0x06002C5B RID: 11355 RVA: 0x000BEB0C File Offset: 0x000BCD0C
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (UI.Fader)
		{
			float num = this.FaderAnimationCurve.Evaluate(value);
			bool flag = UI.Fader.CurrentState == FaderB.State.Timeline || UI.Fader.CurrentState == FaderB.State.Invisible;
			if (num > 0.3f || flag)
			{
				UI.Fader.TimelineSample(num);
			}
		}
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x000BEB7B File Offset: 0x000BCD7B
	public override void RestoreToOriginalState()
	{
		this.SampleValue(0f, true);
	}

	// Token: 0x17000707 RID: 1799
	// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000BEB89 File Offset: 0x000BCD89
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.FaderAnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000708 RID: 1800
	// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000BEB9C File Offset: 0x000BCD9C
	public override bool IsLooping
	{
		get
		{
			return this.FaderAnimationCurve.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x04002839 RID: 10297
	public AnimationCurve FaderAnimationCurve = AnimationCurve.Linear(0f, 0f, 5f, 0f);
}
