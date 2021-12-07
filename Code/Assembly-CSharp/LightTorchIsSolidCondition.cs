using System;
using Game;

// Token: 0x0200064E RID: 1614
public class LightTorchIsSolidCondition : Condition
{
	// Token: 0x06002784 RID: 10116 RVA: 0x000AC030 File Offset: 0x000AA230
	public override bool Validate(IContext context)
	{
		return Items.LightTorch && !Items.LightTorch.IsChasing;
	}
}
