using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200003B RID: 59
	[Map]
	[Flags]
	public enum WaitOptions
	{
		// Token: 0x040001E4 RID: 484
		WNOHANG = 1,
		// Token: 0x040001E5 RID: 485
		WUNTRACED = 2
	}
}
