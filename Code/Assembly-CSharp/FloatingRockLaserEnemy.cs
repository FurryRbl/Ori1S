using System;
using fsm;
using fsm.triggers;
using Game;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class FloatingRockLaserEnemy : Enemy
{
	// Token: 0x060024BF RID: 9407 RVA: 0x000A0228 File Offset: 0x0009E428
	public void PlayAnimationOnce(CharacterAnimationSystem animationSystem, TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && animationSystem)
		{
			animationSystem.Play(anim, layer, null);
		}
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x000A0258 File Offset: 0x0009E458
	public void RestartAnimationLoop(CharacterAnimationSystem animationSystem, TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && animationSystem)
		{
			animationSystem.RestartLoop(anim, layer, null);
		}
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x000A0285 File Offset: 0x0009E485
	public void PlayAnimationLoop(CharacterAnimationSystem animationSystem, TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && animationSystem)
		{
			animationSystem.PlayLoop(anim, layer, null, false);
		}
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x000A02A8 File Offset: 0x0009E4A8
	public new void Awake()
	{
		base.Awake();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x000A02E4 File Offset: 0x0009E4E4
	public new void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Remove(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x000A031F File Offset: 0x0009E51F
	public virtual void OnModifyDamage(Damage damage)
	{
		if (damage.Type == DamageType.SpiritFlame)
		{
			damage.SetAmount(0f);
		}
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x000A033C File Offset: 0x0009E53C
	public void OnEnterIdle()
	{
		this.RestartAnimationLoop(this.Animation, this.Animations.Idle, 0);
		this.RestartAnimationLoop(this.AnimationB, this.AnimationsB.Idle, 0);
		this.RestartAnimationLoop(this.AnimationC, this.AnimationsC.Idle, 0);
		if (this.IdleSound)
		{
			this.IdleSound.Play();
		}
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x000A03AC File Offset: 0x0009E5AC
	public void OnExitIdle()
	{
		if (this.IdleSound)
		{
			this.IdleSound.StopAndFadeOut(0.2f);
		}
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x000A03DC File Offset: 0x0009E5DC
	public void OnEnterCharge()
	{
		this.RestartAnimationLoop(this.Animation, this.Animations.Charging, 0);
		this.RestartAnimationLoop(this.AnimationB, this.AnimationsB.Charging, 0);
		this.RestartAnimationLoop(this.AnimationC, this.AnimationsC.Charging, 0);
		base.PlaySound(this.ChargingSound);
		base.SpawnPrefab(this.ChargingEffect);
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x000A044C File Offset: 0x0009E64C
	public void OnEnterLaser()
	{
		this.RestartAnimationLoop(this.Animation, this.Animations.Laser, 0);
		this.RestartAnimationLoop(this.AnimationB, this.AnimationsB.Laser, 0);
		this.RestartAnimationLoop(this.AnimationC, this.AnimationsC.Laser, 0);
		this.AimLaserAtPlayer();
		this.ActivateLaser();
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x000A04AD File Offset: 0x0009E6AD
	public void OnExitLaser()
	{
		this.DeactivateLaser();
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x000A04B8 File Offset: 0x0009E6B8
	public void OnEnterShooting()
	{
		this.RestartAnimationLoop(this.Animation, this.Animations.Shooting, 0);
		this.RestartAnimationLoop(this.AnimationB, this.AnimationsB.Shooting, 0);
		this.RestartAnimationLoop(this.AnimationC, this.AnimationsC.Shooting, 0);
		base.PlaySound(this.ShootingSound);
		base.SpawnPrefab(this.ShootingEffect);
		this.ProjectileSpawner.AimAt(Characters.Sein.Controller.Transform);
		Projectile projectile = this.ProjectileSpawner.SpawnProjectile();
		projectile.GetComponent<DamageDealer>().Damage = this.Settings.ProjectileDamage;
		this.Movement.ApplyImpulseForce(this.Settings.ShootingForce * this.ProjectileSpawner.Direction * -1f);
	}

	// Token: 0x060024CB RID: 9419 RVA: 0x000A0594 File Offset: 0x0009E794
	public void UpdateLaserState()
	{
		this.Movement.ApplyForce(-this.Settings.LaserForce * this.m_laserDirection);
		this.UpdateLaserDirection();
		this.UpdateLaser();
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x000A05D0 File Offset: 0x0009E7D0
	public new void Start()
	{
		base.Start();
		this.State.Idle = new State();
		this.State.Charge = new State();
		this.State.Laser = new State();
		this.State.Shooting = new State();
		State idle = this.State.Idle;
		idle.OnEnterEvent = (Action)Delegate.Combine(idle.OnEnterEvent, new Action(this.OnEnterIdle));
		State idle2 = this.State.Idle;
		idle2.OnExitEvent = (Action)Delegate.Combine(idle2.OnExitEvent, new Action(this.OnExitIdle));
		State charge = this.State.Charge;
		charge.OnEnterEvent = (Action)Delegate.Combine(charge.OnEnterEvent, new Action(this.OnEnterCharge));
		State laser = this.State.Laser;
		laser.OnEnterEvent = (Action)Delegate.Combine(laser.OnEnterEvent, new Action(this.OnEnterLaser));
		State laser2 = this.State.Laser;
		laser2.OnExitEvent = (Action)Delegate.Combine(laser2.OnExitEvent, new Action(this.OnExitLaser));
		State shooting = this.State.Shooting;
		shooting.OnEnterEvent = (Action)Delegate.Combine(shooting.OnEnterEvent, new Action(this.OnEnterShooting));
		State laser3 = this.State.Laser;
		laser3.UpdateStateEvent = (Action)Delegate.Combine(laser3.UpdateStateEvent, new Action(this.UpdateLaserState));
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Charge,
			this.State.Shooting
		});
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Charge, new Func<bool>(this.ShouldCharge), null);
		this.Controller.StateMachine.Configure(this.State.Charge).AddTransition<OnFixedUpdate>(this.State.Laser, () => base.AfterTime(this.Settings.ChargeDuration), null).AddTransition<OnFixedUpdate>(this.State.Idle, () => this.InCloseDistance(), null);
		this.Controller.StateMachine.Configure(this.State.Laser).AddTransition<OnFixedUpdate>(this.State.Shooting, () => base.AfterTime(this.Settings.LaserDuration), null);
		this.Controller.StateMachine.Configure(this.State.Shooting).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.ShootingDuration), null).AddTransition<OnFixedUpdate>(this.State.Idle, () => this.InCloseDistance(), null);
		this.Controller.StateMachine.ChangeState(this.State.Idle);
		this.ProjectileSpawner.Projectile = this.Settings.Projectile;
		this.ProjectileSpawner.Speed = this.Settings.ProjectileSpeed;
		this.Laser.Activated = false;
		this.Laser.gameObject.SetActive(false);
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x000A0919 File Offset: 0x0009EB19
	public void UpdateLaser()
	{
	}

	// Token: 0x17000609 RID: 1545
	// (get) Token: 0x060024CE RID: 9422 RVA: 0x000A091C File Offset: 0x0009EB1C
	public float DesiredLaserRotationDirection
	{
		get
		{
			bool flag = Vector3.Dot(base.PositionToPlayerPosition.normalized, Vector3.Cross(this.m_laserDirection, Vector3.back)) > 0f;
			return (float)((!flag) ? -1 : 1);
		}
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x000A0964 File Offset: 0x0009EB64
	public void UpdateLaserDirection()
	{
		if (this.Controller.NearSein)
		{
			this.m_laserRotationSpeed = Mathf.MoveTowards(this.m_laserRotationSpeed, this.DesiredLaserRotationDirection, Time.deltaTime * 4f);
		}
		float num = this.LaserAngleOverTimeCurve.Evaluate(this.Controller.StateMachine.CurrentStateTime / this.Settings.LaserDuration);
		float num2 = this.Laser.CurrentLaserLength / this.Settings.LaserChaseSpeedDistance;
		float num3 = (!Mathf.Approximately(num2, 0f)) ? (num * this.Settings.LaserChaseSpeed / num2) : 0f;
		float num4 = MoonMath.Angle.AngleFromVector(this.m_laserDirection) + this.m_laserRotationSpeed * Time.deltaTime * num3;
		this.m_laserDirection = MoonMath.Angle.VectorFromAngle(num4);
		this.Laser.transform.eulerAngles = new Vector3(0f, 0f, num4 - 90f);
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x000A0A64 File Offset: 0x0009EC64
	public void ActivateLaser()
	{
		this.UpdateLaserDirection();
		this.UpdateLaser();
		this.Laser.gameObject.SetActive(true);
		this.Laser.Activated = true;
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x000A0A9A File Offset: 0x0009EC9A
	public void DeactivateLaser()
	{
		this.Laser.Activated = false;
	}

	// Token: 0x060024D2 RID: 9426 RVA: 0x000A0AA8 File Offset: 0x0009ECA8
	public void AimLaserAtPlayer()
	{
		Vector3 vector = Characters.Sein.PlatformBehaviour.PlatformMovement.WorldSpeed;
		this.m_laserDirection = base.PositionToPlayerPosition.normalized;
		bool flag = Vector3.Dot(vector.normalized, Vector3.Cross(this.m_laserDirection, Vector3.forward)) > 0f;
		float num = MoonMath.Angle.AngleFromVector(this.m_laserDirection);
		num += (float)((!flag) ? -1 : 1) * this.Settings.LaserAngularOffset;
		this.m_laserDirection = MoonMath.Angle.VectorFromAngle(num);
		this.m_laserRotationSpeed = this.DesiredLaserRotationDirection;
	}

	// Token: 0x060024D3 RID: 9427 RVA: 0x000A0B54 File Offset: 0x0009ED54
	public new void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.Movement.ApplySpringForce(this.Settings.SpringForce, base.StartPosition);
		this.Movement.ApplyDrag(this.Settings.Drag);
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x000A0B9F File Offset: 0x0009ED9F
	public bool ShouldCharge()
	{
		return this.Controller.NearSein && !this.InCloseDistance();
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x000A0BC0 File Offset: 0x0009EDC0
	public bool InCloseDistance()
	{
		return base.PositionToPlayerPosition.magnitude < this.Settings.CloseDistance;
	}

	// Token: 0x04001F16 RID: 7958
	public FloatingRockLaserEnemy.States State = new FloatingRockLaserEnemy.States();

	// Token: 0x04001F17 RID: 7959
	public FloatingRockLaserEnemySettings Settings;

	// Token: 0x04001F18 RID: 7960
	public FloatingRockLaserEnemyAnimations Animations;

	// Token: 0x04001F19 RID: 7961
	public FloatingRockLaserEnemyAnimations AnimationsB;

	// Token: 0x04001F1A RID: 7962
	public FloatingRockLaserEnemyAnimations AnimationsC;

	// Token: 0x04001F1B RID: 7963
	public CharacterAnimationSystem AnimationB;

	// Token: 0x04001F1C RID: 7964
	public CharacterAnimationSystem AnimationC;

	// Token: 0x04001F1D RID: 7965
	public PrefabSpawner ChargingEffect;

	// Token: 0x04001F1E RID: 7966
	public PrefabSpawner ShootingEffect;

	// Token: 0x04001F1F RID: 7967
	public ProjectileSpawner ProjectileSpawner;

	// Token: 0x04001F20 RID: 7968
	public RigidbodyMovement Movement;

	// Token: 0x04001F21 RID: 7969
	public SoundSource ChargingSound;

	// Token: 0x04001F22 RID: 7970
	public SoundSource ShootingSound;

	// Token: 0x04001F23 RID: 7971
	public SoundSource LaserSound;

	// Token: 0x04001F24 RID: 7972
	public SoundSource IdleSound;

	// Token: 0x04001F25 RID: 7973
	public SoundSource LaserHitSound;

	// Token: 0x04001F26 RID: 7974
	public AnimationCurve LaserThicknessCurve;

	// Token: 0x04001F27 RID: 7975
	public AnimationCurve LaserAngleOverTimeCurve;

	// Token: 0x04001F28 RID: 7976
	public BlockableLaser Laser;

	// Token: 0x04001F29 RID: 7977
	public LayerMask LaserLayerMask;

	// Token: 0x04001F2A RID: 7978
	private float m_laserSpeed;

	// Token: 0x04001F2B RID: 7979
	private Vector3 m_laserDirection;

	// Token: 0x04001F2C RID: 7980
	private float m_laserRotationSpeed;

	// Token: 0x04001F2D RID: 7981
	private Vector3 m_laserStartPosition;

	// Token: 0x0200059F RID: 1439
	public class States
	{
		// Token: 0x04001F2E RID: 7982
		public State Idle;

		// Token: 0x04001F2F RID: 7983
		public State Charge;

		// Token: 0x04001F30 RID: 7984
		public State Laser;

		// Token: 0x04001F31 RID: 7985
		public State Shooting;
	}
}
