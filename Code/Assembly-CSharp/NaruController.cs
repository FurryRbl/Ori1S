using System;
using Core;
using fsm;
using Game;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class NaruController : MonoBehaviour, ISuspendable
{
	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000298 RID: 664 RVA: 0x0000ADE3 File Offset: 0x00008FE3
	public bool LockedInput
	{
		get
		{
			return GameController.Instance.InputLocked || this.m_lockHorizontalMovementDuration > 0f;
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000AE04 File Offset: 0x00009004
	public void Awake()
	{
		SuspensionManager.Register(this);
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000AE16 File Offset: 0x00009016
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000AE1E File Offset: 0x0000901E
	public void Start()
	{
		this.Naru.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
		this.UpdateAnimations();
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000AE48 File Offset: 0x00009048
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.Naru.PlatformBehaviour.Gravity.UpdateCharacterState();
		this.Naru.PlatformBehaviour.JumpSustain.UpdateCharacterState();
		this.Naru.PlatformBehaviour.InstantStop.UpdateCharacterState();
		float num = (float)Core.Input.NormalizedHorizontal;
		if (this.m_lockChangingDirectionDuration > 0f)
		{
			this.m_lockChangingDirectionDuration = Mathf.Max(0f, this.m_lockChangingDirectionDuration - Time.deltaTime);
			num = this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput;
		}
		if (Mathf.Abs(num) > 0f && Mathf.Sign(num) != (float)((!this.Naru.FaceLeft) ? 1 : -1))
		{
			this.m_lockChangingDirectionDuration = 0.3f;
		}
		for (int i = 0; i < MoveHorizontallyZone.All.Count; i++)
		{
			MoveHorizontallyZone moveHorizontallyZone = MoveHorizontallyZone.All[i];
			if (moveHorizontallyZone.Contains(this.Naru.Position))
			{
				num = (float)((moveHorizontallyZone.Direction != MoveHorizontallyZone.MoveDirection.Left) ? 1 : -1);
			}
		}
		this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput = num;
		if (this.LockedInput)
		{
			this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput = 0f;
		}
		this.m_lockHorizontalMovementDuration -= Time.deltaTime;
		bool flag = !this.LockedInput && this.JumpHeight > 0f;
		for (int j = 0; j < RestrictJumpingZone.All.Count; j++)
		{
			RestrictJumpingZone restrictJumpingZone = RestrictJumpingZone.All[j];
			if (restrictJumpingZone.Contains(this.Naru.Position))
			{
				flag = false;
			}
		}
		if (flag && Core.Input.Jump.OnPressed && this.Naru.PlatformBehaviour.PlatformMovement.IsOnGround)
		{
			this.Naru.Sounds.OnJump();
			this.Naru.PlatformBehaviour.PlatformMovement.LocalSpeedY = MoonMath.Physics.SpeedFromHeightAndGravity(this.Naru.PlatformBehaviour.Gravity.BaseSettings.GravityStrength, this.JumpHeight);
			this.Naru.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.Naru.PlatformBehaviour.PlatformMovement.LocalSpeedY, 1f);
			if (this.ShouldJumpAnimationPlay())
			{
				this.Naru.Animation.Play(this.Animations.Jump, 2, new Func<bool>(this.ShouldJumpAnimationPlay));
			}
			if (this.ShouldJumpIdleAnimationPlay())
			{
				this.Naru.Animation.Play(this.Animations.JumpIdle, 2, new Func<bool>(this.ShouldJumpIdleAnimationPlay));
			}
		}
		if (!this.Naru.PlatformBehaviour.PlatformMovement.Ground.WasOn && this.Naru.PlatformBehaviour.PlatformMovement.Ground.IsOn)
		{
			this.OnNaruLand();
		}
		this.Naru.Sounds.HandleFootstepEvents();
		this.UpdateAnimations();
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000B191 File Offset: 0x00009391
	public void OnNaruLand()
	{
		this.Naru.Sounds.OnLand();
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000B1A4 File Offset: 0x000093A4
	public void UpdateAnimations()
	{
		if (this.ShouldAgainstWallAnimationPlay())
		{
			this.Naru.Animation.PlayLoop(this.Animations.AgainstWall, 0, new Func<bool>(this.ShouldAgainstWallAnimationPlay), false);
		}
		else if (this.ShouldLookUpAnimationPlay())
		{
			this.Naru.Animation.PlayLoop(this.Animations.LookUp, 0, new Func<bool>(this.ShouldLookUpAnimationPlay), false);
		}
		else if (this.ShouldIdleAnimationPlay())
		{
			this.Naru.Animation.PlayLoop(this.Animations.Idle, 0, new Func<bool>(this.ShouldIdleAnimationPlay), false);
		}
		else if (this.ShouldWalkAnimationPlay())
		{
			this.Naru.Animation.PlayLoop(this.Animations.Walk, 0, new Func<bool>(this.ShouldWalkAnimationPlay), false);
		}
		else if (this.ShouldFallAnimationPlay())
		{
			this.Naru.Animation.PlayLoop(this.Animations.Fall, 0, new Func<bool>(this.ShouldFallAnimationPlay), false);
		}
		else if (this.ShouldFallIdleAnimationPlay())
		{
			this.Naru.Animation.PlayLoop(this.Animations.FallIdle, 0, new Func<bool>(this.ShouldFallIdleAnimationPlay), false);
		}
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000B308 File Offset: 0x00009508
	public bool ShouldAgainstWallAnimationPlay()
	{
		return this.Animations.AgainstWall && this.Naru.PlatformBehaviour.PlatformMovement.IsOnGround && this.Naru.PlatformBehaviour.PlatformMovement.IsOnWall;
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000B35C File Offset: 0x0000955C
	public bool ShouldLookUpAnimationPlay()
	{
		return this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput < 0f && NaruLookUpZone.IsInside && this.Naru.PlatformBehaviour.PlatformMovement.IsOnWall;
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000B3AC File Offset: 0x000095AC
	public bool ShouldWalkAnimationPlay()
	{
		return this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f && !this.Naru.PlatformBehaviour.PlatformMovement.IsOnWall && this.Naru.PlatformBehaviour.PlatformMovement.IsOnGround;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000B40C File Offset: 0x0000960C
	public bool ShouldIdleAnimationPlay()
	{
		return (this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f || this.Naru.PlatformBehaviour.PlatformMovement.IsOnWall) && this.Naru.PlatformBehaviour.PlatformMovement.IsOnGround;
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000B46A File Offset: 0x0000966A
	public bool ShouldFallAnimationPlay()
	{
		return this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f;
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000B48B File Offset: 0x0000968B
	public bool ShouldFallIdleAnimationPlay()
	{
		return this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000B4A9 File Offset: 0x000096A9
	public bool ShouldJumpIdleAnimationPlay()
	{
		return this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f && this.Naru.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000B4E2 File Offset: 0x000096E2
	public bool ShouldJumpAnimationPlay()
	{
		return this.Naru.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f && this.Naru.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B51B File Offset: 0x0000971B
	// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000B523 File Offset: 0x00009723
	public bool IsSuspended { get; set; }

	// Token: 0x040001E0 RID: 480
	public NaruController.AnimationSet Animations;

	// Token: 0x040001E1 RID: 481
	public float JumpHeight = 10f;

	// Token: 0x040001E2 RID: 482
	public StateMachine Logic;

	// Token: 0x040001E3 RID: 483
	public Naru Naru;

	// Token: 0x040001E4 RID: 484
	private float m_lockHorizontalMovementDuration;

	// Token: 0x040001E5 RID: 485
	private float m_lockChangingDirectionDuration;

	// Token: 0x0200003B RID: 59
	[Serializable]
	public class AnimationSet
	{
		// Token: 0x040001F0 RID: 496
		public TextureAnimationWithTransitions Fall;

		// Token: 0x040001F1 RID: 497
		public TextureAnimationWithTransitions FallIdle;

		// Token: 0x040001F2 RID: 498
		public TextureAnimationWithTransitions Idle;

		// Token: 0x040001F3 RID: 499
		public TextureAnimationWithTransitions Jump;

		// Token: 0x040001F4 RID: 500
		public TextureAnimationWithTransitions JumpIdle;

		// Token: 0x040001F5 RID: 501
		public TextureAnimationWithTransitions Walk;

		// Token: 0x040001F6 RID: 502
		public TextureAnimationWithTransitions AgainstWall;

		// Token: 0x040001F7 RID: 503
		public TextureAnimationWithTransitions LookUp;
	}

	// Token: 0x0200003C RID: 60
	public enum AnimationLayers
	{
		// Token: 0x040001F9 RID: 505
		FallLayer,
		// Token: 0x040001FA RID: 506
		WalkLayer,
		// Token: 0x040001FB RID: 507
		JumpLayer,
		// Token: 0x040001FC RID: 508
		PushLayer
	}
}
