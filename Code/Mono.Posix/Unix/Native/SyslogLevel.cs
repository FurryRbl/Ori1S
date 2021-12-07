using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000031 RID: 49
	[CLSCompliant(false)]
	[Map]
	public enum SyslogLevel
	{
		// Token: 0x0400015C RID: 348
		LOG_EMERG,
		// Token: 0x0400015D RID: 349
		LOG_ALERT,
		// Token: 0x0400015E RID: 350
		LOG_CRIT,
		// Token: 0x0400015F RID: 351
		LOG_ERR,
		// Token: 0x04000160 RID: 352
		LOG_WARNING,
		// Token: 0x04000161 RID: 353
		LOG_NOTICE,
		// Token: 0x04000162 RID: 354
		LOG_INFO,
		// Token: 0x04000163 RID: 355
		LOG_DEBUG
	}
}
