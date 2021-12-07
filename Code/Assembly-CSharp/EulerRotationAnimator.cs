using System;
using UnityEngine;

// Token: 0x02000776 RID: 1910
public class EulerRotationAnimator : BaseAnimator
{
	// Token: 0x06002C53 RID: 11347 RVA: 0x000BE9D4 File Offset: 0x000BCBD4
	public override void CacheOriginals()
	{
		this.m_originalEulerAngles = base.transform.localEulerAngles;
	}

	// Token: 0x17000704 RID: 1796
	// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000BE9E7 File Offset: 0x000BCBE7
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(Mathf.Max(this.X.CurveDuration(), Mathf.Max(this.Y.CurveDuration(), this.Z.CurveDuration())));
		}
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x000BEA1C File Offset: 0x000BCC1C
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		Vector3 b;
		b.x = this.X.Evaluate(value);
		b.y = this.Y.Evaluate(value);
		b.z = this.Z.Evaluate(value);
		base.transform.localEulerAngles = this.m_originalEulerAngles + b;
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x000BEA82 File Offset: 0x000BCC82
	public override void RestoreToOriginalState()
	{
		base.transform.localEulerAngles = this.OriginalEulerAngles;
	}

	// Token: 0x17000705 RID: 1797
	// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000BEA95 File Offset: 0x000BCC95
	public Vector3 OriginalEulerAngles
	{
		get
		{
			return this.m_originalEulerAngles;
		}
	}

	// Token: 0x17000706 RID: 1798
	// (get) Token: 0x06002C58 RID: 11352 RVA: 0x000BEAA0 File Offset: 0x000BCCA0
	public override bool IsLooping
	{
		get
		{
			return this.X.postWrapMode != WrapMode.Once || this.Y.postWrapMode != WrapMode.Once || this.Z.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x04002835 RID: 10293
	public AnimationCurve X = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x04002836 RID: 10294
	public AnimationCurve Y = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x04002837 RID: 10295
	public AnimationCurve Z = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x04002838 RID: 10296
	private Vector3 m_originalEulerAngles;
}
