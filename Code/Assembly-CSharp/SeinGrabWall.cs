using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000410 RID: 1040
public class SeinGrabWall : CharacterState, ISeinReceiver
{
	// Token: 0x170004A8 RID: 1192
	// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0007A25C File Offset: 0x0007845C
	public CharacterGravity CharacterGravity
	{
		get
		{
			return this.Sein.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x170004A9 RID: 1193
	// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0007A26E File Offset: 0x0007846E
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170004AA RID: 1194
	// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0007A280 File Offset: 0x00078480
	public PlatformMovementListOfColliders ListOfCollidedObjects
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovementListOfColliders;
		}
	}

	// Token: 0x170004AB RID: 1195
	// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0007A292 File Offset: 0x00078492
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x0007A2A4 File Offset: 0x000784A4
	public TextureAnimationWithTransitions PickAwayAnimation()
	{
		if (this.Sein.PlayerAbilities.ChargeJump.HasAbility)
		{
			TextureAnimationWithTransitions[] away = this.GrabWallAnimation.Away;
			float angularElevation = this.Sein.Abilities.WallChargeJump.AngularElevation;
			int num = (int)Mathf.Clamp(Mathf.InverseLerp(-45f, 45f, angularElevation) * (float)away.Length, 0f, (float)(away.Length - 1));
			return away[num];
		}
		return this.GrabWallAnimation.GrabAway;
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x0007A324 File Offset: 0x00078524
	public void Start()
	{
		this.CharacterGravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyGravityPlatformMovementSettings;
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x0007A360 File Offset: 0x00078560
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.CharacterGravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyGravityPlatformMovementSettings;
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x0007A3A1 File Offset: 0x000785A1
	public void ModifyGravityPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (this.IsGrabbing)
		{
			settings.GravityStrength = 0f;
		}
	}

	// Token: 0x170004AC RID: 1196
	// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0007A3B9 File Offset: 0x000785B9
	public bool IsNotMoving
	{
		get
		{
			return this.PlatformMovement.LocalSpeedY == 0f;
		}
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x0007A3D0 File Offset: 0x000785D0
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.IsGrabbing && !this.PlatformMovement.IsOnGround && !this.PlatformMovement.IsOnCeiling)
		{
			settings.LockInput = true;
		}
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x0007A410 File Offset: 0x00078610
	public override void OnExit()
	{
		this.IsGrabbing = false;
		if (this.m_climbDownSoundPlayer)
		{
			this.m_climbDownSoundPlayer.FadeOut(0.3f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_climbDownSoundPlayer.gameObject);
			this.m_climbDownSoundPlayer = null;
		}
		base.OnExit();
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x0007A468 File Offset: 0x00078668
	public void OnGrabWall()
	{
		if (GameController.Instance.GameTime > this.m_lastWallGrabEnterSoundTime + this.m_minimumSoundDelay)
		{
			Sound.Play(this.WallGrabEnterSound.GetSoundForMaterial(this.Sein.PlatformBehaviour.WallSurfaceMaterialType, null), this.PlatformMovement.Position, null);
			this.m_lastWallGrabEnterSoundTime = GameController.Instance.GameTime;
		}
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x0007A4D0 File Offset: 0x000786D0
	public void OnReleaseWall()
	{
		this.Sein.Abilities.WallSlide.ResetMovingOffWallLockTimer();
		if (GameController.Instance.GameTime > this.m_lastWallGrabExitSoundTime + this.m_minimumSoundDelay)
		{
			Sound.Play(this.WallGrabExitSound.GetSoundForMaterial(this.Sein.PlatformBehaviour.WallSurfaceMaterialType, null), this.PlatformMovement.Position, null);
			this.m_lastWallGrabExitSoundTime = GameController.Instance.GameTime;
		}
		if (this.m_climbDownSoundPlayer)
		{
			this.m_climbDownSoundPlayer.FadeOut(0f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_climbDownSoundPlayer.gameObject);
			this.m_climbDownSoundPlayer = null;
		}
	}

	// Token: 0x170004AD RID: 1197
	// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0007A589 File Offset: 0x00078789
	// (set) Token: 0x06001C3C RID: 7228 RVA: 0x0007A591 File Offset: 0x00078791
	public bool IsGrabbing
	{
		get
		{
			return this.m_isGrabbing;
		}
		set
		{
			if (this.m_isGrabbing != value)
			{
				this.m_isGrabbing = value;
				if (this.m_isGrabbing)
				{
					this.OnGrabWall();
				}
				else
				{
					this.OnReleaseWall();
				}
			}
		}
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x0007A5C4 File Offset: 0x000787C4
	public void UpdateGrabbing()
	{
		if (this.IsGrabbing && this.Sein.Controller.CanMove && this.Sein.Input.Up.Pressed && !this.PlatformMovement.HeadAgainstWall)
		{
			if (this.Sein.Abilities.Glide)
			{
				this.Sein.Abilities.Glide.NeedsRightTriggerReleased = true;
			}
			this.Sein.Abilities.EdgeClamber.PerformEdgeClamber();
		}
		if (!this.CanGrab)
		{
			this.IsGrabbing = false;
			return;
		}
		if (!this.WantToGrab)
		{
			this.IsGrabbing = false;
			return;
		}
		if (ForceGrabReleaseZone.InsideZone(this.Sein.Position))
		{
			this.m_requiresRelease = true;
		}
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		if (Characters.Sein.Controller.CanMove)
		{
			if (this.LockVerticalMovement || this.Sein.Controller.IsChargingJump)
			{
				localSpeed.y = 0f;
			}
			else if (this.Sein.Input.Up.Pressed)
			{
				localSpeed.y = Mathf.Clamp(localSpeed.y + this.Acceleration * Time.deltaTime, 0f, this.ClimbSpeedUp);
			}
			else if (this.Sein.Input.Down.Pressed)
			{
				localSpeed.y = Mathf.Clamp(localSpeed.y - this.Acceleration * Time.deltaTime, -this.ClimbSpeedDown, 0f);
			}
			else
			{
				localSpeed.y = 0f;
			}
		}
		this.HandleWallClimbUpSteps();
		this.HandleWallClimbDownSteps();
		this.PlatformMovement.LocalSpeed = localSpeed;
		if (this.ShouldGrabWallUpAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.GrabWallAnimation.ClimbUp, 25, new Func<bool>(this.ShouldGrabWallUpAnimationKeepPlaying), false);
		}
		if (this.ShouldGrabWallDownAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.GrabWallAnimation.ClimbDown, 25, new Func<bool>(this.ShouldGrabWallDownAnimationKeepPlaying), false);
		}
		if (this.ShouldGrabWallAwayAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.PickAwayAnimation(), 25, new Func<bool>(this.ShouldGrabWallAwayAnimationKeepPlaying), true);
		}
		if (this.ShouldGrabWallIdleAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.GrabWallAnimation.Idle, 25, new Func<bool>(this.ShouldGrabWallIdleAnimationKeepPlaying), false);
		}
		this.m_currentTime += Time.deltaTime;
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x0007A8BC File Offset: 0x00078ABC
	public override void UpdateCharacterState()
	{
		if (this.IsGrabbing)
		{
			this.UpdateGrabbing();
		}
		else if (this.WantToGrab)
		{
			if (!this.Sein.Abilities.WallSlide.IsWallSliding)
			{
				this.m_requiresRelease = false;
			}
			if (this.CanGrab)
			{
				this.IsGrabbing = true;
			}
		}
		else
		{
			this.m_requiresRelease = false;
		}
	}

	// Token: 0x170004AE RID: 1198
	// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0007A929 File Offset: 0x00078B29
	public bool WantToGrab
	{
		get
		{
			return Core.Input.Glide.IsPressed;
		}
	}

	// Token: 0x170004AF RID: 1199
	// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0007A938 File Offset: 0x00078B38
	public bool CanGrab
	{
		get
		{
			return this.Sein.Abilities.WallSlide.IsOnWall && (this.Sein.PlatformBehaviour.PlatformMovement.HasWallLeft || !this.Sein.Controller.FaceLeft) && (this.Sein.PlatformBehaviour.PlatformMovement.HasWallRight || this.Sein.Controller.FaceLeft) && !this.m_requiresRelease && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities) && this.PlatformMovement.HeadAgainstWall;
		}
	}

	// Token: 0x170004B0 RID: 1200
	// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0007A9E8 File Offset: 0x00078BE8
	public bool ShouldGrabWallUpAnimationPlay
	{
		get
		{
			return this.ShouldGrabWallUpAnimationKeepPlaying();
		}
	}

	// Token: 0x170004B1 RID: 1201
	// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0007A9F0 File Offset: 0x00078BF0
	public bool ShouldGrabWallDownAnimationPlay
	{
		get
		{
			return this.ShouldGrabWallDownAnimationKeepPlaying();
		}
	}

	// Token: 0x170004B2 RID: 1202
	// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0007A9F8 File Offset: 0x00078BF8
	public bool ShouldGrabWallAwayAnimationPlay
	{
		get
		{
			return this.ShouldGrabWallAwayAnimationKeepPlaying();
		}
	}

	// Token: 0x170004B3 RID: 1203
	// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0007AA00 File Offset: 0x00078C00
	public bool ShouldGrabWallIdleAnimationPlay
	{
		get
		{
			return this.ShouldGrabWallIdleAnimationKeepPlaying();
		}
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x0007AA08 File Offset: 0x00078C08
	public bool ShouldGrabWallUpAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.PlatformMovement.LocalSpeedY > 0f;
	}

	// Token: 0x06001C46 RID: 7238 RVA: 0x0007AA38 File Offset: 0x00078C38
	public bool ShouldGrabWallDownAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.PlatformMovement.LocalSpeedY < 0f;
	}

	// Token: 0x06001C47 RID: 7239 RVA: 0x0007AA68 File Offset: 0x00078C68
	public bool ShouldGrabWallAwayAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.PlatformMovement.LocalSpeedY == 0f && this.IsGrabbingAway;
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x0007AAA0 File Offset: 0x00078CA0
	public bool ShouldGrabWallIdleAnimationKeepPlaying()
	{
		return this.IsGrabbing && this.PlatformMovement.LocalSpeedY == 0f && !this.IsGrabbingAway;
	}

	// Token: 0x170004B4 RID: 1204
	// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0007AADC File Offset: 0x00078CDC
	public bool IsGrabbingAway
	{
		get
		{
			return (this.Sein.Input.NormalizedHorizontal == -1 && this.PlatformMovement.HasWallRight) || (this.Sein.Input.NormalizedHorizontal == 1 && this.PlatformMovement.HasWallLeft);
		}
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x0007AB38 File Offset: 0x00078D38
	public void HandleWallClimbUpSteps()
	{
		if (this.PlatformMovement.LocalSpeedY > 0f && this.m_nextWallClimbUpTime < this.m_currentTime)
		{
			Sound.Play(this.WallGrabStepUpSound.GetSoundForMaterial(this.Sein.PlatformBehaviour.WallSurfaceMaterialType, null), this.PlatformMovement.Position, null);
			this.m_nextWallClimbUpTime = this.m_currentTime + 1f / this.WallClimbUpStepsPerSecond;
		}
	}

	// Token: 0x06001C4B RID: 7243 RVA: 0x0007ABB4 File Offset: 0x00078DB4
	public void HandleWallClimbDownSteps()
	{
		if (this.PlatformMovement.LocalSpeedY < 0f)
		{
			if (InstantiateUtility.IsDestroyed(this.m_climbDownSoundPlayer) && GameController.Instance.GameTime > this.m_lastWallGrabStepDownSoundTime + this.m_minimumSoundDelay)
			{
				this.m_climbDownSoundPlayer = Sound.PlayLooping(this.WallGrabStepDownSound.GetSoundForMaterial(this.Sein.PlatformBehaviour.WallSurfaceMaterialType, null), this.PlatformMovement.Position, delegate()
				{
					this.m_climbDownSoundPlayer = null;
				});
				this.m_lastWallGrabStepDownSoundTime = GameController.Instance.GameTime;
			}
		}
		else if (!InstantiateUtility.IsDestroyed(this.m_climbDownSoundPlayer))
		{
			this.m_climbDownSoundPlayer.FadeOut(0.3f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_climbDownSoundPlayer.gameObject);
			this.m_climbDownSoundPlayer = null;
		}
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x0007AC92 File Offset: 0x00078E92
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.GrabWall = this;
	}

	// Token: 0x04001873 RID: 6259
	public SeinCharacter Sein;

	// Token: 0x04001874 RID: 6260
	public float WallClimbUpStepsPerSecond;

	// Token: 0x04001875 RID: 6261
	public float WallClimbDownStepsPerSecond;

	// Token: 0x04001876 RID: 6262
	public SurfaceToSoundProviderMap WallGrabEnterSound;

	// Token: 0x04001877 RID: 6263
	public SurfaceToSoundProviderMap WallGrabExitSound;

	// Token: 0x04001878 RID: 6264
	public SurfaceToSoundProviderMap WallGrabStepUpSound;

	// Token: 0x04001879 RID: 6265
	public SurfaceToSoundProviderMap WallGrabStepDownSound;

	// Token: 0x0400187A RID: 6266
	private float m_minimumSoundDelay = 0.4f;

	// Token: 0x0400187B RID: 6267
	private float m_lastWallGrabEnterSoundTime;

	// Token: 0x0400187C RID: 6268
	private float m_lastWallGrabExitSoundTime;

	// Token: 0x0400187D RID: 6269
	private float m_lastWallGrabStepDownSoundTime = -10f;

	// Token: 0x0400187E RID: 6270
	public bool LockVerticalMovement;

	// Token: 0x0400187F RID: 6271
	public SeinGrabWall.GrabWallAnimationSet GrabWallAnimation;

	// Token: 0x04001880 RID: 6272
	public TextureAnimationWithTransitions EdgeClimbAnimation;

	// Token: 0x04001881 RID: 6273
	public float ClimbSpeedUp;

	// Token: 0x04001882 RID: 6274
	public float ClimbSpeedDown;

	// Token: 0x04001883 RID: 6275
	public float Acceleration = 60f;

	// Token: 0x04001884 RID: 6276
	private float m_currentTime;

	// Token: 0x04001885 RID: 6277
	private bool m_isGrabbing;

	// Token: 0x04001886 RID: 6278
	private float m_nextWallClimbUpTime;

	// Token: 0x04001887 RID: 6279
	private bool m_requiresRelease;

	// Token: 0x04001888 RID: 6280
	private SoundPlayer m_climbDownSoundPlayer;

	// Token: 0x0200044A RID: 1098
	[Serializable]
	public class GrabWallAnimationSet
	{
		// Token: 0x04001A4E RID: 6734
		public TextureAnimationWithTransitions Idle;

		// Token: 0x04001A4F RID: 6735
		public TextureAnimationWithTransitions ClimbUp;

		// Token: 0x04001A50 RID: 6736
		public TextureAnimationWithTransitions ClimbDown;

		// Token: 0x04001A51 RID: 6737
		public TextureAnimationWithTransitions[] Away;

		// Token: 0x04001A52 RID: 6738
		public TextureAnimationWithTransitions GrabAway;
	}
}
