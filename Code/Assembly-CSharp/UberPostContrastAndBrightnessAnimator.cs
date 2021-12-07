using System;
using Game;
using UnityEngine;

// Token: 0x02000791 RID: 1937
public class UberPostContrastAndBrightnessAnimator : BaseAnimator
{
	// Token: 0x06002CED RID: 11501 RVA: 0x000C0645 File Offset: 0x000BE845
	public override void CacheOriginals()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002CEE RID: 11502 RVA: 0x000C0657 File Offset: 0x000BE857
	public void OnDisable()
	{
		this.RestoreToOriginalState();
	}

	// Token: 0x06002CEF RID: 11503 RVA: 0x000C065F File Offset: 0x000BE85F
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06002CF0 RID: 11504 RVA: 0x000C0682 File Offset: 0x000BE882
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06002CF1 RID: 11505 RVA: 0x000C06A5 File Offset: 0x000BE8A5
	public void OnGameReset()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x000C06B8 File Offset: 0x000BE8B8
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveContrast.Contrast = this.ContrastAnimationCurve.Evaluate(value);
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveContrast.Brightness = this.BrightnessAnimationCurve.Evaluate(value);
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x06002CF3 RID: 11507 RVA: 0x000C071C File Offset: 0x000BE91C
	public override void RestoreToOriginalState()
	{
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveContrast.Contrast = 0f;
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveContrast.Brightness = 0f;
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x1700072E RID: 1838
	// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000C0768 File Offset: 0x000BE968
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(Mathf.Max(this.BrightnessAnimationCurve.CurveDuration(), this.ContrastAnimationCurve.CurveDuration()));
		}
	}

	// Token: 0x1700072F RID: 1839
	// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000C078B File Offset: 0x000BE98B
	public override bool IsLooping
	{
		get
		{
			return this.BrightnessAnimationCurve.postWrapMode != WrapMode.Once || this.ContrastAnimationCurve.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x04002898 RID: 10392
	public AnimationCurve ContrastAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04002899 RID: 10393
	public AnimationCurve BrightnessAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400289A RID: 10394
	private CameraPostProcessing m_cameraPostProcessing;
}
