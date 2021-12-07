using System;
using UnityEngine;

// Token: 0x02000517 RID: 1303
public class DropSlugFallState : DropSlugState
{
	// Token: 0x060022C3 RID: 8899 RVA: 0x000984B3 File Offset: 0x000966B3
	public DropSlugFallState(DropSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x000984BC File Offset: 0x000966BC
	public override void OnEnter()
	{
		this.Slug.PlayAnimationLoop(this.Slug.Animations.Fall, 0);
		this.Slug.PlaySound(this.Slug.DropDown);
		base.OnEnter();
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x00098504 File Offset: 0x00096704
	public override void UpdateState()
	{
		this.Slug.FlyMovement.VelocityY = -1f * this.Slug.Settings.Speed * this.Slug.Settings.SpeedMultiplierOverTime.Evaluate(base.CurrentStateTime);
		this.Slug.FlyMovement.VelocityX += (float)((!this.Slug.PlayerIsToLeft) ? 1 : -1) * this.Slug.Settings.HorizontalAcceleration * Time.deltaTime;
		this.Slug.FlyMovement.VelocityX = Mathf.Clamp(this.Slug.FlyMovement.VelocityX, -this.Slug.Settings.HorizontalMaxSpeed, this.Slug.Settings.HorizontalMaxSpeed);
		base.UpdateState();
	}
}
