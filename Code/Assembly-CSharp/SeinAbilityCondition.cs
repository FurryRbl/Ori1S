using System;
using Game;

// Token: 0x020002A1 RID: 673
public class SeinAbilityCondition : Condition
{
	// Token: 0x0600159D RID: 5533 RVA: 0x0005FF31 File Offset: 0x0005E131
	public override bool Validate(IContext context)
	{
		return !(Characters.Sein == null) && Characters.Sein.PlayerAbilities.HasAbility(this.Ability);
	}

	// Token: 0x04001298 RID: 4760
	public AbilityType Ability;
}
