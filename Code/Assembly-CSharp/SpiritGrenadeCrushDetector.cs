using System;
using UnityEngine;

// Token: 0x0200045B RID: 1115
public class SpiritGrenadeCrushDetector : MonoBehaviour
{
	// Token: 0x06001EC9 RID: 7881 RVA: 0x00087A5A File Offset: 0x00085C5A
	public void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<CrushPlayer>())
		{
			this.SpiritGrenade.Explode();
		}
	}

	// Token: 0x04001A9E RID: 6814
	public SpiritGrenade SpiritGrenade;
}
