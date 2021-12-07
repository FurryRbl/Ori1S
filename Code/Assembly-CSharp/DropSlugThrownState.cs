using System;
using UnityEngine;

// Token: 0x02000519 RID: 1305
public class DropSlugThrownState : DropSlugState
{
	// Token: 0x060022C8 RID: 8904 RVA: 0x0009866D File Offset: 0x0009686D
	public DropSlugThrownState(DropSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x00098676 File Offset: 0x00096876
	public override void OnEnter()
	{
		this.Slug.RestartAnimationLoop(this.Slug.Animations.Thrown, 0);
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x00098694 File Offset: 0x00096894
	public override void UpdateState()
	{
		this.Slug.FlyMovement.VelocityY -= this.Slug.Settings.ThrownGravity * Time.deltaTime;
	}
}
