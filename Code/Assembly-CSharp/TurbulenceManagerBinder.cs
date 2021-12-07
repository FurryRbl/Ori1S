using System;
using UnityEngine;

// Token: 0x02000846 RID: 2118
public class TurbulenceManagerBinder
{
	// Token: 0x06003036 RID: 12342 RVA: 0x000CC024 File Offset: 0x000CA224
	private Texture2D CombineTexture(Texture2D tex1, Texture2D tex2, float tweenTime)
	{
		if (tex1.width != tex2.width)
		{
			Debug.LogError("Turbulence Textures not the same size?");
			return null;
		}
		Color[] pixels = tex1.GetPixels();
		Color[] pixels2 = tex2.GetPixels();
		if (this.m_colorSet == null || this.m_colorSet.Length != tex1.width)
		{
			this.m_colorSet = new Color[tex1.width];
		}
		for (int i = 0; i < tex1.width; i++)
		{
			float r = pixels[i].r * (1f - tweenTime) + pixels2[i].r * tweenTime;
			float g = pixels[i].g * (1f - tweenTime) + pixels2[i].g * tweenTime;
			float b = pixels[i].b * (1f - tweenTime) + pixels2[i].b * tweenTime;
			float a = pixels[i].a * (1f - tweenTime) + pixels2[i].a * tweenTime;
			this.m_colorSet[i] = new Color(r, g, b, a);
		}
		if (this.m_lerpTex == null || this.m_lerpTex.width != tex1.width)
		{
			this.m_lerpTex = new Texture2D(tex1.width, 1, TextureFormat.RGBA32, false);
		}
		this.m_lerpTex.SetPixels(this.m_colorSet);
		this.m_lerpTex.Apply();
		return this.m_lerpTex;
	}

	// Token: 0x06003037 RID: 12343 RVA: 0x000CC1B0 File Offset: 0x000CA3B0
	private TurbulenceManagerBinder.CurrentShaderSettings TweenSettings(TurbulenceSettings from, TurbulenceSettings to, float tweenTime, float time, float strMult, float speedMult)
	{
		float curveDuration = from.CurveDuration;
		float curveDuration2 = to.CurveDuration;
		float duration = Mathf.Lerp(curveDuration, curveDuration2, tweenTime);
		float num = from.TurbulenceSpeedOverTime.Evaluate(time) * from.TurbulenceSpeed;
		float num2 = from.TurbulenceMagnitudeOverTime.Evaluate(time) * from.Scale * from.TurbulenceMagnitude;
		Texture2D turbulenceTexture = from.TurbulenceTexture;
		float turbulenceValueOffset = from.TurbulenceValueOffset;
		if (from == to || tweenTime == 0f)
		{
			return new TurbulenceManagerBinder.CurrentShaderSettings
			{
				Speed = num * speedMult,
				Strength = num2 * strMult,
				TurbulenceTexture = turbulenceTexture,
				Duration = curveDuration,
				Offset = turbulenceValueOffset
			};
		}
		float b = to.TurbulenceSpeedOverTime.Evaluate(time) * to.TurbulenceSpeed;
		float b2 = to.TurbulenceMagnitudeOverTime.Evaluate(time) * to.Scale * to.TurbulenceMagnitude;
		Texture2D turbulenceTexture2 = to.TurbulenceTexture;
		float turbulenceValueOffset2 = to.TurbulenceValueOffset;
		Texture2D turbulenceTexture3;
		if (tweenTime == 0f)
		{
			turbulenceTexture3 = turbulenceTexture;
		}
		else if (tweenTime >= 1f)
		{
			turbulenceTexture3 = turbulenceTexture2;
		}
		else
		{
			turbulenceTexture3 = this.CombineTexture(turbulenceTexture, turbulenceTexture2, tweenTime);
		}
		return new TurbulenceManagerBinder.CurrentShaderSettings
		{
			Speed = Mathf.Lerp(num, b, tweenTime) * speedMult,
			Strength = Mathf.Lerp(num2, b2, tweenTime) * strMult,
			TurbulenceTexture = turbulenceTexture3,
			Duration = duration,
			Offset = Mathf.Lerp(turbulenceValueOffset, turbulenceValueOffset2, tweenTime)
		};
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x000CC344 File Offset: 0x000CA544
	public TurbulenceManagerBinder.CurrentShaderSettings Bind(TurbulenceSettings from, TurbulenceSettings to, float tweenTime, float time, float strMult, float speedMult)
	{
		TurbulenceManagerBinder.CurrentShaderSettings result = this.TweenSettings(from, to, tweenTime, time, strMult, speedMult);
		if (result.TurbulenceTexture != null)
		{
			Shader.SetGlobalTexture("_TurbulenceTexture", result.TurbulenceTexture);
		}
		Shader.SetGlobalFloat("_TurbulenceTime", time / result.Duration);
		Shader.SetGlobalFloat("_TurbulenceStrength", result.Strength * 0.5f * 1.3f);
		Shader.SetGlobalFloat("_TurbulenceOffset", result.Offset * 0.5f);
		return result;
	}

	// Token: 0x04002B63 RID: 11107
	private Color[] m_colorSet;

	// Token: 0x04002B64 RID: 11108
	private Texture2D m_lerpTex;

	// Token: 0x02000847 RID: 2119
	public struct CurrentShaderSettings
	{
		// Token: 0x04002B65 RID: 11109
		public float Speed;

		// Token: 0x04002B66 RID: 11110
		public float Strength;

		// Token: 0x04002B67 RID: 11111
		public Texture2D TurbulenceTexture;

		// Token: 0x04002B68 RID: 11112
		public float Duration;

		// Token: 0x04002B69 RID: 11113
		public float Offset;
	}
}
