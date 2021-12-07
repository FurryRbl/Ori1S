using System;
using UnityEngine;

// Token: 0x02000793 RID: 1939
public class UberTextureSettingsAnimator : BaseAnimator
{
	// Token: 0x17000732 RID: 1842
	// (get) Token: 0x06002D02 RID: 11522 RVA: 0x000C0B39 File Offset: 0x000BED39
	public Vector2 OriginalOffset
	{
		get
		{
			return this.m_originalOffset;
		}
	}

	// Token: 0x17000733 RID: 1843
	// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000C0B41 File Offset: 0x000BED41
	public Vector2 OriginalScale
	{
		get
		{
			return this.m_originalScale;
		}
	}

	// Token: 0x17000734 RID: 1844
	// (get) Token: 0x06002D04 RID: 11524 RVA: 0x000C0B49 File Offset: 0x000BED49
	public float OriginalRotation
	{
		get
		{
			return this.m_originalRotation;
		}
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x000C0B51 File Offset: 0x000BED51
	public override void CacheOriginals()
	{
		this.m_originalOffset = this.CurrentTextureOffset;
		this.m_originalScale = this.CurrentTextureScale;
		this.m_originalRotation = this.CurrentTextureRotation;
	}

	// Token: 0x17000735 RID: 1845
	// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000C0B78 File Offset: 0x000BED78
	// (set) Token: 0x06002D07 RID: 11527 RVA: 0x000C0BD8 File Offset: 0x000BEDD8
	public Vector2 CurrentTextureOffset
	{
		get
		{
			if (Application.isPlaying)
			{
				return base.GetComponent<Renderer>().sharedMaterial.GetTextureOffset(this.UberShaderTexturePropertyFromTextureName(this.TextureTarget));
			}
			UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
			UberShaderTextureBase uberShaderTextureBase = this.UberShaderTextureBaseFromTextureName(this.TextureTarget, component);
			if (uberShaderTextureBase != null)
			{
				return uberShaderTextureBase.TextureOffset;
			}
			return Vector2.zero;
		}
		set
		{
			if (!Application.isPlaying)
			{
				UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
				UberShaderTextureBase uberShaderTextureBase = this.UberShaderTextureBaseFromTextureName(this.TextureTarget, component);
				if (uberShaderTextureBase != null)
				{
					uberShaderTextureBase.TextureOffset = value;
				}
			}
			else
			{
				base.GetComponent<Renderer>().material.SetTextureOffset(this.UberShaderTexturePropertyFromTextureName(this.TextureTarget), value);
			}
		}
	}

	// Token: 0x17000736 RID: 1846
	// (get) Token: 0x06002D08 RID: 11528 RVA: 0x000C0C38 File Offset: 0x000BEE38
	// (set) Token: 0x06002D09 RID: 11529 RVA: 0x000C0C98 File Offset: 0x000BEE98
	public Vector2 CurrentTextureScale
	{
		get
		{
			if (Application.isPlaying)
			{
				return base.GetComponent<Renderer>().sharedMaterial.GetTextureScale(this.UberShaderTexturePropertyFromTextureName(this.TextureTarget));
			}
			UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
			UberShaderTextureBase uberShaderTextureBase = this.UberShaderTextureBaseFromTextureName(this.TextureTarget, component);
			if (uberShaderTextureBase != null)
			{
				return uberShaderTextureBase.TextureScale;
			}
			return Vector2.zero;
		}
		set
		{
			if (!Application.isPlaying)
			{
				UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
				UberShaderTextureBase uberShaderTextureBase = this.UberShaderTextureBaseFromTextureName(this.TextureTarget, component);
				if (uberShaderTextureBase != null)
				{
					uberShaderTextureBase.TextureScale = value;
				}
			}
			else
			{
				base.GetComponent<Renderer>().material.SetTextureScale(this.UberShaderTexturePropertyFromTextureName(this.TextureTarget), value);
			}
		}
	}

	// Token: 0x17000737 RID: 1847
	// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000C0CF8 File Offset: 0x000BEEF8
	// (set) Token: 0x06002D0B RID: 11531 RVA: 0x000C0D68 File Offset: 0x000BEF68
	public float CurrentTextureRotation
	{
		get
		{
			if (Application.isPlaying)
			{
				string propertyName = this.UberShaderTexturePropertyStFromTextureName(this.TextureTarget);
				return base.GetComponent<Renderer>().material.GetVector(propertyName).z * 57.29578f;
			}
			UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
			UberShaderTextureBase uberShaderTextureBase = this.UberShaderTextureBaseFromTextureName(this.TextureTarget, component);
			if (uberShaderTextureBase != null)
			{
				return uberShaderTextureBase.TextureRotation;
			}
			return 0f;
		}
		set
		{
			if (!Application.isPlaying)
			{
				UberShaderComponent component = base.gameObject.GetComponent<UberShaderComponent>();
				UberShaderTextureBase uberShaderTextureBase = this.UberShaderTextureBaseFromTextureName(this.TextureTarget, component);
				if (uberShaderTextureBase != null)
				{
					uberShaderTextureBase.TextureRotation = value;
				}
			}
			else
			{
				Renderer component2 = base.GetComponent<Renderer>();
				string propertyName = this.UberShaderTexturePropertyStFromTextureName(this.TextureTarget);
				Vector4 vector = component2.material.GetVector(propertyName);
				vector.z = value * 0.017453292f;
				component2.material.SetVector(propertyName, vector);
			}
		}
	}

	// Token: 0x06002D0C RID: 11532 RVA: 0x000C0DE9 File Offset: 0x000BEFE9
	public string UberShaderTexturePropertyFromTextureName(UberTextureSettingsAnimator.TextureName textureName)
	{
		return UberTextureSettingsAnimator.s_unityTextureProperties[(int)textureName];
	}

	// Token: 0x06002D0D RID: 11533 RVA: 0x000C0DF2 File Offset: 0x000BEFF2
	public string UberShaderTexturePropertyStFromTextureName(UberTextureSettingsAnimator.TextureName textureName)
	{
		return UberTextureSettingsAnimator.s_unityTexturePropertiesSt[(int)textureName];
	}

	// Token: 0x06002D0E RID: 11534 RVA: 0x000C0DFC File Offset: 0x000BEFFC
	public UberShaderTextureBase UberShaderTextureBaseFromTextureName(UberTextureSettingsAnimator.TextureName textureName, UberShaderComponent uberShaderComponent)
	{
		if (uberShaderComponent == null)
		{
			return null;
		}
		switch (textureName)
		{
		case UberTextureSettingsAnimator.TextureName.MainTexture:
			return uberShaderComponent.TexturedBlock.MainTexture;
		case UberTextureSettingsAnimator.TextureName.MaskTexture:
		{
			MaskModifier modifier = uberShaderComponent.GetModifier<MaskModifier>();
			return (!(modifier != null)) ? null : modifier.MaskTexture;
		}
		case UberTextureSettingsAnimator.TextureName.MaskTextureExtra:
		{
			MaskExtraModifier modifier2 = uberShaderComponent.GetModifier<MaskExtraModifier>();
			return (!(modifier2 != null)) ? null : modifier2.MaskTexture;
		}
		case UberTextureSettingsAnimator.TextureName.Distortion:
		{
			DistortModifier modifier3 = uberShaderComponent.GetModifier<DistortModifier>();
			return (!(modifier3 != null)) ? null : modifier3.DistortTexture;
		}
		case UberTextureSettingsAnimator.TextureName.DistortionMask:
		{
			DistortModifier modifier4 = uberShaderComponent.GetModifier<DistortModifier>();
			return (!(modifier4 != null)) ? null : modifier4.DistortMaskTexture;
		}
		case UberTextureSettingsAnimator.TextureName.DistortionExtra:
		{
			DistortExtraModifier modifier5 = uberShaderComponent.GetModifier<DistortExtraModifier>();
			return (!(modifier5 != null)) ? null : modifier5.DistortTexture;
		}
		case UberTextureSettingsAnimator.TextureName.DistortionExtraMask:
		{
			DistortExtraModifier modifier6 = uberShaderComponent.GetModifier<DistortExtraModifier>();
			return (!(modifier6 != null)) ? null : modifier6.DistortMaskTexture;
		}
		case UberTextureSettingsAnimator.TextureName.MultiplyLayer:
		{
			MultiplyLayerModifier modifier7 = uberShaderComponent.GetModifier<MultiplyLayerModifier>();
			return (!(modifier7 != null)) ? null : modifier7.MultiplyLayerTexture;
		}
		case UberTextureSettingsAnimator.TextureName.MultiplyLayerMask:
		{
			MultiplyLayerModifier modifier8 = uberShaderComponent.GetModifier<MultiplyLayerModifier>();
			return (!(modifier8 != null)) ? null : modifier8.MultiplyLayerMaskTexture;
		}
		case UberTextureSettingsAnimator.TextureName.MultiplyLayerExtra:
		{
			MultiplyLayerExtraModifier modifier9 = uberShaderComponent.GetModifier<MultiplyLayerExtraModifier>();
			return (!(modifier9 != null)) ? null : modifier9.MultiplyLayerTexture;
		}
		case UberTextureSettingsAnimator.TextureName.MultiplyLayerExtraMask:
		{
			MultiplyLayerExtraModifier modifier10 = uberShaderComponent.GetModifier<MultiplyLayerExtraModifier>();
			return (!(modifier10 != null)) ? null : modifier10.MultiplyLayerMaskTexture;
		}
		case UberTextureSettingsAnimator.TextureName.MultiplyLayerThird:
		{
			MultiplyLayerThirdModifier modifier11 = uberShaderComponent.GetModifier<MultiplyLayerThirdModifier>();
			return (!(modifier11 != null)) ? null : modifier11.MultiplyLayerTexture;
		}
		case UberTextureSettingsAnimator.TextureName.MultiplyLayerThirdMask:
		{
			MultiplyLayerThirdModifier modifier12 = uberShaderComponent.GetModifier<MultiplyLayerThirdModifier>();
			return (!(modifier12 != null)) ? null : modifier12.MultiplyLayerMaskTexture;
		}
		case UberTextureSettingsAnimator.TextureName.AdditiveLayer:
		{
			AdditiveLayerModifier modifier13 = uberShaderComponent.GetModifier<AdditiveLayerModifier>();
			return (!(modifier13 != null)) ? null : modifier13.AdditiveLayerTexture;
		}
		case UberTextureSettingsAnimator.TextureName.AdditiveLayerMask:
		{
			AdditiveLayerModifier modifier14 = uberShaderComponent.GetModifier<AdditiveLayerModifier>();
			return (!(modifier14 != null)) ? null : modifier14.AdditiveLayerMaskTexture;
		}
		default:
			return null;
		}
	}

	// Token: 0x06002D0F RID: 11535 RVA: 0x000C1050 File Offset: 0x000BF250
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (this.Relative)
		{
			if (this.UseOffset)
			{
				this.CurrentTextureOffset = this.m_originalOffset + new Vector2(this.OffsetX.Evaluate(value), this.OffsetY.Evaluate(value));
			}
			if (this.UseScale)
			{
				this.CurrentTextureScale = Vector2.Scale(this.m_originalScale, new Vector2(this.ScaleX.Evaluate(value), this.ScaleY.Evaluate(value)));
			}
			if (this.UseRotation)
			{
				this.CurrentTextureRotation = this.m_originalRotation + this.Rotation.Evaluate(value);
			}
		}
		else
		{
			if (this.UseOffset)
			{
				this.CurrentTextureOffset = new Vector2(this.OffsetX.Evaluate(value), this.OffsetY.Evaluate(value));
			}
			if (this.UseScale)
			{
				this.CurrentTextureScale = new Vector2(this.ScaleX.Evaluate(value), this.ScaleY.Evaluate(value));
			}
			if (this.UseRotation)
			{
				this.CurrentTextureRotation = this.Rotation.Evaluate(value);
			}
		}
	}

	// Token: 0x17000738 RID: 1848
	// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000C1188 File Offset: 0x000BF388
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(Mathf.Max(Mathf.Max(Mathf.Max(this.OffsetX.CurveDuration(), this.OffsetY.CurveDuration()), Mathf.Max(this.ScaleX.CurveDuration(), this.ScaleY.CurveDuration())), this.Rotation.CurveDuration()));
		}
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x000C11E8 File Offset: 0x000BF3E8
	public override void RestoreToOriginalState()
	{
		this.CurrentTextureOffset = this.m_originalOffset;
		this.CurrentTextureScale = this.m_originalScale;
		this.CurrentTextureRotation = this.m_originalRotation;
	}

	// Token: 0x17000739 RID: 1849
	// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000C121C File Offset: 0x000BF41C
	public override bool IsLooping
	{
		get
		{
			return this.OffsetX.postWrapMode != WrapMode.Once || this.OffsetY.postWrapMode != WrapMode.Once || this.ScaleX.postWrapMode != WrapMode.Once || this.ScaleY.postWrapMode != WrapMode.Once || this.Rotation.postWrapMode != WrapMode.Once;
		}
	}

	// Token: 0x0400289D RID: 10397
	public bool UseOffset = true;

	// Token: 0x0400289E RID: 10398
	public bool UseScale = true;

	// Token: 0x0400289F RID: 10399
	public bool UseRotation = true;

	// Token: 0x040028A0 RID: 10400
	public bool Relative = true;

	// Token: 0x040028A1 RID: 10401
	public AnimationCurve OffsetX = AnimationCurve.Linear(0f, 0f, 1f, 0f);

	// Token: 0x040028A2 RID: 10402
	public AnimationCurve OffsetY = AnimationCurve.Linear(0f, 0f, 1f, 0f);

	// Token: 0x040028A3 RID: 10403
	public AnimationCurve ScaleX = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x040028A4 RID: 10404
	public AnimationCurve ScaleY = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x040028A5 RID: 10405
	public AnimationCurve Rotation = AnimationCurve.Linear(0f, 0f, 0f, 0f);

	// Token: 0x040028A6 RID: 10406
	private Vector2 m_originalOffset;

	// Token: 0x040028A7 RID: 10407
	private Vector2 m_originalScale;

	// Token: 0x040028A8 RID: 10408
	private float m_originalRotation;

	// Token: 0x040028A9 RID: 10409
	public UberTextureSettingsAnimator.TextureName TextureTarget;

	// Token: 0x040028AA RID: 10410
	private static string[] s_unityTextureProperties = new string[]
	{
		"_MainTex",
		"_MaskTexture",
		"_MaskTextureExtra",
		"_DistortionTex",
		"_DistortionMaskTex",
		"_DistortionExtraTex",
		"_DistortionExtraMaskTex",
		"_MultiplyLayerTex",
		"_MultiplyLayerMaskTex",
		"_MultiplyLayerExtraTex",
		"_MultiplyLayerExtraMaskTex",
		"_MultiplyLayerThirdTex",
		"_MultiplyLayerThirdMaskTex",
		"_AdditiveLayerTex",
		"_AdditiveLayerMaskTex",
		"_AdditiveLayerExtraTex",
		"_AdditiveLayerExtraMaskTex"
	};

	// Token: 0x040028AB RID: 10411
	private static string[] s_unityTexturePropertiesSt = new string[]
	{
		"_MainTex_US_ST",
		"_MaskTexture_US_ST",
		"_MaskTextureExtra_US_ST",
		"_DistortionTex_US_ST",
		"_DistortionMaskTex_US_ST",
		"_DistortionExtraTex_US_ST",
		"_DistortionExtraMaskTex_US_ST",
		"_MultiplyLayerTex_US_ST",
		"_MultiplyLayerMaskTex_US_ST",
		"_MultiplyLayerExtraTex_US_ST",
		"_MultiplyLayerExtraMaskTex_US_ST",
		"_MultiplyLayerThirdTex_US_ST",
		"_MultiplyLayerThirdMaskTex_US_ST",
		"_AdditiveLayerTex_US_ST",
		"_AdditiveLayerMaskTex_US_ST",
		"_AdditiveLayerExtraTex_US_ST",
		"_AdditiveLayerExtraMaskTex_US_ST"
	};

	// Token: 0x02000794 RID: 1940
	public enum TextureName
	{
		// Token: 0x040028AD RID: 10413
		MainTexture,
		// Token: 0x040028AE RID: 10414
		MaskTexture,
		// Token: 0x040028AF RID: 10415
		MaskTextureExtra,
		// Token: 0x040028B0 RID: 10416
		Distortion,
		// Token: 0x040028B1 RID: 10417
		DistortionMask,
		// Token: 0x040028B2 RID: 10418
		DistortionExtra,
		// Token: 0x040028B3 RID: 10419
		DistortionExtraMask,
		// Token: 0x040028B4 RID: 10420
		MultiplyLayer,
		// Token: 0x040028B5 RID: 10421
		MultiplyLayerMask,
		// Token: 0x040028B6 RID: 10422
		MultiplyLayerExtra,
		// Token: 0x040028B7 RID: 10423
		MultiplyLayerExtraMask,
		// Token: 0x040028B8 RID: 10424
		MultiplyLayerThird,
		// Token: 0x040028B9 RID: 10425
		MultiplyLayerThirdMask,
		// Token: 0x040028BA RID: 10426
		AdditiveLayer,
		// Token: 0x040028BB RID: 10427
		AdditiveLayerMask,
		// Token: 0x040028BC RID: 10428
		AdditiveLayerExtra,
		// Token: 0x040028BD RID: 10429
		AdditiveLayerExtraMask
	}
}
