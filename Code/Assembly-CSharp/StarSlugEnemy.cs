using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x020005C5 RID: 1477
public class StarSlugEnemy : SlugEnemy
{
	// Token: 0x06002558 RID: 9560 RVA: 0x000A2F70 File Offset: 0x000A1170
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		bool flag;
		if (EnemyStopper.InsideEnemyStopper(base.Position, (!base.FaceLeft) ? this.Movement.Right : this.Movement.Left, out flag))
		{
			base.FaceLeft = !base.FaceLeft;
		}
		if (EnemyStopper.InsideEnemyStopper(base.Position))
		{
			this.Movement.Kickback.Stop();
		}
		if (flag)
		{
			this.Movement.Kickback.Stop();
		}
	}

	// Token: 0x06002559 RID: 9561 RVA: 0x000A3000 File Offset: 0x000A1200
	public new void Awake()
	{
		base.Awake();
		this.DamageReciever.OnDeathEvent.Add(new Action<Damage>(this.OnDeath));
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x000A3030 File Offset: 0x000A1230
	public void OnDeath(Damage damage)
	{
		float num = MoonMath.Angle.AngleFromVector(this.Movement.Up);
		for (int i = -20; i <= 20; i += 40)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Settings.Projectile, base.Position, Quaternion.identity);
			Projectile component = gameObject.GetComponent<Projectile>();
			component.Direction = MoonMath.Angle.VectorFromAngle(num + (float)i);
			component.Speed = this.Settings.ProjectileSpeed;
		}
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x000A30B8 File Offset: 0x000A12B8
	public new void Start()
	{
		base.Start();
		this.State.Crawl = new StarSlugCrawlingState(this);
		this.State.Charging = new StarSlugChargingState(this);
		this.State.Shooting = new StarSlugShootingState(this);
		this.Controller.StateMachine.Configure(this.State.Crawl).AddTransition<OnFixedUpdate>(this.State.Charging, () => base.AfterTime(0.5f) && this.Controller.IsNearSein(), null).AddTransition<OnReceiveDamage>(this.State.Charging, null, null);
		this.Controller.StateMachine.Configure(this.State.Charging).AddTransition<OnAnimationEnded>(this.State.Shooting, () => true, new Action(this.OnShoot));
		this.Controller.StateMachine.Configure(this.State.Shooting).AddTransition<OnFixedUpdate>(this.State.Crawl, () => base.AfterTime(0.5f), null);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Crawl,
			this.State.Charging,
			this.State.Shooting
		});
		this.Controller.StateMachine.ChangeState(this.State.Crawl);
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x000A3234 File Offset: 0x000A1434
	public void OnShoot()
	{
		if (this.Settings.ShootEffect)
		{
			InstantiateUtility.Instantiate(this.Settings.ShootEffect, base.transform.position, base.transform.rotation);
		}
		float num = MoonMath.Angle.AngleFromVector(Vector3.Lerp(this.Movement.Up, (base.PlayerPosition - base.Position).normalized, 0.5f));
		for (int i = -45; i <= 45; i += 45)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Settings.Projectile, base.Position, Quaternion.identity);
			Projectile component = gameObject.GetComponent<Projectile>();
			component.Direction = MoonMath.Angle.VectorFromAngle(num + (float)i);
			component.Speed = this.Settings.ProjectileSpeed;
		}
	}

	// Token: 0x04001FE7 RID: 8167
	public SoundSource ChargingSoundSource;

	// Token: 0x04001FE8 RID: 8168
	public SoundSource ShootingSoundSource;

	// Token: 0x04001FE9 RID: 8169
	public SoundSource CrawlingSoundSource;

	// Token: 0x04001FEA RID: 8170
	public SurfaceBasedSoundProvider CrawlingSoundProvider;

	// Token: 0x04001FEB RID: 8171
	public StarSlugEnemyAnimations Animations;

	// Token: 0x04001FEC RID: 8172
	public StarSlugEnemySettings Settings;

	// Token: 0x04001FED RID: 8173
	public GameObject AttackSphere;

	// Token: 0x04001FEE RID: 8174
	public StarSlugEnemy.States State = new StarSlugEnemy.States();

	// Token: 0x020005C6 RID: 1478
	public class States
	{
		// Token: 0x04001FF0 RID: 8176
		public StarSlugCrawlingState Crawl;

		// Token: 0x04001FF1 RID: 8177
		public StarSlugChargingState Charging;

		// Token: 0x04001FF2 RID: 8178
		public StarSlugShootingState Shooting;
	}
}
