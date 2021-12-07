using System;
using UnityEngine;

// Token: 0x020004F8 RID: 1272
public class DashOwlBashedState : DashOwlState
{
	// Token: 0x06002256 RID: 8790 RVA: 0x00096892 File Offset: 0x00094A92
	public DashOwlBashedState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x06002257 RID: 8791 RVA: 0x0009689C File Offset: 0x00094A9C
	public override void OnEnter()
	{
		this.DashOwl.FlyMovement.Velocity = Vector3.zero;
		this.DashOwl.PlayAnimationOnce(this.DashOwl.Animations.Bashed, 0);
		base.OnEnter();
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x000968E5 File Offset: 0x00094AE5
	public override void OnExit()
	{
		this.DashOwl.SpriteRotation.RotateBackToNormal();
	}
}
