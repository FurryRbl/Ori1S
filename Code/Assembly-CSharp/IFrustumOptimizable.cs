using System;
using UnityEngine;

// Token: 0x02000540 RID: 1344
public interface IFrustumOptimizable
{
	// Token: 0x0600234A RID: 9034
	void OnFrustumEnter();

	// Token: 0x0600234B RID: 9035
	void OnFrustumExit();

	// Token: 0x170005FB RID: 1531
	// (get) Token: 0x0600234C RID: 9036
	bool InsideFrustum { get; }

	// Token: 0x170005FC RID: 1532
	// (get) Token: 0x0600234D RID: 9037
	Bounds Bounds { get; }
}
