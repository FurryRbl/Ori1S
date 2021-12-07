using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000032 RID: 50
	[CLSCompliant(false)]
	[Map]
	[Flags]
	public enum OpenFlags
	{
		// Token: 0x04000165 RID: 357
		O_RDONLY = 0,
		// Token: 0x04000166 RID: 358
		O_WRONLY = 1,
		// Token: 0x04000167 RID: 359
		O_RDWR = 2,
		// Token: 0x04000168 RID: 360
		O_CREAT = 64,
		// Token: 0x04000169 RID: 361
		O_EXCL = 128,
		// Token: 0x0400016A RID: 362
		O_NOCTTY = 256,
		// Token: 0x0400016B RID: 363
		O_TRUNC = 512,
		// Token: 0x0400016C RID: 364
		O_APPEND = 1024,
		// Token: 0x0400016D RID: 365
		O_NONBLOCK = 2048,
		// Token: 0x0400016E RID: 366
		O_SYNC = 4096,
		// Token: 0x0400016F RID: 367
		O_NOFOLLOW = 131072,
		// Token: 0x04000170 RID: 368
		O_DIRECTORY = 65536,
		// Token: 0x04000171 RID: 369
		O_DIRECT = 16384,
		// Token: 0x04000172 RID: 370
		O_ASYNC = 8192,
		// Token: 0x04000173 RID: 371
		O_LARGEFILE = 32768
	}
}
