using System;

// Token: 0x020009C7 RID: 2503
public class SetWaterRisingPropertiesAction : ActionMethod
{
	// Token: 0x06003699 RID: 13977 RVA: 0x000E560C File Offset: 0x000E380C
	public override void Perform(IContext context)
	{
		RisingWater.SetProperties(this.Speed);
	}

	// Token: 0x0400317B RID: 12667
	public float Speed = 5f;
}
