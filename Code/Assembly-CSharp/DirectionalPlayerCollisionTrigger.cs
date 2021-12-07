using System;
using Game;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class DirectionalPlayerCollisionTrigger : Trigger
{
	// Token: 0x060018F6 RID: 6390 RVA: 0x0006A880 File Offset: 0x00068A80
	private void OnCollisionEnter(Collision collision)
	{
		this.m_platformMovement = collision.collider.GetComponent<PlatformMovement>();
		this.Process(collision.gameObject);
	}

	// Token: 0x060018F7 RID: 6391 RVA: 0x0006A8AA File Offset: 0x00068AAA
	private void OnTriggerEnter(Collider collider)
	{
		this.m_platformMovement = collider.GetComponent<PlatformMovement>();
		this.Process(collider.gameObject);
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x0006A8C4 File Offset: 0x00068AC4
	private void Process(GameObject gameObject)
	{
		if (gameObject.transform == UI.Cameras.Current.Target)
		{
			Vector3 vector = base.transform.InverseTransformPoint(this.m_platformMovement.transform.position);
			if (vector.x > 0f)
			{
				this.ActionToRun = this.MovingLeftActionToRun;
			}
			if (vector.x < 0f)
			{
				this.ActionToRun = this.MovingRightActionToRun;
			}
			base.DoTrigger(true);
		}
	}

	// Token: 0x04001561 RID: 5473
	public ActionMethod MovingLeftActionToRun;

	// Token: 0x04001562 RID: 5474
	public ActionMethod MovingRightActionToRun;

	// Token: 0x04001563 RID: 5475
	private PlatformMovement m_platformMovement;
}
