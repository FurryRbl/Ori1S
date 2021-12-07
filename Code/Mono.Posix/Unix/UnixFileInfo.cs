using System;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000016 RID: 22
	public sealed class UnixFileInfo : UnixFileSystemInfo
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000520C File Offset: 0x0000340C
		public UnixFileInfo(string path) : base(path)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005218 File Offset: 0x00003418
		internal UnixFileInfo(string path, Stat stat) : base(path, stat)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005224 File Offset: 0x00003424
		public override string Name
		{
			get
			{
				return UnixPath.GetFileName(base.FullPath);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005234 File Offset: 0x00003434
		public string DirectoryName
		{
			get
			{
				return UnixPath.GetDirectoryName(base.FullPath);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005244 File Offset: 0x00003444
		public UnixDirectoryInfo Directory
		{
			get
			{
				return new UnixDirectoryInfo(this.DirectoryName);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005254 File Offset: 0x00003454
		public override void Delete()
		{
			int retval = Syscall.unlink(base.FullPath);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			base.Refresh();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000527C File Offset: 0x0000347C
		public UnixStream Create()
		{
			FilePermissions mode = FilePermissions.S_IRUSR | FilePermissions.S_IWUSR | FilePermissions.S_IRGRP | FilePermissions.S_IROTH;
			return this.Create(mode);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005298 File Offset: 0x00003498
		[CLSCompliant(false)]
		public UnixStream Create(FilePermissions mode)
		{
			int num = Syscall.creat(base.FullPath, mode);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			base.Refresh();
			return new UnixStream(num);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000052CC File Offset: 0x000034CC
		public UnixStream Create(FileAccessPermissions mode)
		{
			return this.Create((FilePermissions)mode);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000052D8 File Offset: 0x000034D8
		[CLSCompliant(false)]
		public UnixStream Open(OpenFlags flags)
		{
			if ((flags & OpenFlags.O_CREAT) != OpenFlags.O_RDONLY)
			{
				throw new ArgumentException("Cannot specify OpenFlags.O_CREAT without providing FilePermissions.  Use the Open(OpenFlags, FilePermissions) method instead");
			}
			int num = Syscall.open(base.FullPath, flags);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return new UnixStream(num);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005318 File Offset: 0x00003518
		[CLSCompliant(false)]
		public UnixStream Open(OpenFlags flags, FilePermissions mode)
		{
			int num = Syscall.open(base.FullPath, flags, mode);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return new UnixStream(num);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005348 File Offset: 0x00003548
		public UnixStream Open(FileMode mode)
		{
			OpenFlags flags = NativeConvert.ToOpenFlags(mode, FileAccess.ReadWrite);
			return this.Open(flags);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005364 File Offset: 0x00003564
		public UnixStream Open(FileMode mode, FileAccess access)
		{
			OpenFlags flags = NativeConvert.ToOpenFlags(mode, access);
			return this.Open(flags);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005380 File Offset: 0x00003580
		[CLSCompliant(false)]
		public UnixStream Open(FileMode mode, FileAccess access, FilePermissions perms)
		{
			OpenFlags flags = NativeConvert.ToOpenFlags(mode, access);
			int num = Syscall.open(base.FullPath, flags, perms);
			if (num < 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return new UnixStream(num);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000053B8 File Offset: 0x000035B8
		public UnixStream OpenRead()
		{
			return this.Open(FileMode.Open, FileAccess.Read);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000053C4 File Offset: 0x000035C4
		public UnixStream OpenWrite()
		{
			return this.Open(FileMode.OpenOrCreate, FileAccess.Write);
		}
	}
}
