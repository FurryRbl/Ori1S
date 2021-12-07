using System;
using Game;
using UnityEngine;

// Token: 0x020005F5 RID: 1525
public class AcidBlob : MonoBehaviour, IPooled
{
	// Token: 0x06002639 RID: 9785 RVA: 0x000A7B91 File Offset: 0x000A5D91
	public void OnPoolSpawned()
	{
		this.m_hasSplat = false;
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x000A7B9C File Offset: 0x000A5D9C
	public void OnCollisionEnter(Collision collision)
	{
		if (this.m_hasSplat)
		{
			return;
		}
		if (collision.collider.GetComponent<DamageDealer>())
		{
			return;
		}
		if (collision.contacts.Length > 0)
		{
			Vector3 v = PhysicsHelper.CalculateAverageNormalFromContactPoints(collision.contacts);
			ContactPoint contactPoint = collision.contacts[0];
			Vector3 point = contactPoint.point;
			if (UI.Cameras.Current.IsOnScreenPadded(point, 5f))
			{
				GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.SplatMark, point, Quaternion.Euler(0f, 0f, MoonMath.Angle.AngleFromVector(v)));
				gameObject.transform.parent = base.transform.parent;
				FollowPositionRotation followPositionRotation = gameObject.GetComponent<FollowPositionRotation>();
				if (followPositionRotation == null)
				{
					followPositionRotation = gameObject.AddComponent<FollowPositionRotation>();
				}
				followPositionRotation.SetTarget(collision.transform);
			}
			this.m_hasSplat = true;
		}
	}

	// Token: 0x040020D9 RID: 8409
	public GameObject SplatMark;

	// Token: 0x040020DA RID: 8410
	private bool m_hasSplat;
}
