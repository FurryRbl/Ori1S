using System;
using UnityEngine;

// Token: 0x02000491 RID: 1169
[Serializable]
public class OverridableTextureProperty
{
	// Token: 0x06001FB7 RID: 8119 RVA: 0x0008B494 File Offset: 0x00089694
	public OverridableTextureProperty()
	{
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x0008B4A8 File Offset: 0x000896A8
	public OverridableTextureProperty(OverridableTextureProperty textureProperty)
	{
		this.Override = textureProperty.Override;
		this.Name = textureProperty.Name;
		this.Texture = textureProperty.Texture;
		this.Offset = textureProperty.Offset;
		this.Scale = textureProperty.Scale;
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x0008B504 File Offset: 0x00089704
	public void Apply(OverridableTextureProperty textureProperty)
	{
		this.Texture = textureProperty.Texture;
		this.Offset = textureProperty.Offset;
		this.Scale = textureProperty.Scale;
	}

	// Token: 0x04001B51 RID: 6993
	public bool Override;

	// Token: 0x04001B52 RID: 6994
	public string Name;

	// Token: 0x04001B53 RID: 6995
	public Texture Texture;

	// Token: 0x04001B54 RID: 6996
	public Vector2 Offset;

	// Token: 0x04001B55 RID: 6997
	public Vector2 Scale = Vector2.one;
}
