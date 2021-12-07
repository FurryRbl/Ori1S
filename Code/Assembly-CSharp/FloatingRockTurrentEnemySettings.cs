using System;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
[Serializable]
public class FloatingRockTurrentEnemySettings
{
	// Token: 0x04001F56 RID: 8022
	public float ChargeDuration;

	// Token: 0x04001F57 RID: 8023
	public float ShootingDuration;

	// Token: 0x04001F58 RID: 8024
	public float ShootingForce;

	// Token: 0x04001F59 RID: 8025
	public float SpringForce;

	// Token: 0x04001F5A RID: 8026
	public float Drag;

	// Token: 0x04001F5B RID: 8027
	public float DisolveDistance;

	// Token: 0x04001F5C RID: 8028
	public GameObject Projectile;

	// Token: 0x04001F5D RID: 8029
	public float ProjectileSpeed;

	// Token: 0x04001F5E RID: 8030
	public float ProjectileDamage = 5f;

	// Token: 0x04001F5F RID: 8031
	public GameObject RespawnEffect;
}
