using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Posix
{
	// Token: 0x02000072 RID: 114
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.Syscall.")]
	public class Syscall
	{
		// Token: 0x06000515 RID: 1301
		[DllImport("libc", SetLastError = true)]
		public static extern int exit(int status);

		// Token: 0x06000516 RID: 1302
		[DllImport("libc", SetLastError = true)]
		public static extern int fork();

		// Token: 0x06000517 RID: 1303
		[DllImport("libc", SetLastError = true)]
		public unsafe static extern IntPtr read(int fileDescriptor, void* buf, IntPtr count);

		// Token: 0x06000518 RID: 1304
		[DllImport("libc", SetLastError = true)]
		public unsafe static extern IntPtr write(int fileDescriptor, void* buf, IntPtr count);

		// Token: 0x06000519 RID: 1305
		[DllImport("libc", EntryPoint = "open", SetLastError = true)]
		internal static extern int syscall_open(string pathname, int flags, int mode);

		// Token: 0x0600051A RID: 1306
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_OpenFlags(OpenFlags flags);

		// Token: 0x0600051B RID: 1307
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_FileMode(FileMode mode);

		// Token: 0x0600051C RID: 1308 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		public static int open(string pathname, OpenFlags flags)
		{
			if ((flags & OpenFlags.O_CREAT) != OpenFlags.O_RDONLY)
			{
				throw new ArgumentException("If you pass O_CREAT, you must call the method with the mode flag");
			}
			int flags2 = Syscall.map_Mono_Posix_OpenFlags(flags);
			return Syscall.syscall_open(pathname, flags2, 0);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000D51C File Offset: 0x0000B71C
		public static int open(string pathname, OpenFlags flags, FileMode mode)
		{
			int flags2 = Syscall.map_Mono_Posix_OpenFlags(flags);
			int mode2 = Syscall.map_Mono_Posix_FileMode(mode);
			return Syscall.syscall_open(pathname, flags2, mode2);
		}

		// Token: 0x0600051E RID: 1310
		[DllImport("libc", SetLastError = true)]
		public static extern int close(int fileDescriptor);

		// Token: 0x0600051F RID: 1311
		[DllImport("libc", EntryPoint = "waitpid", SetLastError = true)]
		internal unsafe static extern int syscall_waitpid(int pid, int* status, int options);

		// Token: 0x06000520 RID: 1312
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_WaitOptions(WaitOptions wait_options);

		// Token: 0x06000521 RID: 1313 RVA: 0x0000D540 File Offset: 0x0000B740
		public unsafe static int waitpid(int pid, out int status, WaitOptions options)
		{
			int num = 0;
			int result = Syscall.syscall_waitpid(pid, &num, Syscall.map_Mono_Posix_WaitOptions(options));
			status = num;
			return result;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000D564 File Offset: 0x0000B764
		public static int waitpid(int pid, WaitOptions options)
		{
			return Syscall.syscall_waitpid(pid, null, Syscall.map_Mono_Posix_WaitOptions(options));
		}

		// Token: 0x06000523 RID: 1315
		[DllImport("MonoPosixHelper", EntryPoint = "wifexited")]
		public static extern int WIFEXITED(int status);

		// Token: 0x06000524 RID: 1316
		[DllImport("MonoPosixHelper", EntryPoint = "wexitstatus")]
		public static extern int WEXITSTATUS(int status);

		// Token: 0x06000525 RID: 1317
		[DllImport("MonoPosixHelper", EntryPoint = "wifsignaled")]
		public static extern int WIFSIGNALED(int status);

		// Token: 0x06000526 RID: 1318
		[DllImport("MonoPosixHelper", EntryPoint = "wtermsig")]
		public static extern int WTERMSIG(int status);

		// Token: 0x06000527 RID: 1319
		[DllImport("MonoPosixHelper", EntryPoint = "wifstopped")]
		public static extern int WIFSTOPPED(int status);

		// Token: 0x06000528 RID: 1320
		[DllImport("MonoPosixHelper", EntryPoint = "wstopsig")]
		public static extern int WSTOPSIG(int status);

		// Token: 0x06000529 RID: 1321
		[DllImport("libc", EntryPoint = "creat", SetLastError = true)]
		internal static extern int syscall_creat(string pathname, int flags);

		// Token: 0x0600052A RID: 1322 RVA: 0x0000D574 File Offset: 0x0000B774
		public static int creat(string pathname, FileMode flags)
		{
			return Syscall.syscall_creat(pathname, Syscall.map_Mono_Posix_FileMode(flags));
		}

		// Token: 0x0600052B RID: 1323
		[DllImport("libc", SetLastError = true)]
		public static extern int link(string oldPath, string newPath);

		// Token: 0x0600052C RID: 1324
		[DllImport("libc", SetLastError = true)]
		public static extern int unlink(string path);

		// Token: 0x0600052D RID: 1325
		[DllImport("libc", SetLastError = true)]
		public static extern int symlink(string oldpath, string newpath);

		// Token: 0x0600052E RID: 1326
		[DllImport("libc", SetLastError = true)]
		public static extern int chdir(string path);

		// Token: 0x0600052F RID: 1327
		[DllImport("libc", EntryPoint = "chmod", SetLastError = true)]
		internal static extern int syscall_chmod(string path, int mode);

		// Token: 0x06000530 RID: 1328 RVA: 0x0000D584 File Offset: 0x0000B784
		public static int chmod(string path, FileMode mode)
		{
			return Syscall.syscall_chmod(path, Syscall.map_Mono_Posix_FileMode(mode));
		}

		// Token: 0x06000531 RID: 1329
		[DllImport("libc", SetLastError = true)]
		public static extern int chown(string path, int owner, int group);

		// Token: 0x06000532 RID: 1330
		[DllImport("libc", SetLastError = true)]
		public static extern int lchown(string path, int owner, int group);

		// Token: 0x06000533 RID: 1331
		[DllImport("libc", SetLastError = true)]
		public static extern int lseek(int fileDescriptor, int offset, int whence);

		// Token: 0x06000534 RID: 1332
		[DllImport("libc", SetLastError = true)]
		public static extern int getpid();

		// Token: 0x06000535 RID: 1333
		[DllImport("libc", SetLastError = true)]
		public static extern int setuid(int uid);

		// Token: 0x06000536 RID: 1334
		[DllImport("libc", SetLastError = true)]
		public static extern int getuid();

		// Token: 0x06000537 RID: 1335
		[DllImport("libc")]
		public static extern uint alarm(uint seconds);

		// Token: 0x06000538 RID: 1336
		[DllImport("libc", SetLastError = true)]
		public static extern int pause();

		// Token: 0x06000539 RID: 1337
		[DllImport("libc", EntryPoint = "access", SetLastError = true)]
		internal static extern int syscall_access(string pathname, int mode);

		// Token: 0x0600053A RID: 1338
		[DllImport("MonoPosixHelper")]
		internal static extern int map_Mono_Posix_AccessMode(AccessMode mode);

		// Token: 0x0600053B RID: 1339 RVA: 0x0000D594 File Offset: 0x0000B794
		public static int access(string pathname, AccessMode mode)
		{
			return Syscall.syscall_access(pathname, Syscall.map_Mono_Posix_AccessMode(mode));
		}

		// Token: 0x0600053C RID: 1340
		[DllImport("libc", SetLastError = true)]
		public static extern int nice(int increment);

		// Token: 0x0600053D RID: 1341
		[DllImport("libc")]
		public static extern void sync();

		// Token: 0x0600053E RID: 1342
		[DllImport("libc", SetLastError = true)]
		public static extern void kill(int pid, int sig);

		// Token: 0x0600053F RID: 1343
		[DllImport("libc", SetLastError = true)]
		public static extern int rename(string oldPath, string newPath);

		// Token: 0x06000540 RID: 1344
		[DllImport("libc", EntryPoint = "mkdir", SetLastError = true)]
		internal static extern int syscall_mkdir(string pathname, int mode);

		// Token: 0x06000541 RID: 1345 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		public static int mkdir(string pathname, FileMode mode)
		{
			return Syscall.syscall_mkdir(pathname, Syscall.map_Mono_Posix_FileMode(mode));
		}

		// Token: 0x06000542 RID: 1346
		[DllImport("libc", SetLastError = true)]
		public static extern int rmdir(string path);

		// Token: 0x06000543 RID: 1347
		[DllImport("libc", SetLastError = true)]
		public static extern int dup(int fileDescriptor);

		// Token: 0x06000544 RID: 1348
		[DllImport("libc", SetLastError = true)]
		public static extern int setgid(int gid);

		// Token: 0x06000545 RID: 1349
		[DllImport("libc", SetLastError = true)]
		public static extern int getgid();

		// Token: 0x06000546 RID: 1350
		[DllImport("libc", SetLastError = true)]
		public static extern int signal(int signum, Syscall.sighandler_t handler);

		// Token: 0x06000547 RID: 1351
		[DllImport("libc", SetLastError = true)]
		public static extern int geteuid();

		// Token: 0x06000548 RID: 1352
		[DllImport("libc", SetLastError = true)]
		public static extern int getegid();

		// Token: 0x06000549 RID: 1353
		[DllImport("libc", SetLastError = true)]
		public static extern int setpgid(int pid, int pgid);

		// Token: 0x0600054A RID: 1354
		[DllImport("libc")]
		public static extern int umask(int umask);

		// Token: 0x0600054B RID: 1355
		[DllImport("libc", SetLastError = true)]
		public static extern int chroot(string path);

		// Token: 0x0600054C RID: 1356
		[DllImport("libc", SetLastError = true)]
		public static extern int dup2(int oldFileDescriptor, int newFileDescriptor);

		// Token: 0x0600054D RID: 1357
		[DllImport("libc", SetLastError = true)]
		public static extern int getppid();

		// Token: 0x0600054E RID: 1358
		[DllImport("libc", SetLastError = true)]
		public static extern int getpgrp();

		// Token: 0x0600054F RID: 1359
		[DllImport("libc", SetLastError = true)]
		public static extern int setsid();

		// Token: 0x06000550 RID: 1360
		[DllImport("libc", SetLastError = true)]
		public static extern int setreuid(int ruid, int euid);

		// Token: 0x06000551 RID: 1361
		[DllImport("libc", SetLastError = true)]
		public static extern int setregid(int rgid, int egid);

		// Token: 0x06000552 RID: 1362
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern string helper_Mono_Posix_GetUserName(int uid);

		// Token: 0x06000553 RID: 1363
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern string helper_Mono_Posix_GetGroupName(int gid);

		// Token: 0x06000554 RID: 1364 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		public static string getusername(int uid)
		{
			return Syscall.helper_Mono_Posix_GetUserName(uid);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		public static string getgroupname(int gid)
		{
			return Syscall.helper_Mono_Posix_GetGroupName(gid);
		}

		// Token: 0x06000556 RID: 1366
		[DllImport("libc", EntryPoint = "gethostname", SetLastError = true)]
		private static extern int syscall_gethostname(byte[] p, int len);

		// Token: 0x06000557 RID: 1367 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public static string GetHostName()
		{
			byte[] array = new byte[256];
			int i = Syscall.syscall_gethostname(array, array.Length);
			if (i == -1)
			{
				return "localhost";
			}
			for (i = 0; i < array.Length; i++)
			{
				if (array[i] == 0)
				{
					break;
				}
			}
			return Encoding.UTF8.GetString(array, 0, i);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000D624 File Offset: 0x0000B824
		[CLSCompliant(false)]
		public static string gethostname()
		{
			return Syscall.GetHostName();
		}

		// Token: 0x06000559 RID: 1369
		[DllImport("libc", EntryPoint = "isatty")]
		private static extern int syscall_isatty(int desc);

		// Token: 0x0600055A RID: 1370 RVA: 0x0000D62C File Offset: 0x0000B82C
		public static bool isatty(int desc)
		{
			int num = Syscall.syscall_isatty(desc);
			return num == 1;
		}

		// Token: 0x0600055B RID: 1371
		[DllImport("MonoPosixHelper")]
		internal static extern int helper_Mono_Posix_Stat(string filename, bool dereference, out int device, out int inode, out int mode, out int nlinks, out int uid, out int gid, out int rdev, out long size, out long blksize, out long blocks, out long atime, out long mtime, out long ctime);

		// Token: 0x0600055C RID: 1372 RVA: 0x0000D64C File Offset: 0x0000B84C
		private static int stat2(string filename, bool dereference, out Stat stat)
		{
			int device;
			int inode;
			int mode;
			int nlinks;
			int uid;
			int gid;
			int rdev;
			long size;
			long blksize;
			long blocks;
			long atime;
			long mtime;
			long ctime;
			int num = Syscall.helper_Mono_Posix_Stat(filename, dereference, out device, out inode, out mode, out nlinks, out uid, out gid, out rdev, out size, out blksize, out blocks, out atime, out mtime, out ctime);
			stat = new Stat(device, inode, mode, nlinks, uid, gid, rdev, size, blksize, blocks, atime, mtime, ctime);
			if (num != 0)
			{
				return num;
			}
			return 0;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		public static int stat(string filename, out Stat stat)
		{
			return Syscall.stat2(filename, false, out stat);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public static int lstat(string filename, out Stat stat)
		{
			return Syscall.stat2(filename, true, out stat);
		}

		// Token: 0x0600055F RID: 1375
		[DllImport("libc")]
		private static extern int readlink(string path, byte[] buffer, int buflen);

		// Token: 0x06000560 RID: 1376 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		public static string readlink(string path)
		{
			byte[] array = new byte[512];
			int num = Syscall.readlink(path, array, array.Length);
			if (num == -1)
			{
				return null;
			}
			char[] array2 = new char[512];
			int chars = Encoding.Default.GetChars(array, 0, num, array2, 0);
			return new string(array2, 0, chars);
		}

		// Token: 0x06000561 RID: 1377
		[DllImport("libc", EntryPoint = "strerror")]
		private static extern IntPtr _strerror(int errnum);

		// Token: 0x06000562 RID: 1378 RVA: 0x0000D70C File Offset: 0x0000B90C
		public static string strerror(int errnum)
		{
			return Marshal.PtrToStringAnsi(Syscall._strerror(errnum));
		}

		// Token: 0x06000563 RID: 1379
		[DllImport("libc")]
		public static extern IntPtr opendir(string path);

		// Token: 0x06000564 RID: 1380
		[DllImport("libc")]
		public static extern int closedir(IntPtr dir);

		// Token: 0x06000565 RID: 1381
		[DllImport("MonoPosixHelper", EntryPoint = "helper_Mono_Posix_readdir")]
		public static extern string readdir(IntPtr dir);

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x0600060B RID: 1547
		public delegate void sighandler_t(int v);
	}
}
