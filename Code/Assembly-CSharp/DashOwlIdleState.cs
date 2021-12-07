using System;
using UnityEngine;

// Token: 0x02000502 RID: 1282
public class DashOwlIdleState : DashOwlState
{
	// Token: 0x0600227C RID: 8828 RVA: 0x00097042 File Offset: 0x00095242
	public DashOwlIdleState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x0600227D RID: 8829 RVA: 0x0009704C File Offset: 0x0009524C
	public override void OnEnter()
	{
		this.DashOwl.FlyMovement.Velocity = Vector2.zero;
		if (this.DashOwl.Settings.Perched)
		{
			this.DashOwl.PlayAnimationLoop(this.DashOwl.Animations.Idle, 0);
		}
		else
		{
			this.DashOwl.PlayAnimationLoop(this.DashOwl.Animations.FlyHome, 0);
		}
	}
}
