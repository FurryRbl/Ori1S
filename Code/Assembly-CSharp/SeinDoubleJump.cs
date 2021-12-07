using System;
using Core;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class SeinDoubleJump : CharacterState, ISeinReceiver
{
	// Token: 0x0600136C RID: 4972 RVA: 0x00059FF4 File Offset: 0x000581F4
	// Note: this type is marked as 'beforefieldinit'.
	static SeinDoubleJump()
	{
		SeinDoubleJump.OnDoubleJumpEvent = delegate(float A_0)
		{
		};
	}

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x0600136D RID: 4973 RVA: 0x0005A023 File Offset: 0x00058223
	// (remove) Token: 0x0600136E RID: 4974 RVA: 0x0005A03A File Offset: 0x0005823A
	public static event Action<float> OnDoubleJumpEvent;

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x0600136F RID: 4975 RVA: 0x0005A051 File Offset: 0x00058251
	public int ExtraJumpsAvailable
	{
		get
		{
			return (!CheatsHandler.InfiniteDoubleJumps) ? ((!this.Sein.PlayerAbilities.DoubleJumpUpgrade.HasAbility) ? 1 : 2) : 999999;
		}
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x06001370 RID: 4976 RVA: 0x0005A088 File Offset: 0x00058288
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x06001371 RID: 4977 RVA: 0x0005A09A File Offset: 0x0005829A
	public SeinJump Jump
	{
		get
		{
			return this.Sein.Abilities.Jump;
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x06001372 RID: 4978 RVA: 0x0005A0AC File Offset: 0x000582AC
	public bool CanDoubleJump
	{
		get
		{
			return base.enabled && !this.PlatformMovement.IsOnGround && this.m_numberOfJumpsAvailable != 0 && this.m_remainingLockTime <= 0f && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities);
		}
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x0005A0FB File Offset: 0x000582FB
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.DoubleJump = this;
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x0005A118 File Offset: 0x00058318
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_doubleJumpTime);
		ar.Serialize(ref this.m_numberOfJumpsAvailable);
		ar.Serialize(ref this.m_remainingLockTime);
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x0005A14C File Offset: 0x0005834C
	public void PerformDoubleJump()
	{
		if (this.Sein.Abilities.ChargeJump)
		{
			this.Sein.Abilities.ChargeJump.OnDoubleJump();
		}
		this.PlatformMovement.LocalSpeedY = this.JumpStrength;
		this.m_numberOfJumpsAvailable--;
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayRandom(this.DoubleJumpAnimation, 10, new Func<bool>(this.ShouldDoubleJumpAnimationKeepPlaying));
		this.m_doubleJumpSound = Sound.Play(this.DoubleJumpSound.GetSound(null), this.Sein.PlatformBehaviour.PlatformMovement.Position, delegate()
		{
			this.m_doubleJumpSound = null;
		});
		SeinDoubleJump.OnDoubleJumpEvent(this.JumpStrength);
		GameObject original = this.DoubleJumpAfterShock;
		if (this.m_numberOfJumpsAvailable == 0 && this.ExtraJumpsAvailable == 2)
		{
			original = this.TrippleJumpAfterShock;
		}
		Vector2 worldSpeed = this.PlatformMovement.WorldSpeed;
		float num = Mathf.Atan2(worldSpeed.x, worldSpeed.y) * 57.29578f;
		InstantiateUtility.Instantiate(original, this.Sein.Position, Quaternion.Euler(0f, 0f, -num));
		JumpFlipPlatform.OnSeinDoubleJumpEvent();
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x0005A298 File Offset: 0x00058498
	public bool ShouldDoubleJumpAnimationKeepPlaying()
	{
		return this.PlatformMovement.IsInAir && !this.PlatformMovement.IsOnCeiling;
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x0005A2C8 File Offset: 0x000584C8
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		if (this.PlatformMovement.IsOnGround && this.m_numberOfJumpsAvailable != this.ExtraJumpsAvailable)
		{
			this.ResetDoubleJump();
		}
		if (this.m_doubleJumpSound && (this.PlatformMovement.IsOnWall || this.PlatformMovement.IsOnCeiling))
		{
			this.m_doubleJumpSound.FadeOut(0.5f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_doubleJumpSound.gameObject);
			this.m_doubleJumpSound = null;
		}
		if (this.m_remainingLockTime > 0f)
		{
			this.m_remainingLockTime -= Time.deltaTime;
		}
		if (this.m_doubleJumpTime > 0f)
		{
			if (this.PlatformMovement.LocalSpeedY <= 0f)
			{
				this.m_doubleJumpTime = 0f;
			}
			this.m_doubleJumpTime -= Time.deltaTime;
		}
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x0005A3CE File Offset: 0x000585CE
	public void ResetDoubleJump()
	{
		this.m_numberOfJumpsAvailable = this.ExtraJumpsAvailable;
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0005A3DC File Offset: 0x000585DC
	public void LockForDuration(float duration)
	{
		this.m_remainingLockTime = Mathf.Max(this.m_remainingLockTime, duration);
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x0005A3F0 File Offset: 0x000585F0
	public void ResetLock()
	{
		this.m_remainingLockTime = 0f;
	}

	// Token: 0x0400114D RID: 4429
	public TextureAnimationWithTransitions[] DoubleJumpAnimation;

	// Token: 0x0400114E RID: 4430
	public GameObject DoubleJumpAfterShock;

	// Token: 0x0400114F RID: 4431
	public GameObject TrippleJumpAfterShock;

	// Token: 0x04001150 RID: 4432
	public SoundProvider DoubleJumpSound;

	// Token: 0x04001151 RID: 4433
	public float JumpStrength;

	// Token: 0x04001152 RID: 4434
	public SeinCharacter Sein;

	// Token: 0x04001153 RID: 4435
	private SoundPlayer m_doubleJumpSound;

	// Token: 0x04001154 RID: 4436
	private float m_doubleJumpTime;

	// Token: 0x04001155 RID: 4437
	private int m_numberOfJumpsAvailable;

	// Token: 0x04001156 RID: 4438
	private float m_remainingLockTime;
}
