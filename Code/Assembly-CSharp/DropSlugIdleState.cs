using System;

// Token: 0x02000516 RID: 1302
public class DropSlugIdleState : DropSlugState
{
	// Token: 0x060022BF RID: 8895 RVA: 0x00098449 File Offset: 0x00096649
	public DropSlugIdleState(DropSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x060022C0 RID: 8896 RVA: 0x00098452 File Offset: 0x00096652
	public void SetSettings()
	{
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x00098454 File Offset: 0x00096654
	public override void OnEnter()
	{
		this.Slug.Animation.PlayLoop(this.Slug.Animations.Idle, 0, null, false);
		this.Slug.Idle.Play();
		base.OnEnter();
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x0009849B File Offset: 0x0009669B
	public override void OnExit()
	{
		this.Slug.Idle.Stop();
		base.OnExit();
	}
}
