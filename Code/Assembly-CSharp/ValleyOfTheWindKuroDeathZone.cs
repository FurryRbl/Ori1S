using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009E9 RID: 2537
public class ValleyOfTheWindKuroDeathZone : MonoBehaviour
{
	// Token: 0x0600371D RID: 14109 RVA: 0x000E7550 File Offset: 0x000E5750
	public void Start()
	{
		this.Bounds = new Bounds(base.transform.position, base.transform.localScale);
	}

	// Token: 0x0600371E RID: 14110 RVA: 0x000E7580 File Offset: 0x000E5780
	public void FixedUpdate()
	{
		this.Bounds = new Bounds(base.transform.position, base.transform.localScale);
	}

	// Token: 0x0600371F RID: 14111 RVA: 0x000E75AE File Offset: 0x000E57AE
	public void OnEnable()
	{
		ValleyOfTheWindKuroDeathZone.All.Add(this);
	}

	// Token: 0x06003720 RID: 14112 RVA: 0x000E75BB File Offset: 0x000E57BB
	public void OnDisable()
	{
		ValleyOfTheWindKuroDeathZone.All.Remove(this);
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x000E75C9 File Offset: 0x000E57C9
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawSelectedTextFilled(base.transform, "Hide Zone", false);
	}

	// Token: 0x04003218 RID: 12824
	public Bounds Bounds;

	// Token: 0x04003219 RID: 12825
	public static List<ValleyOfTheWindKuroDeathZone> All = new List<ValleyOfTheWindKuroDeathZone>();
}
