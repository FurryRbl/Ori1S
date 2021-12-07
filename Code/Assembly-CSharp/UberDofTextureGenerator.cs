using System;
using UnityEngine;

// Token: 0x020007B2 RID: 1970
public static class UberDofTextureGenerator
{
	// Token: 0x06002D8B RID: 11659 RVA: 0x000C2748 File Offset: 0x000C0948
	private static Material GetBlurMaterial()
	{
		if (UberDofTextureGenerator.s_blurMaterial == null)
		{
			Texture2D texture2D = new Texture2D(128, 1, TextureFormat.Alpha8, false)
			{
				hideFlags = HideFlags.HideAndDontSave,
				wrapMode = TextureWrapMode.Clamp
			};
			Color[] array = new Color[128];
			for (int i = 0; i < 128; i++)
			{
				float num = (float)i / 127f * 4f;
				array[i].a = 0.2389f * Mathf.Exp(-0.5f * num * num) - 0.005f;
			}
			texture2D.SetPixels(array);
			texture2D.Apply();
			UberDofTextureGenerator.s_blurMaterial = new Material(Shader.Find("Hidden/DOFBlur"))
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			UberDofTextureGenerator.s_blurMaterial.SetTexture("GaussianTexture", texture2D);
		}
		return UberDofTextureGenerator.s_blurMaterial;
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x000C2824 File Offset: 0x000C0A24
	public static Texture CreatePreviewDofTexture(Texture2D originalTexture, Vector2 blurSize)
	{
		if (originalTexture == null)
		{
			return null;
		}
		if (blurSize.magnitude < Mathf.Epsilon)
		{
			return originalTexture;
		}
		int num = originalTexture.width;
		int num2 = originalTexture.height;
		if (blurSize.x > 0.04f)
		{
			num /= 2;
		}
		if (blurSize.y > 0.04f)
		{
			num2 /= 2;
		}
		num /= 2;
		num2 /= 2;
		return UberDofTextureGenerator.CreateDofTexture(originalTexture, blurSize, num, num2);
	}

	// Token: 0x06002D8D RID: 11661 RVA: 0x000C289C File Offset: 0x000C0A9C
	public static Texture CreateDofTexture(Texture2D originalTexture, Vector2 blurSize)
	{
		if (originalTexture == null)
		{
			return null;
		}
		if (blurSize.magnitude < Mathf.Epsilon)
		{
			return originalTexture;
		}
		int width = originalTexture.width;
		int height = originalTexture.height;
		return UberDofTextureGenerator.CreateDofTexture(originalTexture, blurSize, width, height);
	}

	// Token: 0x06002D8E RID: 11662 RVA: 0x000C28E4 File Offset: 0x000C0AE4
	public static Texture CreateDofTextureNearestPot(Texture2D originalTexture, Vector2 blurSize, Vector2 maxScreenSize)
	{
		if (originalTexture == null)
		{
			return null;
		}
		if (blurSize.magnitude < Mathf.Epsilon)
		{
			return originalTexture;
		}
		int num = Mathf.Min((int)maxScreenSize.x, originalTexture.width);
		int num2 = Mathf.Min((int)maxScreenSize.y, originalTexture.height);
		int num3 = Mathf.ClosestPowerOfTwo(num);
		int num4 = Mathf.ClosestPowerOfTwo(num2);
		if ((float)Mathf.Abs(num3 - num) > 0.1f * (float)num || (float)Mathf.Abs(num4 - num2) > 0.1f * (float)num2)
		{
			num3 = Mathf.NextPowerOfTwo(num);
			num4 = Mathf.NextPowerOfTwo(num2);
		}
		num3 = Mathf.Max(num3, 32);
		num4 = Mathf.Max(num4, 32);
		return UberDofTextureGenerator.CreateDofTexture(originalTexture, blurSize, num3, num4);
	}

	// Token: 0x06002D8F RID: 11663 RVA: 0x000C29A0 File Offset: 0x000C0BA0
	public static Texture CreateDofTexture(Texture2D originalTexture, Vector2 blurSize, int textureWidth, int textureHeight)
	{
		RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 0, RenderTextureFormat.ARGB32);
		renderTexture.name = originalTexture.name + "Dof";
		renderTexture.Create();
		renderTexture.anisoLevel = 0;
		renderTexture.wrapMode = originalTexture.wrapMode;
		RenderTexture.active = renderTexture;
		GL.Clear(false, true, Color.black);
		RenderTexture.active = null;
		RenderTexture temporary = RenderTexture.GetTemporary(textureWidth, textureHeight, 0, RenderTextureFormat.ARGB32);
		Material blurMaterial = UberDofTextureGenerator.GetBlurMaterial();
		blurMaterial.SetFloat("BlurWidth", blurSize.x);
		blurMaterial.SetVector("BlurDirection", new Vector4(1f, 0f, 0f, 0f));
		TextureWrapMode wrapMode = originalTexture.wrapMode;
		originalTexture.wrapMode = TextureWrapMode.Clamp;
		blurMaterial.SetVector("TextureScalingAndOffset", new Vector4(1f + blurSize.x, 1f + blurSize.y, -blurSize.x / 2f, -blurSize.y / 2f));
		RenderTexture.active = null;
		Graphics.Blit(originalTexture, temporary, blurMaterial);
		RenderTexture.active = null;
		originalTexture.wrapMode = wrapMode;
		blurMaterial.SetFloat("BlurWidth", blurSize.y);
		blurMaterial.SetVector("BlurDirection", new Vector4(0f, 1f, 0f, 0f));
		blurMaterial.SetVector("TextureScalingAndOffset", new Vector4(1f, 1f, 0f, 0f));
		RenderTexture.active = null;
		Graphics.Blit(temporary, renderTexture, blurMaterial);
		RenderTexture.active = null;
		RenderTexture.ReleaseTemporary(temporary);
		return renderTexture;
	}

	// Token: 0x04002909 RID: 10505
	private static Material s_blurMaterial;
}
