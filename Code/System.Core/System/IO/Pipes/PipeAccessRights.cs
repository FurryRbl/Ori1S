using System;

namespace System.IO.Pipes
{
	// Token: 0x0200006C RID: 108
	[Flags]
	public enum PipeAccessRights
	{
		// Token: 0x0400017D RID: 381
		ReadData = 1,
		// Token: 0x0400017E RID: 382
		WriteData = 2,
		// Token: 0x0400017F RID: 383
		ReadAttributes = 4,
		// Token: 0x04000180 RID: 384
		WriteAttributes = 8,
		// Token: 0x04000181 RID: 385
		ReadExtendedAttributes = 16,
		// Token: 0x04000182 RID: 386
		WriteExtendedAttributes = 32,
		// Token: 0x04000183 RID: 387
		CreateNewInstance = 64,
		// Token: 0x04000184 RID: 388
		Delete = 128,
		// Token: 0x04000185 RID: 389
		ReadPermissions = 256,
		// Token: 0x04000186 RID: 390
		ChangePermissions = 512,
		// Token: 0x04000187 RID: 391
		TakeOwnership = 1024,
		// Token: 0x04000188 RID: 392
		Synchronize = 2048,
		// Token: 0x04000189 RID: 393
		FullControl = 1855,
		// Token: 0x0400018A RID: 394
		Read = 277,
		// Token: 0x0400018B RID: 395
		Write = 554,
		// Token: 0x0400018C RID: 396
		ReadWrite = 831,
		// Token: 0x0400018D RID: 397
		AccessSystemSecurity = 1792
	}
}
