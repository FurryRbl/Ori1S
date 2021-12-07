using System;
using Game;

// Token: 0x020008A1 RID: 2209
public class WorldMapRemoveObjective : ActionMethod
{
	// Token: 0x06003173 RID: 12659 RVA: 0x000D3002 File Offset: 0x000D1202
	public override void Perform(IContext context)
	{
		Objectives.CompleteObjective(this.Objective);
	}

	// Token: 0x04002CB6 RID: 11446
	public Objective Objective;
}
