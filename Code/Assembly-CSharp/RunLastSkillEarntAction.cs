using System;
using Game;

// Token: 0x02000928 RID: 2344
public class RunLastSkillEarntAction : ActionMethod
{
	// Token: 0x060033EC RID: 13292 RVA: 0x000DA5B0 File Offset: 0x000D87B0
	public override void Perform(IContext context)
	{
		ActionMethod gainAbilityAction = Characters.Sein.PlayerAbilities.GainAbilityAction;
		if (gainAbilityAction)
		{
			gainAbilityAction.Perform(null);
			Characters.Sein.PlayerAbilities.GainAbilityAction = null;
		}
	}
}
