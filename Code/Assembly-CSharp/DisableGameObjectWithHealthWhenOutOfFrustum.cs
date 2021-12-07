using System;
using UnityEngine;

// Token: 0x020006C6 RID: 1734
public class DisableGameObjectWithHealthWhenOutOfFrustum : MonoBehaviour, IFrustumOptimizable
{
	// Token: 0x06002987 RID: 10631 RVA: 0x000B36CF File Offset: 0x000B18CF
	public void OnFrustumEnter()
	{
		if (this.HealthController.Value > 0f)
		{
			base.gameObject.SetActive(true);
		}
		this.m_insideFrustum = true;
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x000B36F9 File Offset: 0x000B18F9
	public void OnFrustumExit()
	{
		base.gameObject.SetActive(false);
		this.m_insideFrustum = false;
	}

	// Token: 0x17000696 RID: 1686
	// (get) Token: 0x06002989 RID: 10633 RVA: 0x000B370E File Offset: 0x000B190E
	public bool InsideFrustum
	{
		get
		{
			return this.m_insideFrustum;
		}
	}

	// Token: 0x17000697 RID: 1687
	// (get) Token: 0x0600298A RID: 10634 RVA: 0x000B3716 File Offset: 0x000B1916
	public Bounds Bounds
	{
		get
		{
			return this.m_bounds;
		}
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x000B371E File Offset: 0x000B191E
	public void Awake()
	{
		CameraFrustumOptimizer.Register(this);
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x000B3726 File Offset: 0x000B1926
	public void OnDestroy()
	{
		CameraFrustumOptimizer.Unregister(this);
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x000B372E File Offset: 0x000B192E
	public void Start()
	{
		this.m_bounds = Utility.WorldSpaceHierarchyBoundingBox(base.gameObject);
	}

	// Token: 0x04002511 RID: 9489
	public HealthController HealthController;

	// Token: 0x04002512 RID: 9490
	private bool m_insideFrustum = true;

	// Token: 0x04002513 RID: 9491
	private Bounds m_bounds;
}
