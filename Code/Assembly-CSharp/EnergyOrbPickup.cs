using System;
using UnityEngine;

// Token: 0x0200090E RID: 2318
public class EnergyOrbPickup : PickupBase
{
	// Token: 0x06003380 RID: 13184 RVA: 0x000D926C File Offset: 0x000D746C
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectEnergyOrbPickup(this);
		}
	}

	// Token: 0x04002E82 RID: 11906
	public int Amount;
}
