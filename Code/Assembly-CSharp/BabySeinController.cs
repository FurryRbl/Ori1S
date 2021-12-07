using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class BabySeinController : MonoBehaviour, ISuspendable
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000071 RID: 113 RVA: 0x00003767 File Offset: 0x00001967
	public bool LockedInput
	{
		get
		{
			return GameController.Instance.InputLocked;
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003773 File Offset: 0x00001973
	public void Awake()
	{
		SuspensionManager.Register(this);
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003785 File Offset: 0x00001985
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000378D File Offset: 0x0000198D
	public void Start()
	{
		this.BabySein.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
		this.UpdateAnimations();
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000075 RID: 117 RVA: 0x000037B4 File Offset: 0x000019B4
	public Vector3 Position
	{
		get
		{
			return this.BabySein.PlatformBehaviour.PlatformMovement.Position;
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000037CC File Offset: 0x000019CC
	public void HandleStopRunningInDirections()
	{
		foreach (StopRunningInDirectionZone stopRunningInDirectionZone in StopRunningInDirectionZone.All)
		{
			if (stopRunningInDirectionZone.Bounds.Contains(this.Position))
			{
				if (stopRunningInDirectionZone.StopLeft)
				{
					if (this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput < 0f)
					{
						this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput = 0f;
					}
				}
				else if (this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput > 0f)
				{
					this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput = 0f;
				}
			}
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000038BC File Offset: 0x00001ABC
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.BabySein.PlatformBehaviour.Gravity.UpdateCharacterState();
		PlatformMovement platformMovement = this.BabySein.PlatformBehaviour.PlatformMovement;
		CharacterLeftRightMovement leftRightMovement = this.BabySein.PlatformBehaviour.LeftRightMovement;
		if (!this.IgnoreControllerInput)
		{
			if (platformMovement.IsOnGround)
			{
				leftRightMovement.HorizontalInput = 0f;
				if (Mathf.Abs(Core.Input.Horizontal) > 0.4f)
				{
					leftRightMovement.HorizontalInput = Mathf.Sign(Core.Input.Horizontal) * Mathf.Lerp(0.6f, 1f, Mathf.InverseLerp(0.4f, 0.8f, Mathf.Abs(Core.Input.Horizontal)));
				}
			}
			else
			{
				leftRightMovement.HorizontalInput = (float)Core.Input.NormalizedHorizontal;
			}
		}
		if (this.LockedInput)
		{
			leftRightMovement.HorizontalInput = 0f;
		}
		this.HandleStopRunningInDirections();
		foreach (MoveHorizontallyZone moveHorizontallyZone in MoveHorizontallyZone.All)
		{
			if (moveHorizontallyZone.Contains(this.BabySein.Position))
			{
				leftRightMovement.HorizontalInput = (float)((moveHorizontallyZone.Direction != MoveHorizontallyZone.MoveDirection.Left) ? 1 : -1);
			}
		}
		bool flag = !this.LockedInput && !this.IgnoreControllerInput;
		foreach (RestrictJumpingZone restrictJumpingZone in RestrictJumpingZone.All)
		{
			if (restrictJumpingZone.Contains(this.BabySein.Position))
			{
				flag = false;
			}
		}
		if (flag && Core.Input.Jump.OnPressed && platformMovement.IsOnGround)
		{
			this.Jump();
		}
		if (!platformMovement.Ground.WasOn && platformMovement.Ground.IsOn)
		{
			this.Land();
		}
		if (!Core.Input.Jump.Pressed && !platformMovement.IsOnGround && platformMovement.LocalSpeedY > 0f)
		{
			platformMovement.LocalSpeedY -= 38f * Time.deltaTime;
		}
		if (platformMovement.IsOnGround)
		{
			if (MoonMath.Float.Normalize(platformMovement.LocalSpeedX) != MoonMath.Float.Normalize(leftRightMovement.HorizontalInput))
			{
				platformMovement.LocalSpeedX = 0f;
			}
			platformMovement.LocalSpeedY -= 38f * Time.deltaTime;
		}
		this.BabySein.Sounds.HandleFootstepEvents();
		this.UpdateAnimations();
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003B80 File Offset: 0x00001D80
	public void Jump()
	{
		this.BabySein.PlatformBehaviour.PlatformMovement.LocalSpeedY = MoonMath.Physics.SpeedFromHeightAndGravity(this.BabySein.PlatformBehaviour.Gravity.BaseSettings.GravityStrength, this.JumpHeight);
		this.BabySein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.BabySein.PlatformBehaviour.PlatformMovement.LocalSpeedY, 1f);
		if (this.ShouldJumpAnimationPlay())
		{
			this.BabySein.Animation.Play(this.Animations.Jump, 2, new Func<bool>(this.ShouldJumpAnimationPlay));
		}
		if (this.ShouldJumpIdleAnimationPlay())
		{
			this.BabySein.Animation.Play(this.Animations.JumpIdle, 2, new Func<bool>(this.ShouldJumpIdleAnimationPlay));
		}
		this.BabySein.Sounds.OnJump();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003C6E File Offset: 0x00001E6E
	public void Land()
	{
		this.BabySein.Sounds.OnLand();
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003C80 File Offset: 0x00001E80
	public void UpdateAnimations()
	{
		if (this.ShouldIdleAnimationPlay())
		{
			this.BabySein.Animation.PlayLoop(this.Animations.Idle, 0, new Func<bool>(this.ShouldIdleAnimationPlay), false);
		}
		else if (this.ShouldWalkAnimationPlay())
		{
			this.BabySein.Animation.PlayLoop(this.Animations.Walk, 0, new Func<bool>(this.ShouldWalkAnimationPlay), false);
		}
		else if (this.ShouldFallAnimationPlay())
		{
			this.BabySein.Animation.PlayLoop(this.Animations.Fall, 0, new Func<bool>(this.ShouldFallAnimationPlay), false);
		}
		else if (this.ShouldFallIdleAnimationPlay())
		{
			this.BabySein.Animation.PlayLoop(this.Animations.FallIdle, 0, new Func<bool>(this.ShouldFallIdleAnimationPlay), false);
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003D70 File Offset: 0x00001F70
	public bool ShouldWalkAnimationPlay()
	{
		return this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f && !this.BabySein.PlatformBehaviour.PlatformMovement.IsOnWall && this.BabySein.PlatformBehaviour.PlatformMovement.IsOnGround;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003DD0 File Offset: 0x00001FD0
	public bool ShouldIdleAnimationPlay()
	{
		return (this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f || this.BabySein.PlatformBehaviour.PlatformMovement.IsOnWall) && this.BabySein.PlatformBehaviour.PlatformMovement.IsOnGround;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003E2E File Offset: 0x0000202E
	public bool ShouldGrabBoxAnimationPlay()
	{
		return false;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003E31 File Offset: 0x00002031
	public bool ShouldFallAnimationPlay()
	{
		return this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003E52 File Offset: 0x00002052
	public bool ShouldFallIdleAnimationPlay()
	{
		return this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003E70 File Offset: 0x00002070
	public bool ShouldJumpIdleAnimationPlay()
	{
		return this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f && this.BabySein.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003EA9 File Offset: 0x000020A9
	public bool ShouldJumpAnimationPlay()
	{
		return this.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f && this.BabySein.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000082 RID: 130 RVA: 0x00003EE2 File Offset: 0x000020E2
	// (set) Token: 0x06000083 RID: 131 RVA: 0x00003EEA File Offset: 0x000020EA
	public bool IsSuspended { get; set; }

	// Token: 0x0400008D RID: 141
	public BabySein BabySein;

	// Token: 0x0400008E RID: 142
	public BabySeinController.AnimationSet Animations;

	// Token: 0x0400008F RID: 143
	public float JumpHeight = 10f;

	// Token: 0x04000090 RID: 144
	public bool IgnoreControllerInput;

	// Token: 0x02000015 RID: 21
	[Serializable]
	public class AnimationSet
	{
		// Token: 0x040000D3 RID: 211
		public TextureAnimationWithTransitions Fall;

		// Token: 0x040000D4 RID: 212
		public TextureAnimationWithTransitions FallIdle;

		// Token: 0x040000D5 RID: 213
		public TextureAnimationWithTransitions GrabBoxIdle;

		// Token: 0x040000D6 RID: 214
		public TextureAnimationWithTransitions Idle;

		// Token: 0x040000D7 RID: 215
		public TextureAnimationWithTransitions JumpIdle;

		// Token: 0x040000D8 RID: 216
		public TextureAnimationWithTransitions Jump;

		// Token: 0x040000D9 RID: 217
		public TextureAnimationWithTransitions Walk;
	}

	// Token: 0x02000017 RID: 23
	public enum AnimationLayers
	{
		// Token: 0x040000DE RID: 222
		FallLayer,
		// Token: 0x040000DF RID: 223
		WalkLayer,
		// Token: 0x040000E0 RID: 224
		JumpLayer,
		// Token: 0x040000E1 RID: 225
		PushLayer
	}
}
