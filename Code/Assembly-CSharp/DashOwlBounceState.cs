using System;
using fsm.triggers;
using UnityEngine;

// Token: 0x020004FA RID: 1274
public class DashOwlBounceState : DashOwlState
{
	// Token: 0x06002263 RID: 8803 RVA: 0x00096A14 File Offset: 0x00094C14
	public DashOwlBounceState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x06002264 RID: 8804 RVA: 0x00096A1D File Offset: 0x00094C1D
	public override void UpdateState()
	{
		this.DashOwl.FlyMovement.Velocity = this.m_direction * this.DashOwl.Settings.BounceCurve.Evaluate(base.CurrentStateTime);
		base.UpdateState();
	}

	// Token: 0x06002265 RID: 8805 RVA: 0x00096A5C File Offset: 0x00094C5C
	public override void OnEnter()
	{
		OnCollisionEnter onCollisionEnter = (OnCollisionEnter)this.DashOwl.Controller.StateMachine.CurrentTrigger;
		this.DashOwl.FlyMovement.Velocity += this.DashOwl.FlyMovement.Kickback.KickbackVector;
		this.DashOwl.FlyMovement.Kickback.Stop();
		Vector2 vector = PhysicsHelper.CalculateAverageNormalFromContactPoints(onCollisionEnter.Collision.contacts);
		vector.Normalize();
		float num = Mathf.Cos(0.7853982f);
		Vector2 v = Vector3.zero;
		if (Vector2.Dot(vector, Vector3.down) > num)
		{
			this.DashOwl.FacePlayer();
			this.DashOwl.PlayAnimationOnce(this.DashOwl.Animations.HitCeiling, 1);
			Vector3 v2 = Vector3.Cross(this.DashOwl.FaceLeft ? Vector3.back : Vector3.forward, vector);
			v = v2;
			this.DashOwl.PlaySound(this.DashOwl.HitWallSound);
		}
		else if (Vector2.Dot(vector, Vector3.up) > num)
		{
			this.DashOwl.FacePlayer();
			this.DashOwl.PlayAnimationOnce(this.DashOwl.Animations.HitGround, 1);
			Vector3 v3 = Vector3.Cross((!this.DashOwl.FaceLeft) ? Vector3.back : Vector3.forward, vector);
			v = v3;
			this.DashOwl.PlaySound(this.DashOwl.HitWallSound);
		}
		else
		{
			this.DashOwl.FaceLeft = (vector.x < 0f);
			this.DashOwl.PlayAnimationOnce(this.DashOwl.Animations.HitWall, 1);
			v = vector;
			this.DashOwl.PlaySound(this.DashOwl.HitWallSound);
		}
		Vector2 normalized = this.DashOwl.FlyMovement.Velocity.normalized;
		this.m_direction = -2f * Vector2.Dot(normalized, vector) * vector + normalized;
		this.DashOwl.SpriteRotation.RotateTowardsTarget(v, this.DashOwl.FaceLeft);
		this.DashOwl.SpriteRotation.RotateToTargetImmediately();
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x00096CCA File Offset: 0x00094ECA
	public override void OnExit()
	{
		this.DashOwl.SpriteRotation.RotateBackToNormal();
	}

	// Token: 0x04001CCE RID: 7374
	private Vector2 m_direction;
}
