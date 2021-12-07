using System;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
public class PoolAnalyzeToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005BD RID: 1469
	// (get) Token: 0x06002180 RID: 8576 RVA: 0x00092FB2 File Offset: 0x000911B2
	public string Name
	{
		get
		{
			return "Pool analysis";
		}
	}

	// Token: 0x170005BE RID: 1470
	// (get) Token: 0x06002181 RID: 8577 RVA: 0x00092FB9 File Offset: 0x000911B9
	public string HelpText
	{
		get
		{
			return "Toggle differnt pooling Options";
		}
	}

	// Token: 0x170005BF RID: 1471
	// (get) Token: 0x06002182 RID: 8578 RVA: 0x00092FC0 File Offset: 0x000911C0
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Pool analysis Off",
				"Pool analysis On"
			};
		}
	}

	// Token: 0x170005C0 RID: 1472
	// (get) Token: 0x06002183 RID: 8579 RVA: 0x00092FD8 File Offset: 0x000911D8
	// (set) Token: 0x06002184 RID: 8580 RVA: 0x00092FEC File Offset: 0x000911EC
	public int CurrentToggleOptionId
	{
		get
		{
			return (!this.m_doAnalyze) ? 0 : 1;
		}
		set
		{
			this.m_doAnalyze = !this.m_doAnalyze;
			UberPoolManager.Instance.DoPoolAnalysis = this.m_doAnalyze;
		}
	}

	// Token: 0x04001C41 RID: 7233
	private bool m_doAnalyze;
}
