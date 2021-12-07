using System;

// Token: 0x020008F1 RID: 2289
public interface IPickupCollector
{
	// Token: 0x060032FA RID: 13050
	void OnCollectExpOrbPickup(ExpOrbPickup expOrbPickup);

	// Token: 0x060032FB RID: 13051
	void OnCollectSkillPointPickup(SkillPointPickup skillPointPickup);

	// Token: 0x060032FC RID: 13052
	void OnCollectEnergyOrbPickup(EnergyOrbPickup energyOrbPickup);

	// Token: 0x060032FD RID: 13053
	void OnCollectMaxEnergyContainerPickup(MaxEnergyContainerPickup energyOrbPickup);

	// Token: 0x060032FE RID: 13054
	void OnCollectKeystonePickup(KeystonePickup keystonePickup);

	// Token: 0x060032FF RID: 13055
	void OnCollectRestoreHealthPickup(RestoreHealthPickup restoreHealthPickup);

	// Token: 0x06003300 RID: 13056
	void OnCollectMaxHealthContainerPickup(MaxHealthContainerPickup maxHealthContainerPickup);

	// Token: 0x06003301 RID: 13057
	void OnCollectMapStonePickup(MapStonePickup mapStonePickup);
}
