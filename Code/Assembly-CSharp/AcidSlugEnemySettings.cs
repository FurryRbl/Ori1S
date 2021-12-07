using System;
using UnityEngine;

// Token: 0x020005B7 RID: 1463
[Serializable]
public class AcidSlugEnemySettings
{
	// Token: 0x04001FC6 RID: 8134
	public float WalkSpeed = 4f;

	// Token: 0x04001FC7 RID: 8135
	public AnimationCurve WalkSpeedMultiplier;

	// Token: 0x04001FC8 RID: 8136
	public float AcidDripRate;

	// Token: 0x04001FC9 RID: 8137
	public GameObject AcidDrip;

	// Token: 0x04001FCA RID: 8138
	public SoundProvider AcidDripSoundProvider;

	// Token: 0x04001FCB RID: 8139
	public GameObject AcidDripOnDamage;

	// Token: 0x04001FCC RID: 8140
	public float AcidProjectileSpeed = 15f;

	// Token: 0x04001FCD RID: 8141
	public float AcidDamage;

	// Token: 0x04001FCE RID: 8142
	public GameObject ShootEffect;
}
