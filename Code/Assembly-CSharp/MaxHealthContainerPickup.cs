using System;
using UnityEngine;

// Token: 0x02000912 RID: 2322
public class MaxHealthContainerPickup : PickupBase
{
	// Token: 0x06003388 RID: 13192 RVA: 0x000D9300 File Offset: 0x000D7500
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectMaxHealthContainerPickup(this);
		}
	}
}
