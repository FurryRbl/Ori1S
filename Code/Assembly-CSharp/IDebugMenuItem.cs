using System;
using UnityEngine;

// Token: 0x020004A0 RID: 1184
internal interface IDebugMenuItem
{
	// Token: 0x17000581 RID: 1409
	// (get) Token: 0x0600204F RID: 8271
	// (set) Token: 0x06002050 RID: 8272
	string Text { get; set; }

	// Token: 0x17000582 RID: 1410
	// (get) Token: 0x06002051 RID: 8273
	// (set) Token: 0x06002052 RID: 8274
	string HelpText { get; set; }

	// Token: 0x06002053 RID: 8275
	void Draw(Rect rect, bool b);

	// Token: 0x06002054 RID: 8276
	void OnSelected();

	// Token: 0x06002055 RID: 8277
	void OnSelectedUpdate();

	// Token: 0x06002056 RID: 8278
	void OnSelectedFixedUpdate();
}
