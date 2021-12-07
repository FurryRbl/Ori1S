using System;
using System.Collections.Generic;
using Core;
using fsm;
using Game;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class SeinStomp : CharacterState, ISeinReceiver
{
	// Token: 0x06000824 RID: 2084 RVA: 0x00022FD0 File Offset: 0x000211D0
	// Note: this type is marked as 'beforefieldinit'.
	static SeinStomp()
	{
		SeinStomp.OnStompIdleEvent = delegate()
		{
		};
		SeinStomp.OnStompLandEvent = delegate()
		{
		};
		SeinStomp.OnStompDownEvent = delegate()
		{
		};
	}

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06000825 RID: 2085 RVA: 0x00023043 File Offset: 0x00021243
	// (remove) Token: 0x06000826 RID: 2086 RVA: 0x0002305A File Offset: 0x0002125A
	public static event Action OnStompIdleEvent;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06000827 RID: 2087 RVA: 0x00023071 File Offset: 0x00021271
	// (remove) Token: 0x06000828 RID: 2088 RVA: 0x00023088 File Offset: 0x00021288
	public static event Action OnStompLandEvent;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06000829 RID: 2089 RVA: 0x0002309F File Offset: 0x0002129F
	// (remove) Token: 0x0600082A RID: 2090 RVA: 0x000230B6 File Offset: 0x000212B6
	public static event Action OnStompDownEvent;

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x0600082B RID: 2091 RVA: 0x000230CD File Offset: 0x000212CD
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x0600082C RID: 2092 RVA: 0x000230DF File Offset: 0x000212DF
	public SeinDoubleJump DoubleJump
	{
		get
		{
			return this.Sein.Abilities.DoubleJump;
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x0600082D RID: 2093 RVA: 0x000230F1 File Offset: 0x000212F1
	public CharacterUpwardsDeceleration UpwardsDeceleration
	{
		get
		{
			return this.Sein.PlatformBehaviour.UpwardsDeceleration;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x0600082E RID: 2094 RVA: 0x00023103 File Offset: 0x00021303
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x0600082F RID: 2095 RVA: 0x00023115 File Offset: 0x00021315
	public bool Finished
	{
		get
		{
			return this.Logic.CurrentState == this.State.Inactive;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000830 RID: 2096 RVA: 0x0002312F File Offset: 0x0002132F
	public bool IsStomping
	{
		get
		{
			return this.Logic.CurrentState != this.State.Inactive;
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0002314C File Offset: 0x0002134C
	public void OnRestoreCheckpoint()
	{
		this.Logic.ChangeState(this.State.Inactive);
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00023164 File Offset: 0x00021364
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Stomp = this;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0002317E File Offset: 0x0002137E
	public void Start()
	{
		this.Sein.PlatformBehaviour.Gravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyVerticalPlatformMovementSettings;
		this.PlatformMovement.OnCollisionGroundEvent += this.OnCollisionGround;
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x000231B8 File Offset: 0x000213B8
	public override void OnExit()
	{
		this.Logic.ChangeState(this.State.Inactive);
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x000231D0 File Offset: 0x000213D0
	public override void UpdateCharacterState()
	{
		this.Logic.UpdateState(Time.deltaTime);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x000231E4 File Offset: 0x000213E4
	public void ModifyVerticalPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (this.Logic.CurrentState != this.State.Inactive)
		{
			settings.GravityStrength = 0f;
		}
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000837 RID: 2103 RVA: 0x00023217 File Offset: 0x00021417
	public float StompDamage
	{
		get
		{
			if (this.Sein.PlayerAbilities.StompUpgrade.HasAbility)
			{
				return this.UpgradedDamage;
			}
			return this.Damage;
		}
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00023240 File Offset: 0x00021440
	public void OnCollisionGround(Vector3 normal, Collider collider)
	{
		if (this.Logic.CurrentState == this.State.StompDown)
		{
			this.LandStomp();
			if (!this.Sein.Controller.IsSwimming)
			{
				IAttackable attackable = collider.gameObject.FindComponent<IAttackable>();
				if (attackable != null && attackable.CanBeStomped())
				{
					Damage damage = new Damage(this.StompDamage, Vector3.down * 3f, Characters.Sein.Position, DamageType.Stomp, base.gameObject);
					damage.DealToComponents(collider.gameObject);
				}
				this.DoBlastRadius(attackable);
			}
			this.Logic.ChangeState(this.State.StompFinished);
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x000232FC File Offset: 0x000214FC
	public void DoBlastRadius(IAttackable landedStompAttackable)
	{
		this.m_stompBlastAttackables.Clear();
		this.m_stompBlastAttackables.AddRange(Targets.Attackables);
		for (int i = 0; i < this.m_stompBlastAttackables.Count; i++)
		{
			IAttackable attackable = this.m_stompBlastAttackables[i];
			if (!InstantiateUtility.IsDestroyed(attackable as Component))
			{
				if (attackable != landedStompAttackable)
				{
					if (attackable.CanBeStomped())
					{
						Vector3 vector = attackable.Position - this.Sein.Position;
						float magnitude = vector.magnitude;
						if (magnitude < this.StompBlashRadius)
						{
							Vector3 normalized = (vector.normalized + Vector3.up * 2f).normalized;
							GameObject gameObject = ((Component)attackable).gameObject;
							float stompDamage = this.StompDamage;
							Damage damage = new Damage(stompDamage, normalized * 3f, attackable.Position, DamageType.StompBlast, gameObject);
							damage.DealToComponents(gameObject);
						}
					}
				}
			}
		}
		this.m_stompBlastAttackables.Clear();
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00023418 File Offset: 0x00021618
	public override void Awake()
	{
		this.State.Inactive = new State
		{
			OnEnterEvent = new Action(this.OnEnterInactive),
			UpdateStateEvent = new Action(this.UpdateStompInactiveState)
		};
		this.State.StompDown = new State
		{
			OnEnterEvent = new Action(this.OnEnterStompDownState),
			UpdateStateEvent = new Action(this.UpdateStompDownState)
		};
		this.State.StompIdle = new State
		{
			OnEnterEvent = new Action(this.OnEnterStompIdleState),
			UpdateStateEvent = new Action(this.UpdateStompIdleState)
		};
		this.State.StompFinished = new State
		{
			UpdateStateEvent = new Action(this.UpdateStompFinishedState)
		};
		this.Logic.RegisterStates(new IState[]
		{
			this.State.Inactive,
			this.State.StompDown,
			this.State.StompIdle
		});
		this.Logic.ChangeState(this.State.Inactive);
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00023554 File Offset: 0x00021754
	public override void OnDestroy()
	{
		this.Sein.PlatformBehaviour.Gravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyVerticalPlatformMovementSettings;
		this.PlatformMovement.OnCollisionGroundEvent -= this.OnCollisionGround;
		this.Logic.ChangeState(this.State.Inactive);
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		base.OnDestroy();
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x000235CC File Offset: 0x000217CC
	public void OnEnterStompIdleState()
	{
		SeinStomp.OnStompIdleEvent();
		if (!this.Sein.PlayerAbilities.StompUpgrade.HasAbility)
		{
			this.StompStartSound.Play();
		}
		else
		{
			this.StompStartSoundUpgraded.Play();
		}
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.StompIdleAnimation, 111, new Func<bool>(this.ShouldStompAnimationKeepPlaying), false);
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00023648 File Offset: 0x00021848
	public void OnEnterStompDownState()
	{
		SeinStomp.OnStompDownEvent();
		this.PlatformMovement.LocalSpeedX *= 0.5f;
		if (!this.Sein.PlayerAbilities.StompUpgrade.HasAbility)
		{
			this.StompFallSound.Play();
		}
		else
		{
			this.StompFallSoundUpgraded.Play();
		}
		this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.StompDownAnimation, 111, new Func<bool>(this.ShouldStompAnimationKeepPlaying), false);
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x000236DB File Offset: 0x000218DB
	public void UpdateStompFinishedState()
	{
		if (this.Logic.CurrentStateTime > 0.05f)
		{
			this.EndStomp();
		}
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x000236F8 File Offset: 0x000218F8
	public void LandStomp()
	{
		this.PlatformMovement.LocalSpeedX = 0f;
		this.PlatformMovement.LocalSpeedY = 0f;
		SeinStomp.OnStompLandEvent();
		if (!this.Sein.PlayerAbilities.StompUpgrade.HasAbility)
		{
			this.StompLandSound.Play();
		}
		else
		{
			this.StompLandSoundUpgraded.Play();
		}
		this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.StompLandAnimation, 111, new Func<bool>(this.ShouldStompLandAnimationKeepPlaying));
		if (this.Sein.Controller.IsSwimming)
		{
			return;
		}
		this.EndStomp();
		this.DoStompBlastEffect();
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x000237B8 File Offset: 0x000219B8
	public void DoStompBlastEffect()
	{
		if (this.StompLandEffect != null)
		{
			if (this.Sein.PlayerAbilities.StompUpgrade.HasAbility)
			{
				InstantiateUtility.Instantiate(this.StompLandEffectUpgraded, this.Sein.PlatformBehaviour.PlatformMovement.FeetPosition, Quaternion.identity);
			}
			else
			{
				InstantiateUtility.Instantiate(this.StompLandEffect, this.Sein.PlatformBehaviour.PlatformMovement.FeetPosition, Quaternion.identity);
			}
		}
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00023841 File Offset: 0x00021A41
	public void OnEnterInactive()
	{
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00023844 File Offset: 0x00021A44
	public void UpdateStompIdleState()
	{
		if (this.DoubleJump && this.DoubleJump.CanDoubleJump && Core.Input.Jump.OnPressed)
		{
			this.EndStomp();
			this.DoubleJump.PerformDoubleJump();
			return;
		}
		if (this.Logic.CurrentStateTime > this.IdleDuration)
		{
			this.Logic.ChangeState(this.State.StompDown);
		}
		this.PlatformMovement.LocalSpeedX = 0f;
		this.PlatformMovement.LocalSpeedY = 0f;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x000238E0 File Offset: 0x00021AE0
	public void UpdateStompInactiveState()
	{
		if (this.Sein.Controller.IsSwimming)
		{
			return;
		}
		bool flag = this.Sein.Input.Down.OnPressed & this.Sein.Input.NormalizedHorizontal == 0;
		bool flag2 = this.Sein.Input.Down.OnPressed && Core.Input.DigiPadAxis.y < 0f;
		bool flag3 = flag || flag2;
		if (flag3 && !this.Sein.Input.Down.Used && this.CanStomp())
		{
			this.Logic.ChangeState(this.State.StompIdle);
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x000239AC File Offset: 0x00021BAC
	public bool CanStomp()
	{
		return this.Sein.Controller.CanMove && this.Sein.PlatformBehaviour.PlatformMovement.IsInAir && !this.Sein.Controller.IsGliding && !this.Sein.Controller.InputLocked && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities);
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00023A20 File Offset: 0x00021C20
	public void UpdateStompDownState()
	{
		if (Core.Input.Jump.OnPressed)
		{
			this.EndStomp();
			return;
		}
		if (this.Logic.CurrentStateTime > this.StompDownDuration && !Core.Input.Down.Pressed)
		{
			this.EndStomp();
			return;
		}
		this.PlatformMovement.LocalSpeed = new Vector2(0f, -this.StompSpeed);
		this.Sein.Mortality.DamageReciever.MakeInvincibleToEnemies(0.2f);
		if (this.Sein.Controller.IsSwimming)
		{
			this.EndStomp();
		}
		for (int i = 0; i < Targets.Attackables.Count; i++)
		{
			IAttackable attackable = Targets.Attackables[i];
			if (!attackable.IsDead())
			{
				if (!InstantiateUtility.IsDestroyed(attackable as Component))
				{
					if (attackable.IsStompBouncable())
					{
						Vector3 a = Characters.Sein.Position + Vector3.down;
						if (Vector3.Distance(a, attackable.Position) < 1.5f && this.Logic.CurrentState == this.State.StompDown)
						{
							GameObject gameObject = ((Component)attackable).gameObject;
							Damage damage = new Damage(this.StompDamage, Vector3.down * 3f, Characters.Sein.Position, DamageType.Stomp, base.gameObject);
							damage.DealToComponents(gameObject);
							if (attackable.IsDead())
							{
								return;
							}
							this.EndStomp();
							this.PlatformMovement.LocalSpeedY = 17f;
							this.Sein.PlatformBehaviour.UpwardsDeceleration.Deceleration = 20f;
							this.Sein.Animation.Play(this.StompBounceAnimation, 111, null);
							this.Sein.ResetAirLimits();
							this.StompLandSound.Play();
							this.DoBlastRadius(attackable);
							this.DoStompBlastEffect();
							return;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00023C23 File Offset: 0x00021E23
	public void EndStomp()
	{
		this.Logic.ChangeState(this.State.Inactive);
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00023C3B File Offset: 0x00021E3B
	public bool ShouldStompAnimationKeepPlaying()
	{
		return this.Logic.CurrentState != this.State.Inactive;
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00023C58 File Offset: 0x00021E58
	public bool ShouldStompLandAnimationKeepPlaying()
	{
		return this.PlatformMovement.LocalSpeedX == 0f && this.PlatformMovement.LocalSpeedY <= 0f;
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00023C92 File Offset: 0x00021E92
	public override void Serialize(Archive ar)
	{
		this.Logic.Serialize(ar);
		base.Serialize(ar);
	}

	// Token: 0x04000682 RID: 1666
	public float IdleDuration;

	// Token: 0x04000683 RID: 1667
	public StateMachine Logic = new StateMachine();

	// Token: 0x04000684 RID: 1668
	public SeinCharacter Sein;

	// Token: 0x04000685 RID: 1669
	public SeinStomp.States State = new SeinStomp.States();

	// Token: 0x04000686 RID: 1670
	public float StompBlashRadius = 10f;

	// Token: 0x04000687 RID: 1671
	public float Damage = 15f;

	// Token: 0x04000688 RID: 1672
	public float UpgradedDamage = 25f;

	// Token: 0x04000689 RID: 1673
	public AnimationCurve StompBlastFalloutCurve;

	// Token: 0x0400068A RID: 1674
	public TextureAnimationWithTransitions StompBounceAnimation;

	// Token: 0x0400068B RID: 1675
	public TextureAnimationWithTransitions StompDownAnimation;

	// Token: 0x0400068C RID: 1676
	public float StompDownDuration;

	// Token: 0x0400068D RID: 1677
	public SoundSource StompFallSound;

	// Token: 0x0400068E RID: 1678
	public SoundSource StompFallSoundUpgraded;

	// Token: 0x0400068F RID: 1679
	public TextureAnimationWithTransitions StompIdleAnimation;

	// Token: 0x04000690 RID: 1680
	public TextureAnimationWithTransitions StompLandAnimation;

	// Token: 0x04000691 RID: 1681
	public float StompLandDuration;

	// Token: 0x04000692 RID: 1682
	public GameObject StompLandEffect;

	// Token: 0x04000693 RID: 1683
	public GameObject StompLandEffectUpgraded;

	// Token: 0x04000694 RID: 1684
	public SoundSource StompLandSound;

	// Token: 0x04000695 RID: 1685
	public SoundSource StompLandSoundUpgraded;

	// Token: 0x04000696 RID: 1686
	public float StompSpeed;

	// Token: 0x04000697 RID: 1687
	public SoundSource StompStartSound;

	// Token: 0x04000698 RID: 1688
	public SoundSource StompStartSoundUpgraded;

	// Token: 0x04000699 RID: 1689
	public float UpwardDeceleration;

	// Token: 0x0400069A RID: 1690
	private List<IAttackable> m_stompBlastAttackables = new List<IAttackable>();

	// Token: 0x0200043A RID: 1082
	public class States
	{
		// Token: 0x040019F9 RID: 6649
		public IState Inactive;

		// Token: 0x040019FA RID: 6650
		public IState StompDown;

		// Token: 0x040019FB RID: 6651
		public IState StompIdle;

		// Token: 0x040019FC RID: 6652
		public IState StompFinished;
	}
}
