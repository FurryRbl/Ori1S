using System;

// Token: 0x020008EF RID: 2287
public class MapStoneActivatedCondition : Condition
{
	// Token: 0x060032F7 RID: 13047 RVA: 0x000D739A File Offset: 0x000D559A
	public override bool Validate(IContext context)
	{
		return this.MapStone.Activated == this.Activated;
	}

	// Token: 0x04002DFC RID: 11772
	[NotNull]
	public MapStone MapStone;

	// Token: 0x04002DFD RID: 11773
	public bool Activated;
}
