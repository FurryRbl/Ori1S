using System;
using UnityEngine;

// Token: 0x02000915 RID: 2325
public class SkillPointPickup : PickupBase
{
	// Token: 0x0600338E RID: 13198 RVA: 0x000D93B8 File Offset: 0x000D75B8
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectSkillPointPickup(this);
		}
	}

	// Token: 0x04002E8E RID: 11918
	public int Amount;
}
