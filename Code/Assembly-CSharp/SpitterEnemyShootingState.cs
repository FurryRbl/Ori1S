using System;
using UnityEngine;

// Token: 0x020005E2 RID: 1506
public class SpitterEnemyShootingState : SpitterEnemyState
{
	// Token: 0x060025F2 RID: 9714 RVA: 0x000A67B1 File Offset: 0x000A49B1
	public SpitterEnemyShootingState(SpitterEnemy enemy) : base(enemy)
	{
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x000A67BA File Offset: 0x000A49BA
	public override void UpdateState()
	{
	}

	// Token: 0x060025F4 RID: 9716 RVA: 0x000A67BC File Offset: 0x000A49BC
	public override void OnEnter()
	{
		this.ShootProjectile();
	}

	// Token: 0x060025F5 RID: 9717 RVA: 0x000A67C4 File Offset: 0x000A49C4
	public void ShootProjectile()
	{
		this.GroundEnemy.PlayAnimationOnce(this.SpitterEnemy.Animations.Shooting, 0);
		this.GroundEnemy.PlatformMovement.LocalSpeedX = 0f;
		float projectileSpeed = this.SpitterEnemy.Settings.ProjectileSpeed;
		float projectileGravity = this.SpitterEnemy.Settings.ProjectileGravity;
		Vector3 projectileSpawnerPositionToPlayerPosition = this.ProjectileSpawnerPositionToPlayerPosition;
		projectileSpawnerPositionToPlayerPosition.x = ((!this.GroundEnemy.FaceLeft) ? Mathf.Max(1f, projectileSpawnerPositionToPlayerPosition.x) : Mathf.Min(-1f, projectileSpawnerPositionToPlayerPosition.x));
		float num = projectileSpawnerPositionToPlayerPosition.magnitude / projectileSpeed;
		Vector3 vector = projectileSpawnerPositionToPlayerPosition.normalized * projectileSpeed;
		vector.y += projectileGravity * num * 0.5f;
		this.SpawnProjectile(vector);
		if (this.SpitterEnemy.Settings.SpreadShot)
		{
			this.SpawnProjectile(vector + Vector3.right * 4f + Vector3.down * 2f);
			this.SpawnProjectile(vector + Vector3.left * 4f + Vector3.down * 2f);
		}
	}

	// Token: 0x17000616 RID: 1558
	// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000A6918 File Offset: 0x000A4B18
	public Vector3 ProjectileSpawnerPositionToPlayerPosition
	{
		get
		{
			return this.SpitterEnemy.PlayerPosition - this.SpitterEnemy.ProjectileSpawner.transform.position;
		}
	}

	// Token: 0x060025F7 RID: 9719 RVA: 0x000A694C File Offset: 0x000A4B4C
	public void SpawnProjectile(Vector3 speed)
	{
		float projectileGravity = this.SpitterEnemy.Settings.ProjectileGravity;
		float projectileDamage = this.SpitterEnemy.Settings.ProjectileDamage;
		this.SpitterEnemy.SpawnPrefab(this.SpitterEnemy.SpitEffect);
		GameObject gameObject = this.SpitterEnemy.ProjectileSpawner.Spawn(null);
		if (gameObject != null)
		{
			Vector3 positionToPlayerPosition = this.GroundEnemy.PositionToPlayerPosition;
			positionToPlayerPosition.x = ((!this.GroundEnemy.FaceLeft) ? Mathf.Max(1.5f, positionToPlayerPosition.x) : Mathf.Min(-1.5f, positionToPlayerPosition.x));
			Projectile component = gameObject.GetComponent<Projectile>();
			component.Gravity = projectileGravity;
			component.Speed = speed.magnitude;
			component.Direction = speed.normalized;
			component.GetComponentInChildren<DamageDealer>().Damage = projectileDamage;
			component.Owner = this.GroundEnemy.DamageReciever.gameObject;
		}
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x000A6A49 File Offset: 0x000A4C49
	public override void OnExit()
	{
	}
}
