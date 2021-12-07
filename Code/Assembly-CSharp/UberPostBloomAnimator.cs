using System;
using Game;
using UnityEngine;

// Token: 0x02000790 RID: 1936
public class UberPostBloomAnimator : BaseAnimator
{
	// Token: 0x06002CE3 RID: 11491 RVA: 0x000C0455 File Offset: 0x000BE655
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06002CE4 RID: 11492 RVA: 0x000C0478 File Offset: 0x000BE678
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06002CE5 RID: 11493 RVA: 0x000C049B File Offset: 0x000BE69B
	public void OnDisable()
	{
		this.RestoreToOriginalState();
	}

	// Token: 0x06002CE6 RID: 11494 RVA: 0x000C04A3 File Offset: 0x000BE6A3
	public override void CacheOriginals()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002CE7 RID: 11495 RVA: 0x000C04B5 File Offset: 0x000BE6B5
	public void OnGameReset()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002CE8 RID: 11496 RVA: 0x000C04C8 File Offset: 0x000BE6C8
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveBloomIntensity = this.IntensityAnimationCurve.Evaluate(value);
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveBloomThreshhold = this.ThreshholdAnimationCurve.Evaluate(value);
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x06002CE9 RID: 11497 RVA: 0x000C0521 File Offset: 0x000BE721
	public override void RestoreToOriginalState()
	{
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveBloomIntensity = 0f;
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveBloomThreshhold = 0f;
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x1700072C RID: 1836
	// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000C0558 File Offset: 0x000BE758
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(Mathf.Max(this.IntensityAnimationCurve.CurveDuration(), this.ThreshholdAnimationCurve.CurveDuration()));
		}
	}

	// Token: 0x1700072D RID: 1837
	// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000C057B File Offset: 0x000BE77B
	public override bool IsLooping
	{
		get
		{
			return this.IntensityAnimationCurve.postWrapMode != WrapMode.Once || this.ThreshholdAnimationCurve.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x04002895 RID: 10389
	public AnimationCurve IntensityAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04002896 RID: 10390
	public AnimationCurve ThreshholdAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04002897 RID: 10391
	private CameraPostProcessing m_cameraPostProcessing;
}
