using System;
using UnityEngine;

// Token: 0x020007D0 RID: 2000
[Serializable]
public class UberShaderVector : UberShaderProperty
{
	// Token: 0x06002DE2 RID: 11746 RVA: 0x000C3994 File Offset: 0x000C1B94
	public UberShaderVector(float x, float y, float z, float w)
	{
		this.m_vectorValue = new Vector4(x, y, z, w);
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x000C39C2 File Offset: 0x000C1BC2
	public UberShaderVector()
	{
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x000C39D8 File Offset: 0x000C1BD8
	public override void BindProperties()
	{
		Vector3 lossyScale = this.AttachedBlock.transform.lossyScale;
		switch (this.Mode)
		{
		case UberShaderVector.ScalingMode.None:
			base.BindVector(this.MainBindId, Vector4.Scale(this.m_vectorValue, this.VectorValueScale));
			break;
		case UberShaderVector.ScalingMode.Offset:
		{
			Vector3 vector = lossyScale;
			base.BindVector(this.MainBindId, new Vector4(this.m_vectorValue.x * this.VectorValueScale.x, this.m_vectorValue.y * this.VectorValueScale.y, vector.x, vector.y));
			break;
		}
		case UberShaderVector.ScalingMode.PivotOnXy:
			if (lossyScale.x == lossyScale.y && lossyScale.x == lossyScale.z)
			{
				base.BindVector(this.MainBindId, Vector4.Scale(this.m_vectorValue, this.VectorValueScale));
			}
			else
			{
				base.BindVector(this.MainBindId, Vector4.Scale(new Vector4(this.m_vectorValue.x * lossyScale.x, this.m_vectorValue.y * lossyScale.y, this.m_vectorValue.z, this.m_vectorValue.w), this.VectorValueScale));
			}
			break;
		}
	}

	// Token: 0x17000759 RID: 1881
	// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x000C3B2F File Offset: 0x000C1D2F
	// (set) Token: 0x06002DE6 RID: 11750 RVA: 0x000C3B37 File Offset: 0x000C1D37
	public Vector4 VectorValue
	{
		get
		{
			return this.m_vectorValue;
		}
		set
		{
			this.m_vectorValue = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700075A RID: 1882
	// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x000C3B46 File Offset: 0x000C1D46
	// (set) Token: 0x06002DE8 RID: 11752 RVA: 0x000C3B53 File Offset: 0x000C1D53
	public float X
	{
		get
		{
			return this.m_vectorValue.x;
		}
		set
		{
			this.m_vectorValue.x = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700075B RID: 1883
	// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000C3B67 File Offset: 0x000C1D67
	// (set) Token: 0x06002DEA RID: 11754 RVA: 0x000C3B74 File Offset: 0x000C1D74
	public float Y
	{
		get
		{
			return this.m_vectorValue.y;
		}
		set
		{
			this.m_vectorValue.y = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700075C RID: 1884
	// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000C3B88 File Offset: 0x000C1D88
	// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000C3B95 File Offset: 0x000C1D95
	public float Z
	{
		get
		{
			return this.m_vectorValue.z;
		}
		set
		{
			this.m_vectorValue.z = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700075D RID: 1885
	// (get) Token: 0x06002DED RID: 11757 RVA: 0x000C3BA9 File Offset: 0x000C1DA9
	// (set) Token: 0x06002DEE RID: 11758 RVA: 0x000C3BB6 File Offset: 0x000C1DB6
	public float W
	{
		get
		{
			return this.m_vectorValue.w;
		}
		set
		{
			this.m_vectorValue.w = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700075E RID: 1886
	// (set) Token: 0x06002DEF RID: 11759 RVA: 0x000C3BCA File Offset: 0x000C1DCA
	public float Scale
	{
		set
		{
			this.VectorValueScale = new Vector4(value, value, value, value);
		}
	}

	// Token: 0x04002973 RID: 10611
	[NonSerialized]
	public UberShaderVector.ScalingMode Mode;

	// Token: 0x04002974 RID: 10612
	[SerializeField]
	private Vector4 m_vectorValue;

	// Token: 0x04002975 RID: 10613
	[NonSerialized]
	public Vector4 VectorValueScale = Vector4.one;

	// Token: 0x020007D6 RID: 2006
	public enum ScalingMode
	{
		// Token: 0x0400298B RID: 10635
		None,
		// Token: 0x0400298C RID: 10636
		Offset,
		// Token: 0x0400298D RID: 10637
		PivotOnXy
	}
}
