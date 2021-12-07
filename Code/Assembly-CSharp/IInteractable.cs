using System;
using UnityEngine;

// Token: 0x02000831 RID: 2097
public interface IInteractable
{
	// Token: 0x06002FDA RID: 12250
	void SetInteraction(float time, Vector3 position, Vector3 prevPos, Vector4 strength, Vector3 velocity, float radius, bool explosion);

	// Token: 0x06002FDB RID: 12251
	bool DoesOverlap(Vector3 position, Vector3 velocity, float radius, float zScale);

	// Token: 0x06002FDC RID: 12252
	Vector3 GetPosition();

	// Token: 0x06002FDD RID: 12253
	Vector3 GetExplodePoint(Vector3 position);

	// Token: 0x06002FDE RID: 12254
	float MaxRadius();

	// Token: 0x06002FDF RID: 12255
	void OnRegistered();

	// Token: 0x06002FE0 RID: 12256
	bool IsWater();

	// Token: 0x170007A9 RID: 1961
	// (get) Token: 0x06002FE1 RID: 12257
	// (set) Token: 0x06002FE2 RID: 12258
	int Index { get; set; }

	// Token: 0x170007AA RID: 1962
	// (get) Token: 0x06002FE3 RID: 12259
	// (set) Token: 0x06002FE4 RID: 12260
	bool IsRegistered { get; set; }
}
