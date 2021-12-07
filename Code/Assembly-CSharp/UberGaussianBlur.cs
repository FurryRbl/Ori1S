using System;
using UnityEngine;

// Token: 0x0200084F RID: 2127
public static class UberGaussianBlur
{
	// Token: 0x0600304F RID: 12367 RVA: 0x000CCA3C File Offset: 0x000CAC3C
	public static void BlurPingPong(RenderTexture from, RenderTexture temp, int iterations, float blurSpread)
	{
		if (UberGaussianBlur.SeperableBlurMaterial == null)
		{
			Shader shader = Shader.Find("Hidden/SeparableBlur");
			UberGaussianBlur.SeperableBlurMaterial = new Material(shader);
		}
		float num = (float)temp.width / (float)temp.height;
		for (int i = 0; i < iterations; i++)
		{
			float num2 = (1f + (float)i * 0.5f) * blurSpread;
			UberGaussianBlur.SeperableBlurMaterial.SetVector(UberPostCacheIds.Offsets, new Vector4(0f, num2 * 0.0014285714f, 0f, 0f));
			Graphics.Blit(from, temp, UberGaussianBlur.SeperableBlurMaterial, 0);
			from.DiscardContents();
			UberGaussianBlur.SeperableBlurMaterial.SetVector(UberPostCacheIds.Offsets, new Vector4(num2 / num * 0.0014285714f, 0f, 0f, 0f));
			Graphics.Blit(temp, from, UberGaussianBlur.SeperableBlurMaterial, 0);
			temp.DiscardContents();
		}
	}

	// Token: 0x04002B93 RID: 11155
	private static Material SeperableBlurMaterial;
}
