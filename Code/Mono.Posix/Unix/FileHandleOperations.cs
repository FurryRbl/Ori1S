using System;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000008 RID: 8
	public sealed class FileHandleOperations
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002484 File Offset: 0x00000684
		private FileHandleOperations()
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000248C File Offset: 0x0000068C
		public static void AdviseFileAccessPattern(int fd, FileAccessPattern pattern, long offset, long len)
		{
			int retval = Syscall.posix_fadvise(fd, offset, len, (PosixFadviseAdvice)pattern);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024AC File Offset: 0x000006AC
		public static void AdviseFileAccessPattern(int fd, FileAccessPattern pattern)
		{
			FileHandleOperations.AdviseFileAccessPattern(fd, pattern, 0L, 0L);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024BC File Offset: 0x000006BC
		public static void AdviseFileAccessPattern(FileStream file, FileAccessPattern pattern, long offset, long len)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			int retval = Syscall.posix_fadvise(file.Handle.ToInt32(), offset, len, (PosixFadviseAdvice)pattern);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024F8 File Offset: 0x000006F8
		public static void AdviseFileAccessPattern(FileStream file, FileAccessPattern pattern)
		{
			FileHandleOperations.AdviseFileAccessPattern(file, pattern, 0L, 0L);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002508 File Offset: 0x00000708
		public static void AdviseFileAccessPattern(UnixStream stream, FileAccessPattern pattern, long offset, long len)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			int retval = Syscall.posix_fadvise(stream.Handle, offset, len, (PosixFadviseAdvice)pattern);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000253C File Offset: 0x0000073C
		public static void AdviseFileAccessPattern(UnixStream stream, FileAccessPattern pattern)
		{
			FileHandleOperations.AdviseFileAccessPattern(stream, pattern, 0L, 0L);
		}
	}
}
