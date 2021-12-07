using System;
using Game;
using UnityEngine;

// Token: 0x02000779 RID: 1913
public class GameplayToCinematicAnimator : BaseAnimator
{
	// Token: 0x06002C64 RID: 11364 RVA: 0x000BEE45 File Offset: 0x000BD045
	public override void CacheOriginals()
	{
		this.m_puppet = UI.Cameras.Current.Controller.PuppetController;
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x000BEE5C File Offset: 0x000BD05C
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x000BEE7F File Offset: 0x000BD07F
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x000BEEA2 File Offset: 0x000BD0A2
	public void OnGameReset()
	{
		this.m_puppet = UI.Cameras.Current.Controller.PuppetController;
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x000BEEBC File Offset: 0x000BD0BC
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (this.m_puppet)
		{
			this.m_puppet.CinematicPuppet = base.transform;
			float num = this.AnimationCurve.Evaluate(value);
			foreach (FloatProviderZone floatProviderZone in this.AnimationZones)
			{
				num *= floatProviderZone.GetValue();
			}
			this.m_puppet.SetTween(num);
			this.WideScreenAdjustment.ApplyToPuppet(this.m_puppet);
			if (this.WideScreenAdjustment.Enabled && Mathf.Approximately(num, 0f))
			{
				this.m_puppet.ClearWideScreenAdjustments();
			}
		}
	}

	// Token: 0x17000709 RID: 1801
	// (get) Token: 0x06002C69 RID: 11369 RVA: 0x000BEF70 File Offset: 0x000BD170
	public override float Duration
	{
		get
		{
			if (this.AnimationCurve.length == 1)
			{
				return 1f;
			}
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x06002C6A RID: 11370 RVA: 0x000BEFA8 File Offset: 0x000BD1A8
	public override void RestoreToOriginalState()
	{
		if (this.m_puppet)
		{
			this.m_puppet.CinematicPuppet = null;
			this.m_puppet.SetTween(0f);
		}
	}

	// Token: 0x1700070A RID: 1802
	// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000BEFE1 File Offset: 0x000BD1E1
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x0400283B RID: 10299
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400283C RID: 10300
	private CameraPuppetController m_puppet;

	// Token: 0x0400283D RID: 10301
	public WideScreenAdjustmentSettings WideScreenAdjustment;

	// Token: 0x0400283E RID: 10302
	public FloatProviderZone[] AnimationZones = new FloatProviderZone[0];
}
