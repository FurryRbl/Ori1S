using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000056 RID: 86
	[CLSCompliant(false)]
	public sealed class Syscall : Stdlib
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000BB30 File Offset: 0x00009D30
		private Syscall()
		{
		}

		// Token: 0x060003BE RID: 958
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setxattr", SetLastError = true)]
		public static extern int setxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size, XattrFlags flags);

		// Token: 0x060003BF RID: 959 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		public static int setxattr(string path, string name, byte[] value, ulong size)
		{
			return Syscall.setxattr(path, name, value, size, XattrFlags.XATTR_AUTO);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		public static int setxattr(string path, string name, byte[] value, XattrFlags flags)
		{
			return Syscall.setxattr(path, name, value, (ulong)((long)value.Length), flags);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		public static int setxattr(string path, string name, byte[] value)
		{
			return Syscall.setxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060003C2 RID: 962
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lsetxattr", SetLastError = true)]
		public static extern int lsetxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size, XattrFlags flags);

		// Token: 0x060003C3 RID: 963 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public static int lsetxattr(string path, string name, byte[] value, ulong size)
		{
			return Syscall.lsetxattr(path, name, value, size, XattrFlags.XATTR_AUTO);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public static int lsetxattr(string path, string name, byte[] value, XattrFlags flags)
		{
			return Syscall.lsetxattr(path, name, value, (ulong)((long)value.Length), flags);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000BBFC File Offset: 0x00009DFC
		public static int lsetxattr(string path, string name, byte[] value)
		{
			return Syscall.lsetxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060003C6 RID: 966
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fsetxattr", SetLastError = true)]
		public static extern int fsetxattr(int fd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size, XattrFlags flags);

		// Token: 0x060003C7 RID: 967 RVA: 0x0000BC0C File Offset: 0x00009E0C
		public static int fsetxattr(int fd, string name, byte[] value, ulong size)
		{
			return Syscall.fsetxattr(fd, name, value, size, XattrFlags.XATTR_AUTO);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000BC18 File Offset: 0x00009E18
		public static int fsetxattr(int fd, string name, byte[] value, XattrFlags flags)
		{
			return Syscall.fsetxattr(fd, name, value, (ulong)((long)value.Length), flags);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000BC28 File Offset: 0x00009E28
		public static int fsetxattr(int fd, string name, byte[] value)
		{
			return Syscall.fsetxattr(fd, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060003CA RID: 970
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getxattr", SetLastError = true)]
		public static extern long getxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size);

		// Token: 0x060003CB RID: 971 RVA: 0x0000BC38 File Offset: 0x00009E38
		public static long getxattr(string path, string name, byte[] value)
		{
			return Syscall.getxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000BC48 File Offset: 0x00009E48
		public static long getxattr(string path, string name, out byte[] value)
		{
			value = null;
			long num = Syscall.getxattr(path, name, value, 0UL);
			if (num <= 0L)
			{
				return num;
			}
			value = new byte[num];
			return Syscall.getxattr(path, name, value, (ulong)num);
		}

		// Token: 0x060003CD RID: 973
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lgetxattr", SetLastError = true)]
		public static extern long lgetxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size);

		// Token: 0x060003CE RID: 974 RVA: 0x0000BC84 File Offset: 0x00009E84
		public static long lgetxattr(string path, string name, byte[] value)
		{
			return Syscall.lgetxattr(path, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000BC94 File Offset: 0x00009E94
		public static long lgetxattr(string path, string name, out byte[] value)
		{
			value = null;
			long num = Syscall.lgetxattr(path, name, value, 0UL);
			if (num <= 0L)
			{
				return num;
			}
			value = new byte[num];
			return Syscall.lgetxattr(path, name, value, (ulong)num);
		}

		// Token: 0x060003D0 RID: 976
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fgetxattr", SetLastError = true)]
		public static extern long fgetxattr(int fd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, byte[] value, ulong size);

		// Token: 0x060003D1 RID: 977 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public static long fgetxattr(int fd, string name, byte[] value)
		{
			return Syscall.fgetxattr(fd, name, value, (ulong)((long)value.Length));
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		public static long fgetxattr(int fd, string name, out byte[] value)
		{
			value = null;
			long num = Syscall.fgetxattr(fd, name, value, 0UL);
			if (num <= 0L)
			{
				return num;
			}
			value = new byte[num];
			return Syscall.fgetxattr(fd, name, value, (ulong)num);
		}

		// Token: 0x060003D3 RID: 979
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_listxattr", SetLastError = true)]
		public static extern long listxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, byte[] list, ulong size);

		// Token: 0x060003D4 RID: 980 RVA: 0x0000BD1C File Offset: 0x00009F1C
		public static long listxattr(string path, Encoding encoding, out string[] values)
		{
			values = null;
			long num = Syscall.listxattr(path, null, 0UL);
			if (num == 0L)
			{
				values = new string[0];
			}
			if (num <= 0L)
			{
				return (long)((int)num);
			}
			byte[] list = new byte[num];
			long num2 = Syscall.listxattr(path, list, (ulong)num);
			if (num2 < 0L)
			{
				return (long)((int)num2);
			}
			Syscall.GetValues(list, encoding, out values);
			return 0L;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000BD78 File Offset: 0x00009F78
		public static long listxattr(string path, out string[] values)
		{
			return Syscall.listxattr(path, UnixEncoding.Instance, out values);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000BD88 File Offset: 0x00009F88
		private static void GetValues(byte[] list, Encoding encoding, out string[] values)
		{
			int num = 0;
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i] == 0)
				{
					num++;
				}
			}
			values = new string[num];
			num = 0;
			int num2 = 0;
			for (int j = 0; j < list.Length; j++)
			{
				if (list[j] == 0)
				{
					values[num++] = encoding.GetString(list, num2, j - num2);
					num2 = j + 1;
				}
			}
		}

		// Token: 0x060003D7 RID: 983
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_llistxattr", SetLastError = true)]
		public static extern long llistxattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, byte[] list, ulong size);

		// Token: 0x060003D8 RID: 984 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		public static long llistxattr(string path, Encoding encoding, out string[] values)
		{
			values = null;
			long num = Syscall.llistxattr(path, null, 0UL);
			if (num == 0L)
			{
				values = new string[0];
			}
			if (num <= 0L)
			{
				return (long)((int)num);
			}
			byte[] list = new byte[num];
			long num2 = Syscall.llistxattr(path, list, (ulong)num);
			if (num2 < 0L)
			{
				return (long)((int)num2);
			}
			Syscall.GetValues(list, encoding, out values);
			return 0L;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000BE54 File Offset: 0x0000A054
		public static long llistxattr(string path, out string[] values)
		{
			return Syscall.llistxattr(path, UnixEncoding.Instance, out values);
		}

		// Token: 0x060003DA RID: 986
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_flistxattr", SetLastError = true)]
		public static extern long flistxattr(int fd, byte[] list, ulong size);

		// Token: 0x060003DB RID: 987 RVA: 0x0000BE64 File Offset: 0x0000A064
		public static long flistxattr(int fd, Encoding encoding, out string[] values)
		{
			values = null;
			long num = Syscall.flistxattr(fd, null, 0UL);
			if (num == 0L)
			{
				values = new string[0];
			}
			if (num <= 0L)
			{
				return (long)((int)num);
			}
			byte[] list = new byte[num];
			long num2 = Syscall.flistxattr(fd, list, (ulong)num);
			if (num2 < 0L)
			{
				return (long)((int)num2);
			}
			Syscall.GetValues(list, encoding, out values);
			return 0L;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		public static long flistxattr(int fd, out string[] values)
		{
			return Syscall.flistxattr(fd, UnixEncoding.Instance, out values);
		}

		// Token: 0x060003DD RID: 989
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_removexattr", SetLastError = true)]
		public static extern int removexattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060003DE RID: 990
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lremovexattr", SetLastError = true)]
		public static extern int lremovexattr([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060003DF RID: 991
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fremovexattr", SetLastError = true)]
		public static extern int fremovexattr(int fd, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060003E0 RID: 992
		[DllImport("libc", SetLastError = true)]
		public static extern IntPtr opendir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name);

		// Token: 0x060003E1 RID: 993
		[DllImport("libc", SetLastError = true)]
		public static extern int closedir(IntPtr dir);

		// Token: 0x060003E2 RID: 994
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_seekdir", SetLastError = true)]
		public static extern int seekdir(IntPtr dir, long offset);

		// Token: 0x060003E3 RID: 995
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_telldir", SetLastError = true)]
		public static extern long telldir(IntPtr dir);

		// Token: 0x060003E4 RID: 996
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_rewinddir", SetLastError = true)]
		public static extern int rewinddir(IntPtr dir);

		// Token: 0x060003E5 RID: 997 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		private static void CopyDirent(Dirent to, ref Syscall._Dirent from)
		{
			try
			{
				to.d_ino = from.d_ino;
				to.d_off = from.d_off;
				to.d_reclen = from.d_reclen;
				to.d_type = from.d_type;
				to.d_name = UnixMarshal.PtrToString(from.d_name);
			}
			finally
			{
				Stdlib.free(from.d_name);
				from.d_name = IntPtr.Zero;
			}
		}

		// Token: 0x060003E6 RID: 998
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readdir", SetLastError = true)]
		private static extern int sys_readdir(IntPtr dir, out Syscall._Dirent dentry);

		// Token: 0x060003E7 RID: 999 RVA: 0x0000BF58 File Offset: 0x0000A158
		public static Dirent readdir(IntPtr dir)
		{
			object obj = Syscall.readdir_lock;
			Syscall._Dirent dirent;
			int num;
			lock (obj)
			{
				num = Syscall.sys_readdir(dir, out dirent);
			}
			if (num != 0)
			{
				return null;
			}
			Dirent dirent2 = new Dirent();
			Syscall.CopyDirent(dirent2, ref dirent);
			return dirent2;
		}

		// Token: 0x060003E8 RID: 1000
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readdir_r", SetLastError = true)]
		private static extern int sys_readdir_r(IntPtr dirp, out Syscall._Dirent entry, out IntPtr result);

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public static int readdir_r(IntPtr dirp, Dirent entry, out IntPtr result)
		{
			entry.d_ino = 0UL;
			entry.d_off = 0L;
			entry.d_reclen = 0;
			entry.d_type = 0;
			entry.d_name = null;
			Syscall._Dirent dirent;
			int num = Syscall.sys_readdir_r(dirp, out dirent, out result);
			if (num == 0 && result != IntPtr.Zero)
			{
				Syscall.CopyDirent(entry, ref dirent);
			}
			return num;
		}

		// Token: 0x060003EA RID: 1002
		[DllImport("libc", SetLastError = true)]
		public static extern int dirfd(IntPtr dir);

		// Token: 0x060003EB RID: 1003
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd);

		// Token: 0x060003EC RID: 1004
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl_arg", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd, long arg);

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C018 File Offset: 0x0000A218
		public static int fcntl(int fd, FcntlCommand cmd, DirectoryNotifyFlags arg)
		{
			if (cmd != FcntlCommand.F_NOTIFY)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			long arg2 = (long)NativeConvert.FromDirectoryNotifyFlags(arg);
			return Syscall.fcntl(fd, FcntlCommand.F_NOTIFY, arg2);
		}

		// Token: 0x060003EE RID: 1006
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fcntl_lock", SetLastError = true)]
		public static extern int fcntl(int fd, FcntlCommand cmd, ref Flock @lock);

		// Token: 0x060003EF RID: 1007
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_open", SetLastError = true)]
		public static extern int open([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, OpenFlags flags);

		// Token: 0x060003F0 RID: 1008
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_open_mode", SetLastError = true)]
		public static extern int open([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, OpenFlags flags, FilePermissions mode);

		// Token: 0x060003F1 RID: 1009
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_creat", SetLastError = true)]
		public static extern int creat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, FilePermissions mode);

		// Token: 0x060003F2 RID: 1010
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_posix_fadvise", SetLastError = true)]
		public static extern int posix_fadvise(int fd, long offset, long len, PosixFadviseAdvice advice);

		// Token: 0x060003F3 RID: 1011
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_posix_fallocate", SetLastError = true)]
		public static extern int posix_fallocate(int fd, long offset, ulong len);

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C050 File Offset: 0x0000A250
		private static void CopyFstab(Fstab to, ref Syscall._Fstab from)
		{
			try
			{
				to.fs_spec = UnixMarshal.PtrToString(from.fs_spec);
				to.fs_file = UnixMarshal.PtrToString(from.fs_file);
				to.fs_vfstype = UnixMarshal.PtrToString(from.fs_vfstype);
				to.fs_mntops = UnixMarshal.PtrToString(from.fs_mntops);
				to.fs_type = UnixMarshal.PtrToString(from.fs_type);
				to.fs_freq = from.fs_freq;
				to.fs_passno = from.fs_passno;
			}
			finally
			{
				Stdlib.free(from._fs_buf_);
				from._fs_buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x060003F5 RID: 1013
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endfsent", SetLastError = true)]
		private static extern int sys_endfsent();

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000C104 File Offset: 0x0000A304
		public static int endfsent()
		{
			object obj = Syscall.fstab_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_endfsent();
			}
			return result;
		}

		// Token: 0x060003F7 RID: 1015
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getfsent", SetLastError = true)]
		private static extern int sys_getfsent(out Syscall._Fstab fs);

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C154 File Offset: 0x0000A354
		public static Fstab getfsent()
		{
			object obj = Syscall.fstab_lock;
			Syscall._Fstab fstab;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getfsent(out fstab);
			}
			if (num != 0)
			{
				return null;
			}
			Fstab fstab2 = new Fstab();
			Syscall.CopyFstab(fstab2, ref fstab);
			return fstab2;
		}

		// Token: 0x060003F9 RID: 1017
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getfsfile", SetLastError = true)]
		private static extern int sys_getfsfile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string mount_point, out Syscall._Fstab fs);

		// Token: 0x060003FA RID: 1018 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public static Fstab getfsfile(string mount_point)
		{
			object obj = Syscall.fstab_lock;
			Syscall._Fstab fstab;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getfsfile(mount_point, out fstab);
			}
			if (num != 0)
			{
				return null;
			}
			Fstab fstab2 = new Fstab();
			Syscall.CopyFstab(fstab2, ref fstab);
			return fstab2;
		}

		// Token: 0x060003FB RID: 1019
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getfsspec", SetLastError = true)]
		private static extern int sys_getfsspec([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string special_file, out Syscall._Fstab fs);

		// Token: 0x060003FC RID: 1020 RVA: 0x0000C218 File Offset: 0x0000A418
		public static Fstab getfsspec(string special_file)
		{
			object obj = Syscall.fstab_lock;
			Syscall._Fstab fstab;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getfsspec(special_file, out fstab);
			}
			if (num != 0)
			{
				return null;
			}
			Fstab fstab2 = new Fstab();
			Syscall.CopyFstab(fstab2, ref fstab);
			return fstab2;
		}

		// Token: 0x060003FD RID: 1021
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setfsent", SetLastError = true)]
		private static extern int sys_setfsent();

		// Token: 0x060003FE RID: 1022 RVA: 0x0000C27C File Offset: 0x0000A47C
		public static int setfsent()
		{
			object obj = Syscall.fstab_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_setfsent();
			}
			return result;
		}

		// Token: 0x060003FF RID: 1023
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setgroups", SetLastError = true)]
		public static extern int setgroups(ulong size, uint[] list);

		// Token: 0x06000400 RID: 1024 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		public static int setgroups(uint[] list)
		{
			return Syscall.setgroups((ulong)((long)list.Length), list);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		private static void CopyGroup(Group to, ref Syscall._Group from)
		{
			try
			{
				to.gr_gid = from.gr_gid;
				to.gr_name = UnixMarshal.PtrToString(from.gr_name);
				to.gr_passwd = UnixMarshal.PtrToString(from.gr_passwd);
				to.gr_mem = UnixMarshal.PtrToStringArray(from._gr_nmem_, from.gr_mem);
			}
			finally
			{
				Stdlib.free(from.gr_mem);
				Stdlib.free(from._gr_buf_);
				from.gr_mem = IntPtr.Zero;
				from._gr_buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x06000402 RID: 1026
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrnam", SetLastError = true)]
		private static extern int sys_getgrnam(string name, out Syscall._Group group);

		// Token: 0x06000403 RID: 1027 RVA: 0x0000C378 File Offset: 0x0000A578
		public static Group getgrnam(string name)
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getgrnam(name, out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x06000404 RID: 1028
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrgid", SetLastError = true)]
		private static extern int sys_getgrgid(uint uid, out Syscall._Group group);

		// Token: 0x06000405 RID: 1029 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		public static Group getgrgid(uint uid)
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getgrgid(uid, out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x06000406 RID: 1030
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrnam_r", SetLastError = true)]
		private static extern int sys_getgrnam_r([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, out Syscall._Group grbuf, out IntPtr grbufp);

		// Token: 0x06000407 RID: 1031 RVA: 0x0000C440 File Offset: 0x0000A640
		public static int getgrnam_r(string name, Group grbuf, out Group grbufp)
		{
			grbufp = null;
			Syscall._Group group;
			IntPtr value;
			int num = Syscall.sys_getgrnam_r(name, out group, out value);
			if (num == 0 && value != IntPtr.Zero)
			{
				Syscall.CopyGroup(grbuf, ref group);
				grbufp = grbuf;
			}
			return num;
		}

		// Token: 0x06000408 RID: 1032
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrgid_r", SetLastError = true)]
		private static extern int sys_getgrgid_r(uint uid, out Syscall._Group grbuf, out IntPtr grbufp);

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C480 File Offset: 0x0000A680
		public static int getgrgid_r(uint uid, Group grbuf, out Group grbufp)
		{
			grbufp = null;
			Syscall._Group group;
			IntPtr value;
			int num = Syscall.sys_getgrgid_r(uid, out group, out value);
			if (num == 0 && value != IntPtr.Zero)
			{
				Syscall.CopyGroup(grbuf, ref group);
				grbufp = grbuf;
			}
			return num;
		}

		// Token: 0x0600040A RID: 1034
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getgrent", SetLastError = true)]
		private static extern int sys_getgrent(out Syscall._Group grbuf);

		// Token: 0x0600040B RID: 1035 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		public static Group getgrent()
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getgrent(out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x0600040C RID: 1036
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setgrent", SetLastError = true)]
		private static extern int sys_setgrent();

		// Token: 0x0600040D RID: 1037 RVA: 0x0000C520 File Offset: 0x0000A720
		public static int setgrent()
		{
			object obj = Syscall.grp_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_setgrent();
			}
			return result;
		}

		// Token: 0x0600040E RID: 1038
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endgrent", SetLastError = true)]
		private static extern int sys_endgrent();

		// Token: 0x0600040F RID: 1039 RVA: 0x0000C570 File Offset: 0x0000A770
		public static int endgrent()
		{
			object obj = Syscall.grp_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_endgrent();
			}
			return result;
		}

		// Token: 0x06000410 RID: 1040
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fgetgrent", SetLastError = true)]
		private static extern int sys_fgetgrent(IntPtr stream, out Syscall._Group grbuf);

		// Token: 0x06000411 RID: 1041 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public static Group fgetgrent(IntPtr stream)
		{
			object obj = Syscall.grp_lock;
			Syscall._Group group;
			int num;
			lock (obj)
			{
				num = Syscall.sys_fgetgrent(stream, out group);
			}
			if (num != 0)
			{
				return null;
			}
			Group group2 = new Group();
			Syscall.CopyGroup(group2, ref group);
			return group2;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000C624 File Offset: 0x0000A824
		private static void CopyPasswd(Passwd to, ref Syscall._Passwd from)
		{
			try
			{
				to.pw_name = UnixMarshal.PtrToString(from.pw_name);
				to.pw_passwd = UnixMarshal.PtrToString(from.pw_passwd);
				to.pw_uid = from.pw_uid;
				to.pw_gid = from.pw_gid;
				to.pw_gecos = UnixMarshal.PtrToString(from.pw_gecos);
				to.pw_dir = UnixMarshal.PtrToString(from.pw_dir);
				to.pw_shell = UnixMarshal.PtrToString(from.pw_shell);
			}
			finally
			{
				Stdlib.free(from._pw_buf_);
				from._pw_buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x06000413 RID: 1043
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwnam", SetLastError = true)]
		private static extern int sys_getpwnam(string name, out Syscall._Passwd passwd);

		// Token: 0x06000414 RID: 1044 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public static Passwd getpwnam(string name)
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getpwnam(name, out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x06000415 RID: 1045
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwuid", SetLastError = true)]
		private static extern int sys_getpwuid(uint uid, out Syscall._Passwd passwd);

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C73C File Offset: 0x0000A93C
		public static Passwd getpwuid(uint uid)
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getpwuid(uid, out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x06000417 RID: 1047
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwnam_r", SetLastError = true)]
		private static extern int sys_getpwnam_r([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string name, out Syscall._Passwd pwbuf, out IntPtr pwbufp);

		// Token: 0x06000418 RID: 1048 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public static int getpwnam_r(string name, Passwd pwbuf, out Passwd pwbufp)
		{
			pwbufp = null;
			Syscall._Passwd passwd;
			IntPtr value;
			int num = Syscall.sys_getpwnam_r(name, out passwd, out value);
			if (num == 0 && value != IntPtr.Zero)
			{
				Syscall.CopyPasswd(pwbuf, ref passwd);
				pwbufp = pwbuf;
			}
			return num;
		}

		// Token: 0x06000419 RID: 1049
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwuid_r", SetLastError = true)]
		private static extern int sys_getpwuid_r(uint uid, out Syscall._Passwd pwbuf, out IntPtr pwbufp);

		// Token: 0x0600041A RID: 1050 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public static int getpwuid_r(uint uid, Passwd pwbuf, out Passwd pwbufp)
		{
			pwbufp = null;
			Syscall._Passwd passwd;
			IntPtr value;
			int num = Syscall.sys_getpwuid_r(uid, out passwd, out value);
			if (num == 0 && value != IntPtr.Zero)
			{
				Syscall.CopyPasswd(pwbuf, ref passwd);
				pwbufp = pwbuf;
			}
			return num;
		}

		// Token: 0x0600041B RID: 1051
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getpwent", SetLastError = true)]
		private static extern int sys_getpwent(out Syscall._Passwd pwbuf);

		// Token: 0x0600041C RID: 1052 RVA: 0x0000C820 File Offset: 0x0000AA20
		public static Passwd getpwent()
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_getpwent(out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x0600041D RID: 1053
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setpwent", SetLastError = true)]
		private static extern int sys_setpwent();

		// Token: 0x0600041E RID: 1054 RVA: 0x0000C880 File Offset: 0x0000AA80
		public static int setpwent()
		{
			object obj = Syscall.pwd_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_setpwent();
			}
			return result;
		}

		// Token: 0x0600041F RID: 1055
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endpwent", SetLastError = true)]
		private static extern int sys_endpwent();

		// Token: 0x06000420 RID: 1056 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		public static int endpwent()
		{
			object obj = Syscall.pwd_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_endpwent();
			}
			return result;
		}

		// Token: 0x06000421 RID: 1057
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fgetpwent", SetLastError = true)]
		private static extern int sys_fgetpwent(IntPtr stream, out Syscall._Passwd pwbuf);

		// Token: 0x06000422 RID: 1058 RVA: 0x0000C920 File Offset: 0x0000AB20
		public static Passwd fgetpwent(IntPtr stream)
		{
			object obj = Syscall.pwd_lock;
			Syscall._Passwd passwd;
			int num;
			lock (obj)
			{
				num = Syscall.sys_fgetpwent(stream, out passwd);
			}
			if (num != 0)
			{
				return null;
			}
			Passwd passwd2 = new Passwd();
			Syscall.CopyPasswd(passwd2, ref passwd);
			return passwd2;
		}

		// Token: 0x06000423 RID: 1059
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_psignal", SetLastError = true)]
		private static extern int psignal(int sig, string s);

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C984 File Offset: 0x0000AB84
		public static int psignal(Signum sig, string s)
		{
			int sig2 = NativeConvert.FromSignum(sig);
			return Syscall.psignal(sig2, s);
		}

		// Token: 0x06000425 RID: 1061
		[DllImport("libc", EntryPoint = "kill", SetLastError = true)]
		private static extern int sys_kill(int pid, int sig);

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		public static int kill(int pid, Signum sig)
		{
			int sig2 = NativeConvert.FromSignum(sig);
			return Syscall.sys_kill(pid, sig2);
		}

		// Token: 0x06000427 RID: 1063
		[DllImport("libc", EntryPoint = "strsignal", SetLastError = true)]
		private static extern IntPtr sys_strsignal(int sig);

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		public static string strsignal(Signum sig)
		{
			int sig2 = NativeConvert.FromSignum(sig);
			object obj = Syscall.signal_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Syscall.sys_strsignal(sig2);
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x06000429 RID: 1065
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_L_ctermid")]
		private static extern int _L_ctermid();

		// Token: 0x0600042A RID: 1066
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_L_cuserid")]
		private static extern int _L_cuserid();

		// Token: 0x0600042B RID: 1067
		[DllImport("libc", EntryPoint = "cuserid", SetLastError = true)]
		private static extern IntPtr sys_cuserid([Out] StringBuilder @string);

		// Token: 0x0600042C RID: 1068 RVA: 0x0000CA18 File Offset: 0x0000AC18
		[Obsolete("\"Nobody knows precisely what cuserid() does... DO NOT USE cuserid().\n`string' must hold L_cuserid characters.  Use getlogin_r instead.")]
		public static string cuserid(StringBuilder @string)
		{
			if (@string.Capacity < Syscall.L_cuserid)
			{
				throw new ArgumentOutOfRangeException("string", "string.Capacity < L_cuserid");
			}
			object obj = Syscall.getlogin_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Syscall.sys_cuserid(@string);
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x0600042D RID: 1069
		[DllImport("libc", SetLastError = true)]
		public static extern int mkstemp(StringBuilder template);

		// Token: 0x0600042E RID: 1070
		[DllImport("libc", SetLastError = true)]
		public static extern int ttyslot();

		// Token: 0x0600042F RID: 1071 RVA: 0x0000CA90 File Offset: 0x0000AC90
		[Obsolete("This is insecure and should not be used", true)]
		public static int setkey(string key)
		{
			throw new SecurityException("crypt(3) has been broken.  Use something more secure.");
		}

		// Token: 0x06000430 RID: 1072
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_strerror_r", SetLastError = true)]
		private static extern int sys_strerror_r(int errnum, [Out] StringBuilder buf, ulong n);

		// Token: 0x06000431 RID: 1073 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		public static int strerror_r(Errno errnum, StringBuilder buf, ulong n)
		{
			int errnum2 = NativeConvert.FromErrno(errnum);
			return Syscall.sys_strerror_r(errnum2, buf, n);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public static int strerror_r(Errno errnum, StringBuilder buf)
		{
			return Syscall.strerror_r(errnum, buf, (ulong)((long)buf.Capacity));
		}

		// Token: 0x06000433 RID: 1075
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_posix_madvise", SetLastError = true)]
		public static extern int posix_madvise(IntPtr addr, ulong len, PosixMadviseAdvice advice);

		// Token: 0x06000434 RID: 1076
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mmap", SetLastError = true)]
		public static extern IntPtr mmap(IntPtr start, ulong length, MmapProts prot, MmapFlags flags, int fd, long offset);

		// Token: 0x06000435 RID: 1077
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_munmap", SetLastError = true)]
		public static extern int munmap(IntPtr start, ulong length);

		// Token: 0x06000436 RID: 1078
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mprotect", SetLastError = true)]
		public static extern int mprotect(IntPtr start, ulong len, MmapProts prot);

		// Token: 0x06000437 RID: 1079
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_msync", SetLastError = true)]
		public static extern int msync(IntPtr start, ulong len, MsyncFlags flags);

		// Token: 0x06000438 RID: 1080
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mlock", SetLastError = true)]
		public static extern int mlock(IntPtr start, ulong len);

		// Token: 0x06000439 RID: 1081
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_munlock", SetLastError = true)]
		public static extern int munlock(IntPtr start, ulong len);

		// Token: 0x0600043A RID: 1082
		[DllImport("libc", EntryPoint = "mlockall", SetLastError = true)]
		private static extern int sys_mlockall(int flags);

		// Token: 0x0600043B RID: 1083 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		public static int mlockall(MlockallFlags flags)
		{
			int flags2 = NativeConvert.FromMlockallFlags(flags);
			return Syscall.sys_mlockall(flags2);
		}

		// Token: 0x0600043C RID: 1084
		[DllImport("libc", SetLastError = true)]
		public static extern int munlockall();

		// Token: 0x0600043D RID: 1085
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mremap", SetLastError = true)]
		public static extern IntPtr mremap(IntPtr old_address, ulong old_size, ulong new_size, MremapFlags flags);

		// Token: 0x0600043E RID: 1086
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mincore", SetLastError = true)]
		public static extern int mincore(IntPtr start, ulong length, byte[] vec);

		// Token: 0x0600043F RID: 1087
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_remap_file_pages", SetLastError = true)]
		public static extern int remap_file_pages(IntPtr start, ulong size, MmapProts prot, long pgoff, MmapFlags flags);

		// Token: 0x06000440 RID: 1088
		[DllImport("libc", EntryPoint = "poll", SetLastError = true)]
		private static extern int sys_poll(Syscall._pollfd[] ufds, uint nfds, int timeout);

		// Token: 0x06000441 RID: 1089 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public static int poll(Pollfd[] fds, uint nfds, int timeout)
		{
			if ((long)fds.Length < (long)((ulong)nfds))
			{
				throw new ArgumentOutOfRangeException("fds", "Must refer to at least `nfds' elements");
			}
			Syscall._pollfd[] array = new Syscall._pollfd[nfds];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].fd = fds[i].fd;
				array[i].events = NativeConvert.FromPollEvents(fds[i].events);
			}
			int result = Syscall.sys_poll(array, nfds, timeout);
			for (int j = 0; j < array.Length; j++)
			{
				fds[j].revents = NativeConvert.ToPollEvents(array[j].revents);
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000CB98 File Offset: 0x0000AD98
		public static int poll(Pollfd[] fds, int timeout)
		{
			return Syscall.poll(fds, (uint)fds.Length, timeout);
		}

		// Token: 0x06000443 RID: 1091
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sendfile", SetLastError = true)]
		public static extern long sendfile(int out_fd, int in_fd, ref long offset, ulong count);

		// Token: 0x06000444 RID: 1092
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_stat", SetLastError = true)]
		public static extern int stat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string file_name, out Stat buf);

		// Token: 0x06000445 RID: 1093
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fstat", SetLastError = true)]
		public static extern int fstat(int filedes, out Stat buf);

		// Token: 0x06000446 RID: 1094
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lstat", SetLastError = true)]
		public static extern int lstat([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string file_name, out Stat buf);

		// Token: 0x06000447 RID: 1095
		[DllImport("libc", EntryPoint = "chmod", SetLastError = true)]
		private static extern int sys_chmod([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, uint mode);

		// Token: 0x06000448 RID: 1096 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public static int chmod(string path, FilePermissions mode)
		{
			uint mode2 = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_chmod(path, mode2);
		}

		// Token: 0x06000449 RID: 1097
		[DllImport("libc", EntryPoint = "fchmod", SetLastError = true)]
		private static extern int sys_fchmod(int filedes, uint mode);

		// Token: 0x0600044A RID: 1098 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		public static int fchmod(int filedes, FilePermissions mode)
		{
			uint mode2 = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_fchmod(filedes, mode2);
		}

		// Token: 0x0600044B RID: 1099
		[DllImport("libc", EntryPoint = "umask", SetLastError = true)]
		private static extern uint sys_umask(uint mask);

		// Token: 0x0600044C RID: 1100 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		public static FilePermissions umask(FilePermissions mask)
		{
			uint mask2 = NativeConvert.FromFilePermissions(mask);
			uint value = Syscall.sys_umask(mask2);
			return NativeConvert.ToFilePermissions(value);
		}

		// Token: 0x0600044D RID: 1101
		[DllImport("libc", EntryPoint = "mkdir", SetLastError = true)]
		private static extern int sys_mkdir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, uint mode);

		// Token: 0x0600044E RID: 1102 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public static int mkdir(string oldpath, FilePermissions mode)
		{
			uint mode2 = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_mkdir(oldpath, mode2);
		}

		// Token: 0x0600044F RID: 1103
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_mknod", SetLastError = true)]
		public static extern int mknod([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, FilePermissions mode, ulong dev);

		// Token: 0x06000450 RID: 1104
		[DllImport("libc", EntryPoint = "mkfifo", SetLastError = true)]
		private static extern int sys_mkfifo([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, uint mode);

		// Token: 0x06000451 RID: 1105 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public static int mkfifo(string pathname, FilePermissions mode)
		{
			uint mode2 = NativeConvert.FromFilePermissions(mode);
			return Syscall.sys_mkfifo(pathname, mode2);
		}

		// Token: 0x06000452 RID: 1106
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_statvfs", SetLastError = true)]
		public static extern int statvfs([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, out Statvfs buf);

		// Token: 0x06000453 RID: 1107
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fstatvfs", SetLastError = true)]
		public static extern int fstatvfs(int fd, out Statvfs buf);

		// Token: 0x06000454 RID: 1108
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		public static extern int gettimeofday(out Timeval tv, out Timezone tz);

		// Token: 0x06000455 RID: 1109
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		private static extern int gettimeofday(out Timeval tv, IntPtr ignore);

		// Token: 0x06000456 RID: 1110 RVA: 0x0000CC38 File Offset: 0x0000AE38
		public static int gettimeofday(out Timeval tv)
		{
			return Syscall.gettimeofday(out tv, IntPtr.Zero);
		}

		// Token: 0x06000457 RID: 1111
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		private static extern int gettimeofday(IntPtr ignore, out Timezone tz);

		// Token: 0x06000458 RID: 1112 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public static int gettimeofday(out Timezone tz)
		{
			return Syscall.gettimeofday(IntPtr.Zero, out tz);
		}

		// Token: 0x06000459 RID: 1113
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_settimeofday", SetLastError = true)]
		public static extern int settimeofday(ref Timeval tv, ref Timezone tz);

		// Token: 0x0600045A RID: 1114
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gettimeofday", SetLastError = true)]
		private static extern int settimeofday(ref Timeval tv, IntPtr ignore);

		// Token: 0x0600045B RID: 1115 RVA: 0x0000CC58 File Offset: 0x0000AE58
		public static int settimeofday(ref Timeval tv)
		{
			return Syscall.settimeofday(ref tv, IntPtr.Zero);
		}

		// Token: 0x0600045C RID: 1116
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_utimes", SetLastError = true)]
		private static extern int sys_utimes([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename, Timeval[] tvp);

		// Token: 0x0600045D RID: 1117 RVA: 0x0000CC68 File Offset: 0x0000AE68
		public static int utimes(string filename, Timeval[] tvp)
		{
			if (tvp != null && tvp.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			return Syscall.sys_utimes(filename, tvp);
		}

		// Token: 0x0600045E RID: 1118
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lutimes", SetLastError = true)]
		private static extern int sys_lutimes([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename, Timeval[] tvp);

		// Token: 0x0600045F RID: 1119 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public static int lutimes(string filename, Timeval[] tvp)
		{
			if (tvp != null && tvp.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			return Syscall.sys_lutimes(filename, tvp);
		}

		// Token: 0x06000460 RID: 1120
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_futimes", SetLastError = true)]
		private static extern int sys_futimes(int fd, Timeval[] tvp);

		// Token: 0x06000461 RID: 1121 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		public static int futimes(int fd, Timeval[] tvp)
		{
			if (tvp != null && tvp.Length != 2)
			{
				Stdlib.SetLastError(Errno.EINVAL);
				return -1;
			}
			return Syscall.sys_futimes(fd, tvp);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		private static void CopyUtsname(ref Utsname to, ref Syscall._Utsname from)
		{
			try
			{
				to = new Utsname();
				to.sysname = UnixMarshal.PtrToString(from.sysname);
				to.nodename = UnixMarshal.PtrToString(from.nodename);
				to.release = UnixMarshal.PtrToString(from.release);
				to.version = UnixMarshal.PtrToString(from.version);
				to.machine = UnixMarshal.PtrToString(from.machine);
				to.domainname = UnixMarshal.PtrToString(from.domainname);
			}
			finally
			{
				Stdlib.free(from._buf_);
				from._buf_ = IntPtr.Zero;
			}
		}

		// Token: 0x06000463 RID: 1123
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_uname", SetLastError = true)]
		private static extern int sys_uname(out Syscall._Utsname buf);

		// Token: 0x06000464 RID: 1124 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		public static int uname(out Utsname buf)
		{
			Syscall._Utsname utsname;
			int num = Syscall.sys_uname(out utsname);
			buf = new Utsname();
			if (num == 0)
			{
				Syscall.CopyUtsname(ref buf, ref utsname);
			}
			return num;
		}

		// Token: 0x06000465 RID: 1125
		[DllImport("libc", SetLastError = true)]
		public static extern int wait(out int status);

		// Token: 0x06000466 RID: 1126
		[DllImport("libc", SetLastError = true)]
		private static extern int waitpid(int pid, out int status, int options);

		// Token: 0x06000467 RID: 1127 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public static int waitpid(int pid, out int status, WaitOptions options)
		{
			int options2 = NativeConvert.FromWaitOptions(options);
			return Syscall.waitpid(pid, out status, options2);
		}

		// Token: 0x06000468 RID: 1128
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WIFEXITED")]
		private static extern int _WIFEXITED(int status);

		// Token: 0x06000469 RID: 1129 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		public static bool WIFEXITED(int status)
		{
			return Syscall._WIFEXITED(status) != 0;
		}

		// Token: 0x0600046A RID: 1130
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WEXITSTATUS")]
		public static extern int WEXITSTATUS(int status);

		// Token: 0x0600046B RID: 1131
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WIFSIGNALED")]
		private static extern int _WIFSIGNALED(int status);

		// Token: 0x0600046C RID: 1132 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		public static bool WIFSIGNALED(int status)
		{
			return Syscall._WIFSIGNALED(status) != 0;
		}

		// Token: 0x0600046D RID: 1133
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WTERMSIG")]
		private static extern int _WTERMSIG(int status);

		// Token: 0x0600046E RID: 1134 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public static Signum WTERMSIG(int status)
		{
			int value = Syscall._WTERMSIG(status);
			return NativeConvert.ToSignum(value);
		}

		// Token: 0x0600046F RID: 1135
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WIFSTOPPED")]
		private static extern int _WIFSTOPPED(int status);

		// Token: 0x06000470 RID: 1136 RVA: 0x0000CE10 File Offset: 0x0000B010
		public static bool WIFSTOPPED(int status)
		{
			return Syscall._WIFSTOPPED(status) != 0;
		}

		// Token: 0x06000471 RID: 1137
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_WSTOPSIG")]
		private static extern int _WSTOPSIG(int status);

		// Token: 0x06000472 RID: 1138 RVA: 0x0000CE20 File Offset: 0x0000B020
		public static Signum WSTOPSIG(int status)
		{
			int value = Syscall._WSTOPSIG(status);
			return NativeConvert.ToSignum(value);
		}

		// Token: 0x06000473 RID: 1139
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_openlog", SetLastError = true)]
		private static extern int sys_openlog(IntPtr ident, int option, int facility);

		// Token: 0x06000474 RID: 1140 RVA: 0x0000CE3C File Offset: 0x0000B03C
		public static int openlog(IntPtr ident, SyslogOptions option, SyslogFacility defaultFacility)
		{
			int option2 = NativeConvert.FromSyslogOptions(option);
			int facility = NativeConvert.FromSyslogFacility(defaultFacility);
			return Syscall.sys_openlog(ident, option2, facility);
		}

		// Token: 0x06000475 RID: 1141
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_syslog", SetLastError = true)]
		private static extern int sys_syslog(int priority, string message);

		// Token: 0x06000476 RID: 1142 RVA: 0x0000CE60 File Offset: 0x0000B060
		public static int syslog(SyslogFacility facility, SyslogLevel level, string message)
		{
			int num = NativeConvert.FromSyslogFacility(facility);
			int num2 = NativeConvert.FromSyslogLevel(level);
			return Syscall.sys_syslog(num | num2, Syscall.GetSyslogMessage(message));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public static int syslog(SyslogLevel level, string message)
		{
			int priority = NativeConvert.FromSyslogLevel(level);
			return Syscall.sys_syslog(priority, Syscall.GetSyslogMessage(message));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		private static string GetSyslogMessage(string message)
		{
			return UnixMarshal.EscapeFormatString(message, new char[]
			{
				'm'
			});
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse syslog(SyslogFacility, SyslogLevel, string) instead.")]
		public static int syslog(SyslogFacility facility, SyslogLevel level, string format, params object[] parameters)
		{
			int num = NativeConvert.FromSyslogFacility(facility);
			int num2 = NativeConvert.FromSyslogLevel(level);
			object[] array = new object[checked(parameters.Length + 2)];
			array[0] = (num | num2);
			array[1] = format;
			Array.Copy(parameters, 0, array, 2, parameters.Length);
			return (int)XPrintfFunctions.syslog(array);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000CF14 File Offset: 0x0000B114
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse syslog(SyslogLevel, string) instead.")]
		public static int syslog(SyslogLevel level, string format, params object[] parameters)
		{
			int num = NativeConvert.FromSyslogLevel(level);
			object[] array = new object[checked(parameters.Length + 2)];
			array[0] = num;
			array[1] = format;
			Array.Copy(parameters, 0, array, 2, parameters.Length);
			return (int)XPrintfFunctions.syslog(array);
		}

		// Token: 0x0600047B RID: 1147
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_closelog", SetLastError = true)]
		public static extern int closelog();

		// Token: 0x0600047C RID: 1148
		[DllImport("libc", EntryPoint = "setlogmask", SetLastError = true)]
		private static extern int sys_setlogmask(int mask);

		// Token: 0x0600047D RID: 1149 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public static int setlogmask(SyslogLevel mask)
		{
			int mask2 = NativeConvert.FromSyslogLevel(mask);
			return Syscall.sys_setlogmask(mask2);
		}

		// Token: 0x0600047E RID: 1150
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_nanosleep", SetLastError = true)]
		public static extern int nanosleep(ref Timespec req, ref Timespec rem);

		// Token: 0x0600047F RID: 1151
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_stime", SetLastError = true)]
		public static extern int stime(ref long t);

		// Token: 0x06000480 RID: 1152
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_time", SetLastError = true)]
		public static extern long time(out long t);

		// Token: 0x06000481 RID: 1153
		[DllImport("libc", EntryPoint = "access", SetLastError = true)]
		private static extern int sys_access([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname, int mode);

		// Token: 0x06000482 RID: 1154 RVA: 0x0000CF78 File Offset: 0x0000B178
		public static int access(string pathname, AccessModes mode)
		{
			int mode2 = NativeConvert.FromAccessModes(mode);
			return Syscall.sys_access(pathname, mode2);
		}

		// Token: 0x06000483 RID: 1155
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lseek", SetLastError = true)]
		private static extern long sys_lseek(int fd, long offset, int whence);

		// Token: 0x06000484 RID: 1156 RVA: 0x0000CF94 File Offset: 0x0000B194
		public static long lseek(int fd, long offset, SeekFlags whence)
		{
			short whence2 = NativeConvert.FromSeekFlags(whence);
			return Syscall.sys_lseek(fd, offset, (int)whence2);
		}

		// Token: 0x06000485 RID: 1157
		[DllImport("libc", SetLastError = true)]
		public static extern int close(int fd);

		// Token: 0x06000486 RID: 1158
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_read", SetLastError = true)]
		public static extern long read(int fd, IntPtr buf, ulong count);

		// Token: 0x06000487 RID: 1159 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public unsafe static long read(int fd, void* buf, ulong count)
		{
			return Syscall.read(fd, (IntPtr)buf, count);
		}

		// Token: 0x06000488 RID: 1160
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_write", SetLastError = true)]
		public static extern long write(int fd, IntPtr buf, ulong count);

		// Token: 0x06000489 RID: 1161 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public unsafe static long write(int fd, void* buf, ulong count)
		{
			return Syscall.write(fd, (IntPtr)buf, count);
		}

		// Token: 0x0600048A RID: 1162
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pread", SetLastError = true)]
		public static extern long pread(int fd, IntPtr buf, ulong count, long offset);

		// Token: 0x0600048B RID: 1163 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		public unsafe static long pread(int fd, void* buf, ulong count, long offset)
		{
			return Syscall.pread(fd, (IntPtr)buf, count, offset);
		}

		// Token: 0x0600048C RID: 1164
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pwrite", SetLastError = true)]
		public static extern long pwrite(int fd, IntPtr buf, ulong count, long offset);

		// Token: 0x0600048D RID: 1165 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
		public unsafe static long pwrite(int fd, void* buf, ulong count, long offset)
		{
			return Syscall.pwrite(fd, (IntPtr)buf, count, offset);
		}

		// Token: 0x0600048E RID: 1166
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pipe", SetLastError = true)]
		public static extern int pipe(out int reading, out int writing);

		// Token: 0x0600048F RID: 1167 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public static int pipe(int[] filedes)
		{
			if (filedes == null || filedes.Length != 2)
			{
				return -1;
			}
			int num;
			int num2;
			int result = Syscall.pipe(out num, out num2);
			filedes[0] = num;
			filedes[1] = num2;
			return result;
		}

		// Token: 0x06000490 RID: 1168
		[DllImport("libc", SetLastError = true)]
		public static extern uint alarm(uint seconds);

		// Token: 0x06000491 RID: 1169
		[DllImport("libc", SetLastError = true)]
		public static extern uint sleep(uint seconds);

		// Token: 0x06000492 RID: 1170
		[DllImport("libc", SetLastError = true)]
		public static extern uint ualarm(uint usecs, uint interval);

		// Token: 0x06000493 RID: 1171
		[DllImport("libc", SetLastError = true)]
		public static extern int pause();

		// Token: 0x06000494 RID: 1172
		[DllImport("libc", SetLastError = true)]
		public static extern int chown([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, uint owner, uint group);

		// Token: 0x06000495 RID: 1173
		[DllImport("libc", SetLastError = true)]
		public static extern int fchown(int fd, uint owner, uint group);

		// Token: 0x06000496 RID: 1174
		[DllImport("libc", SetLastError = true)]
		public static extern int lchown([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, uint owner, uint group);

		// Token: 0x06000497 RID: 1175
		[DllImport("libc", SetLastError = true)]
		public static extern int chdir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path);

		// Token: 0x06000498 RID: 1176
		[DllImport("libc", SetLastError = true)]
		public static extern int fchdir(int fd);

		// Token: 0x06000499 RID: 1177
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getcwd", SetLastError = true)]
		public static extern IntPtr getcwd([Out] StringBuilder buf, ulong size);

		// Token: 0x0600049A RID: 1178 RVA: 0x0000D024 File Offset: 0x0000B224
		public static StringBuilder getcwd(StringBuilder buf)
		{
			Syscall.getcwd(buf, (ulong)((long)buf.Capacity));
			return buf;
		}

		// Token: 0x0600049B RID: 1179
		[DllImport("libc", SetLastError = true)]
		public static extern int dup(int fd);

		// Token: 0x0600049C RID: 1180
		[DllImport("libc", SetLastError = true)]
		public static extern int dup2(int fd, int fd2);

		// Token: 0x0600049D RID: 1181
		[DllImport("libc", SetLastError = true)]
		public static extern int execve([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string[] argv, string[] envp);

		// Token: 0x0600049E RID: 1182
		[DllImport("libc", SetLastError = true)]
		public static extern int fexecve(int fd, string[] argv, string[] envp);

		// Token: 0x0600049F RID: 1183
		[DllImport("libc", SetLastError = true)]
		public static extern int execv([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string[] argv);

		// Token: 0x060004A0 RID: 1184
		[DllImport("libc", SetLastError = true)]
		public static extern int execvp([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string[] argv);

		// Token: 0x060004A1 RID: 1185
		[DllImport("libc", SetLastError = true)]
		public static extern int nice(int inc);

		// Token: 0x060004A2 RID: 1186
		[CLSCompliant(false)]
		[DllImport("libc", SetLastError = true)]
		public static extern int _exit(int status);

		// Token: 0x060004A3 RID: 1187
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_fpathconf", SetLastError = true)]
		public static extern long fpathconf(int filedes, PathconfName name, Errno defaultError);

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000D038 File Offset: 0x0000B238
		public static long fpathconf(int filedes, PathconfName name)
		{
			return Syscall.fpathconf(filedes, name, (Errno)0);
		}

		// Token: 0x060004A5 RID: 1189
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_pathconf", SetLastError = true)]
		public static extern long pathconf([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, PathconfName name, Errno defaultError);

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000D044 File Offset: 0x0000B244
		public static long pathconf(string path, PathconfName name)
		{
			return Syscall.pathconf(path, name, (Errno)0);
		}

		// Token: 0x060004A7 RID: 1191
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sysconf", SetLastError = true)]
		public static extern long sysconf(SysconfName name, Errno defaultError);

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000D050 File Offset: 0x0000B250
		public static long sysconf(SysconfName name)
		{
			return Syscall.sysconf(name, (Errno)0);
		}

		// Token: 0x060004A9 RID: 1193
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_confstr", SetLastError = true)]
		public static extern ulong confstr(ConfstrName name, [Out] StringBuilder buf, ulong len);

		// Token: 0x060004AA RID: 1194
		[DllImport("libc", SetLastError = true)]
		public static extern int getpid();

		// Token: 0x060004AB RID: 1195
		[DllImport("libc", SetLastError = true)]
		public static extern int getppid();

		// Token: 0x060004AC RID: 1196
		[DllImport("libc", SetLastError = true)]
		public static extern int setpgid(int pid, int pgid);

		// Token: 0x060004AD RID: 1197
		[DllImport("libc", SetLastError = true)]
		public static extern int getpgid(int pid);

		// Token: 0x060004AE RID: 1198
		[DllImport("libc", SetLastError = true)]
		public static extern int setpgrp();

		// Token: 0x060004AF RID: 1199
		[DllImport("libc", SetLastError = true)]
		public static extern int getpgrp();

		// Token: 0x060004B0 RID: 1200
		[DllImport("libc", SetLastError = true)]
		public static extern int setsid();

		// Token: 0x060004B1 RID: 1201
		[DllImport("libc", SetLastError = true)]
		public static extern int getsid(int pid);

		// Token: 0x060004B2 RID: 1202
		[DllImport("libc", SetLastError = true)]
		public static extern uint getuid();

		// Token: 0x060004B3 RID: 1203
		[DllImport("libc", SetLastError = true)]
		public static extern uint geteuid();

		// Token: 0x060004B4 RID: 1204
		[DllImport("libc", SetLastError = true)]
		public static extern uint getgid();

		// Token: 0x060004B5 RID: 1205
		[DllImport("libc", SetLastError = true)]
		public static extern uint getegid();

		// Token: 0x060004B6 RID: 1206
		[DllImport("libc", SetLastError = true)]
		public static extern int getgroups(int size, uint[] list);

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000D05C File Offset: 0x0000B25C
		public static int getgroups(uint[] list)
		{
			return Syscall.getgroups(list.Length, list);
		}

		// Token: 0x060004B8 RID: 1208
		[DllImport("libc", SetLastError = true)]
		public static extern int setuid(uint uid);

		// Token: 0x060004B9 RID: 1209
		[DllImport("libc", SetLastError = true)]
		public static extern int setreuid(uint ruid, uint euid);

		// Token: 0x060004BA RID: 1210
		[DllImport("libc", SetLastError = true)]
		public static extern int setregid(uint rgid, uint egid);

		// Token: 0x060004BB RID: 1211
		[DllImport("libc", SetLastError = true)]
		public static extern int seteuid(uint euid);

		// Token: 0x060004BC RID: 1212
		[DllImport("libc", SetLastError = true)]
		public static extern int setegid(uint uid);

		// Token: 0x060004BD RID: 1213
		[DllImport("libc", SetLastError = true)]
		public static extern int setgid(uint gid);

		// Token: 0x060004BE RID: 1214
		[DllImport("libc", SetLastError = true)]
		public static extern int getresuid(out uint ruid, out uint euid, out uint suid);

		// Token: 0x060004BF RID: 1215
		[DllImport("libc", SetLastError = true)]
		public static extern int getresgid(out uint rgid, out uint egid, out uint sgid);

		// Token: 0x060004C0 RID: 1216
		[DllImport("libc", SetLastError = true)]
		public static extern int setresuid(uint ruid, uint euid, uint suid);

		// Token: 0x060004C1 RID: 1217
		[DllImport("libc", SetLastError = true)]
		public static extern int setresgid(uint rgid, uint egid, uint sgid);

		// Token: 0x060004C2 RID: 1218
		[DllImport("libc", EntryPoint = "ttyname", SetLastError = true)]
		private static extern IntPtr sys_ttyname(int fd);

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D068 File Offset: 0x0000B268
		public static string ttyname(int fd)
		{
			object obj = Syscall.tty_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Syscall.sys_ttyname(fd);
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x060004C4 RID: 1220
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_ttyname_r", SetLastError = true)]
		public static extern int ttyname_r(int fd, [Out] StringBuilder buf, ulong buflen);

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public static int ttyname_r(int fd, StringBuilder buf)
		{
			return Syscall.ttyname_r(fd, buf, (ulong)((long)buf.Capacity));
		}

		// Token: 0x060004C6 RID: 1222
		[DllImport("libc", EntryPoint = "isatty")]
		private static extern int sys_isatty(int fd);

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		public static bool isatty(int fd)
		{
			return Syscall.sys_isatty(fd) == 1;
		}

		// Token: 0x060004C8 RID: 1224
		[DllImport("libc", SetLastError = true)]
		public static extern int link([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x060004C9 RID: 1225
		[DllImport("libc", SetLastError = true)]
		public static extern int symlink([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x060004CA RID: 1226
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_readlink", SetLastError = true)]
		public static extern int readlink([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, [Out] StringBuilder buf, ulong bufsiz);

		// Token: 0x060004CB RID: 1227 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		public static int readlink(string path, [Out] StringBuilder buf)
		{
			return Syscall.readlink(path, buf, (ulong)((long)buf.Capacity));
		}

		// Token: 0x060004CC RID: 1228
		[DllImport("libc", SetLastError = true)]
		public static extern int unlink([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname);

		// Token: 0x060004CD RID: 1229
		[DllImport("libc", SetLastError = true)]
		public static extern int rmdir([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string pathname);

		// Token: 0x060004CE RID: 1230
		[DllImport("libc", SetLastError = true)]
		public static extern int tcgetpgrp(int fd);

		// Token: 0x060004CF RID: 1231
		[DllImport("libc", SetLastError = true)]
		public static extern int tcsetpgrp(int fd, int pgrp);

		// Token: 0x060004D0 RID: 1232
		[DllImport("libc", EntryPoint = "getlogin", SetLastError = true)]
		private static extern IntPtr sys_getlogin();

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		public static string getlogin()
		{
			object obj = Syscall.getlogin_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Syscall.sys_getlogin();
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x060004D2 RID: 1234
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getlogin_r", SetLastError = true)]
		public static extern int getlogin_r([Out] StringBuilder name, ulong bufsize);

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000D140 File Offset: 0x0000B340
		public static int getlogin_r(StringBuilder name)
		{
			return Syscall.getlogin_r(name, (ulong)((long)name.Capacity));
		}

		// Token: 0x060004D4 RID: 1236
		[DllImport("libc", SetLastError = true)]
		public static extern int setlogin(string name);

		// Token: 0x060004D5 RID: 1237
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gethostname", SetLastError = true)]
		public static extern int gethostname([Out] StringBuilder name, ulong len);

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000D150 File Offset: 0x0000B350
		public static int gethostname(StringBuilder name)
		{
			return Syscall.gethostname(name, (ulong)((long)name.Capacity));
		}

		// Token: 0x060004D7 RID: 1239
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sethostname", SetLastError = true)]
		public static extern int sethostname(string name, ulong len);

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000D160 File Offset: 0x0000B360
		public static int sethostname(string name)
		{
			return Syscall.sethostname(name, (ulong)((long)name.Length));
		}

		// Token: 0x060004D9 RID: 1241
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_gethostid", SetLastError = true)]
		public static extern long gethostid();

		// Token: 0x060004DA RID: 1242
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sethostid", SetLastError = true)]
		public static extern int sethostid(long hostid);

		// Token: 0x060004DB RID: 1243
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_getdomainname", SetLastError = true)]
		public static extern int getdomainname([Out] StringBuilder name, ulong len);

		// Token: 0x060004DC RID: 1244 RVA: 0x0000D170 File Offset: 0x0000B370
		public static int getdomainname(StringBuilder name)
		{
			return Syscall.getdomainname(name, (ulong)((long)name.Capacity));
		}

		// Token: 0x060004DD RID: 1245
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setdomainname", SetLastError = true)]
		public static extern int setdomainname(string name, ulong len);

		// Token: 0x060004DE RID: 1246 RVA: 0x0000D180 File Offset: 0x0000B380
		public static int setdomainname(string name)
		{
			return Syscall.setdomainname(name, (ulong)((long)name.Length));
		}

		// Token: 0x060004DF RID: 1247
		[DllImport("libc", SetLastError = true)]
		public static extern int vhangup();

		// Token: 0x060004E0 RID: 1248
		[DllImport("libc", SetLastError = true)]
		public static extern int revoke([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string file);

		// Token: 0x060004E1 RID: 1249
		[DllImport("libc", SetLastError = true)]
		public static extern int acct([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename);

		// Token: 0x060004E2 RID: 1250
		[DllImport("libc", EntryPoint = "getusershell", SetLastError = true)]
		private static extern IntPtr sys_getusershell();

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000D190 File Offset: 0x0000B390
		public static string getusershell()
		{
			object obj = Syscall.usershell_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Syscall.sys_getusershell();
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x060004E4 RID: 1252
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_setusershell", SetLastError = true)]
		private static extern int sys_setusershell();

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		public static int setusershell()
		{
			object obj = Syscall.usershell_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_setusershell();
			}
			return result;
		}

		// Token: 0x060004E6 RID: 1254
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_endusershell", SetLastError = true)]
		private static extern int sys_endusershell();

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000D234 File Offset: 0x0000B434
		public static int endusershell()
		{
			object obj = Syscall.usershell_lock;
			int result;
			lock (obj)
			{
				result = Syscall.sys_endusershell();
			}
			return result;
		}

		// Token: 0x060004E8 RID: 1256
		[DllImport("libc", SetLastError = true)]
		public static extern int chroot([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path);

		// Token: 0x060004E9 RID: 1257
		[DllImport("libc", SetLastError = true)]
		public static extern int fsync(int fd);

		// Token: 0x060004EA RID: 1258
		[DllImport("libc", SetLastError = true)]
		public static extern int fdatasync(int fd);

		// Token: 0x060004EB RID: 1259
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_sync", SetLastError = true)]
		public static extern int sync();

		// Token: 0x060004EC RID: 1260
		[Obsolete("Dropped in POSIX 1003.1-2001.  Use Syscall.sysconf (SysconfName._SC_PAGESIZE).")]
		[DllImport("libc", SetLastError = true)]
		public static extern int getpagesize();

		// Token: 0x060004ED RID: 1261
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_truncate", SetLastError = true)]
		public static extern int truncate([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, long length);

		// Token: 0x060004EE RID: 1262
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_ftruncate", SetLastError = true)]
		public static extern int ftruncate(int fd, long length);

		// Token: 0x060004EF RID: 1263
		[DllImport("libc", SetLastError = true)]
		public static extern int getdtablesize();

		// Token: 0x060004F0 RID: 1264
		[DllImport("libc", SetLastError = true)]
		public static extern int brk(IntPtr end_data_segment);

		// Token: 0x060004F1 RID: 1265
		[DllImport("libc", SetLastError = true)]
		public static extern IntPtr sbrk(IntPtr increment);

		// Token: 0x060004F2 RID: 1266
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lockf", SetLastError = true)]
		public static extern int lockf(int fd, LockfCommand cmd, long len);

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D284 File Offset: 0x0000B484
		[Obsolete("This is insecure and should not be used", true)]
		public static string crypt(string key, string salt)
		{
			throw new SecurityException("crypt(3) has been broken.  Use something more secure.");
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D290 File Offset: 0x0000B490
		[Obsolete("This is insecure and should not be used", true)]
		public static int encrypt(byte[] block, bool decode)
		{
			throw new SecurityException("crypt(3) has been broken.  Use something more secure.");
		}

		// Token: 0x060004F5 RID: 1269
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_swab", SetLastError = true)]
		public static extern int swab(IntPtr from, IntPtr to, long n);

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D29C File Offset: 0x0000B49C
		public unsafe static void swab(void* from, void* to, long n)
		{
			Syscall.swab((IntPtr)from, (IntPtr)to, n);
		}

		// Token: 0x060004F7 RID: 1271
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_utime", SetLastError = true)]
		private static extern int sys_utime([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename, ref Utimbuf buf, int use_buf);

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		public static int utime(string filename, ref Utimbuf buf)
		{
			return Syscall.sys_utime(filename, ref buf, 1);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		public static int utime(string filename)
		{
			Utimbuf utimbuf = default(Utimbuf);
			return Syscall.sys_utime(filename, ref utimbuf, 0);
		}

		// Token: 0x0400037F RID: 895
		internal new const string LIBC = "libc";

		// Token: 0x04000380 RID: 896
		internal static object readdir_lock = new object();

		// Token: 0x04000381 RID: 897
		internal static object fstab_lock = new object();

		// Token: 0x04000382 RID: 898
		internal static object grp_lock = new object();

		// Token: 0x04000383 RID: 899
		internal static object pwd_lock = new object();

		// Token: 0x04000384 RID: 900
		private static object signal_lock = new object();

		// Token: 0x04000385 RID: 901
		public static readonly int L_ctermid = Syscall._L_ctermid();

		// Token: 0x04000386 RID: 902
		public static readonly int L_cuserid = Syscall._L_cuserid();

		// Token: 0x04000387 RID: 903
		internal static object getlogin_lock = new object();

		// Token: 0x04000388 RID: 904
		public static readonly IntPtr MAP_FAILED = (IntPtr)(-1);

		// Token: 0x04000389 RID: 905
		private static object tty_lock = new object();

		// Token: 0x0400038A RID: 906
		internal static object usershell_lock = new object();

		// Token: 0x02000057 RID: 87
		private struct _Dirent
		{
			// Token: 0x0400038B RID: 907
			[ino_t]
			public ulong d_ino;

			// Token: 0x0400038C RID: 908
			[off_t]
			public long d_off;

			// Token: 0x0400038D RID: 909
			public ushort d_reclen;

			// Token: 0x0400038E RID: 910
			public byte d_type;

			// Token: 0x0400038F RID: 911
			public IntPtr d_name;
		}

		// Token: 0x02000058 RID: 88
		[Map]
		private struct _Fstab
		{
			// Token: 0x04000390 RID: 912
			public IntPtr fs_spec;

			// Token: 0x04000391 RID: 913
			public IntPtr fs_file;

			// Token: 0x04000392 RID: 914
			public IntPtr fs_vfstype;

			// Token: 0x04000393 RID: 915
			public IntPtr fs_mntops;

			// Token: 0x04000394 RID: 916
			public IntPtr fs_type;

			// Token: 0x04000395 RID: 917
			public int fs_freq;

			// Token: 0x04000396 RID: 918
			public int fs_passno;

			// Token: 0x04000397 RID: 919
			public IntPtr _fs_buf_;
		}

		// Token: 0x02000059 RID: 89
		[Map]
		private struct _Group
		{
			// Token: 0x04000398 RID: 920
			public IntPtr gr_name;

			// Token: 0x04000399 RID: 921
			public IntPtr gr_passwd;

			// Token: 0x0400039A RID: 922
			[gid_t]
			public uint gr_gid;

			// Token: 0x0400039B RID: 923
			public int _gr_nmem_;

			// Token: 0x0400039C RID: 924
			public IntPtr gr_mem;

			// Token: 0x0400039D RID: 925
			public IntPtr _gr_buf_;
		}

		// Token: 0x0200005A RID: 90
		[Map]
		private struct _Passwd
		{
			// Token: 0x0400039E RID: 926
			public IntPtr pw_name;

			// Token: 0x0400039F RID: 927
			public IntPtr pw_passwd;

			// Token: 0x040003A0 RID: 928
			[uid_t]
			public uint pw_uid;

			// Token: 0x040003A1 RID: 929
			[gid_t]
			public uint pw_gid;

			// Token: 0x040003A2 RID: 930
			public IntPtr pw_gecos;

			// Token: 0x040003A3 RID: 931
			public IntPtr pw_dir;

			// Token: 0x040003A4 RID: 932
			public IntPtr pw_shell;

			// Token: 0x040003A5 RID: 933
			public IntPtr _pw_buf_;
		}

		// Token: 0x0200005B RID: 91
		private struct _pollfd
		{
			// Token: 0x040003A6 RID: 934
			public int fd;

			// Token: 0x040003A7 RID: 935
			public short events;

			// Token: 0x040003A8 RID: 936
			public short revents;
		}

		// Token: 0x0200005C RID: 92
		[Map]
		private struct _Utsname
		{
			// Token: 0x040003A9 RID: 937
			public IntPtr sysname;

			// Token: 0x040003AA RID: 938
			public IntPtr nodename;

			// Token: 0x040003AB RID: 939
			public IntPtr release;

			// Token: 0x040003AC RID: 940
			public IntPtr version;

			// Token: 0x040003AD RID: 941
			public IntPtr machine;

			// Token: 0x040003AE RID: 942
			public IntPtr domainname;

			// Token: 0x040003AF RID: 943
			public IntPtr _buf_;
		}
	}
}
