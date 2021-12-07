using System;
using UnityEngine;

// Token: 0x020005C9 RID: 1481
public class StarSlugShootingState : StarSlugState
{
	// Token: 0x06002567 RID: 9575 RVA: 0x000A3487 File Offset: 0x000A1687
	public StarSlugShootingState(StarSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x000A3490 File Offset: 0x000A1690
	public override void OnEnter()
	{
		this.Slug.Animation.PlayLoop(this.StarSlugEnemy.Animations.Shooting, 0, null, false);
		this.StarSlugEnemy.PlaySound(this.StarSlugEnemy.ShootingSoundSource);
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x000A34D7 File Offset: 0x000A16D7
	public void SpawnStarSpikes(Vector3 up, Vector3 right)
	{
	}
}
