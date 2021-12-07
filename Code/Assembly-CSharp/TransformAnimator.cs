using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class TransformAnimator : BaseAnimator
{
	// Token: 0x060017FE RID: 6142 RVA: 0x00066F8C File Offset: 0x0006518C
	public override void CacheOriginals()
	{
		this.m_originalPosition = base.transform.localPosition;
	}

	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x060017FF RID: 6143 RVA: 0x00066F9F File Offset: 0x0006519F
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(Mathf.Max(this.X.CurveDuration(), Mathf.Max(this.Y.CurveDuration(), this.Z.CurveDuration())));
		}
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x00066FD4 File Offset: 0x000651D4
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		Vector3 b;
		b.x = this.X.Evaluate(value);
		b.y = this.Y.Evaluate(value);
		b.z = this.Z.Evaluate(value);
		base.transform.localPosition = this.m_originalPosition + b;
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x0006703A File Offset: 0x0006523A
	public override void RestoreToOriginalState()
	{
		base.transform.localPosition = this.OriginalPosition;
	}

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x06001802 RID: 6146 RVA: 0x0006704D File Offset: 0x0006524D
	public Vector3 OriginalPosition
	{
		get
		{
			return this.m_originalPosition;
		}
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x06001803 RID: 6147 RVA: 0x00067058 File Offset: 0x00065258
	public override bool IsLooping
	{
		get
		{
			return this.X.postWrapMode != WrapMode.Once || this.Y.postWrapMode != WrapMode.Once || this.Z.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x040014B7 RID: 5303
	public AnimationCurve X = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x040014B8 RID: 5304
	public AnimationCurve Y = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x040014B9 RID: 5305
	public AnimationCurve Z = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x040014BA RID: 5306
	private Vector3 m_originalPosition;
}
