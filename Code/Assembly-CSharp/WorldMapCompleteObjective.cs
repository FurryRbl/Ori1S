using System;
using Game;

// Token: 0x0200089D RID: 2205
public class WorldMapCompleteObjective : ActionMethod
{
	// Token: 0x06003164 RID: 12644 RVA: 0x000D2D2C File Offset: 0x000D0F2C
	public override void Perform(IContext context)
	{
		Objectives.CompleteObjective(this.Objective);
	}

	// Token: 0x04002CAD RID: 11437
	public Objective Objective;
}
