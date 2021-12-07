using System;
using UnityEngine;

// Token: 0x02000724 RID: 1828
[Serializable]
public class ScreenshotIcon
{
	// Token: 0x06002B1B RID: 11035 RVA: 0x000B89E0 File Offset: 0x000B6BE0
	public ScreenshotIcon(ScreenshotIcon.IconType icon, Vector2 position)
	{
		this.Icon = icon;
		this.Position = position;
	}

	// Token: 0x040026BA RID: 9914
	public ScreenshotIcon.IconType Icon;

	// Token: 0x040026BB RID: 9915
	public Vector2 Position;

	// Token: 0x0200072D RID: 1837
	public enum IconType
	{
		// Token: 0x040026E9 RID: 9961
		HealthVessel,
		// Token: 0x040026EA RID: 9962
		EnergyVessel,
		// Token: 0x040026EB RID: 9963
		AbilityPoint,
		// Token: 0x040026EC RID: 9964
		EnergyPlantSmall,
		// Token: 0x040026ED RID: 9965
		EnergyPlantMedium,
		// Token: 0x040026EE RID: 9966
		EnergyPlantLarge,
		// Token: 0x040026EF RID: 9967
		LootPlant,
		// Token: 0x040026F0 RID: 9968
		PetrifiedPlant,
		// Token: 0x040026F1 RID: 9969
		ExpOrbSmall,
		// Token: 0x040026F2 RID: 9970
		ExpOrbMedium,
		// Token: 0x040026F3 RID: 9971
		ExpOrbLarge,
		// Token: 0x040026F4 RID: 9972
		Keystone,
		// Token: 0x040026F5 RID: 9973
		Mapstone,
		// Token: 0x040026F6 RID: 9974
		MapstonePickup,
		// Token: 0x040026F7 RID: 9975
		ChargeFlameWall,
		// Token: 0x040026F8 RID: 9976
		EnergyGateTwo,
		// Token: 0x040026F9 RID: 9977
		EnergyGateFour,
		// Token: 0x040026FA RID: 9978
		StompableFloor,
		// Token: 0x040026FB RID: 9979
		KeystoneDoorTwo,
		// Token: 0x040026FC RID: 9980
		KeystoneDoorFour,
		// Token: 0x040026FD RID: 9981
		AbilityTree,
		// Token: 0x040026FE RID: 9982
		SavePedestal
	}
}
