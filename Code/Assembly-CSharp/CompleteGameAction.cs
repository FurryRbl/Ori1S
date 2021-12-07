using System;

// Token: 0x02000114 RID: 276
public class CompleteGameAction : ActionMethod
{
	// Token: 0x06000AC1 RID: 2753 RVA: 0x0002EC8C File Offset: 0x0002CE8C
	public override void Perform(IContext context)
	{
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.WatchCutscenes)
		{
			return;
		}
		GameController.Instance.SaveGameController.PerformLoadWithoutCheckpointRestore();
		SaveSlotInfo saveSlotInfo = SaveSlotsManager.FindOrCreateSaveSlot(SaveSlotsManager.CurrentSlotIndex);
		saveSlotInfo.Completed = true;
		saveSlotInfo.CompletedWithEverything = GameWorld.Instance.HasCompletedEverything();
		GameController.Instance.SaveGameController.PerformSave();
		GameSettings.Instance.OneLifeModeUnlocked = true;
		GameSettings.Instance.SaveSettings();
	}
}
