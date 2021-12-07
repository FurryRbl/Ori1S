using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class SaveSlotsManager : MonoBehaviour
{
	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002F844 File Offset: 0x0002DA44
	// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002F850 File Offset: 0x0002DA50
	public static int CurrentSlotIndex
	{
		get
		{
			return SaveSlotsManager.Instance.m_currentSlotIndex;
		}
		set
		{
			SaveSlotsManager.Instance.m_currentSlotIndex = value;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002F85D File Offset: 0x0002DA5D
	// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0002F869 File Offset: 0x0002DA69
	public static int BackupIndex
	{
		get
		{
			return SaveSlotsManager.Instance.m_backupIndex;
		}
		set
		{
			SaveSlotsManager.Instance.m_backupIndex = value;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0002F876 File Offset: 0x0002DA76
	public static SaveSlotInfo CurrentSaveSlot
	{
		get
		{
			return SaveSlotsManager.FindOrCreateSaveSlot(SaveSlotsManager.CurrentSlotIndex);
		}
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0002F882 File Offset: 0x0002DA82
	public static bool SlotExists(int slotIndex)
	{
		return SaveSlotsManager.SlotByIndex(slotIndex) != null;
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x0002F890 File Offset: 0x0002DA90
	public static SaveSlotInfo FindOrCreateSaveSlot(int slotIndex)
	{
		if (!SaveSlotsManager.SlotExists(slotIndex))
		{
			SaveSlotsManager.Instance.SaveSlots[slotIndex] = new SaveSlotInfo();
		}
		return SaveSlotsManager.SlotByIndex(slotIndex);
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0002F8C4 File Offset: 0x0002DAC4
	public void Awake()
	{
		SaveSlotsManager.Instance = this;
		for (int i = 0; i < 10; i++)
		{
			this.SaveSlots.Add(null);
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002F8F8 File Offset: 0x0002DAF8
	public bool AnySaveSlotsExist
	{
		get
		{
			return this.SaveSlots.Any((SaveSlotInfo slot) => slot != null);
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002F92D File Offset: 0x0002DB2D
	public static int SaveSlotCount
	{
		get
		{
			return SaveSlotsManager.Instance.SaveSlots.Count;
		}
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0002F940 File Offset: 0x0002DB40
	public static SaveSlotInfo SlotByIndex(int index)
	{
		if (index < SaveSlotsManager.Instance.SaveSlots.Count && index >= 0)
		{
			return SaveSlotsManager.Instance.SaveSlots[index];
		}
		return null;
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0002F97C File Offset: 0x0002DB7C
	public static void CopySlot(int from, int to)
	{
		SaveSlotsManager.Instance.SaveSlots[to] = SaveSlotsManager.Instance.SaveSlots[from];
		SaveSlotBackupsManager.DeleteAllBackups(to);
		string saveFilePath = GameController.Instance.SaveGameController.GetSaveFilePath(from, -1);
		string saveFilePath2 = GameController.Instance.SaveGameController.GetSaveFilePath(to, -1);
		if (File.Exists(saveFilePath2))
		{
			File.Delete(saveFilePath2);
		}
		File.Copy(saveFilePath, saveFilePath2);
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0002F9EC File Offset: 0x0002DBEC
	public static void DeleteSlot(int index)
	{
		SaveSlotBackupsManager.DeleteAllBackups(index);
		SaveSlotsManager.Instance.SaveSlots[index] = null;
		string saveFilePath = GameController.Instance.SaveGameController.GetSaveFilePath(index, -1);
		File.Delete(saveFilePath);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0002FA28 File Offset: 0x0002DC28
	public static void PrepareSlots()
	{
		SaveSlotsManager.Instance.SaveSlots.Clear();
		for (int i = 0; i < 10; i++)
		{
			if (GameController.Instance.SaveGameController.SaveExists(i))
			{
				string saveFilePath = GameController.Instance.SaveGameController.GetSaveFilePath(i, -1);
				using (BinaryReader binaryReader = new BinaryReader(File.Open(saveFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
				{
					SaveSlotInfo saveSlotInfo = new SaveSlotInfo();
					if (saveSlotInfo.LoadFromReader(binaryReader))
					{
						if (GameController.Instance.IsTrial && !saveSlotInfo.IsTrialSave)
						{
							SaveSlotsManager.Instance.SaveSlots.Add(null);
						}
						else
						{
							SaveSlotsManager.Instance.SaveSlots.Add(saveSlotInfo);
						}
					}
					else
					{
						SaveSlotsManager.Instance.SaveSlots.Add(null);
					}
				}
			}
			else
			{
				SaveSlotsManager.Instance.SaveSlots.Add(null);
			}
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0002FB2C File Offset: 0x0002DD2C
	public bool SaveSlotCompleted(int i)
	{
		SaveSlotInfo saveSlotInfo = this.SaveSlots[i];
		return saveSlotInfo != null && saveSlotInfo.Completed;
	}

	// Token: 0x040008F0 RID: 2288
	public static SaveSlotsManager Instance;

	// Token: 0x040008F1 RID: 2289
	private int m_currentSlotIndex;

	// Token: 0x040008F2 RID: 2290
	private int m_backupIndex = -1;

	// Token: 0x040008F3 RID: 2291
	public List<SaveSlotInfo> SaveSlots = new List<SaveSlotInfo>();
}
