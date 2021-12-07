using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public static class ShaderProperties
{
	// Token: 0x0600088B RID: 2187 RVA: 0x00024B6C File Offset: 0x00022D6C
	static ShaderProperties()
	{
		ShaderProperties.ColorMask = Shader.PropertyToID("_UberShaderColorMask");
	}

	// Token: 0x040006C0 RID: 1728
	public static int MainTexture = Shader.PropertyToID("_MainTex");

	// Token: 0x040006C1 RID: 1729
	public static int Color = Shader.PropertyToID("_Color");

	// Token: 0x040006C2 RID: 1730
	public static int TintColor = Shader.PropertyToID("_TintColor");

	// Token: 0x040006C3 RID: 1731
	public static int AdditiveLayerColor = Shader.PropertyToID("_AdditiveLayerColor");

	// Token: 0x040006C4 RID: 1732
	public static int AdditiveLayerDistortColor = Shader.PropertyToID("_AdditiveLayerDistortColor");

	// Token: 0x040006C5 RID: 1733
	public static int AdditiveLayerExtraColor = Shader.PropertyToID("_AdditiveLayerExtraColor");

	// Token: 0x040006C6 RID: 1734
	public static int MultiplyLayerColor = Shader.PropertyToID("_MultiplyLayerColor");

	// Token: 0x040006C7 RID: 1735
	public static int MultiplyLayerExtraColor = Shader.PropertyToID("_MultiplyLayerExtraColor");

	// Token: 0x040006C8 RID: 1736
	public static int MultiplyLayerThirdColor = Shader.PropertyToID("_MultiplyLayerThirdColor");

	// Token: 0x040006C9 RID: 1737
	public static int MultiplyLayerDistortColor = Shader.PropertyToID("_MultiplyLayerDistortColor");

	// Token: 0x040006CA RID: 1738
	public static int Screen = Shader.PropertyToID("_Screen");

	// Token: 0x040006CB RID: 1739
	public static int ScreenMask = Shader.PropertyToID("_ScreenMask");

	// Token: 0x040006CC RID: 1740
	public static int MainTexUSAtlas = Shader.PropertyToID("_MainTex_US_ATLAS");

	// Token: 0x040006CD RID: 1741
	public static int DepthFlipScreen = Shader.PropertyToID("_DepthFlipScreen");

	// Token: 0x040006CE RID: 1742
	public static int MapMaskTextureA = Shader.PropertyToID("_MapMaskTextureA");

	// Token: 0x040006CF RID: 1743
	public static int MapMaskTextureB = Shader.PropertyToID("_MapMaskTextureB");

	// Token: 0x040006D0 RID: 1744
	public static int MapFade = Shader.PropertyToID("_MapFade");

	// Token: 0x040006D1 RID: 1745
	public static int MaskTex = Shader.PropertyToID("_MaskTex");

	// Token: 0x040006D2 RID: 1746
	public static int BlurSize = Shader.PropertyToID("BlurSize");

	// Token: 0x040006D3 RID: 1747
	public static int TextureScalingAndOffset = Shader.PropertyToID("TextureScalingAndOffset");

	// Token: 0x040006D4 RID: 1748
	public static int Fade = Shader.PropertyToID("_Fade");

	// Token: 0x040006D5 RID: 1749
	public static int ColorMask;

	// Token: 0x040006D6 RID: 1750
	public static int RotationCurveSettings = Shader.PropertyToID("_RotationCurve_AnimationSettings");
}
