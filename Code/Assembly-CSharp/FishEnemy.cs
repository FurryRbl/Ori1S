using System;
using fsm;
using fsm.triggers;
using Game;
using UnityEngine;

// Token: 0x02000520 RID: 1312
public class FishEnemy : Enemy
{
	// Token: 0x060022DD RID: 8925 RVA: 0x00098BA4 File Offset: 0x00096DA4
	public override bool CanBeOptimized()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		return currentState == this.State.Idle;
	}

	// Token: 0x170005F5 RID: 1525
	// (get) Token: 0x060022DE RID: 8926 RVA: 0x00098BD0 File Offset: 0x00096DD0
	public float BendValue
	{
		get
		{
			return this.AnimationFromBend.Evaluate((float)((!base.FaceLeft) ? -1 : 1) * this.m_currentAngularVelocity);
		}
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x00098BF8 File Offset: 0x00096DF8
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		float angle = this.Angle;
		float b = Mathf.DeltaAngle(angle, this.m_lastAngle) / Time.deltaTime;
		this.m_lastAngle = angle;
		this.m_currentAngularVelocity = Mathf.Lerp(this.m_currentAngularVelocity, b, 0.5f);
		bool flag = false;
		Vector3 position = base.Position;
		for (int i = 0; i < Zones.WaterZones.Count; i++)
		{
			WaterZone waterZone = Zones.WaterZones[i];
			if (waterZone.Bounds.Contains(position))
			{
				flag = true;
			}
		}
		if (this.m_inWater != flag)
		{
			if (this.m_inWater)
			{
				this.OnExitWater();
			}
			else
			{
				this.OnEnterWater();
			}
		}
		this.m_inWater = flag;
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x00098CC8 File Offset: 0x00096EC8
	public bool ShouldThrow()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.Bash;
	}

	// Token: 0x060022E1 RID: 8929 RVA: 0x00098CFC File Offset: 0x00096EFC
	public void OnBashed()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		this.FlyMovement.Velocity = onReceiveDamage.Damage.Force.normalized * this.Settings.BashSpeed;
	}

	// Token: 0x060022E2 RID: 8930 RVA: 0x00098D50 File Offset: 0x00096F50
	public bool WasBashed()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.Bash;
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x00098D84 File Offset: 0x00096F84
	public bool HitGround()
	{
		OnCollisionStay onCollisionStay = (OnCollisionStay)this.Controller.StateMachine.CurrentTrigger;
		foreach (ContactPoint contactPoint in onCollisionStay.Collision.contacts)
		{
			if (Vector3.Dot(contactPoint.normal, Vector3.up) > Mathf.Cos(1.0471976f))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x00098DF8 File Offset: 0x00096FF8
	public new void Start()
	{
		base.Start();
		this.State.Idle = new FishIdleState(this);
		this.State.Swim = new FishSwimState(this);
		this.State.Attack = new FishAttackState(this);
		this.State.Bashed = new FishBashedState(this);
		this.State.Bounce = new FishBounceState(this);
		this.State.Fall = new FishFallState(this);
		this.State.Flop = new FishFlopState(this);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Swim,
			this.State.Attack,
			this.State.Bashed,
			this.State.Bounce,
			this.State.Fall
		});
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Swim, () => this.InSwimDistance() && !this.PlayerIsTooFarFromStartPosition() && this.PlayerInWater(), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.WasBashed), new Action(this.OnBashed)).AddTransition<OnFixedUpdate>(this.State.Fall, new Func<bool>(this.OutOfWater), null);
		this.Controller.StateMachine.Configure(this.State.Swim).AddTransition<OnFixedUpdate>(this.State.Attack, () => this.InAttackDistance() && !this.PlayerIsTooFarFromStartPosition() && base.AfterTime(0.5f), null).AddTransition<OnFixedUpdate>(this.State.Idle, () => this.OutOfSwimDistance() || this.PlayerIsTooFarFromStartPosition() || !this.PlayerInWater(), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.WasBashed), new Action(this.OnBashed)).AddTransition<OnFixedUpdate>(this.State.Fall, new Func<bool>(this.OutOfWater), null);
		this.Controller.StateMachine.Configure(this.State.Attack).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.AttackDuration) && this.OutOfSwimDistance(), null).AddTransition<OnFixedUpdate>(this.State.Swim, () => base.AfterTime(this.Settings.AttackDuration), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.WasBashed), new Action(this.OnBashed));
		this.Controller.StateMachine.Configure(this.State.Bashed).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.BashDuration) && this.InWater(), null).AddTransition<OnFixedUpdate>(this.State.Fall, () => base.AfterTime(this.Settings.BashDuration - 0.4f) && this.OutOfWater(), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.WasBashed), new Action(this.OnBashed));
		this.Controller.StateMachine.Configure(this.State.Bounce).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.BounceDuration), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.WasBashed), new Action(this.OnBashed));
		this.Controller.StateMachine.Configure(this.State.Fall).AddTransition<OnFixedUpdate>(this.State.Idle, new Func<bool>(this.InWater), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.WasBashed), new Action(this.OnBashed)).AddTransition<OnCollisionStay>(this.State.Flop, new Func<bool>(this.HitGround), null);
		this.Controller.StateMachine.Configure(this.State.Flop).AddTransition<OnFixedUpdate>(this.State.Idle, new Func<bool>(this.InWater), null).AddTransition<OnReceiveDamage>(this.State.Bashed, new Func<bool>(this.WasBashed), new Action(this.OnBashed));
		this.Controller.StateMachine.ChangeState(this.State.Idle);
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x0009925C File Offset: 0x0009745C
	public void ApplyGravity()
	{
		this.FlyMovement.VelocityY -= this.Settings.Gravity * Time.deltaTime;
	}

	// Token: 0x060022E6 RID: 8934 RVA: 0x00099284 File Offset: 0x00097484
	public bool PlayerIsTooFarFromStartPosition()
	{
		return base.StartPositionToPlayerPosition.magnitude > this.Settings.MaxSwimDistance;
	}

	// Token: 0x060022E7 RID: 8935 RVA: 0x000992AC File Offset: 0x000974AC
	public bool InSwimDistance()
	{
		return base.PositionToPlayerPosition.magnitude < this.Settings.EnterSwimRange;
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x000992D4 File Offset: 0x000974D4
	public bool InAttackDistance()
	{
		return base.PositionToPlayerPosition.magnitude < this.Settings.AttackRange;
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x000992FC File Offset: 0x000974FC
	public bool OutOfSwimDistance()
	{
		return base.PositionToPlayerPosition.magnitude > this.Settings.MaxSwimDistance;
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x00099324 File Offset: 0x00097524
	public bool PlayerInWater()
	{
		return Characters.Sein.Controller.IsSwimming;
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x00099335 File Offset: 0x00097535
	public bool OutOfWater()
	{
		return !this.m_inWater;
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x00099340 File Offset: 0x00097540
	public bool InWater()
	{
		return this.m_inWater;
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x00099348 File Offset: 0x00097548
	public void UpdateSpriteRotation()
	{
		float z = this.Angle + (float)((!base.FaceLeft) ? 0 : 180);
		Vector3 eulerAngles = this.Rotation.transform.eulerAngles;
		eulerAngles.z = z;
		this.Rotation.transform.eulerAngles = eulerAngles;
	}

	// Token: 0x170005F6 RID: 1526
	// (get) Token: 0x060022EE RID: 8942 RVA: 0x0009939E File Offset: 0x0009759E
	public Vector2 AngleAsVector
	{
		get
		{
			return MoonMath.Angle.VectorFromAngle(this.Angle);
		}
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x000993AC File Offset: 0x000975AC
	public void ApplySoftSpeed(Vector2 speed)
	{
		this.FlyMovement.Velocity = Vector3.Lerp(this.FlyMovement.Velocity, speed, 0.2f);
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x000993E9 File Offset: 0x000975E9
	public void OnExitWater()
	{
		if (this.Sounds.ExitWater)
		{
			this.Sounds.ExitWater.Play();
		}
	}

	// Token: 0x060022F1 RID: 8945 RVA: 0x00099410 File Offset: 0x00097610
	public void OnEnterWater()
	{
		if (this.Sounds.EnterWater)
		{
			this.Sounds.EnterWater.Play();
		}
	}

	// Token: 0x04001D56 RID: 7510
	public FishEnemyAnimations Animations;

	// Token: 0x04001D57 RID: 7511
	public FishEnemySounds Sounds;

	// Token: 0x04001D58 RID: 7512
	public FlyMovement FlyMovement;

	// Token: 0x04001D59 RID: 7513
	public FishEnemySettings Settings;

	// Token: 0x04001D5A RID: 7514
	public new Transform Rotation;

	// Token: 0x04001D5B RID: 7515
	public FishEnemy.States State = new FishEnemy.States();

	// Token: 0x04001D5C RID: 7516
	public Transform WanderTarget;

	// Token: 0x04001D5D RID: 7517
	public AnimationCurve AnimationFromBend;

	// Token: 0x04001D5E RID: 7518
	private float m_lastAngle;

	// Token: 0x04001D5F RID: 7519
	private float m_currentAngularVelocity;

	// Token: 0x04001D60 RID: 7520
	private bool m_inWater;

	// Token: 0x04001D61 RID: 7521
	public float Angle;

	// Token: 0x02000526 RID: 1318
	public class States
	{
		// Token: 0x04001D88 RID: 7560
		public FishIdleState Idle;

		// Token: 0x04001D89 RID: 7561
		public FishSwimState Swim;

		// Token: 0x04001D8A RID: 7562
		public FishAttackState Attack;

		// Token: 0x04001D8B RID: 7563
		public FishBashedState Bashed;

		// Token: 0x04001D8C RID: 7564
		public FishBounceState Bounce;

		// Token: 0x04001D8D RID: 7565
		public FishFlopState Flop;

		// Token: 0x04001D8E RID: 7566
		public FishFallState Fall;
	}
}
