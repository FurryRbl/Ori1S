using System;
using Game;

// Token: 0x0200027F RID: 639
public class CompareSeinLevelCondition : Condition
{
	// Token: 0x06001524 RID: 5412 RVA: 0x0005E497 File Offset: 0x0005C697
	public override bool Validate(IContext context)
	{
		return Characters.Sein.Level.Current == this.Value;
	}

	// Token: 0x0400124E RID: 4686
	public int Value;
}
