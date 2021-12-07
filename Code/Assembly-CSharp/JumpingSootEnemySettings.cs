using System;
using UnityEngine;

// Token: 0x0200057B RID: 1403
[Serializable]
public class JumpingSootEnemySettings
{
	// Token: 0x04001E71 RID: 7793
	public float ChargeRange = 20f;

	// Token: 0x04001E72 RID: 7794
	public float JumpHeight = 10f;

	// Token: 0x04001E73 RID: 7795
	public float ShortJumpHeight = 3f;

	// Token: 0x04001E74 RID: 7796
	public float JumpDistance = 20f;

	// Token: 0x04001E75 RID: 7797
	public float StompAttackDistance = 30f;

	// Token: 0x04001E76 RID: 7798
	public float Gravity = 30f;

	// Token: 0x04001E77 RID: 7799
	public float MaxFallSpeed = 30f;

	// Token: 0x04001E78 RID: 7800
	public float SphereCastRadius = 2f;

	// Token: 0x04001E79 RID: 7801
	public LayerMask SphereCastMask;

	// Token: 0x04001E7A RID: 7802
	public float ChargingDuration = 1f;

	// Token: 0x04001E7B RID: 7803
	public float StunnedDuration;

	// Token: 0x04001E7C RID: 7804
	public float ThrownDrag = 0.2f;

	// Token: 0x04001E7D RID: 7805
	public int GroundStompDamage = 10;

	// Token: 0x04001E7E RID: 7806
	public float ExplosionDamage = 5f;

	// Token: 0x04001E7F RID: 7807
	public bool HasStompExplosion = true;

	// Token: 0x04001E80 RID: 7808
	public GameObject RespawnEffect;
}
