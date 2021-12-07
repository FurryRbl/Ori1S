using System;

// Token: 0x02000536 RID: 1334
public class ChargeFlameWall : Entity
{
	// Token: 0x0600232F RID: 9007 RVA: 0x0009A1E8 File Offset: 0x000983E8
	public override void Awake()
	{
		base.Awake();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Combine(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
		this.DamageReciever.OnDeathEvent.Add(new Action<Damage>(this.OnDeathCallback));
	}

	// Token: 0x06002330 RID: 9008 RVA: 0x0009A240 File Offset: 0x00098440
	public override void OnDestroy()
	{
		base.OnDestroy();
		EntityDamageReciever damageReciever = this.DamageReciever;
		damageReciever.OnModifyDamage = (EntityDamageReciever.ModifyDamageDelegate)Delegate.Remove(damageReciever.OnModifyDamage, new EntityDamageReciever.ModifyDamageDelegate(this.OnModifyDamage));
		this.DamageReciever.OnDeathEvent.Remove(new Action<Damage>(this.OnDeathCallback));
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x0009A298 File Offset: 0x00098498
	public void OnModifyDamage(Damage damage)
	{
		if (damage.Type == DamageType.Enemy)
		{
			damage.SetAmount(0f);
			return;
		}
		if (this.GrenadeOnly && damage.Type != DamageType.Grenade)
		{
			damage.SetAmount(0f);
			base.PlaySound(this.Deflected);
			return;
		}
		if (damage.Type != DamageType.ChargeFlame && damage.Type != DamageType.Grenade && damage.Type != DamageType.Stomp && damage.Type != DamageType.StompBlast && damage.Type != DamageType.Explosion)
		{
			damage.SetAmount(0f);
			base.PlaySound(this.Deflected);
			return;
		}
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x0009A346 File Offset: 0x00098546
	public void OnDeathCallback(Damage damage)
	{
		if (damage.Type == DamageType.ChargeFlame)
		{
			AchievementsLogic.Instance.OnChargeFlameWallDestroyed();
		}
	}

	// Token: 0x04001DA2 RID: 7586
	public bool GrenadeOnly;

	// Token: 0x04001DA3 RID: 7587
	public SoundProvider Deflected;
}
