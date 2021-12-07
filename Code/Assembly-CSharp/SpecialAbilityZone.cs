using System;
using Game;
using UnityEngine;

// Token: 0x02000957 RID: 2391
public class SpecialAbilityZone : MonoBehaviour
{
	// Token: 0x17000849 RID: 2121
	// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000DCE3C File Offset: 0x000DB03C
	public static bool IsInside
	{
		get
		{
			if (Characters.Sein == null)
			{
				return false;
			}
			for (int i = 0; i < SpecialAbilityZone.All.Count; i++)
			{
				if (SpecialAbilityZone.All[i].Bounds.Contains(Characters.Sein.Position) && !SpecialAbilityZone.All[i].IsRainbowZone)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x1700084A RID: 2122
	// (get) Token: 0x060034A6 RID: 13478 RVA: 0x000DCEB8 File Offset: 0x000DB0B8
	public static bool IsInsideRainbowZone
	{
		get
		{
			if (Characters.Sein == null)
			{
				return false;
			}
			for (int i = 0; i < SpecialAbilityZone.All.Count; i++)
			{
				if (SpecialAbilityZone.All[i].Bounds.Contains(Characters.Sein.Position) && SpecialAbilityZone.All[i].IsRainbowZone)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x060034A7 RID: 13479 RVA: 0x000DCF31 File Offset: 0x000DB131
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x000DCF49 File Offset: 0x000DB149
	public void OnEnable()
	{
		SpecialAbilityZone.All.Add(this);
	}

	// Token: 0x060034A9 RID: 13481 RVA: 0x000DCF56 File Offset: 0x000DB156
	public void OnDisable()
	{
		SpecialAbilityZone.All.Remove(this);
	}

	// Token: 0x1700084B RID: 2123
	// (get) Token: 0x060034AA RID: 13482 RVA: 0x000DCF63 File Offset: 0x000DB163
	public Rect Bounds
	{
		get
		{
			return this.m_bounds;
		}
	}

	// Token: 0x04002F77 RID: 12151
	private bool m_inside;

	// Token: 0x04002F78 RID: 12152
	private Rect m_bounds;

	// Token: 0x04002F79 RID: 12153
	public bool IsRainbowZone;

	// Token: 0x04002F7A RID: 12154
	public static AllContainer<SpecialAbilityZone> All = new AllContainer<SpecialAbilityZone>();
}
