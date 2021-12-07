using System;

// Token: 0x020008B5 RID: 2229
public class XboxStorageController
{
	// Token: 0x060031AB RID: 12715 RVA: 0x000D3521 File Offset: 0x000D1721
	public void Destroy()
	{
	}

	// Token: 0x060031AC RID: 12716 RVA: 0x000D3523 File Offset: 0x000D1723
	public bool AskForStorageDevice(uint userIndex, bool forceShow, uint maxSizeForSave)
	{
		return true;
	}

	// Token: 0x060031AD RID: 12717 RVA: 0x000D3526 File Offset: 0x000D1726
	public void UpdateState(uint userIndex)
	{
	}

	// Token: 0x060031AE RID: 12718 RVA: 0x000D3528 File Offset: 0x000D1728
	public bool IsStorageInvalid()
	{
		return false;
	}

	// Token: 0x060031AF RID: 12719 RVA: 0x000D352B File Offset: 0x000D172B
	public bool IsStorageValid()
	{
		return true;
	}

	// Token: 0x060031B0 RID: 12720 RVA: 0x000D352E File Offset: 0x000D172E
	public bool IsEnumerating()
	{
		return false;
	}
}
