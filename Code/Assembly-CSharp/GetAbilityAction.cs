using System;
using Game;

// Token: 0x020008E6 RID: 2278
[Category("Sein")]
public class GetAbilityAction : ActionMethod
{
	// Token: 0x060032D1 RID: 13009 RVA: 0x000D6CFA File Offset: 0x000D4EFA
	public override void Perform(IContext context)
	{
		Characters.Sein.PlayerAbilities.SetAbility(this.Ability, this.Gain);
	}

	// Token: 0x04002DCE RID: 11726
	public AbilityType Ability;

	// Token: 0x04002DCF RID: 11727
	public bool Gain = true;
}
