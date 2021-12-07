using System;
using Game;
using UnityEngine;

// Token: 0x02000925 RID: 2341
public class DestroyOnMenuOpen : MonoBehaviour
{
	// Token: 0x060033E0 RID: 13280 RVA: 0x000DA438 File Offset: 0x000D8638
	public void Awake()
	{
		Events.Scheduler.OnMenuOpen.Add(new Action(this.OnMenuOpen));
	}

	// Token: 0x060033E1 RID: 13281 RVA: 0x000DA455 File Offset: 0x000D8655
	public void OnDestroy()
	{
		Events.Scheduler.OnMenuOpen.Remove(new Action(this.OnMenuOpen));
	}

	// Token: 0x060033E2 RID: 13282 RVA: 0x000DA472 File Offset: 0x000D8672
	public void OnMenuOpen()
	{
		InstantiateUtility.Destroy(base.gameObject);
	}
}
