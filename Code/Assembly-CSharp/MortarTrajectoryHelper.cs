using System;
using Game;
using UnityEngine;

// Token: 0x020005FB RID: 1531
public class MortarTrajectoryHelper
{
	// Token: 0x06002666 RID: 9830 RVA: 0x000A86C3 File Offset: 0x000A68C3
	public MortarTrajectoryHelper(MortarWormEnemy mortarWormEnemy)
	{
		this.m_mortarWormEnemy = mortarWormEnemy;
	}

	// Token: 0x1700061D RID: 1565
	// (get) Token: 0x06002667 RID: 9831 RVA: 0x000A86D2 File Offset: 0x000A68D2
	// (set) Token: 0x06002668 RID: 9832 RVA: 0x000A86DA File Offset: 0x000A68DA
	public float RemainingWaitTime { get; set; }

	// Token: 0x1700061E RID: 1566
	// (get) Token: 0x06002669 RID: 9833 RVA: 0x000A86E3 File Offset: 0x000A68E3
	// (set) Token: 0x0600266A RID: 9834 RVA: 0x000A86EB File Offset: 0x000A68EB
	public bool HitTarget { get; private set; }

	// Token: 0x0600266B RID: 9835 RVA: 0x000A86F4 File Offset: 0x000A68F4
	public void UpdateProjectileSpawner(bool cast)
	{
		Vector3 position = Characters.Sein.Position;
		Vector3 position2 = this.m_mortarWormEnemy.ProjectileSpawner.transform.position;
		Vector2 vector = position - position2;
		float projectileGravity = this.m_mortarWormEnemy.Settings.ProjectileGravity;
		Vector2 v = this.m_mortarWormEnemy.Settings.ProjectileSpeed * vector.normalized;
		float num = vector.magnitude / v.magnitude;
		if (Vector3.Dot(this.m_mortarWormEnemy.transform.up, Vector3.up) > Mathf.Cos(0.7853982f))
		{
			num += 0.2f;
			v = v.normalized * vector.magnitude / num;
		}
		v.y += projectileGravity * num * 0.5f;
		if (Mathf.Abs(position.x - this.m_mortarWormEnemy.Position.x) < 2f)
		{
			this.HitTarget = false;
			return;
		}
		RaycastHit raycastHit;
		if (!cast || PhysicsHelper.ArcSphereCast(projectileGravity, 0.5f, position2, v, num, this.m_mortarWormEnemy.RayTestLayerMask, Characters.Sein.gameObject, out raycastHit))
		{
			this.m_mortarWormEnemy.ProjectileSpawner.Direction = v.normalized;
			this.m_mortarWormEnemy.ProjectileSpawner.Speed = v.magnitude;
			this.m_mortarWormEnemy.ProjectileSpawner.Gravity = projectileGravity;
			this.HitTarget = true;
		}
		else
		{
			this.HitTarget = false;
		}
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x000A8898 File Offset: 0x000A6A98
	public void UpdateMortarTrajectory()
	{
		Vector3 localShootDirection = this.m_mortarWormEnemy.LocalShootDirection;
		Vector3 position = this.m_mortarWormEnemy.ProjectileSpawner.transform.position;
		float speed = this.m_mortarWormEnemy.ProjectileSpawner.Speed;
		Vector3 direction = this.m_mortarWormEnemy.ProjectileSpawner.Direction;
		this.UpdateProjectileSpawner(true);
		if (this.HitTarget)
		{
			this.m_mortarWormEnemy.LocalShootDirection = this.m_mortarWormEnemy.transform.InverseTransformDirection(this.m_mortarWormEnemy.ProjectileSpawner.Direction);
			if (this.m_mortarWormEnemy.LocalShootDirection.y < 0f)
			{
				this.m_mortarWormEnemy.LocalShootDirection.y = 0f;
			}
			this.m_mortarWormEnemy.ProjectileSpawner.Direction = this.m_mortarWormEnemy.transform.TransformDirection(this.m_mortarWormEnemy.LocalShootDirection);
			if (this.m_mortarWormEnemy.FaceLeft)
			{
				MortarWormEnemy mortarWormEnemy = this.m_mortarWormEnemy;
				mortarWormEnemy.LocalShootDirection.x = mortarWormEnemy.LocalShootDirection.x * -1f;
			}
			this.m_mortarWormEnemy.ProjectileSpawner.transform.position = this.m_mortarWormEnemy.ProjectileTrajectorySpawnPoint.position;
		}
		if (!this.HitTarget)
		{
			this.m_mortarWormEnemy.LocalShootDirection = localShootDirection;
			this.m_mortarWormEnemy.ProjectileSpawner.transform.position = position;
			this.m_mortarWormEnemy.ProjectileSpawner.Speed = speed;
			this.m_mortarWormEnemy.ProjectileSpawner.Direction = direction;
		}
	}

	// Token: 0x040020FE RID: 8446
	private readonly MortarWormEnemy m_mortarWormEnemy;
}
