using System;
using UnityEngine;

// Token: 0x02000807 RID: 2055
[Serializable]
public class UberShaderMultiplyLayerColor : UberShaderProperty
{
	// Token: 0x06002F40 RID: 12096 RVA: 0x000C8053 File Offset: 0x000C6253
	public UberShaderMultiplyLayerColor()
	{
	}

	// Token: 0x06002F41 RID: 12097 RVA: 0x000C8066 File Offset: 0x000C6266
	public UberShaderMultiplyLayerColor(Color defaultColor)
	{
		this.m_color = defaultColor;
	}

	// Token: 0x06002F42 RID: 12098 RVA: 0x000C8080 File Offset: 0x000C6280
	private float CalculateColorComponentValue(float v)
	{
		float result;
		if (v < 0.5f)
		{
			result = 0.1f * v * 2f;
		}
		else
		{
			result = 0.1f + 0.9f * (v - 0.5f) * 2f;
		}
		return result;
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x000C80C8 File Offset: 0x000C62C8
	private Color CalculateFinalMultiplyColor(Color color)
	{
		Color result = color;
		result.r = this.CalculateColorComponentValue(result.r);
		result.g = this.CalculateColorComponentValue(result.g);
		result.b = this.CalculateColorComponentValue(result.b);
		return result;
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x000C8114 File Offset: 0x000C6314
	public override void BindProperties()
	{
		base.BindColor(this.MainBindId, this.CalculateFinalMultiplyColor(this.m_color));
	}

	// Token: 0x17000799 RID: 1945
	// (get) Token: 0x06002F45 RID: 12101 RVA: 0x000C812E File Offset: 0x000C632E
	// (set) Token: 0x06002F46 RID: 12102 RVA: 0x000C8136 File Offset: 0x000C6336
	public Color Color
	{
		get
		{
			return this.m_color;
		}
		set
		{
			this.m_color = value;
			base.BindColor(this.MainBindId, this.CalculateFinalMultiplyColor(value));
		}
	}

	// Token: 0x1700079A RID: 1946
	// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000C8152 File Offset: 0x000C6352
	// (set) Token: 0x06002F48 RID: 12104 RVA: 0x000C815F File Offset: 0x000C635F
	public float R
	{
		get
		{
			return this.m_color.r;
		}
		set
		{
			this.m_color.r = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700079B RID: 1947
	// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000C8173 File Offset: 0x000C6373
	// (set) Token: 0x06002F4A RID: 12106 RVA: 0x000C8180 File Offset: 0x000C6380
	public float G
	{
		get
		{
			return this.m_color.g;
		}
		set
		{
			this.m_color.g = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700079C RID: 1948
	// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000C8194 File Offset: 0x000C6394
	// (set) Token: 0x06002F4C RID: 12108 RVA: 0x000C81A1 File Offset: 0x000C63A1
	public float B
	{
		get
		{
			return this.m_color.b;
		}
		set
		{
			this.m_color.b = value;
			this.BindProperties();
		}
	}

	// Token: 0x1700079D RID: 1949
	// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000C81B5 File Offset: 0x000C63B5
	// (set) Token: 0x06002F4E RID: 12110 RVA: 0x000C81C2 File Offset: 0x000C63C2
	public float A
	{
		get
		{
			return this.m_color.a;
		}
		set
		{
			this.m_color.a = value;
			this.BindProperties();
		}
	}

	// Token: 0x04002A58 RID: 10840
	[SerializeField]
	private Color m_color = Color.white;
}
