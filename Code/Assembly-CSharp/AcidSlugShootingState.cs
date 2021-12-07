using System;
using UnityEngine;

// Token: 0x020005BD RID: 1469
public class AcidSlugShootingState : AcidSlugState
{
	// Token: 0x06002548 RID: 9544 RVA: 0x000A2B5F File Offset: 0x000A0D5F
	public AcidSlugShootingState(AcidSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x000A2B68 File Offset: 0x000A0D68
	public override void OnEnter()
	{
		this.Slug.Animation.PlayLoop(this.AcidSlugEnemy.Animations.Shooting, 0, null, false);
		this.AcidSlugEnemy.PlaySound(this.AcidSlugEnemy.ShootingSoundSource);
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x000A2BAF File Offset: 0x000A0DAF
	public void SpawnStarSpikes(Vector3 up, Vector3 right)
	{
	}
}
