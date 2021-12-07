using System;
using UnityEngine;

// Token: 0x0200079D RID: 1949
[Serializable]
public class UberShaderMainTexture : UberShaderTextureBase
{
	// Token: 0x06002D3D RID: 11581 RVA: 0x000C19AE File Offset: 0x000BFBAE
	public override void BindProperties()
	{
	}

	// Token: 0x06002D3E RID: 11582 RVA: 0x000C19B0 File Offset: 0x000BFBB0
	public IUberAtlasExternal GetExternalAtlasProvider()
	{
		Component[] components = this.AttachedBlock.GetComponents<Component>();
		foreach (Component component in components)
		{
			if (component is IUberAtlasExternal)
			{
				if ((component as IUberAtlasExternal).DoesProvideAtlas())
				{
					return component as IUberAtlasExternal;
				}
			}
		}
		return null;
	}

	// Token: 0x1700073F RID: 1855
	// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000C1A0B File Offset: 0x000BFC0B
	public UberShaderAtlasTexture AtlasTexture
	{
		get
		{
			return this.m_texture;
		}
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x000C1A14 File Offset: 0x000BFC14
	public override void Set(string bindName, UberShaderBlock attachedBlock)
	{
		base.Set(bindName, attachedBlock);
		if (UberShaderMainTexture.s_depthFlipScreen == 0)
		{
			UberShaderMainTexture.s_depthFlipScreen = Shader.PropertyToID("_DepthFlipScreen");
		}
		if (UberShaderMainTexture.s_bindAtlasUvId == 0)
		{
			UberShaderMainTexture.s_bindAtlasUvId = Shader.PropertyToID(this.BindName + "_US_ATLAS");
		}
		if (UberShaderMainTexture.s_bindAtlasScaleId == 0)
		{
			UberShaderMainTexture.s_bindAtlasScaleId = Shader.PropertyToID(this.BindName + "_US_ATLAS_ST");
		}
		if (this.AttachedBlock.Component == null)
		{
			Debug.Log("Component could not be loaded. Delete this object, and undo to fix", attachedBlock.gameObject);
		}
		UberShaderComponent component = attachedBlock.Component;
		this.AtlasTexture.AttachedComponent = component;
	}

	// Token: 0x040028D8 RID: 10456
	[SerializeField]
	private UberShaderAtlasTexture m_texture = new UberShaderAtlasTexture();

	// Token: 0x040028D9 RID: 10457
	private Texture m_currentBindTexture;

	// Token: 0x040028DA RID: 10458
	private static int s_bindAtlasUvId;

	// Token: 0x040028DB RID: 10459
	private static int s_bindAtlasScaleId;

	// Token: 0x040028DC RID: 10460
	private static int s_depthFlipScreen;
}
