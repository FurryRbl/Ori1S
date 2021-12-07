using System;

namespace UnityEngine
{
	// Token: 0x020001D2 RID: 466
	[Flags]
	internal enum TerrainChangedFlags
	{
		// Token: 0x040005AC RID: 1452
		NoChange = 0,
		// Token: 0x040005AD RID: 1453
		Heightmap = 1,
		// Token: 0x040005AE RID: 1454
		TreeInstances = 2,
		// Token: 0x040005AF RID: 1455
		DelayedHeightmapUpdate = 4,
		// Token: 0x040005B0 RID: 1456
		FlushEverythingImmediately = 8,
		// Token: 0x040005B1 RID: 1457
		RemoveDirtyDetailsImmediately = 16,
		// Token: 0x040005B2 RID: 1458
		WillBeDestroyed = 256
	}
}
