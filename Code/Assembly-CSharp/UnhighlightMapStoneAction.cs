using System;

// Token: 0x02000357 RID: 855
[Category("Map Stone")]
internal class UnhighlightMapStoneAction : ActionMethod
{
	// Token: 0x0600185C RID: 6236 RVA: 0x0006873F File Offset: 0x0006693F
	public override void Perform(IContext context)
	{
		this.MapStone.Unhighlight();
	}

	// Token: 0x040014E7 RID: 5351
	[NotNull]
	public MapStone MapStone;
}
