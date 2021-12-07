using System;
using UnityEngine;

// Token: 0x020007CF RID: 1999
[Serializable]
public abstract class UberShaderProperty
{
	// Token: 0x17000758 RID: 1880
	// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000C3778 File Offset: 0x000C1978
	protected Material BindMaterial
	{
		get
		{
			if (this.AttachedBlock == null)
			{
				Debug.LogError("Not set");
				return null;
			}
			if (this.AttachedBlock.GetComponent<Renderer>() == null)
			{
				return null;
			}
			if (!Application.isPlaying)
			{
				return this.AttachedBlock.GetComponent<Renderer>().sharedMaterial;
			}
			return this.AttachedBlock.GetComponent<Renderer>().material;
		}
	}

	// Token: 0x06002DDB RID: 11739
	public abstract void BindProperties();

	// Token: 0x06002DDC RID: 11740 RVA: 0x000C37E5 File Offset: 0x000C19E5
	public virtual void Set(string bindName, UberShaderBlock attachedBlock)
	{
		this.AttachedBlock = attachedBlock;
		this.BindName = bindName;
		this.MainBindId = Shader.PropertyToID(bindName);
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x000C3804 File Offset: 0x000C1A04
	protected void BindTexture(int nameId, Texture texture)
	{
		if (this.BindMaterial.HasProperty(nameId) && this.BindMaterial.GetTexture(nameId) != texture)
		{
			this.BindMaterial.SetTexture(nameId, texture);
		}
	}

	// Token: 0x06002DDE RID: 11742 RVA: 0x000C3848 File Offset: 0x000C1A48
	protected void BindColor(int nameId, Color color)
	{
		if (this.BindMaterial == null)
		{
			return;
		}
		if (this.BindMaterial.HasProperty(nameId) && this.BindMaterial.GetColor(nameId) != color)
		{
			this.BindMaterial.SetColor(nameId, color);
		}
	}

	// Token: 0x06002DDF RID: 11743 RVA: 0x000C389C File Offset: 0x000C1A9C
	protected void BindVector(int nameId, Vector4 vector)
	{
		if (this.BindMaterial == null)
		{
			return;
		}
		if (this.BindMaterial.HasProperty(nameId) && this.BindMaterial.GetVector(nameId) != vector)
		{
			this.BindMaterial.SetVector(nameId, vector);
		}
	}

	// Token: 0x06002DE0 RID: 11744 RVA: 0x000C38F0 File Offset: 0x000C1AF0
	protected void BindVector(string nameId, Vector4 vector)
	{
		if (this.BindMaterial == null)
		{
			return;
		}
		if (this.BindMaterial.HasProperty(nameId) && this.BindMaterial.GetVector(nameId) != vector)
		{
			this.BindMaterial.SetVector(nameId, vector);
		}
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x000C3944 File Offset: 0x000C1B44
	protected void BindFloat(int nameId, float val)
	{
		if (this.BindMaterial == null)
		{
			return;
		}
		if (this.BindMaterial.HasProperty(nameId) && this.BindMaterial.GetFloat(nameId) != val)
		{
			this.BindMaterial.SetFloat(nameId, val);
		}
	}

	// Token: 0x04002970 RID: 10608
	[NonSerialized]
	public UberShaderBlock AttachedBlock;

	// Token: 0x04002971 RID: 10609
	protected int MainBindId;

	// Token: 0x04002972 RID: 10610
	[NonSerialized]
	public string BindName;
}
