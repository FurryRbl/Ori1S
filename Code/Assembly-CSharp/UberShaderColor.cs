using System;
using UnityEngine;

// Token: 0x02000786 RID: 1926
[Serializable]
public class UberShaderColor : UberShaderProperty
{
	// Token: 0x06002CB1 RID: 11441 RVA: 0x000BFC2F File Offset: 0x000BDE2F
	public UberShaderColor()
	{
	}

	// Token: 0x06002CB2 RID: 11442 RVA: 0x000BFC42 File Offset: 0x000BDE42
	public UberShaderColor(Color defaultColor)
	{
		this.m_color = defaultColor;
	}

	// Token: 0x06002CB3 RID: 11443 RVA: 0x000BFC5C File Offset: 0x000BDE5C
	public override void BindProperties()
	{
		base.BindColor(this.MainBindId, this.m_color);
	}

	// Token: 0x17000722 RID: 1826
	// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000BFC70 File Offset: 0x000BDE70
	// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x000BFC78 File Offset: 0x000BDE78
	public Color Color
	{
		get
		{
			return this.m_color;
		}
		set
		{
			this.m_color = value;
			this.BindProperties();
		}
	}

	// Token: 0x17000723 RID: 1827
	// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000BFC87 File Offset: 0x000BDE87
	// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x000BFC94 File Offset: 0x000BDE94
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

	// Token: 0x17000724 RID: 1828
	// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000BFCA8 File Offset: 0x000BDEA8
	// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000BFCB5 File Offset: 0x000BDEB5
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

	// Token: 0x17000725 RID: 1829
	// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000BFCC9 File Offset: 0x000BDEC9
	// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000BFCD6 File Offset: 0x000BDED6
	public float b
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

	// Token: 0x17000726 RID: 1830
	// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000BFCEA File Offset: 0x000BDEEA
	// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000BFCF7 File Offset: 0x000BDEF7
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

	// Token: 0x04002873 RID: 10355
	[SerializeField]
	private Color m_color = Color.white;
}
