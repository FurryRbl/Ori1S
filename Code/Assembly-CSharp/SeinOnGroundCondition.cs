using System;
using Game;

// Token: 0x020002A2 RID: 674
public class SeinOnGroundCondition : Condition
{
	// Token: 0x0600159F RID: 5535 RVA: 0x0005FF64 File Offset: 0x0005E164
	public override bool Validate(IContext context)
	{
		return !Characters.Sein.Controller.IsSwimming && Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround;
	}
}
