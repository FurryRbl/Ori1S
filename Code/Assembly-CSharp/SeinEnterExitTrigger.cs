using System;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class SeinEnterExitTrigger : MonoBehaviour
{
	// Token: 0x0600196F RID: 6511 RVA: 0x0006D890 File Offset: 0x0006BA90
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player") && this.OnEnterAction)
		{
			this.OnEnterAction.Perform(null);
		}
	}

	// Token: 0x06001970 RID: 6512 RVA: 0x0006D8CC File Offset: 0x0006BACC
	private void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player") && this.OnExitAction)
		{
			this.OnExitAction.Perform(null);
		}
	}

	// Token: 0x040015E1 RID: 5601
	public ActionMethod OnEnterAction;

	// Token: 0x040015E2 RID: 5602
	public ActionMethod OnExitAction;
}
