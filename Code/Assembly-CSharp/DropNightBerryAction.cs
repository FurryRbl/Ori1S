using System;
using Game;

// Token: 0x020002DC RID: 732
[Category("Obsolete")]
public class DropNightBerryAction : ActionMethod
{
	// Token: 0x0600166D RID: 5741 RVA: 0x00062A7C File Offset: 0x00060C7C
	public override void Perform(IContext context)
	{
		DropNightBerryAction.NightBerryAction action = this.Action;
		if (action != DropNightBerryAction.NightBerryAction.DropNightBerry)
		{
			if (action == DropNightBerryAction.NightBerryAction.ShrinkSpiritRing)
			{
				Items.NightBerry.ShrinkSpiritRing();
			}
		}
		else if (Characters.Sein.Abilities.Carry.CurrentCarryable != null)
		{
			Characters.Sein.Abilities.Carry.CurrentCarryable.Drop();
		}
	}

	// Token: 0x04001359 RID: 4953
	public DropNightBerryAction.NightBerryAction Action;

	// Token: 0x020002DD RID: 733
	public enum NightBerryAction
	{
		// Token: 0x0400135B RID: 4955
		DropNightBerry,
		// Token: 0x0400135C RID: 4956
		ShrinkSpiritRing
	}
}
