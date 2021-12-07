using System;
using UnityEngine;

// Token: 0x020003BF RID: 959
public class SpiritGrenadeDamageDealer : DamageDealer, IDamageReciever
{
	// Token: 0x06001AA2 RID: 6818 RVA: 0x00072BFE File Offset: 0x00070DFE
	public void Awake()
	{
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x00072C00 File Offset: 0x00070E00
	public override void DealDamage(GameObject target)
	{
		if (target.GetComponent<SeinDamageReciever>())
		{
			return;
		}
		if (base.GetComponent<Collider>() && !base.GetComponent<Collider>().enabled)
		{
			return;
		}
		if (InstantiateUtility.IsDestroyed(target))
		{
			return;
		}
		if (this.Condition && !this.Condition.Validate(null))
		{
			return;
		}
		if (this.ShouldDealDamage != null && !this.ShouldDealDamage(target))
		{
			return;
		}
		Vector2 vector = target.transform.position - base.transform.position;
		Damage arg = new Damage(this.AmountOfDamage(target), vector.normalized, base.transform.position, this.DamageType, base.gameObject);
		IDamageReciever damageReciever = target.FindComponent<IDamageReciever>();
		if (damageReciever != null)
		{
			this.OnDamageDealtEvent(target, arg);
		}
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x00072CF4 File Offset: 0x00070EF4
	public void OnRecieveDamage(Damage damage)
	{
		base.transform.parent.GetComponent<SpiritGrenade>().OnRecieveDamage(damage);
	}
}
