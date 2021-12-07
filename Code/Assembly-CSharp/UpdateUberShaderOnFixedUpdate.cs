using System;
using UnityEngine;

// Token: 0x02000845 RID: 2117
public class UpdateUberShaderOnFixedUpdate : MonoBehaviour
{
	// Token: 0x170007B9 RID: 1977
	// (get) Token: 0x06003033 RID: 12339 RVA: 0x000CBFA0 File Offset: 0x000CA1A0
	public UberShaderComponent AttachedUberShader
	{
		get
		{
			if (this.m_attachedUberShader == null)
			{
				this.m_attachedUberShader = base.GetComponent<UberShaderComponent>();
			}
			return this.m_attachedUberShader;
		}
	}

	// Token: 0x06003034 RID: 12340 RVA: 0x000CBFC8 File Offset: 0x000CA1C8
	private void FixedUpdate()
	{
		MultiplyLayerModifier modifier = this.AttachedUberShader.GetModifier<MultiplyLayerModifier>();
		if (modifier == null)
		{
			return;
		}
		if (modifier.MultiplyLayerTexture == null || modifier.MultiplyLayerMaskTexture == null)
		{
			return;
		}
		modifier.MultiplyLayerTexture.BindProperties();
		modifier.MultiplyLayerMaskTexture.BindProperties();
	}

	// Token: 0x04002B62 RID: 11106
	private UberShaderComponent m_attachedUberShader;
}
