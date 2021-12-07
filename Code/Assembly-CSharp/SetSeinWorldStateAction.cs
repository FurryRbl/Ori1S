using System;
using Sein.World;

// Token: 0x0200099D RID: 2461
public class SetSeinWorldStateAction : ActionMethod
{
	// Token: 0x060035A9 RID: 13737 RVA: 0x000E114C File Offset: 0x000DF34C
	public override void Perform(IContext context)
	{
		switch (this.State)
		{
		case WorldState.WaterPurified:
			Events.WaterPurified = this.IsTrue;
			break;
		case WorldState.GumoFree:
			Events.GumoFree = this.IsTrue;
			break;
		case WorldState.SpiritTreeReached:
			Events.SpiritTreeReached = this.IsTrue;
			break;
		case WorldState.GinsoTreeKey:
			Keys.GinsoTree = this.IsTrue;
			break;
		case WorldState.GinsoTreeEntered:
			Events.GinsoTreeEntered = this.IsTrue;
			break;
		case WorldState.WindRestored:
			Events.WindRestored = this.IsTrue;
			break;
		case WorldState.GravityActivated:
			Events.GravityActivated = this.IsTrue;
			break;
		case WorldState.MistLifted:
			Events.MistLifted = this.IsTrue;
			break;
		case WorldState.ForlornRuinsKey:
			Keys.ForlornRuins = this.IsTrue;
			break;
		case WorldState.MountHoruKey:
			Keys.MountHoru = this.IsTrue;
			break;
		case WorldState.WarmthReturned:
			Events.WarmthReturned = this.IsTrue;
			break;
		case WorldState.DarknessLifted:
			Events.DarknessLifted = this.IsTrue;
			break;
		}
	}

	// Token: 0x060035AA RID: 13738 RVA: 0x000E1264 File Offset: 0x000DF464
	public override string GetNiceName()
	{
		return "Set " + ActionHelper.GetName(this.State.ToString()) + " to " + ActionHelper.GetName(this.IsTrue.ToString());
	}

	// Token: 0x04003043 RID: 12355
	public WorldState State;

	// Token: 0x04003044 RID: 12356
	public bool IsTrue;
}
