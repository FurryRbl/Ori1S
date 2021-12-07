using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200002F RID: 47
	[CLSCompliant(false)]
	[Flags]
	[Map]
	public enum SyslogOptions
	{
		// Token: 0x04000140 RID: 320
		LOG_PID = 1,
		// Token: 0x04000141 RID: 321
		LOG_CONS = 2,
		// Token: 0x04000142 RID: 322
		LOG_ODELAY = 4,
		// Token: 0x04000143 RID: 323
		LOG_NDELAY = 8,
		// Token: 0x04000144 RID: 324
		LOG_NOWAIT = 16,
		// Token: 0x04000145 RID: 325
		LOG_PERROR = 32
	}
}
