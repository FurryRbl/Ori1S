using System;

// Token: 0x020004A9 RID: 1193
public interface IDebugMenuToggleable
{
	// Token: 0x1700058C RID: 1420
	// (get) Token: 0x06002083 RID: 8323
	string Name { get; }

	// Token: 0x1700058D RID: 1421
	// (get) Token: 0x06002084 RID: 8324
	string HelpText { get; }

	// Token: 0x1700058E RID: 1422
	// (get) Token: 0x06002085 RID: 8325
	string[] ToggleOptions { get; }

	// Token: 0x1700058F RID: 1423
	// (get) Token: 0x06002086 RID: 8326
	// (set) Token: 0x06002087 RID: 8327
	int CurrentToggleOptionId { get; set; }
}
