using System;
using UnityEngine;

// Token: 0x020004FB RID: 1275
[Serializable]
public class DashOwlEnemySettings
{
	// Token: 0x04001CCF RID: 7375
	public AnimationCurve DashCurve;

	// Token: 0x04001CD0 RID: 7376
	public AnimationCurve BounceCurve;

	// Token: 0x04001CD1 RID: 7377
	public float DashDistance = 10f;

	// Token: 0x04001CD2 RID: 7378
	public float DashRange = 20f;

	// Token: 0x04001CD3 RID: 7379
	public float MaxDistanceFromStartPosition = 15f;

	// Token: 0x04001CD4 RID: 7380
	public float DashAlertDelay = 0.5f;

	// Token: 0x04001CD5 RID: 7381
	public float MoveBackSpeed = 4f;

	// Token: 0x04001CD6 RID: 7382
	public int BashBounceDamage = 10;

	// Token: 0x04001CD7 RID: 7383
	public AnimationCurve FlyBackVertical;

	// Token: 0x04001CD8 RID: 7384
	public AnimationCurve FlyBackHorizontal;

	// Token: 0x04001CD9 RID: 7385
	public bool Perched = true;
}
