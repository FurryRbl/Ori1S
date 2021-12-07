using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000041 RID: 65
	[Flags]
	[Map]
	public enum PollEvents : short
	{
		// Token: 0x040002FE RID: 766
		POLLIN = 1,
		// Token: 0x040002FF RID: 767
		POLLPRI = 2,
		// Token: 0x04000300 RID: 768
		POLLOUT = 4,
		// Token: 0x04000301 RID: 769
		POLLERR = 8,
		// Token: 0x04000302 RID: 770
		POLLHUP = 16,
		// Token: 0x04000303 RID: 771
		POLLNVAL = 32,
		// Token: 0x04000304 RID: 772
		POLLRDNORM = 64,
		// Token: 0x04000305 RID: 773
		POLLRDBAND = 128,
		// Token: 0x04000306 RID: 774
		POLLWRNORM = 256,
		// Token: 0x04000307 RID: 775
		POLLWRBAND = 512
	}
}
