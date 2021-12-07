using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class RestrictJumpingZone : MonoBehaviour
{
	// Token: 0x060001AC RID: 428 RVA: 0x0000779A File Offset: 0x0000599A
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000077B2 File Offset: 0x000059B2
	public bool Contains(Vector3 position)
	{
		return this.m_bounds.Contains(position);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000077C0 File Offset: 0x000059C0
	public void OnEnable()
	{
		RestrictJumpingZone.All.Add(this);
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000077CD File Offset: 0x000059CD
	public void OnDisable()
	{
		RestrictJumpingZone.All.Remove(this);
	}

	// Token: 0x04000146 RID: 326
	public static List<RestrictJumpingZone> All = new List<RestrictJumpingZone>();

	// Token: 0x04000147 RID: 327
	private Rect m_bounds;
}
