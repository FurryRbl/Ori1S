using System;
using Sein.World;

// Token: 0x0200099C RID: 2460
public class SeinWorldStateCondition : Condition
{
	// Token: 0x060035A7 RID: 13735 RVA: 0x000E1050 File Offset: 0x000DF250
	public override bool Validate(IContext context)
	{
		switch (this.State)
		{
		case WorldState.WaterPurified:
			return Events.WaterPurified == this.IsTrue;
		case WorldState.GumoFree:
			return Events.GumoFree == this.IsTrue;
		case WorldState.SpiritTreeReached:
			return Events.SpiritTreeReached == this.IsTrue;
		case WorldState.GinsoTreeKey:
			return Keys.GinsoTree == this.IsTrue;
		case WorldState.WindRestored:
			return Events.WindRestored == this.IsTrue;
		case WorldState.GravityActivated:
			return Events.GravityActivated == this.IsTrue;
		case WorldState.MistLifted:
			return Events.MistLifted == this.IsTrue;
		case WorldState.ForlornRuinsKey:
			return Keys.ForlornRuins == this.IsTrue;
		case WorldState.MountHoruKey:
			return Keys.MountHoru == this.IsTrue;
		case WorldState.WarmthReturned:
			return Events.WarmthReturned == this.IsTrue;
		case WorldState.DarknessLifted:
			return Events.DarknessLifted == this.IsTrue;
		}
		return false;
	}

	// Token: 0x04003041 RID: 12353
	public WorldState State;

	// Token: 0x04003042 RID: 12354
	public bool IsTrue = true;
}
