using System;
using UnityEngine;

// Token: 0x02000378 RID: 888
public class PressurePlate : MonoBehaviour
{
	// Token: 0x06001958 RID: 6488 RVA: 0x0006D04E File Offset: 0x0006B24E
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x0006D057 File Offset: 0x0006B257
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x0006D060 File Offset: 0x0006B260
	public void OnCollision(Collision collision)
	{
		if (this.pressed)
		{
			return;
		}
		GameObject gameObject = collision.gameObject;
		if (gameObject.CompareTag("Player"))
		{
			foreach (ContactPoint contactPoint in collision.contacts)
			{
				if (Vector3.Dot(contactPoint.normal, -base.transform.up) > Mathf.Cos(0.7853982f))
				{
					this.pressed = true;
					this.OnPressed.Perform(null);
					return;
				}
			}
		}
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x0006D0F8 File Offset: 0x0006B2F8
	public void OnCollisionExit(Collision collision)
	{
		GameObject gameObject = collision.gameObject;
		if (gameObject.CompareTag("Player") && this.pressed)
		{
			this.pressed = false;
			this.OnReleased.Perform(null);
		}
	}

	// Token: 0x040015C7 RID: 5575
	public ActionMethod OnPressed;

	// Token: 0x040015C8 RID: 5576
	public ActionMethod OnReleased;

	// Token: 0x040015C9 RID: 5577
	private bool pressed;
}
