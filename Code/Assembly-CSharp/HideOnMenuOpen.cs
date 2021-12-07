using System;
using Game;
using UnityEngine;

// Token: 0x02000926 RID: 2342
public class HideOnMenuOpen : MonoBehaviour
{
	// Token: 0x060033E4 RID: 13284 RVA: 0x000DA487 File Offset: 0x000D8687
	public void Awake()
	{
		Events.Scheduler.OnMenuOpen.Add(new Action(this.OnMenuOpen));
		Events.Scheduler.OnMenuClose.Add(new Action(this.OnMenuClose));
	}

	// Token: 0x060033E5 RID: 13285 RVA: 0x000DA4BF File Offset: 0x000D86BF
	public void OnDestroy()
	{
		Events.Scheduler.OnMenuOpen.Remove(new Action(this.OnMenuOpen));
		Events.Scheduler.OnMenuClose.Remove(new Action(this.OnMenuClose));
	}

	// Token: 0x060033E6 RID: 13286 RVA: 0x000DA4F7 File Offset: 0x000D86F7
	public void OnMenuOpen()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060033E7 RID: 13287 RVA: 0x000DA505 File Offset: 0x000D8705
	public void OnMenuClose()
	{
		base.gameObject.SetActive(true);
	}
}
