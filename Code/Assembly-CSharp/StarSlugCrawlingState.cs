using System;

// Token: 0x020005C7 RID: 1479
public class StarSlugCrawlingState : StarSlugState
{
	// Token: 0x06002561 RID: 9569 RVA: 0x000A3351 File Offset: 0x000A1551
	public StarSlugCrawlingState(StarSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x000A335C File Offset: 0x000A155C
	public override void UpdateState()
	{
		StarSlugEnemySettings settings = this.StarSlugEnemy.Settings;
		this.Slug.Movement.Speed = (float)((!this.Slug.SpriteMirror.FaceLeft) ? 1 : -1) * settings.WalkSpeed * settings.WalkSpeedMultiplier.Evaluate(this.Slug.SpriteAnimator.CurrentAnimationTime);
		this.Slug.Animation.PlayLoop(this.StarSlugEnemy.Animations.Crawling.GetAnimation(this.StarSlugEnemy.BendValue), 0, null, true);
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x000A33F9 File Offset: 0x000A15F9
	public override void OnExit()
	{
		this.StarSlugEnemy.StopSound(this.StarSlugEnemy.CrawlingSoundSource);
		this.Slug.Movement.Speed = 0f;
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x000A3426 File Offset: 0x000A1626
	public override void OnEnter()
	{
		this.StarSlugEnemy.PlaySound(this.StarSlugEnemy.CrawlingSoundSource);
	}
}
