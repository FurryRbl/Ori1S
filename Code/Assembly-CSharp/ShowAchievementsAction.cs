using System;

// Token: 0x0200026B RID: 619
public class ShowAchievementsAction : ActionMethod
{
	// Token: 0x060014C4 RID: 5316 RVA: 0x0005D8B4 File Offset: 0x0005BAB4
	public override void Perform(IContext context)
	{
		AchievementsUI.Visible = true;
	}
}
