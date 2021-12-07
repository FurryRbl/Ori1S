using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000043 RID: 67
	[CLSCompliant(false)]
	[Flags]
	[Map]
	public enum MountFlags : ulong
	{
		// Token: 0x0400030D RID: 781
		ST_RDONLY = 1UL,
		// Token: 0x0400030E RID: 782
		ST_NOSUID = 2UL,
		// Token: 0x0400030F RID: 783
		ST_NODEV = 4UL,
		// Token: 0x04000310 RID: 784
		ST_NOEXEC = 8UL,
		// Token: 0x04000311 RID: 785
		ST_SYNCHRONOUS = 16UL,
		// Token: 0x04000312 RID: 786
		ST_REMOUNT = 32UL,
		// Token: 0x04000313 RID: 787
		ST_MANDLOCK = 64UL,
		// Token: 0x04000314 RID: 788
		ST_WRITE = 128UL,
		// Token: 0x04000315 RID: 789
		ST_APPEND = 256UL,
		// Token: 0x04000316 RID: 790
		ST_IMMUTABLE = 512UL,
		// Token: 0x04000317 RID: 791
		ST_NOATIME = 1024UL,
		// Token: 0x04000318 RID: 792
		ST_NODIRATIME = 2048UL,
		// Token: 0x04000319 RID: 793
		ST_BIND = 4096UL
	}
}
