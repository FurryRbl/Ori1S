using System;
using UnityEngine;

// Token: 0x020004DA RID: 1242
public class EntityPlatformingMovement : PlatformingMovement, IDamageReciever
{
	// Token: 0x06002195 RID: 8597 RVA: 0x0009332E File Offset: 0x0009152E
	public void ApplyKickback(float kickbackMultiplier)
	{
		this.Kickback.ApplyKickback(kickbackMultiplier);
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x0009333C File Offset: 0x0009153C
	public void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.StompBlast || damage.Type == DamageType.Bash)
		{
			return;
		}
		float kickbackMultiplier = Vector3.Dot(Vector3.right, damage.Force);
		this.Kickback.ApplyKickback(kickbackMultiplier);
	}

	// Token: 0x06002197 RID: 8599 RVA: 0x00093386 File Offset: 0x00091586
	public new void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
		this.MovingGroundCollision(collision);
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x00093396 File Offset: 0x00091596
	public new void OnCollisionStay(Collision collision)
	{
		base.OnCollisionStay(collision);
		this.MovingGroundCollision(collision);
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x000933A8 File Offset: 0x000915A8
	public void MovingGroundCollision(Collision collision)
	{
		if (Vector3.Dot(PhysicsHelper.CalculateAverageNormalFromContactPoints(collision.contacts), Vector3.up) > Mathf.Cos(0.7853982f))
		{
			this.m_movingGround.SetGround(collision.transform);
		}
	}

	// Token: 0x0600219A RID: 8602 RVA: 0x000933EC File Offset: 0x000915EC
	public new void FixedUpdate()
	{
		base.transform.position += this.m_movingGround.CalculateDelta(base.transform);
		this.Kickback.AdvanceTime();
		if (EnemyStopper.InsideEnemyStopper(this.Position))
		{
			this.Kickback.Stop();
		}
		base.LocalSpeedX += this.Kickback.CurrentKickbackSpeed;
		base.FixedUpdate();
		base.LocalSpeedX -= this.Kickback.CurrentKickbackSpeed;
		this.m_movingGround.Update();
	}

	// Token: 0x04001C45 RID: 7237
	public Kickback Kickback = new Kickback();

	// Token: 0x04001C46 RID: 7238
	private readonly MovingGroundHelper m_movingGround = new MovingGroundHelper();
}
