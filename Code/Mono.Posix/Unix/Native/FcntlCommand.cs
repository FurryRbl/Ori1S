using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000034 RID: 52
	[Map]
	[CLSCompliant(false)]
	public enum FcntlCommand
	{
		// Token: 0x04000190 RID: 400
		F_DUPFD,
		// Token: 0x04000191 RID: 401
		F_GETFD,
		// Token: 0x04000192 RID: 402
		F_SETFD,
		// Token: 0x04000193 RID: 403
		F_GETFL,
		// Token: 0x04000194 RID: 404
		F_SETFL,
		// Token: 0x04000195 RID: 405
		F_GETLK = 12,
		// Token: 0x04000196 RID: 406
		F_SETLK,
		// Token: 0x04000197 RID: 407
		F_SETLKW,
		// Token: 0x04000198 RID: 408
		F_SETOWN = 8,
		// Token: 0x04000199 RID: 409
		F_GETOWN,
		// Token: 0x0400019A RID: 410
		F_SETSIG,
		// Token: 0x0400019B RID: 411
		F_GETSIG,
		// Token: 0x0400019C RID: 412
		F_SETLEASE = 1024,
		// Token: 0x0400019D RID: 413
		F_GETLEASE,
		// Token: 0x0400019E RID: 414
		F_NOTIFY
	}
}
