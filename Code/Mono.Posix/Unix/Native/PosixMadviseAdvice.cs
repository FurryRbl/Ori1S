using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000039 RID: 57
	[Map]
	[CLSCompliant(false)]
	public enum PosixMadviseAdvice
	{
		// Token: 0x040001BA RID: 442
		POSIX_MADV_NORMAL,
		// Token: 0x040001BB RID: 443
		POSIX_MADV_RANDOM,
		// Token: 0x040001BC RID: 444
		POSIX_MADV_SEQUENTIAL,
		// Token: 0x040001BD RID: 445
		POSIX_MADV_WILLNEED,
		// Token: 0x040001BE RID: 446
		POSIX_MADV_DONTNEED
	}
}
