using System;
using UnityEngine;

// Token: 0x020003C5 RID: 965
public class ProjectileDamageDealer : DamageDealer
{
	// Token: 0x06001ABF RID: 6847 RVA: 0x0007303E File Offset: 0x0007123E
	public void Awake()
	{
		this.m_projectile = base.GetComponent<Projectile>();
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x0007304C File Offset: 0x0007124C
	public override void DealDamage(GameObject target)
	{
		if (target == this.m_projectile.Owner)
		{
			return;
		}
		base.DealDamage(target);
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x0007306C File Offset: 0x0007126C
	public override float AmountOfDamage(GameObject target)
	{
		if (target.GetComponent<Entity>())
		{
			return this.Damage * this.EnemyMultiplier;
		}
		return this.Damage;
	}

	// Token: 0x0400172B RID: 5931
	private Projectile m_projectile;

	// Token: 0x0400172C RID: 5932
	public float EnemyMultiplier = 4f;
}
