using System;
using UnityEngine;

// Token: 0x020006EA RID: 1770
public abstract class UberShaderBlock : MonoBehaviour
{
	// Token: 0x170006BA RID: 1722
	// (get) Token: 0x06002A4D RID: 10829 RVA: 0x000B5D4D File Offset: 0x000B3F4D
	public double RandomOffset
	{
		get
		{
			if (this.RawRandomOffset <= Mathf.Epsilon)
			{
				this.RandomizeOffset();
			}
			return -0.01 + 0.02 * (double)this.RawRandomOffset;
		}
	}

	// Token: 0x170006BB RID: 1723
	// (get) Token: 0x06002A4E RID: 10830 RVA: 0x000B5D80 File Offset: 0x000B3F80
	protected MeshFilter Filter
	{
		get
		{
			if (this.m_filter == null)
			{
				this.m_filter = base.gameObject.GetComponent<MeshFilter>();
			}
			return this.m_filter;
		}
	}

	// Token: 0x170006BC RID: 1724
	// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000B5DB8 File Offset: 0x000B3FB8
	public bool HasCustomMesh
	{
		get
		{
			return base.gameObject.GetComponent<TextMesh>() || (!(this.Filter == null) && ((this.Filter.sharedMesh != null && this.Filter.sharedMesh.name.Contains("Temp")) || (this.Filter.sharedMesh != null && !this.Filter.sharedMesh.name.Contains("UberShaderCustomMesh") && this.Filter.sharedMesh.name != "plane" && this.Filter.sharedMesh.name != "UberShaderMesh")));
		}
	}

	// Token: 0x170006BD RID: 1725
	// (get) Token: 0x06002A50 RID: 10832 RVA: 0x000B5E97 File Offset: 0x000B4097
	public UberShaderComponent Component
	{
		get
		{
			if (this.m_component == null)
			{
				this.m_component = base.GetComponent<UberShaderComponent>();
			}
			return this.m_component;
		}
	}

	// Token: 0x170006BE RID: 1726
	// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000B5EBC File Offset: 0x000B40BC
	// (set) Token: 0x06002A52 RID: 10834 RVA: 0x000B5EE9 File Offset: 0x000B40E9
	public bool UseFog
	{
		get
		{
			return this.m_useFog && this.BlendMode == BlendModeType.AlphaBlend && base.GetType() == typeof(UberShaderBlockTextured);
		}
		set
		{
			this.m_useFog = value;
		}
	}

	// Token: 0x170006BF RID: 1727
	// (get) Token: 0x06002A53 RID: 10835 RVA: 0x000B5EF2 File Offset: 0x000B40F2
	public Renderer Renderer
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

	// Token: 0x170006C0 RID: 1728
	// (get) Token: 0x06002A54 RID: 10836 RVA: 0x000B5F18 File Offset: 0x000B4118
	public bool IsRotated
	{
		get
		{
			float x = base.transform.eulerAngles.x;
			return Mathf.Abs(Mathf.DeltaAngle(x, 0f)) > 60f && this.Renderer is MeshRenderer;
		}
	}

	// Token: 0x170006C1 RID: 1729
	// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000B5F64 File Offset: 0x000B4164
	public Material Material
	{
		get
		{
			if (!this.Renderer)
			{
				return null;
			}
			return this.Renderer.sharedMaterial;
		}
	}

	// Token: 0x06002A56 RID: 10838
	public abstract void SetProperties();

	// Token: 0x06002A57 RID: 10839 RVA: 0x000B5F8E File Offset: 0x000B418E
	public virtual void UberShaderEditorUpdate()
	{
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x000B5F90 File Offset: 0x000B4190
	public void RandomizeOffset()
	{
		this.RawRandomOffset = UnityEngine.Random.value;
	}

	// Token: 0x040025A7 RID: 9639
	public BlendModeType BlendMode;

	// Token: 0x040025A8 RID: 9640
	public bool WriteRGB = true;

	// Token: 0x040025A9 RID: 9641
	public bool WriteA;

	// Token: 0x040025AA RID: 9642
	[Range(-25f, 25f)]
	public float OffsetPositionZ;

	// Token: 0x040025AB RID: 9643
	public float RawRandomOffset;

	// Token: 0x040025AC RID: 9644
	public TimeMode TimeMode;

	// Token: 0x040025AD RID: 9645
	public bool IsAlphaMasked;

	// Token: 0x040025AE RID: 9646
	public bool IsAlphaInverse;

	// Token: 0x040025AF RID: 9647
	public bool BackSideOnly;

	// Token: 0x040025B0 RID: 9648
	public bool DoReflection;

	// Token: 0x040025B1 RID: 9649
	public bool DoAlphaMaskedOrder;

	// Token: 0x040025B2 RID: 9650
	[SerializeField]
	private bool m_useFog = true;

	// Token: 0x040025B3 RID: 9651
	private MeshFilter m_filter;

	// Token: 0x040025B4 RID: 9652
	private UberShaderComponent m_component;

	// Token: 0x040025B5 RID: 9653
	private Renderer m_renderer;
}
