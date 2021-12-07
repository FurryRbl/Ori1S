using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class SeinJump : CharacterState, ISeinReceiver
{
	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06001D45 RID: 7493 RVA: 0x00080647 File Offset: 0x0007E847
	// (remove) Token: 0x06001D46 RID: 7494 RVA: 0x00080660 File Offset: 0x0007E860
	public event Action<float> OnJumpEvent = delegate(float A_0)
	{
	};

	// Token: 0x170004F1 RID: 1265
	// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0008067C File Offset: 0x0007E87C
	public bool CanJump
	{
		get
		{
			return base.enabled && this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY <= 0.0001f && this.m_timeWeCanJumpRemaining > 0f && !this.Sein.PlatformBehaviour.PlatformMovement.Ceiling.IsOn && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities);
		}
	}

	// Token: 0x170004F2 RID: 1266
	// (get) Token: 0x06001D48 RID: 7496 RVA: 0x000806EE File Offset: 0x0007E8EE
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170004F3 RID: 1267
	// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00080700 File Offset: 0x0007E900
	// (set) Token: 0x06001D4A RID: 7498 RVA: 0x00080708 File Offset: 0x0007E908
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

	// Token: 0x170004F4 RID: 1268
	// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00080759 File Offset: 0x0007E959
	public CharacterSpriteMirror CharacterSpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x170004F5 RID: 1269
	// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00080770 File Offset: 0x0007E970
	public bool HasSharplyTurnedAround
	{
		get
		{
			return (this.m_timeSinceMovingRight > 0f && this.m_timeSinceMovingRight < 0.2f && this.PlatformMovement.LocalSpeedX < 0f) || (this.m_timeSinceMovingLeft > 0f && this.m_timeSinceMovingLeft < 0.2f && this.PlatformMovement.LocalSpeedX > 0f);
		}
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x000807EA File Offset: 0x0007E9EA
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Jump = this;
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x00080804 File Offset: 0x0007EA04
	public override void UpdateCharacterState()
	{
		if (this.m_timeWeCanJumpRemaining > 0f)
		{
			this.m_timeWeCanJumpRemaining -= Time.deltaTime;
		}
		if (this.Sein.PlatformBehaviour.PlatformMovement.Ground.IsOn)
		{
			this.m_timeWeCanJumpRemaining = this.DurationSinceLastOnGroundThatWeCanStillJump;
		}
		else
		{
			this.m_bunnyHopTimeRemaining = 0.2f;
		}
		if (this.m_bunnyHopTimeRemaining > 0f)
		{
			this.m_bunnyHopTimeRemaining -= Time.deltaTime;
			if (this.m_bunnyHopTimeRemaining < 0f)
			{
				this.ResetRunningJumpCount();
			}
		}
		if (!this.PlatformMovement.MovingHorizontally && this.PlatformMovement.IsOnGround)
		{
			this.ResetRunningJumpCount();
		}
		if (this.PlatformMovement.MovingHorizontally && this.PlatformMovement.IsOnGround)
		{
			this.ResetJumpIdleCount();
		}
		this.UpdateTimeSinceFacing();
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x000808F8 File Offset: 0x0007EAF8
	public void ResetRunningJumpCount()
	{
		this.m_runningJumpNumber = 0;
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x00080901 File Offset: 0x0007EB01
	public void ResetJumpIdleCount()
	{
		this.m_jumpIdleNumber = 0;
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x0008090C File Offset: 0x0007EB0C
	public float CalculateSpeedFromHeight(float height)
	{
		return PhysicsHelper.CalculateSpeedFromHeight(height, this.Sein.PlatformBehaviour.Gravity.BaseSettings.GravityStrength);
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x0008093C File Offset: 0x0007EB3C
	public void PerformTurnAroundBackFlipJump()
	{
		this.PlatformMovement.LocalSpeedY = this.CalculateSpeedFromHeight(this.BackflipJumpHeight);
		this.Sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = true;
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY * 0.5f, 1f);
		}
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.BackflipAnimation, 10, new Func<bool>(this.ShouldBackflipAnimationKeepPlaying));
		characterAnimationState.OnStartPlaying = new Action(this.OnAnimationStart);
		characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x00080A10 File Offset: 0x0007EC10
	public void PerformJump()
	{
		this.m_currentJumpingMaterial = SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider);
		if (this.Sein.Controller.IsCrouching)
		{
			this.PerformCrouchJump();
			Sound.Play(this.JumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		}
		else if (this.HasSharplyTurnedAround)
		{
			this.PerformTurnAroundBackFlipJump();
			Sound.Play(this.FlipJumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		}
		else if (this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f || this.PlatformMovement.IsOnWall)
		{
			if (this.PlatformMovement.IsOnWall && this.Sein.PlayerAbilities.WallJump.HasAbility && this.Sein.Abilities.WallSlide.IsOnWall)
			{
				this.PerformWallSlideJump();
				Sound.Play(this.JumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
			}
			else
			{
				this.PerformIdleJump();
			}
		}
		else
		{
			this.PerformRunningJump();
		}
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.JumpParticleEffect, this.Sein.PlatformBehaviour.PlatformMovement.FeetPosition, Quaternion.identity);
		gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(-this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed));
		this.Sein.PlatformBehaviour.Force.ApplyGroundForce(Vector3.down * this.JumpImpulse, ForceMode.Impulse);
		this.OnJumpEvent(this.PlatformMovement.LocalSpeedY);
		JumpFlipPlatform.OnSeinJumpEvent();
		this.m_timeWeCanJumpRemaining = 0f;
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x00080C44 File Offset: 0x0007EE44
	public void PerformRunningJump()
	{
		switch (this.m_runningJumpNumber)
		{
		case 0:
			this.PerformFirstRunningJump();
			break;
		case 1:
			this.PerformSecondRunningJump();
			break;
		case 2:
			this.PerformThirdRunningJump();
			break;
		}
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x00080C90 File Offset: 0x0007EE90
	private void CacheDelegates()
	{
		if (this.m_shouldJumpMoving == null)
		{
			this.m_shouldJumpMoving = new Func<bool>(this.ShouldJumpMovingAnimationKeepPlaying);
		}
		if (this.onAnimationEnd == null)
		{
			this.onAnimationEnd = new Action(this.OnAnimationEnd);
		}
	}

	// Token: 0x06001D56 RID: 7510 RVA: 0x00080CD8 File Offset: 0x0007EED8
	public void PerformFirstRunningJump()
	{
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		localSpeed.y = this.CalculateSpeedFromHeight(this.FirstJumpHeight);
		this.PlatformMovement.LocalSpeed = localSpeed;
		this.CacheDelegates();
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpAnimation[0], 10, this.m_shouldJumpMoving);
		characterAnimationState.OnStopPlaying = this.onAnimationEnd;
		characterAnimationState.OnStartPlaying = null;
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY, 1f);
		}
		Sound.Play(this.JumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.m_runningJumpNumber++;
	}

	// Token: 0x06001D57 RID: 7511 RVA: 0x00080DD0 File Offset: 0x0007EFD0
	public void PerformSecondRunningJump()
	{
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		localSpeed.y = this.CalculateSpeedFromHeight((this.m_runningJumpNumber != 0) ? this.SecondJumpHeight : this.FirstJumpHeight);
		this.PlatformMovement.LocalSpeed = localSpeed;
		this.CacheDelegates();
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpAnimation[1], 10, this.m_shouldJumpMoving);
		characterAnimationState.OnStopPlaying = this.onAnimationEnd;
		characterAnimationState.OnStartPlaying = null;
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY, 1f);
		}
		Sound.Play(this.JumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.m_runningJumpNumber++;
	}

	// Token: 0x06001D58 RID: 7512 RVA: 0x00080EE0 File Offset: 0x0007F0E0
	public void PerformThirdRunningJump()
	{
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		localSpeed.y = this.CalculateSpeedFromHeight(this.ThirdJumpHeight);
		this.PlatformMovement.LocalSpeed = localSpeed;
		this.CacheDelegates();
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpAnimation[2], 10, this.m_shouldJumpMoving);
		characterAnimationState.OnStartPlaying = null;
		characterAnimationState.OnStopPlaying = this.onAnimationEnd;
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY * 0.5f, 1f);
		}
		Sound.Play(this.SpinJumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.m_runningJumpNumber = 0;
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x00080FD8 File Offset: 0x0007F1D8
	private void PerformIdleJump()
	{
		switch (this.m_jumpIdleNumber)
		{
		case 0:
			this.PerformFirstIdleJump();
			break;
		case 1:
			this.PerformSecondIdleJump();
			break;
		case 2:
			this.PerformThirdIldleJump();
			break;
		}
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x00081024 File Offset: 0x0007F224
	public void PerformFirstIdleJump()
	{
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpIdleAnimation[0], 10, new Func<bool>(this.ShouldJumpIdleAnimationKeepPlaying));
		characterAnimationState.OnStartPlaying = null;
		characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
		this.PlatformMovement.LocalSpeedY = this.CalculateSpeedFromHeight(this.FirstJumpHeight);
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY, 1f);
		}
		Sound.Play(this.JumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.m_jumpIdleNumber++;
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x00081110 File Offset: 0x0007F310
	public void PerformSecondIdleJump()
	{
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpIdleAnimation[1], 10, new Func<bool>(this.ShouldJumpIdleAnimationKeepPlaying));
		characterAnimationState.OnStartPlaying = null;
		characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
		this.PlatformMovement.LocalSpeedY = this.CalculateSpeedFromHeight(this.SecondJumpHeight);
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY, 1f);
		}
		Sound.Play(this.JumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.m_jumpIdleNumber++;
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x000811FC File Offset: 0x0007F3FC
	private void PerformThirdIldleJump()
	{
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpIdleAnimation[2], 10, new Func<bool>(this.ShouldJumpIdleAnimationKeepPlaying));
		characterAnimationState.OnStartPlaying = null;
		characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
		this.PlatformMovement.LocalSpeedY = this.CalculateSpeedFromHeight(this.ThirdJumpHeight);
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY, 1f);
		}
		Sound.Play(this.SpinJumpSoundProvider.GetSoundForMaterial(this.m_currentJumpingMaterial, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.m_jumpIdleNumber = 0;
	}

	// Token: 0x06001D5D RID: 7517 RVA: 0x000812E0 File Offset: 0x0007F4E0
	private void PerformWallSlideJump()
	{
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.WallSlideJumpAnimation, 24, new Func<bool>(this.ShouldWallSlideJumpAnimationKeepPlaying));
		characterAnimationState.OnStartPlaying = null;
		characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
		this.PlatformMovement.LocalSpeedY = this.CalculateSpeedFromHeight(this.FirstJumpHeight);
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY, 1f);
		}
	}

	// Token: 0x06001D5E RID: 7518 RVA: 0x0008138C File Offset: 0x0007F58C
	private void PerformCrouchJump()
	{
		bool flag = false;
		List<Collider> groundColliders = this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.GroundColliders;
		for (int i = 0; i < groundColliders.Count; i++)
		{
			Collider component = groundColliders[i];
			if (component.GetComponentInParents<GoThroughPlatform>() && this.Sein.GetComponent<GoThroughPlatformHandler>().FallThroughPlatform())
			{
				this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedX = 0f;
				this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY = 0f;
				this.Sein.PlatformBehaviour.PlatformMovement.Ground.FutureOn = false;
				this.Sein.PlatformBehaviour.PlatformMovement.Ground.IsOn = false;
				this.Sein.PlatformBehaviour.PlatformMovement.Ground.WasOn = false;
				flag = true;
			}
		}
		if (!flag)
		{
			this.PlatformMovement.LocalSpeedY = this.CalculateSpeedFromHeight(this.CrouchJumpHeight);
			this.PlatformMovement.LocalSpeedX = (float)((!this.CharacterSpriteMirror.FaceLeft) ? -3 : 3);
			this.Sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = true;
			CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.CrouchJumpAnimation, 10, new Func<bool>(this.ShouldBackflipAnimationKeepPlaying));
			characterAnimationState.OnStartPlaying = new Action(this.OnAnimationStart);
			characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
		}
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x00081529 File Offset: 0x0007F729
	public bool ShouldBackflipAnimationKeepPlaying()
	{
		return this.Sein.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x00081548 File Offset: 0x0007F748
	public bool ShouldJumpIdleAnimationKeepPlaying()
	{
		return this.Sein.PlatformBehaviour.PlatformMovement.IsInAir && (!Characters.Sein.Controller.CanMove || Core.Input.NormalizedHorizontal == 0 || this.PlatformMovement.IsOnWall);
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x000815A4 File Offset: 0x0007F7A4
	public bool ShouldWallSlideJumpAnimationKeepPlaying()
	{
		return this.PlatformMovement.IsOnWall && this.PlatformMovement.IsInAir && this.PlatformMovement.Jumping && this.PlatformMovement.HeadAgainstWall && this.PlatformMovement.FeetAgainstWall;
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x00081600 File Offset: 0x0007F800
	public bool ShouldJumpMovingAnimationKeepPlaying()
	{
		return this.Sein.PlatformBehaviour.PlatformMovement.IsInAir && (!Characters.Sein.Controller.CanMove || (this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f && (!this.PlatformMovement.IsOnWall || !this.PlatformMovement.HeadAgainstWall)));
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x0008167F File Offset: 0x0007F87F
	public bool ShouldThirdJumpMovingAnimationKeepPlaying()
	{
		return this.Sein.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x000816A0 File Offset: 0x0007F8A0
	public void UpdateTimeSinceFacing()
	{
		this.m_timeSinceMovingLeft += Time.deltaTime;
		this.m_timeSinceMovingRight += Time.deltaTime;
		if (this.PlatformMovement.LocalSpeedX < 0f)
		{
			this.m_timeSinceMovingLeft = 0f;
		}
		if (this.PlatformMovement.LocalSpeedX > 0f)
		{
			this.m_timeSinceMovingRight = 0f;
		}
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x00081711 File Offset: 0x0007F911
	public void OnAnimationEnd()
	{
		this.SpriteMirrorLock = false;
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x0008171A File Offset: 0x0007F91A
	public void OnAnimationStart()
	{
		this.SpriteMirrorLock = true;
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x00081724 File Offset: 0x0007F924
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_bunnyHopTimeRemaining);
		ar.Serialize(ref this.m_jumpIdleNumber);
		ar.Serialize(ref this.m_runningJumpNumber);
		ar.Serialize(ref this.m_spriteMirrorLock);
		ar.Serialize(ref this.m_timeSinceMovingLeft);
		ar.Serialize(ref this.m_timeSinceMovingRight);
		ar.Serialize(ref this.m_timeWeCanJumpRemaining);
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x00081785 File Offset: 0x0007F985
	public override void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x000817A3 File Offset: 0x0007F9A3
	public override void OnDestroy()
	{
		base.OnDestroy();
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x000817C1 File Offset: 0x0007F9C1
	public void OnRestoreCheckpoint()
	{
		this.m_spriteMirrorLock = false;
	}

	// Token: 0x04001957 RID: 6487
	public TextureAnimationWithTransitions BackflipAnimation;

	// Token: 0x04001958 RID: 6488
	public float BackflipJumpHeight = 3f;

	// Token: 0x04001959 RID: 6489
	public TextureAnimationWithTransitions CrouchJumpAnimation;

	// Token: 0x0400195A RID: 6490
	public float CrouchJumpHeight = 4.5f;

	// Token: 0x0400195B RID: 6491
	public float DurationSinceLastOnGroundThatWeCanStillJump = 0.2f;

	// Token: 0x0400195C RID: 6492
	public float FirstJumpHeight = 3f;

	// Token: 0x0400195D RID: 6493
	public TextureAnimationWithTransitions[] JumpAnimation;

	// Token: 0x0400195E RID: 6494
	public TextureAnimationWithTransitions[] JumpIdleAnimation;

	// Token: 0x0400195F RID: 6495
	public float JumpIdleHeight = 3f;

	// Token: 0x04001960 RID: 6496
	public float JumpImpulse;

	// Token: 0x04001961 RID: 6497
	public GameObject JumpParticleEffect;

	// Token: 0x04001962 RID: 6498
	public SurfaceToSoundProviderMap JumpSoundProvider;

	// Token: 0x04001963 RID: 6499
	public SurfaceToSoundProviderMap FlipJumpSoundProvider;

	// Token: 0x04001964 RID: 6500
	public SurfaceToSoundProviderMap SpinJumpSoundProvider;

	// Token: 0x04001965 RID: 6501
	private SurfaceMaterialType m_currentJumpingMaterial;

	// Token: 0x04001966 RID: 6502
	public float SecondJumpHeight = 3.75f;

	// Token: 0x04001967 RID: 6503
	public SeinCharacter Sein;

	// Token: 0x04001968 RID: 6504
	public float ThirdJumpHeight = 4.5f;

	// Token: 0x04001969 RID: 6505
	public TextureAnimationWithTransitions WallSlideJumpAnimation;

	// Token: 0x0400196A RID: 6506
	private float m_bunnyHopTimeRemaining;

	// Token: 0x0400196B RID: 6507
	private int m_jumpIdleNumber;

	// Token: 0x0400196C RID: 6508
	private int m_runningJumpNumber;

	// Token: 0x0400196D RID: 6509
	private bool m_spriteMirrorLock;

	// Token: 0x0400196E RID: 6510
	private float m_timeSinceMovingLeft;

	// Token: 0x0400196F RID: 6511
	private float m_timeSinceMovingRight;

	// Token: 0x04001970 RID: 6512
	private float m_timeWeCanJumpRemaining;

	// Token: 0x04001971 RID: 6513
	private Func<bool> m_shouldJumpMoving;

	// Token: 0x04001972 RID: 6514
	private Action onAnimationEnd;
}
