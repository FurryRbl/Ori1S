using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009AF RID: 2479
public class RockExplodeZone : MonoBehaviour
{
	// Token: 0x06003610 RID: 13840 RVA: 0x000E2BA4 File Offset: 0x000E0DA4
	public void Awake()
	{
		this.m_bounds = new Rect
		{
			width = base.transform.lossyScale.x,
			height = base.transform.lossyScale.y,
			center = base.transform.position
		};
		RockExplodeZone.All.Add(this);
	}

	// Token: 0x06003611 RID: 13841 RVA: 0x000E2C18 File Offset: 0x000E0E18
	public static bool IsInsideAZone(Vector3 position)
	{
		foreach (RockExplodeZone rockExplodeZone in RockExplodeZone.All)
		{
			if (rockExplodeZone.IsInside(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003612 RID: 13842 RVA: 0x000E2C80 File Offset: 0x000E0E80
	public void OnDestroy()
	{
		RockExplodeZone.All.Remove(this);
	}

	// Token: 0x06003613 RID: 13843 RVA: 0x000E2C8E File Offset: 0x000E0E8E
	public bool IsInside(Vector3 position)
	{
		return this.m_bounds.Contains(position);
	}

	// Token: 0x0400309B RID: 12443
	public static List<RockExplodeZone> All = new List<RockExplodeZone>();

	// Token: 0x0400309C RID: 12444
	private Rect m_bounds;
}
