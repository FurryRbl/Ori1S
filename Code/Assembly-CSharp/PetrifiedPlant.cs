using System;

// Token: 0x02000557 RID: 1367
public class PetrifiedPlant : Entity
{
	// Token: 0x060023AB RID: 9131 RVA: 0x0009C2B7 File Offset: 0x0009A4B7
	public override void Awake()
	{
		base.Awake();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnPreProcessDamage));
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x0009C2E6 File Offset: 0x0009A4E6
	public override void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Remove(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnPreProcessDamage));
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x0009C318 File Offset: 0x0009A518
	public void OnPreProcessDamage(Damage damage)
	{
		if (damage.Type == DamageType.SpiritFlame)
		{
			damage.SetAmount(0f);
			base.PlaySound(this.Deflected);
		}
		else if (damage.Type != DamageType.ChargeFlame && damage.Type != DamageType.Grenade)
		{
			damage.SetAmount(0f);
		}
	}

	// Token: 0x04001DE7 RID: 7655
	public SoundProvider Deflected;
}
