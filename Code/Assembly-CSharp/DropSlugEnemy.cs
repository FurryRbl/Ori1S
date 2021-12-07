using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x02000513 RID: 1299
public class DropSlugEnemy : Enemy
{
	// Token: 0x060022B1 RID: 8881 RVA: 0x00097ECC File Offset: 0x000960CC
	public override bool CanBeOptimized()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		return currentState == this.State.Idle;
	}

	// Token: 0x060022B2 RID: 8882 RVA: 0x00097EF8 File Offset: 0x000960F8
	public new void OnTimedRespawn()
	{
		this.m_timedRespawn = true;
	}

	// Token: 0x060022B3 RID: 8883 RVA: 0x00097F04 File Offset: 0x00096104
	public new void Start()
	{
		base.Start();
		this.State.Idle = new DropSlugIdleState(this);
		this.State.Alert = new DropSlugAlertState(this);
		this.State.Fall = new DropSlugFallState(this);
		this.State.Land = new DropSlugLandState(this);
		this.State.Thrown = new DropSlugThrownState(this);
		this.State.Respawn = new State();
		this.State.Respawn.OnEnterEvent = delegate()
		{
			base.PlayAnimationOnce(this.Animations.Respawn, 0);
			base.FacePlayer();
			base.SpawnPrefab(this.Settings.RespawnEffect);
		};
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Alert,
			this.State.Fall,
			this.State.Land,
			this.State.Respawn
		});
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Alert, new Func<bool>(this.ShouldAlert), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Fall, null, null);
		this.Controller.StateMachine.Configure(this.State.Alert).AddTransition<OnFixedUpdate>(this.State.Idle, new Func<bool>(this.ShouldNotAlert), null).AddTransition<OnFixedUpdate>(this.State.Fall, new Func<bool>(this.ShouldFall), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Fall, null, null);
		this.Controller.StateMachine.Configure(this.State.Fall).AddTransition<OnCollisionEnter>(this.State.Land, new Func<bool>(this.HasHitGround), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow2), new Action(this.OnThrow));
		this.Controller.StateMachine.Configure(this.State.Thrown).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow2), new Action(this.OnThrow)).AddTransition<OnCollisionEnter>(this.State.Land, null, null);
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
	}

	// Token: 0x060022B4 RID: 8884 RVA: 0x0009822C File Offset: 0x0009642C
	public bool ShouldThrow()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.Bash;
	}

	// Token: 0x060022B5 RID: 8885 RVA: 0x00098260 File Offset: 0x00096460
	public bool ShouldThrow2()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.Bash || onReceiveDamage.Damage.Type == DamageType.StompBlast;
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x000982A8 File Offset: 0x000964A8
	public void OnThrow()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		this.FlyMovement.Velocity = onReceiveDamage.Damage.Force * 10f;
	}

	// Token: 0x060022B7 RID: 8887 RVA: 0x000982EC File Offset: 0x000964EC
	public bool ShouldFall()
	{
		return Math.Abs(base.PositionToPlayerPosition.x) < this.Settings.FallRange && base.PositionToPlayerPosition.y < -this.Settings.BelowOffset && this.Controller.NearSein;
	}

	// Token: 0x060022B8 RID: 8888 RVA: 0x0009834C File Offset: 0x0009654C
	public bool ShouldAlert()
	{
		return Math.Abs(base.PositionToPlayerPosition.x) < this.Settings.AlertRange && this.Controller.NearSein;
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x0009838A File Offset: 0x0009658A
	public bool ShouldNotAlert()
	{
		return !this.ShouldAlert();
	}

	// Token: 0x060022BA RID: 8890 RVA: 0x00098398 File Offset: 0x00096598
	public bool HasHitGround()
	{
		OnCollisionEnter onCollisionEnter = (OnCollisionEnter)this.Controller.StateMachine.CurrentTrigger;
		Vector3 rhs = PhysicsHelper.CalculateAverageNormalFromContactPoints(onCollisionEnter.Collision.contacts);
		return Vector3.Dot(Vector3.up, rhs) >= Mathf.Cos(1.2217305f);
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x000983E9 File Offset: 0x000965E9
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

	// Token: 0x04001D20 RID: 7456
	public FlyMovement FlyMovement;

	// Token: 0x04001D21 RID: 7457
	public SpriteRotationController SpriteRotation;

	// Token: 0x04001D22 RID: 7458
	public PrefabSpawner Explosion;

	// Token: 0x04001D23 RID: 7459
	public DropSlugEnemyAnimations Animations;

	// Token: 0x04001D24 RID: 7460
	public DropSlugEnemySettings Settings;

	// Token: 0x04001D25 RID: 7461
	public SoundSource Idle;

	// Token: 0x04001D26 RID: 7462
	public SoundSource Alert;

	// Token: 0x04001D27 RID: 7463
	public SoundSource DropDown;

	// Token: 0x04001D28 RID: 7464
	public DropSlugEnemy.States State = new DropSlugEnemy.States();

	// Token: 0x04001D29 RID: 7465
	private bool m_timedRespawn;

	// Token: 0x02000515 RID: 1301
	public class States
	{
		// Token: 0x04001D2F RID: 7471
		public State Respawn;

		// Token: 0x04001D30 RID: 7472
		public DropSlugIdleState Idle;

		// Token: 0x04001D31 RID: 7473
		public DropSlugAlertState Alert;

		// Token: 0x04001D32 RID: 7474
		public DropSlugFallState Fall;

		// Token: 0x04001D33 RID: 7475
		public DropSlugLandState Land;

		// Token: 0x04001D34 RID: 7476
		public DropSlugThrownState Thrown;
	}
}
