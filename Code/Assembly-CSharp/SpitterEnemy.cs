using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x020005DC RID: 1500
public class SpitterEnemy : GroundEnemy
{
	// Token: 0x17000615 RID: 1557
	// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000A5840 File Offset: 0x000A3A40
	// (set) Token: 0x060025C8 RID: 9672 RVA: 0x000A5848 File Offset: 0x000A3A48
	public Vector2 ThrownDirection { get; set; }

	// Token: 0x060025C9 RID: 9673 RVA: 0x000A5851 File Offset: 0x000A3A51
	public override void Awake()
	{
		base.Awake();
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x000A585C File Offset: 0x000A3A5C
	public override bool CanBeOptimized()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		return currentState == this.State.Idle || currentState == this.State.Walk;
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x000A589C File Offset: 0x000A3A9C
	public bool WilhelmScreamZoneRectanglesContain(Vector2 position)
	{
		for (int i = 0; i < this.ActionZones.Length; i++)
		{
			Transform transform = this.ActionZones[i];
			Rect rect = default(Rect);
			rect.width = transform.lossyScale.x;
			rect.height = transform.lossyScale.y;
			rect.center = transform.position;
			if (rect.Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x000A5920 File Offset: 0x000A3B20
	public new void Start()
	{
		base.Start();
		this.State.Idle = new SpitterEnemyIdleState(this);
		this.State.Walk = new SpitterEnemyWalkState(this);
		this.State.RunBack = new SpitterEnemyRunBackState(this);
		this.State.SpitterEnemyCharging = new SpitterEnemyChargingState(this);
		this.State.Shooting = new SpitterEnemyShootingState(this);
		this.State.Thrown = new SpitterEnemyThrownState(this);
		this.State.Stomped = new SpitterEnemyStompedState(this);
		this.State.Stunned = new SpitterEnemyStunnedState(this);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Walk,
			this.State.RunBack,
			this.State.SpitterEnemyCharging,
			this.State.Shooting,
			this.State.Stunned,
			this.State.Thrown,
			this.State.Stomped
		});
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<AttackTriggered>(this.State.SpitterEnemyCharging, null, null).AddTransition<OnFixedUpdate>(this.State.Walk, () => base.AfterTime(this.Settings.IdleDuration) && base.IsOnScreen(), null).AddTransition<OnFixedUpdate>(this.State.RunBack, new Func<bool>(this.CanSeePlayer), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.State.Stomped.OnStomped)).AddTransition<OnReceiveDamage>(this.State.SpitterEnemyCharging, null, null);
		this.Controller.StateMachine.Configure(this.State.Walk).AddTransition<AttackTriggered>(this.State.SpitterEnemyCharging, null, null).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.WalkDuration) && base.IsOnScreen(), null).AddTransition<OnFixedUpdate>(this.State.Idle, () => !base.IsOnScreen(), null).AddTransition<OnFixedUpdate>(this.State.Idle, new Func<bool>(this.HasHitWall), new Action(this.TurnAround)).AddTransition<OnFixedUpdate>(this.State.RunBack, new Func<bool>(this.CanSeePlayer), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.State.Stomped.OnStomped)).AddTransition<OnReceiveDamage>(this.State.SpitterEnemyCharging, null, null);
		this.Controller.StateMachine.Configure(this.State.RunBack).AddTransition<AttackTriggered>(this.State.SpitterEnemyCharging, null, null).AddTransition<OnFixedUpdate>(this.State.Idle, () => !this.CanSeePlayer(), null).AddTransition<OnFixedUpdate>(this.State.SpitterEnemyCharging, new Func<bool>(this.FurtherThanMinChargeDistance), null).AddTransition<OnFixedUpdate>(this.State.SpitterEnemyCharging, new Func<bool>(this.HasHitWall), new Action(this.TurnAround)).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.State.Stomped.OnStomped)).AddTransition<OnReceiveDamage>(this.State.SpitterEnemyCharging, null, null);
		this.Controller.StateMachine.Configure(this.State.SpitterEnemyCharging).AddTransition<OnFixedUpdate>(this.State.Shooting, () => base.AfterTime(this.Settings.ChargeDuration), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.State.Stomped.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Shooting).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.ShootingDuration) && !this.CanSeePlayer(), null).AddTransition<OnFixedUpdate>(this.State.RunBack, () => base.AfterTime(this.Settings.ShootingDuration) && this.CloserThanMinChargeDistance(), null).AddTransition<OnFixedUpdate>(this.State.SpitterEnemyCharging, () => base.AfterTime(this.Settings.ShootingDuration) && this.FurtherThanMinChargeDistance(), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.State.Stomped.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Thrown).AddTransition<OnFixedUpdate>(this.State.Stunned, new Func<bool>(this.IsOnGround), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow));
		this.Controller.StateMachine.Configure(this.State.Stomped).AddTransition<OnFixedUpdate>(this.State.Stunned, new Func<bool>(this.IsOnGround), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.State.Stomped.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Stunned).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.StunnedDuration) && !this.CanSeePlayer(), null).AddTransition<OnFixedUpdate>(this.State.RunBack, () => base.AfterTime(this.Settings.StunnedDuration) && this.CanSeePlayer(), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.State.Thrown.OnThrow));
		this.Controller.StateMachine.ChangeState(this.State.Idle);
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x000A6067 File Offset: 0x000A4267
	public bool IsOnGround()
	{
		return this.PlatformMovement.IsOnGround;
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x000A6074 File Offset: 0x000A4274
	public bool HasHitWall()
	{
		return this.PlatformMovement.IsOnWall;
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x000A6081 File Offset: 0x000A4281
	public void TurnAround()
	{
		base.FaceLeft = !base.FaceLeft;
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x000A6094 File Offset: 0x000A4294
	public bool CanSeePlayer()
	{
		return this.Controller.NearSein && base.PositionToPlayerPosition.magnitude < this.Settings.SeePlayerDistance;
	}

	// Token: 0x060025D1 RID: 9681 RVA: 0x000A60D0 File Offset: 0x000A42D0
	public bool FurtherThanMinChargeDistance()
	{
		return base.PositionToPlayerPosition.magnitude > this.Settings.MinChargeDistance;
	}

	// Token: 0x060025D2 RID: 9682 RVA: 0x000A60F8 File Offset: 0x000A42F8
	public bool CloserThanMinChargeDistance()
	{
		return base.PositionToPlayerPosition.magnitude < this.Settings.MinChargeDistance;
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x000A6120 File Offset: 0x000A4320
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		bool flag;
		if (this.PlatformMovement.MovingHorizontally && EnemyStopper.InsideEnemyStopper(base.Position, (!base.FaceLeft) ? Vector3.right : Vector3.left, out flag))
		{
			base.FaceLeft = !base.FaceLeft;
			if (this.Controller.StateMachine.CurrentState == this.State.RunBack)
			{
				this.Controller.StateMachine.ChangeState(this.State.SpitterEnemyCharging);
			}
		}
		if (!this.PlatformMovement.IsSuspended && this.PlatformMovement.IsInAir)
		{
			this.PlatformMovement.LocalSpeedY -= this.Settings.Gravity * Time.deltaTime;
		}
		this.UpdateRotation();
		if (base.IsInWater)
		{
			base.Drown();
		}
		if (this.WilhelmScreamZoneRectanglesContain(base.transform.position) && !this.m_hasEnteredZone && this.EnterZoneAction)
		{
			this.m_hasEnteredZone = true;
			this.EnterZoneAction.Perform(null);
		}
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x000A6268 File Offset: 0x000A4468
	public void UpdateRotation()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		float currentStateTime = this.Controller.StateMachine.CurrentStateTime;
		if (currentState == this.State.Thrown)
		{
			float num = 1f - Mathf.InverseLerp(0.3f, 0.6f, currentStateTime);
			this.FeetTransform.eulerAngles = new Vector3(0f, 0f, (MoonMath.Angle.AngleFromVector(this.ThrownDirection) - 90f) * num);
		}
		else
		{
			float b = (!this.PlatformMovement.IsOnGround) ? 0f : this.PlatformMovement.GroundAngle;
			this.FeetTransform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(this.FeetTransform.eulerAngles.z, b, 0.2f));
		}
	}

	// Token: 0x060025D5 RID: 9685 RVA: 0x000A6350 File Offset: 0x000A4550
	public bool ShouldThrow()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.Bash;
	}

	// Token: 0x060025D6 RID: 9686 RVA: 0x000A6384 File Offset: 0x000A4584
	public bool ShouldStomped()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.StompBlast;
	}

	// Token: 0x0400205A RID: 8282
	public PrefabSpawner SpitEffect;

	// Token: 0x0400205B RID: 8283
	public PrefabSpawner ProjectileSpawner;

	// Token: 0x0400205C RID: 8284
	public ChargingSootEnemyAnimations Animations;

	// Token: 0x0400205D RID: 8285
	public ChargingSootEnemySettings Settings;

	// Token: 0x0400205E RID: 8286
	public SoundSource IdleSound;

	// Token: 0x0400205F RID: 8287
	public SoundSource WalkSound;

	// Token: 0x04002060 RID: 8288
	public SoundSource RunAwaySound;

	// Token: 0x04002061 RID: 8289
	public SoundSource AttackSound;

	// Token: 0x04002062 RID: 8290
	public SoundSource LandSound;

	// Token: 0x04002063 RID: 8291
	public ActionMethod EnterZoneAction;

	// Token: 0x04002064 RID: 8292
	public Transform[] ActionZones;

	// Token: 0x04002065 RID: 8293
	private bool m_hasEnteredZone;

	// Token: 0x04002066 RID: 8294
	public SpitterEnemy.States State = new SpitterEnemy.States();

	// Token: 0x020005DD RID: 1501
	public class States
	{
		// Token: 0x04002068 RID: 8296
		public SpitterEnemyIdleState Idle;

		// Token: 0x04002069 RID: 8297
		public SpitterEnemyWalkState Walk;

		// Token: 0x0400206A RID: 8298
		public SpitterEnemyRunBackState RunBack;

		// Token: 0x0400206B RID: 8299
		public SpitterEnemyChargingState SpitterEnemyCharging;

		// Token: 0x0400206C RID: 8300
		public SpitterEnemyShootingState Shooting;

		// Token: 0x0400206D RID: 8301
		public SpitterEnemyThrownState Thrown;

		// Token: 0x0400206E RID: 8302
		public SpitterEnemyStompedState Stomped;

		// Token: 0x0400206F RID: 8303
		public SpitterEnemyStunnedState Stunned;
	}
}
