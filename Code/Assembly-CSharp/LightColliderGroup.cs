using System;
using System.Collections.Generic;
using Sein.World;
using UnityEngine;

// Token: 0x0200064A RID: 1610
public class LightColliderGroup : MonoBehaviour
{
	// Token: 0x0600276C RID: 10092 RVA: 0x000ABBD4 File Offset: 0x000A9DD4
	public void Awake()
	{
		foreach (Collider collider in base.GetComponentsInChildren<Collider>())
		{
			this.m_colliders.Add(collider);
			this.m_centers.Add(collider.bounds.center);
		}
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x000ABC28 File Offset: 0x000A9E28
	public void FixedUpdate()
	{
		int count = this.m_colliders.Count;
		if (this.Dynamic)
		{
			for (int i = 0; i < count; i++)
			{
				this.m_centers[i] = this.m_colliders[i].bounds.center;
			}
		}
		for (int j = 0; j < count; j++)
		{
			bool flag = this.m_colliders[j].enabled ^ !this.InsideLight;
			float padding = (!flag) ? 0f : 0.6f;
			this.EnableCollider(this.m_colliders[j], this.InsideLight == LightSource.TestPosition(this.m_centers[j], padding) || Events.DarknessLifted);
		}
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x000ABD06 File Offset: 0x000A9F06
	private void EnableCollider(Collider collider, bool enable)
	{
		if (collider.enabled != enable)
		{
			collider.enabled = enable;
		}
	}

	// Token: 0x0400220C RID: 8716
	private const float PLATFORM_LIGHT_PADDING = 0.6f;

	// Token: 0x0400220D RID: 8717
	public bool InsideLight;

	// Token: 0x0400220E RID: 8718
	public bool Dynamic;

	// Token: 0x0400220F RID: 8719
	private readonly List<Collider> m_colliders = new List<Collider>();

	// Token: 0x04002210 RID: 8720
	private readonly List<Vector3> m_centers = new List<Vector3>();
}
