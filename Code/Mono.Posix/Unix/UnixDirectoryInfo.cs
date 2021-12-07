using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200000E RID: 14
	public sealed class UnixDirectoryInfo : UnixFileSystemInfo
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000032D8 File Offset: 0x000014D8
		public UnixDirectoryInfo(string path) : base(path)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000032E4 File Offset: 0x000014E4
		internal UnixDirectoryInfo(string path, Stat stat) : base(path, stat)
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000032F0 File Offset: 0x000014F0
		public override string Name
		{
			get
			{
				string fileName = UnixPath.GetFileName(base.FullPath);
				if (fileName == null || fileName.Length == 0)
				{
					return base.FullPath;
				}
				return fileName;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003324 File Offset: 0x00001524
		public UnixDirectoryInfo Parent
		{
			get
			{
				if (base.FullPath == "/")
				{
					return this;
				}
				string directoryName = UnixPath.GetDirectoryName(base.FullPath);
				if (directoryName == string.Empty)
				{
					throw new InvalidOperationException("Do not know parent directory for path `" + base.FullPath + "'");
				}
				return new UnixDirectoryInfo(directoryName);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003388 File Offset: 0x00001588
		public UnixDirectoryInfo Root
		{
			get
			{
				string pathRoot = UnixPath.GetPathRoot(base.FullPath);
				if (pathRoot == null)
				{
					return null;
				}
				return new UnixDirectoryInfo(pathRoot);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033B0 File Offset: 0x000015B0
		[CLSCompliant(false)]
		public void Create(FilePermissions mode)
		{
			int retval = Syscall.mkdir(base.FullPath, mode);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			base.Refresh();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000033D8 File Offset: 0x000015D8
		public void Create(FileAccessPermissions mode)
		{
			this.Create((FilePermissions)mode);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033E4 File Offset: 0x000015E4
		public void Create()
		{
			FilePermissions mode = FilePermissions.ACCESSPERMS;
			this.Create(mode);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003400 File Offset: 0x00001600
		public override void Delete()
		{
			this.Delete(false);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000340C File Offset: 0x0000160C
		public void Delete(bool recursive)
		{
			if (recursive)
			{
				foreach (UnixFileSystemInfo unixFileSystemInfo in this.GetFileSystemEntries())
				{
					UnixDirectoryInfo unixDirectoryInfo = unixFileSystemInfo as UnixDirectoryInfo;
					if (unixDirectoryInfo != null)
					{
						unixDirectoryInfo.Delete(true);
					}
					else
					{
						unixFileSystemInfo.Delete();
					}
				}
			}
			int retval = Syscall.rmdir(base.FullPath);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			base.Refresh();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003478 File Offset: 0x00001678
		public Dirent[] GetEntries()
		{
			IntPtr intPtr = Syscall.opendir(base.FullPath);
			if (intPtr == IntPtr.Zero)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			bool flag = false;
			Dirent[] result;
			try
			{
				Dirent[] entries = UnixDirectoryInfo.GetEntries(intPtr);
				flag = true;
				result = entries;
			}
			finally
			{
				int retval = Syscall.closedir(intPtr);
				if (flag)
				{
					UnixMarshal.ThrowExceptionForLastErrorIf(retval);
				}
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034F0 File Offset: 0x000016F0
		private static Dirent[] GetEntries(IntPtr dirp)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr value;
			int num;
			do
			{
				Dirent dirent = new Dirent();
				num = Syscall.readdir_r(dirp, dirent, out value);
				if (num == 0 && value != IntPtr.Zero && dirent.d_name != "." && dirent.d_name != "..")
				{
					arrayList.Add(dirent);
				}
			}
			while (num == 0 && value != IntPtr.Zero);
			if (num != 0)
			{
				UnixMarshal.ThrowExceptionForLastErrorIf(num);
			}
			return (Dirent[])arrayList.ToArray(typeof(Dirent));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003594 File Offset: 0x00001794
		public Dirent[] GetEntries(Regex regex)
		{
			IntPtr intPtr = Syscall.opendir(base.FullPath);
			if (intPtr == IntPtr.Zero)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			Dirent[] entries;
			try
			{
				entries = UnixDirectoryInfo.GetEntries(intPtr, regex);
			}
			finally
			{
				int retval = Syscall.closedir(intPtr);
				UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			}
			return entries;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003600 File Offset: 0x00001800
		private static Dirent[] GetEntries(IntPtr dirp, Regex regex)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr value;
			int num;
			do
			{
				Dirent dirent = new Dirent();
				num = Syscall.readdir_r(dirp, dirent, out value);
				if (num == 0 && value != IntPtr.Zero && regex.Match(dirent.d_name).Success && dirent.d_name != "." && dirent.d_name != "..")
				{
					arrayList.Add(dirent);
				}
			}
			while (num == 0 && value != IntPtr.Zero);
			if (num != 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return (Dirent[])arrayList.ToArray(typeof(Dirent));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000036B8 File Offset: 0x000018B8
		public Dirent[] GetEntries(string regex)
		{
			Regex regex2 = new Regex(regex);
			return this.GetEntries(regex2);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000036D4 File Offset: 0x000018D4
		public UnixFileSystemInfo[] GetFileSystemEntries()
		{
			Dirent[] entries = this.GetEntries();
			return this.GetFileSystemEntries(entries);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000036F0 File Offset: 0x000018F0
		private UnixFileSystemInfo[] GetFileSystemEntries(Dirent[] dentries)
		{
			UnixFileSystemInfo[] array = new UnixFileSystemInfo[dentries.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = UnixFileSystemInfo.GetFileSystemEntry(UnixPath.Combine(base.FullPath, new string[]
				{
					dentries[num].d_name
				}));
			}
			return array;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003740 File Offset: 0x00001940
		public UnixFileSystemInfo[] GetFileSystemEntries(Regex regex)
		{
			Dirent[] entries = this.GetEntries(regex);
			return this.GetFileSystemEntries(entries);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000375C File Offset: 0x0000195C
		public UnixFileSystemInfo[] GetFileSystemEntries(string regex)
		{
			Regex regex2 = new Regex(regex);
			return this.GetFileSystemEntries(regex2);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003778 File Offset: 0x00001978
		public static string GetCurrentDirectory()
		{
			StringBuilder stringBuilder = new StringBuilder(16);
			IntPtr value = IntPtr.Zero;
			do
			{
				stringBuilder.Capacity *= 2;
				value = Syscall.getcwd(stringBuilder, (ulong)((long)stringBuilder.Capacity));
			}
			while (value == IntPtr.Zero && Stdlib.GetLastError() == Errno.ERANGE);
			if (value == IntPtr.Zero)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000037E8 File Offset: 0x000019E8
		public static void SetCurrentDirectory(string path)
		{
			int retval = Syscall.chdir(path);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}
	}
}
