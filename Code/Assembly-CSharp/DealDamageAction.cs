using System;
using Game;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class DealDamageAction : ActionMethod
{
	// Token: 0x06001436 RID: 5174 RVA: 0x0005BDBC File Offset: 0x00059FBC
	public override void Perform(IContext context)
	{
		if (this.Target == null)
		{
			Damage damage = new Damage(10000f, Vector2.zero, base.transform.position, DamageType.Lava, base.gameObject);
			Characters.Sein.Mortality.DamageReciever.OnRecieveDamage(damage);
		}
		else
		{
			Damage damage2 = new Damage(this.Amount, Vector3.zero, base.transform.position, this.DamageType, base.gameObject);
			damage2.DealToComponents(this.Target);
		}
	}

	// Token: 0x040011BB RID: 4539
	public DamageType DamageType;

	// Token: 0x040011BC RID: 4540
	public float Amount;

	// Token: 0x040011BD RID: 4541
	public GameObject Target;
}
