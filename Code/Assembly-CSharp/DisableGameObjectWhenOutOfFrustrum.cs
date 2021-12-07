using System;
using UnityEngine;

// Token: 0x020006C4 RID: 1732
public class DisableGameObjectWhenOutOfFrustrum : MonoBehaviour, IFrustumOptimizable, IPooled, ISceneRootPreEnableObserver
{
	// Token: 0x0600297A RID: 10618 RVA: 0x000B34B0 File Offset: 0x000B16B0
	public void OnDrawGizmosSelected()
	{
		this.UpdateBounds();
		Gizmos.DrawWireCube(this.m_bounds.center + base.transform.position, this.m_bounds.size);
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x000B34EE File Offset: 0x000B16EE
	public void OnFrustumEnter()
	{
		base.gameObject.SetActive(true);
		this.m_insideFrustum = true;
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x000B3503 File Offset: 0x000B1703
	public void OnFrustumExit()
	{
		base.gameObject.SetActive(false);
		this.m_insideFrustum = false;
	}

	// Token: 0x17000694 RID: 1684
	// (get) Token: 0x0600297D RID: 10621 RVA: 0x000B3518 File Offset: 0x000B1718
	public bool InsideFrustum
	{
		get
		{
			return this.m_insideFrustum;
		}
	}

	// Token: 0x17000695 RID: 1685
	// (get) Token: 0x0600297E RID: 10622 RVA: 0x000B3520 File Offset: 0x000B1720
	public Bounds Bounds
	{
		get
		{
			return new Bounds(this.m_bounds.center + base.transform.position, this.m_bounds.size);
		}
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x000B3558 File Offset: 0x000B1758
	public void Awake()
	{
		this.m_awakeCalled = true;
		this.UpdateBounds();
		if (this.IsValid())
		{
			CameraFrustumOptimizer.Register(this);
		}
		else
		{
			UnityEngine.Object.DestroyObject(this);
		}
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x000B3584 File Offset: 0x000B1784
	public bool IsValid()
	{
		return (this.Activated || this.SpecifyBounds) && base.gameObject.activeSelf;
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x000B35B8 File Offset: 0x000B17B8
	private void UpdateBounds()
	{
		if (this.SpecifyBounds)
		{
			this.m_bounds = this.SetBounds;
		}
		else if (this.SpecifyBoundingBox)
		{
			this.m_bounds = new Bounds(new Vector3(this.BoundingBox.xMin, this.BoundingBox.yMin), new Vector3(this.BoundingBox.width, this.BoundingBox.height, 1f));
		}
		else
		{
			this.m_bounds = Utility.WorldSpaceHierarchyBoundingBox(base.gameObject);
			this.m_bounds.center = this.m_bounds.center - base.transform.position;
		}
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x000B3669 File Offset: 0x000B1869
	public void OnDestroy()
	{
		CameraFrustumOptimizer.Unregister(this);
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x000B3674 File Offset: 0x000B1874
	public void OnSceneRootPreEnable()
	{
		if (this.m_awakeCalled)
		{
			return;
		}
		if (this.IsValid())
		{
			this.UpdateBounds();
			this.m_insideFrustum = false;
			CameraFrustumOptimizer.RegisterUninitialized(this);
		}
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x000B36AB File Offset: 0x000B18AB
	public void OnPoolSpawned()
	{
		this.m_insideFrustum = true;
		base.gameObject.SetActive(true);
	}

	// Token: 0x04002509 RID: 9481
	public bool Activated;

	// Token: 0x0400250A RID: 9482
	public bool SpecifyBoundingBox;

	// Token: 0x0400250B RID: 9483
	public Rect BoundingBox;

	// Token: 0x0400250C RID: 9484
	public bool SpecifyBounds;

	// Token: 0x0400250D RID: 9485
	public Bounds SetBounds;

	// Token: 0x0400250E RID: 9486
	private bool m_insideFrustum = true;

	// Token: 0x0400250F RID: 9487
	private bool m_awakeCalled;

	// Token: 0x04002510 RID: 9488
	private Bounds m_bounds;
}
