using System;
using UnityEngine;

// Token: 0x020005CB RID: 1483
[Serializable]
public class StarSlugEnemySettings
{
	// Token: 0x04001FF6 RID: 8182
	public float WalkSpeed = 4f;

	// Token: 0x04001FF7 RID: 8183
	public AnimationCurve WalkSpeedMultiplier;

	// Token: 0x04001FF8 RID: 8184
	public float ProjectileSpeed = 10f;

	// Token: 0x04001FF9 RID: 8185
	public GameObject Projectile;

	// Token: 0x04001FFA RID: 8186
	public float BashedSlugSpeed;

	// Token: 0x04001FFB RID: 8187
	public GameObject ShootEffect;
}
