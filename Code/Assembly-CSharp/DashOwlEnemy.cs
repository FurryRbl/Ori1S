using System;
using fsm;
using fsm.triggers;

// Token: 0x020004F3 RID: 1267
public class DashOwlEnemy : OwlEnemy
{
	// Token: 0x06002223 RID: 8739 RVA: 0x00095E38 File Offset: 0x00094038
	public override bool CanBeOptimized()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		return currentState == this.State.Idle;
	}

	// Token: 0x06002224 RID: 8740 RVA: 0x00095E64 File Offset: 0x00094064
	public new void Awake()
	{
		base.Awake();
		this.State.Idle = new DashOwlIdleState(this);
		this.State.Bashed = new DashOwlBashedState(this);
		this.State.Bounce = new DashOwlBounceState(this);
		this.State.Dash = new DashOwlDashState(this);
		this.State.DashAlert = new DashOwlDashAlertState(this);
		this.State.FlyHome = new DashOwlFlyHomeState(this);
		this.State.Hurt = new DashOwlHurtState(this);
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.DashAlert, new Func<bool>(this.PlayerInDashRange), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.DamageTypeIsBash), null).AddTransition<OnReceiveDamage>(this.State.Hurt, null, null);
		this.Controller.StateMachine.Configure(this.State.Bashed).AddTransition<OnCollisionEnter>(this.State.Bounce, () => true, new Action(this.OnBashBounce)).AddTransition<OnAnimationEnded>(this.State.DashAlert, new Func<bool>(this.PlayerInDashRange), null).AddTransition<OnAnimationEnded>(this.State.FlyHome, null, null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.DamageTypeIsBash), null).AddTransition<OnReceiveDamage>(this.State.Hurt, new Func<bool>(this.DamageTypeIsStompBlast), null);
		this.Controller.StateMachine.Configure(this.State.Bounce).AddTransition<OnAnimationEnded>(this.State.DashAlert, new Func<bool>(this.PlayerInDashRange), null).AddTransition<OnAnimationEnded>(this.State.FlyHome, null, null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.DamageTypeIsBash), null).AddTransition<OnReceiveDamage>(this.State.Hurt, new Func<bool>(this.DamageTypeIsStompBlast), null);
		this.Controller.StateMachine.Configure(this.State.Dash).AddTransition<OnCollisionEnter>(this.State.Bounce, null, null).AddTransition<OnAnimationEnded>(this.State.DashAlert, new Func<bool>(this.PlayerInDashRange), null).AddTransition<OnAnimationEnded>(this.State.FlyHome, null, null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.DamageTypeIsBash), null).AddTransition<OnReceiveDamage>(this.State.Hurt, new Func<bool>(this.DamageTypeIsStompBlast), null);
		this.Controller.StateMachine.Configure(this.State.DashAlert).AddTransition<OnAnimationEnded>(this.State.Dash, null, null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.DamageTypeIsBash), null).AddTransition<OnReceiveDamage>(this.State.Hurt, new Func<bool>(this.DamageTypeIsStompBlast), null);
		this.Controller.StateMachine.Configure(this.State.FlyHome).AddTransition<OnFixedUpdate>(this.State.DashAlert, new Func<bool>(this.PlayerInDashRange), null).AddTransition<OnFixedUpdate>(this.State.Idle, new Func<bool>(this.State.FlyHome.IsHome), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.DamageTypeIsBash), null).AddTransition<OnReceiveDamage>(this.State.Hurt, null, null);
		this.Controller.StateMachine.Configure(this.State.Hurt).AddTransition<OnAnimationEnded>(this.State.DashAlert, null, null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.DamageTypeIsBash), null).AddTransition<OnReceiveDamage>(this.State.Hurt, new Func<bool>(this.DamageTypeIsStompBlast), null);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.FlyHome,
			this.State.Bashed,
			this.State.Bounce,
			this.State.Dash,
			this.State.DashAlert,
			this.State.Hurt
		});
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x00096307 File Offset: 0x00094507
	public new void Start()
	{
		base.Start();
		this.Controller.StateMachine.ChangeState(this.State.Idle);
	}

	// Token: 0x06002226 RID: 8742 RVA: 0x0009632A File Offset: 0x0009452A
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		if (base.IsInWater)
		{
			base.Drown();
		}
	}

	// Token: 0x06002227 RID: 8743 RVA: 0x0009634F File Offset: 0x0009454F
	public void OnBashBounce()
	{
	}

	// Token: 0x06002228 RID: 8744 RVA: 0x00096351 File Offset: 0x00094551
	public bool CurrentStateTimeGreaterThan(float duration)
	{
		return this.Controller.StateMachine.CurrentStateTime > duration;
	}

	// Token: 0x06002229 RID: 8745 RVA: 0x00096366 File Offset: 0x00094566
	public bool NearPlayer()
	{
		return this.Controller.NearSein;
	}

	// Token: 0x0600222A RID: 8746 RVA: 0x00096373 File Offset: 0x00094573
	public bool PlayerInAggressiveRange()
	{
		return this.NearPlayer();
	}

	// Token: 0x0600222B RID: 8747 RVA: 0x0009637B File Offset: 0x0009457B
	public bool PlayerOutsideAggressiveRange()
	{
		return !this.NearPlayer();
	}

	// Token: 0x0600222C RID: 8748 RVA: 0x00096388 File Offset: 0x00094588
	public bool PlayerInDashRange()
	{
		return base.IsOnScreen() && this.IsWithinDistanceFromStartPosition() && this.NearPlayer() && base.PositionToPlayerPosition.magnitude < this.Settings.DashRange;
	}

	// Token: 0x0600222D RID: 8749 RVA: 0x000963D4 File Offset: 0x000945D4
	public bool IsWithinDistanceFromStartPosition()
	{
		return base.StartPositionToPlayerPosition.magnitude < this.Settings.MaxDistanceFromStartPosition;
	}

	// Token: 0x0600222E RID: 8750 RVA: 0x000963FC File Offset: 0x000945FC
	public bool DamageTypeIsBash()
	{
		return ((OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger).Damage.Type == DamageType.Bash;
	}

	// Token: 0x0600222F RID: 8751 RVA: 0x00096421 File Offset: 0x00094621
	public bool DamageTypeIsStompBlast()
	{
		return ((OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger).Damage.Type == DamageType.StompBlast;
	}

	// Token: 0x04001CAE RID: 7342
	public SoundSource DashSound;

	// Token: 0x04001CAF RID: 7343
	public SoundSource DashAlertSound;

	// Token: 0x04001CB0 RID: 7344
	public Varying2DSoundProvider HitWallSound;

	// Token: 0x04001CB1 RID: 7345
	public DashOwlEnemyAnimations Animations;

	// Token: 0x04001CB2 RID: 7346
	public DashOwlEnemySettings Settings;

	// Token: 0x04001CB3 RID: 7347
	public DashOwlEnemy.States State = new DashOwlEnemy.States();

	// Token: 0x02000501 RID: 1281
	public class States
	{
		// Token: 0x04001CE0 RID: 7392
		public DashOwlIdleState Idle;

		// Token: 0x04001CE1 RID: 7393
		public DashOwlFlyHomeState FlyHome;

		// Token: 0x04001CE2 RID: 7394
		public DashOwlBashedState Bashed;

		// Token: 0x04001CE3 RID: 7395
		public DashOwlBounceState Bounce;

		// Token: 0x04001CE4 RID: 7396
		public DashOwlDashState Dash;

		// Token: 0x04001CE5 RID: 7397
		public DashOwlDashAlertState DashAlert;

		// Token: 0x04001CE6 RID: 7398
		public DashOwlHurtState Hurt;
	}
}
