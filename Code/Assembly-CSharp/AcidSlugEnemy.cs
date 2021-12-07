using System;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
public class AcidSlugEnemy : SlugEnemy
{
	// Token: 0x06002535 RID: 9525 RVA: 0x000A270C File Offset: 0x000A090C
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

	// Token: 0x06002536 RID: 9526 RVA: 0x000A279C File Offset: 0x000A099C
	public new void Awake()
	{
		base.Awake();
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x000A27A4 File Offset: 0x000A09A4
	public new void Start()
	{
		base.Start();
		this.State.Crawl = new AcidSlugCrawlingState(this, this.CrawlingSoundSource, this.MovingSoundSource);
		this.State.Charging = new AcidSlugChargingState(this);
		this.State.Shooting = new AcidSlugShootingState(this);
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

	// Token: 0x06002538 RID: 9528 RVA: 0x000A292C File Offset: 0x000A0B2C
	public void DropAcid(float speed, Vector3 direction)
	{
		GameObject gameObject = InstantiateUtility.Instantiate(this.Settings.AcidDrip, base.transform.position, base.transform.rotation) as GameObject;
		Projectile component = gameObject.GetComponent<Projectile>();
		component.Direction = direction;
		component.Speed = speed;
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x000A297C File Offset: 0x000A0B7C
	public void OnShoot()
	{
		if (this.Settings.ShootEffect)
		{
			InstantiateUtility.Instantiate(this.Settings.ShootEffect, base.transform.position, base.transform.rotation);
		}
		float num = MoonMath.Angle.AngleFromVector(Vector3.Lerp(this.Movement.Up, (base.PlayerPosition - base.Position).normalized, 0.5f));
		for (int i = -45; i <= 45; i += 45)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Settings.AcidDrip, base.Position, Quaternion.identity);
			Projectile component = gameObject.GetComponent<Projectile>();
			component.Direction = MoonMath.Angle.VectorFromAngle(num + (float)i);
			component.Speed = this.Settings.AcidProjectileSpeed;
		}
	}

	// Token: 0x04001FBE RID: 8126
	public SoundSource CrawlingSoundSource;

	// Token: 0x04001FBF RID: 8127
	public SoundSource ChargingSoundSource;

	// Token: 0x04001FC0 RID: 8128
	public SoundSource ShootingSoundSource;

	// Token: 0x04001FC1 RID: 8129
	public SoundSource MovingSoundSource;

	// Token: 0x04001FC2 RID: 8130
	public AcidSlugEnemyAnimations Animations;

	// Token: 0x04001FC3 RID: 8131
	public AcidSlugEnemySettings Settings;

	// Token: 0x04001FC4 RID: 8132
	public AcidSlugEnemy.States State = new AcidSlugEnemy.States();

	// Token: 0x020005BB RID: 1467
	public class States
	{
		// Token: 0x04001FD5 RID: 8149
		public AcidSlugCrawlingState Crawl;

		// Token: 0x04001FD6 RID: 8150
		public AcidSlugChargingState Charging;

		// Token: 0x04001FD7 RID: 8151
		public AcidSlugShootingState Shooting;
	}
}
