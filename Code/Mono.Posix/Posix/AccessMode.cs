using System;

namespace Mono.Posix
{
	// Token: 0x02000070 RID: 112
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.AccessModes")]
	[Flags]
	public enum AccessMode
	{
		// Token: 0x040003D1 RID: 977
		R_OK = 1,
		// Token: 0x040003D2 RID: 978
		W_OK = 2,
		// Token: 0x040003D3 RID: 979
		X_OK = 4,
		// Token: 0x040003D4 RID: 980
		F_OK = 8
	}
}
