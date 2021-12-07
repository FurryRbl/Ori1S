using System;

// Token: 0x02000568 RID: 1384
public class StompableFloor : Entity
{
	// Token: 0x060023EE RID: 9198 RVA: 0x0009CDD8 File Offset: 0x0009AFD8
	public override void Awake()
	{
		base.Awake();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x0009CE07 File Offset: 0x0009B007
	public override void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Remove(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
	}

	// Token: 0x060023F0 RID: 9200 RVA: 0x0009CE36 File Offset: 0x0009B036
	public void OnModifyDamage(Damage damage)
	{
		if (damage.Type == DamageType.StompBlast)
		{
			damage.SetAmount(0f);
		}
	}
}
