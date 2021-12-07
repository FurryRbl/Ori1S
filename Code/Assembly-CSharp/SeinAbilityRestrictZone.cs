using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class SeinAbilityRestrictZone : MonoBehaviour
{
	// Token: 0x060005DB RID: 1499 RVA: 0x00017218 File Offset: 0x00015418
	public static bool IsInside(SeinAbilityRestrictZoneMode restrictMode = SeinAbilityRestrictZoneMode.AllAbilities)
	{
		if (Characters.Current == null)
		{
			return false;
		}
		for (int i = 0; i < SeinAbilityRestrictZone.All.Count; i++)
		{
			if (SeinAbilityRestrictZone.All[i].Bounds.Contains(Characters.Current.Position) && SeinAbilityRestrictZone.All[i].RestrictMode == restrictMode)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0001728C File Offset: 0x0001548C
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x000172A4 File Offset: 0x000154A4
	public void OnEnable()
	{
		SeinAbilityRestrictZone.All.Add(this);
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x000172B1 File Offset: 0x000154B1
	public void OnDisable()
	{
		SeinAbilityRestrictZone.All.Remove(this);
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x060005DF RID: 1503 RVA: 0x000172BF File Offset: 0x000154BF
	public Rect Bounds
	{
		get
		{
			return this.m_bounds;
		}
	}

	// Token: 0x0400048D RID: 1165
	private bool m_inside;

	// Token: 0x0400048E RID: 1166
	private Rect m_bounds;

	// Token: 0x0400048F RID: 1167
	public SeinAbilityRestrictZoneMode RestrictMode;

	// Token: 0x04000490 RID: 1168
	public static List<SeinAbilityRestrictZone> All = new List<SeinAbilityRestrictZone>();
}
