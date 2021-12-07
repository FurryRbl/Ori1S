using System;
using System.Collections;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000010 RID: 16
	public sealed class UnixDriveInfo
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003804 File Offset: 0x00001A04
		public UnixDriveInfo(string mountPoint)
		{
			if (mountPoint == null)
			{
				throw new ArgumentNullException("mountPoint");
			}
			Fstab fstab = Syscall.getfsfile(mountPoint);
			if (fstab != null)
			{
				this.FromFstab(fstab);
			}
			else
			{
				this.mount_point = mountPoint;
				this.block_device = string.Empty;
				this.fstype = "Unknown";
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003864 File Offset: 0x00001A64
		private UnixDriveInfo(Fstab fstab)
		{
			this.FromFstab(fstab);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003874 File Offset: 0x00001A74
		private void FromFstab(Fstab fstab)
		{
			this.fstype = fstab.fs_vfstype;
			this.mount_point = fstab.fs_file;
			this.block_device = fstab.fs_spec;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000038A8 File Offset: 0x00001AA8
		public static UnixDriveInfo GetForSpecialFile(string specialFile)
		{
			if (specialFile == null)
			{
				throw new ArgumentNullException("specialFile");
			}
			Fstab fstab = Syscall.getfsspec(specialFile);
			if (fstab == null)
			{
				throw new ArgumentException("specialFile isn't valid: " + specialFile);
			}
			return new UnixDriveInfo(fstab);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000038F0 File Offset: 0x00001AF0
		public long AvailableFreeSpace
		{
			get
			{
				this.Refresh();
				return Convert.ToInt64(this.stat.f_bavail * this.stat.f_frsize);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003920 File Offset: 0x00001B20
		public string DriveFormat
		{
			get
			{
				return this.fstype;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003928 File Offset: 0x00001B28
		public UnixDriveType DriveType
		{
			get
			{
				return UnixDriveType.Unknown;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000392C File Offset: 0x00001B2C
		public bool IsReady
		{
			get
			{
				bool flag = this.Refresh(false);
				if (this.mount_point == "/" || !flag)
				{
					return flag;
				}
				Statvfs statvfs;
				int num = Syscall.statvfs(this.RootDirectory.Parent.FullName, out statvfs);
				return num == 0 && statvfs.f_fsid != this.stat.f_fsid;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003998 File Offset: 0x00001B98
		public string Name
		{
			get
			{
				return this.mount_point;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000039A0 File Offset: 0x00001BA0
		public UnixDirectoryInfo RootDirectory
		{
			get
			{
				return new UnixDirectoryInfo(this.mount_point);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000039B0 File Offset: 0x00001BB0
		public long TotalFreeSpace
		{
			get
			{
				this.Refresh();
				return (long)(this.stat.f_bfree * this.stat.f_frsize);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000039D0 File Offset: 0x00001BD0
		public long TotalSize
		{
			get
			{
				this.Refresh();
				return (long)(this.stat.f_frsize * this.stat.f_blocks);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000039F0 File Offset: 0x00001BF0
		public string VolumeLabel
		{
			get
			{
				return this.block_device;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000039F8 File Offset: 0x00001BF8
		public long MaximumFilenameLength
		{
			get
			{
				this.Refresh();
				return Convert.ToInt64(this.stat.f_namemax);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003A10 File Offset: 0x00001C10
		public static UnixDriveInfo[] GetDrives()
		{
			ArrayList arrayList = new ArrayList();
			object fstab_lock = Syscall.fstab_lock;
			lock (fstab_lock)
			{
				int num = Syscall.setfsent();
				if (num != 1)
				{
					throw new IOException("Error calling setfsent(3)", new UnixIOException());
				}
				try
				{
					Fstab fstab;
					while ((fstab = Syscall.getfsent()) != null)
					{
						if (fstab.fs_file.StartsWith("/"))
						{
							arrayList.Add(new UnixDriveInfo(fstab));
						}
					}
				}
				finally
				{
					Syscall.endfsent();
				}
			}
			return (UnixDriveInfo[])arrayList.ToArray(typeof(UnixDriveInfo));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public override string ToString()
		{
			return this.VolumeLabel;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003AF0 File Offset: 0x00001CF0
		private void Refresh()
		{
			this.Refresh(true);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003AFC File Offset: 0x00001CFC
		private bool Refresh(bool throwException)
		{
			int num = Syscall.statvfs(this.mount_point, out this.stat);
			if (num == -1 && throwException)
			{
				Errno lastError = Stdlib.GetLastError();
				throw new InvalidOperationException(UnixMarshal.GetErrorDescription(lastError), new UnixIOException(lastError));
			}
			return num != -1;
		}

		// Token: 0x04000057 RID: 87
		private Statvfs stat;

		// Token: 0x04000058 RID: 88
		private string fstype;

		// Token: 0x04000059 RID: 89
		private string mount_point;

		// Token: 0x0400005A RID: 90
		private string block_device;
	}
}
