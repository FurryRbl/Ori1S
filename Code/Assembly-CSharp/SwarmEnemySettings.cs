using System;
using UnityEngine;

// Token: 0x020005EF RID: 1519
[Serializable]
public class SwarmEnemySettings
{
	// Token: 0x040020BA RID: 8378
	public float Acceleration;

	// Token: 0x040020BB RID: 8379
	public float Decceleration;

	// Token: 0x040020BC RID: 8380
	public float Speed;

	// Token: 0x040020BD RID: 8381
	public GameObject Child;

	// Token: 0x040020BE RID: 8382
	public float AlertRange;

	// Token: 0x040020BF RID: 8383
	public float Gravity = 60f;

	// Token: 0x040020C0 RID: 8384
	public float JumpDelay;

	// Token: 0x040020C1 RID: 8385
	public float JumpStrength;

	// Token: 0x040020C2 RID: 8386
	public AnimationCurve MoveCurve;

	// Token: 0x040020C3 RID: 8387
	public float MaxFallSpeed = 30f;
}
