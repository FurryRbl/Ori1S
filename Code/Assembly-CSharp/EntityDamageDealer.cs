using System;

// Token: 0x020003C2 RID: 962
public class EntityDamageDealer : DamageDealer
{
	// Token: 0x06001AB6 RID: 6838 RVA: 0x00072FF3 File Offset: 0x000711F3
	public void OnValidate()
	{
		this.Entity = base.transform.FindComponentUpwards<Entity>();
		this.Entity.DamageDealer = this;
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x00073012 File Offset: 0x00071212
	public void Awake()
	{
		if (this.Entity == null)
		{
			this.OnValidate();
		}
	}

	// Token: 0x0400172A RID: 5930
	public Entity Entity;
}
