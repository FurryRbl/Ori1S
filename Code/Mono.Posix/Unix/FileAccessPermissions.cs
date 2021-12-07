using System;

namespace Mono.Unix
{
	// Token: 0x02000006 RID: 6
	[Flags]
	public enum FileAccessPermissions
	{
		// Token: 0x04000020 RID: 32
		UserReadWriteExecute = 448,
		// Token: 0x04000021 RID: 33
		UserRead = 256,
		// Token: 0x04000022 RID: 34
		UserWrite = 128,
		// Token: 0x04000023 RID: 35
		UserExecute = 64,
		// Token: 0x04000024 RID: 36
		GroupReadWriteExecute = 56,
		// Token: 0x04000025 RID: 37
		GroupRead = 32,
		// Token: 0x04000026 RID: 38
		GroupWrite = 16,
		// Token: 0x04000027 RID: 39
		GroupExecute = 8,
		// Token: 0x04000028 RID: 40
		OtherReadWriteExecute = 7,
		// Token: 0x04000029 RID: 41
		OtherRead = 4,
		// Token: 0x0400002A RID: 42
		OtherWrite = 2,
		// Token: 0x0400002B RID: 43
		OtherExecute = 1,
		// Token: 0x0400002C RID: 44
		DefaultPermissions = 438,
		// Token: 0x0400002D RID: 45
		AllPermissions = 511
	}
}
