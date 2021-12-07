using System;

// Token: 0x020007DB RID: 2011
[UberShaderOrder(UberShaderOrder.TransparencyAnimator)]
[CustomShaderModifier("Transparancy animator")]
[UberShaderCategory(UberShaderCategory.Animation)]
public class TransparancyAnimatorModifier : UberShaderModifier
{
	// Token: 0x06002E25 RID: 11813 RVA: 0x000C44D5 File Offset: 0x000C26D5
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.TransparancyCurve.TimeScale *= speed;
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x000C44EA File Offset: 0x000C26EA
	public override void SetProperties()
	{
		this.TransparancyCurve.Set("_TransparancyCurve", base.AttachedToShaderBlock);
	}

	// Token: 0x0400299B RID: 10651
	public UberShaderCurve TransparancyCurve = new UberShaderCurve();
}
