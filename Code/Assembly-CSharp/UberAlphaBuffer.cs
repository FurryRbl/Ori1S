using System;
using UnityEngine;

// Token: 0x02000849 RID: 2121
public class UberAlphaBuffer
{
	// Token: 0x06003042 RID: 12354 RVA: 0x000CC540 File Offset: 0x000CA740
	public void OnRenderImage(RenderTexture source, Material blitMat)
	{
		if (this.m_alphaBuffer == null || this.m_alphaBuffer.width != Screen.width / 2 || this.m_alphaBuffer.height != Screen.height / 2)
		{
			this.GenerateAlphaBuffer();
		}
		Graphics.Blit(source, this.m_alphaBuffer, blitMat, 2);
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x000CC5A0 File Offset: 0x000CA7A0
	public void GenerateAlphaBuffer()
	{
		if (this.m_alphaBuffer != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_alphaBuffer);
		}
		this.m_alphaBuffer = new RenderTexture(Screen.width / 2, Screen.height / 2, 0, RenderTextureFormat.ARGB32);
		this.m_alphaBuffer.name = "alphaBuffer";
	}

	// Token: 0x06003044 RID: 12356 RVA: 0x000CC5F4 File Offset: 0x000CA7F4
	public void SetCurrentAlphaGrab()
	{
		if (this.m_alphaBuffer)
		{
			this.m_alphaBuffer.SetGlobalShaderProperty("_AlphaGrabTex");
		}
	}

	// Token: 0x06003045 RID: 12357 RVA: 0x000CC624 File Offset: 0x000CA824
	public void Destroy()
	{
		if (this.m_alphaBuffer)
		{
			UnityEngine.Object.DestroyImmediate(this.m_alphaBuffer);
			this.m_alphaBuffer = null;
		}
	}

	// Token: 0x04002B6D RID: 11117
	private const int c_alphaDownSample = 2;

	// Token: 0x04002B6E RID: 11118
	private RenderTexture m_alphaBuffer;
}
