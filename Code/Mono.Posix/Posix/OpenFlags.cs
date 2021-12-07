using System;

namespace Mono.Posix
{
	// Token: 0x0200006D RID: 109
	[CLSCompliant(false)]
	[Flags]
	[Obsolete("Use Mono.Unix.Native.OpenFlags")]
	public enum OpenFlags
	{
		// Token: 0x040003B6 RID: 950
		O_RDONLY = 0,
		// Token: 0x040003B7 RID: 951
		O_WRONLY = 1,
		// Token: 0x040003B8 RID: 952
		O_RDWR = 2,
		// Token: 0x040003B9 RID: 953
		O_CREAT = 4,
		// Token: 0x040003BA RID: 954
		O_EXCL = 8,
		// Token: 0x040003BB RID: 955
		O_NOCTTY = 16,
		// Token: 0x040003BC RID: 956
		O_TRUNC = 32,
		// Token: 0x040003BD RID: 957
		O_APPEND = 64,
		// Token: 0x040003BE RID: 958
		O_NONBLOCK = 128,
		// Token: 0x040003BF RID: 959
		O_SYNC = 256
	}
}
