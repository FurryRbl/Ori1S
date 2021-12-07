﻿using System;

namespace System.IO
{
	// Token: 0x02000249 RID: 585
	internal enum MonoIOError
	{
		// Token: 0x04000B61 RID: 2913
		ERROR_SUCCESS,
		// Token: 0x04000B62 RID: 2914
		ERROR_FILE_NOT_FOUND = 2,
		// Token: 0x04000B63 RID: 2915
		ERROR_PATH_NOT_FOUND,
		// Token: 0x04000B64 RID: 2916
		ERROR_TOO_MANY_OPEN_FILES,
		// Token: 0x04000B65 RID: 2917
		ERROR_ACCESS_DENIED,
		// Token: 0x04000B66 RID: 2918
		ERROR_INVALID_HANDLE,
		// Token: 0x04000B67 RID: 2919
		ERROR_INVALID_DRIVE = 15,
		// Token: 0x04000B68 RID: 2920
		ERROR_NOT_SAME_DEVICE = 17,
		// Token: 0x04000B69 RID: 2921
		ERROR_NO_MORE_FILES,
		// Token: 0x04000B6A RID: 2922
		ERROR_WRITE_FAULT = 29,
		// Token: 0x04000B6B RID: 2923
		ERROR_READ_FAULT,
		// Token: 0x04000B6C RID: 2924
		ERROR_GEN_FAILURE,
		// Token: 0x04000B6D RID: 2925
		ERROR_SHARING_VIOLATION,
		// Token: 0x04000B6E RID: 2926
		ERROR_LOCK_VIOLATION,
		// Token: 0x04000B6F RID: 2927
		ERROR_HANDLE_DISK_FULL = 39,
		// Token: 0x04000B70 RID: 2928
		ERROR_FILE_EXISTS = 80,
		// Token: 0x04000B71 RID: 2929
		ERROR_CANNOT_MAKE = 82,
		// Token: 0x04000B72 RID: 2930
		ERROR_INVALID_PARAMETER = 87,
		// Token: 0x04000B73 RID: 2931
		ERROR_BROKEN_PIPE = 109,
		// Token: 0x04000B74 RID: 2932
		ERROR_INVALID_NAME = 123,
		// Token: 0x04000B75 RID: 2933
		ERROR_DIR_NOT_EMPTY = 145,
		// Token: 0x04000B76 RID: 2934
		ERROR_ALREADY_EXISTS = 183,
		// Token: 0x04000B77 RID: 2935
		ERROR_FILENAME_EXCED_RANGE = 206,
		// Token: 0x04000B78 RID: 2936
		ERROR_ENCRYPTION_FAILED = 6000
	}
}
