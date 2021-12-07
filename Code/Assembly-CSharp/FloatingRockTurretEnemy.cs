using System;
using fsm;
using fsm.triggers;
using Game;

// Token: 0x020005A5 RID: 1445
public class FloatingRockTurretEnemy : Enemy
{
	// Token: 0x060024F3 RID: 9459 RVA: 0x000A11C8 File Offset: 0x0009F3C8
	public void PlayAnimationOnce(CharacterAnimationSystem animationSystem, TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && animationSystem)
		{
			animationSystem.Play(anim, layer, null);
		}
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x000A11F8 File Offset: 0x0009F3F8
	public void RestartAnimationLoop(CharacterAnimationSystem animationSystem, TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && animationSystem)
		{
			animationSystem.RestartLoop(anim, layer, null);
		}
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x000A1225 File Offset: 0x0009F425
	public void PlayAnimationLoop(CharacterAnimationSystem animationSystem, TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && animationSystem)
		{
			animationSystem.PlayLoop(anim, layer, null, false);
		}
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x000A1248 File Offset: 0x0009F448
	public void OnEnterIdle()
	{
		this.RestartAnimationLoop(this.Animation, this.Animations.Idle, 0);
		this.RestartAnimationLoop(this.AnimationB, this.AnimationsB.Idle, 0);
		this.RestartAnimationLoop(this.AnimationC, this.AnimationsC.Idle, 0);
		this.SpriteRotation.RotateBackToNormal();
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x000A12A8 File Offset: 0x0009F4A8
	public void OnEnterCharge()
	{
		this.RestartAnimationLoop(this.Animation, this.Animations.Charging, 0);
		this.RestartAnimationLoop(this.AnimationB, this.AnimationsB.Charging, 0);
		this.RestartAnimationLoop(this.AnimationC, this.AnimationsC.Charging, 0);
		base.PlaySound(this.FormingSound);
		base.SpawnPrefab(this.ChargingEffect);
		this.Effects.BeginCharge();
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x000A1320 File Offset: 0x0009F520
	public void OnExitCharge()
	{
		this.Effects.StopCharge();
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x000A1330 File Offset: 0x0009F530
	public void UpdateCharge()
	{
		this.SpriteRotation.RotateTowardsTarget(base.PlayerPosition - base.Position, 135f);
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x000A1360 File Offset: 0x0009F560
	public void OnEnterShooting()
	{
		this.Effects.OnShoot();
		this.PlayAnimationOnce(this.Animation, this.Animations.Shooting, 0);
		this.PlayAnimationOnce(this.AnimationB, this.AnimationsB.Shooting, 0);
		this.PlayAnimationOnce(this.AnimationC, this.AnimationsC.Shooting, 0);
		base.PlaySound(this.ShootingSound);
		base.PlaySound(this.DeformingSound);
		base.SpawnPrefab(this.ShootingEffect);
		this.ProjectileSpawner.AimAt(Characters.Sein.Controller.Transform);
		Projectile projectile = this.ProjectileSpawner.SpawnProjectile();
		projectile.GetComponent<DamageDealer>().Damage = this.Settings.ProjectileDamage;
		this.Movement.ApplyImpulseForce(this.Settings.ShootingForce * this.ProjectileSpawner.Direction * -1f);
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x000A1450 File Offset: 0x0009F650
	private void OnEnterRespawn()
	{
		this.PlayAnimationOnce(this.Animation, this.Animations.Respawn, 0);
		this.PlayAnimationOnce(this.AnimationB, this.AnimationsB.Respawn, 0);
		this.PlayAnimationOnce(this.AnimationC, this.AnimationsC.Respawn, 0);
		base.FacePlayer();
		base.SpawnPrefab(this.Settings.RespawnEffect);
		if (this.RespawnAnimator)
		{
			this.RespawnAnimator.Initialize();
			this.RespawnAnimator.AnimatorDriver.Restart();
		}
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x000A14E8 File Offset: 0x0009F6E8
	public new void Start()
	{
		base.Start();
		this.State.Respawn = new State();
		this.State.Idle = new State();
		this.State.Charge = new State();
		this.State.Shooting = new State();
		State idle = this.State.Idle;
		idle.OnEnterEvent = (Action)Delegate.Combine(idle.OnEnterEvent, new Action(this.OnEnterIdle));
		State charge = this.State.Charge;
		charge.OnEnterEvent = (Action)Delegate.Combine(charge.OnEnterEvent, new Action(this.OnEnterCharge));
		State charge2 = this.State.Charge;
		charge2.OnExitEvent = (Action)Delegate.Combine(charge2.OnExitEvent, new Action(this.OnExitCharge));
		State charge3 = this.State.Charge;
		charge3.UpdateStateEvent = (Action)Delegate.Combine(charge3.UpdateStateEvent, new Action(this.UpdateCharge));
		State shooting = this.State.Shooting;
		shooting.OnEnterEvent = (Action)Delegate.Combine(shooting.OnEnterEvent, new Action(this.OnEnterShooting));
		this.State.Respawn.OnEnterEvent = new Action(this.OnEnterRespawn);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Charge,
			this.State.Shooting,
			this.State.Respawn
		});
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Charge, new Func<bool>(this.ShouldCharge), null);
		this.Controller.StateMachine.Configure(this.State.Charge).AddTransition<OnFixedUpdate>(this.State.Shooting, () => base.AfterTime(this.Settings.ChargeDuration), null).AddTransition<OnReceiveDamage>(this.State.Idle, null, null).AddTransition<OnFixedUpdate>(this.State.Idle, new Func<bool>(this.ShouldDisolve), null);
		this.Controller.StateMachine.Configure(this.State.Shooting).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.ShootingDuration), null).AddTransition<OnReceiveDamage>(this.State.Idle, null, null);
		this.Controller.StateMachine.Configure(this.State.Respawn).AddTransition<OnAnimationEnded>(this.State.Idle, null, null);
		if (this.m_timedRespawn)
		{
			this.Controller.StateMachine.ChangeState(this.State.Respawn);
			this.m_timedRespawn = false;
		}
		else
		{
			this.Controller.StateMachine.ChangeState(this.State.Idle);
		}
		this.ProjectileSpawner.Projectile = this.Settings.Projectile;
		this.ProjectileSpawner.Speed = this.Settings.ProjectileSpeed;
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x000A1814 File Offset: 0x0009FA14
	public new void OnTimedRespawn()
	{
		this.m_timedRespawn = true;
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x000A181D File Offset: 0x0009FA1D
	public bool ShouldCharge()
	{
		return this.Controller.NearSein && !this.ShouldDisolve();
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x000A183C File Offset: 0x0009FA3C
	public bool ShouldDisolve()
	{
		return base.PositionToPlayerPosition.magnitude < this.Settings.DisolveDistance;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x000A1864 File Offset: 0x0009FA64
	public new void Awake()
	{
		base.Awake();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x000A18A0 File Offset: 0x0009FAA0
	public new void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Remove(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x000A18DB File Offset: 0x0009FADB
	public virtual void OnModifyDamage(Damage damage)
	{
		if (damage.Type == DamageType.SpiritFlame)
		{
			damage.SetAmount(0f);
		}
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x000A18F8 File Offset: 0x0009FAF8
	public new void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.Movement.ApplySpringForce(this.Settings.SpringForce, base.StartPosition);
		this.Movement.ApplyDrag(this.Settings.Drag);
	}

	// Token: 0x04001F64 RID: 8036
	public FloatingRockTurretEnemy.States State = new FloatingRockTurretEnemy.States();

	// Token: 0x04001F65 RID: 8037
	public FloatingRockTurrentEnemySettings Settings;

	// Token: 0x04001F66 RID: 8038
	public FloatingRockTurretEnemyAnimations Animations;

	// Token: 0x04001F67 RID: 8039
	public FloatingRockTurretEnemyAnimations AnimationsB;

	// Token: 0x04001F68 RID: 8040
	public FloatingRockTurretEnemyAnimations AnimationsC;

	// Token: 0x04001F69 RID: 8041
	public CharacterAnimationSystem AnimationB;

	// Token: 0x04001F6A RID: 8042
	public CharacterAnimationSystem AnimationC;

	// Token: 0x04001F6B RID: 8043
	public PrefabSpawner ChargingEffect;

	// Token: 0x04001F6C RID: 8044
	public PrefabSpawner ShootingEffect;

	// Token: 0x04001F6D RID: 8045
	public SoundProvider FormingSound;

	// Token: 0x04001F6E RID: 8046
	public SoundProvider ShootingSound;

	// Token: 0x04001F6F RID: 8047
	public SoundProvider DeformingSound;

	// Token: 0x04001F70 RID: 8048
	public ProjectileSpawner ProjectileSpawner;

	// Token: 0x04001F71 RID: 8049
	public RigidbodyMovement Movement;

	// Token: 0x04001F72 RID: 8050
	public SpriteRotationController SpriteRotation;

	// Token: 0x04001F73 RID: 8051
	public FloatingRockTurretEnemyEffects Effects;

	// Token: 0x04001F74 RID: 8052
	public BaseAnimator RespawnAnimator;

	// Token: 0x04001F75 RID: 8053
	private bool m_timedRespawn;

	// Token: 0x020005A6 RID: 1446
	public class States
	{
		// Token: 0x04001F76 RID: 8054
		public State Respawn;

		// Token: 0x04001F77 RID: 8055
		public State Idle;

		// Token: 0x04001F78 RID: 8056
		public State Charge;

		// Token: 0x04001F79 RID: 8057
		public State Laser;

		// Token: 0x04001F7A RID: 8058
		public State Shooting;
	}
}
