using System;
using UnityEngine;

// Token: 0x02000518 RID: 1304
public class DropSlugLandState : DropSlugState
{
	// Token: 0x060022C6 RID: 8902 RVA: 0x000985E5 File Offset: 0x000967E5
	public DropSlugLandState(DropSlugEnemy slug) : base(slug)
	{
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x000985F0 File Offset: 0x000967F0
	public override void OnEnter()
	{
		GameObject gameObject = this.Slug.Explosion.Spawn(null);
		gameObject.GetComponentInChildren<DamageDealer>().Damage = this.Slug.Settings.ExplosionDamage;
		base.OnEnter();
		Damage damage = new Damage(1000f, Vector2.zero, this.Slug.Position, DamageType.Explosion, this.Slug.gameObject);
		damage.DealToComponents(this.Slug.DamageReciever.gameObject);
	}
}
