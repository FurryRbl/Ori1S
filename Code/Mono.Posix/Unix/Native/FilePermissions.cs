using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000033 RID: 51
	[Flags]
	[CLSCompliant(false)]
	[Map]
	public enum FilePermissions : uint
	{
		// Token: 0x04000175 RID: 373
		S_ISUID = 2048U,
		// Token: 0x04000176 RID: 374
		S_ISGID = 1024U,
		// Token: 0x04000177 RID: 375
		S_ISVTX = 512U,
		// Token: 0x04000178 RID: 376
		S_IRUSR = 256U,
		// Token: 0x04000179 RID: 377
		S_IWUSR = 128U,
		// Token: 0x0400017A RID: 378
		S_IXUSR = 64U,
		// Token: 0x0400017B RID: 379
		S_IRGRP = 32U,
		// Token: 0x0400017C RID: 380
		S_IWGRP = 16U,
		// Token: 0x0400017D RID: 381
		S_IXGRP = 8U,
		// Token: 0x0400017E RID: 382
		S_IROTH = 4U,
		// Token: 0x0400017F RID: 383
		S_IWOTH = 2U,
		// Token: 0x04000180 RID: 384
		S_IXOTH = 1U,
		// Token: 0x04000181 RID: 385
		S_IRWXG = 56U,
		// Token: 0x04000182 RID: 386
		S_IRWXU = 448U,
		// Token: 0x04000183 RID: 387
		S_IRWXO = 7U,
		// Token: 0x04000184 RID: 388
		ACCESSPERMS = 511U,
		// Token: 0x04000185 RID: 389
		ALLPERMS = 4095U,
		// Token: 0x04000186 RID: 390
		DEFFILEMODE = 438U,
		// Token: 0x04000187 RID: 391
		S_IFMT = 61440U,
		// Token: 0x04000188 RID: 392
		[Map(SuppressFlags = "S_IFMT")]
		S_IFDIR = 16384U,
		// Token: 0x04000189 RID: 393
		[Map(SuppressFlags = "S_IFMT")]
		S_IFCHR = 8192U,
		// Token: 0x0400018A RID: 394
		[Map(SuppressFlags = "S_IFMT")]
		S_IFBLK = 24576U,
		// Token: 0x0400018B RID: 395
		[Map(SuppressFlags = "S_IFMT")]
		S_IFREG = 32768U,
		// Token: 0x0400018C RID: 396
		[Map(SuppressFlags = "S_IFMT")]
		S_IFIFO = 4096U,
		// Token: 0x0400018D RID: 397
		[Map(SuppressFlags = "S_IFMT")]
		S_IFLNK = 40960U,
		// Token: 0x0400018E RID: 398
		[Map(SuppressFlags = "S_IFMT")]
		S_IFSOCK = 49152U
	}
}
