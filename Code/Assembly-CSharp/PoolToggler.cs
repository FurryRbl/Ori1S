using System;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
public class PoolToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005C1 RID: 1473
	// (get) Token: 0x06002186 RID: 8582 RVA: 0x0009301C File Offset: 0x0009121C
	public string Name
	{
		get
		{
			return "Pooling";
		}
	}

	// Token: 0x170005C2 RID: 1474
	// (get) Token: 0x06002187 RID: 8583 RVA: 0x00093023 File Offset: 0x00091223
	public string HelpText
	{
		get
		{
			return "Toggle differnt pooling Options";
		}
	}

	// Token: 0x170005C3 RID: 1475
	// (get) Token: 0x06002188 RID: 8584 RVA: 0x0009302A File Offset: 0x0009122A
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Pooling Off",
				"Pooling On"
			};
		}
	}

	// Token: 0x170005C4 RID: 1476
	// (get) Token: 0x06002189 RID: 8585 RVA: 0x00093042 File Offset: 0x00091242
	// (set) Token: 0x0600218A RID: 8586 RVA: 0x00093056 File Offset: 0x00091256
	public int CurrentToggleOptionId
	{
		get
		{
			return (!this.m_doPool) ? 0 : 1;
		}
		set
		{
			this.m_doPool = !this.m_doPool;
			UberPoolManager.Instance.DoPool = this.m_doPool;
		}
	}

	// Token: 0x04001C42 RID: 7234
	private bool m_doPool = true;
}
