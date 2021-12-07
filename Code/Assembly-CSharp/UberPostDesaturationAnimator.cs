using System;
using Game;
using UnityEngine;

// Token: 0x02000792 RID: 1938
public class UberPostDesaturationAnimator : BaseAnimator
{
	// Token: 0x06002CF7 RID: 11511 RVA: 0x000C080E File Offset: 0x000BEA0E
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06002CF8 RID: 11512 RVA: 0x000C0831 File Offset: 0x000BEA31
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x000C0854 File Offset: 0x000BEA54
	public void OnGameReset()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x000C0866 File Offset: 0x000BEA66
	public override void CacheOriginals()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002CFB RID: 11515 RVA: 0x000C0878 File Offset: 0x000BEA78
	public void OnDisable()
	{
		this.RestoreToOriginalState();
	}

	// Token: 0x06002CFC RID: 11516 RVA: 0x000C0880 File Offset: 0x000BEA80
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveDesaturation = this.AnimationCurve.Evaluate(value);
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x06002CFD RID: 11517 RVA: 0x000C08C0 File Offset: 0x000BEAC0
	public override void RestoreToOriginalState()
	{
		this.m_cameraPostProcessing.AdditiveSettings.AdditiveDesaturation = 0f;
		this.m_cameraPostProcessing.Apply();
	}

	// Token: 0x17000730 RID: 1840
	// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000C08ED File Offset: 0x000BEAED
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000731 RID: 1841
	// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000C0900 File Offset: 0x000BEB00
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.Once || this.AnimationCurve.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x0400289B RID: 10395
	public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400289C RID: 10396
	private CameraPostProcessing m_cameraPostProcessing;
}
