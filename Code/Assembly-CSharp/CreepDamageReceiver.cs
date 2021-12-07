using System;

// Token: 0x02000539 RID: 1337
public class CreepDamageReceiver : EntityDamageReciever
{
	// Token: 0x06002339 RID: 9017 RVA: 0x0009A370 File Offset: 0x00098570
	public override void OnRecieveDamage(Damage damage)
	{
		if (this.DamageAction)
		{
			this.DamageAction.Perform(new DamageContext(damage));
		}
	}
}
