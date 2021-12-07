using System;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x020008FA RID: 2298
public class DamageDealerUnlessCarryingNightberry : DamageDealer
{
	// Token: 0x06003321 RID: 13089 RVA: 0x000D79B3 File Offset: 0x000D5BB3
	public void Awake()
	{
		this.m_frame = Time.renderedFrameCount + 2;
	}

	// Token: 0x06003322 RID: 13090 RVA: 0x000D79C4 File Offset: 0x000D5BC4
	public override void DealDamage(GameObject target)
	{
		if (Time.renderedFrameCount <= this.m_frame)
		{
			return;
		}
		if (Sein.World.Events.GravityActivated)
		{
			return;
		}
		if (Items.NightBerry && Characters.Sein && (Vector3.Distance(target.transform.position, Items.NightBerry.Position) < Items.NightBerry.SafeFromDamageRadius || Characters.Sein.Controller.IsPlayingAnimation))
		{
			return;
		}
		base.DealDamage(target);
	}

	// Token: 0x04002E22 RID: 11810
	private int m_frame;
}
