using System;
using Game;
using UnityEngine;

// Token: 0x0200079F RID: 1951
public class UberVignettingAnimator : BaseAnimator
{
	// Token: 0x06002D45 RID: 11589 RVA: 0x000C1B7E File Offset: 0x000BFD7E
	public void OnDisable()
	{
		this.RestoreToOriginalState();
	}

	// Token: 0x06002D46 RID: 11590 RVA: 0x000C1B86 File Offset: 0x000BFD86
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06002D47 RID: 11591 RVA: 0x000C1BA9 File Offset: 0x000BFDA9
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06002D48 RID: 11592 RVA: 0x000C1BCC File Offset: 0x000BFDCC
	public override void CacheOriginals()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002D49 RID: 11593 RVA: 0x000C1BDE File Offset: 0x000BFDDE
	public void OnGameReset()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002D4A RID: 11594 RVA: 0x000C1BF0 File Offset: 0x000BFDF0
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveVignettingIntensity = this.IntensityAnimationCurve.Evaluate(value);
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x06002D4B RID: 11595 RVA: 0x000C1C30 File Offset: 0x000BFE30
	public override void RestoreToOriginalState()
	{
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveVignettingIntensity = 0f;
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x17000740 RID: 1856
	// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000C1C5D File Offset: 0x000BFE5D
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.IntensityAnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000741 RID: 1857
	// (get) Token: 0x06002D4D RID: 11597 RVA: 0x000C1C70 File Offset: 0x000BFE70
	public override bool IsLooping
	{
		get
		{
			return this.IntensityAnimationCurve.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x040028DF RID: 10463
	public AnimationCurve IntensityAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x040028E0 RID: 10464
	private CameraPostProcessing m_cameraPostProcessing;
}
