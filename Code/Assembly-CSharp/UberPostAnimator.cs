using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200078E RID: 1934
public class UberPostAnimator : BaseAnimator
{
	// Token: 0x06002CD9 RID: 11481 RVA: 0x000C0173 File Offset: 0x000BE373
	public new void Awake()
	{
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06002CDA RID: 11482 RVA: 0x000C0196 File Offset: 0x000BE396
	public new void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06002CDB RID: 11483 RVA: 0x000C01B9 File Offset: 0x000BE3B9
	public void OnGameReset()
	{
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x000C01CC File Offset: 0x000BE3CC
	public override void CacheOriginals()
	{
		this.m_sceneSettings = SceneRoot.FindFromTransform(base.transform).SceneSettings.GetSettings;
		this.m_cameraSettings.Clear();
		this.m_cameraPostProcessing = UI.Cameras.Current.CameraPostProcessing;
		int num = Mathf.Max(this.FogSettings.Count, this.CameraPostSettings.Count);
		for (int i = 0; i < num; i++)
		{
			CameraSettingsAsset cameraSettings = this.CameraPostSettings[i];
			FogSettings fogSettings = this.FogSettings[i];
			CameraSettings item = new CameraSettings(cameraSettings, fogSettings.Gradient, fogSettings.Range);
			this.m_cameraSettings.Add(item);
		}
		this.m_transitionSceneSettings = new CameraSettings(this.m_cameraSettings[0]);
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x000C0290 File Offset: 0x000BE490
	public override void SampleValue(float value, bool forceSample)
	{
		if (this.CameraPostSettings.Count < 1)
		{
			return;
		}
		float num = this.AnimationCurve.Evaluate(base.TimeToAnimationCurveTime(value));
		float num2 = 1f / (float)(this.CameraPostSettings.Count - 1);
		float num3 = num2;
		int num4 = 0;
		while (num > num3)
		{
			num4++;
			num3 += num2;
		}
		num = (num - (num3 - num2)) / num2;
		CameraSettings fromSettings = this.m_cameraSettings[Mathf.Clamp(num4, 0, this.m_cameraSettings.Count - 1)];
		CameraSettings toSettings = this.m_cameraSettings[Mathf.Clamp(num4 + 1, 0, this.m_cameraSettings.Count - 1)];
		UberPostProcessingAnimation.AnimateCameraSettings(ref this.m_transitionSceneSettings, fromSettings, toSettings, num);
		this.m_sceneSettings.CameraSettings = this.m_transitionSceneSettings;
		this.m_cameraPostProcessing.SetCameraSettings(this.m_transitionSceneSettings);
	}

	// Token: 0x06002CDE RID: 11486 RVA: 0x000C036D File Offset: 0x000BE56D
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x1700072A RID: 1834
	// (get) Token: 0x06002CDF RID: 11487 RVA: 0x000C036F File Offset: 0x000BE56F
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x1700072B RID: 1835
	// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000C0382 File Offset: 0x000BE582
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x0400288C RID: 10380
	public AnimationCurve AnimationCurve;

	// Token: 0x0400288D RID: 10381
	public List<CameraSettingsAsset> CameraPostSettings = new List<CameraSettingsAsset>();

	// Token: 0x0400288E RID: 10382
	public List<FogSettings> FogSettings = new List<FogSettings>();

	// Token: 0x0400288F RID: 10383
	private CameraSettings m_transitionSceneSettings;

	// Token: 0x04002890 RID: 10384
	private CameraPostProcessing m_cameraPostProcessing;

	// Token: 0x04002891 RID: 10385
	private readonly List<CameraSettings> m_cameraSettings = new List<CameraSettings>();

	// Token: 0x04002892 RID: 10386
	private SceneSettings m_sceneSettings;
}
