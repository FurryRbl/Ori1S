using System;
using UnityEngine;

// Token: 0x0200078C RID: 1932
[ExecuteInEditMode]
public class UberInteractionExplosionAnimator : BaseAnimator
{
	// Token: 0x17000727 RID: 1831
	// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000BFFED File Offset: 0x000BE1ED
	public UberExplosionActor Actor
	{
		get
		{
			if (this.m_actor == null)
			{
				this.m_actor = base.GetComponent<UberExplosionActor>();
			}
			return this.m_actor;
		}
	}

	// Token: 0x06002CCF RID: 11471 RVA: 0x000C0012 File Offset: 0x000BE212
	public override void CacheOriginals()
	{
		if (this.Actor)
		{
			this.m_originalStrength = this.Actor.ExplodeStrength;
		}
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x000C0038 File Offset: 0x000BE238
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (this.Actor)
		{
			this.Actor.ExplodeStrength = this.AnimationCurve.Evaluate(value) * this.m_originalStrength;
			this.Actor.ExplodeThis();
		}
	}

	// Token: 0x06002CD1 RID: 11473 RVA: 0x000C008B File Offset: 0x000BE28B
	public override void RestoreToOriginalState()
	{
		this.Actor.ExplodeStrength = this.m_originalStrength;
	}

	// Token: 0x17000728 RID: 1832
	// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000C009E File Offset: 0x000BE29E
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000729 RID: 1833
	// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x000C00B1 File Offset: 0x000BE2B1
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x04002885 RID: 10373
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x04002886 RID: 10374
	private Vector4 m_originalStrength;

	// Token: 0x04002887 RID: 10375
	private UberExplosionActor m_actor;
}
