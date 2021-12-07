using System;
using UnityEngine;

// Token: 0x020005D3 RID: 1491
[Serializable]
public class ShootingSpiderSettings
{
	// Token: 0x04002013 RID: 8211
	public float ChargingDuration;

	// Token: 0x04002014 RID: 8212
	public float ChargingRange;

	// Token: 0x04002015 RID: 8213
	public float Gravity;

	// Token: 0x04002016 RID: 8214
	public GameObject Projectile;

	// Token: 0x04002017 RID: 8215
	public float ProjectileSpeed;

	// Token: 0x04002018 RID: 8216
	public float ShootingDuration;

	// Token: 0x04002019 RID: 8217
	public float ShootingImpulse;

	// Token: 0x0400201A RID: 8218
	public float ProjectileDamage = 5f;

	// Token: 0x0400201B RID: 8219
	public bool SpreadShot;

	// Token: 0x0400201C RID: 8220
	public GameObject RespawnEffect;
}
