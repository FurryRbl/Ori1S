using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class MoveHorizontallyZone : MonoBehaviour
{
	// Token: 0x060001A6 RID: 422 RVA: 0x00007745 File Offset: 0x00005945
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000775D File Offset: 0x0000595D
	public bool Contains(Vector3 position)
	{
		return this.m_bounds.Contains(position);
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000776B File Offset: 0x0000596B
	public void OnEnable()
	{
		MoveHorizontallyZone.All.Add(this);
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00007778 File Offset: 0x00005978
	public void OnDisable()
	{
		MoveHorizontallyZone.All.Remove(this);
	}

	// Token: 0x04000143 RID: 323
	public static List<MoveHorizontallyZone> All = new List<MoveHorizontallyZone>();

	// Token: 0x04000144 RID: 324
	public MoveHorizontallyZone.MoveDirection Direction;

	// Token: 0x04000145 RID: 325
	private Rect m_bounds;

	// Token: 0x02000023 RID: 35
	public enum MoveDirection
	{
		// Token: 0x0400017D RID: 381
		Left,
		// Token: 0x0400017E RID: 382
		Right
	}
}
