using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001AE RID: 430
[ExecuteInEditMode]
public class SceneSettingsComponent : MonoBehaviour
{
	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06001039 RID: 4153 RVA: 0x00049FEE File Offset: 0x000481EE
	public SceneSettings GetSettings
	{
		get
		{
			if (this.m_sceneSettings == null)
			{
				this.m_sceneSettings = new SceneSettings(this);
			}
			return this.m_sceneSettings;
		}
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x0004A010 File Offset: 0x00048210
	public void UpdateSceneSettings()
	{
		if (this.CameraSettingOverrides.Count == 0)
		{
			return;
		}
		SceneSettings getSettings = this.GetSettings;
		foreach (CameraSettingOverrides cameraSettingOverrides in this.CameraSettingOverrides)
		{
			if (!(cameraSettingOverrides.Condition == null))
			{
				if (cameraSettingOverrides.Condition.Validate(null))
				{
					getSettings.CameraSettings = cameraSettingOverrides.GetCameraSettings();
				}
			}
		}
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0004A0B0 File Offset: 0x000482B0
	public void ResetSettings()
	{
		this.m_sceneSettings = null;
		this.m_cameraSetting = null;
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0004A0C0 File Offset: 0x000482C0
	public CameraSettings GetCameraSettings()
	{
		if (this.m_cameraSetting == null)
		{
			this.m_cameraSetting = new CameraSettings(this.CameraSettings, this.SceneFogSettings);
		}
		return this.m_cameraSetting;
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x0600103D RID: 4157 RVA: 0x0004A0F5 File Offset: 0x000482F5
	// (set) Token: 0x0600103E RID: 4158 RVA: 0x0004A0FD File Offset: 0x000482FD
	public FogGradientController SceneFogSettings
	{
		get
		{
			return this.m_sceneFogSettings;
		}
		set
		{
			this.m_sceneFogSettings = value;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x0600103F RID: 4159 RVA: 0x0004A106 File Offset: 0x00048306
	public bool HasFogSettings
	{
		get
		{
			return this.m_sceneFogSettings != null;
		}
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0004A114 File Offset: 0x00048314
	public void FixedUpdate()
	{
		if (Application.isPlaying)
		{
			this.UpdateSceneSettings();
		}
	}

	// Token: 0x04000D6A RID: 3434
	public List<CameraSettingOverrides> CameraSettingOverrides = new List<CameraSettingOverrides>();

	// Token: 0x04000D6B RID: 3435
	public CameraSettingsAsset CameraSettings;

	// Token: 0x04000D6C RID: 3436
	public DepthOfFieldController DepthOfFieldController;

	// Token: 0x04000D6D RID: 3437
	public Vector3 DefaultCameraZoom = new Vector3(0f, 0f, 21f);

	// Token: 0x04000D6E RID: 3438
	public SoundProvider DefaultMusic;

	// Token: 0x04000D6F RID: 3439
	public SoundProvider DefaultAmbience;

	// Token: 0x04000D70 RID: 3440
	public TurbulenceOverride TurbulenceSettings = new TurbulenceOverride();

	// Token: 0x04000D71 RID: 3441
	public float BlurredBackgroundDepth = 17.5f;

	// Token: 0x04000D72 RID: 3442
	[SerializeField]
	private FogGradientController m_sceneFogSettings;

	// Token: 0x04000D73 RID: 3443
	private CameraSettings m_cameraSetting;

	// Token: 0x04000D74 RID: 3444
	private TurbulenceSettings m_turbulenceSettngs;

	// Token: 0x04000D75 RID: 3445
	private SceneSettings m_sceneSettings;

	// Token: 0x04000D76 RID: 3446
	public MixerSnapshot DefaultMixerSnapshot;
}
