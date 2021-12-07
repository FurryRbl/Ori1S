using System;
using UnityEngine;

// Token: 0x020006C0 RID: 1728
[Serializable]
public class MeshRendererFrustrumOptimiser : IFrustumOptimizable
{
	// Token: 0x06002970 RID: 10608 RVA: 0x000B33B9 File Offset: 0x000B15B9
	public MeshRendererFrustrumOptimiser(GameObject gameObject)
	{
		this.GameObject = gameObject;
		this.BoundingBox = Utility.WorldSpaceHierarchyBoundingBox(gameObject);
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x000B33D4 File Offset: 0x000B15D4
	public void OnFrustumEnter()
	{
		this.m_outsideFrustum = false;
		if (this.GameObject)
		{
			this.GameObject.SetActive(true);
		}
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x000B33F9 File Offset: 0x000B15F9
	public void OnFrustumExit()
	{
		this.m_outsideFrustum = true;
		if (this.GameObject)
		{
			this.GameObject.SetActive(false);
		}
	}

	// Token: 0x17000692 RID: 1682
	// (get) Token: 0x06002973 RID: 10611 RVA: 0x000B341E File Offset: 0x000B161E
	public bool InsideFrustum
	{
		get
		{
			return !this.m_outsideFrustum;
		}
	}

	// Token: 0x17000693 RID: 1683
	// (get) Token: 0x06002974 RID: 10612 RVA: 0x000B3429 File Offset: 0x000B1629
	public Bounds Bounds
	{
		get
		{
			return this.BoundingBox;
		}
	}

	// Token: 0x04002504 RID: 9476
	public GameObject GameObject;

	// Token: 0x04002505 RID: 9477
	public Bounds BoundingBox;

	// Token: 0x04002506 RID: 9478
	private bool m_outsideFrustum;
}
