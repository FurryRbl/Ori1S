using System;
using Sein.World;

// Token: 0x020009D3 RID: 2515
public class MistAction : ActionMethod
{
	// Token: 0x060036C4 RID: 14020 RVA: 0x000E5EF8 File Offset: 0x000E40F8
	public override void Perform(IContext context)
	{
		MistAction.ActionType action = this.Action;
		if (action != MistAction.ActionType.ShowMist)
		{
			if (action == MistAction.ActionType.HideMist)
			{
				Events.MistLifted = true;
			}
		}
		else
		{
			Events.MistLifted = false;
		}
	}

	// Token: 0x040031AA RID: 12714
	public MistAction.ActionType Action = MistAction.ActionType.HideMist;

	// Token: 0x020009D4 RID: 2516
	public enum ActionType
	{
		// Token: 0x040031AC RID: 12716
		ShowMist,
		// Token: 0x040031AD RID: 12717
		HideMist
	}
}
