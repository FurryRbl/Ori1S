using System;
using Game;

// Token: 0x0200049E RID: 1182
public static class AbilityDebugMenuItems
{
	// Token: 0x06001FEC RID: 8172 RVA: 0x0008C0E4 File Offset: 0x0008A2E4
	public static void SetAllAbilities(bool enabled)
	{
		Characters.Sein.PlayerAbilities.SetAllAbilitys(enabled);
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06001FED RID: 8173 RVA: 0x0008C105 File Offset: 0x0008A305
	public static void ToggleAbilities()
	{
		AbilityDebugMenuItems.SetAllAbilities(!AbilityDebugMenuItems.AllAbilitiesGetter());
	}

	// Token: 0x06001FEE RID: 8174 RVA: 0x0008C114 File Offset: 0x0008A314
	public static void AllAbilitiesSetter(bool newValue)
	{
		AbilityDebugMenuItems.ToggleAbilities();
	}

	// Token: 0x06001FEF RID: 8175 RVA: 0x0008C11B File Offset: 0x0008A31B
	public static bool AllAbilitiesGetter()
	{
		return Characters.Sein.PlayerAbilities.Bash.HasAbility;
	}

	// Token: 0x06001FF0 RID: 8176 RVA: 0x0008C131 File Offset: 0x0008A331
	public static void SpiritFlameRapidFireUpgradeSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.RapidFire.HasAbility = newValue;
	}

	// Token: 0x06001FF1 RID: 8177 RVA: 0x0008C148 File Offset: 0x0008A348
	public static void SpiritFlameSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.SpiritFlame.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
		Characters.Ori.gameObject.SetActive(newValue);
	}

	// Token: 0x06001FF2 RID: 8178 RVA: 0x0008C189 File Offset: 0x0008A389
	public static bool SpiritFlameGetter()
	{
		return Characters.Sein.PlayerAbilities.SpiritFlame.HasAbility;
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x0008C1A0 File Offset: 0x0008A3A0
	public static void ChargeFlameSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.ChargeFlame.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x0008C1D1 File Offset: 0x0008A3D1
	public static bool ChargeFlameGetter()
	{
		return Characters.Sein.PlayerAbilities.ChargeFlame.HasAbility;
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x0008C1E8 File Offset: 0x0008A3E8
	public static void ClimbSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Climb.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x0008C21C File Offset: 0x0008A41C
	public static bool ClimbGetter()
	{
		return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.Climb.HasAbility;
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x0008C250 File Offset: 0x0008A450
	public static void ChargeJumpSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.ChargeJump.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x0008C284 File Offset: 0x0008A484
	public static bool ChargeJumpGetter()
	{
		return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.ChargeJump.HasAbility;
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x0008C2B8 File Offset: 0x0008A4B8
	public static void StompSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Stomp.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x0008C2EC File Offset: 0x0008A4EC
	public static bool StompGetter()
	{
		return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.Stomp.HasAbility;
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x0008C320 File Offset: 0x0008A520
	public static void WallJumpSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.WallJump.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x0008C354 File Offset: 0x0008A554
	public static bool WallJumpGetter()
	{
		return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.WallJump.HasAbility;
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x0008C388 File Offset: 0x0008A588
	public static void DoubleJumpSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.DoubleJump.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x0008C3BC File Offset: 0x0008A5BC
	public static bool DoubleJumpGetter()
	{
		return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.DoubleJump.HasAbility;
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x0008C3F0 File Offset: 0x0008A5F0
	public static bool BashGetter()
	{
		return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.Bash.HasAbility;
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x0008C424 File Offset: 0x0008A624
	public static void BashSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Bash.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x0008C455 File Offset: 0x0008A655
	public static bool GlideGetter()
	{
		return Characters.Sein.Abilities.Glide != null;
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x0008C46C File Offset: 0x0008A66C
	public static void GlideSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Glide.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x0008C4A0 File Offset: 0x0008A6A0
	public static void DashSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Dash.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x0008C4D1 File Offset: 0x0008A6D1
	public static bool DashGetter()
	{
		return Characters.Sein.PlayerAbilities.Dash.HasAbility;
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x0008C4E8 File Offset: 0x0008A6E8
	public static void GrenadeSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Grenade.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x0008C519 File Offset: 0x0008A719
	public static bool GrenadeGetter()
	{
		return Characters.Sein.PlayerAbilities.Grenade.HasAbility;
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x0008C52F File Offset: 0x0008A72F
	public static bool WaterBreathGetter()
	{
		return Characters.Sein.PlayerAbilities.WaterBreath.HasAbility;
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x0008C548 File Offset: 0x0008A748
	public static void WaterBreathSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.WaterBreath.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002009 RID: 8201 RVA: 0x0008C579 File Offset: 0x0008A779
	public static bool MagnetGetter()
	{
		return Characters.Sein.PlayerAbilities.Magnet.HasAbility;
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x0008C590 File Offset: 0x0008A790
	public static void MagnetSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Magnet.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x0008C5C1 File Offset: 0x0008A7C1
	public static bool UltraMagnetGetter()
	{
		return Characters.Sein.PlayerAbilities.UltraMagnet.HasAbility;
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x0008C5D8 File Offset: 0x0008A7D8
	public static void UltraMagnetSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.UltraMagnet.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x0008C609 File Offset: 0x0008A809
	public static bool RapidFireGetter()
	{
		return Characters.Sein.PlayerAbilities.RapidFire.HasAbility;
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x0008C620 File Offset: 0x0008A820
	public static void RapidFireSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.RapidFire.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x0008C651 File Offset: 0x0008A851
	public static bool SoulEfficiencyGetter()
	{
		return Characters.Sein.PlayerAbilities.SoulEfficiency.HasAbility;
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x0008C668 File Offset: 0x0008A868
	public static void SoulEfficiencySetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.SoulEfficiency.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x0008C699 File Offset: 0x0008A899
	public static bool ChargeFlameBlastGetter()
	{
		return Characters.Sein.PlayerAbilities.ChargeFlameBlast.HasAbility;
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x0008C6B0 File Offset: 0x0008A8B0
	public static void ChargeFlameBlastSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.ChargeFlameBlast.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002013 RID: 8211 RVA: 0x0008C6E1 File Offset: 0x0008A8E1
	public static bool DoubleJumpUpgradeGetter()
	{
		return Characters.Sein.PlayerAbilities.DoubleJumpUpgrade.HasAbility;
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x0008C6F8 File Offset: 0x0008A8F8
	public static void DoubleJumpUpgradeSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.DoubleJumpUpgrade.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x0008C729 File Offset: 0x0008A929
	public static bool BashUpgradeGetter()
	{
		return Characters.Sein.PlayerAbilities.BashBuff.HasAbility;
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x0008C740 File Offset: 0x0008A940
	public static void BashUpgradeSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.BashBuff.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x0008C771 File Offset: 0x0008A971
	public static bool UltraDefenseGetter()
	{
		return Characters.Sein.PlayerAbilities.UltraDefense.HasAbility;
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x0008C788 File Offset: 0x0008A988
	public static void UltraDefenseSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.UltraDefense.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x0008C7B9 File Offset: 0x0008A9B9
	public static bool HealthEfficiencyGetter()
	{
		return Characters.Sein.PlayerAbilities.HealthEfficiency.HasAbility;
	}

	// Token: 0x0600201A RID: 8218 RVA: 0x0008C7D0 File Offset: 0x0008A9D0
	public static void HealthEfficiencySetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.HealthEfficiency.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600201B RID: 8219 RVA: 0x0008C801 File Offset: 0x0008AA01
	public static bool SenseGetter()
	{
		return Characters.Sein.PlayerAbilities.Sense.HasAbility;
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x0008C818 File Offset: 0x0008AA18
	public static void SenseSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Sense.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x0008C849 File Offset: 0x0008AA49
	public static bool StompUpgradeGetter()
	{
		return Characters.Sein.PlayerAbilities.StompUpgrade.HasAbility;
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x0008C860 File Offset: 0x0008AA60
	public static void StompUpgradeSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.StompUpgrade.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600201F RID: 8223 RVA: 0x0008C891 File Offset: 0x0008AA91
	public static bool QuickFlameGetter()
	{
		return Characters.Sein.PlayerAbilities.QuickFlame.HasAbility;
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x0008C8A8 File Offset: 0x0008AAA8
	public static void QuickFlameSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.QuickFlame.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x0008C8D9 File Offset: 0x0008AAD9
	public static bool SparkFlameGetter()
	{
		return Characters.Sein.PlayerAbilities.SparkFlame.HasAbility;
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x0008C8F0 File Offset: 0x0008AAF0
	public static void SparkFlameSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.SparkFlame.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x0008C921 File Offset: 0x0008AB21
	public static bool SplitFlameUpgradeGetter()
	{
		return Characters.Sein.PlayerAbilities.SplitFlameUpgrade.HasAbility;
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x0008C938 File Offset: 0x0008AB38
	public static void SplitFlameUpgradeSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.SplitFlameUpgrade.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x0008C969 File Offset: 0x0008AB69
	public static bool CinderFlameGetter()
	{
		return Characters.Sein.PlayerAbilities.CinderFlame.HasAbility;
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x0008C980 File Offset: 0x0008AB80
	public static void CinderFlameSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.CinderFlame.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x0008C9B1 File Offset: 0x0008ABB1
	public static bool UltraSplitFlameGetter()
	{
		return Characters.Sein.PlayerAbilities.UltraSplitFlame.HasAbility;
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x0008C9C8 File Offset: 0x0008ABC8
	public static void UltraSplitFlameSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.UltraSplitFlame.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x0008C9F9 File Offset: 0x0008ABF9
	public static bool GrenadeUpgradeGetter()
	{
		return Characters.Sein.PlayerAbilities.GrenadeUpgrade.HasAbility;
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x0008CA0F File Offset: 0x0008AC0F
	public static void GrenadeUpgradeSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.GrenadeUpgrade.HasAbility = newValue;
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x0008CA26 File Offset: 0x0008AC26
	public static bool ChargeDashGetter()
	{
		return Characters.Sein.PlayerAbilities.ChargeDash.HasAbility;
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x0008CA3C File Offset: 0x0008AC3C
	public static void ChargeDashSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.ChargeDash.HasAbility = newValue;
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x0008CA53 File Offset: 0x0008AC53
	public static bool AirDashGetter()
	{
		return Characters.Sein.PlayerAbilities.AirDash.HasAbility;
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x0008CA69 File Offset: 0x0008AC69
	public static void AirDashSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.AirDash.HasAbility = newValue;
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x0008CA80 File Offset: 0x0008AC80
	public static bool GrenadeEfficiencyGetter()
	{
		return Characters.Sein.PlayerAbilities.GrenadeEfficiency.HasAbility;
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x0008CA96 File Offset: 0x0008AC96
	public static void GrenadeEfficiencySetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.GrenadeEfficiency.HasAbility = newValue;
	}

	// Token: 0x06002031 RID: 8241 RVA: 0x0008CAAD File Offset: 0x0008ACAD
	public static bool MapMarkersGetter()
	{
		return Characters.Sein.PlayerAbilities.MapMarkers.HasAbility;
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x0008CAC4 File Offset: 0x0008ACC4
	public static void MapMarkersSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.MapMarkers.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x0008CAF5 File Offset: 0x0008ACF5
	public static bool EnergyEfficiencyGetter()
	{
		return Characters.Sein.PlayerAbilities.EnergyEfficiency.HasAbility;
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x0008CB0C File Offset: 0x0008AD0C
	public static void EnergyEfficiencySetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.EnergyEfficiency.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x0008CB3D File Offset: 0x0008AD3D
	public static bool HealthMarkersGetter()
	{
		return Characters.Sein.PlayerAbilities.HealthMarkers.HasAbility;
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x0008CB54 File Offset: 0x0008AD54
	public static void HealthMarkersSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.HealthMarkers.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x0008CB85 File Offset: 0x0008AD85
	public static bool EnergyMarkersGetter()
	{
		return Characters.Sein.PlayerAbilities.EnergyMarkers.HasAbility;
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x0008CB9C File Offset: 0x0008AD9C
	public static void EnergyMarkersSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.EnergyMarkers.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x0008CBCD File Offset: 0x0008ADCD
	public static bool AbilityMarkersGetter()
	{
		return Characters.Sein.PlayerAbilities.AbilityMarkers.HasAbility;
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x0008CBE4 File Offset: 0x0008ADE4
	public static void AbilityMarkersSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.AbilityMarkers.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x0008CC15 File Offset: 0x0008AE15
	public static bool RekindleGetter()
	{
		return Characters.Sein.PlayerAbilities.Rekindle.HasAbility;
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x0008CC2C File Offset: 0x0008AE2C
	public static void RekindleSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Rekindle.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x0008CC5D File Offset: 0x0008AE5D
	public static bool RegroupGetter()
	{
		return Characters.Sein.PlayerAbilities.Regroup.HasAbility;
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x0008CC74 File Offset: 0x0008AE74
	public static void RegroupSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.Regroup.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x0008CCA5 File Offset: 0x0008AEA5
	public static bool ChargeFlameEfficiencyGetter()
	{
		return Characters.Sein.PlayerAbilities.ChargeFlameEfficiency.HasAbility;
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x0008CCBC File Offset: 0x0008AEBC
	public static void ChargeFlameEfficiencySetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.ChargeFlameEfficiency.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x0008CCED File Offset: 0x0008AEED
	public static bool SoulFlameEfficiencyGetter()
	{
		return Characters.Sein.PlayerAbilities.SoulFlameEfficiency.HasAbility;
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x0008CD04 File Offset: 0x0008AF04
	public static void SoulFlameEfficiencySetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.ChargeFlameEfficiency.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x0008CD35 File Offset: 0x0008AF35
	public static bool UltraSoulFlameGetter()
	{
		return Characters.Sein.PlayerAbilities.UltraSoulFlame.HasAbility;
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x0008CD4C File Offset: 0x0008AF4C
	public static void UltraSoulFlameSetter(bool newValue)
	{
		Characters.Sein.PlayerAbilities.UltraSoulFlame.HasAbility = newValue;
		Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}
}
