using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000045 RID: 69
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum MmapProts
	{
		// Token: 0x0400032A RID: 810
		PROT_READ = 1,
		// Token: 0x0400032B RID: 811
		PROT_WRITE = 2,
		// Token: 0x0400032C RID: 812
		PROT_EXEC = 4,
		// Token: 0x0400032D RID: 813
		PROT_NONE = 0,
		// Token: 0x0400032E RID: 814
		PROT_GROWSDOWN = 16777216,
		// Token: 0x0400032F RID: 815
		PROT_GROWSUP = 33554432
	}
}
