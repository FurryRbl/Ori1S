using System;
using UnityEngine;

// Token: 0x0200077D RID: 1917
public class ScaleAnimator : BaseAnimator
{
	// Token: 0x06002C80 RID: 11392 RVA: 0x000BF28C File Offset: 0x000BD48C
	public override void CacheOriginals()
	{
		this.m_originalScale = base.transform.localScale;
		this.m_transform = base.transform;
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x000BF2C4 File Offset: 0x000BD4C4
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		float num = this.AnimationCurve.Evaluate(value);
		this.m_transform.localScale = this.m_originalScale * num;
		if (this.m_renderer != null)
		{
			this.m_renderer.enabled = (num > 0.01f);
		}
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x000BF322 File Offset: 0x000BD522
	public override void RestoreToOriginalState()
	{
		this.m_transform.localScale = this.m_originalScale;
	}

	// Token: 0x17000712 RID: 1810
	// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000BF335 File Offset: 0x000BD535
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000713 RID: 1811
	// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000BF348 File Offset: 0x000BD548
	public Vector3 OriginalScale
	{
		get
		{
			return this.m_originalScale;
		}
	}

	// Token: 0x17000714 RID: 1812
	// (get) Token: 0x06002C85 RID: 11397 RVA: 0x000BF350 File Offset: 0x000BD550
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x04002848 RID: 10312
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 1f, 5f, 1f);

	// Token: 0x04002849 RID: 10313
	private Vector3 m_originalScale;

	// Token: 0x0400284A RID: 10314
	private Renderer m_renderer;

	// Token: 0x0400284B RID: 10315
	private Transform m_transform;
}
