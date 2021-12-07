using System;
using UnityEngine;

// Token: 0x020006E3 RID: 1763
public interface IPortalVisitor
{
	// Token: 0x170006B2 RID: 1714
	// (get) Token: 0x06002A1C RID: 10780
	// (set) Token: 0x06002A1D RID: 10781
	Vector3 Position { get; set; }

	// Token: 0x170006B3 RID: 1715
	// (get) Token: 0x06002A1E RID: 10782
	// (set) Token: 0x06002A1F RID: 10783
	Vector3 Speed { get; set; }

	// Token: 0x06002A20 RID: 10784
	void OnGoThroughPortal();

	// Token: 0x06002A21 RID: 10785
	void OnPortalOverlapEnter();

	// Token: 0x06002A22 RID: 10786
	void OnPortalOverlapExit();
}
