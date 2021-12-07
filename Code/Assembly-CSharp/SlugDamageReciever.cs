using System;

// Token: 0x020005C1 RID: 1473
public class SlugDamageReciever : EntityDamageReciever
{
	// Token: 0x06002550 RID: 9552 RVA: 0x000A2D1F File Offset: 0x000A0F1F
	public override void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.Acid)
		{
			return;
		}
		base.OnRecieveDamage(damage);
	}
}
