using System;

// Token: 0x020008B9 RID: 2233
[Category("SPAConfig")]
public class SetUserFloatAction : ActionMethod
{
	// Token: 0x060031B8 RID: 12728 RVA: 0x000D35DE File Offset: 0x000D17DE
	public override void Perform(IContext context)
	{
	}

	// Token: 0x04002CEE RID: 11502
	public SPAConfigValue SpaConfigPropertyID = new SPAConfigValue();

	// Token: 0x04002CEF RID: 11503
	public float Value;
}
