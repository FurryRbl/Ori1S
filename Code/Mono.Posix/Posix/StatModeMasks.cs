using System;

namespace Mono.Posix
{
	// Token: 0x02000073 RID: 115
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	public enum StatModeMasks
	{
		// Token: 0x040003F4 RID: 1012
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFMT")]
		TypeMask = 61440,
		// Token: 0x040003F5 RID: 1013
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXU")]
		OwnerMask = 448,
		// Token: 0x040003F6 RID: 1014
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXG")]
		GroupMask = 56,
		// Token: 0x040003F7 RID: 1015
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXO")]
		OthersMask = 7
	}
}
