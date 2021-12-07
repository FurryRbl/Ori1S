using System;
using UnityEngine;

// Token: 0x020001AA RID: 426
public class CameraSettings
{
	// Token: 0x06001031 RID: 4145 RVA: 0x00049BB8 File Offset: 0x00047DB8
	public CameraSettings(CameraSettings settings)
	{
		this.UseBloomAndLensFlares = false;
		this.UseSeinPostProcessing = true;
		this.Vignetting = settings.Vignetting.Clone();
		this.Noise = settings.Noise.Clone();
		this.Contrast = settings.Contrast.Clone();
		this.Desaturation = settings.Desaturation.Clone();
		this.ColorCorrection = settings.ColorCorrection.Clone();
		this.BloomAndFlaresSettings = settings.BloomAndFlaresSettings.Clone();
		this.TwirlSettings = settings.TwirlSettings.Clone();
		this.Fog = settings.Fog;
		this.FogGradient = new Color[settings.FogGradient.Length];
		Array.Copy(settings.FogGradient, this.FogGradient, this.FogGradient.Length);
		this.FogRange = settings.FogRange;
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x00049CA8 File Offset: 0x00047EA8
	public CameraSettings(CameraSettingsAsset settings, FogGradientController fogGradient)
	{
		if (settings == null)
		{
			return;
		}
		this.UseBloomAndLensFlares = settings.UseBloomAndLensFlares;
		this.UseSeinPostProcessing = settings.UseSeinPostProcessing;
		this.Vignetting = settings.Vignetting.Clone();
		this.Noise = settings.Noise.Clone();
		this.Contrast = settings.Contrast.Clone();
		this.Desaturation = settings.Desaturation.Clone();
		this.ColorCorrection = settings.ColorCorrection.Clone();
		this.BloomAndFlaresSettings = settings.BloomAndFlaresSettings.Clone();
		this.TwirlSettings = settings.TwirlSettings.Clone();
		this.Fog = settings.Fog;
		if (fogGradient)
		{
			this.FogGradient = CameraSettings.ConvertGradient(fogGradient.FogGradient);
			this.FogRange = fogGradient.FogRange;
		}
		else
		{
			this.FogGradient = new Color[128];
			this.FogRange = 100f;
		}
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x00049DC0 File Offset: 0x00047FC0
	public CameraSettings(CameraSettingsAsset cameraSettings, Gradient fogGradient, float range)
	{
		this.UseBloomAndLensFlares = cameraSettings.UseBloomAndLensFlares;
		this.UseSeinPostProcessing = cameraSettings.UseSeinPostProcessing;
		this.Vignetting = cameraSettings.Vignetting;
		this.Noise = cameraSettings.Noise;
		this.Contrast = cameraSettings.Contrast;
		this.Desaturation = cameraSettings.Desaturation;
		this.ColorCorrection = cameraSettings.ColorCorrection;
		this.BloomAndFlaresSettings = cameraSettings.BloomAndFlaresSettings;
		this.TwirlSettings = cameraSettings.TwirlSettings;
		this.Fog = cameraSettings.Fog;
		if (fogGradient != null)
		{
			this.FogGradient = CameraSettings.ConvertGradient(fogGradient);
		}
		else
		{
			this.FogGradient = new Color[128];
		}
		this.FogRange = range;
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x00049E8C File Offset: 0x0004808C
	public static Color[] ConvertGradient(Gradient gradient)
	{
		Color[] array = new Color[128];
		for (int i = 0; i < 128; i++)
		{
			array[i] = gradient.Evaluate((float)i / 127f);
		}
		return array;
	}

	// Token: 0x04000D52 RID: 3410
	public bool UseBloomAndLensFlares;

	// Token: 0x04000D53 RID: 3411
	public bool UseSeinPostProcessing = true;

	// Token: 0x04000D54 RID: 3412
	public VignettingSettings Vignetting;

	// Token: 0x04000D55 RID: 3413
	public NoiseSettings Noise;

	// Token: 0x04000D56 RID: 3414
	public ContrastSettings Contrast;

	// Token: 0x04000D57 RID: 3415
	public DesaturationSettings Desaturation;

	// Token: 0x04000D58 RID: 3416
	public ColorCorrectionSettings ColorCorrection;

	// Token: 0x04000D59 RID: 3417
	public BloomAndFlaresSettings BloomAndFlaresSettings;

	// Token: 0x04000D5A RID: 3418
	public TwirlSettings TwirlSettings;

	// Token: 0x04000D5B RID: 3419
	public Color Fog;

	// Token: 0x04000D5C RID: 3420
	public Color[] FogGradient;

	// Token: 0x04000D5D RID: 3421
	public float FogRange = 100f;
}
