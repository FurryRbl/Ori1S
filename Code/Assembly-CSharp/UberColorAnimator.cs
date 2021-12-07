using System;
using UnityEngine;

// Token: 0x02000784 RID: 1924
public class UberColorAnimator : BaseAnimator
{
	// Token: 0x06002CA7 RID: 11431 RVA: 0x000BF92C File Offset: 0x000BDB2C
	public string ColorPropertyFromColorName(UberColorAnimator.ColorName colorName)
	{
		return this.m_unityColorProperties[(int)colorName];
	}

	// Token: 0x06002CA8 RID: 11432 RVA: 0x000BF938 File Offset: 0x000BDB38
	public int ColorPropertyIDFromColorName(UberColorAnimator.ColorName colorName)
	{
		switch (colorName)
		{
		case UberColorAnimator.ColorName.Color:
			return ShaderProperties.Color;
		case UberColorAnimator.ColorName.TintColor:
			return ShaderProperties.TintColor;
		case UberColorAnimator.ColorName.AdditiveLayerColor:
			return ShaderProperties.AdditiveLayerColor;
		case UberColorAnimator.ColorName.AdditiveLayerDistortColor:
			return ShaderProperties.AdditiveLayerDistortColor;
		case UberColorAnimator.ColorName.AdditiveLayerExtraColor:
			return ShaderProperties.AdditiveLayerExtraColor;
		case UberColorAnimator.ColorName.MultiplyLayerColor:
			return ShaderProperties.MultiplyLayerColor;
		case UberColorAnimator.ColorName.MultiplyLayerExtraColor:
			return ShaderProperties.MultiplyLayerExtraColor;
		case UberColorAnimator.ColorName.MultiplyLayerThirdColor:
			return ShaderProperties.MultiplyLayerThirdColor;
		case UberColorAnimator.ColorName.MultiplyLayerDistortColor:
			return ShaderProperties.MultiplyLayerDistortColor;
		default:
			throw new ArgumentOutOfRangeException("colorName", colorName, null);
		}
	}

	// Token: 0x1700071F RID: 1823
	// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000BF9C0 File Offset: 0x000BDBC0
	// (set) Token: 0x06002CAA RID: 11434 RVA: 0x000BFA20 File Offset: 0x000BDC20
	public Color CurrentColor
	{
		get
		{
			if (Application.isPlaying)
			{
				return base.GetComponent<Renderer>().sharedMaterial.GetColor(this.ColorPropertyIDFromColorName(this.ColorTarget));
			}
			UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
			UberShaderColor uberShaderColor = this.UberShaderColorFromColorName(this.ColorTarget, component);
			if (uberShaderColor != null)
			{
				return uberShaderColor.Color;
			}
			return Color.clear;
		}
		set
		{
			if (!Application.isPlaying)
			{
				UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
				UberShaderColor uberShaderColor = this.UberShaderColorFromColorName(this.ColorTarget, component);
				if (uberShaderColor != null)
				{
					uberShaderColor.Color = value;
				}
			}
			else
			{
				base.GetComponent<Renderer>().material.SetColor(this.ColorPropertyIDFromColorName(this.ColorTarget), value);
			}
		}
	}

	// Token: 0x06002CAB RID: 11435 RVA: 0x000BFA80 File Offset: 0x000BDC80
	public UberShaderColor UberShaderColorFromColorName(UberColorAnimator.ColorName colorName, UberShaderComponent uberShaderComponent)
	{
		if (uberShaderComponent == null)
		{
			return null;
		}
		switch (colorName)
		{
		case UberColorAnimator.ColorName.Color:
			return uberShaderComponent.TexturedBlock.Color;
		case UberColorAnimator.ColorName.TintColor:
		{
			TintModifier modifier = uberShaderComponent.GetModifier<TintModifier>();
			return (!(modifier != null)) ? null : modifier.Tint;
		}
		case UberColorAnimator.ColorName.AdditiveLayerColor:
		{
			AdditiveLayerModifier modifier2 = uberShaderComponent.GetModifier<AdditiveLayerModifier>();
			return (!(modifier2 != null)) ? null : modifier2.AdditiveLayerColor;
		}
		case UberColorAnimator.ColorName.AdditiveLayerDistortColor:
		{
			AdditiveLayerDistortedModifier modifier3 = uberShaderComponent.GetModifier<AdditiveLayerDistortedModifier>();
			return (!(modifier3 != null)) ? null : modifier3.AdditiveLayerColor;
		}
		case UberColorAnimator.ColorName.AdditiveLayerExtraColor:
		{
			AdditiveLayerExtraModifier modifier4 = uberShaderComponent.GetModifier<AdditiveLayerExtraModifier>();
			return (!(modifier4 != null)) ? null : modifier4.AdditiveLayerColor;
		}
		default:
			return null;
		}
	}

	// Token: 0x17000720 RID: 1824
	// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000BFB4B File Offset: 0x000BDD4B
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.IsLooping();
		}
	}

	// Token: 0x06002CAD RID: 11437 RVA: 0x000BFB58 File Offset: 0x000BDD58
	public override void CacheOriginals()
	{
		this.m_originalColor = this.CurrentColor;
	}

	// Token: 0x06002CAE RID: 11438 RVA: 0x000BFB68 File Offset: 0x000BDD68
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		Color currentColor = this.CurrentColor;
		Color color = Color.Lerp(this.m_originalColor, this.Color, this.AnimationCurve.Evaluate(value));
		if (this.Red)
		{
			currentColor.r = color.r;
		}
		if (this.Green)
		{
			currentColor.g = color.g;
		}
		if (this.Blue)
		{
			currentColor.b = color.b;
		}
		if (this.Alpha)
		{
			currentColor.a = color.a;
		}
		this.CurrentColor = currentColor;
	}

	// Token: 0x17000721 RID: 1825
	// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000BFC0E File Offset: 0x000BDE0E
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x06002CB0 RID: 11440 RVA: 0x000BFC21 File Offset: 0x000BDE21
	public override void RestoreToOriginalState()
	{
		this.CurrentColor = this.m_originalColor;
	}

	// Token: 0x04002860 RID: 10336
	private readonly string[] m_unityColorProperties = new string[]
	{
		"_Color",
		"_TintColor",
		"_AdditiveLayerColor",
		"_AdditiveLayerDistortColor",
		"_AdditiveLayerExtraColor",
		"_MultiplyLayerColor",
		"_MultiplyLayerExtraColor",
		"_MultiplyLayerThirdColor",
		"_MultiplyLayerDistortColor"
	};

	// Token: 0x04002861 RID: 10337
	public Color Color;

	// Token: 0x04002862 RID: 10338
	public bool Red = true;

	// Token: 0x04002863 RID: 10339
	public bool Green = true;

	// Token: 0x04002864 RID: 10340
	public bool Blue = true;

	// Token: 0x04002865 RID: 10341
	public bool Alpha = true;

	// Token: 0x04002866 RID: 10342
	public AnimationCurve AnimationCurve;

	// Token: 0x04002867 RID: 10343
	public UberColorAnimator.ColorName ColorTarget;

	// Token: 0x04002868 RID: 10344
	private Color m_originalColor;

	// Token: 0x02000785 RID: 1925
	public enum ColorName
	{
		// Token: 0x0400286A RID: 10346
		Color,
		// Token: 0x0400286B RID: 10347
		TintColor,
		// Token: 0x0400286C RID: 10348
		AdditiveLayerColor,
		// Token: 0x0400286D RID: 10349
		AdditiveLayerDistortColor,
		// Token: 0x0400286E RID: 10350
		AdditiveLayerExtraColor,
		// Token: 0x0400286F RID: 10351
		MultiplyLayerColor,
		// Token: 0x04002870 RID: 10352
		MultiplyLayerExtraColor,
		// Token: 0x04002871 RID: 10353
		MultiplyLayerThirdColor,
		// Token: 0x04002872 RID: 10354
		MultiplyLayerDistortColor
	}
}
