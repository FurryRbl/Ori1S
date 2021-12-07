using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020002B8 RID: 696
	public enum CameraEvent
	{
		// Token: 0x04000B1A RID: 2842
		BeforeDepthTexture,
		// Token: 0x04000B1B RID: 2843
		AfterDepthTexture,
		// Token: 0x04000B1C RID: 2844
		BeforeDepthNormalsTexture,
		// Token: 0x04000B1D RID: 2845
		AfterDepthNormalsTexture,
		// Token: 0x04000B1E RID: 2846
		BeforeGBuffer,
		// Token: 0x04000B1F RID: 2847
		AfterGBuffer,
		// Token: 0x04000B20 RID: 2848
		BeforeLighting,
		// Token: 0x04000B21 RID: 2849
		AfterLighting,
		// Token: 0x04000B22 RID: 2850
		BeforeFinalPass,
		// Token: 0x04000B23 RID: 2851
		AfterFinalPass,
		// Token: 0x04000B24 RID: 2852
		BeforeForwardOpaque,
		// Token: 0x04000B25 RID: 2853
		AfterForwardOpaque,
		// Token: 0x04000B26 RID: 2854
		BeforeImageEffectsOpaque,
		// Token: 0x04000B27 RID: 2855
		AfterImageEffectsOpaque,
		// Token: 0x04000B28 RID: 2856
		BeforeSkybox,
		// Token: 0x04000B29 RID: 2857
		AfterSkybox,
		// Token: 0x04000B2A RID: 2858
		BeforeForwardAlpha,
		// Token: 0x04000B2B RID: 2859
		AfterForwardAlpha,
		// Token: 0x04000B2C RID: 2860
		BeforeImageEffects,
		// Token: 0x04000B2D RID: 2861
		AfterImageEffects,
		// Token: 0x04000B2E RID: 2862
		AfterEverything,
		// Token: 0x04000B2F RID: 2863
		BeforeReflections,
		// Token: 0x04000B30 RID: 2864
		AfterReflections
	}
}
