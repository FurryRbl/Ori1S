using System;

// Token: 0x0200060F RID: 1551
public class EntityHealthProvider : FloatValueProvider
{
	// Token: 0x0600269F RID: 9887 RVA: 0x000A9603 File Offset: 0x000A7803
	public void OnValidate()
	{
		this.Entity = base.transform.FindComponentUpwards<Entity>();
	}

	// Token: 0x060026A0 RID: 9888 RVA: 0x000A9616 File Offset: 0x000A7816
	public void Awake()
	{
		if (this.Entity == null)
		{
			this.OnValidate();
		}
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x000A962F File Offset: 0x000A782F
	public override float GetFloatValue()
	{
		return this.Entity.DamageReciever.Health / this.Entity.DamageReciever.MaxHealth;
	}

	// Token: 0x04002152 RID: 8530
	public Entity Entity;
}
