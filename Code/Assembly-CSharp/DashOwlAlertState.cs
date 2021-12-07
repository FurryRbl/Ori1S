using System;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
public class DashOwlAlertState : DashOwlState
{
	// Token: 0x0600221B RID: 8731 RVA: 0x00095DB4 File Offset: 0x00093FB4
	public DashOwlAlertState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x00095DBD File Offset: 0x00093FBD
	public override void OnEnter()
	{
		this.DashOwl.PlayAnimationOnce(this.DashOwl.Animations.Alert, 0);
		this.DashOwl.FlyMovement.Velocity = Vector2.zero;
		base.OnEnter();
	}
}
