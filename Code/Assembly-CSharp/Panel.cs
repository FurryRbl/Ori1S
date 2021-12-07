using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class Panel : MonoBehaviour
{
	// Token: 0x06001064 RID: 4196 RVA: 0x0004B1DC File Offset: 0x000493DC
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x0004B1EA File Offset: 0x000493EA
	public void Show()
	{
		base.gameObject.SetActive(true);
	}
}
