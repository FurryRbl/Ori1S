using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000030 RID: 48
	[Map]
	[CLSCompliant(false)]
	public enum SyslogFacility
	{
		// Token: 0x04000147 RID: 327
		LOG_KERN,
		// Token: 0x04000148 RID: 328
		LOG_USER = 8,
		// Token: 0x04000149 RID: 329
		LOG_MAIL = 16,
		// Token: 0x0400014A RID: 330
		LOG_DAEMON = 24,
		// Token: 0x0400014B RID: 331
		LOG_AUTH = 32,
		// Token: 0x0400014C RID: 332
		LOG_SYSLOG = 40,
		// Token: 0x0400014D RID: 333
		LOG_LPR = 48,
		// Token: 0x0400014E RID: 334
		LOG_NEWS = 56,
		// Token: 0x0400014F RID: 335
		LOG_UUCP = 64,
		// Token: 0x04000150 RID: 336
		LOG_CRON = 72,
		// Token: 0x04000151 RID: 337
		LOG_AUTHPRIV = 80,
		// Token: 0x04000152 RID: 338
		LOG_FTP = 88,
		// Token: 0x04000153 RID: 339
		LOG_LOCAL0 = 128,
		// Token: 0x04000154 RID: 340
		LOG_LOCAL1 = 136,
		// Token: 0x04000155 RID: 341
		LOG_LOCAL2 = 144,
		// Token: 0x04000156 RID: 342
		LOG_LOCAL3 = 152,
		// Token: 0x04000157 RID: 343
		LOG_LOCAL4 = 160,
		// Token: 0x04000158 RID: 344
		LOG_LOCAL5 = 168,
		// Token: 0x04000159 RID: 345
		LOG_LOCAL6 = 176,
		// Token: 0x0400015A RID: 346
		LOG_LOCAL7 = 184
	}
}
