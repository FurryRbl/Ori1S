using System;
using System.Text;
using UnityEngine;

// Token: 0x0200081D RID: 2077
[Serializable]
public class UberShaderAtlasTexture
{
	// Token: 0x06002FB0 RID: 12208 RVA: 0x000CA1A0 File Offset: 0x000C83A0
	public void UsePrefabAtlas()
	{
		this.m_usePrefabAtlas = true;
	}

	// Token: 0x04002AEA RID: 10986
	public string OriginalTextureGUID;

	// Token: 0x04002AEB RID: 10987
	[NonSerialized]
	public UberShaderComponent AttachedComponent;

	// Token: 0x04002AEC RID: 10988
	private string m_lastGUID;

	// Token: 0x04002AED RID: 10989
	private bool m_usePrefabAtlas;

	// Token: 0x04002AEE RID: 10990
	private bool m_setDofExplicit;

	// Token: 0x04002AEF RID: 10991
	private Vector2 m_explicitDof;

	// Token: 0x04002AF0 RID: 10992
	private static StringBuilder s_builder = new StringBuilder(128);

	// Token: 0x04002AF1 RID: 10993
	private Texture2D m_textureCache;

	// Token: 0x04002AF2 RID: 10994
	private bool m_hasExternal;

	// Token: 0x04002AF3 RID: 10995
	private double m_lastExternalCheck;
}
