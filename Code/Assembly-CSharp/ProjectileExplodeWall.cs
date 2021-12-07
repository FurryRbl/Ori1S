using System;

// Token: 0x02000559 RID: 1369
public class ProjectileExplodeWall : Entity
{
	// Token: 0x060023B1 RID: 9137 RVA: 0x0009C383 File Offset: 0x0009A583
	public override void Awake()
	{
		base.Awake();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnPreProcessDamage));
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x0009C3B2 File Offset: 0x0009A5B2
	public override void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Remove(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnPreProcessDamage));
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x0009C3E1 File Offset: 0x0009A5E1
	public void OnPreProcessDamage(Damage damage)
	{
		if (damage.Type != DamageType.Projectile)
		{
			damage.SetAmount(0f);
			base.PlaySound(this.Deflected);
		}
	}

	// Token: 0x04001DE8 RID: 7656
	public SoundProvider Deflected;
}
