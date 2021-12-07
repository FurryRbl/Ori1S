using System;
using UnityEngine;

// Token: 0x020009B4 RID: 2484
public class CatAndMouseKuroLandZone : MonoBehaviour
{
	// Token: 0x06003623 RID: 13859 RVA: 0x000E30B0 File Offset: 0x000E12B0
	public void Awake()
	{
		this.Bounds = new Rect(base.transform.position.x - base.transform.lossyScale.x * 0.5f, base.transform.position.y - base.transform.lossyScale.y * 0.5f, base.transform.lossyScale.x, base.transform.lossyScale.y);
	}

	// Token: 0x040030B3 RID: 12467
	public BaseAnimator Animator;

	// Token: 0x040030B4 RID: 12468
	public Rect Bounds;
}
