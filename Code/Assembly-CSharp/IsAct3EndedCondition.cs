using System;

// Token: 0x0200028D RID: 653
public class IsAct3EndedCondition : Condition
{
	// Token: 0x06001551 RID: 5457 RVA: 0x0005EFA4 File Offset: 0x0005D1A4
	public override bool Validate(IContext context)
	{
		return AchievementsLogic.Act3Ended;
	}
}
