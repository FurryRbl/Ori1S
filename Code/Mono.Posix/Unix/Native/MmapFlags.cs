using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000044 RID: 68
	[CLSCompliant(false)]
	[Flags]
	[Map]
	public enum MmapFlags
	{
		// Token: 0x0400031B RID: 795
		MAP_SHARED = 1,
		// Token: 0x0400031C RID: 796
		MAP_PRIVATE = 2,
		// Token: 0x0400031D RID: 797
		MAP_TYPE = 15,
		// Token: 0x0400031E RID: 798
		MAP_FIXED = 16,
		// Token: 0x0400031F RID: 799
		MAP_FILE = 0,
		// Token: 0x04000320 RID: 800
		MAP_ANONYMOUS = 32,
		// Token: 0x04000321 RID: 801
		MAP_ANON = 32,
		// Token: 0x04000322 RID: 802
		MAP_GROWSDOWN = 256,
		// Token: 0x04000323 RID: 803
		MAP_DENYWRITE = 2048,
		// Token: 0x04000324 RID: 804
		MAP_EXECUTABLE = 4096,
		// Token: 0x04000325 RID: 805
		MAP_LOCKED = 8192,
		// Token: 0x04000326 RID: 806
		MAP_NORESERVE = 16384,
		// Token: 0x04000327 RID: 807
		MAP_POPULATE = 32768,
		// Token: 0x04000328 RID: 808
		MAP_NONBLOCK = 65536
	}
}
