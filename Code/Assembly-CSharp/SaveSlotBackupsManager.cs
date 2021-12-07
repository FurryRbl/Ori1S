using System;
using System.Collections.Generic;
using System.IO;
using Game;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class SaveSlotBackupsManager : MonoBehaviour
{
	// Token: 0x06001448 RID: 5192 RVA: 0x0005BF8C File Offset: 0x0005A18C
	public void Awake()
	{
		SaveSlotBackupsManager.m_instance = this;
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		XboxOneSave.OnSaveGameCacheCleared += this.OnSaveGameCacheCleared;
		this.ClearCache();
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x0005BFD4 File Offset: 0x0005A1D4
	public void OnDestroy()
	{
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		XboxOneSave.OnSaveGameCacheCleared -= this.OnSaveGameCacheCleared;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x0005C00D File Offset: 0x0005A20D
	public void OnGameReset()
	{
		this.ClearCache();
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0005C015 File Offset: 0x0005A215
	public void OnSaveGameCacheCleared()
	{
		this.ClearCache();
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0005C020 File Offset: 0x0005A220
	public static void RequestReadBackups(int slotIndex, Action onFinishedReading)
	{
		SaveSlotBackupsManager.m_instance.m_currentReadingSlot = slotIndex;
		SaveSlotBackup saveSlotBackup = SaveSlotBackupsManager.m_instance.FindByIndex(slotIndex);
		if (saveSlotBackup.IsLoaded)
		{
			if (onFinishedReading != null)
			{
				onFinishedReading();
			}
		}
		else
		{
			SaveSlotBackupsManager.m_instance.m_onFinishedReaded = onFinishedReading;
		}
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x0005C06B File Offset: 0x0005A26B
	public static SaveSlotBackup SaveSlotBackupAtIndex(int index)
	{
		return SaveSlotBackupsManager.m_instance.m_saveSlotBackups[index];
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0005C080 File Offset: 0x0005A280
	public static void DeleteAllBackups(int slotIndex)
	{
		SaveSlotBackup saveSlotBackup = SaveSlotBackupsManager.SaveSlotBackupAtIndex(slotIndex);
		for (int i = 0; i < 5; i++)
		{
			string path = SaveSlotBackupsManager.m_instance.BackupName(slotIndex, i);
			File.Delete(path);
		}
		for (int j = 0; j < saveSlotBackup.SaveSlotInfos.Length; j++)
		{
			saveSlotBackup.SaveSlotInfos[j] = null;
		}
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x0005C0DC File Offset: 0x0005A2DC
	public static void CreateCurrentBackup()
	{
		try
		{
			if (Time.realtimeSinceStartup >= SaveSlotBackupsManager.m_instance.m_lastSaveTime + 60f)
			{
				SaveSlotBackupsManager.m_instance.m_lastSaveTime = Time.realtimeSinceStartup;
				SaveSlotBackupsManager.m_instance.CreateBackup(SaveSlotsManager.CurrentSlotIndex);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x0005C148 File Offset: 0x0005A348
	public static void ResetBackupDelay()
	{
		if (SaveSlotBackupsManager.m_instance)
		{
			SaveSlotBackupsManager.m_instance.m_lastSaveTime = 0f;
		}
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x0005C168 File Offset: 0x0005A368
	public void RestoreBackup(int slotIndex, int backupIndex)
	{
		string filename = this.BackupName(slotIndex, backupIndex);
		GameController.Instance.SaveGameController.LoadFromFile(filename);
		GameController.Instance.SaveGameController.RestoreCheckpoint();
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x0005C1A0 File Offset: 0x0005A3A0
	private void CreateBackup(int slotIndex)
	{
		SaveGameController saveGameController = GameController.Instance.SaveGameController;
		SaveSlotBackup saveSlotBackup = this.FindByIndex(slotIndex);
		int num = saveSlotBackup.IndexOfOldestSaveSlotInfo();
		string destFileName = this.BackupName(slotIndex, num);
		File.Copy(saveGameController.GetSaveFilePath(SaveSlotsManager.CurrentSlotIndex, -1), destFileName, true);
		SaveSlotInfo saveSlot = new SaveSlotInfo(SaveSlotsManager.CurrentSaveSlot);
		saveSlotBackup.SaveSlotInfos[num] = new SaveSlotBackupInfo(num, saveSlot);
		if (saveSlotBackup.Count < 5)
		{
			saveSlotBackup.Count++;
		}
		SaveSlotsManager.CurrentSaveSlot.Order = saveSlotBackup.GetLargestOrderValue();
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0005C22C File Offset: 0x0005A42C
	public void Update()
	{
		if (this.IsBusyLoading())
		{
			return;
		}
		if (this.m_createBackupPending)
		{
			this.m_createBackupPending = false;
			XboxOneSave.WriteSaveGame(this.m_backupBytes, this.m_backupName, null, null);
			this.m_backupBytes = null;
		}
		if (this.IsBusyLoading())
		{
			return;
		}
		if (this.m_buffersToDelete.Count > 0)
		{
			int[] array = this.m_buffersToDelete.Pop();
			XboxOneSave.DeleteSaveGame(array[0], array[1]);
		}
		if (this.IsBusyLoading())
		{
			return;
		}
		if (this.m_currentReadingSlot != -1)
		{
			SaveSlotBackup saveSlotBackup = this.FindByIndex(this.m_currentReadingSlot);
			if (!saveSlotBackup.IsLoaded)
			{
				this.LookForBackup(this.m_currentReadingSlot, saveSlotBackup.Count);
			}
			if (saveSlotBackup.IsLoaded)
			{
				if (this.m_onFinishedReaded != null)
				{
					this.m_onFinishedReaded();
					this.m_onFinishedReaded = null;
				}
				this.m_currentReadingSlot = -1;
			}
		}
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x0005C318 File Offset: 0x0005A518
	private void ClearCache()
	{
		this.m_saveSlotBackups.Clear();
		for (int i = 0; i < 10; i++)
		{
			this.m_saveSlotBackups.Add(new SaveSlotBackup(i));
		}
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x0005C354 File Offset: 0x0005A554
	private SaveSlotBackup FindByIndex(int index)
	{
		return this.m_saveSlotBackups[index];
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x0005C364 File Offset: 0x0005A564
	private string BackupName(int slot, int index)
	{
		return Path.Combine(OutputFolder.PlayerDataFolderPath, string.Format("saveFile{0}_bkup{1}.sav", slot, index));
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x0005C394 File Offset: 0x0005A594
	private void LookForBackup(int slotIndex, int backupIndex)
	{
		SaveSlotBackup saveSlotBackup = this.FindByIndex(this.m_currentReadingSlot);
		string path = this.BackupName(slotIndex, backupIndex);
		if (File.Exists(path))
		{
			using (BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
			{
				SaveSlotInfo saveSlotInfo = new SaveSlotInfo();
				if (saveSlotInfo.LoadFromReader(binaryReader))
				{
					saveSlotBackup.SaveSlotInfos[backupIndex] = new SaveSlotBackupInfo(backupIndex, saveSlotInfo);
				}
				else
				{
					saveSlotBackup.SaveSlotInfos[backupIndex] = null;
				}
			}
			if (backupIndex + 1 == 5)
			{
				saveSlotBackup.IsLoaded = true;
			}
			if (saveSlotBackup.Count < 5)
			{
				saveSlotBackup.Count++;
			}
		}
		else
		{
			saveSlotBackup.IsLoaded = true;
		}
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x0005C458 File Offset: 0x0005A658
	private bool IsBusyLoading()
	{
		return false;
	}

	// Token: 0x040011C0 RID: 4544
	public const float TIME_BETWEEN_SAVES = 60f;

	// Token: 0x040011C1 RID: 4545
	private static SaveSlotBackupsManager m_instance;

	// Token: 0x040011C2 RID: 4546
	private byte[] m_backupBytes;

	// Token: 0x040011C3 RID: 4547
	private string m_backupName;

	// Token: 0x040011C4 RID: 4548
	private readonly Stack<int[]> m_buffersToDelete = new Stack<int[]>();

	// Token: 0x040011C5 RID: 4549
	private bool m_createBackupPending;

	// Token: 0x040011C6 RID: 4550
	private int m_currentReadingSlot = -1;

	// Token: 0x040011C7 RID: 4551
	private float m_lastSaveTime;

	// Token: 0x040011C8 RID: 4552
	private Action m_onFinishedReaded;

	// Token: 0x040011C9 RID: 4553
	private readonly List<SaveSlotBackup> m_saveSlotBackups = new List<SaveSlotBackup>();
}
