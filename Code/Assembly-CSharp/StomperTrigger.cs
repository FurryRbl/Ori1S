using System;
using UnityEngine;

// Token: 0x02000931 RID: 2353
public class StomperTrigger : MonoBehaviour
{
	// Token: 0x0600340C RID: 13324 RVA: 0x000DAF63 File Offset: 0x000D9163
	private void OnTriggerEnter(Collider collider)
	{
		this.PerformTrigger(collider);
	}

	// Token: 0x0600340D RID: 13325 RVA: 0x000DAF6C File Offset: 0x000D916C
	private void OnTriggerStay(Collider collider)
	{
		this.PerformTrigger(collider);
	}

	// Token: 0x0600340E RID: 13326 RVA: 0x000DAF78 File Offset: 0x000D9178
	private void PerformTrigger(Collider collider)
	{
		if (this.Activated && collider.gameObject.FindComponent<ICanActivateStompers>() != null)
		{
			this.StomperToTrigger.PlayerTouchedTrigger();
		}
	}

	// Token: 0x04002F0B RID: 12043
	public bool TriggerOncePerOnEnter;

	// Token: 0x04002F0C RID: 12044
	public Stomper StomperToTrigger;

	// Token: 0x04002F0D RID: 12045
	public bool Activated = true;
}
