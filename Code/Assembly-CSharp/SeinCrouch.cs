using System;

// Token: 0x0200040F RID: 1039
public class SeinCrouch : CharacterState, ISeinReceiver
{
	// Token: 0x170004A3 RID: 1187
	// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00079FBC File Offset: 0x000781BC
	public CharacterCapsuleController CapsuleController
	{
		get
		{
			return this.Sein.PlatformBehaviour.CapsuleController;
		}
	}

	// Token: 0x170004A4 RID: 1188
	// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00079FCE File Offset: 0x000781CE
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170004A5 RID: 1189
	// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00079FE0 File Offset: 0x000781E0
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001C23 RID: 7203 RVA: 0x00079FF2 File Offset: 0x000781F2
	public void Start()
	{
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x0007A00B File Offset: 0x0007820B
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x0007A02A File Offset: 0x0007822A
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.IsCrouching)
		{
			settings.Ground.Acceleration = 0f;
			settings.Ground.MaxSpeed = 0f;
		}
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x0007A057 File Offset: 0x00078257
	public override void OnExit()
	{
		this.IsCrouching = false;
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x0007A060 File Offset: 0x00078260
	public override void UpdateCharacterState()
	{
		if (this.Sein.PlatformBehaviour.PlatformMovement.IsOnGround && this.Sein.Input.Down.OnPressed && this.Sein.Controller.CanMove && !this.Sein.Controller.IgnoreControllerInput)
		{
			this.PlatformMovement.LocalSpeedX = 0f;
			this.IsCrouching = true;
		}
		else if (this.Sein.PlatformBehaviour.PlatformMovement.IsOnGround && this.Sein.Input.Down.Pressed && !this.PlatformMovement.MovingHorizontally && this.Sein.Controller.CanMove && !this.Sein.Controller.IgnoreControllerInput)
		{
			this.PlatformMovement.LocalSpeedX = 0f;
			this.IsCrouching = true;
		}
		else
		{
			this.IsCrouching = false;
		}
		if (this.ShouldCrouchIdleAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.IdleAnimation, 40, new Func<bool>(this.ShouldCrouchIdleAnimationKeepPlaying), false);
		}
	}

	// Token: 0x170004A6 RID: 1190
	// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0007A1B3 File Offset: 0x000783B3
	public bool ShouldCrouchIdleAnimationPlay
	{
		get
		{
			return this.ShouldCrouchIdleAnimationKeepPlaying();
		}
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x0007A1BB File Offset: 0x000783BB
	public bool ShouldCrouchIdleAnimationKeepPlaying()
	{
		return this.PlatformMovement.IsOnGround && this.IsCrouching;
	}

	// Token: 0x170004A7 RID: 1191
	// (get) Token: 0x06001C2A RID: 7210 RVA: 0x0007A1D6 File Offset: 0x000783D6
	// (set) Token: 0x06001C2B RID: 7211 RVA: 0x0007A1DE File Offset: 0x000783DE
	public bool IsCrouching
	{
		get
		{
			return this.m_isCrouching;
		}
		set
		{
			if (this.m_isCrouching != value)
			{
				this.m_isCrouching = value;
				if (this.m_isCrouching)
				{
					this.CapsuleController.ChangeToSphere();
				}
				else
				{
					this.CapsuleController.Restore();
				}
			}
		}
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x0007A219 File Offset: 0x00078419
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Crouch = this;
	}

	// Token: 0x04001870 RID: 6256
	public SeinCharacter Sein;

	// Token: 0x04001871 RID: 6257
	public TextureAnimationWithTransitions IdleAnimation;

	// Token: 0x04001872 RID: 6258
	private bool m_isCrouching;
}
