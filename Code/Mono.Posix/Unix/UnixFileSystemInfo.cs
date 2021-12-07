using System;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000017 RID: 23
	public abstract class UnixFileSystemInfo
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000053D0 File Offset: 0x000035D0
		protected UnixFileSystemInfo(string path)
		{
			UnixPath.CheckPath(path);
			this.originalPath = path;
			this.fullPath = UnixPath.GetFullPath(path);
			this.Refresh(true);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005404 File Offset: 0x00003604
		internal UnixFileSystemInfo(string path, Stat stat)
		{
			this.originalPath = path;
			this.fullPath = UnixPath.GetFullPath(path);
			this.stat = stat;
			this.valid = true;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005430 File Offset: 0x00003630
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00005438 File Offset: 0x00003638
		protected string FullPath
		{
			get
			{
				return this.fullPath;
			}
			set
			{
				if (this.fullPath != value)
				{
					UnixPath.CheckPath(value);
					this.valid = false;
					this.fullPath = value;
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005460 File Offset: 0x00003660
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005468 File Offset: 0x00003668
		protected string OriginalPath
		{
			get
			{
				return this.originalPath;
			}
			set
			{
				this.originalPath = value;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005474 File Offset: 0x00003674
		private void AssertValid()
		{
			this.Refresh(false);
			if (!this.valid)
			{
				throw new InvalidOperationException("Path doesn't exist!");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005494 File Offset: 0x00003694
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E5 RID: 229
		public abstract string Name { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000549C File Offset: 0x0000369C
		public bool Exists
		{
			get
			{
				this.Refresh(true);
				return this.valid;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000054AC File Offset: 0x000036AC
		public long Device
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_dev);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000054C4 File Offset: 0x000036C4
		public long Inode
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_ino);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000054DC File Offset: 0x000036DC
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000054F0 File Offset: 0x000036F0
		[CLSCompliant(false)]
		public FilePermissions Protection
		{
			get
			{
				this.AssertValid();
				return this.stat.st_mode;
			}
			set
			{
				int retval = Syscall.chmod(this.FullPath, value);
				UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005510 File Offset: 0x00003710
		public FileTypes FileType
		{
			get
			{
				this.AssertValid();
				return (FileTypes)(this.stat.st_mode & FilePermissions.S_IFMT);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000552C File Offset: 0x0000372C
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00005554 File Offset: 0x00003754
		public FileAccessPermissions FileAccessPermissions
		{
			get
			{
				this.AssertValid();
				int st_mode = (int)this.stat.st_mode;
				return (FileAccessPermissions)(st_mode & 511);
			}
			set
			{
				this.AssertValid();
				int num = (int)this.stat.st_mode;
				num &= -512;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005588 File Offset: 0x00003788
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000055B0 File Offset: 0x000037B0
		public FileSpecialAttributes FileSpecialAttributes
		{
			get
			{
				this.AssertValid();
				int st_mode = (int)this.stat.st_mode;
				return (FileSpecialAttributes)(st_mode & 3584);
			}
			set
			{
				this.AssertValid();
				int num = (int)this.stat.st_mode;
				num &= -3585;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000055E4 File Offset: 0x000037E4
		public long LinkCount
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_nlink);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000055FC File Offset: 0x000037FC
		public UnixUserInfo OwnerUser
		{
			get
			{
				this.AssertValid();
				return new UnixUserInfo(this.stat.st_uid);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005614 File Offset: 0x00003814
		public long OwnerUserId
		{
			get
			{
				this.AssertValid();
				return (long)((ulong)this.stat.st_uid);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005628 File Offset: 0x00003828
		public UnixGroupInfo OwnerGroup
		{
			get
			{
				this.AssertValid();
				return new UnixGroupInfo((long)((ulong)this.stat.st_gid));
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005644 File Offset: 0x00003844
		public long OwnerGroupId
		{
			get
			{
				this.AssertValid();
				return (long)((ulong)this.stat.st_gid);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005658 File Offset: 0x00003858
		public long DeviceType
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_rdev);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005670 File Offset: 0x00003870
		public long Length
		{
			get
			{
				this.AssertValid();
				return this.stat.st_size;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005684 File Offset: 0x00003884
		public long BlockSize
		{
			get
			{
				this.AssertValid();
				return this.stat.st_blksize;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005698 File Offset: 0x00003898
		public long BlocksAllocated
		{
			get
			{
				this.AssertValid();
				return this.stat.st_blocks;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000056AC File Offset: 0x000038AC
		public DateTime LastAccessTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_atime);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000056C4 File Offset: 0x000038C4
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return this.LastAccessTime.ToUniversalTime();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000056E0 File Offset: 0x000038E0
		public DateTime LastWriteTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_mtime);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000056F8 File Offset: 0x000038F8
		public DateTime LastWriteTimeUtc
		{
			get
			{
				return this.LastWriteTime.ToUniversalTime();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005714 File Offset: 0x00003914
		public DateTime LastStatusChangeTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_ctime);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000572C File Offset: 0x0000392C
		public DateTime LastStatusChangeTimeUtc
		{
			get
			{
				return this.LastStatusChangeTime.ToUniversalTime();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005748 File Offset: 0x00003948
		public bool IsDirectory
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFDIR);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005768 File Offset: 0x00003968
		public bool IsCharacterDevice
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFCHR);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005788 File Offset: 0x00003988
		public bool IsBlockDevice
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFBLK);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000057A8 File Offset: 0x000039A8
		public bool IsRegularFile
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFREG);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000057C8 File Offset: 0x000039C8
		public bool IsFifo
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFIFO);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000057E8 File Offset: 0x000039E8
		public bool IsSymbolicLink
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFLNK);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005808 File Offset: 0x00003A08
		public bool IsSocket
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFSOCK);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005828 File Offset: 0x00003A28
		public bool IsSetUser
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISUID);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005848 File Offset: 0x00003A48
		public bool IsSetGroup
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISGID);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005868 File Offset: 0x00003A68
		public bool IsSticky
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISVTX);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005888 File Offset: 0x00003A88
		internal static bool IsFileType(FilePermissions mode, FilePermissions type)
		{
			return (mode & FilePermissions.S_IFMT) == type;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005894 File Offset: 0x00003A94
		internal static bool IsSet(FilePermissions mode, FilePermissions type)
		{
			return (mode & type) == type;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000589C File Offset: 0x00003A9C
		[CLSCompliant(false)]
		public bool CanAccess(AccessModes mode)
		{
			int num = Syscall.access(this.FullPath, mode);
			return num == 0;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000058BC File Offset: 0x00003ABC
		public UnixFileSystemInfo CreateLink(string path)
		{
			int retval = Syscall.link(this.FullName, path);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			return UnixFileSystemInfo.GetFileSystemEntry(path);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000058E4 File Offset: 0x00003AE4
		public UnixSymbolicLinkInfo CreateSymbolicLink(string path)
		{
			int retval = Syscall.symlink(this.FullName, path);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			return new UnixSymbolicLinkInfo(path);
		}

		// Token: 0x0600010E RID: 270
		public abstract void Delete();

		// Token: 0x0600010F RID: 271 RVA: 0x0000590C File Offset: 0x00003B0C
		[CLSCompliant(false)]
		public long GetConfigurationValue(PathconfName name)
		{
			long num = Syscall.pathconf(this.FullPath, name);
			if (num == -1L && Stdlib.GetLastError() != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005940 File Offset: 0x00003B40
		public void Refresh()
		{
			this.Refresh(true);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000594C File Offset: 0x00003B4C
		internal void Refresh(bool force)
		{
			if (this.valid && !force)
			{
				return;
			}
			this.valid = this.GetFileStatus(this.FullPath, out this.stat);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005984 File Offset: 0x00003B84
		protected virtual bool GetFileStatus(string path, out Stat stat)
		{
			return Syscall.stat(path, out stat) == 0;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005990 File Offset: 0x00003B90
		public void SetLength(long length)
		{
			int num;
			do
			{
				num = Syscall.truncate(this.FullPath, length);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000059BC File Offset: 0x00003BBC
		public virtual void SetOwner(long owner, long group)
		{
			uint owner2 = Convert.ToUInt32(owner);
			uint group2 = Convert.ToUInt32(group);
			int retval = Syscall.chown(this.FullPath, owner2, group2);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000059EC File Offset: 0x00003BEC
		public void SetOwner(string owner)
		{
			Passwd passwd = Syscall.getpwnam(owner);
			if (passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid username"), "owner");
			}
			uint pw_uid = passwd.pw_uid;
			uint pw_gid = passwd.pw_gid;
			this.SetOwner((long)((ulong)pw_uid), (long)((ulong)pw_gid));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005A3C File Offset: 0x00003C3C
		public void SetOwner(string owner, string group)
		{
			long owner2 = -1L;
			if (owner != null)
			{
				owner2 = new UnixUserInfo(owner).UserId;
			}
			long group2 = -1L;
			if (group != null)
			{
				group2 = new UnixGroupInfo(group).GroupId;
			}
			this.SetOwner(owner2, group2);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005A7C File Offset: 0x00003C7C
		public void SetOwner(UnixUserInfo owner)
		{
			long owner2;
			long group = owner2 = -1L;
			if (owner != null)
			{
				owner2 = owner.UserId;
				group = owner.GroupId;
			}
			this.SetOwner(owner2, group);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005AAC File Offset: 0x00003CAC
		public void SetOwner(UnixUserInfo owner, UnixGroupInfo group)
		{
			long owner2;
			long group2 = owner2 = -1L;
			if (owner != null)
			{
				owner2 = owner.UserId;
			}
			if (group != null)
			{
				group2 = owner.GroupId;
			}
			this.SetOwner(owner2, group2);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public override string ToString()
		{
			return this.FullPath;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public Stat ToStat()
		{
			this.AssertValid();
			return this.stat;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public static UnixFileSystemInfo GetFileSystemEntry(string path)
		{
			Stat stat;
			int num = Syscall.lstat(path, out stat);
			if (num == -1 && Stdlib.GetLastError() == Errno.ENOENT)
			{
				return new UnixFileInfo(path);
			}
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			if (UnixFileSystemInfo.IsFileType(stat.st_mode, FilePermissions.S_IFDIR))
			{
				return new UnixDirectoryInfo(path, stat);
			}
			if (UnixFileSystemInfo.IsFileType(stat.st_mode, FilePermissions.S_IFLNK))
			{
				return new UnixSymbolicLinkInfo(path, stat);
			}
			return new UnixFileInfo(path, stat);
		}

		// Token: 0x04000061 RID: 97
		internal const FileSpecialAttributes AllSpecialAttributes = FileSpecialAttributes.SetUserId | FileSpecialAttributes.SetGroupId | FileSpecialAttributes.Sticky;

		// Token: 0x04000062 RID: 98
		internal const FileTypes AllFileTypes = (FileTypes)61440;

		// Token: 0x04000063 RID: 99
		private Stat stat;

		// Token: 0x04000064 RID: 100
		private string fullPath;

		// Token: 0x04000065 RID: 101
		private string originalPath;

		// Token: 0x04000066 RID: 102
		private bool valid;
	}
}
