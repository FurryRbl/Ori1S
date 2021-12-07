using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class SeinBashAttack : CharacterState, ISeinReceiver
{
	// Token: 0x060003C2 RID: 962 RVA: 0x0000F6EC File Offset: 0x0000D8EC
	// Note: this type is marked as 'beforefieldinit'.
	static SeinBashAttack()
	{
		SeinBashAttack.OnBashAttackEvent = delegate(Vector2 A_0)
		{
		};
		SeinBashAttack.OnBashBegin = delegate()
		{
		};
		SeinBashAttack.OnBashEnemy = delegate(EntityTargetting A_0)
		{
		};
	}

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x060003C3 RID: 963 RVA: 0x0000F75F File Offset: 0x0000D95F
	// (remove) Token: 0x060003C4 RID: 964 RVA: 0x0000F776 File Offset: 0x0000D976
	public static event Action<Vector2> OnBashAttackEvent;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x060003C5 RID: 965 RVA: 0x0000F78D File Offset: 0x0000D98D
	// (remove) Token: 0x060003C6 RID: 966 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
	public static event Action OnBashBegin;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x060003C7 RID: 967 RVA: 0x0000F7BB File Offset: 0x0000D9BB
	// (remove) Token: 0x060003C8 RID: 968 RVA: 0x0000F7D2 File Offset: 0x0000D9D2
	public static event Action<EntityTargetting> OnBashEnemy;

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000F7E9 File Offset: 0x0000D9E9
	public Component TargetAsComponent
	{
		get
		{
			return this.Target as Component;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060003CA RID: 970 RVA: 0x0000F7F6 File Offset: 0x0000D9F6
	public CharacterAirNoDeceleration AirNoDeceleration
	{
		get
		{
			return this.Sein.PlatformBehaviour.AirNoDeceleration;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060003CB RID: 971 RVA: 0x0000F808 File Offset: 0x0000DA08
	public SeinDoubleJump DoubleJump
	{
		get
		{
			return this.Sein.Abilities.DoubleJump;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060003CC RID: 972 RVA: 0x0000F81A File Offset: 0x0000DA1A
	public CharacterApplyFrictionToSpeed ApplyFrictionToSpeed
	{
		get
		{
			return this.Sein.PlatformBehaviour.ApplyFrictionToSpeed;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060003CD RID: 973 RVA: 0x0000F82C File Offset: 0x0000DA2C
	public CharacterGravity Gravity
	{
		get
		{
			return this.Sein.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060003CE RID: 974 RVA: 0x0000F83E File Offset: 0x0000DA3E
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060003CF RID: 975 RVA: 0x0000F850 File Offset: 0x0000DA50
	public PlayerAbilities PlayerAbilities
	{
		get
		{
			return this.Sein.PlayerAbilities;
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000F85D File Offset: 0x0000DA5D
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000F86F File Offset: 0x0000DA6F
	public SeinController SeinController
	{
		get
		{
			return this.Sein.Controller;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000F87C File Offset: 0x0000DA7C
	public TextureAnimationWithTransitions BashChargeAnimation
	{
		get
		{
			Vector2 v = this.m_directionToTarget;
			float num = Mathf.Cos(0.3926991f);
			SeinBashAttack.DirectionalAnimationSet directionalAnimationSet = (!this.Sein.Controller.IsSwimming) ? this.BashChargeAnimationSet : this.SwimBashChargeAnimationSet;
			v.x = Mathf.Abs(v.x);
			if (Vector3.Dot(Vector3.up, v) > num)
			{
				return directionalAnimationSet.Up;
			}
			Vector3 vector = new Vector3(1f, 1f);
			if (Vector3.Dot(vector.normalized, v) > num)
			{
				return directionalAnimationSet.UpDiagonal;
			}
			if (Vector3.Dot(Vector3.right, v) > num)
			{
				return directionalAnimationSet.Horizontal;
			}
			Vector3 vector2 = new Vector3(1f, -1f);
			if (Vector3.Dot(vector2.normalized, v) > num)
			{
				return directionalAnimationSet.DownDiagonal;
			}
			if (Vector3.Dot(Vector3.down, v) > num)
			{
				return directionalAnimationSet.Down;
			}
			return directionalAnimationSet.Up;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000F998 File Offset: 0x0000DB98
	public TextureAnimationWithTransitions BashJumpAnimation
	{
		get
		{
			float angle = this.m_bashAngle + 90f;
			Vector2 v = MoonMath.Angle.VectorFromAngle(angle);
			float num = Mathf.Cos(0.3926991f);
			SeinBashAttack.DirectionalAnimationSet directionalAnimationSet = (!this.Sein.Controller.IsSwimming) ? this.BashJumpAnimationSet : this.SwimBashJumpAnimationSet;
			v.x = Mathf.Abs(v.x);
			if (Vector3.Dot(Vector3.up, v) > num)
			{
				return directionalAnimationSet.Up;
			}
			Vector3 vector = new Vector3(1f, 1f);
			if (Vector3.Dot(vector.normalized, v) > num)
			{
				return directionalAnimationSet.UpDiagonal;
			}
			if (Vector3.Dot(Vector3.right, v) > num)
			{
				return directionalAnimationSet.Horizontal;
			}
			Vector3 vector2 = new Vector3(1f, -1f);
			if (Vector3.Dot(vector2.normalized, v) > num)
			{
				return directionalAnimationSet.DownDiagonal;
			}
			if (Vector3.Dot(Vector3.down, v) > num)
			{
				return directionalAnimationSet.Down;
			}
			return directionalAnimationSet.Up;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000FABB File Offset: 0x0000DCBB
	// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
	public bool SpriteMirrorLock
	{
		get
		{
			return this.m_spriteMirrorLock;
		}
		set
		{
			if (this.m_spriteMirrorLock != value)
			{
				this.m_spriteMirrorLock = value;
				if (value)
				{
					this.Sein.PlatformBehaviour.Visuals.SpriteMirror.Lock++;
				}
				else
				{
					this.Sein.PlatformBehaviour.Visuals.SpriteMirror.Lock--;
				}
			}
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000FB34 File Offset: 0x0000DD34
	public bool CanBash
	{
		get
		{
			return this.PlayerAbilities.Bash.HasAbility && !(this.TargetAsComponent == null) && this.TargetAsComponent.gameObject.activeInHierarchy && (!(this.Sein != null) || this.Sein.Active) && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities);
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0000FBB3 File Offset: 0x0000DDB3
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.m_seinTransform = this.Sein.transform;
		this.Sein.Abilities.Bash = this;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
	public void Start()
	{
		this.m_hasStarted = true;
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
		this.Gravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyGravityPlatformMovementSettings;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0000FC38 File Offset: 0x0000DE38
	public new void OnDestroy()
	{
		base.OnDestroy();
		if (this.m_hasStarted)
		{
			Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
			this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
			this.Gravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyGravityPlatformMovementSettings;
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0000FC9A File Offset: 0x0000DE9A
	public void ModifyGravityPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (this.IsBashing)
		{
			settings.GravityStrength = 0f;
		}
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.IsBashing)
		{
			settings.LockInput = true;
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
	public void OnRestoreCheckpoint()
	{
		if (this.IsBashing)
		{
			this.ExitBash();
		}
		this.ApplyFrictionToSpeed.SpeedFactor = 0f;
		this.m_spriteMirrorLock = false;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0000FCFD File Offset: 0x0000DEFD
	public void OnDisable()
	{
		if (this.IsBashing)
		{
			this.ExitBash();
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0000FD10 File Offset: 0x0000DF10
	public void ExitBash()
	{
		if (GameController.Instance)
		{
			GameController.Instance.ResumeGameplay();
		}
		this.ApplyFrictionToSpeed.SpeedFactor = 0f;
		this.IsBashing = false;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0000FD50 File Offset: 0x0000DF50
	public void MovePlayerToTargetAndCreateEffect()
	{
		Component component = this.Target as Component;
		Vector3 vector = (!InstantiateUtility.IsDestroyed(component)) ? component.transform.position : this.PlatformMovement.Position;
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.BashFromFx);
		gameObject.transform.position = vector;
		Vector3 localScale = gameObject.transform.localScale;
		localScale.x = (vector - this.PlatformMovement.Position).magnitude;
		gameObject.transform.localScale = localScale;
		gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, MoonMath.Angle.AngleFromVector(this.PlatformMovement.Position - vector));
		if (!this.PlatformMovement.IsOnGround)
		{
			this.PlatformMovement.Position2D = vector;
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0000FE3C File Offset: 0x0000E03C
	public void BeginBash()
	{
		this.m_timeRemainingOfBashButtonPress = 0f;
		this.IsBashing = true;
		this.Target.OnEnterBash();
		Transform transform = this.TargetAsComponent.transform;
		Sound.Play((!this.Sein.PlayerAbilities.BashBuff.HasAbility) ? this.BashStartSound.GetSound(null) : this.UpgradedBashStartSound.GetSound(null), this.m_seinTransform.position, null);
		if (GameController.Instance)
		{
			GameController.Instance.SuspendGameplay();
		}
		if (UI.Cameras.Current != null)
		{
			SuspensionManager.GetSuspendables(this.m_bashSuspendables, UI.Cameras.Current.GameObject);
			SuspensionManager.Resume(this.m_bashSuspendables);
			this.m_bashSuspendables.Clear();
		}
		this.PlatformMovement.LocalSpeed = Vector2.zero;
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.BashAttackGamePrefab);
		this.m_bashAttackGame = gameObject.GetComponent<BashAttackGame>();
		this.m_bashAttackGame.SendDirection(transform.position - this.PlatformMovement.Position);
		this.m_bashAttackGame.BashGameComplete += this.BashGameComplete;
		this.m_bashAttackGame.transform.position = transform.position;
		Vector3 b = Vector3.ClampMagnitude(transform.position - this.PlatformMovement.Position, 2f);
		this.m_playerTargetPosition = transform.position - b;
		this.m_directionToTarget = b.normalized;
		SeinBashAttack.OnBashBegin();
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.BashChargeAnimation, 10, new Func<bool>(this.ShouldBashChargeAnimationKeepPlaying), false);
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00010009 File Offset: 0x0000E209
	public void BashGameComplete(float angle)
	{
		this.JumpOffTarget(angle);
		this.AttackTarget();
		this.ExitBash();
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00010020 File Offset: 0x0000E220
	public void JumpOffTarget(float angle)
	{
		if (GameController.Instance)
		{
			GameController.Instance.ResumeGameplay();
		}
		Vector2 vector = Quaternion.Euler(0f, 0f, angle) * Vector2.up;
		Vector2 vector2 = vector * this.BashVelocity;
		this.PlatformMovement.WorldSpeed = vector2;
		this.AirNoDeceleration.NoDeceleration = true;
		this.Sein.ResetAirLimits();
		this.m_frictionTimeRemaining = this.FrictionDuration;
		this.ApplyFrictionToSpeed.SpeedToSlowDown = this.PlatformMovement.LocalSpeed;
		this.MovePlayerToTargetAndCreateEffect();
		Component component = this.Target as Component;
		Vector3 position = (!InstantiateUtility.IsDestroyed(component)) ? component.transform.position : this.Sein.Position;
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.BashOffFx);
		gameObject.transform.position = position;
		Vector3 localScale = gameObject.transform.localScale;
		localScale.x = vector2.magnitude * 0.1f;
		gameObject.transform.localScale = localScale;
		gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, MoonMath.Angle.AngleFromVector(vector));
		if (this.BashReleaseEffect)
		{
			GameObject gameObject2 = (GameObject)InstantiateUtility.Instantiate(this.BashReleaseEffect);
			gameObject2.transform.position = position;
		}
		SeinBashAttack.OnBashAttackEvent(vector2);
		this.m_timeRemainingTillNextBash = this.DelayTillNextBash;
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.BashJumpAnimation, 10, new Func<bool>(this.ShouldBashJumpAnimationKeepPlaying));
		characterAnimationState.OnStartPlaying = new Action(this.OnAnimationStart);
		characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
		this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft = (vector2.x > 0f);
		if (this.Sein.Abilities.Swimming)
		{
			this.Sein.Abilities.Swimming.OnBash(angle);
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0001025F File Offset: 0x0000E45F
	public void OnAnimationStart()
	{
		this.SpriteMirrorLock = true;
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x00010268 File Offset: 0x0000E468
	public void AttackTarget()
	{
		Component component = this.Target as Component;
		if (!InstantiateUtility.IsDestroyed(component))
		{
			Vector2 force = -MoonMath.Angle.VectorFromAngle(this.m_bashAngle + 90f) * 4f;
			Damage damage = new Damage((!this.Sein.PlayerAbilities.BashBuff.HasAbility) ? this.Damage : this.UpgradedDamage, force, Characters.Sein.Position, DamageType.Bash, base.gameObject);
			damage.DealToComponents(component.gameObject);
			EntityTargetting component2 = component.gameObject.GetComponent<EntityTargetting>();
			if (component2 && component2.Entity is Enemy)
			{
				SeinBashAttack.OnBashEnemy(component2);
			}
			if (this.Sein.PlayerAbilities.BashBuff.HasAbility)
			{
				this.BeginBashThroughEnemies();
			}
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0001034F File Offset: 0x0000E54F
	private void BeginBashThroughEnemies()
	{
		this.m_bashThroughEnemiesRemainingTime = 0.5f;
		this.Sein.Mortality.DamageReciever.MakeInvincibleToEnemies(this.m_bashThroughEnemiesRemainingTime);
		this.m_enemiesBashedThrough.Clear();
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00010384 File Offset: 0x0000E584
	public void UpdateBashThroughEnemies()
	{
		if (this.m_bashThroughEnemiesRemainingTime > 0f)
		{
			this.m_bashThroughEnemiesRemainingTime -= Time.deltaTime;
			for (int i = 0; i < Targets.Attackables.Count; i++)
			{
				IAttackable attackable = Targets.Attackables[i];
				if (attackable.CanBeSpiritFlamed())
				{
					if (!this.m_enemiesBashedThrough.Contains(attackable))
					{
						Vector3 vector = attackable.Position - this.Sein.PlatformBehaviour.PlatformMovement.Position;
						float magnitude = vector.magnitude;
						if (magnitude < 3f && Vector2.Dot(vector.normalized, this.PlatformMovement.LocalSpeed.normalized) > 0f)
						{
							Damage damage = new Damage(this.UpgradedDamage, this.PlatformMovement.WorldSpeed.normalized, this.Sein.Position, DamageType.SpiritFlame, base.gameObject);
							GameObject gameObject = ((Component)attackable).gameObject;
							damage.DealToComponents(gameObject);
							this.m_enemiesBashedThrough.Add(attackable);
							break;
						}
					}
				}
			}
			if (this.m_bashThroughEnemiesRemainingTime <= 0f)
			{
				this.m_bashThroughEnemiesRemainingTime = 0f;
				this.FinishBashThroughEnemies();
			}
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x000104E0 File Offset: 0x0000E6E0
	private void FinishBashThroughEnemies()
	{
		this.m_enemiesBashedThrough.Clear();
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x000104F0 File Offset: 0x0000E6F0
	public void UpdateBashingState()
	{
		this.HandleBashAngle();
		this.Sein.Mortality.DamageReciever.MakeInvincibleToEnemies(0.2f);
		this.HandleMovingTowardsBashTarget();
		this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft = (this.m_directionToTarget.x < 0f);
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00010550 File Offset: 0x0000E750
	public void BashFailed()
	{
		if (this.NoBashTargetEffect)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.NoBashTargetEffect, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.m_seinTransform;
		}
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000105A0 File Offset: 0x0000E7A0
	public void UpdateNormalState()
	{
		if (Core.Input.Bash.OnPressed)
		{
			this.m_timeRemainingOfBashButtonPress = 0.5f;
			if (this.Sein.IsOnGround && this.Sein.Speed.x == 0f && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities) && !this.Sein.Abilities.Carry.IsCarrying)
			{
				this.Sein.Animation.Play(this.BackFlipAnimation, 10, null);
				this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY = this.BackFlipSpeed;
				if ((!this.Sein.PlayerAbilities.BashBuff.HasAbility) ? this.StationaryBashSound : this.UpgradedStationaryBashSound)
				{
					Sound.Play((!this.Sein.PlayerAbilities.BashBuff.HasAbility) ? this.StationaryBashSound.GetSound(null) : this.UpgradedStationaryBashSound.GetSound(null), base.transform.position, null);
				}
			}
		}
		if (this.m_timeRemainingOfBashButtonPress > 0f)
		{
			this.m_timeRemainingOfBashButtonPress -= Time.deltaTime;
			if ((Core.Input.Bash.OnReleased || ((double)this.m_timeRemainingOfBashButtonPress <= 0.4 && (double)this.m_timeRemainingOfBashButtonPress >= 0.4 - (double)Time.deltaTime)) && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities) && !this.Sein.Abilities.Carry.IsCarrying)
			{
				this.BashFailed();
			}
			if (Core.Input.Bash.Released || this.m_timeRemainingOfBashButtonPress <= 0f)
			{
				this.m_timeRemainingOfBashButtonPress = 0f;
			}
		}
		if (this.m_timeRemainingOfBashButtonPress > 0f && this.CanBash)
		{
			this.BeginBash();
		}
		this.HandleFindingTarget();
		this.UpdateTargetHighlight(this.Target);
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000107B8 File Offset: 0x0000E9B8
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		if (!this.Sein.PlayerAbilities.Bash.HasAbility)
		{
			return;
		}
		if (!this.Sein.Active)
		{
			this.ExitBash();
			return;
		}
		if (this.m_timeRemainingTillNextBash > 0f)
		{
			this.m_timeRemainingTillNextBash -= Time.deltaTime;
		}
		this.UpdateBashThroughEnemies();
		if (this.m_frictionTimeRemaining > 0f)
		{
			this.m_frictionTimeRemaining -= Time.deltaTime;
			float time = this.FrictionDuration - this.m_frictionTimeRemaining;
			this.ApplyFrictionToSpeed.SpeedFactor = this.FrictionCurve.Evaluate(time);
		}
		if (this.m_frictionTimeRemaining + this.NoAirDecelerationDuration - this.FrictionDuration > 0f)
		{
			this.AirNoDeceleration.NoDeceleration = true;
		}
		if (this.IsBashing)
		{
			this.UpdateBashingState();
		}
		else
		{
			this.UpdateNormalState();
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000108C0 File Offset: 0x0000EAC0
	public void HandleMovingTowardsBashTarget()
	{
		Vector3 a = this.m_playerTargetPosition - this.PlatformMovement.Position;
		this.PlatformMovement.WorldSpeed = a / Time.deltaTime * 0.1f;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00010909 File Offset: 0x0000EB09
	private void HandleBashAngle()
	{
		if (!InstantiateUtility.IsDestroyed(this.m_bashAttackGame))
		{
			this.m_bashAngle = this.m_bashAttackGame.Angle;
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0001092C File Offset: 0x0000EB2C
	private void HandleFindingTarget()
	{
		if (this.Sein.Controller.IsCarrying)
		{
			this.Target = null;
		}
		else if (this.m_timeRemainingTillNextBash > 0f)
		{
			this.Target = null;
		}
		else if (this.PlayerAbilities.Bash.HasAbility)
		{
			this.Target = this.FindClosestAttackHandler();
		}
		else
		{
			this.Target = null;
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000109A4 File Offset: 0x0000EBA4
	private void UpdateTargetHighlight(IBashAttackable target)
	{
		if (this.m_lastTarget == target)
		{
			return;
		}
		if (!InstantiateUtility.IsDestroyed(this.m_lastTarget as Component))
		{
			this.m_lastTarget.OnBashDehighlight();
		}
		this.m_lastTarget = target;
		if (!InstantiateUtility.IsDestroyed(this.m_lastTarget as Component))
		{
			this.m_lastTarget.OnBashHighlight();
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00010A08 File Offset: 0x0000EC08
	private IBashAttackable FindClosestAttackHandler()
	{
		IBashAttackable result = null;
		float num = float.MaxValue;
		int num2 = int.MinValue;
		Vector3 position = this.Sein.Position;
		for (int i = 0; i < Targets.Attackables.Count; i++)
		{
			IAttackable attackable = Targets.Attackables[i];
			if (attackable.CanBeBashed())
			{
				float magnitude = (attackable.Position - position).magnitude;
				if (magnitude <= this.Range)
				{
					IBashAttackable bashAttackable = attackable as IBashAttackable;
					if (bashAttackable != null)
					{
						int bashPriority = bashAttackable.BashPriority;
						if (bashPriority > num2 || (magnitude <= num && bashPriority == num2))
						{
							if (this.Sein.Controller.RayTest(((Component)bashAttackable).gameObject))
							{
								num = magnitude;
								num2 = bashPriority;
								result = bashAttackable;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00010AFD File Offset: 0x0000ECFD
	private bool ShouldBashChargeAnimationKeepPlaying()
	{
		return this.IsBashing;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00010B05 File Offset: 0x0000ED05
	private bool ShouldBashJumpAnimationKeepPlaying()
	{
		return !this.PlatformMovement.IsOnGround;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00010B15 File Offset: 0x0000ED15
	private void OnAnimationEnd()
	{
		this.SpriteMirrorLock = false;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00010B20 File Offset: 0x0000ED20
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_timeRemainingOfBashButtonPress);
		ar.Serialize(ref this.m_frictionTimeRemaining);
		ar.Serialize(ref this.m_timeRemainingTillNextBash);
		ar.Serialize(ref this.m_spriteMirrorLock);
		base.Serialize(ar);
		if (ar.Reading && !InstantiateUtility.IsDestroyed(this.m_bashAttackGame))
		{
			InstantiateUtility.Destroy(this.m_bashAttackGame.gameObject);
		}
	}

	// Token: 0x04000311 RID: 785
	public SeinBashAttack.DirectionalAnimationSet BashChargeAnimationSet;

	// Token: 0x04000312 RID: 786
	public SeinBashAttack.DirectionalAnimationSet BashJumpAnimationSet;

	// Token: 0x04000313 RID: 787
	public SeinBashAttack.DirectionalAnimationSet SwimBashChargeAnimationSet;

	// Token: 0x04000314 RID: 788
	public SeinBashAttack.DirectionalAnimationSet SwimBashJumpAnimationSet;

	// Token: 0x04000315 RID: 789
	public TextureAnimationWithTransitions BackFlipAnimation;

	// Token: 0x04000316 RID: 790
	public GameObject BashAttackGamePrefab;

	// Token: 0x04000317 RID: 791
	public SoundProvider BashEndSound;

	// Token: 0x04000318 RID: 792
	public SoundProvider BashLoopSound;

	// Token: 0x04000319 RID: 793
	public SoundProvider BashStartSound;

	// Token: 0x0400031A RID: 794
	public SoundProvider StationaryBashSound;

	// Token: 0x0400031B RID: 795
	public SoundProvider UpgradedBashEndSound;

	// Token: 0x0400031C RID: 796
	public SoundProvider UpgradedBashLoopSound;

	// Token: 0x0400031D RID: 797
	public SoundProvider UpgradedBashStartSound;

	// Token: 0x0400031E RID: 798
	public SoundProvider UpgradedStationaryBashSound;

	// Token: 0x0400031F RID: 799
	public GameObject BashFromFx;

	// Token: 0x04000320 RID: 800
	public GameObject BashOffFx;

	// Token: 0x04000321 RID: 801
	public GameObject BashReleaseEffect;

	// Token: 0x04000322 RID: 802
	public float BashVelocity = 56.568f;

	// Token: 0x04000323 RID: 803
	public float Damage = 2f;

	// Token: 0x04000324 RID: 804
	public float UpgradedDamage = 5f;

	// Token: 0x04000325 RID: 805
	public float DelayTillNextBash = 0.2f;

	// Token: 0x04000326 RID: 806
	public AnimationCurve FrictionCurve;

	// Token: 0x04000327 RID: 807
	public float FrictionDuration;

	// Token: 0x04000328 RID: 808
	public float NoAirDecelerationDuration = 0.2f;

	// Token: 0x04000329 RID: 809
	public float Range = 4f;

	// Token: 0x0400032A RID: 810
	public SeinCharacter Sein;

	// Token: 0x0400032B RID: 811
	public IBashAttackable Target;

	// Token: 0x0400032C RID: 812
	private Vector3 m_directionToTarget;

	// Token: 0x0400032D RID: 813
	private float m_bashAngle;

	// Token: 0x0400032E RID: 814
	private Vector3 m_playerTargetPosition;

	// Token: 0x0400032F RID: 815
	private BashAttackGame m_bashAttackGame;

	// Token: 0x04000330 RID: 816
	private float m_frictionTimeRemaining;

	// Token: 0x04000331 RID: 817
	private IBashAttackable m_lastTarget;

	// Token: 0x04000332 RID: 818
	private Transform m_seinTransform;

	// Token: 0x04000333 RID: 819
	private bool m_spriteMirrorLock;

	// Token: 0x04000334 RID: 820
	private float m_timeRemainingTillNextBash;

	// Token: 0x04000335 RID: 821
	private float m_timeRemainingOfBashButtonPress;

	// Token: 0x04000336 RID: 822
	private readonly HashSet<ISuspendable> m_bashSuspendables = new HashSet<ISuspendable>();

	// Token: 0x04000337 RID: 823
	public GameObject NoBashTargetEffect;

	// Token: 0x04000338 RID: 824
	public bool IsBashing;

	// Token: 0x04000339 RID: 825
	private float m_bashThroughEnemiesRemainingTime;

	// Token: 0x0400033A RID: 826
	private HashSet<IAttackable> m_enemiesBashedThrough = new HashSet<IAttackable>();

	// Token: 0x0400033B RID: 827
	private bool m_hasStarted;

	// Token: 0x0400033C RID: 828
	public float BackFlipSpeed = 5f;

	// Token: 0x0200042F RID: 1071
	[Serializable]
	public class DirectionalAnimationSet
	{
		// Token: 0x040019BA RID: 6586
		public TextureAnimationWithTransitions Down;

		// Token: 0x040019BB RID: 6587
		public TextureAnimationWithTransitions DownDiagonal;

		// Token: 0x040019BC RID: 6588
		public TextureAnimationWithTransitions Horizontal;

		// Token: 0x040019BD RID: 6589
		public TextureAnimationWithTransitions Up;

		// Token: 0x040019BE RID: 6590
		public TextureAnimationWithTransitions UpDiagonal;
	}
}
