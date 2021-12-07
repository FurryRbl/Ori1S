using System;

// Token: 0x02000359 RID: 857
[Category("Ori")]
internal class UnhighlightOriInterestTrigger : ActionMethod
{
	// Token: 0x0600186A RID: 6250 RVA: 0x00068B32 File Offset: 0x00066D32
	public override void Perform(IContext context)
	{
		this.OriInterestTrigger.Unhighlight();
	}

	// Token: 0x040014F6 RID: 5366
	[NotNull]
	public OriInterestTriggerB OriInterestTrigger;
}
