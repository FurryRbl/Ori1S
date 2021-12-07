using System;
using UnityEngine;

// Token: 0x02000914 RID: 2324
public class RestoreHealthPickup : PickupBase
{
	// Token: 0x0600338C RID: 13196 RVA: 0x000D938C File Offset: 0x000D758C
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectRestoreHealthPickup(this);
		}
	}

	// Token: 0x04002E8D RID: 11917
	public int Amount;
}
