using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class SeinWallJump : CharacterState, ISeinReceiver
{
	// Token: 0x14000033 RID: 51
	// (add) Token: 0x06001D16 RID: 7446 RVA: 0x0007F444 File Offset: 0x0007D644
	// (remove) Token: 0x06001D17 RID: 7447 RVA: 0x0007F45D File Offset: 0x0007D65D
	public event Action<Vector2> OnWallJumpEvent = delegate(Vector2 A_0)
	{
	};

	// Token: 0x170004E6 RID: 1254
	// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0007F476 File Offset: 0x0007D676
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170004E7 RID: 1255
	// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0007F488 File Offset: 0x0007D688
	public SeinDoubleJump DoubleJump
	{
		get
		{
			return this.Sein.Abilities.DoubleJump;
		}
	}

	// Token: 0x170004E8 RID: 1256
	// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0007F49A File Offset: 0x0007D69A
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170004E9 RID: 1257
	// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0007F4AC File Offset: 0x0007D6AC
	public CharacterSpriteMirror CharacterSpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x170004EA RID: 1258
	// (get) Token: 0x06001D1C RID: 7452 RVA: 0x0007F4C4 File Offset: 0x0007D6C4
	public bool CanPerformWallJump
	{
		get
		{
			return base.enabled && this.Sein.Abilities.WallSlide.IsOnWall && !this.PlatformMovement.IsOnGround && this.Sein.PlayerAbilities.WallJump.HasAbility;
		}
	}

	// Token: 0x170004EB RID: 1259
	// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0007F51E File Offset: 0x0007D71E
	// (set) Token: 0x06001D1E RID: 7454 RVA: 0x0007F528 File Offset: 0x0007D728
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

	// Token: 0x06001D1F RID: 7455 RVA: 0x0007F579 File Offset: 0x0007D779
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.WallJump = this;
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x0007F594 File Offset: 0x0007D794
	public void PerformWallJump()
	{
		if (this.PlatformMovement.HasWallLeft)
		{
			this.PerformWallJumpRight();
		}
		if (this.PlatformMovement.HasWallRight)
		{
			this.PerformWallJumpLeft();
		}
	}

	// Token: 0x06001D21 RID: 7457 RVA: 0x0007F5D0 File Offset: 0x0007D7D0
	public void PerformWallJumpLeft()
	{
		if (this.m_hasWallJumpedLeft)
		{
			return;
		}
		if (this.DontAllowJumpingTowardsWall && this.LeftRightMovement.BaseHorizontalInput > 0f)
		{
			return;
		}
		if (this.LeftRightMovement.BaseHorizontalInput > 0f && this.DoubleJump)
		{
			this.DoubleJump.LockForDuration(this.LockDoubleJumpTowardsDuration);
		}
		if (this.LimitWallJumping)
		{
			this.m_hasWallJumpedLeft = true;
		}
		this.m_hasWallJumpedRight = false;
		this.PlatformMovement.LocalSpeedX = -this.JumpStrength.x;
		this.PlatformMovement.LocalSpeedY = this.JumpStrength.y;
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		this.ApplyImpulseToWall(localSpeed);
		if (this.Sein.Input.NormalizedHorizontal < 0)
		{
			this.CharacterSpriteMirror.FaceLeft = true;
			CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.AwayAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpLeftAwayAnimation));
			characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
			characterAnimationState.OnStartPlaying = new Action(this.OnAnimationStart);
		}
		else if (this.Sein.Input.NormalizedHorizontal > 0)
		{
			Vector3 origin = this.PlatformMovement.Position2D + this.PlatformMovement.LocalToWorld(Vector3.up * 2f);
			float maxDistance = this.PlatformMovement.CapsuleCollider.radius + 2f;
			Ray ray = new Ray(origin, this.PlatformMovement.LocalToWorld(Vector3.right));
			if (Physics.Raycast(ray, maxDistance))
			{
				CharacterAnimationSystem.CharacterAnimationState characterAnimationState2 = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.TowardsAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpLeftTowardsAnimation));
				characterAnimationState2.OnStopPlaying = new Action(this.OnAnimationEnd);
				base.StartCoroutine(this.RoutineForMegWhoPlaysMarioAndSucksAtWallJumping());
			}
			else
			{
				CharacterAnimationSystem.CharacterAnimationState characterAnimationState3 = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.EdgeJumpAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpLeftTowardsAnimation));
				characterAnimationState3.OnStopPlaying = new Action(this.OnAnimationEnd);
				localSpeed.y = 0f;
			}
		}
		else
		{
			CharacterAnimationSystem.CharacterAnimationState characterAnimationState4 = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.RegularAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpLeftRegularAnimation));
			characterAnimationState4.OnStopPlaying = new Action(this.OnAnimationEnd);
			characterAnimationState4.OnStartPlaying = new Action(this.OnAnimationStart);
		}
		Sound.Play(this.WallJumpSound.GetSoundForMaterial(this.Sein.PlatformBehaviour.WallSurfaceMaterialType, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.OnWallJumpEvent(localSpeed);
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(localSpeed.y, 1f);
		}
		this.Sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = true;
		this.Sein.ResetAirLimits();
		JumpFlipPlatform.OnSeinWallJumpEvent();
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x0007F950 File Offset: 0x0007DB50
	public IEnumerator RoutineForMegWhoPlaysMarioAndSucksAtWallJumping()
	{
		float i = (float)this.Sein.Input.NormalizedHorizontal;
		bool left = i < 0f;
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		for (float t = 0f; t < 0.2f; t += Time.deltaTime)
		{
			if (Core.Input.Jump.OnPressed)
			{
				break;
			}
			if (this.PlatformMovement.IsOnWall)
			{
				break;
			}
			if ((float)this.Sein.Input.NormalizedHorizontal == -i)
			{
				this.PlatformMovement.LocalSpeedX = this.JumpStrength.x * (float)((!left) ? -1 : 1);
				this.CharacterSpriteMirror.FaceLeft = !left;
				CharacterAnimationSystem.CharacterAnimationState state = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.AwayAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpLeftAwayAnimation));
				state.OnStopPlaying = new Action(this.OnAnimationEnd);
				state.OnStartPlaying = new Action(this.OnAnimationStart);
				if (this.DoubleJump)
				{
					this.DoubleJump.ResetLock();
				}
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x0007F96B File Offset: 0x0007DB6B
	public void OnAnimationEnd()
	{
		this.SpriteMirrorLock = false;
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x0007F974 File Offset: 0x0007DB74
	public void OnAnimationStart()
	{
		this.SpriteMirrorLock = true;
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x0007F980 File Offset: 0x0007DB80
	public bool ShouldKeepPlayingWallJumpLeftTowardsAnimation()
	{
		return this.LeftRightMovement.HorizontalInput >= 0f && this.PlatformMovement.IsInAir && !this.PlatformMovement.IsOnCeiling && (!this.PlatformMovement.IsOnWall || !this.PlatformMovement.HeadAndFeetAgainstWall);
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0007F9E8 File Offset: 0x0007DBE8
	public bool ShouldKeepPlayingWallJumpLeftAwayAnimation()
	{
		return this.PlatformMovement.IsInAir && !this.PlatformMovement.IsOnCeiling && (!this.PlatformMovement.IsOnWall || !this.PlatformMovement.HeadAndFeetAgainstWall);
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0007FA3C File Offset: 0x0007DC3C
	public bool ShouldKeepPlayingWallJumpLeftRegularAnimation()
	{
		return this.PlatformMovement.IsInAir && !this.PlatformMovement.IsOnCeiling && (!this.PlatformMovement.IsOnWall || !this.PlatformMovement.HeadAndFeetAgainstWall);
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x0007FA8D File Offset: 0x0007DC8D
	public override void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x0007FAAB File Offset: 0x0007DCAB
	public override void OnDestroy()
	{
		base.OnDestroy();
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0007FACC File Offset: 0x0007DCCC
	public void PerformWallJumpRight()
	{
		if (this.m_hasWallJumpedRight)
		{
			return;
		}
		if (this.DontAllowJumpingTowardsWall && this.LeftRightMovement.BaseHorizontalInput < 0f)
		{
			return;
		}
		if (this.LeftRightMovement.BaseHorizontalInput < 0f && this.DoubleJump)
		{
			this.DoubleJump.LockForDuration(this.LockDoubleJumpTowardsDuration);
		}
		if (this.LimitWallJumping)
		{
			this.m_hasWallJumpedRight = true;
		}
		this.m_hasWallJumpedLeft = false;
		this.PlatformMovement.LocalSpeedX = this.JumpStrength.x;
		this.PlatformMovement.LocalSpeedY = this.JumpStrength.y;
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		this.ApplyImpulseToWall(localSpeed);
		if (this.Sein.Input.NormalizedHorizontal > 0)
		{
			this.CharacterSpriteMirror.FaceLeft = false;
			CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.AwayAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpRightAwayAnimation));
			characterAnimationState.OnStopPlaying = new Action(this.OnAnimationEnd);
			characterAnimationState.OnStartPlaying = new Action(this.OnAnimationStart);
		}
		else if (this.Sein.Input.NormalizedHorizontal < 0)
		{
			Vector3 origin = this.PlatformMovement.Position + Vector3.up * 2f;
			float maxDistance = this.PlatformMovement.CapsuleCollider.radius + 2f;
			Ray ray = new Ray(origin, this.PlatformMovement.LocalToWorld(Vector3.left));
			if (Physics.Raycast(ray, maxDistance))
			{
				CharacterAnimationSystem.CharacterAnimationState characterAnimationState2 = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.TowardsAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpRightTowardsAnimation));
				characterAnimationState2.OnStopPlaying = new Action(this.OnAnimationEnd);
				base.StartCoroutine(this.RoutineForMegWhoPlaysMarioAndSucksAtWallJumping());
			}
			else
			{
				CharacterAnimationSystem.CharacterAnimationState characterAnimationState3 = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.EdgeJumpAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpRightTowardsAnimation));
				characterAnimationState3.OnStopPlaying = new Action(this.OnAnimationEnd);
				localSpeed.y = 0f;
			}
		}
		else
		{
			CharacterAnimationSystem.CharacterAnimationState characterAnimationState4 = this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.RegularAnimation, 10, new Func<bool>(this.ShouldKeepPlayingWallJumpRightRegularAnimation));
			characterAnimationState4.OnStopPlaying = new Action(this.OnAnimationEnd);
			characterAnimationState4.OnStartPlaying = new Action(this.OnAnimationStart);
		}
		Sound.Play(this.WallJumpSound.GetSoundForMaterial(this.Sein.PlatformBehaviour.WallSurfaceMaterialType, null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.OnWallJumpEvent(localSpeed);
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(localSpeed.y, 1f);
		}
		this.Sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = true;
		this.Sein.ResetAirLimits();
		JumpFlipPlatform.OnSeinWallJumpEvent();
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x0007FE34 File Offset: 0x0007E034
	public bool ShouldKeepPlayingWallJumpRightTowardsAnimation()
	{
		return this.LeftRightMovement.HorizontalInput <= 0f && this.PlatformMovement.IsInAir && (!this.PlatformMovement.IsOnWall || !this.PlatformMovement.HeadAndFeetAgainstWall);
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x0007FE8C File Offset: 0x0007E08C
	public bool ShouldKeepPlayingWallJumpRightAwayAnimation()
	{
		return this.PlatformMovement.IsInAir && (!this.PlatformMovement.IsOnWall || !this.PlatformMovement.HeadAndFeetAgainstWall);
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x0007FED0 File Offset: 0x0007E0D0
	public bool ShouldKeepPlayingWallJumpRightRegularAnimation()
	{
		return this.PlatformMovement.IsInAir && (!this.PlatformMovement.IsOnWall || !this.PlatformMovement.HeadAndFeetAgainstWall);
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x0007FF14 File Offset: 0x0007E114
	public void ApplyImpulseToWall(Vector2 speed)
	{
		PlatformMovementListOfColliders platformMovementListOfColliders = this.Sein.PlatformBehaviour.PlatformMovementListOfColliders;
		for (int i = 0; i < platformMovementListOfColliders.WallLeftColliders.Count; i++)
		{
			Collider collider = platformMovementListOfColliders.WallLeftColliders[i];
			if (collider)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (attachedRigidbody)
				{
					Vector3 force = this.PlatformMovement.LocalToWorld(-speed.normalized * this.WallJumpImpulse);
					attachedRigidbody.AddForceAtPosition(force, this.PlatformMovement.Position, ForceMode.Impulse);
				}
			}
		}
		for (int j = 0; j < platformMovementListOfColliders.WallRightColliders.Count; j++)
		{
			Collider collider2 = platformMovementListOfColliders.WallRightColliders[j];
			if (collider2)
			{
				Rigidbody attachedRigidbody2 = collider2.attachedRigidbody;
				if (attachedRigidbody2)
				{
					Vector3 force2 = this.PlatformMovement.LocalToWorld(-speed.normalized * this.WallJumpImpulse);
					attachedRigidbody2.AddForceAtPosition(force2, this.PlatformMovement.Position, ForceMode.Impulse);
				}
			}
		}
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x00080043 File Offset: 0x0007E243
	public override void UpdateCharacterState()
	{
		if (this.PlatformMovement.IsOnGround)
		{
			this.m_hasWallJumpedLeft = false;
			this.m_hasWallJumpedRight = false;
		}
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x00080064 File Offset: 0x0007E264
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_hasWallJumpedLeft);
		ar.Serialize(ref this.m_hasWallJumpedRight);
		ar.Serialize(ref this.m_lockInputTimeRemaining);
		ar.Serialize(ref this.m_spriteMirrorLock);
	}

	// Token: 0x06001D31 RID: 7473 RVA: 0x000800A1 File Offset: 0x0007E2A1
	public void OnRestoreCheckpoint()
	{
		this.m_spriteMirrorLock = false;
	}

	// Token: 0x04001936 RID: 6454
	public TextureAnimationWithTransitions[] AwayAnimation;

	// Token: 0x04001937 RID: 6455
	public bool DontAllowJumpingTowardsWall;

	// Token: 0x04001938 RID: 6456
	public TextureAnimationWithTransitions[] EdgeJumpAnimation;

	// Token: 0x04001939 RID: 6457
	public Vector2 JumpStrength;

	// Token: 0x0400193A RID: 6458
	public bool LimitWallJumping;

	// Token: 0x0400193B RID: 6459
	public float LockDoubleJumpTowardsDuration = 1.5f;

	// Token: 0x0400193C RID: 6460
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet MoveSpeed;

	// Token: 0x0400193D RID: 6461
	public TextureAnimationWithTransitions[] RegularAnimation;

	// Token: 0x0400193E RID: 6462
	public SeinCharacter Sein;

	// Token: 0x0400193F RID: 6463
	public TextureAnimationWithTransitions[] TowardsAnimation;

	// Token: 0x04001940 RID: 6464
	public float WallJumpImpulse = 20f;

	// Token: 0x04001941 RID: 6465
	public SurfaceToSoundProviderMap WallJumpSound;

	// Token: 0x04001942 RID: 6466
	private bool m_hasWallJumpedLeft;

	// Token: 0x04001943 RID: 6467
	private bool m_hasWallJumpedRight;

	// Token: 0x04001944 RID: 6468
	private float m_lockInputTimeRemaining;

	// Token: 0x04001945 RID: 6469
	private bool m_spriteMirrorLock;
}
