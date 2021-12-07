using System;
using Game;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class NaruLookUpZone : MonoBehaviour
{
	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x060002CA RID: 714 RVA: 0x0000BB0C File Offset: 0x00009D0C
	public static bool IsInside
	{
		get
		{
			if (Characters.Naru == null)
			{
				return false;
			}
			for (int i = 0; i < NaruLookUpZone.All.Count; i++)
			{
				if (NaruLookUpZone.All[i].Bounds.Contains(Characters.Naru.Position))
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000BB70 File Offset: 0x00009D70
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000BB88 File Offset: 0x00009D88
	public void OnEnable()
	{
		NaruLookUpZone.All.Add(this);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000BB95 File Offset: 0x00009D95
	public void OnDisable()
	{
		NaruLookUpZone.All.Remove(this);
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x060002CE RID: 718 RVA: 0x0000BBA2 File Offset: 0x00009DA2
	public Rect Bounds
	{
		get
		{
			return this.m_bounds;
		}
	}

	// Token: 0x04000205 RID: 517
	private bool m_inside;

	// Token: 0x04000206 RID: 518
	private Rect m_bounds;

	// Token: 0x04000207 RID: 519
	public static AllContainer<NaruLookUpZone> All = new AllContainer<NaruLookUpZone>();
}
