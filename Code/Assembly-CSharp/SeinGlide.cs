using System;
using Core;
using UnityEngine;

// Token: 0x02000411 RID: 1041
public class SeinGlide : CharacterState, ISeinReceiver
{
	// Token: 0x170004B5 RID: 1205
	// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0007ACD3 File Offset: 0x00078ED3
	public CharacterGravity CharacterGravity
	{
		get
		{
			return this.Sein.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x170004B6 RID: 1206
	// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0007ACE5 File Offset: 0x00078EE5
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170004B7 RID: 1207
	// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0007ACF7 File Offset: 0x00078EF7
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x0007AD0C File Offset: 0x00078F0C
	public void Start()
	{
		this.CharacterGravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyGravityPlatformMovementSettings;
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x0007AD48 File Offset: 0x00078F48
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.CharacterGravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyGravityPlatformMovementSettings;
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
		this.IsGliding = false;
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x0007AD90 File Offset: 0x00078F90
	public override void OnExit()
	{
		this.IsGliding = false;
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x0007AD9C File Offset: 0x00078F9C
	public void ModifyGravityPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (this.IsGliding && this.PlatformMovement.LocalSpeedY < 0f)
		{
			settings.GravityStrength *= this.GravityMultiplier;
		}
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x0007ADDC File Offset: 0x00078FDC
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.IsGliding)
		{
			settings.Air.ApplySpeedMultiplier(this.MoveSpeed);
		}
	}

	// Token: 0x170004B8 RID: 1208
	// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0007ADFA File Offset: 0x00078FFA
	// (set) Token: 0x06001C58 RID: 7256 RVA: 0x0007AE02 File Offset: 0x00079002
	public bool IsGliding
	{
		get
		{
			return this.m_isGliding;
		}
		set
		{
			if (this.m_isGliding != value)
			{
				this.m_isGliding = value;
				if (this.m_isGliding)
				{
					this.OnEnterGlide();
				}
				else
				{
					this.OnExitGlide();
				}
			}
		}
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x0007AE33 File Offset: 0x00079033
	public void OnEnterGlide()
	{
		this.UpdateAnimations();
	}

	// Token: 0x06001C5A RID: 7258 RVA: 0x0007AE3C File Offset: 0x0007903C
	public void OnExitGlide()
	{
		if (this.m_parachuteLoopLastSound)
		{
			this.m_parachuteLoopLastSound.FadeOut(1f, true);
		}
		base.OnExit();
		if (this.RunningTime > 0.3f)
		{
			Sound.Play(this.CloseParachuteSound.GetSound(null), this.PlatformMovement.Position, null);
		}
		this.RunningTime = 0f;
		this.m_playedOpenSound = false;
	}

	// Token: 0x170004B9 RID: 1209
	// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0007AEB0 File Offset: 0x000790B0
	public bool CanGlide
	{
		get
		{
			return !this.PlatformMovement.IsOnGround && !this.PlatformMovement.IsOnWall && !this.Sein.Controller.InputLocked && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities);
		}
	}

	// Token: 0x170004BA RID: 1210
	// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0007AEFE File Offset: 0x000790FE
	public bool WantsToGlide
	{
		get
		{
			return Core.Input.Glide.Pressed && !this.NeedsRightTriggerReleased && this.m_lockGlidingRemainingTime <= 0f;
		}
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x0007AF2D File Offset: 0x0007912D
	public void LockGliding(float time)
	{
		this.m_lockGlidingRemainingTime = time;
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x0007AF38 File Offset: 0x00079138
	public void UpdateGliding()
	{
		if (!this.CanGlide || !this.WantsToGlide)
		{
			this.IsGliding = false;
		}
		this.m_pressedMoveHorizontally = false;
		this.RunningTime += Time.deltaTime;
		if (!this.m_playedOpenSound && this.RunningTime > 0.15f && this.RunningTime < 0.2f)
		{
			Sound.Play(this.OpenParachuteSound.GetSound(null), this.PlatformMovement.Position, null);
			this.m_playedOpenSound = true;
		}
		if (!this.IsGliding)
		{
			this.Exit();
			return;
		}
		if (this.PlatformMovement.LocalSpeedY < -this.GlideSpeed)
		{
			this.PlatformMovement.LocalSpeedY = -this.GlideSpeed;
		}
		this.UpdateAnimations();
		if (this.m_pressedMoveHorizontally && !this.m_wasMovingHorizantally)
		{
			Sound.Play(this.TurnLeftRightSound.GetSound(null), this.PlatformMovement.Position, null);
		}
		else if (this.m_parachuteLoopLastSound == null)
		{
			this.m_parachuteLoopLastSound = Sound.Play(this.ParachuteLoopSound.GetSound(null), this.PlatformMovement.Position, delegate()
			{
				this.m_parachuteLoopLastSound = null;
			});
			if (this.m_parachuteLoopLastSound)
			{
				this.m_parachuteLoopLastSound.AttachTo = this.PlatformMovement.transform;
			}
		}
		this.m_wasMovingHorizantally = this.m_pressedMoveHorizontally;
		this.HandleFloatZones();
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x0007B0BC File Offset: 0x000792BC
	private void UpdateAnimations()
	{
		if (this.ShouldGlideMovingAnimationPlay)
		{
			this.m_pressedMoveHorizontally = true;
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.MovingAnimation, 110, new Func<bool>(this.ShouldGlideMovingAnimationKeepPlaying), false);
		}
		else if (this.ShouldGlideIdleAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.IdleAnimation, 110, new Func<bool>(this.ShouldGlideIdleAnimationKeepPlaying), false);
		}
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x0007B14C File Offset: 0x0007934C
	public void HandleFloatZones()
	{
		for (int i = 0; i < FloatZone.All.Count; i++)
		{
			FloatZone floatZone = FloatZone.All[i];
			if (floatZone.BoundingRect.Contains(this.Sein.Position))
			{
				PlatformMovement platformMovement = this.Sein.PlatformBehaviour.PlatformMovement;
				Vector2 b = Vector2.up * this.Sein.PlatformBehaviour.Gravity.CurrentSettings.GravityStrength * Time.deltaTime;
				platformMovement.LocalSpeed += b;
				Vector2 localSpeed = platformMovement.LocalSpeed;
				if (localSpeed.y < 0f)
				{
					localSpeed.y = MoonMath.Float.ClampedAdd(localSpeed.y, floatZone.Deceleration * Time.deltaTime, 0f, 0f);
				}
				if (localSpeed.y >= 0f)
				{
					localSpeed.y = MoonMath.Float.ClampedAdd(localSpeed.y, floatZone.Acceleration * Time.deltaTime, 0f, floatZone.DesiredSpeed);
					localSpeed.y = MoonMath.Float.ClampedSubtract(localSpeed.y, floatZone.TooFastDeceleration * Time.deltaTime, 0f, floatZone.DesiredSpeed);
				}
				platformMovement.LocalSpeed = localSpeed;
				this.Sein.ResetAirLimits();
				return;
			}
		}
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x0007B2B0 File Offset: 0x000794B0
	public override void UpdateCharacterState()
	{
		if (this.CharacterLeftRightMovement.HorizontalInput != 0f)
		{
			this.m_isMoveAnimation = 3;
		}
		else if (this.m_isMoveAnimation > 0)
		{
			this.m_isMoveAnimation--;
		}
		if (this.m_lockGlidingRemainingTime > 0f)
		{
			this.m_lockGlidingRemainingTime -= Time.deltaTime;
			if (this.m_lockGlidingRemainingTime < 0f)
			{
				this.m_lockGlidingRemainingTime = 0f;
			}
		}
		if (this.NeedsRightTriggerReleased && Core.Input.Glide.Released)
		{
			this.NeedsRightTriggerReleased = false;
		}
		if (this.IsGliding)
		{
			this.UpdateGliding();
		}
		else if (this.CanGlide && this.WantsToGlide && this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY < 0f)
		{
			this.IsGliding = true;
		}
	}

	// Token: 0x170004BB RID: 1211
	// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0007B3A8 File Offset: 0x000795A8
	public bool CanEnter
	{
		get
		{
			bool isGliding = this.IsGliding;
			if (isGliding)
			{
			}
			return isGliding;
		}
	}

	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0007B3C3 File Offset: 0x000795C3
	public float GlideOpeningTime
	{
		get
		{
			return 0.5f;
		}
	}

	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0007B3CA File Offset: 0x000795CA
	public bool ShouldGlideIdleAnimationPlay
	{
		get
		{
			return this.ShouldGlideIdleAnimationKeepPlaying();
		}
	}

	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0007B3D2 File Offset: 0x000795D2
	public bool ShouldGlideMovingAnimationPlay
	{
		get
		{
			return this.ShouldGlideMovingAnimationKeepPlaying();
		}
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x0007B3DA File Offset: 0x000795DA
	public bool ShouldGlideIdleAnimationKeepPlaying()
	{
		return this.IsGliding;
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x0007B3E2 File Offset: 0x000795E2
	public bool ShouldGlideMovingAnimationKeepPlaying()
	{
		return this.IsGliding && this.m_isMoveAnimation > 0;
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x0007B3FB File Offset: 0x000795FB
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Glide = this;
	}

	// Token: 0x04001889 RID: 6281
	public SeinCharacter Sein;

	// Token: 0x0400188A RID: 6282
	public TextureAnimationWithTransitions IdleAnimation;

	// Token: 0x0400188B RID: 6283
	public TextureAnimationWithTransitions MovingAnimation;

	// Token: 0x0400188C RID: 6284
	public SoundProvider OpenParachuteSound;

	// Token: 0x0400188D RID: 6285
	public SoundProvider CloseParachuteSound;

	// Token: 0x0400188E RID: 6286
	public SoundProvider ParachuteLoopSound;

	// Token: 0x0400188F RID: 6287
	public SoundProvider TurnLeftRightSound;

	// Token: 0x04001890 RID: 6288
	private SoundPlayer m_parachuteLoopLastSound;

	// Token: 0x04001891 RID: 6289
	private bool m_playedOpenSound;

	// Token: 0x04001892 RID: 6290
	private bool m_pressedMoveHorizontally;

	// Token: 0x04001893 RID: 6291
	private bool m_wasMovingHorizantally;

	// Token: 0x04001894 RID: 6292
	private bool m_isGliding;

	// Token: 0x04001895 RID: 6293
	public bool NeedsRightTriggerReleased;

	// Token: 0x04001896 RID: 6294
	private float m_lockGlidingRemainingTime;

	// Token: 0x04001897 RID: 6295
	private int m_isMoveAnimation;

	// Token: 0x04001898 RID: 6296
	public float RunningTime;

	// Token: 0x04001899 RID: 6297
	public int Level;

	// Token: 0x0400189A RID: 6298
	public float MinHeightToGlide = 2f;

	// Token: 0x0400189B RID: 6299
	public float GlideSpeed;

	// Token: 0x0400189C RID: 6300
	public float GravityMultiplier = 0.5f;

	// Token: 0x0400189D RID: 6301
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet MoveSpeed;
}
