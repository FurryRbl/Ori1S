using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x020005EC RID: 1516
public class SwarmEnemy : GroundEnemy
{
	// Token: 0x0600260F RID: 9743 RVA: 0x000A6F5C File Offset: 0x000A515C
	public override bool CanBeOptimized()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		return currentState == this.State.Idle;
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x000A6F88 File Offset: 0x000A5188
	public override void Awake()
	{
		base.Awake();
		this.DamageReciever.OnDeathEvent.Add(new Action<Damage>(this.OnDeath));
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnPreProcessDamage));
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x000A6FE0 File Offset: 0x000A51E0
	public void OnPreProcessDamage(Damage damage)
	{
		EntityDamageDealer component = damage.Sender.GetComponent<EntityDamageDealer>();
		if (component != null)
		{
			Entity entity = component.Entity;
			if (entity is SwarmEnemy && base.GetInstanceID() > entity.GetInstanceID())
			{
				this.PlatformMovement.LocalSpeedX *= 0.7f;
			}
		}
	}

	// Token: 0x06002612 RID: 9746 RVA: 0x000A7040 File Offset: 0x000A5240
	public new void Start()
	{
		base.Start();
		this.State.Idle = new State
		{
			OnEnterEvent = new Action(this.OnEnterIdle),
			UpdateStateEvent = new Action(this.UpdateIdle),
			OnExitEvent = new Action(this.OnExitIdle)
		};
		this.State.Run = new State
		{
			OnEnterEvent = new Action(this.OnEnterRun),
			UpdateStateEvent = new Action(this.UpdateRun),
			OnExitEvent = new Action(this.OnExitRun)
		};
		this.State.Spawned = new State
		{
			OnEnterEvent = new Action(this.OnEnterSpawned),
			UpdateStateEvent = new Action(this.UpdateSpawned)
		};
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Run,
			this.State.Spawned
		});
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Run, new Func<bool>(this.ShouldRun), null);
		this.Controller.StateMachine.Configure(this.State.Run).AddTransition<OnFixedUpdate>(this.State.Idle, () => !this.ShouldRun(), null);
		this.Controller.StateMachine.Configure(this.State.Spawned).AddTransition<OnFixedUpdate>(this.State.Run, () => base.AfterTime(0.5f), null);
		this.Controller.StateMachine.ChangeState((!this.m_wasSpawned) ? this.State.Idle : this.State.Spawned);
	}

	// Token: 0x06002613 RID: 9747 RVA: 0x000A723C File Offset: 0x000A543C
	public bool ShouldRun()
	{
		float num = (float)Math.Sign(base.PositionToPlayerPosition.x);
		bool flag = this.Size != 0f && Physics.Linecast(base.transform.position + new Vector3(num * (this.Size - 1f), 0f), base.transform.position + new Vector3(num * this.Size, 0f));
		bool flag2;
		return !EnemyStopper.InsideEnemyStopper(base.Position, (!base.PlayerIsToLeft) ? Vector3.right : Vector3.left, out flag2) && (this.Controller.IsNearSein() && Mathf.Abs(base.PositionToPlayerPosition.x) > 0.5f) && !flag;
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x000A7327 File Offset: 0x000A5527
	public void SetModeToSpawned()
	{
		this.m_wasSpawned = true;
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x000A7330 File Offset: 0x000A5530
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		this.PlatformMovement.LocalSpeedY -= this.Settings.Gravity * Time.deltaTime;
		if (this.PlatformMovement.LocalSpeedY < -this.Settings.MaxFallSpeed)
		{
			this.PlatformMovement.LocalSpeedY = -this.Settings.MaxFallSpeed;
		}
		this.UpdateRotation();
		if (base.IsInWater)
		{
			base.Drown();
		}
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x000A73BC File Offset: 0x000A55BC
	public void UpdateRotation()
	{
		float num = this.SpeedXToRotation.Evaluate(this.PlatformMovement.LocalSpeedX) * this.SpeedYToRotation.Evaluate(this.PlatformMovement.LocalSpeedX) * this.AirTiltAngle;
		float b = (!this.PlatformMovement.IsOnGround) ? num : this.PlatformMovement.GroundAngle;
		this.FeetTransform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(this.FeetTransform.eulerAngles.z, b, 0.1f));
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x000A7458 File Offset: 0x000A5658
	public void OnEnterIdle()
	{
		if (this.Idle)
		{
			this.Idle.Play();
		}
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x000A7475 File Offset: 0x000A5675
	public void OnEnterRun()
	{
		if (this.Walking)
		{
			this.Walking.Play();
		}
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x000A7492 File Offset: 0x000A5692
	public void OnExitIdle()
	{
		if (this.Idle)
		{
			this.Idle.Stop();
		}
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x000A74AF File Offset: 0x000A56AF
	public void OnExitRun()
	{
		if (this.Walking)
		{
			this.Walking.Stop();
		}
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x000A74CC File Offset: 0x000A56CC
	public void OnEnterSpawned()
	{
		base.RestartAnimationLoop(this.Animations.Spawned, 0);
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x000A74E0 File Offset: 0x000A56E0
	public void UpdateIdle()
	{
		if (this.PlatformMovement.IsOnGround)
		{
			base.PlayAnimationLoop(this.Animations.Idle, 0);
		}
		else
		{
			base.PlayAnimationLoop((!this.CanFall) ? this.Animations.Idle : this.Animations.Fall, 0);
		}
		this.PlatformMovement.LocalSpeedX = MoonMath.Movement.DecelerateSpeed(this.PlatformMovement.LocalSpeedX, this.Settings.Decceleration);
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x000A7568 File Offset: 0x000A5768
	public void UpdateRun()
	{
		if (this.PlatformMovement.IsOnGround)
		{
			base.PlayAnimationLoop((!base.PlayerIsToLeft) ? this.Animations.RunRight : this.Animations.RunLeft, 0);
		}
		else
		{
			base.PlayAnimationLoop((!this.CanFall) ? ((!base.PlayerIsToLeft) ? this.Animations.RunRight : this.Animations.RunLeft) : this.Animations.Fall, 0);
		}
		this.PlatformMovement.LocalSpeedX = this.Settings.Speed * this.Settings.MoveCurve.Evaluate(base.SpriteAnimator.CurrentAnimationTime) * (float)((!base.PlayerIsToLeft) ? 1 : -1);
		if (this.Settings.JumpDelay > 0f)
		{
			if (this.m_jumpDelay < 0f && this.PlatformMovement.IsOnGround)
			{
				this.m_jumpDelay = this.Settings.JumpDelay;
				this.PlatformMovement.LocalSpeedY = this.Settings.JumpStrength;
				base.PlayAnimationOnce(this.Animations.Jump, 1);
			}
			this.m_jumpDelay -= Time.deltaTime;
		}
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x000A76C5 File Offset: 0x000A58C5
	public void UpdateSpawned()
	{
	}

	// Token: 0x0600261F RID: 9759 RVA: 0x000A76C8 File Offset: 0x000A58C8
	public void OnDeath(Damage damage)
	{
		if (this.Settings.Child)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector3 velocity = (((i != 0) ? Vector3.right : Vector3.left) + Vector3.up * 3f) * 7f;
				SwarmEnemyManager.Instance.QueueSpawn(base.transform.position, velocity, (int)(this.Loot.LootAmount * this.Loot.LootMultiplier), this.OrbSpawner, this.DamageDealer.Damage, this.Settings.Child, this.SceneRootGUID, this.Owner);
			}
		}
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x000A7786 File Offset: 0x000A5986
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.Owner.OnChildComponentDestroy(this);
	}

	// Token: 0x040020A0 RID: 8352
	public SwarmEnemyAnimations Animations;

	// Token: 0x040020A1 RID: 8353
	public SwarmEnemySettings Settings;

	// Token: 0x040020A2 RID: 8354
	public SwarmEnemyLootSettings Loot;

	// Token: 0x040020A3 RID: 8355
	public OrbSpawner OrbSpawner;

	// Token: 0x040020A4 RID: 8356
	public SoundSource Idle;

	// Token: 0x040020A5 RID: 8357
	public SoundSource Walking;

	// Token: 0x040020A6 RID: 8358
	public bool CanFall = true;

	// Token: 0x040020A7 RID: 8359
	public float Size;

	// Token: 0x040020A8 RID: 8360
	public SwarmEnemy.States State = new SwarmEnemy.States();

	// Token: 0x040020A9 RID: 8361
	private bool m_wasSpawned;

	// Token: 0x040020AA RID: 8362
	public AnimationCurve SpeedXToRotation;

	// Token: 0x040020AB RID: 8363
	public AnimationCurve SpeedYToRotation;

	// Token: 0x040020AC RID: 8364
	public float AirTiltAngle;

	// Token: 0x040020AD RID: 8365
	private float m_jumpDelay;

	// Token: 0x040020AE RID: 8366
	public SwarmEnemyPlaceholder Owner;

	// Token: 0x020005ED RID: 1517
	public class States
	{
		// Token: 0x040020AF RID: 8367
		public State Idle;

		// Token: 0x040020B0 RID: 8368
		public State Run;

		// Token: 0x040020B1 RID: 8369
		public State Spawned;

		// Token: 0x040020B2 RID: 8370
		public State Thrown;

		// Token: 0x040020B3 RID: 8371
		public State Frozen;
	}
}
