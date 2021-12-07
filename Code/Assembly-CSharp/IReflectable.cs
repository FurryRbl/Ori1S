using System;
using UnityEngine;

// Token: 0x020008D6 RID: 2262
internal interface IReflectable
{
	// Token: 0x17000802 RID: 2050
	// (get) Token: 0x06003258 RID: 12888
	// (set) Token: 0x06003259 RID: 12889
	Vector3 Direction { get; set; }

	// Token: 0x17000803 RID: 2051
	// (get) Token: 0x0600325A RID: 12890
	// (set) Token: 0x0600325B RID: 12891
	float Speed { get; set; }

	// Token: 0x17000804 RID: 2052
	// (get) Token: 0x0600325C RID: 12892
	// (set) Token: 0x0600325D RID: 12893
	GameObject LastReflector { get; set; }

	// Token: 0x0600325E RID: 12894
	bool CanBeReflected(float maximumReflectableDamage);

	// Token: 0x0600325F RID: 12895
	void OnGrabbed();

	// Token: 0x06003260 RID: 12896
	void OnReleased(float speed, Vector3 direction);
}
