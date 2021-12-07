using System;
using UnityEngine;

// Token: 0x0200051B RID: 1307
[Serializable]
public class DropSlugEnemySettings
{
	// Token: 0x04001D3D RID: 7485
	public AnimationCurve SpeedMultiplierOverTime;

	// Token: 0x04001D3E RID: 7486
	public float Speed;

	// Token: 0x04001D3F RID: 7487
	public float BelowOffset;

	// Token: 0x04001D40 RID: 7488
	public float AlertRange;

	// Token: 0x04001D41 RID: 7489
	public float FallRange;

	// Token: 0x04001D42 RID: 7490
	public float HorizontalMaxSpeed = 10f;

	// Token: 0x04001D43 RID: 7491
	public float HorizontalAcceleration = 100f;

	// Token: 0x04001D44 RID: 7492
	public float ExplosionDamage = 10f;

	// Token: 0x04001D45 RID: 7493
	public float ThrownGravity = 60f;

	// Token: 0x04001D46 RID: 7494
	public GameObject RespawnEffect;
}
