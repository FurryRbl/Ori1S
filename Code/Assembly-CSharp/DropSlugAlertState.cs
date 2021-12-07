using System;

// Token: 0x02000511 RID: 1297
public class DropSlugAlertState : DropSlugState
{
	// Token: 0x060022A8 RID: 8872 RVA: 0x00097E1C File Offset: 0x0009601C
	public DropSlugAlertState(DropSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x060022A9 RID: 8873 RVA: 0x00097E28 File Offset: 0x00096028
	public override void OnEnter()
	{
		this.Slug.PlayAnimationLoop(this.Slug.Animations.Alert, 0);
		this.Slug.PlaySound(this.Slug.Alert);
		base.OnEnter();
	}

	// Token: 0x060022AA RID: 8874 RVA: 0x00097E6D File Offset: 0x0009606D
	public override void OnExit()
	{
		this.Slug.StopSound(this.Slug.Alert);
		base.OnExit();
	}
}
