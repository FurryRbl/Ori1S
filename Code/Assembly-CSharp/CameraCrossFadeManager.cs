using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class CameraCrossFadeManager : MonoBehaviour, ISuspendable
{
	// Token: 0x06000DC0 RID: 3520 RVA: 0x00040139 File Offset: 0x0003E339
	public void Awake()
	{
		SuspensionManager.Register(this);
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0004015C File Offset: 0x0003E35C
	public void Start()
	{
		if (this.UberPostProcessingCrossFade)
		{
			this.UberPostProcessingCrossFade.GenerateBuffer();
		}
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00040179 File Offset: 0x0003E379
	public void OnDestroy()
	{
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		SuspensionManager.Unregister(this);
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0004019C File Offset: 0x0003E39C
	public bool IsCrossFading
	{
		get
		{
			return this.m_isCrossFading;
		}
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x000401A4 File Offset: 0x0003E3A4
	public void OnGameReset()
	{
		CameraCrossFadeManager.CrossFadeMenuHack = MoonGuid.Empty;
		if (this.m_isCrossFading)
		{
			this.StopCrossFade();
		}
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x000401C4 File Offset: 0x0003E3C4
	public void PerformCrossFade(SceneMetaData sceneMetaData, float crossFadeDuration)
	{
		CameraController cameraControllerEnd = this.CameraControllerEnd;
		this.CameraControllerEnd = this.CameraControllerStart;
		this.CameraControllerStart = cameraControllerEnd;
		UI.Cameras.Current.Controller = this.CameraControllerEnd;
		this.CameraControllerEnd.PuppetController.Tween = 0f;
		this.CameraControllerEnd.PuppetController.CinematicPuppet = null;
		this.CameraControllerEnd.enabled = true;
		this.m_crossFadeDuration = crossFadeDuration;
		this.m_crossFadeTime = 0f;
		this.m_isCrossFading = true;
		if (!Scenes.Manager.SceneIsLoaded(sceneMetaData.SceneMoonGuid))
		{
			CameraCrossFadeManager.CrossFadeMenuHack = sceneMetaData.SceneMoonGuid;
			UI.Menu.ShowMenuScreen(false);
		}
		if (sceneMetaData)
		{
			Vector3 seinPlaceholderPosition = sceneMetaData.SeinPlaceholderPosition;
			Scenes.Manager.SetTargetPositions(seinPlaceholderPosition);
			if (Characters.Current as Component)
			{
				Characters.Current.Position = seinPlaceholderPosition;
			}
			Scenes.Manager.EnableDisabledScenesAtPosition(false);
			UI.Cameras.Current.CameraTarget.SetTargetPosition(seinPlaceholderPosition);
			UI.Cameras.Current.MoveCameraToTargetInstantly(true);
		}
		this.ApplyCrossFadeSettings();
		this.UberPostProcessingCrossFade.StartCrossFade();
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x000402E4 File Offset: 0x0003E4E4
	private void Apply(UberPostProcessingCrossFade.CameraInformation cameraInformation, CameraController cameraController)
	{
		cameraInformation.Position = cameraController.Position;
		cameraInformation.Rotation = cameraController.Rotation;
		cameraInformation.Settings = cameraController.CameraPostProcessing.CameraSettingsToUse;
		cameraInformation.AdditiveSettings = cameraController.CameraPostProcessing.AdditiveSettings;
		cameraInformation.FogTexture = cameraController.CameraPostProcessing.FogTextureToUse;
		cameraInformation.Speed = cameraController.Speed;
		cameraInformation.FieldOfView = cameraController.FieldOfView;
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00040354 File Offset: 0x0003E554
	public void ApplyCrossFadeSettings()
	{
		float time = this.m_crossFadeTime / this.m_crossFadeDuration;
		this.UberPostProcessingCrossFade.TweenTime = this.TweenCurve.Evaluate(time);
		this.Apply(this.UberPostProcessingCrossFade.FromInfo, this.CameraControllerStart);
		this.Apply(this.UberPostProcessingCrossFade.ToInfo, this.CameraControllerEnd);
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x000403B4 File Offset: 0x0003E5B4
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (!this.m_isCrossFading)
		{
			return;
		}
		this.m_crossFadeTime += Time.deltaTime;
		if (this.m_crossFadeTime > this.m_crossFadeDuration)
		{
			this.StopCrossFade();
			return;
		}
		this.ApplyCrossFadeSettings();
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x00040409 File Offset: 0x0003E609
	public void StopCrossFade()
	{
		this.m_crossFadeTime = 0f;
		this.CameraControllerStart.enabled = false;
		this.m_isCrossFading = false;
		this.UberPostProcessingCrossFade.StopCrossFade();
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00040434 File Offset: 0x0003E634
	// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0004043C File Offset: 0x0003E63C
	public bool IsSuspended { get; set; }

	// Token: 0x04000B31 RID: 2865
	private bool m_isCrossFading;

	// Token: 0x04000B32 RID: 2866
	public CameraController CameraControllerEnd;

	// Token: 0x04000B33 RID: 2867
	public CameraController CameraControllerStart;

	// Token: 0x04000B34 RID: 2868
	private float m_crossFadeTime;

	// Token: 0x04000B35 RID: 2869
	private float m_crossFadeDuration;

	// Token: 0x04000B36 RID: 2870
	public AnimationCurve TweenCurve;

	// Token: 0x04000B37 RID: 2871
	public UberPostProcessingCrossFade UberPostProcessingCrossFade;

	// Token: 0x04000B38 RID: 2872
	public static MoonGuid CrossFadeMenuHack = MoonGuid.Empty;
}
