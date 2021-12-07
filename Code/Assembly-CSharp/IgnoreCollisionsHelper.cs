using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200097B RID: 2427
public struct IgnoreCollisionsHelper
{
	// Token: 0x06003534 RID: 13620 RVA: 0x000DEFA7 File Offset: 0x000DD1A7
	public IgnoreCollisionsHelper(Collider collider)
	{
		this.m_collider = collider;
		this.IgnoredColliders = null;
	}

	// Token: 0x06003535 RID: 13621 RVA: 0x000DEFB7 File Offset: 0x000DD1B7
	public bool IsIgnorning(Collider other)
	{
		return this.IgnoredColliders != null && this.IgnoredColliders.Contains(other);
	}

	// Token: 0x06003536 RID: 13622 RVA: 0x000DEFD4 File Offset: 0x000DD1D4
	public void IgnoreCollision(Collider other, bool ignore = true)
	{
		if (other == null || other.gameObject == null || !other.enabled)
		{
			return;
		}
		if (!other.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.m_collider == null || !this.m_collider.enabled)
		{
			return;
		}
		if (this.IgnoredColliders == null)
		{
			this.IgnoredColliders = new HashSet<Collider>();
		}
		if (this.IgnoredColliders.Contains(other) != ignore)
		{
			Physics.IgnoreCollision(this.m_collider, other, ignore);
		}
		if (ignore)
		{
			this.IgnoredColliders.Add(other);
		}
		else
		{
			this.IgnoredColliders.Remove(other);
		}
	}

	// Token: 0x06003537 RID: 13623 RVA: 0x000DF098 File Offset: 0x000DD298
	public void ResetCollisions()
	{
		if (this.IgnoredColliders == null)
		{
			this.IgnoredColliders = new HashSet<Collider>();
		}
		foreach (Collider collider in this.IgnoredColliders)
		{
			if (collider && collider.gameObject.activeInHierarchy)
			{
				Physics.IgnoreCollision(this.m_collider, collider, false);
			}
		}
		this.IgnoredColliders.Clear();
	}

	// Token: 0x06003538 RID: 13624 RVA: 0x000DF134 File Offset: 0x000DD334
	public void ApplyIgnoredColliders()
	{
		if (this.IgnoredColliders == null)
		{
			this.IgnoredColliders = new HashSet<Collider>();
		}
		foreach (Collider collider in this.IgnoredColliders)
		{
			if (collider && collider.gameObject.activeInHierarchy)
			{
				Physics.IgnoreCollision(this.m_collider, collider);
			}
		}
	}

	// Token: 0x04002FD3 RID: 12243
	public HashSet<Collider> IgnoredColliders;

	// Token: 0x04002FD4 RID: 12244
	private readonly Collider m_collider;
}
