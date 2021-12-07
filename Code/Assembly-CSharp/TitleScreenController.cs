using System;
using Core;
using UnityEngine;

// Token: 0x020009E8 RID: 2536
public class TitleScreenController : MonoBehaviour
{
	// Token: 0x0600371A RID: 14106 RVA: 0x000E750D File Offset: 0x000E570D
	private void FixedUpdate()
	{
		if (!this.m_hasRun && Core.Input.Start.OnPressed)
		{
			this.StartAction.Perform(null);
			this.m_hasRun = true;
		}
	}

	// Token: 0x04003216 RID: 12822
	public ActionMethod StartAction;

	// Token: 0x04003217 RID: 12823
	private bool m_hasRun;
}
