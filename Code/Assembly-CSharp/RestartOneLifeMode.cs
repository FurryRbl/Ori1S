using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class RestartOneLifeMode : ActionMethod
{
	// Token: 0x06000DDA RID: 3546 RVA: 0x00040B3C File Offset: 0x0003ED3C
	public override void Perform(IContext context)
	{
		Scenes.Manager.UnloadAllScenes();
		GameController.Instance.Timer.Reset();
		GameController.Instance.MainMenuCanBeOpened = false;
		GameController.Instance.RequireInitialValues = true;
		GameController.Instance.IsLoadingGame = false;
		InstantLoadScenesController.Instance.OnGameReset();
		GameController.Instance.StartCoroutine(this.RestartingCleanupNextFrame());
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x00040BA0 File Offset: 0x0003EDA0
	public IEnumerator RestartingCleanupNextFrame()
	{
		GameController.Instance.RemoveGameplayObjects();
		GameController.Instance.ResetInputLocks();
		if (UI.Fader.IsFadingInOrStay() || UI.Fader.IsTimelineFading())
		{
			UI.Fader.FadeOut(2f);
		}
		yield return new WaitForFixedUpdate();
		GameController.Instance.ActiveObjectives.Clear();
		SaveSceneManager.ClearSaveSlotForOneLife(Game.Checkpoint.SaveGameData);
		Events.Scheduler.OnGameSerializeLoad.Call();
		Events.Scheduler.OnGameReset.Call();
		SaveSceneManager.Master.Load(Game.Checkpoint.SaveGameData.Master);
		if (UI.Fader.IsFadingInOrStay() || UI.Fader.IsTimelineFading())
		{
			UI.Fader.FadeOut(2f);
		}
		yield return new WaitForFixedUpdate();
		RuntimeSceneMetaData sceneMetaData = Scenes.Manager.GetSceneInformation("sunkenGladesRunaway");
		GoToSceneController.Instance.GoToSceneAsync(sceneMetaData, null, false);
		InstantLoadScenesController.Instance.FreezeIfLoadingScenes();
		DifficultyController.Instance.SetDifficulty(DifficultyMode.OneLife);
		yield break;
	}
}
