using System;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class SeinLandOnTrigger : MonoBehaviour
{
	// Token: 0x06001977 RID: 6519 RVA: 0x0006DA60 File Offset: 0x0006BC60
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			foreach (ContactPoint contactPoint in collision.contacts)
			{
				if (Vector3.Dot(contactPoint.normal, Vector3.down) > Mathf.Cos(0.7853982f))
				{
					this.OnLand.Perform(null);
					return;
				}
			}
		}
	}

	// Token: 0x040015E5 RID: 5605
	public ActionMethod OnLand;
}
