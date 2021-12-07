using System;

// Token: 0x02000707 RID: 1799
public class SaveSlotBackupInfo
{
	// Token: 0x06002AB6 RID: 10934 RVA: 0x000B7107 File Offset: 0x000B5307
	public SaveSlotBackupInfo(int slotIndex, SaveSlotInfo saveSlot)
	{
		this.Index = slotIndex;
		this.SaveSlotInfo = saveSlot;
	}

	// Token: 0x170006CF RID: 1743
	// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x000B711D File Offset: 0x000B531D
	public int TotalSeconds
	{
		get
		{
			return this.SaveSlotInfo.TotalSeconds;
		}
	}

	// Token: 0x04002603 RID: 9731
	public int Index;

	// Token: 0x04002604 RID: 9732
	public SaveSlotInfo SaveSlotInfo;
}
