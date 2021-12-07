using System;
using UnityEngine;

// Token: 0x0200077E RID: 1918
public class ScaleAxisAnimator : BaseAnimator
{
	// Token: 0x06002C87 RID: 11399 RVA: 0x000BF3C3 File Offset: 0x000BD5C3
	public override void CacheOriginals()
	{
		this.m_transform = base.transform;
		this.m_originalScale = this.m_transform.localScale;
	}

	// Token: 0x06002C88 RID: 11400 RVA: 0x000BF3E4 File Offset: 0x000BD5E4
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		Vector3 localScale = this.m_transform.localScale;
		if (this.UseX)
		{
			localScale.x = this.m_originalScale.x * this.X.Evaluate(value);
		}
		if (this.UseY)
		{
			localScale.y = this.m_originalScale.y * this.Y.Evaluate(value);
		}
		this.m_transform.localScale = localScale;
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x000BF466 File Offset: 0x000BD666
	public override void RestoreToOriginalState()
	{
		this.m_transform.localScale = this.m_originalScale;
	}

	// Token: 0x17000715 RID: 1813
	// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000BF479 File Offset: 0x000BD679
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(Mathf.Max(this.X.CurveDuration(), this.Y.CurveDuration()));
		}
	}

	// Token: 0x17000716 RID: 1814
	// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000BF49C File Offset: 0x000BD69C
	public Vector3 OriginalScale
	{
		get
		{
			return this.m_originalScale;
		}
	}

	// Token: 0x17000717 RID: 1815
	// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000BF4A4 File Offset: 0x000BD6A4
	public override bool IsLooping
	{
		get
		{
			return this.X.postWrapMode != WrapMode.Once || this.Y.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x0400284C RID: 10316
	public AnimationCurve X = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x0400284D RID: 10317
	public AnimationCurve Y = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x0400284E RID: 10318
	public bool UseX = true;

	// Token: 0x0400284F RID: 10319
	public bool UseY = true;

	// Token: 0x04002850 RID: 10320
	private Vector3 m_originalScale;

	// Token: 0x04002851 RID: 10321
	private Transform m_transform;
}
