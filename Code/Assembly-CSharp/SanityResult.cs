using System;
using UnityEngine;

// Token: 0x020006F9 RID: 1785
public class SanityResult
{
	// Token: 0x06002A85 RID: 10885 RVA: 0x000B6891 File Offset: 0x000B4A91
	public SanityResult(string summary, string details, bool passed, GameObject gameObject, Func<GameObject[], bool> fixFunction)
	{
		this.m_summary = summary;
		this.m_details = details;
		this.m_passed = passed;
		this.m_gameObject = gameObject;
		this.m_fixFunction = fixFunction;
	}

	// Token: 0x170006C6 RID: 1734
	// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000B68BE File Offset: 0x000B4ABE
	public string Summary
	{
		get
		{
			return this.m_summary;
		}
	}

	// Token: 0x170006C7 RID: 1735
	// (get) Token: 0x06002A87 RID: 10887 RVA: 0x000B68C6 File Offset: 0x000B4AC6
	public string Details
	{
		get
		{
			return this.m_details;
		}
	}

	// Token: 0x170006C8 RID: 1736
	// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000B68CE File Offset: 0x000B4ACE
	public bool Passed
	{
		get
		{
			return this.m_passed;
		}
	}

	// Token: 0x170006C9 RID: 1737
	// (get) Token: 0x06002A89 RID: 10889 RVA: 0x000B68D6 File Offset: 0x000B4AD6
	public GameObject GameObject
	{
		get
		{
			return this.m_gameObject;
		}
	}

	// Token: 0x170006CA RID: 1738
	// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000B68DE File Offset: 0x000B4ADE
	public Func<GameObject[], bool> FixFunction
	{
		get
		{
			return this.m_fixFunction;
		}
	}

	// Token: 0x040025DF RID: 9695
	private string m_summary;

	// Token: 0x040025E0 RID: 9696
	private string m_details;

	// Token: 0x040025E1 RID: 9697
	private bool m_passed;

	// Token: 0x040025E2 RID: 9698
	private GameObject m_gameObject;

	// Token: 0x040025E3 RID: 9699
	private Func<GameObject[], bool> m_fixFunction;
}
