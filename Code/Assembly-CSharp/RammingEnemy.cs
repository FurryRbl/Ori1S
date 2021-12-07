using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class RammingEnemy : GroundEnemy
{
	// Token: 0x06001572 RID: 5490 RVA: 0x0005F25D File Offset: 0x0005D45D
	public override bool CanBeOptimized()
	{
		return this.Controller.StateMachine.CurrentState == this.State.Idle;
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x0005F27C File Offset: 0x0005D47C
	public bool ZoneRectanglesContain(Vector2 position)
	{
		for (int i = 0; i < this.Zones.Length; i++)
		{
			Transform transform = this.Zones[i];
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

	// Token: 0x06001574 RID: 5492 RVA: 0x0005F300 File Offset: 0x0005D500
	public void OnDash()
	{
		base.PlaySound(this.Sounds.Dash);
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x0005F313 File Offset: 0x0005D513
	public bool IsOnGround()
	{
		return this.PlatformMovement.IsOnGround;
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x0005F320 File Offset: 0x0005D520
	public override void Awake()
	{
		base.Awake();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
		EntityDamageDealer damageDealer = this.DamageDealer;
		damageDealer.OnDamageDealtEvent = (Action<GameObject, Damage>)Delegate.Combine(damageDealer.OnDamageDealtEvent, new Action<GameObject, Damage>(this.OnDamageDealt));
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x0005F382 File Offset: 0x0005D582
	public void OnDamageDealt(GameObject go, Damage damage)
	{
		if (go.CompareTag("Player"))
		{
			base.PlaySound(this.Sounds.HitSein);
		}
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x0005F3A8 File Offset: 0x0005D5A8
	public override void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Remove(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x0005F3E4 File Offset: 0x0005D5E4
	public virtual void OnModifyDamage(Damage damage)
	{
		if (damage.Type != DamageType.Enemy)
		{
			base.PlaySound(this.Sounds.Hurt);
		}
		if (damage.Type == DamageType.Bash)
		{
			this.Controller.StateMachine.ChangeState(this.State.KnockBack);
			base.PlaySound(this.Sounds.Deflected);
			base.FaceLeft = (damage.Force.x > 0f);
		}
		if (damage.Type == DamageType.Crush)
		{
			AchievementsLogic.Instance.OnCrushRamWithStomper();
		}
	}

	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x0600157A RID: 5498 RVA: 0x0005F47A File Offset: 0x0005D67A
	public bool CanBeFrozen
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x0600157B RID: 5499 RVA: 0x0005F47D File Offset: 0x0005D67D
	public float CurrentStateTime
	{
		get
		{
			return this.Controller.StateMachine.CurrentStateTime;
		}
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x0005F490 File Offset: 0x0005D690
	public new void Start()
	{
		base.FacePlayer();
		base.Start();
		this.State.Idle = new RammingIdleState(this);
		this.State.Alert = new RammingAlertState(this);
		this.State.Running = new RammingRunningState(this);
		this.State.Braking = new RammingBrakingState(this);
		this.State.RetreatBraking = new RammingBrakingState(this);
		this.State.HitWall = new RammingHitWallState(this);
		this.State.KnockBack = new RammingKnockBackState(this);
		this.State.Stunned = new RammingStunnedState(this);
		this.State.Retreat = new RammingRetreatState(this);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Alert,
			this.State.Running,
			this.State.Braking,
			this.State.RetreatBraking,
			this.State.HitWall,
			this.State.KnockBack,
			this.State.Stunned,
			this.State.Retreat
		});
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Alert, () => base.AfterTime(this.IdleWaitTime) && this.CanSeePlayer(), null).AddTransition<OnReceiveDamage>(this.State.Alert, () => base.AfterTime(this.IdleWaitTime), null).AddTransition<OnFixedUpdate>(this.State.Retreat, () => base.AfterTime(this.IdleWaitTime) && this.CantSeePlayerAndTooClose() && !this.IsCornered(), null);
		this.Controller.StateMachine.Configure(this.State.Alert).AddTransition<OnAnimationOrTransitionEnded>(this.State.Running, null, new Action(this.OnDash));
		this.Controller.StateMachine.Configure(this.State.Running).AddTransition<OnFixedUpdate>(this.State.Braking, () => this.CantSeePlayer() || !base.IsFacingPlayer, null).AddTransition<OnFixedUpdate>(this.State.HitWall, new Func<bool>(this.HasHitWall), null);
		this.Controller.StateMachine.Configure(this.State.Braking).AddTransition<OnAnimationEnded>(this.State.Retreat, new Func<bool>(this.CantSeePlayer), null).AddTransition<OnAnimationEnded>(this.State.Alert, null, null).AddTransition<OnFixedUpdate>(this.State.HitWall, () => this.HasHitWall() && this.State.Braking.HitWallIsAppropriate(), null);
		this.Controller.StateMachine.Configure(this.State.RetreatBraking).AddTransition<OnAnimationEnded>(this.State.Idle, new Func<bool>(this.CantSeePlayer), null).AddTransition<OnAnimationEnded>(this.State.Alert, null, null);
		this.Controller.StateMachine.Configure(this.State.HitWall).AddTransition<OnFixedUpdate>(this.State.Alert, () => base.AfterTime(this.Settings.BouncingDuration) && this.CanSeePlayer(), null).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.BouncingDuration), null);
		this.Controller.StateMachine.Configure(this.State.Stunned).AddTransition<OnFixedUpdate>(this.State.Alert, () => base.AfterTime(this.Settings.StunnedDuration) && this.CanSeePlayer(), null).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.StunnedDuration), null);
		this.Controller.StateMachine.Configure(this.State.Retreat).AddTransition<OnFixedUpdate>(this.State.Alert, () => base.AfterTime(0.5f) && this.ShouldStopRetreating() && this.CanSeePlayer(), null).AddTransition<OnFixedUpdate>(this.State.RetreatBraking, () => base.AfterTime(0.5f) && this.ShouldStopRetreating(), null).AddTransition<OnFixedUpdate>(this.State.Idle, new Func<bool>(this.HasHitWall), null);
		this.Controller.StateMachine.Configure(this.State.KnockBack).AddTransition<OnFixedUpdate>(this.State.Alert, () => base.AfterTime(this.Settings.KnockBackDuration) && this.CanSeePlayer(), null).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.KnockBackDuration), null);
		this.Controller.StateMachine.ChangeState(this.State.Idle);
		this.PlatformMovement.PlaceOnGround(0.5f, 0f);
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x0005F934 File Offset: 0x0005DB34
	public bool HasHitWall()
	{
		return (this.PlatformMovement.HasWallLeft && this.PlatformMovement.LocalSpeedX < 0f) || (this.PlatformMovement.HasWallRight && this.PlatformMovement.LocalSpeedX > 0f);
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x0005F990 File Offset: 0x0005DB90
	public bool IsCornered()
	{
		return (Physics.Linecast(this.PlatformMovement.Position, this.PlatformMovement.Position - new Vector3(1.4f, 0f)) && base.PlayerPosition.x > this.PlatformMovement.PositionX) || (Physics.Linecast(this.PlatformMovement.Position, this.PlatformMovement.Position + new Vector3(1.4f, 0f)) && base.PlayerPosition.x < this.PlatformMovement.PositionX);
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x0005FA44 File Offset: 0x0005DC44
	public bool HasLanded()
	{
		return this.PlatformMovement.IsOnGround;
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x0005FA54 File Offset: 0x0005DC54
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.IsSuspended)
		{
			return;
		}
		this.PlatformMovement.LocalSpeedY -= this.Settings.Gravity * Time.deltaTime;
		this.UpdateRotation();
		if (base.IsInWater)
		{
			base.Drown();
		}
		if (!this.EnemyInsideZone)
		{
			(this.PlatformMovement as EntityPlatformingMovement).Kickback.Stop();
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06001581 RID: 5505 RVA: 0x0005FACD File Offset: 0x0005DCCD
	public bool EnemyInsideZone
	{
		get
		{
			return this.ZoneRectanglesContain(base.Position);
		}
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x0005FAE0 File Offset: 0x0005DCE0
	public bool CanSeePlayer()
	{
		return (!this.PlatformMovement.HasWallLeft || !base.PlayerIsToLeft) && (!this.PlatformMovement.HasWallRight || base.PlayerIsToLeft) && (this.PlayerInsideZone() && base.PositionToPlayerPosition.magnitude < this.Settings.AlertRange) && base.IsOnScreen();
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x0005FB5C File Offset: 0x0005DD5C
	public bool PlayerInsideZone()
	{
		return this.EnemyInsideZone && this.ZoneRectanglesContain(base.PlayerPosition);
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x0005FB88 File Offset: 0x0005DD88
	public bool CantSeePlayer()
	{
		return (this.PlatformMovement.HasWallLeft && base.PlayerIsToLeft) || (this.PlatformMovement.HasWallRight && !base.PlayerIsToLeft) || !this.PlayerInsideZone();
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x0005FBD8 File Offset: 0x0005DDD8
	public void UpdateRotation()
	{
		float b = (!this.PlatformMovement.IsOnGround) ? 0f : this.PlatformMovement.GroundAngle;
		this.FeetTransform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(this.FeetTransform.eulerAngles.z, b, 0.2f));
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x0005FC44 File Offset: 0x0005DE44
	public bool CantSeePlayerAndTooClose()
	{
		return !this.EnemyInsideZone || (base.PositionToPlayerPosition.magnitude < this.Settings.RetreatDistance && !this.PlayerInsideZone());
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x0005FC8C File Offset: 0x0005DE8C
	public bool ShouldStopRetreating()
	{
		return base.PositionToPlayerPosition.magnitude > this.Settings.RetreatDistance || this.PlayerInsideZone();
	}

	// Token: 0x0400128E RID: 4750
	public RammingEnemyAnimations Animations;

	// Token: 0x0400128F RID: 4751
	public RammingEnemySettings Settings;

	// Token: 0x04001290 RID: 4752
	public RammingEnemySounds Sounds;

	// Token: 0x04001291 RID: 4753
	public PrefabSpawner HitWallEffect;

	// Token: 0x04001292 RID: 4754
	public Transform[] Zones;

	// Token: 0x04001293 RID: 4755
	public RammingEnemy.States State = new RammingEnemy.States();

	// Token: 0x04001294 RID: 4756
	public float IdleWaitTime;

	// Token: 0x0200058D RID: 1421
	public class States
	{
		// Token: 0x04001ED3 RID: 7891
		public RammingIdleState Idle;

		// Token: 0x04001ED4 RID: 7892
		public RammingAlertState Alert;

		// Token: 0x04001ED5 RID: 7893
		public RammingRunningState Running;

		// Token: 0x04001ED6 RID: 7894
		public RammingBrakingState Braking;

		// Token: 0x04001ED7 RID: 7895
		public RammingBrakingState RetreatBraking;

		// Token: 0x04001ED8 RID: 7896
		public RammingHitWallState HitWall;

		// Token: 0x04001ED9 RID: 7897
		public RammingStunnedState Stunned;

		// Token: 0x04001EDA RID: 7898
		public RammingKnockBackState KnockBack;

		// Token: 0x04001EDB RID: 7899
		public RammingRetreatState Retreat;
	}
}
