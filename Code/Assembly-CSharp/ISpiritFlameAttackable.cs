using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
public interface ISpiritFlameAttackable
{
	// Token: 0x060005AA RID: 1450
	void OnSpiritFlameHighlight();

	// Token: 0x060005AB RID: 1451
	void OnSpiritFlameDehighlight();

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060005AC RID: 1452
	int SpiritFlamePriority { get; }

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x060005AD RID: 1453
	float OriDistanceFromAttackable { get; }

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x060005AE RID: 1454
	float SpiritFlameRange { get; }

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x060005AF RID: 1455
	bool RequiresSpiritFlameAbilityToTarget { get; }

	// Token: 0x060005B0 RID: 1456
	Vector3 GenerateSpiritFlameProjectileOffset(Vector3 position);
}
