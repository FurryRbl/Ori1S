using System;

namespace Mono.Posix
{
	// Token: 0x0200006E RID: 110
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	[CLSCompliant(false)]
	[Flags]
	public enum FileMode
	{
		// Token: 0x040003C1 RID: 961
		S_ISUID = 2048,
		// Token: 0x040003C2 RID: 962
		S_ISGID = 1024,
		// Token: 0x040003C3 RID: 963
		S_ISVTX = 512,
		// Token: 0x040003C4 RID: 964
		S_IRUSR = 256,
		// Token: 0x040003C5 RID: 965
		S_IWUSR = 128,
		// Token: 0x040003C6 RID: 966
		S_IXUSR = 64,
		// Token: 0x040003C7 RID: 967
		S_IRGRP = 32,
		// Token: 0x040003C8 RID: 968
		S_IWGRP = 16,
		// Token: 0x040003C9 RID: 969
		S_IXGRP = 8,
		// Token: 0x040003CA RID: 970
		S_IROTH = 4,
		// Token: 0x040003CB RID: 971
		S_IWOTH = 2,
		// Token: 0x040003CC RID: 972
		S_IXOTH = 1
	}
}
