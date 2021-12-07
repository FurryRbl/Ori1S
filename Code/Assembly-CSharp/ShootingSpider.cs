using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x020005D5 RID: 1493
public class ShootingSpider : Enemy
{
	// Token: 0x0600257B RID: 9595 RVA: 0x000A38A8 File Offset: 0x000A1AA8
	public new void Awake()
	{
		base.Awake();
		this.State.Respawn = new State();
		this.State.Idle = new State();
		this.State.Charging = new State();
		this.State.Shooting = new State();
		this.State.Frozen = new State();
		this.State.Hurt = new State();
		this.State.Respawn.OnEnterEvent = delegate()
		{
			base.PlayAnimationOnce(this.Animations.Respawn, 0);
			base.FacePlayer();
			base.SpawnPrefab(this.Settings.RespawnEffect);
		};
		this.State.Hurt.OnEnterEvent = delegate()
		{
			base.PlaySound(this.Sounds.Hurt);
			base.PlayAnimationOnce(this.Animations.Hurt, 0);
			base.FacePlayer();
		};
		this.State.Idle.OnEnterEvent = delegate()
		{
			base.RestartAnimationLoop(this.Animations.Idle, 0);
			base.PlaySound(this.Sounds.Idle);
		};
		this.State.Idle.OnExitEvent = delegate()
		{
			base.StopSound(this.Sounds.Idle);
		};
		this.State.Charging.OnEnterEvent = delegate()
		{
			base.RestartAnimationLoop(this.Animations.Charging, 0);
			base.PlaySound(this.Sounds.Charge);
			base.SpawnPrefab(this.ChargingEffect);
		};
		this.State.Charging.UpdateStateEvent = delegate()
		{
			base.FacePlayer();
		};
		this.State.Charging.OnExitEvent = delegate()
		{
			base.DestroyPrefab(this.ChargingEffect);
		};
		this.State.Charging.UpdateStateEvent = delegate()
		{
			base.FacePlayer();
		};
		this.State.Shooting.OnEnterEvent = delegate()
		{
			base.PlayAnimationOnce(this.Animations.Shooting, 0);
			base.PlaySound(this.Sounds.Shoot);
			base.SpawnPrefab(this.ShootingEffect);
			this.ShootProjectileAtPlayer();
		};
		this.Controller.StateMachine.Configure(this.State.Idle).AddTransition<OnFixedUpdate>(this.State.Charging, new Func<bool>(this.ShouldCharge), null).AddTransition<OnReceiveDamage>(this.State.Hurt, null, null);
		this.Controller.StateMachine.Configure(this.State.Charging).AddTransition<OnFixedUpdate>(this.State.Shooting, () => base.AfterTime(this.Settings.ChargingDuration), null);
		this.Controller.StateMachine.Configure(this.State.Shooting).AddTransition<OnFixedUpdate>(this.State.Idle, () => base.AfterTime(this.Settings.ShootingDuration), null).AddTransition<OnReceiveDamage>(this.State.Hurt, null, null);
		this.Controller.StateMachine.Configure(this.State.Hurt).AddTransition<OnAnimationEnded>(this.State.Idle, null, null);
		this.Controller.StateMachine.Configure(this.State.Respawn).AddTransition<OnAnimationEnded>(this.State.Idle, null, null);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Idle,
			this.State.Charging,
			this.State.Shooting,
			this.State.Frozen,
			this.State.Hurt,
			this.State.Respawn
		});
		this.Controller.StateMachine.ChangeState(this.State.Idle);
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x000A3BC7 File Offset: 0x000A1DC7
	public new void OnTimedRespawn()
	{
		this.Controller.StateMachine.ChangeState(this.State.Respawn);
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x000A3BE4 File Offset: 0x000A1DE4
	public new void FixedUpdate()
	{
		if (!this.IsSuspended)
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.AddForceSafe(Vector3.down * this.Settings.Gravity, ForceMode.Acceleration);
		}
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x000A3C24 File Offset: 0x000A1E24
	public bool ShouldCharge()
	{
		return this.Controller.NearSein && base.PositionToPlayerPosition.magnitude < this.Settings.ChargingRange;
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x000A3C60 File Offset: 0x000A1E60
	public void ShootProjectileAtPlayer()
	{
		Vector3 position = this.ProjectileSpawner.position;
		Vector3 normalized = (base.PlayerPosition - position).normalized;
		float projectileSpeed = this.Settings.ProjectileSpeed;
		if (this.Settings.SpreadShot)
		{
			float num = MoonMath.Angle.AngleFromVector(normalized);
			Vector2 v = MoonMath.Angle.VectorFromAngle(num + 20f);
			Vector2 v2 = MoonMath.Angle.VectorFromAngle(num - 20f);
			this.ShootProjectile(position, normalized, projectileSpeed);
			this.ShootProjectile(position, v, projectileSpeed * 0.8f);
			this.ShootProjectile(position, v2, projectileSpeed * 0.8f);
		}
		else
		{
			this.ShootProjectile(position, normalized, projectileSpeed);
		}
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.AddForceSafe(normalized * -this.Settings.ShootingImpulse, ForceMode.Force);
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x000A3D40 File Offset: 0x000A1F40
	public void ShootProjectile(Vector3 position, Vector3 direction, float speed)
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Settings.Projectile, position, Quaternion.identity);
		Projectile component = gameObject.GetComponent<Projectile>();
		component.Direction = direction;
		component.Speed = speed;
		component.GetComponent<DamageDealer>().Damage = this.Settings.ProjectileDamage;
		component.Owner = this.DamageReciever.gameObject;
	}

	// Token: 0x04002021 RID: 8225
	public ShootingSpiderAnimations Animations;

	// Token: 0x04002022 RID: 8226
	public ShootingSpiderSounds Sounds;

	// Token: 0x04002023 RID: 8227
	public PrefabSpawner ChargingEffect;

	// Token: 0x04002024 RID: 8228
	public GameObject IceBlock;

	// Token: 0x04002025 RID: 8229
	public Transform ProjectileSpawner;

	// Token: 0x04002026 RID: 8230
	public ShootingSpiderSettings Settings;

	// Token: 0x04002027 RID: 8231
	public PrefabSpawner ShootingEffect;

	// Token: 0x04002028 RID: 8232
	public bool ShootThree;

	// Token: 0x04002029 RID: 8233
	public ShootingSpider.States State = new ShootingSpider.States();

	// Token: 0x020005D6 RID: 1494
	public class States
	{
		// Token: 0x0400202A RID: 8234
		public State Respawn;

		// Token: 0x0400202B RID: 8235
		public State Charging;

		// Token: 0x0400202C RID: 8236
		public State Frozen;

		// Token: 0x0400202D RID: 8237
		public State Idle;

		// Token: 0x0400202E RID: 8238
		public State Shooting;

		// Token: 0x0400202F RID: 8239
		public State Hurt;
	}
}
