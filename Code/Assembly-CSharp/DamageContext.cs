using System;

// Token: 0x020002A9 RID: 681
public struct DamageContext : IContext, IDamageContext
{
	// Token: 0x060015AB RID: 5547 RVA: 0x00060053 File Offset: 0x0005E253
	public DamageContext(Damage damage)
	{
		this.m_damage = damage;
	}

	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x060015AC RID: 5548 RVA: 0x0006005C File Offset: 0x0005E25C
	// (set) Token: 0x060015AD RID: 5549 RVA: 0x00060064 File Offset: 0x0005E264
	public Damage Damage
	{
		get
		{
			return this.m_damage;
		}
		private set
		{
			this.m_damage = value;
		}
	}

	// Token: 0x0400129E RID: 4766
	private Damage m_damage;
}
