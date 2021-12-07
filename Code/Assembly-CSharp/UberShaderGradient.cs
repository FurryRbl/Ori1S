using System;
using UnityEngine;

// Token: 0x0200083F RID: 2111
[Serializable]
public class UberShaderGradient : UberShaderProperty
{
	// Token: 0x170007B7 RID: 1975
	// (get) Token: 0x0600301F RID: 12319 RVA: 0x000CB9BF File Offset: 0x000C9BBF
	public Gradient Gradient
	{
		get
		{
			return this.m_gradient;
		}
	}

	// Token: 0x06003020 RID: 12320 RVA: 0x000CB9C7 File Offset: 0x000C9BC7
	public override void BindProperties()
	{
		this.CreateTexture();
		base.BindMaterial.SetTexture(this.MainBindId, this.m_texture);
	}

	// Token: 0x06003021 RID: 12321 RVA: 0x000CB9E6 File Offset: 0x000C9BE6
	private void CreateTexture()
	{
		UnityEngine.Object.DestroyImmediate(this.m_texture);
		this.m_texture = UberShaderCurveBake.BakeAnimationGradient(this.m_gradient, 64);
	}

	// Token: 0x06003022 RID: 12322 RVA: 0x000CBA06 File Offset: 0x000C9C06
	public override void Set(string bindName, UberShaderBlock attachedBlock)
	{
		base.Set(bindName, attachedBlock);
		this.CreateTexture();
		this.BindProperties();
	}

	// Token: 0x04002B55 RID: 11093
	[SerializeField]
	private Gradient m_gradient = new Gradient();

	// Token: 0x04002B56 RID: 11094
	private Texture2D m_texture;
}
