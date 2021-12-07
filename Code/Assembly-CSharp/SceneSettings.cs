using System;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class SceneSettings
{
	// Token: 0x06001035 RID: 4149 RVA: 0x00049ED8 File Offset: 0x000480D8
	public SceneSettings(SceneSettingsComponent sceneSettings)
	{
		this.CameraSettings = new CameraSettings(sceneSettings.CameraSettings, sceneSettings.SceneFogSettings);
		this.DepthOfFieldController = sceneSettings.DepthOfFieldController;
		this.DefaultCameraZoom = sceneSettings.DefaultCameraZoom;
		this.DefaultMusic = sceneSettings.DefaultMusic;
		this.DefaultAmbience = sceneSettings.DefaultAmbience;
		this.TurbulenceSettings = sceneSettings.TurbulenceSettings;
		this.BlurredBackgroundDepth = sceneSettings.BlurredBackgroundDepth;
	}

	// Token: 0x04000D5E RID: 3422
	public CameraSettings CameraSettings;

	// Token: 0x04000D5F RID: 3423
	public DepthOfFieldController DepthOfFieldController;

	// Token: 0x04000D60 RID: 3424
	public Vector3 DefaultCameraZoom = new Vector3(0f, 0f, 21f);

	// Token: 0x04000D61 RID: 3425
	public SoundProvider DefaultMusic;

	// Token: 0x04000D62 RID: 3426
	public SoundProvider DefaultAmbience;

	// Token: 0x04000D63 RID: 3427
	public TurbulenceOverride TurbulenceSettings = new TurbulenceOverride();

	// Token: 0x04000D64 RID: 3428
	public float BlurredBackgroundDepth = 17.5f;
}
