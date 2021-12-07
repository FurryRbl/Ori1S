using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class SeinChargeJump : CharacterState, ISeinReceiver
{
	// Token: 0x14000034 RID: 52
	// (add) Token: 0x06001D34 RID: 7476 RVA: 0x0008010B File Offset: 0x0007E30B
	// (remove) Token: 0x06001D35 RID: 7477 RVA: 0x00080124 File Offset: 0x0007E324
	public event Action<float> OnJumpEvent = delegate(float A_0)
	{
	};

	// Token: 0x170004EC RID: 1260
	// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0008013D File Offset: 0x0007E33D
	public PlayerAbilities PlayerAbilities
	{
		get
		{
			return this.Sein.PlayerAbilities;
		}
	}

	// Token: 0x170004ED RID: 1261
	// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0008014A File Offset: 0x0007E34A
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170004EE RID: 1262
	// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0008015C File Offset: 0x0007E35C
	public SeinChargeJump ChargeJump
	{
		get
		{
			return this.Sein.Abilities.ChargeJump;
		}
	}

	// Token: 0x170004EF RID: 1263
	// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0008016E File Offset: 0x0007E36E
	public CharacterUpwardsDeceleration UpwardsDeceleration
	{
		get
		{
			return this.Sein.PlatformBehaviour.UpwardsDeceleration;
		}
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x00080180 File Offset: 0x0007E380
	public void OnDoubleJump()
	{
		this.UpwardsDeceleration.Reset();
		this.ChangeState(SeinChargeJump.State.Normal);
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x00080194 File Offset: 0x0007E394
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		this.UpdateState();
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x000801B0 File Offset: 0x0007E3B0
	public void ChangeState(SeinChargeJump.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		this.m_attackablesIgnore.Clear();
		SeinChargeJump.State currentState = this.CurrentState;
		if (currentState != SeinChargeJump.State.Normal)
		{
			if (currentState != SeinChargeJump.State.Jumping)
			{
			}
		}
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x00080200 File Offset: 0x0007E400
	public void UpdateState()
	{
		SeinChargeJump.State currentState = this.CurrentState;
		if (currentState != SeinChargeJump.State.Normal)
		{
			if (currentState == SeinChargeJump.State.Jumping)
			{
				if (this.m_stateCurrentTime > this.JumpDuration)
				{
					this.ChangeState(SeinChargeJump.State.Normal);
				}
				for (int i = 0; i < Targets.Attackables.Count; i++)
				{
					IAttackable attackable = Targets.Attackables[i];
					if (!InstantiateUtility.IsDestroyed(attackable as Component))
					{
						if (!this.m_attackablesIgnore.Contains(attackable))
						{
							if (attackable.CanBeStomped())
							{
								Vector3 vector = attackable.Position - this.Sein.PlatformBehaviour.PlatformMovement.HeadPosition;
								float magnitude = vector.magnitude;
								if (magnitude < 3f && Vector2.Dot(vector.normalized, this.PlatformMovement.LocalSpeed.normalized) > 0f)
								{
									this.m_attackablesIgnore.Add(attackable);
									Damage damage = new Damage((float)this.Damage, this.PlatformMovement.WorldSpeed.normalized * 3f, this.Sein.Position, DamageType.Stomp, base.gameObject);
									damage.DealToComponents(((Component)attackable).gameObject);
									if (attackable.IsDead() && attackable is IStompAttackable && ((IStompAttackable)attackable).CountsTowardsSuperJumpAchievement())
									{
										AchievementsLogic.Instance.OnSuperJumpedThroughEnemy();
									}
									if (this.ExplosionEffect)
									{
										InstantiateUtility.Instantiate(this.ExplosionEffect, Vector3.Lerp(base.transform.position, attackable.Position, 0.5f), Quaternion.identity);
									}
									break;
								}
							}
						}
					}
				}
			}
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x170004F0 RID: 1264
	// (get) Token: 0x06001D3E RID: 7486 RVA: 0x000803F6 File Offset: 0x0007E5F6
	public bool CanChargeJump
	{
		get
		{
			return this.Sein.Abilities.ChargeJumpCharging.IsCharged && this.PlatformMovement.IsOnGround;
		}
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x00080420 File Offset: 0x0007E620
	public void PerformChargeJump()
	{
		float chargedJumpStrength = this.ChargedJumpStrength;
		this.PlatformMovement.LocalSpeedY = chargedJumpStrength;
		this.OnJumpEvent(chargedJumpStrength);
		Sound.Play(this.JumpSound.GetSound(null), this.Sein.PlatformBehaviour.PlatformMovement.Position, null);
		this.UpwardsDeceleration.Deceleration = this.Deceleration;
		this.Sein.Mortality.DamageReciever.MakeInvincibleToEnemies(this.JumpDuration);
		this.ChangeState(SeinChargeJump.State.Jumping);
		this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.JumpAnimation, 10, new Func<bool>(this.ShouldChargeJumpAnimationKeepPlaying));
		this.Sein.PlatformBehaviour.Visuals.SpriteRotater.BeginTiltLeftRightInAir(1.5f);
		if (this.Sein.PlatformBehaviour.JumpSustain)
		{
			this.Sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(this.PlatformMovement.LocalSpeedY, 1f);
		}
		this.Sein.Abilities.ChargeJumpCharging.EndCharge();
		JumpFlipPlatform.OnSeinChargeJumpEvent();
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x00080554 File Offset: 0x0007E754
	public bool ShouldChargeJumpAnimationKeepPlaying()
	{
		return this.PlatformMovement.IsInAir && !this.PlatformMovement.IsOnWall && !this.PlatformMovement.IsOnCeiling;
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00080592 File Offset: 0x0007E792
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.ChargeJump = this;
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x000805AC File Offset: 0x0007E7AC
	public override void Serialize(Archive ar)
	{
		base.Serialize(ar);
		ar.Serialize(ref this.m_superJumpedEnemies);
	}

	// Token: 0x04001948 RID: 6472
	public SeinCharacter Sein;

	// Token: 0x04001949 RID: 6473
	public TextureAnimationWithTransitions JumpAnimation;

	// Token: 0x0400194A RID: 6474
	public SoundProvider JumpSound;

	// Token: 0x0400194B RID: 6475
	public float JumpDuration = 0.5f;

	// Token: 0x0400194C RID: 6476
	public SeinChargeJump.State CurrentState;

	// Token: 0x0400194D RID: 6477
	private float m_stateCurrentTime;

	// Token: 0x0400194E RID: 6478
	private HashSet<IAttackable> m_attackablesIgnore = new HashSet<IAttackable>();

	// Token: 0x0400194F RID: 6479
	public GameObject ExplosionEffect;

	// Token: 0x04001950 RID: 6480
	public int Damage = 50;

	// Token: 0x04001951 RID: 6481
	public float ChargingTime;

	// Token: 0x04001952 RID: 6482
	public float ChargedJumpStrength;

	// Token: 0x04001953 RID: 6483
	public float Deceleration = 20f;

	// Token: 0x04001954 RID: 6484
	private int m_superJumpedEnemies;

	// Token: 0x02000433 RID: 1075
	public enum State
	{
		// Token: 0x040019CF RID: 6607
		Normal,
		// Token: 0x040019D0 RID: 6608
		Jumping
	}
}
