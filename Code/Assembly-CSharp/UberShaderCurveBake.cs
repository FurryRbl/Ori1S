using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

// Token: 0x02000834 RID: 2100
public static class UberShaderCurveBake
{
	// Token: 0x06002FFA RID: 12282 RVA: 0x000CB208 File Offset: 0x000C9408
	private static bool GradientsAreEqual(Gradient a, UberShaderCurveBake.GradientData data)
	{
		GradientAlphaKey[] alphakeys = UberShaderCurveBake.GetAlphakeys(a);
		GradientColorKey[] colorKeys = UberShaderCurveBake.GetColorKeys(a);
		if (alphakeys.Length != data.CheckAlpha.Length || colorKeys.Length != data.CheckColor.Length)
		{
			return false;
		}
		for (int i = 0; i < alphakeys.Length; i++)
		{
			GradientAlphaKey gradientAlphaKey = alphakeys[i];
			GradientAlphaKey gradientAlphaKey2 = data.CheckAlpha[i];
			if (gradientAlphaKey.time != gradientAlphaKey2.time || gradientAlphaKey.alpha != gradientAlphaKey2.alpha)
			{
				return false;
			}
		}
		for (int j = 0; j < colorKeys.Length; j++)
		{
			GradientColorKey gradientColorKey = colorKeys[j];
			GradientColorKey gradientColorKey2 = data.CheckColor[j];
			if (gradientColorKey.time != gradientColorKey2.time || gradientColorKey.color != gradientColorKey2.color)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002FFB RID: 12283 RVA: 0x000CB310 File Offset: 0x000C9510
	private static void Init()
	{
		if (UberShaderCurveBake.s_curves == null)
		{
			UberShaderCurveBake.s_curves = new Dictionary<string, UberShaderCurveBake.CurveData>();
		}
		if (UberShaderCurveBake.s_gradients == null)
		{
			UberShaderCurveBake.s_gradients = new Dictionary<string, UberShaderCurveBake.GradientData>();
		}
	}

	// Token: 0x06002FFC RID: 12284 RVA: 0x000CB348 File Offset: 0x000C9548
	private static string GetCurveString(AnimationCurve curve)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Keyframe keyframe in curve.keys)
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				keyframe.time,
				"_",
				keyframe.value,
				"_",
				keyframe.inTangent,
				"_",
				keyframe.outTangent
			}));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06002FFD RID: 12285 RVA: 0x000CB3F0 File Offset: 0x000C95F0
	private static string GetGradientString(Gradient g)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (GradientColorKey gradientColorKey in UberShaderCurveBake.GetColorKeys(g))
		{
			stringBuilder.Append(gradientColorKey.color + "_" + gradientColorKey.time);
		}
		foreach (GradientAlphaKey gradientAlphaKey in UberShaderCurveBake.GetAlphakeys(g))
		{
			stringBuilder.Append(gradientAlphaKey.alpha + "_" + gradientAlphaKey.time);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06002FFE RID: 12286 RVA: 0x000CB4B4 File Offset: 0x000C96B4
	public static Texture2D BakeAnimationCurve(AnimationCurve curve, TextureWrapMode wrapMode, int resolution, out float scale, out float duration)
	{
		UberShaderCurveBake.Init();
		if (curve == null || curve.length == 0)
		{
			duration = 0f;
			scale = 0f;
			return null;
		}
		scale = -1f;
		duration = curve[curve.length - 1].time;
		Color[] array = new Color[resolution];
		float[] array2 = new float[resolution];
		for (int i = 0; i < resolution; i++)
		{
			float time = (float)i / (float)(resolution - 1) * duration;
			array2[i] = curve.Evaluate(time);
			scale = Mathf.Max(scale, Mathf.Abs(array2[i]));
		}
		Texture2D texture2D = new Texture2D(resolution, 1, TextureFormat.Alpha8, false);
		for (int j = 0; j < resolution; j++)
		{
			array[j] = new Color(0f, 0f, 0f, (array2[j] / scale + 1f) / 2f);
		}
		texture2D.SetPixels(array);
		texture2D.wrapMode = wrapMode;
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06002FFF RID: 12287 RVA: 0x000CB5BE File Offset: 0x000C97BE
	private static GradientColorKey[] GetColorKeys(Gradient gradient)
	{
		return gradient.GetType().GetMethod("get_colorKeys", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(gradient, null) as GradientColorKey[];
	}

	// Token: 0x06003000 RID: 12288 RVA: 0x000CB5DE File Offset: 0x000C97DE
	private static GradientAlphaKey[] GetAlphakeys(Gradient gradient)
	{
		return gradient.GetType().GetMethod("get_alphaKeys", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(gradient, null) as GradientAlphaKey[];
	}

	// Token: 0x06003001 RID: 12289 RVA: 0x000CB600 File Offset: 0x000C9800
	public static Texture2D BakeAnimationGradient(Gradient gradient, int resolution)
	{
		UberShaderCurveBake.Init();
		if (gradient == null)
		{
			return null;
		}
		string gradientString = UberShaderCurveBake.GetGradientString(gradient);
		if (UberShaderCurveBake.s_gradients.ContainsKey(gradientString))
		{
			UberShaderCurveBake.GradientData value = UberShaderCurveBake.s_gradients[gradientString];
			value.CheckTime = Time.realtimeSinceStartup;
			value.CheckAlpha = UberShaderCurveBake.GetAlphakeys(gradient);
			value.CheckColor = UberShaderCurveBake.GetColorKeys(gradient);
			UberShaderCurveBake.s_gradients[gradientString] = value;
			return UberShaderCurveBake.s_gradients[gradientString].Texture;
		}
		Color[] array = new Color[resolution];
		for (int i = 0; i < resolution; i++)
		{
			float time = (float)i / (float)resolution;
			array[i] = gradient.Evaluate(time);
		}
		Texture2D texture2D = new Texture2D(resolution, 1, TextureFormat.ARGB32, false);
		texture2D.SetPixels(array);
		texture2D.wrapMode = TextureWrapMode.Repeat;
		texture2D.Apply();
		UberShaderCurveBake.GradientData value2 = new UberShaderCurveBake.GradientData
		{
			Texture = texture2D,
			CheckAlpha = UberShaderCurveBake.GetAlphakeys(gradient),
			CheckColor = UberShaderCurveBake.GetColorKeys(gradient),
			CheckTime = Time.realtimeSinceStartup
		};
		if (!UberShaderCurveBake.s_gradients.ContainsKey(gradientString))
		{
			UberShaderCurveBake.s_gradients.Add(gradientString, value2);
		}
		else
		{
			UberShaderCurveBake.s_gradients[gradientString] = value2;
		}
		return texture2D;
	}

	// Token: 0x04002B30 RID: 11056
	private static Dictionary<string, UberShaderCurveBake.CurveData> s_curves;

	// Token: 0x04002B31 RID: 11057
	private static Dictionary<string, UberShaderCurveBake.GradientData> s_gradients;

	// Token: 0x0200083D RID: 2109
	public struct CurveData
	{
		// Token: 0x04002B49 RID: 11081
		public TextureWrapMode WrapMode;

		// Token: 0x04002B4A RID: 11082
		public Keyframe[] CheckCurve;

		// Token: 0x04002B4B RID: 11083
		public Texture2D Texture;

		// Token: 0x04002B4C RID: 11084
		public float Duration;

		// Token: 0x04002B4D RID: 11085
		public float Scale;

		// Token: 0x04002B4E RID: 11086
		public float CheckTime;
	}

	// Token: 0x0200083E RID: 2110
	public struct GradientData
	{
		// Token: 0x04002B4F RID: 11087
		public GradientColorKey[] CheckColor;

		// Token: 0x04002B50 RID: 11088
		public GradientAlphaKey[] CheckAlpha;

		// Token: 0x04002B51 RID: 11089
		public Texture2D Texture;

		// Token: 0x04002B52 RID: 11090
		public float Duration;

		// Token: 0x04002B53 RID: 11091
		public float Scale;

		// Token: 0x04002B54 RID: 11092
		public float CheckTime;
	}
}
