using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200003C RID: 60
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum AccessModes
	{
		// Token: 0x040001E7 RID: 487
		R_OK = 1,
		// Token: 0x040001E8 RID: 488
		W_OK = 2,
		// Token: 0x040001E9 RID: 489
		X_OK = 4,
		// Token: 0x040001EA RID: 490
		F_OK = 8
	}
}
