using System;
using Core;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public class SeinWallSlide : CharacterState, ISeinReceiver
{
	// Token: 0x17000515 RID: 1301
	// (get) Token: 0x06001E38 RID: 7736 RVA: 0x00085243 File Offset: 0x00083443
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x17000516 RID: 1302
	// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0008525A File Offset: 0x0008345A
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x17000517 RID: 1303
	// (get) Token: 0x06001E3A RID: 7738 RVA: 0x0008526C File Offset: 0x0008346C
	public SeinDoubleJump DoubleJump
	{
		get
		{
			return this.Sein.Abilities.DoubleJump;
		}
	}

	// Token: 0x17000518 RID: 1304
	// (get) Token: 0x06001E3B RID: 7739 RVA: 0x0008527E File Offset: 0x0008347E
	public SeinJump Jump
	{
		get
		{
			return this.Sein.Abilities.Jump;
		}
	}

	// Token: 0x17000519 RID: 1305
	// (get) Token: 0x06001E3C RID: 7740 RVA: 0x00085290 File Offset: 0x00083490
	public CharacterGravity Gravity
	{
		get
		{
			return this.Sein.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x1700051A RID: 1306
	// (get) Token: 0x06001E3D RID: 7741 RVA: 0x000852A2 File Offset: 0x000834A2
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x1700051B RID: 1307
	// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000852B4 File Offset: 0x000834B4
	public bool IsOnWall
	{
		get
		{
			float num = Mathf.Cos(0.27925268f);
			PlatformMovementListOfColliders platformMovementListOfColliders = this.Sein.PlatformBehaviour.PlatformMovementListOfColliders;
			Collider wallLeftCollider = platformMovementListOfColliders.WallLeftCollider;
			Collider wallRightCollider = platformMovementListOfColliders.WallRightCollider;
			PlatformMovement platformMovement = this.PlatformMovement;
			if (platformMovement.HasWallLeft)
			{
				if (wallLeftCollider && wallLeftCollider.CompareTag("NoWallSlide"))
				{
					return false;
				}
				if (Vector3.Dot(platformMovement.WallLeftNormal, platformMovement.LocalToWorld(Vector3.up)) > 0f || Vector3.Dot(platformMovement.WallLeftNormal, platformMovement.LocalToWorld(Vector3.right)) > num || (wallLeftCollider && wallLeftCollider.GetComponent<SteepWall>()))
				{
					return true;
				}
			}
			if (platformMovement.HasWallRight)
			{
				if (wallRightCollider && wallRightCollider.CompareTag("NoWallSlide"))
				{
					return false;
				}
				if (Vector3.Dot(platformMovement.WallRightNormal, platformMovement.LocalToWorld(Vector3.up)) > 0f || Vector3.Dot(platformMovement.WallRightNormal, platformMovement.LocalToWorld(Vector3.left)) > num || (wallRightCollider && wallRightCollider.GetComponent<SteepWall>()))
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x1700051C RID: 1308
	// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00085428 File Offset: 0x00083628
	public bool CanWallSlide
	{
		get
		{
			return base.Active && !this.PlatformMovement.IsOnGround && !this.PlatformMovement.IsOnCeiling && this.IsOnWall && this.PlatformMovement.HeadAndFeetAgainstWall;
		}
	}

	// Token: 0x1700051D RID: 1309
	// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0008547C File Offset: 0x0008367C
	public bool IsWallSliding
	{
		get
		{
			return base.Active && (this.CurrentState == SeinWallSlide.State.SlidingLeft || this.CurrentState == SeinWallSlide.State.SlidingRight);
		}
	}

	// Token: 0x1700051E RID: 1310
	// (get) Token: 0x06001E41 RID: 7745 RVA: 0x000854B0 File Offset: 0x000836B0
	public bool ShouldWallSlideUpAnimationPlay
	{
		get
		{
			return this.ShouldWallSlideUpAnimationKeepPlaying() && this.PlatformMovement.LocalSpeedY > this.WallSlideUpAnimationMinimiumSpeed;
		}
	}

	// Token: 0x1700051F RID: 1311
	// (get) Token: 0x06001E42 RID: 7746 RVA: 0x000854DE File Offset: 0x000836DE
	public bool ShouldWallSlideDownAnimationPlay
	{
		get
		{
			return this.ShouldWallSlideDownAnimationKeepPlaying();
		}
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x000854E6 File Offset: 0x000836E6
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.WallSlide = this;
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x00085500 File Offset: 0x00083700
	public void Start()
	{
		base.Active = true;
		this.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
		this.Gravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyGravityPlatformMovementSettings;
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x00085544 File Offset: 0x00083744
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
		this.Gravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyGravityPlatformMovementSettings;
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x00085588 File Offset: 0x00083788
	public void ModifyGravityPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (this.IsWallSliding && this.Sein.PlayerAbilities.WallJump.HasAbility)
		{
			settings.GravityStrength *= this.GravityMultiplier;
			settings.MaxFallSpeed = Mathf.Min(this.MaxFallSpeed, settings.MaxFallSpeed);
		}
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x000855E4 File Offset: 0x000837E4
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.IsWallSliding && this.m_inputLockTimeRemaining > 0f)
		{
			settings.LockInput = true;
		}
	}

	// Token: 0x06001E48 RID: 7752 RVA: 0x00085614 File Offset: 0x00083814
	public override void UpdateCharacterState()
	{
		this.m_inputLockTimeRemaining -= Time.deltaTime;
		this.UpdateState();
		if (this.IsWallSliding)
		{
			if (this.ShouldWallSlideUpAnimationPlay)
			{
				this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.SlideUpAnimation, 23, new Func<bool>(this.ShouldWallSlideUpAnimationKeepPlaying), false);
			}
			else if (this.ShouldWallSlideDownAnimationPlay)
			{
				this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.SlideDownAnimation, 23, new Func<bool>(this.ShouldWallSlideDownAnimationKeepPlaying), false);
			}
		}
		this.HandleSounds();
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x000856C5 File Offset: 0x000838C5
	public override void OnExit()
	{
		this.HandleSounds();
	}

	// Token: 0x17000520 RID: 1312
	// (get) Token: 0x06001E4A RID: 7754 RVA: 0x000856CD File Offset: 0x000838CD
	public SurfaceMaterialType WallSurfaceMaterialType
	{
		get
		{
			return this.Sein.PlatformBehaviour.WallSurfaceMaterialType;
		}
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x000856E0 File Offset: 0x000838E0
	public void HandleSounds()
	{
		if (this.WallEnterSounds != null && (this.PlatformMovement.WallLeft.OnThisFrame || this.PlatformMovement.WallRight.OnThisFrame))
		{
			Sound.Play(this.WallEnterSounds.GetSoundForMaterial(this.WallSurfaceMaterialType, null), this.Sein.Position, null);
		}
		if (this.WallExitSounds != null && (this.PlatformMovement.WallLeft.OffThisFrame || this.PlatformMovement.WallRight.OffThisFrame))
		{
			Sound.Play(this.WallExitSounds.GetSoundForMaterial(this.WallSurfaceMaterialType, null), this.Sein.Position, null);
		}
		if (this.m_wallSlideUpSound == null && this.ShouldWallSlideUpAnimationPlay && !this.Sein.Controller.IsGrabbingWall)
		{
			this.m_wallSlideUpSound = Sound.Play(this.WallSlideUpSound.GetSoundForMaterial(this.WallSurfaceMaterialType, null), this.Sein.Position, delegate()
			{
				this.m_wallSlideUpSound = null;
			});
		}
		if ((!this.ShouldWallSlideUpAnimationPlay || this.Sein.Controller.IsGrabbingWall) && this.m_wallSlideUpSound)
		{
			this.m_wallSlideUpSound.FadeOut(0.25f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_wallSlideUpSound.gameObject);
			this.m_wallSlideUpSound = null;
		}
		if (this.m_wallSlideDownSound == null && this.ShouldWallSlideDownAnimationPlay && !this.Sein.Controller.IsGrabbingWall && GameController.Instance.GameTime > this.m_lastWallSlideDownSoundTime + this.m_minimumSoundDelay)
		{
			this.m_lastWallSlideDownSoundTime = GameController.Instance.GameTime;
			this.m_wallSlideDownSound = Sound.Play(this.WallSlideDownSound.GetSoundForMaterial(this.WallSurfaceMaterialType, null), this.Sein.Position, delegate()
			{
				this.m_wallSlideDownSound = null;
			});
		}
		if ((!this.ShouldWallSlideDownAnimationPlay || this.Sein.Controller.IsGrabbingWall) && this.m_wallSlideDownSound)
		{
			this.m_wallSlideDownSound.FadeOut(0.25f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_wallSlideDownSound.gameObject);
			this.m_wallSlideDownSound = null;
		}
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x0008595C File Offset: 0x00083B5C
	public void ChangeState(SeinWallSlide.State state)
	{
		this.CurrentState = state;
		switch (this.CurrentState)
		{
		case SeinWallSlide.State.SlidingLeft:
			this.ResetMovingOffWallLockTimer();
			this.Sein.ResetAirLimits();
			if (this.Jump)
			{
				this.Jump.ResetRunningJumpCount();
			}
			break;
		case SeinWallSlide.State.SlidingRight:
			this.ResetMovingOffWallLockTimer();
			this.Sein.ResetAirLimits();
			if (this.Jump)
			{
				this.Jump.ResetRunningJumpCount();
			}
			break;
		}
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x000859F8 File Offset: 0x00083BF8
	public void ResetMovingOffWallLockTimer()
	{
		if (this.Sein.Abilities.WallJump && this.Sein.Abilities.WallJump.Active)
		{
			this.m_inputLockTimeRemaining = this.InputLockDuration;
		}
		else
		{
			this.m_inputLockTimeRemaining = 0f;
		}
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x00085A58 File Offset: 0x00083C58
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case SeinWallSlide.State.Normal:
			if (!this.PlatformMovement.IsOnGround)
			{
				if (this.CanWallSlide && this.PlatformMovement.HasWallLeft)
				{
					this.ChangeState(SeinWallSlide.State.SlidingLeft);
				}
				else if (this.CanWallSlide && this.PlatformMovement.HasWallRight)
				{
					this.ChangeState(SeinWallSlide.State.SlidingRight);
				}
			}
			break;
		case SeinWallSlide.State.SlidingLeft:
			if (!this.CanWallSlide)
			{
				this.ChangeState(SeinWallSlide.State.Normal);
				return;
			}
			if (this.LeftRightMovement.BaseHorizontalInput <= 0f)
			{
				this.ResetMovingOffWallLockTimer();
			}
			if (this.PlatformMovement.HeadAndFeetAgainstWall)
			{
				this.SpriteMirror.FaceLeft = true;
			}
			break;
		case SeinWallSlide.State.SlidingRight:
			if (!this.CanWallSlide)
			{
				this.ChangeState(SeinWallSlide.State.Normal);
				return;
			}
			if (this.LeftRightMovement.BaseHorizontalInput >= 0f)
			{
				this.ResetMovingOffWallLockTimer();
			}
			if (this.PlatformMovement.HeadAndFeetAgainstWall)
			{
				this.SpriteMirror.FaceLeft = false;
			}
			break;
		}
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x00085B80 File Offset: 0x00083D80
	public bool ShouldWallSlideUpAnimationKeepPlaying()
	{
		return base.Active && this.IsOnWall && this.PlatformMovement.IsInAir && this.PlatformMovement.Jumping && this.PlatformMovement.HeadAgainstWall && this.PlatformMovement.FeetAgainstWall;
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x00085BE4 File Offset: 0x00083DE4
	public bool ShouldWallSlideDownAnimationKeepPlaying()
	{
		return base.Active && this.IsOnWall && this.PlatformMovement.IsInAir && this.PlatformMovement.FeetAgainstWall && this.PlatformMovement.HeadAgainstWall;
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x00085C35 File Offset: 0x00083E35
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_inputLockTimeRemaining);
		base.Serialize(ar);
	}

	// Token: 0x04001A07 RID: 6663
	public SeinWallSlide.State CurrentState;

	// Token: 0x04001A08 RID: 6664
	public float GravityMultiplier;

	// Token: 0x04001A09 RID: 6665
	public float InputLockDuration = 0.2f;

	// Token: 0x04001A0A RID: 6666
	public float MaxFallSpeed = 10f;

	// Token: 0x04001A0B RID: 6667
	public SeinCharacter Sein;

	// Token: 0x04001A0C RID: 6668
	public TextureAnimationWithTransitions SlideDownAnimation;

	// Token: 0x04001A0D RID: 6669
	public TextureAnimationWithTransitions SlideUpAnimation;

	// Token: 0x04001A0E RID: 6670
	public SurfaceToSoundProviderMap WallEnterSounds;

	// Token: 0x04001A0F RID: 6671
	public SurfaceToSoundProviderMap WallExitSounds;

	// Token: 0x04001A10 RID: 6672
	public SurfaceToSoundProviderMap WallSlideDownSound;

	// Token: 0x04001A11 RID: 6673
	public float WallSlideUpAnimationMinimiumSpeed = 5f;

	// Token: 0x04001A12 RID: 6674
	public SurfaceToSoundProviderMap WallSlideUpSound;

	// Token: 0x04001A13 RID: 6675
	private float m_inputLockTimeRemaining;

	// Token: 0x04001A14 RID: 6676
	private SoundPlayer m_wallSlideDownSound;

	// Token: 0x04001A15 RID: 6677
	private SoundPlayer m_wallSlideUpSound;

	// Token: 0x04001A16 RID: 6678
	private float m_lastWallSlideDownSoundTime;

	// Token: 0x04001A17 RID: 6679
	private float m_minimumSoundDelay = 0.4f;

	// Token: 0x0200045A RID: 1114
	public enum State
	{
		// Token: 0x04001A9B RID: 6811
		Normal,
		// Token: 0x04001A9C RID: 6812
		SlidingLeft,
		// Token: 0x04001A9D RID: 6813
		SlidingRight
	}
}
