using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class CameraSettingsAsset : ScriptableObject
{
	// Token: 0x04000D48 RID: 3400
	public bool UseBloomAndLensFlares;

	// Token: 0x04000D49 RID: 3401
	public bool UseSeinPostProcessing = true;

	// Token: 0x04000D4A RID: 3402
	public VignettingSettings Vignetting = new VignettingSettings();

	// Token: 0x04000D4B RID: 3403
	public NoiseSettings Noise = new NoiseSettings();

	// Token: 0x04000D4C RID: 3404
	public ContrastSettings Contrast = new ContrastSettings();

	// Token: 0x04000D4D RID: 3405
	public DesaturationSettings Desaturation = new DesaturationSettings();

	// Token: 0x04000D4E RID: 3406
	public ColorCorrectionSettings ColorCorrection = new ColorCorrectionSettings();

	// Token: 0x04000D4F RID: 3407
	public BloomAndFlaresSettings BloomAndFlaresSettings = new BloomAndFlaresSettings();

	// Token: 0x04000D50 RID: 3408
	public TwirlSettings TwirlSettings = new TwirlSettings();

	// Token: 0x04000D51 RID: 3409
	public Color Fog;
}
