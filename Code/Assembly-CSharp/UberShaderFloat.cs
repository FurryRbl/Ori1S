using System;
using UnityEngine;

// Token: 0x020007EF RID: 2031
[Serializable]
public class UberShaderFloat : UberShaderProperty
{
	// Token: 0x06002EA0 RID: 11936 RVA: 0x000C5AB1 File Offset: 0x000C3CB1
	public UberShaderFloat(float val)
	{
		this.m_floatValue = val;
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x000C5ACB File Offset: 0x000C3CCB
	public UberShaderFloat()
	{
	}

	// Token: 0x06002EA2 RID: 11938 RVA: 0x000C5ADE File Offset: 0x000C3CDE
	public override void BindProperties()
	{
		base.BindFloat(this.MainBindId, this.m_floatValue * this.Scale);
	}

	// Token: 0x1700077E RID: 1918
	// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000C5AF9 File Offset: 0x000C3CF9
	// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000C5B01 File Offset: 0x000C3D01
	public float FloatValue
	{
		get
		{
			return this.m_floatValue;
		}
		set
		{
			this.m_floatValue = value;
			this.BindProperties();
		}
	}

	// Token: 0x040029D3 RID: 10707
	[SerializeField]
	private float m_floatValue;

	// Token: 0x040029D4 RID: 10708
	[NonSerialized]
	public float Scale = 1f;
}
