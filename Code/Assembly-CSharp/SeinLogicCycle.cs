using System;
using Game;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class SeinLogicCycle : MonoBehaviour
{
	// Token: 0x06001F07 RID: 7943 RVA: 0x00088ADC File Offset: 0x00086CDC
	public void Start()
	{
		this.Sein = Characters.Sein;
	}

	// Token: 0x17000545 RID: 1349
	// (get) Token: 0x06001F08 RID: 7944 RVA: 0x00088AE9 File Offset: 0x00086CE9
	public SeinMortality Mortality
	{
		get
		{
			return this.Sein.Mortality;
		}
	}

	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00088AF6 File Offset: 0x00086CF6
	public SeinAbilities Abilities
	{
		get
		{
			return this.Sein.Abilities;
		}
	}

	// Token: 0x17000547 RID: 1351
	// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00088B03 File Offset: 0x00086D03
	public PlatformBehaviour PlatformBehaviour
	{
		get
		{
			return this.Sein.PlatformBehaviour;
		}
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x00088B10 File Offset: 0x00086D10
	public void FixedUpdate()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		SeinAbilities abilities = this.Abilities;
		this.PlatformBehaviour.Gravity.SetStateActive(this.AllowGravity);
		this.PlatformBehaviour.GravityToGround.SetStateActive(this.AllowGravityToGround);
		this.PlatformBehaviour.InstantStop.SetStateActive(this.AllowInstantStop);
		this.PlatformBehaviour.LeftRightMovement.SetStateActive(this.AllowLeftRightMovement);
		this.PlatformBehaviour.AirNoDeceleration.SetStateActive(this.AllowAirNoDeceleration);
		this.PlatformBehaviour.ApplyFrictionToSpeed.SetStateActive(this.ApplyFrictionToSpeed);
		abilities.StandardSpiritFlame.SetStateActive(this.AllowStandardSpiritFlame);
		abilities.Bash.SetStateActive(this.AllowBash);
		abilities.LookUp.SetStateActive(this.AllowLooking);
		abilities.Lever.SetStateActive(this.AllowLever);
		abilities.Footsteps.SetStateActive(this.AllowFootsteps);
		abilities.SpiritFlameTargetting.SetStateActive(this.AllowSpiritFlameTargetting);
		abilities.ChargeFlame.SetStateActive(this.AllowChargeFlame);
		abilities.WallSlide.SetStateActive(this.AllowWallSlide);
		abilities.Stomp.SetStateActive(this.AllowStomp);
		abilities.Carry.SetStateActive(this.AllowCarry);
		abilities.Fall.SetStateActive(this.AllowFall);
		abilities.GrabBlock.SetStateActive(this.AllowGrabBlock);
		abilities.Idle.SetStateActive(this.AllowIdle);
		abilities.Run.SetStateActive(this.AllowRun);
		abilities.Crouch.SetStateActive(this.AllowCrouching);
		abilities.GrabWall.SetStateActive(this.AllowWallGrabbing);
		abilities.Jump.SetStateActive(this.AllowJumping);
		abilities.DoubleJump.SetStateActive(this.AllowDoubleJump);
		abilities.Glide.SetStateActive(this.AllowGliding);
		abilities.WallJump.SetStateActive(this.AllowWallJump);
		abilities.ChargeJumpCharging.SetStateActive(this.AllowChargeJumpCharging);
		abilities.ChargeJump.SetStateActive(this.AllowChargeJump);
		abilities.WallChargeJump.SetStateActive(this.AllowWallChargeJump);
		abilities.StandingOnEdge.SetStateActive(this.AllowStandingOnEdge);
		abilities.PushAgainstWall.SetStateActive(this.AllowPushAgainstWall);
		abilities.EdgeClamber.SetStateActive(this.AllowEdgeClamber);
		this.Mortality.CrushDetector.SetStateActive(this.AllowCrushDetector);
		this.PlatformBehaviour.Visuals.SpriteRotater.SetStateActive(this.AllowSpriteRotater);
		this.Mortality.DamageReciever.SetStateActive(this.AllowDamageReciever);
		abilities.Invincibility.SetStateActive(this.AllowInvincibility);
		this.PlatformBehaviour.JumpSustain.SetStateActive(this.AllowJumpSustain);
		this.PlatformBehaviour.UpwardsDeceleration.SetStateActive(this.AllowUpwardsDeceleration);
		this.Sein.ForceController.SetStateActive(this.AllowForceController);
		abilities.Swimming.SetStateActive(this.AllowSwimming);
		abilities.Dash.SetStateActive(this.AllowDash);
		abilities.Grenade.SetStateActive(this.AllowGrenade);
		this.Sein.SoulFlame.SetStateActive(true);
		CharacterState.UpdateCharacterState(this.Mortality.CrushDetector);
		CharacterState.UpdateCharacterState(this.Mortality.DamageReciever);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.Gravity);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.GravityToGround);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.InstantStop);
		CharacterState.UpdateCharacterState(this.Abilities.Carry);
		CharacterState.UpdateCharacterState(this.Abilities.GrabBlock);
		CharacterState.UpdateCharacterState(this.Abilities.SpiritFlameTargetting);
		CharacterState.UpdateCharacterState(this.Abilities.SpiritFlame);
		CharacterState.UpdateCharacterState(this.Abilities.ChargeFlame);
		CharacterState.UpdateCharacterState(this.Abilities.StandardSpiritFlame);
		CharacterState.UpdateCharacterState(this.Abilities.IceSpiritFlame);
		CharacterState.UpdateCharacterState(this.Abilities.StandingOnEdge);
		CharacterState.UpdateCharacterState(this.Abilities.Glide);
		CharacterState.UpdateCharacterState(this.Abilities.Bash);
		CharacterState.UpdateCharacterState(this.Abilities.WallJump);
		CharacterState.UpdateCharacterState(this.Abilities.EdgeClamber);
		CharacterState.UpdateCharacterState(this.Abilities.DoubleJump);
		CharacterState.UpdateCharacterState(this.Abilities.ChargeJumpCharging);
		CharacterState.UpdateCharacterState(this.Abilities.ChargeJump);
		CharacterState.UpdateCharacterState(this.Abilities.WallChargeJump);
		CharacterState.UpdateCharacterState(this.Abilities.Jump);
		CharacterState.UpdateCharacterState(this.Abilities.Fall);
		CharacterState.UpdateCharacterState(this.Abilities.PushAgainstWall);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.AirNoDeceleration);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.ApplyFrictionToSpeed);
		CharacterState.UpdateCharacterState(this.Abilities.Crouch);
		CharacterState.UpdateCharacterState(this.Abilities.Invincibility);
		CharacterState.UpdateCharacterState(this.Abilities.Run);
		CharacterState.UpdateCharacterState(this.Abilities.Idle);
		CharacterState.UpdateCharacterState(this.Abilities.LookUp);
		CharacterState.UpdateCharacterState(this.Abilities.GrabWall);
		CharacterState.UpdateCharacterState(this.Abilities.Footsteps);
		CharacterState.UpdateCharacterState(this.Sein.Abilities.Lever);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.JumpSustain);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.UpwardsDeceleration);
		CharacterState.UpdateCharacterState(this.Sein.ForceController);
		CharacterState.UpdateCharacterState(this.Abilities.WallSlide);
		CharacterState.UpdateCharacterState(this.Abilities.Stomp);
		CharacterState.UpdateCharacterState(this.Abilities.Swimming);
		CharacterState.UpdateCharacterState(this.PlatformBehaviour.Visuals.SpriteRotater);
		CharacterState.UpdateCharacterState(this.Sein.SoulFlame);
		CharacterState.UpdateCharacterState(this.Abilities.Dash);
		CharacterState.UpdateCharacterState(this.Abilities.Grenade);
		this.Sein.Controller.HandleOffscreenIssue();
	}

	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0008912B File Offset: 0x0008732B
	public bool AllowInvincibility
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0008912E File Offset: 0x0008732E
	public bool AllowAirNoDeceleration
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00089131 File Offset: 0x00087331
	public bool ApplyFrictionToSpeed
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700054B RID: 1355
	// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00089134 File Offset: 0x00087334
	public bool AllowSpiritFlameTargetting
	{
		get
		{
			return this.Sein.PlayerAbilities.SpiritFlame.HasAbility && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsBashing;
		}
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0008918C File Offset: 0x0008738C
	public bool AllowCrushDetector
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000891A6 File Offset: 0x000873A6
	public bool AllowSpriteRotater
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700054E RID: 1358
	// (get) Token: 0x06001F12 RID: 7954 RVA: 0x000891A9 File Offset: 0x000873A9
	public bool AllowDamageReciever
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x1700054F RID: 1359
	// (get) Token: 0x06001F13 RID: 7955 RVA: 0x000891C3 File Offset: 0x000873C3
	public bool AllowJumpSustain
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000550 RID: 1360
	// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000891DD File Offset: 0x000873DD
	public bool AllowUpwardsDeceleration
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000551 RID: 1361
	// (get) Token: 0x06001F15 RID: 7957 RVA: 0x000891F7 File Offset: 0x000873F7
	public bool AllowForceController
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000552 RID: 1362
	// (get) Token: 0x06001F16 RID: 7958 RVA: 0x00089211 File Offset: 0x00087411
	public bool AllowGravity
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000553 RID: 1363
	// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00089226 File Offset: 0x00087426
	public bool AllowGravityToGround
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000554 RID: 1364
	// (get) Token: 0x06001F18 RID: 7960 RVA: 0x00089257 File Offset: 0x00087457
	public bool AllowSwimming
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000555 RID: 1365
	// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0008925C File Offset: 0x0008745C
	public bool AllowDash
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsGrabbingLever && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsPushPulling && !this.Sein.Controller.IsAimingGrenade && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsBashing && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities) && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.Dash) && this.Sein.Controller.CanMove;
		}
	}

	// Token: 0x17000556 RID: 1366
	// (get) Token: 0x06001F1A RID: 7962 RVA: 0x00089354 File Offset: 0x00087554
	public bool AllowGrenade
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsGrabbingLever && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsPushPulling && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities) && this.Sein.Controller.CanMove && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsStandingOnEdge && !this.Sein.Controller.IsDashing;
		}
	}

	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0008943E File Offset: 0x0008763E
	public bool AllowInstantStop
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000558 RID: 1368
	// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00089470 File Offset: 0x00087670
	public bool AllowLeftRightMovement
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation && (!this.Sein.Controller.IsSwimming || !this.Sein.Abilities.Swimming.IsUnderwater);
		}
	}

	// Token: 0x17000559 RID: 1369
	// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000894C8 File Offset: 0x000876C8
	public bool AllowBash
	{
		get
		{
			return this.Sein.PlayerAbilities.Bash.HasAbility && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsPushPulling && !this.Sein.Controller.IsGrabbingLever && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x1700055A RID: 1370
	// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00089550 File Offset: 0x00087750
	public bool AllowLooking
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x1700055B RID: 1371
	// (get) Token: 0x06001F1F RID: 7967 RVA: 0x000895A4 File Offset: 0x000877A4
	public bool AllowLever
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsPushPulling && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x1700055C RID: 1372
	// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0008963C File Offset: 0x0008783C
	public bool AllowFootsteps
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsSwimming;
		}
	}

	// Token: 0x1700055D RID: 1373
	// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00089670 File Offset: 0x00087870
	public bool AllowStandardSpiritFlame
	{
		get
		{
			return this.Sein.PlayerAbilities.SpiritFlame.HasAbility && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsBashing;
		}
	}

	// Token: 0x1700055E RID: 1374
	// (get) Token: 0x06001F22 RID: 7970 RVA: 0x000896C8 File Offset: 0x000878C8
	public bool AllowChargeFlame
	{
		get
		{
			return this.Sein.PlayerAbilities.ChargeFlame.HasAbility && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsBashing;
		}
	}

	// Token: 0x1700055F RID: 1375
	// (get) Token: 0x06001F23 RID: 7971 RVA: 0x00089720 File Offset: 0x00087920
	public bool AllowWallSlide
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsGliding && !this.Sein.Controller.IsStomping;
		}
	}

	// Token: 0x17000560 RID: 1376
	// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000897B8 File Offset: 0x000879B8
	public bool AllowStomp
	{
		get
		{
			return this.Sein.PlayerAbilities.Stomp.HasAbility && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsGrabbingWall && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x17000561 RID: 1377
	// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00089858 File Offset: 0x00087A58
	public bool AllowCarry
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x17000562 RID: 1378
	// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000898C4 File Offset: 0x00087AC4
	public bool AllowFall
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsBashing;
		}
	}

	// Token: 0x17000563 RID: 1379
	// (get) Token: 0x06001F27 RID: 7975 RVA: 0x00089918 File Offset: 0x00087B18
	public bool AllowGrabBlock
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x17000564 RID: 1380
	// (get) Token: 0x06001F28 RID: 7976 RVA: 0x000899B0 File Offset: 0x00087BB0
	public bool AllowIdle
	{
		get
		{
			return !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsPushPulling;
		}
	}

	// Token: 0x17000565 RID: 1381
	// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00089A34 File Offset: 0x00087C34
	public bool AllowRun
	{
		get
		{
			return !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsPushPulling;
		}
	}

	// Token: 0x17000566 RID: 1382
	// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00089AB8 File Offset: 0x00087CB8
	public bool AllowCrouching
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsAimingGrenade && !this.Sein.Controller.IsDashing;
		}
	}

	// Token: 0x17000567 RID: 1383
	// (get) Token: 0x06001F2B RID: 7979 RVA: 0x00089B50 File Offset: 0x00087D50
	public bool AllowWallGrabbing
	{
		get
		{
			return this.Sein.PlayerAbilities.Climb.HasAbility && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000568 RID: 1384
	// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00089BF0 File Offset: 0x00087DF0
	public bool AllowJumping
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000569 RID: 1385
	// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00089C44 File Offset: 0x00087E44
	public bool AllowDoubleJump
	{
		get
		{
			return this.Sein.PlayerAbilities.DoubleJump.HasAbility && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x1700056A RID: 1386
	// (get) Token: 0x06001F2E RID: 7982 RVA: 0x00089CCC File Offset: 0x00087ECC
	public bool AllowGliding
	{
		get
		{
			return this.Sein.PlayerAbilities.Glide.HasAbility && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsGrabbingWall && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsDashing;
		}
	}

	// Token: 0x1700056B RID: 1387
	// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00089D98 File Offset: 0x00087F98
	public bool AllowWallJump
	{
		get
		{
			return this.Sein.PlayerAbilities.WallJump.HasAbility && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsGliding && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x1700056C RID: 1388
	// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00089E35 File Offset: 0x00088035
	public bool AllowChargeJumpCharging
	{
		get
		{
			return this.AllowChargeJump || this.AllowDash;
		}
	}

	// Token: 0x1700056D RID: 1389
	// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00089E4C File Offset: 0x0008804C
	public bool AllowChargeJump
	{
		get
		{
			return this.Sein.PlayerAbilities.ChargeJump.HasAbility && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x1700056E RID: 1390
	// (get) Token: 0x06001F32 RID: 7986 RVA: 0x00089EEC File Offset: 0x000880EC
	public bool AllowWallChargeJump
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x1700056F RID: 1391
	// (get) Token: 0x06001F33 RID: 7987 RVA: 0x00089F70 File Offset: 0x00088170
	public bool AllowStandingOnEdge
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsStomping && !this.Sein.Controller.IsPlayingAnimation && !this.Sein.Controller.IsAimingGrenade;
		}
	}

	// Token: 0x17000570 RID: 1392
	// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00089FF4 File Offset: 0x000881F4
	public bool AllowPushAgainstWall
	{
		get
		{
			return !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x17000571 RID: 1393
	// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0008A048 File Offset: 0x00088248
	public bool AllowEdgeClamber
	{
		get
		{
			return !this.Sein.Controller.IsCarrying && !this.Sein.Controller.IsSwimming && !this.Sein.Controller.IsPlayingAnimation;
		}
	}

	// Token: 0x04001AF3 RID: 6899
	public SeinCharacter Sein;
}
