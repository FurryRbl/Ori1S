using System;
using UnityEngine;

// Token: 0x02000911 RID: 2321
public class MaxEnergyContainerPickup : PickupBase
{
	// Token: 0x06003386 RID: 13190 RVA: 0x000D92D4 File Offset: 0x000D74D4
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectMaxEnergyContainerPickup(this);
		}
	}

	// Token: 0x04002E89 RID: 11913
	public int Amount;
}
