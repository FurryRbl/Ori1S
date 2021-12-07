using System;

// Token: 0x020005FC RID: 1532
[Serializable]
public class MortarWormSettings
{
	// Token: 0x04002101 RID: 8449
	public float HideDistance = 5f;

	// Token: 0x04002102 RID: 8450
	public float ShootDelay = 0.3f;

	// Token: 0x04002103 RID: 8451
	public float MinHideTime = 1f;

	// Token: 0x04002104 RID: 8452
	public float ProjectileSpeed = 20f;

	// Token: 0x04002105 RID: 8453
	public float ProjectileGravity = 30f;

	// Token: 0x04002106 RID: 8454
	public float ProjectileDamage = 10f;

	// Token: 0x04002107 RID: 8455
	public bool CanTurnAround;

	// Token: 0x04002108 RID: 8456
	public float ChargingDuration = 0.8f;

	// Token: 0x04002109 RID: 8457
	public float ShootingDuration = 0.5f;

	// Token: 0x0400210A RID: 8458
	public float WaitBetweenShots = 2f;
}
