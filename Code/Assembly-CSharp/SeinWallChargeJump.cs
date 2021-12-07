using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public class SeinWallChargeJump : CharacterState, ISeinReceiver
{
	// Token: 0x170004DE RID: 1246
	// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0007E9C3 File Offset: 0x0007CBC3
	public PlayerAbilities PlayerAbilities
	{
		get
		{
			return this.m_sein.PlayerAbilities;
		}
	}

	// Token: 0x170004DF RID: 1247
	// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0007E9D0 File Offset: 0x0007CBD0
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.m_sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0007E9E2 File Offset: 0x0007CBE2
	public void OnDoubleJump()
	{
		this.ChangeState(SeinWallChargeJump.State.Normal);
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x0007E9EB File Offset: 0x0007CBEB
	public override void UpdateCharacterState()
	{
		if (this.m_sein.IsSuspended)
		{
			return;
		}
		this.UpdateState();
	}

	// Token: 0x170004E0 RID: 1248
	// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0007EA04 File Offset: 0x0007CC04
	public float AngularElevation
	{
		get
		{
			return this.m_angularElevation;
		}
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x0007EA0C File Offset: 0x0007CC0C
	public override void OnExit()
	{
		base.OnExit();
		this.ChangeState(SeinWallChargeJump.State.Normal);
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x0007EA1B File Offset: 0x0007CC1B
	public void Start()
	{
		this.m_sein.PlatformBehaviour.Gravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyGravityPlatformMovementSettings;
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x0007EA3E File Offset: 0x0007CC3E
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.m_sein.PlatformBehaviour.Gravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyGravityPlatformMovementSettings;
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x0007EA7D File Offset: 0x0007CC7D
	public void OnAnimationEnd()
	{
		this.SpriteMirrorLock = false;
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x0007EA86 File Offset: 0x0007CC86
	public void OnAnimationStart()
	{
		this.SpriteMirrorLock = true;
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x0007EA8F File Offset: 0x0007CC8F
	public void ModifyGravityPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (this.m_currentState == SeinWallChargeJump.State.Jumping)
		{
			settings.GravityStrength = 0f;
		}
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x0007EAA8 File Offset: 0x0007CCA8
	public void ChangeState(SeinWallChargeJump.State state)
	{
		this.m_attackablesIgnore.Clear();
		SeinWallChargeJump.State currentState = this.m_currentState;
		if (currentState == SeinWallChargeJump.State.Aiming)
		{
			if (this.Arrow)
			{
				this.Arrow.AnimatorDriver.ContinueBackwards();
			}
		}
		this.m_currentState = state;
		this.m_stateCurrentTime = 0f;
		currentState = this.m_currentState;
		if (currentState != SeinWallChargeJump.State.Normal)
		{
			if (currentState == SeinWallChargeJump.State.Aiming)
			{
				if (this.m_sein.Abilities.GrabWall)
				{
					this.m_sein.Abilities.GrabWall.LockVerticalMovement = true;
				}
				if (this.Arrow)
				{
					this.Arrow.AnimatorDriver.ContinueForward();
				}
			}
		}
		else if (this.m_sein.Abilities.GrabWall)
		{
			this.m_sein.Abilities.GrabWall.LockVerticalMovement = false;
		}
	}

	// Token: 0x170004E1 RID: 1249
	// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0007EBB0 File Offset: 0x0007CDB0
	public bool IsCharged
	{
		get
		{
			return this.m_sein.Controller.IsGrabbingWall && this.m_sein.Abilities.GrabWall.IsGrabbingAway && Characters.Sein.Controller.CanMove && this.m_sein.Abilities.ChargeJumpCharging.IsCharged;
		}
	}

	// Token: 0x170004E2 RID: 1250
	// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0007EC18 File Offset: 0x0007CE18
	public bool IsCharging
	{
		get
		{
			return this.m_sein.Controller.IsGrabbingWall && this.m_sein.Abilities.GrabWall.IsGrabbingAway && Characters.Sein.Controller.CanMove && this.m_sein.Abilities.ChargeJumpCharging.IsCharging;
		}
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x0007EC80 File Offset: 0x0007CE80
	public void UpdateState()
	{
		switch (this.m_currentState)
		{
		case SeinWallChargeJump.State.Normal:
			this.UpdateNormalState();
			break;
		case SeinWallChargeJump.State.Aiming:
			this.UpdateAimingState();
			break;
		case SeinWallChargeJump.State.Jumping:
			this.UpdateJumpingState();
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x0007ECE0 File Offset: 0x0007CEE0
	private void UpdateNormalState()
	{
		if (this.PlayerAbilities.ChargeJump.HasAbility)
		{
			if (this.IsCharged)
			{
				this.ChangeState(SeinWallChargeJump.State.Aiming);
			}
			else if (this.IsCharging)
			{
				this.UpdateAimElevation();
			}
			else
			{
				this.m_angularElevation = 0f;
			}
		}
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x0007ED3C File Offset: 0x0007CF3C
	private void UpdateJumpingState()
	{
		this.PlatformMovement.LocalSpeedX = this.PlatformMovement.LocalSpeedX * (1f - this.HorizontalDrag);
		this.PlatformMovement.LocalSpeedY = this.PlatformMovement.LocalSpeedY * (1f - this.HorizontalDrag);
		if (this.m_stateCurrentTime > this.AntiGravityDuration)
		{
			this.ChangeState(SeinWallChargeJump.State.Normal);
			return;
		}
		this.m_sein.PlatformBehaviour.Visuals.SpriteRotater.CenterAngle = this.m_angleDirection;
		this.m_sein.PlatformBehaviour.Visuals.SpriteRotater.UpdateRotation();
		for (int i = 0; i < Targets.Attackables.Count; i++)
		{
			IAttackable attackable = Targets.Attackables[i];
			if (!this.m_attackablesIgnore.Contains(attackable))
			{
				if (attackable.CanBeStomped())
				{
					Vector3 vector = attackable.Position - this.m_sein.PlatformBehaviour.PlatformMovement.Position;
					float magnitude = vector.magnitude;
					if (magnitude < 4f && Vector2.Dot(vector.normalized, this.PlatformMovement.LocalSpeed.normalized) > 0f)
					{
						this.m_attackablesIgnore.Add(attackable);
						Damage damage = new Damage((float)this.Damage, this.PlatformMovement.WorldSpeed.normalized * 3f, this.m_sein.Position, DamageType.Stomp, base.gameObject);
						damage.DealToComponents(((Component)attackable).gameObject);
						if (this.ExplosionEffect)
						{
							InstantiateUtility.Instantiate(this.ExplosionEffect, Vector3.Lerp(base.transform.position, attackable.Position, 0.5f), Quaternion.identity);
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x0007EF38 File Offset: 0x0007D138
	public void UpdateAimElevation()
	{
		bool hasWallLeft = this.PlatformMovement.HasWallLeft;
		Vector2 analogAxisLeft = Core.Input.AnalogAxisLeft;
		if (analogAxisLeft.magnitude > 0.2f)
		{
			this.m_angularElevation = Mathf.Atan2(analogAxisLeft.y, analogAxisLeft.x * (float)((!hasWallLeft) ? -1 : 1)) * 57.29578f;
		}
		else if (Core.Input.Up.Pressed && !Core.Input.Down.Pressed)
		{
			this.m_angularElevationSpeed = Mathf.Clamp(this.m_angularElevationSpeed + Time.deltaTime * 500f, 0f, 200f);
		}
		else if (Core.Input.Down.Pressed)
		{
			this.m_angularElevationSpeed = Mathf.Clamp(this.m_angularElevationSpeed - Time.deltaTime * 500f, -200f, 0f);
		}
		else
		{
			this.m_angularElevationSpeed = 0f;
		}
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x0007F02C File Offset: 0x0007D22C
	private void UpdateAimingState()
	{
		if (!this.IsCharged)
		{
			this.ChangeState(SeinWallChargeJump.State.Normal);
		}
		if (this.Arrow)
		{
			this.UpdateAimElevation();
			bool hasWallLeft = this.PlatformMovement.HasWallLeft;
			this.m_angularElevation = Mathf.Clamp(this.m_angularElevation + this.m_angularElevationSpeed * Time.deltaTime, -45f, 45f);
			this.Arrow.transform.eulerAngles = new Vector3(0f, 0f, (!hasWallLeft) ? (180f - this.m_angularElevation) : this.m_angularElevation);
		}
	}

	// Token: 0x170004E3 RID: 1251
	// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0007F0D4 File Offset: 0x0007D2D4
	public bool CanChargeJump
	{
		get
		{
			return this.m_sein.Abilities.GrabWall.IsGrabbing && this.m_sein.Abilities.ChargeJumpCharging.IsCharged && this.m_currentState == SeinWallChargeJump.State.Aiming;
		}
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x0007F121 File Offset: 0x0007D321
	public void OnRestoreCheckpoint()
	{
		this.m_spriteMirrorLock = false;
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x0007F12A File Offset: 0x0007D32A
	public override void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x170004E4 RID: 1252
	// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0007F148 File Offset: 0x0007D348
	public CharacterSpriteMirror CharacterSpriteMirror
	{
		get
		{
			return this.m_sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x170004E5 RID: 1253
	// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0007F15F File Offset: 0x0007D35F
	// (set) Token: 0x06001D11 RID: 7441 RVA: 0x0007F168 File Offset: 0x0007D368
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
					this.CharacterSpriteMirror.Lock++;
				}
				else
				{
					this.CharacterSpriteMirror.Lock--;
				}
			}
		}
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x0007F1BC File Offset: 0x0007D3BC
	public void PerformChargeJump()
	{
		float chargedJumpStrength = this.ChargedJumpStrength;
		this.PlatformMovement.LocalSpeedX = chargedJumpStrength * this.Arrow.transform.right.x;
		this.PlatformMovement.LocalSpeedY = chargedJumpStrength * this.Arrow.transform.right.y;
		Vector2 normalized = this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeed.normalized;
		this.m_angleDirection = Mathf.Atan2(normalized.y, Mathf.Abs(normalized.x)) * 57.29578f * (float)((normalized.x >= 0f) ? 1 : -1);
		Sound.Play(this.JumpSound.GetSound(null), this.m_sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.m_sein.Mortality.DamageReciever.MakeInvincibleToEnemies(this.AntiGravityDuration);
		this.ChangeState(SeinWallChargeJump.State.Jumping);
		this.m_sein.FaceLeft = (this.PlatformMovement.LocalSpeedX < 0f);
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.m_sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpAnimation, 10, new Func<bool>(this.ShouldChargeJumpAnimationKeepPlaying));
		characterAnimationState.OnStartPlaying = new Action(this.OnAnimationStart);
		characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
		this.m_sein.PlatformBehaviour.Visuals.SpriteRotater.BeginTiltUpDownInAir(1.5f);
		if (this.m_sein.Abilities.Glide)
		{
			this.m_sein.Abilities.Glide.NeedsRightTriggerReleased = true;
		}
		JumpFlipPlatform.OnSeinChargeJumpEvent();
		this.m_sein.Abilities.ChargeJumpCharging.EndCharge();
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0007F3A0 File Offset: 0x0007D5A0
	public bool ShouldChargeJumpAnimationKeepPlaying()
	{
		return this.PlatformMovement.IsInAir && !this.PlatformMovement.IsOnWall && !this.PlatformMovement.IsOnCeiling;
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0007F3DE File Offset: 0x0007D5DE
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.Abilities.WallChargeJump = this;
	}

	// Token: 0x04001925 RID: 6437
	public TextureAnimationWithTransitions ChargeAnimation;

	// Token: 0x04001926 RID: 6438
	public TextureAnimationWithTransitions JumpAnimation;

	// Token: 0x04001927 RID: 6439
	public SoundProvider JumpSound;

	// Token: 0x04001928 RID: 6440
	public float AntiGravityDuration = 0.2f;

	// Token: 0x04001929 RID: 6441
	public float HorizontalDrag = 30f;

	// Token: 0x0400192A RID: 6442
	public BaseAnimator Arrow;

	// Token: 0x0400192B RID: 6443
	public int Damage = 50;

	// Token: 0x0400192C RID: 6444
	public float ChargedJumpStrength;

	// Token: 0x0400192D RID: 6445
	private SeinWallChargeJump.State m_currentState;

	// Token: 0x0400192E RID: 6446
	private float m_angularElevation;

	// Token: 0x0400192F RID: 6447
	private float m_angularElevationSpeed;

	// Token: 0x04001930 RID: 6448
	private float m_stateCurrentTime;

	// Token: 0x04001931 RID: 6449
	private float m_angleDirection;

	// Token: 0x04001932 RID: 6450
	private bool m_spriteMirrorLock;

	// Token: 0x04001933 RID: 6451
	private SeinCharacter m_sein;

	// Token: 0x04001934 RID: 6452
	private HashSet<IAttackable> m_attackablesIgnore = new HashSet<IAttackable>();

	// Token: 0x04001935 RID: 6453
	public GameObject ExplosionEffect;

	// Token: 0x02000457 RID: 1111
	public enum State
	{
		// Token: 0x04001A8F RID: 6799
		Normal,
		// Token: 0x04001A90 RID: 6800
		Aiming,
		// Token: 0x04001A91 RID: 6801
		Jumping
	}
}
