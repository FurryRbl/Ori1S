using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public interface IAttackable
{
	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000406 RID: 1030
	Vector3 Position { get; }

	// Token: 0x06000407 RID: 1031
	bool IsDead();

	// Token: 0x06000408 RID: 1032
	bool CanBeChargeFlamed();

	// Token: 0x06000409 RID: 1033
	bool CanBeChargeDashed();

	// Token: 0x0600040A RID: 1034
	bool CanBeGrenaded();

	// Token: 0x0600040B RID: 1035
	bool CanBeStomped();

	// Token: 0x0600040C RID: 1036
	bool CanBeBashed();

	// Token: 0x0600040D RID: 1037
	bool CanBeSpiritFlamed();

	// Token: 0x0600040E RID: 1038
	bool IsStompBouncable();

	// Token: 0x0600040F RID: 1039
	bool CanBeLevelUpBlasted();
}
