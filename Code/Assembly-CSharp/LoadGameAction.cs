using System;

// Token: 0x0200025C RID: 604
public class LoadGameAction : ActionMethod
{
	// Token: 0x06001446 RID: 5190 RVA: 0x0005BF36 File Offset: 0x0005A136
	public override void Perform(IContext context)
	{
		SaveSlotBackupsManager.ResetBackupDelay();
		InstantLoadScenesController.Instance.LockFinishingLoading = true;
		GameStateMachine.Instance.SetToGame();
		if (!GameController.Instance.SaveGameController.PerformLoad())
		{
		}
	}
}
