using System;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class Spring : MonoBehaviour
{
	// Token: 0x06001826 RID: 6182 RVA: 0x0006773C File Offset: 0x0006593C
	public void OnCollisionEnter(Collision collision)
	{
		GameObject gameObject = collision.gameObject;
		bool flag = gameObject.GetComponent<SpiritGrenade>();
		if (gameObject.CompareTag("Player") || flag)
		{
			foreach (ContactPoint contactPoint in collision.contacts)
			{
				if (Vector3.Dot(contactPoint.normal, -base.transform.up) > Mathf.Cos(0.7853982f))
				{
					if (this.m_context == null)
					{
						this.m_context = new SpringContext(this);
					}
					this.LastObject = gameObject;
					if (flag)
					{
						this.OnLandGrenade.Perform(this.m_context);
					}
					else
					{
						this.OnLand.Perform(this.m_context);
					}
					return;
				}
			}
		}
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x06001827 RID: 6183 RVA: 0x00067816 File Offset: 0x00065A16
	// (set) Token: 0x06001828 RID: 6184 RVA: 0x0006781E File Offset: 0x00065A1E
	public GameObject LastObject { get; set; }

	// Token: 0x040014C9 RID: 5321
	public ActionMethod OnLand;

	// Token: 0x040014CA RID: 5322
	public ActionMethod OnLandGrenade;

	// Token: 0x040014CB RID: 5323
	private SpringContext m_context;
}
