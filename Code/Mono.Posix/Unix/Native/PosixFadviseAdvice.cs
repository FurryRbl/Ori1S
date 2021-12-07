using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000038 RID: 56
	[Map]
	[CLSCompliant(false)]
	public enum PosixFadviseAdvice
	{
		// Token: 0x040001B3 RID: 435
		POSIX_FADV_NORMAL,
		// Token: 0x040001B4 RID: 436
		POSIX_FADV_RANDOM,
		// Token: 0x040001B5 RID: 437
		POSIX_FADV_SEQUENTIAL,
		// Token: 0x040001B6 RID: 438
		POSIX_FADV_WILLNEED,
		// Token: 0x040001B7 RID: 439
		POSIX_FADV_DONTNEED,
		// Token: 0x040001B8 RID: 440
		POSIX_FADV_NOREUSE
	}
}
