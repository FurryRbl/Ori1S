using System;
using UnityEngine;

// Token: 0x020006E2 RID: 1762
public interface IGoThroughPlatformTester
{
	// Token: 0x170006AE RID: 1710
	// (get) Token: 0x06002A18 RID: 10776
	Ray GoThroughPlatformTestingRayLeft { get; }

	// Token: 0x170006AF RID: 1711
	// (get) Token: 0x06002A19 RID: 10777
	Ray GoThroughPlatformTestingRayRight { get; }

	// Token: 0x170006B0 RID: 1712
	// (get) Token: 0x06002A1A RID: 10778
	Collider GoThroughPlatformTesterCollider { get; }

	// Token: 0x170006B1 RID: 1713
	// (get) Token: 0x06002A1B RID: 10779
	float GoThroughPlatformTestingRayRadius { get; }
}
