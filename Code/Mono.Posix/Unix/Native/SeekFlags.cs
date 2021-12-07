using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000036 RID: 54
	[Map]
	[CLSCompliant(false)]
	public enum SeekFlags : short
	{
		// Token: 0x040001A4 RID: 420
		SEEK_SET,
		// Token: 0x040001A5 RID: 421
		SEEK_CUR,
		// Token: 0x040001A6 RID: 422
		SEEK_END,
		// Token: 0x040001A7 RID: 423
		L_SET = 0,
		// Token: 0x040001A8 RID: 424
		L_INCR,
		// Token: 0x040001A9 RID: 425
		L_XTND
	}
}
