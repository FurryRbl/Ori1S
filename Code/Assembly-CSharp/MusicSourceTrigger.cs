using System;
using UnityEngine;

// Token: 0x020006A9 RID: 1705
public class MusicSourceTrigger : MusicSource
{
	// Token: 0x0600292F RID: 10543 RVA: 0x000B1DC8 File Offset: 0x000AFFC8
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
		}
	}
}
