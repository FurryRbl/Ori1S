using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000669 RID: 1641
public class TextureTilingAdjuster : MonoBehaviour
{
	// Token: 0x06002801 RID: 10241 RVA: 0x000ADC08 File Offset: 0x000ABE08
	private void Start()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		Material sharedMaterial = this.m_renderer.sharedMaterial;
		foreach (string text in TextureTilingAdjuster.s_supportedTextureProperties)
		{
			if (sharedMaterial.HasProperty(text))
			{
				TextureTilingAdjuster.AdjustedTexture item;
				item.TexturePropertyName = text;
				item.OriginaTexturelScale = sharedMaterial.GetTextureScale(text);
				this.m_adjustedTextures.Add(item);
			}
		}
	}

	// Token: 0x06002802 RID: 10242 RVA: 0x000ADC80 File Offset: 0x000ABE80
	private void FixedUpdate()
	{
		this.m_frame++;
		if (this.m_frame % 2 == 0 && this.ScaleMultiplier != this.m_prevScale)
		{
			Material sharedMaterial = this.m_renderer.sharedMaterial;
			for (int i = 0; i < this.m_adjustedTextures.Count; i++)
			{
				TextureTilingAdjuster.AdjustedTexture adjustedTexture = this.m_adjustedTextures[i];
				Vector2 originaTexturelScale = adjustedTexture.OriginaTexturelScale;
				originaTexturelScale.x *= this.ScaleMultiplier.x;
				originaTexturelScale.y *= this.ScaleMultiplier.y;
				sharedMaterial.SetTextureScale(adjustedTexture.TexturePropertyName, originaTexturelScale);
			}
			this.m_prevScale = this.ScaleMultiplier;
		}
	}

	// Token: 0x0400228D RID: 8845
	public Vector2 ScaleMultiplier = new Vector2(1f, 1f);

	// Token: 0x0400228E RID: 8846
	private static string[] s_supportedTextureProperties = new string[]
	{
		"_MainTex",
		"_DistortionTex"
	};

	// Token: 0x0400228F RID: 8847
	private List<TextureTilingAdjuster.AdjustedTexture> m_adjustedTextures = new List<TextureTilingAdjuster.AdjustedTexture>();

	// Token: 0x04002290 RID: 8848
	private Vector2 m_prevScale;

	// Token: 0x04002291 RID: 8849
	private Renderer m_renderer;

	// Token: 0x04002292 RID: 8850
	private int m_frame;

	// Token: 0x0200066A RID: 1642
	private struct AdjustedTexture
	{
		// Token: 0x04002293 RID: 8851
		public string TexturePropertyName;

		// Token: 0x04002294 RID: 8852
		public Vector2 OriginaTexturelScale;
	}
}
