using System;
using Game;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class ShorterHintZone : MonoBehaviour
{
	// Token: 0x17000220 RID: 544
	// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002B658 File Offset: 0x00029858
	public static bool IsInside
	{
		get
		{
			if (Characters.Sein == null)
			{
				return false;
			}
			for (int i = 0; i < Zones.ShorterHintZones.Count; i++)
			{
				if (Zones.ShorterHintZones[i].Bounds.Contains(Characters.Sein.Position))
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0002B6BC File Offset: 0x000298BC
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0002B6D4 File Offset: 0x000298D4
	public void OnEnable()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Zones.ShorterHintZones.Add(this);
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0002B6EC File Offset: 0x000298EC
	public void OnDisable()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Zones.ShorterHintZones.Remove(this);
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0002B705 File Offset: 0x00029905
	public Rect Bounds
	{
		get
		{
			return this.m_bounds;
		}
	}

	// Token: 0x04000837 RID: 2103
	private Rect m_bounds;
}
