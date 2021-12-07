using System;

namespace Mono.Posix
{
	// Token: 0x0200006F RID: 111
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.WaitOptions")]
	public enum WaitOptions
	{
		// Token: 0x040003CE RID: 974
		WNOHANG = 0,
		// Token: 0x040003CF RID: 975
		WUNTRACED = 1
	}
}
