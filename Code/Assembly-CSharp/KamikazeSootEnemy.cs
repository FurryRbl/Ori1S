using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x02000547 RID: 1351
public class KamikazeSootEnemy : GroundEnemy
{
	// Token: 0x0600235A RID: 9050 RVA: 0x0009A750 File Offset: 0x00098950
	public override bool CanBeOptimized()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		return currentState == this.State.Idle;
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x0009A77C File Offset: 0x0009897C
	public void ForceAttackPlayer()
	{
		this.Controller.StateMachine.ChangeState(this.State.Alert);
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x0009A799 File Offset: 0x00098999
	public bool DamageTypeIsBash()
	{
		return ((OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger).Damage.Type == DamageType.Bash;
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x0009A7C0 File Offset: 0x000989C0
	public new void Start()
	{
		base.Start();
		this.State.Idle = new KamikazeSootEnemyIdleState(this);
		this.State.Drop = new KamikazeSootEnemyDropState(this);
		this.State.Alert = new KamikazeSootEnemyAlertState(this);
		this.State.Rolling = new KamikazeSootEnemyRollingState(this);
		this.State.Respawn = new State();
		this.State.Respawn.OnEnterEvent = delegate()
		{
			base.PlayAnimationOnce(this.Animations.Respawn, 0);
			base.FacePlayer();
			base.SpawnPrefab(this.Settings.RespawnEffect);
		};
		this.Controller.StateMachine.Configure(this.State.Drop).AddTransition<OnFixedUpdate>(this.State.Idle, () => this.PlatformMovement.IsOnGround, null).AddTransition<OnReceiveDamage>(this.State.Rolling, new Func<bool>(this.DamageTypeIsBash), null);
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Alert, new Func<bool>(this.InRange), null).AddTransition<OnReceiveDamage>(this.State.Rolling, new Func<bool>(this.DamageTypeIsBash), null);
		this.Controller.StateMachine.Configure(this.State.Alert).AddTransition<OnFixedUpdate>(this.State.Rolling, () => base.AfterTime(this.Settings.AlertDuration), delegate()
		{
			if (this.StartRollingSound)
			{
				this.StartRollingSound.Play();
			}
		}).AddTransition<OnReceiveDamage>(this.State.Rolling, new Func<bool>(this.DamageTypeIsBash), null);
		this.Controller.StateMachine.Configure(this.State.Rolling);
		this.Controller.StateMachine.Configure(this.State.Respawn).AddTransition<OnAnimationEnded>(this.State.Idle, null, null).AddTransition<OnReceiveDamage>(this.State.Rolling, new Func<bool>(this.DamageTypeIsBash), null);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Alert,
			this.State.Rolling,
			this.State.Drop,
			this.State.Respawn
		});
		if (this.m_timedRespawn)
		{
			this.Controller.StateMachine.ChangeState(this.State.Respawn);
			this.m_timedRespawn = false;
		}
		else
		{
			this.Controller.StateMachine.ChangeState(this.State.Drop);
		}
		EntityDamageDealer damageDealer = this.DamageDealer;
		damageDealer.OnDamageDealtEvent = (Action<GameObject, Damage>)Delegate.Combine(damageDealer.OnDamageDealtEvent, new Action<GameObject, Damage>(this.OnDamageDealt));
		this.RollingMovement.OnCollisionWallLeftEvent += this.OnWallCollision;
		this.RollingMovement.OnCollisionWallRightEvent += this.OnWallCollision;
		this.RollingMovement.OnCollisionGroundEvent += this.OnGroundCollision;
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x0009AAF4 File Offset: 0x00098CF4
	public void OnModifyDamage(Damage damage)
	{
		if (damage.Type == DamageType.SpiritFlame || damage.Type == DamageType.SpiritFlameSplatter || damage.Type == DamageType.Bash || damage.Type == DamageType.ChargeFlame || damage.Type == DamageType.Grenade || damage.Type == DamageType.Stomp || damage.Type == DamageType.StompBlast || damage.Type == DamageType.Ice)
		{
			damage.SetAmount(0f);
		}
		if (damage.Type == DamageType.Bash)
		{
			this.RollingMovement.Speed = damage.Force * 10f;
			base.PlayAnimationOnce(this.Animations.Idle, 0);
		}
		else
		{
			this.RollingMovement.Speed += damage.Force * 10f;
		}
		if (damage.Amount > 0f)
		{
			this.SelfDestruct();
		}
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x0009ABF8 File Offset: 0x00098DF8
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		bool flag;
		if (EnemyStopper.InsideEnemyStopper(base.Position, (!base.FaceLeft) ? Vector3.right : Vector3.left, out flag))
		{
			base.FaceLeft = !base.FaceLeft;
		}
		this.UpdateRotation();
		if (base.IsInWater)
		{
			this.SelfDestruct();
		}
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x0009AC69 File Offset: 0x00098E69
	public new void OnTimedRespawn()
	{
		this.m_timedRespawn = true;
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x0009AC74 File Offset: 0x00098E74
	public void UpdateRotation()
	{
		float b = (!this.PlatformMovement.IsOnGround) ? 0f : this.PlatformMovement.GroundAngle;
		this.FeetTransform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(this.FeetTransform.eulerAngles.z, b, 0.2f));
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x0009ACE0 File Offset: 0x00098EE0
	public void AccelerateForwards(float acceleration, float maxSpeed)
	{
		this.PlatformMovement.LocalSpeedX = MoonMath.Movement.AccelerateSpeed(this.PlatformMovement.LocalSpeedX, acceleration, maxSpeed, base.FaceLeft);
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x0009AD10 File Offset: 0x00098F10
	public void ApplyGravity(float gravity, float maxFallSpeed)
	{
		this.PlatformMovement.LocalSpeedY = MoonMath.Movement.ApplyGravity(this.PlatformMovement.LocalSpeedY, gravity, maxFallSpeed);
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x0009AD2F File Offset: 0x00098F2F
	public void Decelerate(float deceleration)
	{
		this.PlatformMovement.LocalSpeedX = MoonMath.Movement.DecelerateSpeed(this.PlatformMovement.LocalSpeedX, deceleration);
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x0009AD4D File Offset: 0x00098F4D
	public void OnWallCollision(Vector3 normal, float strength, Collider collider)
	{
		this.SelfDestruct();
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x0009AD55 File Offset: 0x00098F55
	public void OnGroundCollision(Vector3 normal, float strength, Collider collider)
	{
		if (strength > 10f)
		{
			base.PlaySound(this.HitGroundSound);
		}
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x0009AD70 File Offset: 0x00098F70
	public void OnDamageDealt(GameObject target, Damage damage)
	{
		if (damage.Amount == 0f)
		{
			return;
		}
		if (target.GetComponent<SpiritGrenadeDamageDealer>())
		{
			return;
		}
		this.SelfDestruct();
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x0009ADA8 File Offset: 0x00098FA8
	public void SelfDestruct()
	{
		if (this.m_isSelfDestructing)
		{
			return;
		}
		this.m_isSelfDestructing = true;
		Damage damage = new Damage(9999f, Vector2.zero, base.transform.position, DamageType.Explosion, base.gameObject);
		damage.DealToComponents(this.DamageReciever.gameObject);
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.KamikazeExplosion, base.transform.position, Quaternion.identity);
		gameObject.GetComponentInChildren<DamageDealer>().Damage = (float)this.Settings.ExplosionDamage;
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x0009AE34 File Offset: 0x00099034
	public bool InRange()
	{
		return this.Controller.NearSein && base.PositionToPlayerPosition.magnitude < this.Settings.InRange && EnemyZone.InSameZone(base.Position, base.PlayerPosition);
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x0009AE90 File Offset: 0x00099090
	public bool OutOfRange()
	{
		return (base.PositionToPlayerPosition.magnitude >= 1f && !this.Controller.NearSein) || base.PositionToPlayerPosition.magnitude > this.Settings.OutRange;
	}

	// Token: 0x04001DB1 RID: 7601
	public KamikazeSootEnemyAnimations Animations;

	// Token: 0x04001DB2 RID: 7602
	public KamikazeSootEnemySettings Settings;

	// Token: 0x04001DB3 RID: 7603
	public KamikazeSootEnemy.States State = new KamikazeSootEnemy.States();

	// Token: 0x04001DB4 RID: 7604
	public RollingMovement RollingMovement;

	// Token: 0x04001DB5 RID: 7605
	public SoundSource IdleSound;

	// Token: 0x04001DB6 RID: 7606
	public SoundSource AlertSound;

	// Token: 0x04001DB7 RID: 7607
	public SoundSource RollingSound;

	// Token: 0x04001DB8 RID: 7608
	public SoundSource StartRollingSound;

	// Token: 0x04001DB9 RID: 7609
	public SoundSource HitGroundSound;

	// Token: 0x04001DBA RID: 7610
	public GameObject KamikazeExplosion;

	// Token: 0x04001DBB RID: 7611
	private bool m_timedRespawn;

	// Token: 0x04001DBC RID: 7612
	private bool m_isSelfDestructing;

	// Token: 0x0200057F RID: 1407
	public class States
	{
		// Token: 0x04001E94 RID: 7828
		public State Respawn;

		// Token: 0x04001E95 RID: 7829
		public KamikazeSootEnemyDropState Drop;

		// Token: 0x04001E96 RID: 7830
		public KamikazeSootEnemyIdleState Idle;

		// Token: 0x04001E97 RID: 7831
		public KamikazeSootEnemyAlertState Alert;

		// Token: 0x04001E98 RID: 7832
		public KamikazeSootEnemyRollingState Rolling;
	}
}
