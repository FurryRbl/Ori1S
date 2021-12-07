using System;
using UnityEngine;

// Token: 0x02000585 RID: 1413
[Serializable]
public class KamikazeSootEnemySettings
{
	// Token: 0x04001EA0 RID: 7840
	public float InRange = 7f;

	// Token: 0x04001EA1 RID: 7841
	public float OutRange = 15f;

	// Token: 0x04001EA2 RID: 7842
	public float RollAcceleration = 60f;

	// Token: 0x04001EA3 RID: 7843
	public float RollDecceleration = 60f;

	// Token: 0x04001EA4 RID: 7844
	public float MaxRollSpeed = 30f;

	// Token: 0x04001EA5 RID: 7845
	public float RunAcceleration = 30f;

	// Token: 0x04001EA6 RID: 7846
	public float MaxRunSpeed = 10f;

	// Token: 0x04001EA7 RID: 7847
	public float WalkAcceleration = 30f;

	// Token: 0x04001EA8 RID: 7848
	public float MaxWalkSpeed = 10f;

	// Token: 0x04001EA9 RID: 7849
	public float Decceleration = 30f;

	// Token: 0x04001EAA RID: 7850
	public float Gravity = 60f;

	// Token: 0x04001EAB RID: 7851
	public float MaxFallSpeed = 30f;

	// Token: 0x04001EAC RID: 7852
	public float AlertDuration = 1f;

	// Token: 0x04001EAD RID: 7853
	public float RunDuration = 3f;

	// Token: 0x04001EAE RID: 7854
	public float AirDeceleration = 15f;

	// Token: 0x04001EAF RID: 7855
	public int ExplosionDamage = 10;

	// Token: 0x04001EB0 RID: 7856
	public GameObject RespawnEffect;
}
