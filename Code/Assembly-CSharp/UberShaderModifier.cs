using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006E9 RID: 1769
[ExecuteInEditMode]
public abstract class UberShaderModifier : MonoBehaviour, IStrippable
{
	// Token: 0x170006B4 RID: 1716
	// (get) Token: 0x06002A33 RID: 10803 RVA: 0x000B5998 File Offset: 0x000B3B98
	protected UberShaderBlock AttachedToShaderBlock
	{
		get
		{
			if (this.m_attachedToShaderBlock == null)
			{
				this.m_attachedToShaderBlock = base.GetComponent<UberShaderBlock>();
			}
			if (this.m_attachedToShaderBlock == null)
			{
				Debug.LogWarning("Cant find block :(");
				return null;
			}
			return this.m_attachedToShaderBlock;
		}
	}

	// Token: 0x170006B5 RID: 1717
	// (get) Token: 0x06002A34 RID: 10804 RVA: 0x000B59E8 File Offset: 0x000B3BE8
	public bool HasCageMesh
	{
		get
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			return component != null && component.sharedMesh != null && component.sharedMesh.name.Contains("UberShaderCustomMeshCage");
		}
	}

	// Token: 0x170006B6 RID: 1718
	// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000B5A36 File Offset: 0x000B3C36
	protected Renderer Renderer
	{
		get
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer;
		}
	}

	// Token: 0x170006B7 RID: 1719
	// (get) Token: 0x06002A36 RID: 10806 RVA: 0x000B5A5B File Offset: 0x000B3C5B
	protected MeshFilter Filter
	{
		get
		{
			if (this.m_filter == null)
			{
				this.m_filter = base.GetComponent<MeshFilter>();
			}
			return this.m_filter;
		}
	}

	// Token: 0x170006B8 RID: 1720
	// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000B5A80 File Offset: 0x000B3C80
	protected Material BindMaterial
	{
		get
		{
			return this.Renderer.sharedMaterial;
		}
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x000B5A90 File Offset: 0x000B3C90
	public static bool IsModifierSupported(UberShaderComponent comp, Type modifierType)
	{
		return !typeof(IAnimationVertex).IsAssignableFrom(modifierType) || comp.Block.Renderer is MeshRenderer;
	}

	// Token: 0x170006B9 RID: 1721
	// (get) Token: 0x06002A39 RID: 10809 RVA: 0x000B5AC8 File Offset: 0x000B3CC8
	public bool IsSupported
	{
		get
		{
			return UberShaderModifier.IsModifierSupported(this.AttachedToShaderBlock.Component, base.GetType());
		}
	}

	// Token: 0x06002A3A RID: 10810
	public abstract void SetProperties();

	// Token: 0x06002A3B RID: 10811 RVA: 0x000B5AEC File Offset: 0x000B3CEC
	protected float RangeRandom(float b, float mag)
	{
		float num = (UnityEngine.Random.value - 0.5f) * mag;
		b = Mathf.Sign(num) * b;
		return b + num;
	}

	// Token: 0x06002A3C RID: 10812 RVA: 0x000B5B14 File Offset: 0x000B3D14
	protected void RandomizeScrolling(UberShaderTexture tex)
	{
		if (tex.TextureScroll == Vector2.zero)
		{
			return;
		}
		Vector2 textureScroll = tex.TextureScroll;
		textureScroll.x *= UnityEngine.Random.Range(0.9f, 1.1f);
		textureScroll.y *= UnityEngine.Random.Range(0.9f, 1.1f);
		tex.TextureScroll = textureScroll;
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x000B5B7F File Offset: 0x000B3D7F
	public virtual void Randomize()
	{
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x000B5B81 File Offset: 0x000B3D81
	public virtual string[] GetRandomizeOptions()
	{
		return null;
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x000B5B84 File Offset: 0x000B3D84
	public virtual void UberShaderEditorUpdate()
	{
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x000B5B86 File Offset: 0x000B3D86
	public virtual void ApplyMultipliers(float strength, float speed)
	{
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x000B5B88 File Offset: 0x000B3D88
	public virtual float GetQuadExpandSize()
	{
		return 0f;
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x000B5B8F File Offset: 0x000B3D8F
	public virtual bool NeedsMipMap()
	{
		return false;
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x000B5B92 File Offset: 0x000B3D92
	public virtual bool DoStrip()
	{
		return true;
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x000B5B95 File Offset: 0x000B3D95
	public virtual IEnumerable<string> GetKeywordsForShader()
	{
		return null;
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x000B5B98 File Offset: 0x000B3D98
	public virtual IEnumerable<string> GetBaseVertexTextureNames()
	{
		yield break;
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x000B5BB4 File Offset: 0x000B3DB4
	public virtual string GetBaseShaderProperties()
	{
		return string.Empty;
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x000B5BBC File Offset: 0x000B3DBC
	protected string ShaderlabString(string bind, string type, string def)
	{
		if (bind == string.Empty)
		{
			Debug.Log("Something went wrong when generating shaderlab properties! Make sure you call Set() on all your uber shader properties");
		}
		return string.Concat(new string[]
		{
			bind,
			"(\"",
			this.NicifyVariableName(bind),
			"\", ",
			type,
			") = ",
			def,
			Environment.NewLine
		});
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x000B5C24 File Offset: 0x000B3E24
	private string NicifyVariableName(string name)
	{
		name = name.Replace("m_", string.Empty);
		if (name[0] == '_')
		{
			name = name.Substring(1);
		}
		else if (name[0] == '2' && name[1] == '_')
		{
			name = name.Substring(2);
		}
		else if (name[0] == 'k' && char.IsUpper(name[1]))
		{
			name = name.Substring(1);
		}
		string text = string.Empty;
		for (int i = 0; i < name.Length; i++)
		{
			if (i == 0)
			{
				text += name[i];
			}
			else if (char.IsUpper(name[i]))
			{
				text = text + " " + name[i];
			}
			else
			{
				text += name[i];
			}
		}
		return text;
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x000B5D2E File Offset: 0x000B3F2E
	public virtual bool RequiresNormals()
	{
		return false;
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x000B5D31 File Offset: 0x000B3F31
	public virtual bool RequiresVertexColor()
	{
		return false;
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x000B5D34 File Offset: 0x000B3F34
	public virtual bool DoesChangeShape()
	{
		return false;
	}

	// Token: 0x040025A3 RID: 9635
	public bool Enabled = true;

	// Token: 0x040025A4 RID: 9636
	private UberShaderBlock m_attachedToShaderBlock;

	// Token: 0x040025A5 RID: 9637
	private Renderer m_renderer;

	// Token: 0x040025A6 RID: 9638
	private MeshFilter m_filter;
}
