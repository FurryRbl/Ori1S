using System;
using UnityEngine;

// Token: 0x02000607 RID: 1543
public class WormMortarShootingState : WormState
{
	// Token: 0x06002685 RID: 9861 RVA: 0x000A8EF0 File Offset: 0x000A70F0
	public WormMortarShootingState(WormEnemy worm, MortarWormDirectionalAnimations shoot, PrefabSpawner shootEffect, SoundSource shootSound, ProjectileSpawner projectileSpawner, float shootDelay, float projectileDamage) : base(worm)
	{
		this.m_shoot = shoot;
		this.m_shootEffect = shootEffect;
		this.m_shootSound = shootSound;
		this.m_projectileSpawner = projectileSpawner;
		this.m_shootDelay = shootDelay;
		this.m_projectileDamage = projectileDamage;
	}

	// Token: 0x06002686 RID: 9862 RVA: 0x000A8F28 File Offset: 0x000A7128
	public override void OnEnter()
	{
		MortarWormEnemy mortarWormEnemy = (MortarWormEnemy)this.Worm;
		Vector3 direction = (this.m_projectileSpawner.Speed * this.m_projectileSpawner.Direction + 0.5f * this.m_projectileSpawner.Gravity * this.m_shootDelay * this.m_shootDelay * Vector3.down).normalized;
		direction = mortarWormEnemy.transform.InverseTransformDirection(direction);
		if (mortarWormEnemy.FaceLeft)
		{
			direction.x *= -1f;
		}
		this.Worm.Animation.Play(this.m_shoot.PickWithDirection(direction), 0, null);
		this.m_projectileAnimationPosition = mortarWormEnemy.Spawn.FindPosition(direction);
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x000A8FEF File Offset: 0x000A71EF
	public override void OnExit()
	{
		this.m_hasShot = false;
		base.OnExit();
	}

	// Token: 0x06002688 RID: 9864 RVA: 0x000A9000 File Offset: 0x000A7200
	public override void UpdateState()
	{
		if (base.CurrentStateTime >= this.m_shootDelay && !this.m_hasShot)
		{
			this.m_hasShot = true;
			if (this.m_shootEffect)
			{
				this.m_shootEffect.Spawn(null);
			}
			if (this.m_shootSound)
			{
				this.m_shootSound.Play();
			}
			Projectile projectile = this.m_projectileSpawner.SpawnProjectile();
			Vector3 b = projectile.Direction * projectile.Speed * this.m_shootDelay + Vector3.down * projectile.Gravity * this.m_shootDelay * this.m_shootDelay * 0.5f;
			projectile.Position += b;
			projectile.SpeedVector += Vector3.down * projectile.Gravity * this.m_shootDelay;
			projectile.GetComponent<DamageDealer>().Damage = this.m_projectileDamage;
			Vector3 vector = this.m_projectileAnimationPosition - projectile.Position;
			vector.z = 0f;
			projectile.Position += vector;
			projectile.Displacement = vector;
		}
		base.UpdateState();
	}

	// Token: 0x0400212D RID: 8493
	private readonly MortarWormDirectionalAnimations m_shoot;

	// Token: 0x0400212E RID: 8494
	private readonly PrefabSpawner m_shootEffect;

	// Token: 0x0400212F RID: 8495
	private readonly SoundSource m_shootSound;

	// Token: 0x04002130 RID: 8496
	private readonly ProjectileSpawner m_projectileSpawner;

	// Token: 0x04002131 RID: 8497
	private readonly float m_shootDelay;

	// Token: 0x04002132 RID: 8498
	private readonly float m_projectileDamage;

	// Token: 0x04002133 RID: 8499
	private Vector3 m_projectileAnimationPosition;

	// Token: 0x04002134 RID: 8500
	private bool m_hasShot;
}
