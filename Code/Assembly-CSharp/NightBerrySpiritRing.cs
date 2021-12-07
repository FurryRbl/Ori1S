using System;
using UnityEngine;

// Token: 0x020008FD RID: 2301
public class NightBerrySpiritRing : MonoBehaviour
{
	// Token: 0x06003328 RID: 13096 RVA: 0x000D7AFC File Offset: 0x000D5CFC
	public void OnTriggerEnter(Collider collider)
	{
		INightBerrySpiritRingReciever nightBerrySpiritRingReciever = collider.gameObject.FindComponent<INightBerrySpiritRingReciever>();
		if (nightBerrySpiritRingReciever != null)
		{
			nightBerrySpiritRingReciever.OnSpiritRingEnter();
		}
	}

	// Token: 0x06003329 RID: 13097 RVA: 0x000D7B24 File Offset: 0x000D5D24
	public void OnTriggerExit(Collider collider)
	{
		INightBerrySpiritRingReciever nightBerrySpiritRingReciever = collider.gameObject.FindComponent<INightBerrySpiritRingReciever>();
		if (nightBerrySpiritRingReciever != null)
		{
			nightBerrySpiritRingReciever.OnSpiritRingLeave();
		}
	}
}
