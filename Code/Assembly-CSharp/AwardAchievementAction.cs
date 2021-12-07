using System;

// Token: 0x020008B7 RID: 2231
[Category("SPAConfig")]
public class AwardAchievementAction : ActionMethod
{
	// Token: 0x060031B4 RID: 12724 RVA: 0x000D3594 File Offset: 0x000D1794
	public override void Perform(IContext context)
	{
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.Game)
		{
			AchievementsController.AwardAchievement(this.Achievement);
		}
	}

	// Token: 0x04002CEA RID: 11498
	public AchievementAsset Achievement;
}
