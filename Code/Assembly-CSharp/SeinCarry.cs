using System;
using System.Collections;
using Core;
using fsm;
using Game;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class SeinCarry : CharacterState, ISeinReceiver
{
	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x0600084F RID: 2127 RVA: 0x00023D03 File Offset: 0x00021F03
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000850 RID: 2128 RVA: 0x00023D15 File Offset: 0x00021F15
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06000851 RID: 2129 RVA: 0x00023D27 File Offset: 0x00021F27
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06000852 RID: 2130 RVA: 0x00023D3E File Offset: 0x00021F3E
	// (set) Token: 0x06000853 RID: 2131 RVA: 0x00023D46 File Offset: 0x00021F46
	public bool LockDroppingObject { get; set; }

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06000854 RID: 2132 RVA: 0x00023D4F File Offset: 0x00021F4F
	public ICarryable CurrentCarryable
	{
		get
		{
			return this.m_currentCarryable;
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00023D57 File Offset: 0x00021F57
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Carry = this;
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00023D74 File Offset: 0x00021F74
	public override void Awake()
	{
		base.Awake();
		this.State.Inactive = new State
		{
			UpdateStateEvent = new Action(this.UpdateInactiveState),
			OnEnterEvent = new Action(this.EnterInactiveState)
		};
		this.State.Carry = new State
		{
			UpdateStateEvent = new Action(this.UpdateCarryState)
		};
		this.State.Pickup = new State();
		this.State.Dropping = new State();
		this.Logic.RegisterStates(new IState[]
		{
			this.State.Inactive,
			this.State.Carry,
			this.State.Pickup,
			this.State.Dropping
		});
		this.Logic.ChangeState(this.State.Inactive);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00023E60 File Offset: 0x00022060
	private void EnterInactiveState()
	{
		this.m_currentCarryable = null;
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00023E6C File Offset: 0x0002206C
	public void Start()
	{
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		this.Sein.PlatformBehaviour.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.OnModifyHorizontalPlatformMovementSettings;
		this.Sein.Abilities.Jump.OnJumpEvent += this.OnJump;
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00023ED1 File Offset: 0x000220D1
	public override void UpdateCharacterState()
	{
		this.Logic.UpdateState(Time.deltaTime);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00023EE4 File Offset: 0x000220E4
	public override void Serialize(Archive ar)
	{
		this.Logic.Serialize(ar);
		ar.Serialize(ref this.m_pressedXTime);
		ar.Serialize(ref this.m_lockPlayer);
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00023F15 File Offset: 0x00022115
	public void OnRestoreCheckpoint()
	{
		this.m_lockPlayer = false;
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00023F20 File Offset: 0x00022120
	public override void OnDestroy()
	{
		base.OnDestroy();
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		this.Sein.PlatformBehaviour.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.OnModifyHorizontalPlatformMovementSettings;
		this.Sein.Abilities.Jump.OnJumpEvent -= this.OnJump;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00023F8C File Offset: 0x0002218C
	public void OnPickup(ICarryable carryable)
	{
		this.m_pressedXTime = float.MaxValue;
		this.Logic.ChangeState(this.State.Pickup);
		this.Sein.Controller.PlayAnimation(this.Animations.PickupAnimation);
		this.Sein.Controller.OnTriggeredAnimationFinished += this.OnPickupAnimationFinished;
		if (base.gameObject.activeSelf)
		{
			base.StartCoroutine(this.DelayedPickupSound());
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0002400E File Offset: 0x0002220E
	public void OnSetToCarryMode(ICarryable carryable)
	{
		this.m_currentCarryable = carryable;
		this.Logic.ChangeState(this.State.Carry);
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00024030 File Offset: 0x00022230
	private IEnumerator DelayedPickupSound()
	{
		yield return new WaitForSeconds(0.8f);
		if (this.OnPickupSoundProvider)
		{
			Sound.Play(this.OnPickupSoundProvider.GetSound(null), this.PlatformMovement.Position, null);
		}
		yield break;
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0002404C File Offset: 0x0002224C
	private void OnPickupAnimationFinished()
	{
		this.Sein.Controller.OnTriggeredAnimationFinished -= this.OnPickupAnimationFinished;
		this.Logic.ChangeState(this.State.Carry);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0002408C File Offset: 0x0002228C
	private void OnDropAnimationFinished()
	{
		this.Sein.Controller.OnTriggeredAnimationFinished -= this.OnDropAnimationFinished;
		this.Logic.ChangeState(this.State.Inactive);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x000240CC File Offset: 0x000222CC
	public void OnDrop()
	{
		if (this.Sein.Controller.IsPlayingAnimation)
		{
			this.Logic.ChangeState(this.State.Inactive);
		}
		else
		{
			this.Sein.Controller.PlayAnimation(this.Animations.DropAnimation);
			this.Sein.Controller.OnTriggeredAnimationFinished += this.OnDropAnimationFinished;
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00024140 File Offset: 0x00022340
	public void OnSetToDropMode()
	{
		this.Logic.ChangeState(this.State.Inactive);
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00024158 File Offset: 0x00022358
	private void UpdateCarryState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		if (Core.Input.Glide.OnPressed)
		{
			Core.Input.Glide.Used = true;
			this.m_pressedXTime = 0f;
		}
		if (Core.Input.Glide.Pressed)
		{
			this.m_pressedXTime += Time.deltaTime;
		}
		if ((Component)this.m_currentCarryable == null)
		{
			this.Logic.ChangeState(this.State.Inactive);
			return;
		}
		if (!this.LockDroppingObject && Core.Input.Glide.Released && this.m_pressedXTime < 1f && this.Sein.Controller.CanMove && this.PlatformMovement.IsOnGround)
		{
			this.CurrentCarryable.Drop();
		}
		this.UpdateAnimations();
		this.HandleFootsteps();
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00024250 File Offset: 0x00022450
	private void UpdateInactiveState()
	{
		if (this.Sein.Controller.CanMove && this.PlatformMovement.IsOnGround)
		{
			for (int i = 0; i < Items.Carryables.Count; i++)
			{
				ICarryable carryable = Items.Carryables[i];
				if (Vector3.Distance(((Component)carryable).transform.position, this.Sein.Position) < this.CarryRange && carryable != null && carryable.CanBeCarried() && Core.Input.Glide.OnPressed)
				{
					carryable.Pickup();
				}
			}
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x000242FA File Offset: 0x000224FA
	private void OnModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.IsCarrying)
		{
			settings.Ground.ApplySpeedMultiplier(this.GroundMultiplier);
			settings.Air.ApplySpeedMultiplier(this.AirMultiplier);
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0002432C File Offset: 0x0002252C
	private void HandleFootsteps()
	{
		if (this.PlatformMovement.IsOnGround && this.PlatformMovement.MovingHorizontally && this.m_nextWalkTime < this.Logic.CurrentStateTime)
		{
			Sound.Play(this.FootstepsSoundProvider.GetSoundForMaterial(SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider), null), this.PlatformMovement.Position, null);
			this.m_nextWalkTime = this.Logic.CurrentStateTime + 1f / this.WalkSoundsPerSecond;
		}
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000243C8 File Offset: 0x000225C8
	private void UpdateAnimations()
	{
		if (this.ShouldRunMovingAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Animations.RunMovingAnimation, 152, new Func<bool>(this.ShouldRunMovingAnimationKeepPlaying), false);
		}
		else if (this.ShouldRunIdleAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Animations.RunIdleAnimation, 152, new Func<bool>(this.ShouldRunIdleAnimationKeepPlaying), false);
		}
		if (this.ShouldFallMovingAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Animations.FallMovingAnimation, 151, new Func<bool>(this.ShouldFallMovingAnimationKeepPlaying), false);
		}
		else if (this.ShouldFallIdleAnimationPlay)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Animations.FallIdleAnimation, 151, new Func<bool>(this.ShouldFallIdleAnimationKeepPlaying), false);
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x000244EC File Offset: 0x000226EC
	private void OnJump(float strength)
	{
		if (this.IsCarrying)
		{
			if (this.Sein.Input.NormalizedHorizontal == 0)
			{
				this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.Animations.JumpIdleAnimation, 153, new Func<bool>(this.ShouldJumpIdleAnimationKeepPlaying));
			}
			else
			{
				this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.Animations.JumpMovingAnimation, 153, new Func<bool>(this.ShouldJumpMovingAnimationKeepPlaying));
			}
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x0600086A RID: 2154 RVA: 0x0002458C File Offset: 0x0002278C
	private bool ShouldRunMovingAnimationPlay
	{
		get
		{
			return this.ShouldRunMovingAnimationKeepPlaying();
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x0600086B RID: 2155 RVA: 0x00024594 File Offset: 0x00022794
	private bool ShouldRunIdleAnimationPlay
	{
		get
		{
			return this.ShouldRunIdleAnimationKeepPlaying();
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x0600086C RID: 2156 RVA: 0x0002459C File Offset: 0x0002279C
	private bool ShouldFallMovingAnimationPlay
	{
		get
		{
			return this.ShouldFallMovingAnimationKeepPlaying();
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x0600086D RID: 2157 RVA: 0x000245A4 File Offset: 0x000227A4
	private bool ShouldFallIdleAnimationPlay
	{
		get
		{
			return this.ShouldFallIdleAnimationKeepPlaying();
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x0600086E RID: 2158 RVA: 0x000245AC File Offset: 0x000227AC
	public bool IsCarrying
	{
		get
		{
			return this.Logic.CurrentState == this.State.Carry;
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x0600086F RID: 2159 RVA: 0x000245C6 File Offset: 0x000227C6
	public bool IsPickingUp
	{
		get
		{
			return this.Logic.CurrentState == this.State.Pickup;
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x000245E0 File Offset: 0x000227E0
	private bool ShouldRunMovingAnimationKeepPlaying()
	{
		return this && this.IsCarrying && this.PlatformMovement.IsOnGround && Mathf.Abs(this.LeftRightMovement.HorizontalInput) > 0f;
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00024630 File Offset: 0x00022830
	private bool ShouldRunIdleAnimationKeepPlaying()
	{
		return this && this.IsCarrying && this.PlatformMovement.IsOnGround && this.LeftRightMovement.HorizontalInput == 0f;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0002467C File Offset: 0x0002287C
	private bool ShouldJumpIdleAnimationKeepPlaying()
	{
		return this && this.IsCarrying && this.Sein.PlatformBehaviour.PlatformMovement.IsInAir && (!this.Sein.Controller.CanMove || this.Sein.Input.NormalizedHorizontal == 0);
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x000246EC File Offset: 0x000228EC
	private bool ShouldJumpMovingAnimationKeepPlaying()
	{
		return this && this.IsCarrying && this.Sein.PlatformBehaviour.PlatformMovement.IsInAir && (!this.Sein.Controller.CanMove || this.Sein.Input.NormalizedHorizontal != 0);
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0002475C File Offset: 0x0002295C
	private bool ShouldFallMovingAnimationKeepPlaying()
	{
		return this && this.IsCarrying && (!this.Sein.Controller.CanMove || this.Sein.Input.NormalizedHorizontal != 0) && !this.PlatformMovement.IsOnGround;
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x000247BC File Offset: 0x000229BC
	private bool ShouldFallIdleAnimationKeepPlaying()
	{
		return this && this.IsCarrying && (!this.Sein.Controller.CanMove || this.Sein.Input.NormalizedHorizontal == 0) && !this.PlatformMovement.IsOnGround;
	}

	// Token: 0x040006A3 RID: 1699
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet AirMultiplier;

	// Token: 0x040006A4 RID: 1700
	public SeinCarry.CarryAnimations Animations;

	// Token: 0x040006A5 RID: 1701
	public float CarryRange = 2f;

	// Token: 0x040006A6 RID: 1702
	public SurfaceToSoundProviderMap FootstepsSoundProvider;

	// Token: 0x040006A7 RID: 1703
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet GroundMultiplier;

	// Token: 0x040006A8 RID: 1704
	public StateMachine Logic = new StateMachine();

	// Token: 0x040006A9 RID: 1705
	public Varying2DSoundProvider OnPickupSoundProvider;

	// Token: 0x040006AA RID: 1706
	public SeinCharacter Sein;

	// Token: 0x040006AB RID: 1707
	public SeinCarry.States State = new SeinCarry.States();

	// Token: 0x040006AC RID: 1708
	public float WalkSoundsPerSecond;

	// Token: 0x040006AD RID: 1709
	private Transform m_crossHair;

	// Token: 0x040006AE RID: 1710
	private ICarryable m_currentCarryable;

	// Token: 0x040006AF RID: 1711
	private bool m_lockPlayer;

	// Token: 0x040006B0 RID: 1712
	private float m_nextWalkTime;

	// Token: 0x040006B1 RID: 1713
	private float m_pressedXTime = float.MaxValue;

	// Token: 0x02000430 RID: 1072
	[Serializable]
	public class CarryAnimations
	{
		// Token: 0x040019BF RID: 6591
		public TextureAnimationWithTransitions DropAnimation;

		// Token: 0x040019C0 RID: 6592
		public TextureAnimationWithTransitions FallIdleAnimation;

		// Token: 0x040019C1 RID: 6593
		public TextureAnimationWithTransitions FallMovingAnimation;

		// Token: 0x040019C2 RID: 6594
		public TextureAnimationWithTransitions JumpIdleAnimation;

		// Token: 0x040019C3 RID: 6595
		public TextureAnimationWithTransitions JumpMovingAnimation;

		// Token: 0x040019C4 RID: 6596
		public TextureAnimationWithTransitions PickupAnimation;

		// Token: 0x040019C5 RID: 6597
		public TextureAnimationWithTransitions RunIdleAnimation;

		// Token: 0x040019C6 RID: 6598
		public TextureAnimationWithTransitions RunMovingAnimation;
	}

	// Token: 0x02000431 RID: 1073
	public class States
	{
		// Token: 0x040019C7 RID: 6599
		public IState Carry;

		// Token: 0x040019C8 RID: 6600
		public IState Dropping;

		// Token: 0x040019C9 RID: 6601
		public IState Inactive;

		// Token: 0x040019CA RID: 6602
		public IState Pickup;
	}
}
