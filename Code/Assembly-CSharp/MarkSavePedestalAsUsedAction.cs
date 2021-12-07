using System;

// Token: 0x02000262 RID: 610
public class MarkSavePedestalAsUsedAction : ActionMethod
{
	// Token: 0x0600148B RID: 5259 RVA: 0x0005CBBD File Offset: 0x0005ADBD
	public override void Perform(IContext context)
	{
		this.SavePedestal.MarkAsUsed();
	}

	// Token: 0x040011E1 RID: 4577
	[NotNull]
	public SavePedestal SavePedestal;
}
