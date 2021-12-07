using System;
using UnityEngine;

// Token: 0x020006C7 RID: 1735
public class DisableRendererWhenOutOfFrustrum : MonoBehaviour, IFrustumOptimizable
{
	// Token: 0x0600298F RID: 10639 RVA: 0x000B3750 File Offset: 0x000B1950
	public void OnFrustumEnter()
	{
		base.gameObject.SetActive(true);
		this.m_insideFrustum = true;
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x000B3765 File Offset: 0x000B1965
	public void OnFrustumExit()
	{
		base.gameObject.SetActive(false);
		this.m_insideFrustum = false;
	}

	// Token: 0x17000698 RID: 1688
	// (get) Token: 0x06002991 RID: 10641 RVA: 0x000B377A File Offset: 0x000B197A
	public bool InsideFrustum
	{
		get
		{
			return this.m_insideFrustum;
		}
	}

	// Token: 0x17000699 RID: 1689
	// (get) Token: 0x06002992 RID: 10642 RVA: 0x000B3782 File Offset: 0x000B1982
	public Bounds Bounds
	{
		get
		{
			return this.m_bounds;
		}
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x000B378A File Offset: 0x000B198A
	public void Awake()
	{
		this.m_bounds = Utility.WorldSpaceHierarchyBoundingBox(base.gameObject);
		CameraFrustumOptimizer.Register(this);
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x000B37A3 File Offset: 0x000B19A3
	public void OnDestroy()
	{
		CameraFrustumOptimizer.Unregister(this);
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x000B37AB File Offset: 0x000B19AB
	public void Start()
	{
	}

	// Token: 0x04002514 RID: 9492
	private bool m_insideFrustum = true;

	// Token: 0x04002515 RID: 9493
	private Bounds m_bounds;
}
