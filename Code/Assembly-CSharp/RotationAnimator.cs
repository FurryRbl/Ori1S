using System;
using UnityEngine;

// Token: 0x0200077C RID: 1916
public class RotationAnimator : BaseAnimator
{
	// Token: 0x06002C79 RID: 11385 RVA: 0x000BF1BD File Offset: 0x000BD3BD
	public override void CacheOriginals()
	{
		this.m_transform = base.transform;
		this.m_originalRotation = this.m_transform.localEulerAngles;
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x000BF1DC File Offset: 0x000BD3DC
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.m_transform.localEulerAngles = this.OriginalRotation + new Vector3(0f, 0f, this.AnimationCurve.Evaluate(value));
	}

	// Token: 0x06002C7B RID: 11387 RVA: 0x000BF223 File Offset: 0x000BD423
	public override void RestoreToOriginalState()
	{
		this.m_transform.localEulerAngles = this.OriginalRotation;
	}

	// Token: 0x1700070F RID: 1807
	// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000BF236 File Offset: 0x000BD436
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000710 RID: 1808
	// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000BF249 File Offset: 0x000BD449
	public Vector3 OriginalRotation
	{
		get
		{
			return this.m_originalRotation;
		}
	}

	// Token: 0x17000711 RID: 1809
	// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000BF251 File Offset: 0x000BD451
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x04002845 RID: 10309
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x04002846 RID: 10310
	private Vector3 m_originalRotation;

	// Token: 0x04002847 RID: 10311
	private Transform m_transform;
}
