using System;
using UnityEngine;

// Token: 0x020003EA RID: 1002
public static class UberPostProcessingAnimation
{
	// Token: 0x06001B5F RID: 7007 RVA: 0x00075E15 File Offset: 0x00074015
	public static void CopyCameraSettings(ref CameraSettings settings, CameraSettings fromSettings)
	{
		UberPostProcessingAnimation.AnimateCameraSettings(ref settings, fromSettings, fromSettings, 0f);
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x00075E24 File Offset: 0x00074024
	public static void AnimateCameraSettings(ref CameraSettings settings, CameraSettings fromSettings, CameraSettings toSettings, float curveValue)
	{
		UberPostProcessingAnimation.AnimateVignettingSettings(ref settings.Vignetting, fromSettings.Vignetting, toSettings.Vignetting, curveValue);
		UberPostProcessingAnimation.AnimateNoiseSettings(ref settings.Noise, fromSettings.Noise, toSettings.Noise, curveValue);
		UberPostProcessingAnimation.AnimateContrastSettings(ref settings.Contrast, fromSettings.Contrast, toSettings.Contrast, curveValue);
		UberPostProcessingAnimation.AnimateDesaturationSettings(ref settings.Desaturation, fromSettings.Desaturation, toSettings.Desaturation, curveValue);
		UberPostProcessingAnimation.AnimateColorCorrectionSettings(ref settings.ColorCorrection, fromSettings.ColorCorrection, toSettings.ColorCorrection, curveValue);
		UberPostProcessingAnimation.AnimateBloomAndFlaresSettings(ref settings.BloomAndFlaresSettings, fromSettings.BloomAndFlaresSettings, toSettings.BloomAndFlaresSettings, curveValue);
		settings.Fog = Color.Lerp(fromSettings.Fog, toSettings.Fog, curveValue);
		settings.FogRange = Mathf.Lerp(fromSettings.FogRange, toSettings.FogRange, curveValue);
		settings.TwirlSettings.Strength = Mathf.Lerp(fromSettings.TwirlSettings.Strength, toSettings.TwirlSettings.Strength, curveValue);
		settings.TwirlSettings.PosVariation = Mathf.Lerp(fromSettings.TwirlSettings.PosVariation, toSettings.TwirlSettings.PosVariation, curveValue);
		int num = fromSettings.FogGradient.Length;
		for (int i = 0; i < num; i++)
		{
			Color a = fromSettings.FogGradient[i];
			Color a2 = toSettings.FogGradient[i];
			Color color = Color.Lerp(a * a.a, a2 * a2.a, curveValue);
			color.a = Mathf.Lerp(a.a, a2.a, curveValue);
			if (color.a != 0f)
			{
				color.r /= color.a;
				color.g /= color.a;
				color.b /= color.a;
			}
			settings.FogGradient[i] = color;
		}
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x00076029 File Offset: 0x00074229
	public static void AnimateVignettingSettings(ref VignettingSettings vignettingSettings, VignettingSettings fromSettings, VignettingSettings toSettings, float curveValue)
	{
		vignettingSettings.Intensity = Mathf.Lerp(fromSettings.Intensity, toSettings.Intensity, curveValue);
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x00076044 File Offset: 0x00074244
	public static void AnimateNoiseSettings(ref NoiseSettings noiseSettings, NoiseSettings fromSettings, NoiseSettings toSettings, float curveValue)
	{
		noiseSettings.GrainIntensityMax = Mathf.Lerp(fromSettings.GrainIntensityMax, toSettings.GrainIntensityMax, curveValue);
		noiseSettings.GrainIntensityMin = Mathf.Lerp(fromSettings.GrainIntensityMin, toSettings.GrainIntensityMin, curveValue);
		noiseSettings.GrainSize = Mathf.Lerp(fromSettings.GrainSize, toSettings.GrainSize, curveValue);
		noiseSettings.GrainTexture = fromSettings.GrainTexture;
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x000760AC File Offset: 0x000742AC
	public static void AnimateContrastSettings(ref ContrastSettings contrastSettings, ContrastSettings fromSettings, ContrastSettings toSettings, float curveValue)
	{
		contrastSettings.Brightness = Mathf.Lerp(fromSettings.Brightness, toSettings.Brightness, curveValue);
		contrastSettings.Contrast = Mathf.Lerp(fromSettings.Contrast, toSettings.Contrast, curveValue);
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x000760EB File Offset: 0x000742EB
	public static void AnimateDesaturationSettings(ref DesaturationSettings desaturationSettings, DesaturationSettings fromSettings, DesaturationSettings toSettings, float curveValue)
	{
		desaturationSettings.Amount = Mathf.Lerp(fromSettings.Amount, toSettings.Amount, curveValue);
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x00076108 File Offset: 0x00074308
	public static void AnimateColorCorrectionSettings(ref ColorCorrectionSettings colorCorrectionSettings, ColorCorrectionSettings fromSettings, ColorCorrectionSettings toSettings, float curveValue)
	{
		UberPostProcessingAnimation.AnimateCurveKeyframe(ref colorCorrectionSettings.Red, 0, fromSettings.Red, toSettings.Red, curveValue);
		UberPostProcessingAnimation.AnimateCurveKeyframe(ref colorCorrectionSettings.Red, 1, fromSettings.Red, toSettings.Red, curveValue);
		UberPostProcessingAnimation.AnimateCurveKeyframe(ref colorCorrectionSettings.Green, 0, fromSettings.Green, toSettings.Green, curveValue);
		UberPostProcessingAnimation.AnimateCurveKeyframe(ref colorCorrectionSettings.Green, 1, fromSettings.Green, toSettings.Green, curveValue);
		UberPostProcessingAnimation.AnimateCurveKeyframe(ref colorCorrectionSettings.Blue, 0, fromSettings.Blue, toSettings.Blue, curveValue);
		UberPostProcessingAnimation.AnimateCurveKeyframe(ref colorCorrectionSettings.Blue, 1, fromSettings.Blue, toSettings.Blue, curveValue);
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000761B4 File Offset: 0x000743B4
	public static void AnimateCurveKeyframe(ref AnimationCurve curve, int keyframeIndex, AnimationCurve fromCurve, AnimationCurve toCurve, float curveValue)
	{
		curve.MoveKey(keyframeIndex, new Keyframe(Mathf.Lerp(fromCurve[keyframeIndex].time, toCurve[keyframeIndex].time, curveValue), Mathf.Lerp(fromCurve[keyframeIndex].value, toCurve[keyframeIndex].value, curveValue), Mathf.Lerp(fromCurve[keyframeIndex].inTangent, toCurve[keyframeIndex].inTangent, curveValue), Mathf.Lerp(fromCurve[keyframeIndex].outTangent, toCurve[keyframeIndex].outTangent, curveValue)));
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x00076268 File Offset: 0x00074468
	public static void AnimateBloomAndFlaresSettings(ref BloomAndFlaresSettings bloomAndFlaresSettings, BloomAndFlaresSettings fromSettings, BloomAndFlaresSettings toSettings, float curveValue)
	{
		bloomAndFlaresSettings.Intensity = Mathf.Lerp(fromSettings.Intensity, toSettings.Intensity, curveValue);
		bloomAndFlaresSettings.Threshhold = Mathf.Lerp(fromSettings.Threshhold, toSettings.Threshhold, curveValue);
		bloomAndFlaresSettings.BlurSpread = Mathf.Lerp(fromSettings.BlurSpread, toSettings.BlurSpread, curveValue);
		bloomAndFlaresSettings.LocalIntensity = Mathf.Lerp(fromSettings.LocalIntensity, toSettings.LocalIntensity, curveValue);
		bloomAndFlaresSettings.LocalThreshhold = Mathf.Lerp(fromSettings.LocalThreshhold, toSettings.LocalThreshhold, curveValue);
		bloomAndFlaresSettings.FlareColorA = Color.Lerp(fromSettings.FlareColorA, toSettings.FlareColorA, curveValue);
		bloomAndFlaresSettings.FlareColorB = Color.Lerp(fromSettings.FlareColorB, toSettings.FlareColorB, curveValue);
		bloomAndFlaresSettings.FlareColorC = Color.Lerp(fromSettings.FlareColorC, toSettings.FlareColorC, curveValue);
		bloomAndFlaresSettings.FlareColorD = Color.Lerp(fromSettings.FlareColorD, toSettings.FlareColorD, curveValue);
		bloomAndFlaresSettings.BlurIterations = fromSettings.BlurIterations;
	}
}
