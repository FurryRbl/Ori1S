using System;

namespace Mono.Posix
{
	// Token: 0x02000074 RID: 116
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	[Flags]
	public enum StatMode
	{
		// Token: 0x040003F9 RID: 1017
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFSOCK")]
		Socket = 49152,
		// Token: 0x040003FA RID: 1018
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFLNK")]
		SymLink = 40960,
		// Token: 0x040003FB RID: 1019
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFREG")]
		Regular = 32768,
		// Token: 0x040003FC RID: 1020
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFBLK")]
		BlockDevice = 24576,
		// Token: 0x040003FD RID: 1021
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFDIR")]
		Directory = 16384,
		// Token: 0x040003FE RID: 1022
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFCHR")]
		CharDevice = 8192,
		// Token: 0x040003FF RID: 1023
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFIFO")]
		FIFO = 4096,
		// Token: 0x04000400 RID: 1024
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISUID")]
		SUid = 2048,
		// Token: 0x04000401 RID: 1025
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISGID")]
		SGid = 1024,
		// Token: 0x04000402 RID: 1026
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISVTX")]
		Sticky = 512,
		// Token: 0x04000403 RID: 1027
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IRUSR")]
		OwnerRead = 256,
		// Token: 0x04000404 RID: 1028
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWUSR")]
		OwnerWrite = 128,
		// Token: 0x04000405 RID: 1029
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXUSR")]
		OwnerExecute = 64,
		// Token: 0x04000406 RID: 1030
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IRGRP")]
		GroupRead = 32,
		// Token: 0x04000407 RID: 1031
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWGRP")]
		GroupWrite = 16,
		// Token: 0x04000408 RID: 1032
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXGRP")]
		GroupExecute = 8,
		// Token: 0x04000409 RID: 1033
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IROTH")]
		OthersRead = 4,
		// Token: 0x0400040A RID: 1034
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWOTH")]
		OthersWrite = 2,
		// Token: 0x0400040B RID: 1035
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXOTH")]
		OthersExecute = 1
	}
}
