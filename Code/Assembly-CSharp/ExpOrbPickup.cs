using System;
using UnityEngine;

// Token: 0x02000451 RID: 1105
public class ExpOrbPickup : PickupBase
{
	// Token: 0x06001EAA RID: 7850 RVA: 0x0008711C File Offset: 0x0008531C
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectExpOrbPickup(this);
		}
	}

	// Token: 0x04001A77 RID: 6775
	public ExpOrbPickup.ExpOrbMessageType MessageType;

	// Token: 0x04001A78 RID: 6776
	public int Amount;

	// Token: 0x0200090F RID: 2319
	public enum ExpOrbMessageType
	{
		// Token: 0x04002E84 RID: 11908
		None,
		// Token: 0x04002E85 RID: 11909
		PickupSmall,
		// Token: 0x04002E86 RID: 11910
		PickupMedium,
		// Token: 0x04002E87 RID: 11911
		PickupLarge
	}
}
