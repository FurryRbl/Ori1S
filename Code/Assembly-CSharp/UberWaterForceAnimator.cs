using System;
using UnityEngine;

// Token: 0x020007A0 RID: 1952
[ExecuteInEditMode]
public class UberWaterForceAnimator : BaseAnimator
{
	// Token: 0x17000742 RID: 1858
	// (get) Token: 0x06002D4F RID: 11599 RVA: 0x000C1CAA File Offset: 0x000BFEAA
	public UberWaterForceActor Actor
	{
		get
		{
			if (this.m_actor == null)
			{
				this.m_actor = base.GetComponent<UberWaterForceActor>();
			}
			return this.m_actor;
		}
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x000C1CD0 File Offset: 0x000BFED0
	private void OnEnable()
	{
		if (!Application.isPlaying && this.Actor)
		{
			this.Actor.Strength = this.AnimationCurve.Evaluate(0f);
		}
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x000C1D12 File Offset: 0x000BFF12
	public override void CacheOriginals()
	{
		if (this.Actor)
		{
			this.m_originalStrength = this.Actor.Strength;
		}
	}

	// Token: 0x06002D52 RID: 11602 RVA: 0x000C1D38 File Offset: 0x000BFF38
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (this.Actor)
		{
			this.Actor.Strength = this.AnimationCurve.Evaluate(value);
		}
	}

	// Token: 0x06002D53 RID: 11603 RVA: 0x000C1D75 File Offset: 0x000BFF75
	public override void RestoreToOriginalState()
	{
		this.Actor.Strength = this.m_originalStrength;
	}

	// Token: 0x17000743 RID: 1859
	// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000C1D88 File Offset: 0x000BFF88
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000744 RID: 1860
	// (get) Token: 0x06002D55 RID: 11605 RVA: 0x000C1D9B File Offset: 0x000BFF9B
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x040028E1 RID: 10465
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x040028E2 RID: 10466
	private float m_originalStrength;

	// Token: 0x040028E3 RID: 10467
	private UberWaterForceActor m_actor;
}
