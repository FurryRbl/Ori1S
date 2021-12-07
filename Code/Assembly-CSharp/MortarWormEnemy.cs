using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x020005FA RID: 1530
public class MortarWormEnemy : WormEnemy
{
	// Token: 0x06002653 RID: 9811 RVA: 0x000A804C File Offset: 0x000A624C
	public override bool CanBeOptimized()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		return currentState == this.State.Hidden;
	}

	// Token: 0x06002654 RID: 9812 RVA: 0x000A8078 File Offset: 0x000A6278
	public bool IgnoreDamage(Damage damage)
	{
		return this.Controller.StateMachine.CurrentState == this.State.Hidden || (damage != null && (damage.Type == DamageType.Lava || damage.Type == DamageType.Spikes));
	}

	// Token: 0x06002655 RID: 9813 RVA: 0x000A80C8 File Offset: 0x000A62C8
	public bool ShouldHide()
	{
		return !this.Controller.NearSein || base.PositionToPlayerPosition.magnitude < this.Settings.HideDistance || !base.IsFacingPlayer || this.IsBlocked;
	}

	// Token: 0x06002656 RID: 9814 RVA: 0x000A8117 File Offset: 0x000A6317
	public bool ShouldEmerge()
	{
		return !this.ShouldHide() && this.m_trajectoryHelper.HitTarget && base.AfterTime(1f);
	}

	// Token: 0x06002657 RID: 9815 RVA: 0x000A8144 File Offset: 0x000A6344
	public bool ShouldCharge()
	{
		return this.ProjectileSpawner.TimeSinceLastShot >= this.Settings.WaitBetweenShots && base.IsFacingPlayer && this.Controller.IsNearSein() && this.m_trajectoryHelper.HitTarget;
	}

	// Token: 0x06002658 RID: 9816 RVA: 0x000A8195 File Offset: 0x000A6395
	public void PrintDebugText(string param)
	{
	}

	// Token: 0x1700061C RID: 1564
	// (get) Token: 0x06002659 RID: 9817 RVA: 0x000A8197 File Offset: 0x000A6397
	public bool IsHidden
	{
		get
		{
			return this.Controller.StateMachine.CurrentState == this.State.Hidden;
		}
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x000A81B8 File Offset: 0x000A63B8
	public new void Start()
	{
		base.Start();
		this.State.Charging = new MortarWormChargingState(this, this.Animations.Charging, this.ChargingEffect, this.ChargingSound);
		this.State.Emerging = new WormEmergingState(this, this.Animations.Emerging, this.EmergingEffect, this.EmergingSound);
		this.State.Hidden = new WormHiddenState(this, this.Animations.Hidden);
		this.State.Hiding = new WormHidingState(this, this.Animations.Hiding, this.HidingEffect, this.HidingSound);
		this.State.Idle = new MortarWormIdleState(this, this.Animations.Idle);
		this.State.Shooting = new WormMortarShootingState(this, this.Animations.Shooting, this.ShootingEffect, this.ShootingSound, this.ProjectileSpawner, this.Settings.ShootDelay, this.Settings.ProjectileDamage);
		this.State.Frozen = new State();
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Charging, new Func<bool>(this.ShouldCharge), null).AddTransition<OnFixedUpdate>(this.State.Hiding, () => base.AfterTime(0.4f), null);
		this.Controller.StateMachine.Configure(this.State.Charging).AddTransition<OnFixedUpdate>(this.State.Shooting, () => base.AfterTime(this.Settings.ChargingDuration), null).AddTransition<OnFixedUpdate>(this.State.Hiding, new Func<bool>(this.ShouldHide), null);
		this.Controller.StateMachine.Configure(this.State.Shooting).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.ShootingDuration), null);
		this.Controller.StateMachine.Configure(this.State.Hidden).AddTransition<OnFixedUpdate>(this.State.Emerging, () => base.AfterTime(this.Settings.MinHideTime) && this.ShouldEmerge(), null);
		this.Controller.StateMachine.Configure(this.State.Hiding).AddTransition<OnAnimationEnded>(this.State.Hidden, null, delegate()
		{
			MortarWormEnemy.OnMortarHide(this);
		});
		this.Controller.StateMachine.Configure(this.State.Emerging).AddTransition<OnAnimationEnded>(this.State.Idle, null, null);
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.IgnoreDamageCondition = (Func<Damage, bool>)Delegate.Combine(damageReciever.IgnoreDamageCondition, new Func<Damage, bool>(this.IgnoreDamage));
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Charging,
			this.State.Shooting,
			this.State.Hidden,
			this.State.Hiding,
			this.State.Emerging
		});
		this.Controller.StateMachine.ChangeState(this.State.Hidden);
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x000A8500 File Offset: 0x000A6700
	public void ForceEmerge()
	{
		this.Controller.StateMachine.ChangeState(this.State.Emerging);
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x000A851D File Offset: 0x000A671D
	public new void Awake()
	{
		this.m_trajectoryHelper = new MortarTrajectoryHelper(this);
		base.Awake();
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x000A8531 File Offset: 0x000A6731
	public override void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.IgnoreDamageCondition = (Func<Damage, bool>)Delegate.Remove(damageReciever.IgnoreDamageCondition, new Func<Damage, bool>(this.IgnoreDamage));
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x000A8560 File Offset: 0x000A6760
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		if (this.Settings.CanTurnAround && (this.Controller.StateMachine.CurrentState == this.State.Idle || this.Controller.StateMachine.CurrentState == this.State.Hidden))
		{
			base.FacePlayer();
		}
		if (this.Controller.NearSein && this.Controller.StateMachine.CurrentState != this.State.Shooting)
		{
			this.UpdateMortarTrajectoryPeriodically();
		}
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x000A860C File Offset: 0x000A680C
	public void UpdateMortarTrajectoryPeriodically()
	{
		this.m_trajectoryHelper.RemainingWaitTime -= Time.deltaTime;
		if (this.m_trajectoryHelper.RemainingWaitTime < 0f)
		{
			this.m_trajectoryHelper.RemainingWaitTime = 0.1f;
			this.m_trajectoryHelper.UpdateMortarTrajectory();
		}
	}

	// Token: 0x040020E8 RID: 8424
	public MortarWormSettings Settings;

	// Token: 0x040020E9 RID: 8425
	public MortarWormEnemyAnimations Animations;

	// Token: 0x040020EA RID: 8426
	public PrefabSpawner ChargingEffect;

	// Token: 0x040020EB RID: 8427
	public PrefabSpawner EmergingEffect;

	// Token: 0x040020EC RID: 8428
	public PrefabSpawner HidingEffect;

	// Token: 0x040020ED RID: 8429
	public PrefabSpawner ShootingEffect;

	// Token: 0x040020EE RID: 8430
	public SoundSource ChargingSound;

	// Token: 0x040020EF RID: 8431
	public SoundSource EmergingSound;

	// Token: 0x040020F0 RID: 8432
	public SoundSource HidingSound;

	// Token: 0x040020F1 RID: 8433
	public SoundSource ShootingSound;

	// Token: 0x040020F2 RID: 8434
	public MortarWormEnemyProjectileSpawnerTransform Spawn;

	// Token: 0x040020F3 RID: 8435
	public WormHole WormHole;

	// Token: 0x040020F4 RID: 8436
	public bool IsBlocked;

	// Token: 0x040020F5 RID: 8437
	public Transform ProjectileTrajectorySpawnPoint;

	// Token: 0x040020F6 RID: 8438
	public GameObject HideGroup;

	// Token: 0x040020F7 RID: 8439
	public static Action<MortarWormEnemy> OnMortarHide = delegate(MortarWormEnemy A_0)
	{
	};

	// Token: 0x040020F8 RID: 8440
	public MortarWormEnemy.States State = new MortarWormEnemy.States();

	// Token: 0x040020F9 RID: 8441
	public ProjectileSpawner ProjectileSpawner;

	// Token: 0x040020FA RID: 8442
	public LayerMask RayTestLayerMask;

	// Token: 0x040020FB RID: 8443
	public Vector3 LocalShootDirection;

	// Token: 0x040020FC RID: 8444
	private MortarTrajectoryHelper m_trajectoryHelper;

	// Token: 0x020005FF RID: 1535
	public class States
	{
		// Token: 0x04002112 RID: 8466
		public IState Idle;

		// Token: 0x04002113 RID: 8467
		public IState Charging;

		// Token: 0x04002114 RID: 8468
		public IState Shooting;

		// Token: 0x04002115 RID: 8469
		public IState Hidden;

		// Token: 0x04002116 RID: 8470
		public IState Hiding;

		// Token: 0x04002117 RID: 8471
		public IState Emerging;

		// Token: 0x04002118 RID: 8472
		public IState Frozen;
	}
}
