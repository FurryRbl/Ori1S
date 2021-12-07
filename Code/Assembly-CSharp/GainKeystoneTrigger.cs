using System;
using Game;

// Token: 0x02000367 RID: 871
public class GainKeystoneTrigger : Trigger
{
	// Token: 0x060018FB RID: 6395 RVA: 0x0006A958 File Offset: 0x00068B58
	public void Start()
	{
		Characters.Sein.Inventory.OnCollectKeystones += this.OnCollectKeystones;
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x0006A975 File Offset: 0x00068B75
	public new void OnDestroy()
	{
		base.OnDestroy();
		Characters.Sein.Inventory.OnCollectKeystones -= this.OnCollectKeystones;
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x0006A998 File Offset: 0x00068B98
	public void OnCollectKeystones()
	{
		base.DoTrigger(true);
	}
}
