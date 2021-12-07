using System;
using UnityEngine;

// Token: 0x020004FF RID: 1279
public class DashOwlDashAlertState : DashOwlState
{
	// Token: 0x06002273 RID: 8819 RVA: 0x00096E20 File Offset: 0x00095020
	public DashOwlDashAlertState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x00096E2C File Offset: 0x0009502C
	public override void OnEnter()
	{
		this.DashOwl.Animation.Play(this.DashOwl.Animations.DashAlert, 0, null);
		this.DashOwl.FlyMovement.Velocity = Vector2.zero;
		this.DashOwl.DashAlertSound.Play();
		this.DashOwl.SpriteRotation.RotateTowardsTarget(this.DashOwl.PositionToPlayerPosition, this.DashOwl.FaceLeft);
	}

	// Token: 0x06002275 RID: 8821 RVA: 0x00096EA7 File Offset: 0x000950A7
	public override void UpdateState()
	{
		this.DashOwl.FacePlayer();
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x00096EB4 File Offset: 0x000950B4
	public override void OnExit()
	{
		this.DashOwl.SpriteRotation.RotateBackToNormal();
	}
}
