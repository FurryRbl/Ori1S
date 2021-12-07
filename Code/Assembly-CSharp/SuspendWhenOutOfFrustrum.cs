using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C8 RID: 1736
public class SuspendWhenOutOfFrustrum : MonoBehaviour, IFrustumOptimizable
{
	// Token: 0x06002997 RID: 10647 RVA: 0x000B37E8 File Offset: 0x000B19E8
	public void OnDrawGizmosSelected()
	{
		Bounds bounds = this.Bounds;
		Gizmos.DrawWireCube(bounds.center, bounds.size);
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x000B380F File Offset: 0x000B1A0F
	public void OnFrustumEnter()
	{
		this.m_insideFrustum = true;
		SuspensionManager.Resume(this.m_suspendables);
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x000B3823 File Offset: 0x000B1A23
	public void OnFrustumExit()
	{
		this.m_insideFrustum = false;
		SuspensionManager.Suspend(this.m_suspendables);
	}

	// Token: 0x1700069A RID: 1690
	// (get) Token: 0x0600299A RID: 10650 RVA: 0x000B3837 File Offset: 0x000B1A37
	public bool InsideFrustum
	{
		get
		{
			return this.m_insideFrustum;
		}
	}

	// Token: 0x1700069B RID: 1691
	// (get) Token: 0x0600299B RID: 10651 RVA: 0x000B3840 File Offset: 0x000B1A40
	public Bounds Bounds
	{
		get
		{
			Vector3 size = new Vector3(this.BoundingBox.width, this.BoundingBox.height, 0f);
			Vector3 vector = base.transform.position;
			vector += new Vector3(this.BoundingBox.center.x, this.BoundingBox.center.y, 0f);
			return new Bounds(vector, size);
		}
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x000B38B9 File Offset: 0x000B1AB9
	public void Awake()
	{
		this.m_transform = base.transform;
		SuspensionManager.GetSuspendables(this.m_suspendables, base.gameObject);
		CameraFrustumOptimizer.Register(this);
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x000B38DE File Offset: 0x000B1ADE
	public void OnDestroy()
	{
		CameraFrustumOptimizer.Unregister(this);
	}

	// Token: 0x04002516 RID: 9494
	private readonly HashSet<ISuspendable> m_suspendables = new HashSet<ISuspendable>();

	// Token: 0x04002517 RID: 9495
	public Rect BoundingBox = new Rect(-1f, 1f, 2f, 2f);

	// Token: 0x04002518 RID: 9496
	private bool m_insideFrustum = true;

	// Token: 0x04002519 RID: 9497
	private Transform m_transform;
}
