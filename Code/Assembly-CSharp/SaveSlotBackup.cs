using System;
using UnityEngine;

// Token: 0x02000706 RID: 1798
public class SaveSlotBackup
{
	// Token: 0x06002AB3 RID: 10931 RVA: 0x000B7012 File Offset: 0x000B5212
	public SaveSlotBackup(int index)
	{
		this.Index = index;
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x000B7030 File Offset: 0x000B5230
	public int IndexOfOldestSaveSlotInfo()
	{
		if (this.Count < 5)
		{
			return this.Count;
		}
		int result = 0;
		int num = int.MaxValue;
		for (int i = 0; i < this.SaveSlotInfos.Length; i++)
		{
			SaveSlotBackupInfo saveSlotBackupInfo = this.SaveSlotInfos[i];
			if (saveSlotBackupInfo == null || saveSlotBackupInfo.SaveSlotInfo == null)
			{
				return i;
			}
			if (num > saveSlotBackupInfo.SaveSlotInfo.Order)
			{
				result = i;
				num = saveSlotBackupInfo.SaveSlotInfo.Order;
			}
		}
		return result;
	}

	// Token: 0x06002AB5 RID: 10933 RVA: 0x000B70B0 File Offset: 0x000B52B0
	public int GetLargestOrderValue()
	{
		int num = 0;
		for (int i = 0; i < this.SaveSlotInfos.Length; i++)
		{
			SaveSlotBackupInfo saveSlotBackupInfo = this.SaveSlotInfos[i];
			if (saveSlotBackupInfo != null && saveSlotBackupInfo.SaveSlotInfo != null)
			{
				num = Mathf.Max(num, saveSlotBackupInfo.SaveSlotInfo.Order);
			}
		}
		return num + 1;
	}

	// Token: 0x040025FE RID: 9726
	public const int MAX_BACKUPS = 5;

	// Token: 0x040025FF RID: 9727
	public int Index;

	// Token: 0x04002600 RID: 9728
	public int Count;

	// Token: 0x04002601 RID: 9729
	public bool IsLoaded;

	// Token: 0x04002602 RID: 9730
	public SaveSlotBackupInfo[] SaveSlotInfos = new SaveSlotBackupInfo[5];
}
