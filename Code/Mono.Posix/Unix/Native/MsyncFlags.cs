using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000046 RID: 70
	[Map]
	[CLSCompliant(false)]
	[Flags]
	public enum MsyncFlags
	{
		// Token: 0x04000331 RID: 817
		MS_ASYNC = 1,
		// Token: 0x04000332 RID: 818
		MS_SYNC = 4,
		// Token: 0x04000333 RID: 819
		MS_INVALIDATE = 2
	}
}
