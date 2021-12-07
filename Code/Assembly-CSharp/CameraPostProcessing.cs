using System;
using Frameworks;
using Game;
using UnityEngine;

// Token: 0x020003A0 RID: 928
[Serializable]
public class CameraPostProcessing
{
	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0006FEB3 File Offset: 0x0006E0B3
	public CameraSettings CameraSettingsToUse
	{
		get
		{
			return this.m_currentCameraSettings;
		}
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x0006FEBB File Offset: 0x0006E0BB
	public void SaveCameraSettings(CameraSettingsAsset cameraSettings)
	{
		cameraSettings.UseSeinPostProcessing = true;
		if (cameraSettings.UseSeinPostProcessing)
		{
			this.UberPostProcess.SaveSettings(cameraSettings);
		}
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x0006FEDC File Offset: 0x0006E0DC
	public void ApplyCameraSettings(CameraSettings cameraSettings)
	{
		if (UI.Cameras.Current == null)
		{
			return;
		}
		if (UI.Cameras.Current.CameraPostProcessing != this)
		{
			return;
		}
		if (cameraSettings.UseSeinPostProcessing)
		{
			this.UberPostProcess.enabled = true;
			this.UberPostProcess.ApplySettings(cameraSettings);
		}
		else
		{
			this.UberPostProcess.enabled = false;
		}
		this.UberPostProcess.ApplyAdditiveSettings(this.AdditiveSettings);
		this.FogTextureToUse = this.GetFogGradientTexture();
		Frameworks.Shader.ConvertColorsToTexture(this.GetFogGradientTexture(), cameraSettings.FogGradient);
		Frameworks.Shader.Globals.FogGradientTexture = this.FogTextureToUse;
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x0006FF78 File Offset: 0x0006E178
	public void Apply()
	{
		if (this.CameraSettingsToUse != null)
		{
			this.ApplyCameraSettings(this.CameraSettingsToUse);
			this.m_applySettings = false;
		}
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x0006FF98 File Offset: 0x0006E198
	public void Advance(float timeDelta)
	{
		Vector3 cameraPositionForSampling = UI.Cameras.Current.CameraPositionForSampling;
		for (int i = 0; i < CameraSettingsZone.All.Count; i++)
		{
			CameraSettingsZone cameraSettingsZone = CameraSettingsZone.All[i];
			cameraSettingsZone.Advance(cameraPositionForSampling, timeDelta);
		}
		for (int j = 0; j < VerticalCameraSettingsZone.All.Count; j++)
		{
			VerticalCameraSettingsZone verticalCameraSettingsZone = VerticalCameraSettingsZone.All[j];
			verticalCameraSettingsZone.Advance(cameraPositionForSampling, timeDelta);
		}
		if (WorldMapUI.UseCameraSettings)
		{
			if (this.m_currentCameraSettings != WorldMapUI.CameraSettings)
			{
				this.m_applySettings = true;
				this.m_currentCameraSettings = WorldMapUI.CameraSettings;
			}
		}
		else
		{
			Vector2 v = cameraPositionForSampling;
			this.m_defaultSettingsHelper.Advance(v, timeDelta);
			bool flag = false;
			if (this.m_defaultSettingsHelper.TweenTime == 0f)
			{
				if (this.m_defaultSettingsHelper.HasFromSettings)
				{
					CameraSettings cameraSettings = this.m_defaultSettingsHelper.FromSettings.CameraSettings;
					if (cameraSettings != this.m_currentCameraSettings)
					{
						this.m_currentCameraSettings = cameraSettings;
						this.m_applySettings = true;
					}
					if (this.m_transitionSceneSettings == null)
					{
						this.m_transitionSceneSettings = new CameraSettings(this.m_currentCameraSettings);
					}
				}
			}
			else if (this.m_defaultSettingsHelper.TweenTime == 1f)
			{
				if (this.m_defaultSettingsHelper.HasToSettings)
				{
					CameraSettings cameraSettings2 = this.m_defaultSettingsHelper.ToSettings.CameraSettings;
					if (cameraSettings2 != this.m_currentCameraSettings)
					{
						this.m_currentCameraSettings = cameraSettings2;
						this.m_applySettings = true;
					}
					if (this.m_transitionSceneSettings == null)
					{
						this.m_transitionSceneSettings = new CameraSettings(this.m_currentCameraSettings);
					}
				}
			}
			else if (this.m_defaultSettingsHelper.HasToSettings && this.m_defaultSettingsHelper.HasFromSettings)
			{
				CameraSettings cameraSettings3 = this.m_defaultSettingsHelper.FromSettings.CameraSettings;
				CameraSettings cameraSettings4 = this.m_defaultSettingsHelper.ToSettings.CameraSettings;
				if (this.m_transitionSceneSettings == null)
				{
					this.m_transitionSceneSettings = new CameraSettings(cameraSettings4);
				}
				float tweenTime = this.m_defaultSettingsHelper.TweenTime;
				if (cameraSettings3 != cameraSettings4 || cameraSettings3 != this.m_currentCameraSettings)
				{
					this.m_currentCameraSettings = this.m_transitionSceneSettings;
					UberPostProcessingAnimation.AnimateCameraSettings(ref this.m_transitionSceneSettings, cameraSettings3, cameraSettings4, tweenTime);
					this.m_applySettings = true;
					flag = true;
				}
			}
			for (int k = 0; k < VerticalCameraSettingsZone.All.Count; k++)
			{
				VerticalCameraSettingsZone verticalCameraSettingsZone2 = VerticalCameraSettingsZone.All[k];
				if (verticalCameraSettingsZone2.Strength > 0f)
				{
					if (!flag)
					{
						UberPostProcessingAnimation.CopyCameraSettings(ref this.m_transitionSceneSettings, this.m_currentCameraSettings);
						flag = true;
					}
					UberPostProcessingAnimation.AnimateCameraSettings(ref this.m_transitionSceneSettings, this.m_transitionSceneSettings, verticalCameraSettingsZone2.CurrentSettings, verticalCameraSettingsZone2.Strength);
					this.m_currentCameraSettings = this.m_transitionSceneSettings;
					this.m_applySettings = true;
				}
			}
			for (int l = 0; l < CameraSettingsZone.All.Count; l++)
			{
				CameraSettingsZone cameraSettingsZone2 = CameraSettingsZone.All[l];
				if (cameraSettingsZone2.Strength > 0f)
				{
					if (!flag)
					{
						UberPostProcessingAnimation.CopyCameraSettings(ref this.m_transitionSceneSettings, this.m_currentCameraSettings);
						flag = true;
					}
					CameraSettings settings = cameraSettingsZone2.GetSettings();
					if (settings != null)
					{
						UberPostProcessingAnimation.AnimateCameraSettings(ref this.m_transitionSceneSettings, this.m_transitionSceneSettings, settings, cameraSettingsZone2.Strength);
						this.m_currentCameraSettings = this.m_transitionSceneSettings;
					}
					this.m_applySettings = true;
				}
			}
		}
		if (this.m_applySettings)
		{
			this.Apply();
			this.m_applySettings = false;
		}
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x00070330 File Offset: 0x0006E530
	public void SetCameraSettings(CameraSettings cameraSettings)
	{
		this.m_currentCameraSettings = cameraSettings;
		this.m_applySettings = true;
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x00070340 File Offset: 0x0006E540
	public void ForceFogIntoCurrentCameraSettings(FogGradientController fogGradientController)
	{
		this.m_currentCameraSettings.FogGradient = CameraSettings.ConvertGradient(fogGradientController.FogGradient);
		this.m_currentCameraSettings.FogRange = fogGradientController.FogRange;
		this.m_applySettings = true;
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x0007037C File Offset: 0x0006E57C
	private Texture2D GetFogGradientTexture()
	{
		if (this.m_fogGradientTexture2D == null)
		{
			this.m_fogGradientTexture2D = new Texture2D(128, 1);
		}
		return this.m_fogGradientTexture2D;
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000703B1 File Offset: 0x0006E5B1
	public void ResetFog()
	{
		this.m_applySettings = true;
	}

	// Token: 0x0400166F RID: 5743
	public CameraController CameraController;

	// Token: 0x04001670 RID: 5744
	public UberPostProcess UberPostProcess;

	// Token: 0x04001671 RID: 5745
	public Texture2D FogTextureToUse;

	// Token: 0x04001672 RID: 5746
	private readonly SceneDefaultSettingsHelper m_defaultSettingsHelper = new SceneDefaultSettingsHelper(2f);

	// Token: 0x04001673 RID: 5747
	private CameraSettings m_currentCameraSettings;

	// Token: 0x04001674 RID: 5748
	private CameraSettings m_transitionSceneSettings;

	// Token: 0x04001675 RID: 5749
	private bool m_applySettings;

	// Token: 0x04001676 RID: 5750
	public CameraAdditiveSettings AdditiveSettings = new CameraAdditiveSettings();

	// Token: 0x04001677 RID: 5751
	[SerializeField]
	private Texture2D m_fogGradientTexture2D;
}
