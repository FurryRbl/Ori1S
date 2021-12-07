using System;
using System.Collections.Generic;

// Token: 0x02000708 RID: 1800
public class XboxOneSave
{
	// Token: 0x1400003F RID: 63
	// (add) Token: 0x06002AB9 RID: 10937 RVA: 0x000B713D File Offset: 0x000B533D
	// (remove) Token: 0x06002ABA RID: 10938 RVA: 0x000B7154 File Offset: 0x000B5354
	public static event Action OnSaveGameCacheCleared;

	// Token: 0x06002ABB RID: 10939 RVA: 0x000B716B File Offset: 0x000B536B
	public static void OnSlotsAllQueried()
	{
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x000B716D File Offset: 0x000B536D
	public static bool SaveGameAvailableInSlot(int index)
	{
		return false;
	}

	// Token: 0x06002ABD RID: 10941 RVA: 0x000B7170 File Offset: 0x000B5370
	public static bool DeleteSaveGame(int index, int backupIndex = -1)
	{
		return false;
	}

	// Token: 0x170006D0 RID: 1744
	// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000B7173 File Offset: 0x000B5373
	public static bool SaveGameAvailable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170006D1 RID: 1745
	// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000B7176 File Offset: 0x000B5376
	public static bool SaveGameQueried
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06002AC0 RID: 10944 RVA: 0x000B7179 File Offset: 0x000B5379
	public static byte[] SaveGameData(int slot, int backup = -1)
	{
		return null;
	}

	// Token: 0x06002AC1 RID: 10945 RVA: 0x000B717C File Offset: 0x000B537C
	public static byte[] SaveGameData(string bufferName)
	{
		return null;
	}

	// Token: 0x170006D2 RID: 1746
	// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000B717F File Offset: 0x000B537F
	// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x000B7186 File Offset: 0x000B5386
	public static bool EnableSave { get; set; }

	// Token: 0x06002AC4 RID: 10948 RVA: 0x000B718E File Offset: 0x000B538E
	public static void Update()
	{
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x000B7190 File Offset: 0x000B5390
	public static bool ClearSaveGameCache()
	{
		return false;
	}

	// Token: 0x06002AC6 RID: 10950 RVA: 0x000B7193 File Offset: 0x000B5393
	public static bool RequireStorage(Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06002AC7 RID: 10951 RVA: 0x000B7196 File Offset: 0x000B5396
	public static string SaveGameName(int slotIndex, int backupIndex = -1)
	{
		return null;
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x000B7199 File Offset: 0x000B5399
	public static bool CopySaveGame(int slotIndexFrom, int slotIndexTo)
	{
		return false;
	}

	// Token: 0x06002AC9 RID: 10953 RVA: 0x000B719C File Offset: 0x000B539C
	public static bool UpdateSaveGame(int slotIndex, Action success = null, Action failure = null)
	{
		return XboxOneSave.UpdateSaveGame(XboxOneSave.SaveGameName(slotIndex, -1), success, failure);
	}

	// Token: 0x170006D3 RID: 1747
	// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000B71AC File Offset: 0x000B53AC
	public static bool IsStorageBusy
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170006D4 RID: 1748
	// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000B71AF File Offset: 0x000B53AF
	public static bool StorageHasOperationsInProgress
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06002ACC RID: 10956 RVA: 0x000B71B2 File Offset: 0x000B53B2
	public static bool UpdateSaveGame(string bufferName, Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06002ACD RID: 10957 RVA: 0x000B71B5 File Offset: 0x000B53B5
	public static bool WriteSaveGame(byte[] data, int saveSlot, Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06002ACE RID: 10958 RVA: 0x000B71B8 File Offset: 0x000B53B8
	public static bool WriteSaveGame(byte[] data, string bufferName, Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x04002605 RID: 9733
	private static int m_waitFramesForStorage;

	// Token: 0x04002606 RID: 9734
	private Queue<Action> m_queuedStorageOperations = new Queue<Action>();

	// Token: 0x04002607 RID: 9735
	private string m_bufferName;
}
