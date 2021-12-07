using System;
using Game;

// Token: 0x020008A5 RID: 2213
public class SetWorldEventAction : ActionMethod
{
	// Token: 0x0600317E RID: 12670 RVA: 0x000D31B9 File Offset: 0x000D13B9
	public override void Perform(IContext context)
	{
		World.Events.Find(this.WorldEvents).Value = this.State;
	}

	// Token: 0x04002CBF RID: 11455
	public WorldEvents WorldEvents;

	// Token: 0x04002CC0 RID: 11456
	public int State;
}
