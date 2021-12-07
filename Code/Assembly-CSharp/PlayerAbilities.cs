using System;
using Game;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class PlayerAbilities : SaveSerialize, ISeinReceiver
{
	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000EC0B File Offset: 0x0000CE0B
	// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000EC13 File Offset: 0x0000CE13
	public CharacterAbility[] Abilities { get; private set; }

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000EC1C File Offset: 0x0000CE1C
	public int OriStrength
	{
		get
		{
			if (this.UltraSplitFlame.HasAbility)
			{
				return 3;
			}
			if (this.CinderFlame.HasAbility)
			{
				return 2;
			}
			if (this.SparkFlame.HasAbility)
			{
				return 1;
			}
			return 0;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000EC55 File Offset: 0x0000CE55
	public int SplitFlameTargets
	{
		get
		{
			if (this.UltraSplitFlame.HasAbility)
			{
				return 4;
			}
			if (this.SplitFlameUpgrade.HasAbility)
			{
				return 2;
			}
			return 1;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000EC7C File Offset: 0x0000CE7C
	public float AttractionDistance
	{
		get
		{
			if (Characters.Sein.PlayerAbilities.UltraMagnet.HasAbility)
			{
				return 200f;
			}
			if (Characters.Sein.PlayerAbilities.Magnet.HasAbility)
			{
				return 8f;
			}
			return 0f;
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0000ECCC File Offset: 0x0000CECC
	public new void Awake()
	{
		base.Awake();
		this.Abilities = new CharacterAbility[]
		{
			this.Bash,
			this.ChargeFlame,
			this.WallJump,
			this.Stomp,
			this.DoubleJump,
			this.ChargeJump,
			this.Magnet,
			this.UltraMagnet,
			this.Climb,
			this.Glide,
			this.SpiritFlame,
			this.RapidFire,
			this.SoulEfficiency,
			this.WaterBreath,
			this.ChargeFlameBlast,
			this.ChargeFlameBurn,
			this.DoubleJumpUpgrade,
			this.BashBuff,
			this.UltraDefense,
			this.HealthEfficiency,
			this.Sense,
			this.StompUpgrade,
			this.QuickFlame,
			this.MapMarkers,
			this.EnergyEfficiency,
			this.HealthMarkers,
			this.EnergyMarkers,
			this.AbilityMarkers,
			this.Rekindle,
			this.Regroup,
			this.ChargeFlameEfficiency,
			this.UltraSoulFlame,
			this.SoulFlameEfficiency,
			this.SplitFlameUpgrade,
			this.SparkFlame,
			this.CinderFlame,
			this.UltraSplitFlame,
			this.Dash,
			this.Grenade,
			this.GrenadeUpgrade,
			this.ChargeDash,
			this.AirDash,
			this.GrenadeEfficiency
		};
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x0000EE94 File Offset: 0x0000D094
	public void SetAllAbilitys(bool abilityEnabled)
	{
		foreach (CharacterAbility characterAbility in this.Abilities)
		{
			characterAbility.HasAbility = abilityEnabled;
		}
		this.m_sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0000EED8 File Offset: 0x0000D0D8
	public override void Serialize(Archive ar)
	{
		try
		{
			foreach (CharacterAbility characterAbility in this.Abilities)
			{
				ar.Serialize(ref characterAbility.HasAbility);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		if (ar.Reading)
		{
			this.m_sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0000EF4C File Offset: 0x0000D14C
	public void SetAbility(AbilityType ability, bool value)
	{
		switch (ability)
		{
		case AbilityType.Bash:
			this.Bash.HasAbility = value;
			break;
		case AbilityType.ChargeFlame:
			this.ChargeFlame.HasAbility = value;
			break;
		case AbilityType.WallJump:
			this.WallJump.HasAbility = value;
			break;
		case AbilityType.Stomp:
			this.Stomp.HasAbility = value;
			break;
		case AbilityType.DoubleJump:
			this.DoubleJump.HasAbility = value;
			break;
		case AbilityType.ChargeJump:
			this.ChargeJump.HasAbility = value;
			break;
		case AbilityType.Magnet:
			this.Magnet.HasAbility = value;
			break;
		case AbilityType.UltraMagnet:
			this.UltraMagnet.HasAbility = value;
			break;
		case AbilityType.Climb:
			this.Climb.HasAbility = value;
			break;
		case AbilityType.Glide:
			this.Glide.HasAbility = value;
			break;
		case AbilityType.SpiritFlame:
			this.SpiritFlame.HasAbility = value;
			Characters.Ori.MoveOriToPlayer();
			break;
		case AbilityType.RapidFlame:
			this.RapidFire.HasAbility = value;
			break;
		case AbilityType.SplitFlameUpgrade:
			this.SplitFlameUpgrade.HasAbility = value;
			break;
		case AbilityType.SoulEfficiency:
			this.SoulEfficiency.HasAbility = value;
			break;
		case AbilityType.WaterBreath:
			this.WaterBreath.HasAbility = value;
			break;
		case AbilityType.ChargeFlameBlast:
			this.ChargeFlameBlast.HasAbility = value;
			break;
		case AbilityType.ChargeFlameBurn:
			this.ChargeFlameBurn.HasAbility = value;
			break;
		case AbilityType.DoubleJumpUpgrade:
			this.DoubleJumpUpgrade.HasAbility = value;
			break;
		case AbilityType.BashBuff:
			this.BashBuff.HasAbility = value;
			break;
		case AbilityType.UltraDefense:
			this.UltraDefense.HasAbility = value;
			break;
		case AbilityType.HealthEfficiency:
			this.HealthEfficiency.HasAbility = value;
			break;
		case AbilityType.Sense:
			this.Sense.HasAbility = value;
			break;
		case AbilityType.UltraStomp:
			this.StompUpgrade.HasAbility = value;
			break;
		case AbilityType.SparkFlame:
			this.SparkFlame.HasAbility = value;
			break;
		case AbilityType.QuickFlame:
			this.QuickFlame.HasAbility = value;
			break;
		case AbilityType.MapMarkers:
			this.MapMarkers.HasAbility = value;
			break;
		case AbilityType.EnergyEfficiency:
			this.EnergyEfficiency.HasAbility = value;
			break;
		case AbilityType.HealthMarkers:
			this.HealthMarkers.HasAbility = value;
			break;
		case AbilityType.EnergyMarkers:
			this.EnergyMarkers.HasAbility = value;
			break;
		case AbilityType.AbilityMarkers:
			this.AbilityMarkers.HasAbility = value;
			break;
		case AbilityType.Rekindle:
			this.Rekindle.HasAbility = value;
			break;
		case AbilityType.Regroup:
			this.Regroup.HasAbility = value;
			break;
		case AbilityType.ChargeFlameEfficiency:
			this.ChargeFlameEfficiency.HasAbility = value;
			break;
		case AbilityType.UltraSoulFlame:
			this.UltraSoulFlame.HasAbility = value;
			break;
		case AbilityType.SoulFlameEfficiency:
			this.SoulFlameEfficiency.HasAbility = value;
			break;
		case AbilityType.CinderFlame:
			this.CinderFlame.HasAbility = value;
			break;
		case AbilityType.UltraSplitFlame:
			this.UltraSplitFlame.HasAbility = value;
			break;
		case AbilityType.Dash:
			this.Dash.HasAbility = value;
			break;
		case AbilityType.Grenade:
			this.Grenade.HasAbility = value;
			break;
		case AbilityType.GrenadeUpgrade:
			this.GrenadeUpgrade.HasAbility = value;
			break;
		case AbilityType.ChargeDash:
			this.ChargeDash.HasAbility = value;
			break;
		case AbilityType.AirDash:
			this.AirDash.HasAbility = value;
			break;
		case AbilityType.GrenadeEfficiency:
			this.GrenadeEfficiency.HasAbility = value;
			break;
		}
		this.m_sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0000F340 File Offset: 0x0000D540
	public bool HasAbility(AbilityType ability)
	{
		switch (ability)
		{
		case AbilityType.Bash:
			return this.Bash.HasAbility;
		case AbilityType.ChargeFlame:
			return this.ChargeFlame.HasAbility;
		case AbilityType.WallJump:
			return this.WallJump.HasAbility;
		case AbilityType.Stomp:
			return this.Stomp.HasAbility;
		case AbilityType.DoubleJump:
			return this.DoubleJump.HasAbility;
		case AbilityType.ChargeJump:
			return this.ChargeJump.HasAbility;
		case AbilityType.Magnet:
			return this.Magnet.HasAbility;
		case AbilityType.UltraMagnet:
			return this.UltraMagnet.HasAbility;
		case AbilityType.Climb:
			return this.Climb.HasAbility;
		case AbilityType.Glide:
			return this.Glide.HasAbility;
		case AbilityType.SpiritFlame:
			return this.SpiritFlame.HasAbility;
		case AbilityType.RapidFlame:
			return this.RapidFire.HasAbility;
		case AbilityType.SplitFlameUpgrade:
			return this.SplitFlameUpgrade.HasAbility;
		case AbilityType.SoulEfficiency:
			return this.SoulEfficiency.HasAbility;
		case AbilityType.WaterBreath:
			return this.WaterBreath.HasAbility;
		case AbilityType.ChargeFlameBlast:
			return this.ChargeFlameBlast.HasAbility;
		case AbilityType.ChargeFlameBurn:
			return this.ChargeFlameBurn.HasAbility;
		case AbilityType.DoubleJumpUpgrade:
			return this.DoubleJumpUpgrade.HasAbility;
		case AbilityType.BashBuff:
			return this.BashBuff.HasAbility;
		case AbilityType.UltraDefense:
			return this.UltraDefense.HasAbility;
		case AbilityType.HealthEfficiency:
			return this.HealthEfficiency.HasAbility;
		case AbilityType.Sense:
			return this.Sense.HasAbility;
		case AbilityType.UltraStomp:
			return this.StompUpgrade.HasAbility;
		case AbilityType.SparkFlame:
			return this.SparkFlame.HasAbility;
		case AbilityType.QuickFlame:
			return this.QuickFlame.HasAbility;
		case AbilityType.MapMarkers:
			return this.MapMarkers.HasAbility;
		case AbilityType.EnergyEfficiency:
			return this.EnergyEfficiency.HasAbility;
		case AbilityType.HealthMarkers:
			return this.HealthMarkers.HasAbility;
		case AbilityType.EnergyMarkers:
			return this.EnergyMarkers.HasAbility;
		case AbilityType.AbilityMarkers:
			return this.AbilityMarkers.HasAbility;
		case AbilityType.Rekindle:
			return this.Rekindle.HasAbility;
		case AbilityType.Regroup:
			return this.Regroup.HasAbility;
		case AbilityType.ChargeFlameEfficiency:
			return this.ChargeFlameEfficiency.HasAbility;
		case AbilityType.UltraSoulFlame:
			return this.UltraSoulFlame.HasAbility;
		case AbilityType.SoulFlameEfficiency:
			return this.SoulFlameEfficiency.HasAbility;
		case AbilityType.CinderFlame:
			return this.CinderFlame.HasAbility;
		case AbilityType.UltraSplitFlame:
			return this.UltraSplitFlame.HasAbility;
		case AbilityType.Dash:
			return this.Dash.HasAbility;
		case AbilityType.Grenade:
			return this.Grenade.HasAbility;
		case AbilityType.GrenadeUpgrade:
			return this.GrenadeUpgrade.HasAbility;
		case AbilityType.ChargeDash:
			return this.ChargeDash.HasAbility;
		case AbilityType.AirDash:
			return this.AirDash.HasAbility;
		case AbilityType.GrenadeEfficiency:
			return this.GrenadeEfficiency.HasAbility;
		}
		return false;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0000F644 File Offset: 0x0000D844
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.PlayerAbilities = this;
	}

	// Token: 0x040002C2 RID: 706
	public CharacterAbility Bash;

	// Token: 0x040002C3 RID: 707
	public CharacterAbility ChargeFlame;

	// Token: 0x040002C4 RID: 708
	public CharacterAbility WallJump;

	// Token: 0x040002C5 RID: 709
	public CharacterAbility Stomp;

	// Token: 0x040002C6 RID: 710
	public CharacterAbility DoubleJump;

	// Token: 0x040002C7 RID: 711
	public CharacterAbility ChargeJump;

	// Token: 0x040002C8 RID: 712
	public CharacterAbility Magnet;

	// Token: 0x040002C9 RID: 713
	public CharacterAbility UltraMagnet;

	// Token: 0x040002CA RID: 714
	public CharacterAbility Climb;

	// Token: 0x040002CB RID: 715
	public CharacterAbility Glide;

	// Token: 0x040002CC RID: 716
	public CharacterAbility SpiritFlame;

	// Token: 0x040002CD RID: 717
	public CharacterAbility RapidFire;

	// Token: 0x040002CE RID: 718
	public CharacterAbility SoulEfficiency;

	// Token: 0x040002CF RID: 719
	public CharacterAbility WaterBreath;

	// Token: 0x040002D0 RID: 720
	public CharacterAbility ChargeFlameBlast;

	// Token: 0x040002D1 RID: 721
	public CharacterAbility ChargeFlameBurn;

	// Token: 0x040002D2 RID: 722
	public CharacterAbility DoubleJumpUpgrade;

	// Token: 0x040002D3 RID: 723
	public CharacterAbility BashBuff;

	// Token: 0x040002D4 RID: 724
	public CharacterAbility UltraDefense;

	// Token: 0x040002D5 RID: 725
	public CharacterAbility HealthEfficiency;

	// Token: 0x040002D6 RID: 726
	public CharacterAbility Sense;

	// Token: 0x040002D7 RID: 727
	public CharacterAbility StompUpgrade;

	// Token: 0x040002D8 RID: 728
	public CharacterAbility QuickFlame;

	// Token: 0x040002D9 RID: 729
	public CharacterAbility MapMarkers;

	// Token: 0x040002DA RID: 730
	public CharacterAbility EnergyEfficiency;

	// Token: 0x040002DB RID: 731
	public CharacterAbility HealthMarkers;

	// Token: 0x040002DC RID: 732
	public CharacterAbility EnergyMarkers;

	// Token: 0x040002DD RID: 733
	public CharacterAbility AbilityMarkers;

	// Token: 0x040002DE RID: 734
	public CharacterAbility Rekindle;

	// Token: 0x040002DF RID: 735
	public CharacterAbility Regroup;

	// Token: 0x040002E0 RID: 736
	public CharacterAbility ChargeFlameEfficiency;

	// Token: 0x040002E1 RID: 737
	public CharacterAbility UltraSoulFlame;

	// Token: 0x040002E2 RID: 738
	public CharacterAbility SoulFlameEfficiency;

	// Token: 0x040002E3 RID: 739
	public CharacterAbility SplitFlameUpgrade;

	// Token: 0x040002E4 RID: 740
	public CharacterAbility SparkFlame;

	// Token: 0x040002E5 RID: 741
	public CharacterAbility CinderFlame;

	// Token: 0x040002E6 RID: 742
	public CharacterAbility UltraSplitFlame;

	// Token: 0x040002E7 RID: 743
	public CharacterAbility Grenade;

	// Token: 0x040002E8 RID: 744
	public CharacterAbility Dash;

	// Token: 0x040002E9 RID: 745
	public CharacterAbility GrenadeUpgrade;

	// Token: 0x040002EA RID: 746
	public CharacterAbility ChargeDash;

	// Token: 0x040002EB RID: 747
	public CharacterAbility AirDash;

	// Token: 0x040002EC RID: 748
	public CharacterAbility GrenadeEfficiency;

	// Token: 0x040002ED RID: 749
	public ActionMethod GainAbilityAction;

	// Token: 0x040002EE RID: 750
	private SeinCharacter m_sein;
}
