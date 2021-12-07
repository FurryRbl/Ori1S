using System;

// Token: 0x020008BA RID: 2234
[Category("SPAConfig")]
public class SetUserIntegerAction : ActionMethod
{
	// Token: 0x060031BA RID: 12730 RVA: 0x000D35F3 File Offset: 0x000D17F3
	public override void Perform(IContext context)
	{
	}

	// Token: 0x04002CF0 RID: 11504
	public SPAConfigValue SpaConfigPropertyID = new SPAConfigValue();

	// Token: 0x04002CF1 RID: 11505
	public int Value;
}
