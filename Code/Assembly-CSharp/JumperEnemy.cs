using System;
using fsm;
using fsm.triggers;
using Game;
using UnityEngine;

// Token: 0x02000551 RID: 1361
public class JumperEnemy : GroundEnemy
{
	// Token: 0x06002387 RID: 9095 RVA: 0x0009B2A8 File Offset: 0x000994A8
	public override bool CanBeOptimized()
	{
		return this.Controller.StateMachine.CurrentState == this.State.Idle;
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x0009B2C7 File Offset: 0x000994C7
	public void ForceAttackPlayer()
	{
		this.Controller.StateMachine.ChangeState(this.State.JumpCharge);
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x0009B2E4 File Offset: 0x000994E4
	public new void Start()
	{
		base.Start();
		this.State.Idle = new JumperEnemyIdleState(this);
		this.State.JumpCharge = new JumperEnemyChargingState(this);
		this.State.Fall = new JumperEnemyFallState(this);
		this.State.Thrown = new JumperEnemyThrownState(this);
		this.State.Stomped = new JumperEnemyStompedState(this);
		this.State.Stunned = new JumperEnemyStunnedState(this);
		this.State.Respawn = new State();
		this.State.Respawn.OnEnterEvent = delegate()
		{
			base.PlayAnimationOnce(this.Animations.Respawn, 0);
			base.FacePlayer();
			base.SpawnPrefab(this.Settings.RespawnEffect);
		};
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.JumpCharge, new Func<bool>(this.PlayerInRange), null).AddTransition<OnFixedUpdate>(this.State.JumpCharge, new Func<bool>(this.OutOfJumpingZone), null).AddTransition<AttackTriggered>(this.State.JumpCharge, null, null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.OnStomped)).AddTransition<OnReceiveDamage>(this.State.JumpCharge, null, null);
		this.Controller.StateMachine.Configure(this.State.JumpCharge).AddTransition<OnFixedUpdate>(this.State.Fall, () => base.AfterTime(this.Settings.ChargingDuration), new Action(this.DoJump)).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Fall).AddTransition<OnFixedUpdate>(this.State.JumpCharge, () => this.LandedOnGround() && this.PlayerInRange(), new Action(this.OnLanded)).AddTransition<OnFixedUpdate>(this.State.Idle, () => this.LandedOnGround() && !this.PlayerInRange(), new Action(this.OnLanded)).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Thrown).AddTransition<OnFixedUpdate>(this.State.Stunned, new Func<bool>(this.IsOnGround), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Stomped).AddTransition<OnFixedUpdate>(this.State.Stunned, new Func<bool>(this.IsOnGround), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Stunned).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.StunnedDuration), null).AddTransition<OnReceiveDamage>(this.State.Thrown, new Func<bool>(this.ShouldThrow), new Action(this.OnThrow)).AddTransition<OnReceiveDamage>(this.State.Stomped, new Func<bool>(this.ShouldStomped), new Action(this.OnStomped));
		this.Controller.StateMachine.Configure(this.State.Respawn).AddTransition<OnAnimationEnded>(this.State.Idle, null, null);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.JumpCharge,
			this.State.Fall,
			this.State.Stunned,
			this.State.Thrown,
			this.State.Stomped,
			this.State.Respawn
		});
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

	// Token: 0x0600238A RID: 9098 RVA: 0x0009B831 File Offset: 0x00099A31
	public new void OnTimedRespawn()
	{
		this.m_timedRespawn = true;
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x0009B83C File Offset: 0x00099A3C
	public bool OutOfJumpingZone()
	{
		return !(this.JumpingZone == null) && !new Rect
		{
			width = this.JumpingZone.lossyScale.x,
			height = this.JumpingZone.lossyScale.y,
			center = this.JumpingZone.position
		}.Contains(base.Position);
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x0009B8C3 File Offset: 0x00099AC3
	public bool IsOnGround()
	{
		return this.PlatformMovement.IsOnGround;
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x0009B8D0 File Offset: 0x00099AD0
	public bool ShouldThrow()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.Bash;
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x0009B904 File Offset: 0x00099B04
	public bool ShouldStomped()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		return onReceiveDamage.Damage.Type == DamageType.StompBlast;
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x0009B938 File Offset: 0x00099B38
	public void OnThrow()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		this.PlatformMovement.WorldSpeed = onReceiveDamage.Damage.Force * 10f;
		if (this.PlatformMovement.IsOnGround && this.PlatformMovement.LocalSpeedY < 0f)
		{
			this.PlatformMovement.LocalSpeedY *= -0.5f;
		}
		this.m_thrownDirection = onReceiveDamage.Damage.Force.normalized;
		base.FaceLeft = (this.PlatformMovement.LocalSpeedX < 0f);
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x0009B9F0 File Offset: 0x00099BF0
	public void OnStomped()
	{
		OnReceiveDamage onReceiveDamage = (OnReceiveDamage)this.Controller.StateMachine.CurrentTrigger;
		this.PlatformMovement.WorldSpeed = onReceiveDamage.Damage.Force * 8f;
		if (this.PlatformMovement.IsOnGround && this.PlatformMovement.LocalSpeedY < 0f)
		{
			this.PlatformMovement.LocalSpeedY *= -0.5f;
		}
		this.m_thrownDirection = onReceiveDamage.Damage.Force.normalized;
		base.FaceLeft = (this.PlatformMovement.LocalSpeedX < 0f);
	}

	// Token: 0x06002391 RID: 9105 RVA: 0x0009BAA8 File Offset: 0x00099CA8
	public void DoJump()
	{
		Vector2 localSpeed;
		localSpeed.y = PhysicsHelper.CalculateSpeedFromHeight(this.Settings.JumpHeight, this.Settings.Gravity);
		float num = 2f * localSpeed.y / this.Settings.Gravity;
		localSpeed.x = this.Settings.JumpDistance / num;
		if (this.OutOfJumpingZone())
		{
			localSpeed.x = Mathf.Clamp((this.JumpingZone.position.x - base.Position.x) / num, -localSpeed.x, localSpeed.x);
			this.m_shouldStomp = false;
		}
		else
		{
			localSpeed.x = Mathf.Clamp(base.PositionToPlayerPosition.x / num, -localSpeed.x, localSpeed.x);
			this.m_shouldStomp = (Mathf.Abs((base.PlayerPosition + this.m_playerSmoothSpeed - base.Position).x) < this.Settings.StompAttackDistance);
		}
		Vector3 vector = new Vector3(localSpeed.x * num * 0.5f, this.Settings.JumpHeight);
		bool flag = Mathf.Sign(localSpeed.x) != (float)base.FaceLeftSign;
		if (Physics.Raycast(new Ray(base.Position, vector.normalized), vector.magnitude, this.RaycastLayerMask) || !this.m_shouldStomp)
		{
			localSpeed.y = PhysicsHelper.CalculateSpeedFromHeight(this.Settings.ShortJumpHeight, this.Settings.Gravity);
			this.Animation.Play((!flag) ? this.Animations.ShortJump : this.Animations.JumpFlip, 1, () => !this.PlatformMovement.IsOnGround);
			this.m_shouldStomp = false;
		}
		else
		{
			this.Animation.Play((!flag) ? this.Animations.Jump : this.Animations.JumpFlip, 1, () => !this.PlatformMovement.IsOnGround);
		}
		base.PlaySound(this.Sounds.Jump);
		if (flag)
		{
			base.FaceLeft = !base.FaceLeft;
		}
		this.PlatformMovement.LocalSpeed = localSpeed;
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x0009BD10 File Offset: 0x00099F10
	public bool PlayerInRange()
	{
		return base.PositionToPlayerPosition.magnitude < this.Settings.ChargeRange && this.Controller.NearSein;
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x0009BD4C File Offset: 0x00099F4C
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		this.PlatformMovement.LocalSpeedY -= Time.deltaTime * this.Settings.Gravity;
		if (this.PlatformMovement.LocalSpeedY < -this.Settings.MaxFallSpeed)
		{
			this.PlatformMovement.LocalSpeedY = -this.Settings.MaxFallSpeed;
		}
		if (this.PlatformMovement.IsOnCeiling)
		{
			this.PlatformMovement.LocalSpeedY = Mathf.Min(0f, this.PlatformMovement.LocalSpeedY);
		}
		this.UpdateRotation();
		if (Characters.Sein)
		{
			this.m_playerSmoothSpeed = Vector3.Lerp(this.m_playerSmoothSpeed, Characters.Sein.Speed, 0.1f);
		}
		if (base.IsInWater)
		{
			base.Drown();
		}
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x0009BE38 File Offset: 0x0009A038
	public void UpdateRotation()
	{
		IState currentState = this.Controller.StateMachine.CurrentState;
		float currentStateTime = this.Controller.StateMachine.CurrentStateTime;
		if (currentState == this.State.Thrown)
		{
			float num = 1f - Mathf.InverseLerp(0.3f, 0.6f, currentStateTime);
			this.FeetTransform.eulerAngles = new Vector3(0f, 0f, (MoonMath.Angle.AngleFromVector(this.m_thrownDirection) - 90f) * num);
		}
		else
		{
			float b = (!this.PlatformMovement.IsOnGround) ? 0f : this.PlatformMovement.GroundAngle;
			this.FeetTransform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(this.FeetTransform.eulerAngles.z, b, 0.2f));
		}
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x0009BF28 File Offset: 0x0009A128
	public bool LandedOnGround()
	{
		return this.PlatformMovement.IsOnGround && this.PlatformMovement.LocalSpeedY <= 0f;
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x0009BF60 File Offset: 0x0009A160
	public void OnLanded()
	{
		if (this.m_shouldStomp && this.Settings.HasStompExplosion)
		{
			if (this.StompEffect)
			{
				GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.StompEffect, base.Position, Quaternion.identity);
				gameObject.GetComponentInChildren<DamageDealer>().Damage = this.Settings.ExplosionDamage;
			}
			base.PlaySound(this.Sounds.Impact);
		}
		else
		{
			if (this.LandEffect)
			{
				InstantiateUtility.Instantiate(this.LandEffect, base.Position, Quaternion.identity);
			}
			base.PlaySound(this.Sounds.Impact);
		}
		Collider groundCollider = this.PlatformMovementListOfColliders.GroundCollider;
		Damage damage = new Damage((float)this.Settings.GroundStompDamage, Vector3.down * 3f, base.transform.position, DamageType.Stomp, base.gameObject);
		if (groundCollider)
		{
			damage.DealToComponents(groundCollider.gameObject);
		}
		this.PlatformMovement.LocalSpeed = Vector3.zero;
	}

	// Token: 0x04001DC8 RID: 7624
	public JumpingSootEnemyAnimations Animations;

	// Token: 0x04001DC9 RID: 7625
	public JumpingSootEnemySettings Settings;

	// Token: 0x04001DCA RID: 7626
	public JumpingSootEnemySounds Sounds;

	// Token: 0x04001DCB RID: 7627
	public JumperEnemy.States State = new JumperEnemy.States();

	// Token: 0x04001DCC RID: 7628
	public Transform JumpingZone;

	// Token: 0x04001DCD RID: 7629
	public LayerMask RaycastLayerMask;

	// Token: 0x04001DCE RID: 7630
	private Vector3 m_playerSmoothSpeed;

	// Token: 0x04001DCF RID: 7631
	private bool m_shouldStomp;

	// Token: 0x04001DD0 RID: 7632
	private Vector3 m_thrownDirection;

	// Token: 0x04001DD1 RID: 7633
	private bool m_timedRespawn;

	// Token: 0x04001DD2 RID: 7634
	public GameObject StompEffect;

	// Token: 0x04001DD3 RID: 7635
	public GameObject LandEffect;

	// Token: 0x02000575 RID: 1397
	public class States
	{
		// Token: 0x04001E6A RID: 7786
		public State Respawn;

		// Token: 0x04001E6B RID: 7787
		public JumperEnemyIdleState Idle;

		// Token: 0x04001E6C RID: 7788
		public JumperEnemyChargingState JumpCharge;

		// Token: 0x04001E6D RID: 7789
		public JumperEnemyFallState Fall;

		// Token: 0x04001E6E RID: 7790
		public JumperEnemyThrownState Thrown;

		// Token: 0x04001E6F RID: 7791
		public JumperEnemyStompedState Stomped;

		// Token: 0x04001E70 RID: 7792
		public JumperEnemyStunnedState Stunned;
	}
}
