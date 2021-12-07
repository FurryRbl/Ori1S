using System;
using UnityEngine;

// Token: 0x020008B1 RID: 2225
[Serializable]
public class SPAConfigValue
{
	// Token: 0x06003199 RID: 12697 RVA: 0x000D3439 File Offset: 0x000D1639
	public SPAConfigValue()
	{
	}

	// Token: 0x0600319A RID: 12698 RVA: 0x000D3441 File Offset: 0x000D1641
	public SPAConfigValue(SPAConfig spaConfig)
	{
		this.Value = spaConfig;
	}

	// Token: 0x170007DC RID: 2012
	// (get) Token: 0x0600319B RID: 12699 RVA: 0x000D3450 File Offset: 0x000D1650
	// (set) Token: 0x0600319C RID: 12700 RVA: 0x000D3458 File Offset: 0x000D1658
	public SPAConfig Value
	{
		get
		{
			return (SPAConfig)this.m_spaConfig;
		}
		set
		{
			this.m_spaConfig = (int)value;
		}
	}

	// Token: 0x170007DD RID: 2013
	// (get) Token: 0x0600319D RID: 12701 RVA: 0x000D3461 File Offset: 0x000D1661
	public uint UIntValue
	{
		get
		{
			return (uint)this.m_spaConfig;
		}
	}

	// Token: 0x170007DE RID: 2014
	// (get) Token: 0x0600319E RID: 12702 RVA: 0x000D3469 File Offset: 0x000D1669
	public ushort UShortValue
	{
		get
		{
			return (ushort)this.m_spaConfig;
		}
	}

	// Token: 0x04002CDB RID: 11483
	[SerializeField]
	private int m_spaConfig;
}
