using System;
using UnityEngine;

// Token: 0x020008F0 RID: 2288
public class MapStonePickup : PickupBase
{
	// Token: 0x060032F9 RID: 13049 RVA: 0x000D73B8 File Offset: 0x000D55B8
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectMapStonePickup(this);
		}
	}

	// Token: 0x04002DFE RID: 11774
	public int Amount;
}
