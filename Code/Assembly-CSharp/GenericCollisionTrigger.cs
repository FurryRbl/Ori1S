using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class GenericCollisionTrigger : Trigger
{
	// Token: 0x06001911 RID: 6417 RVA: 0x0006B1EC File Offset: 0x000693EC
	private void OnCollisionEnter(Collision collision)
	{
		if (!this.OnCollision)
		{
			return;
		}
		foreach (ContactPoint contactPoint in collision.contacts)
		{
			this.Process(contactPoint.otherCollider.gameObject, collision);
		}
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x0006B240 File Offset: 0x00069440
	private void OnTriggerEnter(Collider collider)
	{
		if (!this.OnTrigger)
		{
			return;
		}
		this.Process(collider.gameObject, null);
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x0006B25C File Offset: 0x0006945C
	private void Process(GameObject gameObject, Collision collision)
	{
		if (this.TriggeringObjects.Count == 0 || this.TriggeringObjects.Contains(gameObject))
		{
			if (this.Condition && !this.Condition.Validate(new CollisionContext(collision, gameObject.GetComponent<Collider>())))
			{
				return;
			}
			base.DoTrigger(false);
		}
	}

	// Token: 0x0400157A RID: 5498
	public List<GameObject> TriggeringObjects;

	// Token: 0x0400157B RID: 5499
	public bool OnTrigger = true;

	// Token: 0x0400157C RID: 5500
	public bool OnCollision = true;
}
