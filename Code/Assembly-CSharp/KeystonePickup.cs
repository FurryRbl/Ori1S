using System;
using UnityEngine;

// Token: 0x02000910 RID: 2320
public class KeystonePickup : PickupBase
{
	// Token: 0x06003382 RID: 13186 RVA: 0x000D9298 File Offset: 0x000D7498
	public override void OnCollectorCandidateTouch(GameObject collector)
	{
		IPickupCollector pickupCollector = collector.FindComponentInChildren<IPickupCollector>();
		if (pickupCollector != null)
		{
			pickupCollector.OnCollectKeystonePickup(this);
		}
	}

	// Token: 0x17000821 RID: 2081
	// (get) Token: 0x06003383 RID: 13187 RVA: 0x000D92B9 File Offset: 0x000D74B9
	public int CompletionAmount
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000822 RID: 2082
	// (get) Token: 0x06003384 RID: 13188 RVA: 0x000D92BC File Offset: 0x000D74BC
	public Vector3 AreaCompletorPosition
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x04002E88 RID: 11912
	public int Amount;
}
