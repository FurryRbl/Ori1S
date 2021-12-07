using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class CheckpointPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000EBE RID: 3774 RVA: 0x00043C42 File Offset: 0x00041E42
	public bool OnGameLoad()
	{
		return false;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00043C45 File Offset: 0x00041E45
	public void PlayCycle(int frame)
	{
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x00043C47 File Offset: 0x00041E47
	public void RecordCycle(int frame)
	{
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x00043C49 File Offset: 0x00041E49
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00043C51 File Offset: 0x00041E51
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00043C5E File Offset: 0x00041E5E
	public void OnDestroy()
	{
		Recorder.Instance.DeregisterPlugin(this);
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00043C6C File Offset: 0x00041E6C
	public void PerformLoadFromCurrentFrame()
	{
		this.m_frame = Recorder.Instance.CurrentFrameIndex;
		this.m_data = RecorderPlaybackUI.Instance.Recorder.CurrentFrame.GetFrameDataOfType<CheckpointData>();
		if (this.m_data != null)
		{
			SaveGameData saveGameData = Game.Checkpoint.SaveGameData;
			SaveScene master = saveGameData.Master;
			master.SaveObjects.Clear();
			foreach (SaveObject item in this.m_data.GlobalSaveObjects)
			{
				master.SaveObjects.Add(item);
			}
			saveGameData.Scenes.Clear();
			master = saveGameData.Master;
			master.SaveObjects.Clear();
			foreach (SaveObject item2 in this.m_data.GlobalSaveObjects)
			{
				master.SaveObjects.Add(item2);
			}
			foreach (KeyValuePair<MoonGuid, List<SaveObject>> keyValuePair in this.m_data.SceneSaveObjects)
			{
				SaveScene saveScene = saveGameData.InsertScene(keyValuePair.Key);
				saveScene.SaveObjects.Clear();
				foreach (SaveObject item3 in keyValuePair.Value)
				{
					saveScene.SaveObjects.Add(item3);
				}
			}
			Recorder.Instance.CurrentFrameIndex = this.m_frame;
			GameController.Instance.IsLoadingGame = true;
			LateStartHook.AddLateStartMethod(new Action(this.RestoreCheckpointPart1));
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00043E7C File Offset: 0x0004207C
	public void RestoreCheckpointPart1()
	{
		Music.OnRestoreCheckpoint();
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
		SaveSceneManager.Master.Load(Game.Checkpoint.SaveGameData.Master);
		LateStartHook.AddLateStartMethod(new Action(this.RestoreCheckpointPart2));
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x00043EC0 File Offset: 0x000420C0
	public void RestoreCheckpointPart2()
	{
		Scenes.Manager.UnloadAllScenes();
		InstantLoadScenesController.Instance.LoadScenesAtPosition(new Action(this.OnFinishedLoading), false, true);
		CameraPivotZone.InstantUpdate();
		Scenes.Manager.ClearCameraPuppetPositions();
		Scenes.Manager.UnloadScenesAtPosition(true);
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x00043F14 File Offset: 0x00042114
	public void OnFinishedLoading()
	{
		SaveSceneManager.Master.Load(Game.Checkpoint.SaveGameData.Master);
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
		try
		{
			Events.Scheduler.OnGameSerializeLoad.Call();
		}
		catch (Exception ex)
		{
		}
		GameController.Instance.IsLoadingGame = false;
		this.ApplyPreviousInput(this.m_frame, Recorder.Instance);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00043F88 File Offset: 0x00042188
	public void PerformLoad(int keyframe)
	{
		int keyframe2 = RecorderPlaybackUI.Instance.Timeline.GetKeyframe(keyframe);
		this.PerformLoadFromFrame(keyframe2);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00043FAD File Offset: 0x000421AD
	public void PerformLoadFromFrame(int frame)
	{
		Recorder.Instance.CurrentFrameIndex = frame;
		this.PerformLoadFromCurrentFrame();
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x00043FC0 File Offset: 0x000421C0
	public void ApplyPreviousInput(int frame, Recorder recorder)
	{
		for (int i = frame; i >= 0; i--)
		{
			RecorderFrame frame2 = recorder.RecorderData.GetFrame(i);
			InputData frameDataOfType = frame2.GetFrameDataOfType<InputData>();
			if (frameDataOfType != null)
			{
				InputPlugin component = base.GetComponent<InputPlugin>();
				if (component != null)
				{
					component.Apply(frameDataOfType);
				}
				break;
			}
		}
		for (int j = frame; j >= 0; j--)
		{
			RecorderFrame frame3 = recorder.RecorderData.GetFrame(j);
			AnalogueInputData frameDataOfType2 = frame3.GetFrameDataOfType<AnalogueInputData>();
			if (frameDataOfType2 != null)
			{
				InputPlugin component2 = base.GetComponent<InputPlugin>();
				if (component2 != null)
				{
					component2.Apply(frameDataOfType2);
				}
				break;
			}
		}
		PlayerInput.Instance.RefreshControls();
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0004407A File Offset: 0x0004227A
	public void MakeCheckpointAtEndOfFrame()
	{
		this.m_createCheckpoint = true;
		LateStartHook.AddLateStartMethod(new Action(this.MakeCheckpoint));
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x00044094 File Offset: 0x00042294
	private void MakeCheckpoint()
	{
		if (this.m_createCheckpoint)
		{
			this.m_createCheckpoint = false;
			foreach (SceneManagerScene sceneManagerScene in Scenes.Manager.ActiveScenes)
			{
				if (!sceneManagerScene.UnityIsLoading)
				{
					this.m_sceneNames.Add(sceneManagerScene.MetaData.SceneMoonGuid);
				}
			}
			CheckpointData.Record(Recorder.Instance.RecorderStream, this.m_sceneNames);
			InputData.Record(Recorder.Instance.RecorderStream);
			CursorInputData.Record(Recorder.Instance.RecorderStream);
			AnalogueInputData.Record(Recorder.Instance.RecorderStream);
			this.m_sceneNames.Clear();
		}
	}

	// Token: 0x04000BD4 RID: 3028
	private CheckpointData m_data;

	// Token: 0x04000BD5 RID: 3029
	private bool m_createCheckpoint;

	// Token: 0x04000BD6 RID: 3030
	private int m_frame;

	// Token: 0x04000BD7 RID: 3031
	private List<MoonGuid> m_sceneNames = new List<MoonGuid>(5);
}
