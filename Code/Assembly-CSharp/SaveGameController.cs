using System;
using System.Collections.Generic;
using System.IO;
using Core;
using Game;

// Token: 0x02000116 RID: 278
[Serializable]
public class SaveGameController
{
	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0002F267 File Offset: 0x0002D467
	public int CurrentSlotIndex
	{
		get
		{
			return SaveSlotsManager.CurrentSlotIndex;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002F26E File Offset: 0x0002D46E
	public int CurrentBackupIndex
	{
		get
		{
			return SaveSlotsManager.BackupIndex;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002F275 File Offset: 0x0002D475
	public bool SaveGameQueried
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0002F278 File Offset: 0x0002D478
	public void SaveToFile(string filename)
	{
		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
		{
			this.SaveToWriter(binaryWriter);
		}
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0002F2C0 File Offset: 0x0002D4C0
	public bool LoadFromFile(string filename)
	{
		bool result;
		using (BinaryReader binaryReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
		{
			result = this.LoadFromReader(binaryReader);
		}
		return result;
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0002F30C File Offset: 0x0002D50C
	public byte[] SaveToBytes()
	{
		MemoryStream memoryStream = new MemoryStream();
		using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
		{
			this.SaveToWriter(binaryWriter);
		}
		return memoryStream.ToArray();
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x0002F358 File Offset: 0x0002D558
	public void SaveToWriter(BinaryWriter writer)
	{
		SaveSlotsManager.CurrentSaveSlot.SaveToWriter(writer);
		Game.Checkpoint.SaveGameData.SaveToWriter(writer);
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002F370 File Offset: 0x0002D570
	private bool SaveWasOneLifeAndKilled
	{
		get
		{
			SaveSlotInfo currentSaveSlot = SaveSlotsManager.CurrentSaveSlot;
			return currentSaveSlot.Difficulty == DifficultyMode.OneLife && currentSaveSlot.WasKilled;
		}
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0002F398 File Offset: 0x0002D598
	public bool LoadFromReader(BinaryReader reader)
	{
		if (!SaveSlotsManager.CurrentSaveSlot.LoadFromReader(reader))
		{
			return false;
		}
		if (!Game.Checkpoint.SaveGameData.LoadFromReader(reader))
		{
			return false;
		}
		if (this.SaveWasOneLifeAndKilled)
		{
			SaveSceneManager.ClearSaveSlotForOneLife(Game.Checkpoint.SaveGameData);
		}
		return true;
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
	public bool LoadFromBytes(byte[] binary)
	{
		bool result;
		using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(binary)))
		{
			result = this.LoadFromReader(binaryReader);
		}
		return result;
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0002F42C File Offset: 0x0002D62C
	public bool SaveExists(int slotIndex)
	{
		if (!this.CanPerformLoad())
		{
			return false;
		}
		if (Recorder.Instance && Recorder.Instance.State == Recorder.RecorderState.Playing)
		{
			InputData frameDataOfType = Recorder.Instance.CurrentFrame.GetFrameDataOfType<InputData>();
			return frameDataOfType != null && frameDataOfType.SaveFileExists;
		}
		string saveFilePath = this.GetSaveFilePath(slotIndex, -1);
		return File.Exists(saveFilePath);
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002F494 File Offset: 0x0002D694
	public bool SaveFileExists
	{
		get
		{
			if (!this.CanPerformLoad())
			{
				return false;
			}
			if (Recorder.Instance && Recorder.Instance.State == Recorder.RecorderState.Playing)
			{
				List<InputData> frameData = Recorder.Instance.CurrentFrame.GetFrameData<InputData>();
				if (frameData != null)
				{
					InputData inputData = frameData[0];
					if (inputData != null)
					{
						return inputData.SaveFileExists;
					}
				}
				return false;
			}
			string currentSaveFilePath = this.CurrentSaveFilePath;
			return File.Exists(currentSaveFilePath);
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002F507 File Offset: 0x0002D707
	public string CurrentSaveFilePath
	{
		get
		{
			return this.GetSaveFilePath(this.CurrentSlotIndex, -1);
		}
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0002F518 File Offset: 0x0002D718
	public string GetSaveFilePath(int slotIndex, int backupIndex = -1)
	{
		if (backupIndex == -1)
		{
			return Path.Combine(OutputFolder.PlayerDataFolderPath, "saveFile" + slotIndex + ".sav");
		}
		return Path.Combine(OutputFolder.PlayerDataFolderPath, string.Format("saveFile{0}_bkup{1}.sav", slotIndex, backupIndex));
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0002F56C File Offset: 0x0002D76C
	public void Refresh()
	{
		if (!this.CanPerformLoad())
		{
			return;
		}
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0002F57C File Offset: 0x0002D77C
	public bool PerformLoad()
	{
		if (Recorder.IsPlaying)
		{
			return Recorder.Instance.OnPerformLoad();
		}
		if (!this.CanPerformLoad())
		{
			return false;
		}
		bool result = this.LoadFromFile(this.GetSaveFilePath(this.CurrentSlotIndex, this.CurrentBackupIndex));
		this.RestoreCheckpoint();
		return result;
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0002F5CC File Offset: 0x0002D7CC
	public bool PerformLoadWithoutCheckpointRestore()
	{
		if (Recorder.IsPlaying)
		{
			return Recorder.Instance.OnPerformLoad();
		}
		return this.CanPerformLoad() && this.LoadFromFile(this.GetSaveFilePath(this.CurrentSlotIndex, this.CurrentBackupIndex));
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0002F614 File Offset: 0x0002D814
	public bool OnLoadComplete(byte[] buffer)
	{
		bool result = this.LoadFromBytes(buffer);
		this.RestoreCheckpoint();
		return result;
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0002F630 File Offset: 0x0002D830
	public void PerformSave()
	{
		if (!this.CanPerformSave())
		{
			return;
		}
		SaveSlotsManager.CurrentSaveSlot.FillData();
		SaveSlotsManager.BackupIndex = -1;
		this.SaveToFile(this.CurrentSaveFilePath);
		if (Recorder.IsRecordering)
		{
			Recorder.Instance.OnPerformSave();
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0002F679 File Offset: 0x0002D879
	public bool CanPerformLoad()
	{
		return !GameController.Instance.IsDemo;
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0002F68D File Offset: 0x0002D88D
	public bool CanPerformSave()
	{
		return !Recorder.IsPlaying && !GameController.Instance.IsDemo;
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0002F6AD File Offset: 0x0002D8AD
	public void OnSaveComplete()
	{
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0002F6AF File Offset: 0x0002D8AF
	public void RestoreCheckpoint()
	{
		GameController.Instance.IsLoadingGame = true;
		LateStartHook.AddLateStartMethod(new Action(this.RestoreCheckpointPart1));
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0002F6D0 File Offset: 0x0002D8D0
	public void RestoreCheckpointPart1()
	{
		GameController.Instance.IsLoadingGame = true;
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
		HashSet<SaveSerialize> hashSet = new HashSet<SaveSerialize>();
		hashSet.Add(Scenes.Manager);
		hashSet.Add(GameController.Instance);
		hashSet.Add(SeinWorldState.Instance);
		SaveSceneManager.Master.Load(Game.Checkpoint.SaveGameData.Master, hashSet);
		Scenes.Manager.AutoLoadingUnloading = false;
		GoToSceneController.Instance.StartInScene = MoonGuid.Empty;
		Game.Checkpoint.SaveGameData.ClearPendingScenes();
		Scenes.Manager.MarkLoadingScenesAsCancel();
		if (this.SaveWasOneLifeAndKilled)
		{
			RuntimeSceneMetaData sceneInformation = Scenes.Manager.GetSceneInformation("sunkenGladesRunaway");
			GameController.Instance.RequireInitialValues = true;
			GameStateMachine.Instance.SetToGame();
			DifficultyController.Instance.ChangeDifficulty(DifficultyMode.OneLife);
			GoToSceneController.Instance.StartInScene = sceneInformation.SceneMoonGuid;
			GameController.Instance.IsLoadingGame = false;
			GoToSceneController.Instance.GoToSceneAsync(sceneInformation, new Action(this.OnFinishedLoading), false);
		}
		else
		{
			InstantLoadScenesController.Instance.OnScenesEnabledCallback = new Action(this.OnFinishedLoading);
			InstantLoadScenesController.Instance.LoadScenesAtPosition(null, true, false);
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0002F7F5 File Offset: 0x0002D9F5
	public void OnFinishedLoading()
	{
		GameController.Instance.MainMenuCanBeOpened = true;
		UI.Cameras.Current.Controller.PuppetController.Reset();
		GameController.Instance.RestoreCheckpointImmediate();
		Scenes.Manager.MarkActiveScenesAsKeepLoaded();
	}

	// Token: 0x040008EE RID: 2286
	public const int MAX_SAVES = 10;

	// Token: 0x040008EF RID: 2287
	private float m_startTime;
}
