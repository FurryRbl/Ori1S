using System;
using UnityEngine;

// Token: 0x020008D1 RID: 2257
public interface ILocationInformation
{
	// Token: 0x170007F8 RID: 2040
	// (get) Token: 0x06003241 RID: 12865
	string SceneName { get; }

	// Token: 0x170007F9 RID: 2041
	// (get) Token: 0x06003242 RID: 12866
	string TargetName { get; }

	// Token: 0x170007FA RID: 2042
	// (get) Token: 0x06003243 RID: 12867
	Transform TargetTransform { get; }

	// Token: 0x06003244 RID: 12868
	Vector3 TargetOffset(Transform other);

	// Token: 0x170007FB RID: 2043
	// (get) Token: 0x06003245 RID: 12869
	bool UseFader { get; }
}
