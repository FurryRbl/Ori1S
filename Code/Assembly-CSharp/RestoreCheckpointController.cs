using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000973 RID: 2419
public class RestoreCheckpointController
{
	// Token: 0x06003510 RID: 13584 RVA: 0x000DE8CC File Offset: 0x000DCACC
	public void RestoreCheckpoint()
	{
		GameController.Instance.IsLoadingGame = true;
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
		SaveSceneManager.Master.Load(Game.Checkpoint.SaveGameData.Master);
		Scenes.Manager.UnloadScenesAtPosition(true);
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
		CameraPivotZone.InstantUpdate();
		Music.OnRestoreCheckpoint();
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
		if (Scenes.Manager)
		{
			foreach (SceneManagerScene sceneManagerScene in Scenes.Manager.ActiveScenes)
			{
				if (sceneManagerScene.HasStartBeenCalled && sceneManagerScene.SceneRoot.SaveSceneManager && Game.Checkpoint.SaveGameData.SceneExists(sceneManagerScene.MetaData.SceneMoonGuid))
				{
					sceneManagerScene.SceneRoot.SaveSceneManager.Load(Game.Checkpoint.SaveGameData.GetScene(sceneManagerScene.MetaData.SceneMoonGuid));
				}
			}
		}
		try
		{
			Events.Scheduler.OnGameSerializeLoad.Call();
		}
		catch (Exception ex)
		{
		}
		Game.Checkpoint.Events.OnPostRestore.Call();
		if (UI.Cameras.Current != null)
		{
			UI.Cameras.Current.MoveCameraToTargetInstantly(true);
		}
		if (UI.Fader.IsFadingInOrStay())
		{
			UI.Fader.FadeOut(2f);
		}
		LateStartHook.AddLateStartMethod(new Action(this.FinishLoading));
		GameController.Instance.StartCoroutine(this.MoveCameraInstantlyAgain());
	}

	// Token: 0x06003511 RID: 13585 RVA: 0x000DEA70 File Offset: 0x000DCC70
	private IEnumerator MoveCameraInstantlyAgain()
	{
		yield return new WaitForFixedUpdate();
		if (UI.Cameras.Current != null)
		{
			UI.Cameras.Current.MoveCameraToTargetInstantly(true);
		}
		yield break;
	}

	// Token: 0x06003512 RID: 13586 RVA: 0x000DEA84 File Offset: 0x000DCC84
	private void FinishLoading()
	{
		GameController.Instance.IsLoadingGame = false;
	}
}
