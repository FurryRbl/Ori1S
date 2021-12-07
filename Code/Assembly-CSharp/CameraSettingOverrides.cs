using System;
using UnityEngine;

// Token: 0x020001A8 RID: 424
[Serializable]
public class CameraSettingOverrides
{
	// Token: 0x0600102F RID: 4143 RVA: 0x00049B14 File Offset: 0x00047D14
	public CameraSettings GetCameraSettings()
	{
		if (this.m_cameraSettings == null)
		{
			this.m_cameraSettings = new CameraSettings(this.CameraSettings, this.FogGradient, this.FogRange);
		}
		return this.m_cameraSettings;
	}

	// Token: 0x04000D43 RID: 3395
	public CameraSettingsAsset CameraSettings;

	// Token: 0x04000D44 RID: 3396
	public float FogRange;

	// Token: 0x04000D45 RID: 3397
	public Gradient FogGradient;

	// Token: 0x04000D46 RID: 3398
	public Condition Condition;

	// Token: 0x04000D47 RID: 3399
	private CameraSettings m_cameraSettings;
}
