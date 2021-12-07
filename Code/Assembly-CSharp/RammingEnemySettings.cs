using System;
using UnityEngine;

// Token: 0x0200058F RID: 1423
[Serializable]
public class RammingEnemySettings
{
	// Token: 0x04001EDC RID: 7900
	public float AlertDuration = 0.5f;

	// Token: 0x04001EDD RID: 7901
	public float StunnedDuration = 2f;

	// Token: 0x04001EDE RID: 7902
	public float RunSpeed = 20f;

	// Token: 0x04001EDF RID: 7903
	public float BrakingDuration = 0.35f;

	// Token: 0x04001EE0 RID: 7904
	public float AccelerationDuration = 0.2f;

	// Token: 0x04001EE1 RID: 7905
	public float AlertRange = 3f;

	// Token: 0x04001EE2 RID: 7906
	public float Gravity = 26f;

	// Token: 0x04001EE3 RID: 7907
	public float HitWallStunSpeed = 10f;

	// Token: 0x04001EE4 RID: 7908
	public float KnockBackSpeed = 10f;

	// Token: 0x04001EE5 RID: 7909
	public float BouncingDuration = 0.5f;

	// Token: 0x04001EE6 RID: 7910
	public float KnockBackDuration = 0.8666f;

	// Token: 0x04001EE7 RID: 7911
	public float RecoverTime = 0.5f;

	// Token: 0x04001EE8 RID: 7912
	public float BashSpeed = 40f;

	// Token: 0x04001EE9 RID: 7913
	public float RetreatSpeed = 20f;

	// Token: 0x04001EEA RID: 7914
	public float RetreatDistance = 15f;

	// Token: 0x04001EEB RID: 7915
	public bool CanDieToLevelUpBlast = true;

	// Token: 0x04001EEC RID: 7916
	public AnimationCurve RunningSpeedMultipliedOverTime;

	// Token: 0x04001EED RID: 7917
	public AnimationCurve BrakingSpeedMultiplierOverTime;

	// Token: 0x04001EEE RID: 7918
	public AnimationCurve BouncingSpeedMultiplierOverTime;
}
