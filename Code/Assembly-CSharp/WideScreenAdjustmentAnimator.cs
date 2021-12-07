using System;
using Game;
using UnityEngine;

// Token: 0x02000783 RID: 1923
public class WideScreenAdjustmentAnimator : BaseAnimator
{
	// Token: 0x06002C9E RID: 11422 RVA: 0x000BF72C File Offset: 0x000BD92C
	public override void CacheOriginals()
	{
		this.m_puppet = UI.Cameras.Current.Controller.PuppetController;
	}

	// Token: 0x06002C9F RID: 11423 RVA: 0x000BF743 File Offset: 0x000BD943
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06002CA0 RID: 11424 RVA: 0x000BF766 File Offset: 0x000BD966
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06002CA1 RID: 11425 RVA: 0x000BF789 File Offset: 0x000BD989
	public void OnGameReset()
	{
		this.m_puppet = UI.Cameras.Current.Controller.PuppetController;
	}

	// Token: 0x06002CA2 RID: 11426 RVA: 0x000BF7A0 File Offset: 0x000BD9A0
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (this.m_puppet)
		{
			this.m_puppet.SetWideScreenHorizontalPanStrength(this.WideScreenHorizontalPanCurve.Evaluate(value));
			this.m_puppet.SetWideScreenZoomStrength(this.WideScreenZoomCurve.Evaluate(value));
			this.m_puppet.SetWideScreenVerticalPanStrength(this.WideScreenVerticalPanCurve.Evaluate(value));
		}
	}

	// Token: 0x1700071D RID: 1821
	// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000BF80C File Offset: 0x000BDA0C
	public override float Duration
	{
		get
		{
			float time = Mathf.Max(new float[]
			{
				this.WideScreenHorizontalPanCurve.CurveDuration(),
				this.WideScreenZoomCurve.CurveDuration(),
				this.WideScreenVerticalPanCurve.CurveDuration()
			});
			return base.AnimationCurveTimeToTime(time);
		}
	}

	// Token: 0x06002CA4 RID: 11428 RVA: 0x000BF858 File Offset: 0x000BDA58
	public override void RestoreToOriginalState()
	{
		if (this.m_puppet)
		{
			this.m_puppet.SetWideScreenHorizontalPanStrength(0f);
			this.m_puppet.SetWideScreenZoomStrength(0f);
			this.m_puppet.SetWideScreenVerticalPanStrength(0f);
		}
	}

	// Token: 0x1700071E RID: 1822
	// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000BF8A5 File Offset: 0x000BDAA5
	public override bool IsLooping
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0400285C RID: 10332
	public AnimationCurve WideScreenHorizontalPanCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400285D RID: 10333
	public AnimationCurve WideScreenVerticalPanCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400285E RID: 10334
	public AnimationCurve WideScreenZoomCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400285F RID: 10335
	private CameraPuppetController m_puppet;
}
