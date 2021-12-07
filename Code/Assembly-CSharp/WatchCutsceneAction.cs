using System;

// Token: 0x02000861 RID: 2145
public class WatchCutsceneAction : ActionMethod
{
	// Token: 0x06003095 RID: 12437 RVA: 0x000CE9EC File Offset: 0x000CCBEC
	public override void Perform(IContext context)
	{
		GameController.Instance.MainMenuCanBeOpened = true;
		GameController.Instance.RequireInitialValues = true;
		GameStateMachine.Instance.SetToWatchCutscene();
	}
}
