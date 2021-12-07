using System;

namespace Mono.Posix
{
	// Token: 0x02000075 RID: 117
	[Obsolete("Use Mono.Unix.Native.Stat")]
	public struct Stat
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0000D71C File Offset: 0x0000B91C
		internal Stat(int device, int inode, int mode, int nlinks, int uid, int gid, int rdev, long size, long blksize, long blocks, long atime, long mtime, long ctime)
		{
			this.Device = device;
			this.INode = inode;
			this.Mode = (StatMode)mode;
			this.NLinks = nlinks;
			this.Uid = uid;
			this.Gid = gid;
			this.DeviceType = (long)rdev;
			this.Size = size;
			this.BlockSize = blksize;
			this.Blocks = blocks;
			if (atime != 0L)
			{
				this.ATime = Stat.UnixToDateTime(atime);
			}
			else
			{
				this.ATime = default(DateTime);
			}
			if (mtime != 0L)
			{
				this.MTime = Stat.UnixToDateTime(mtime);
			}
			else
			{
				this.MTime = default(DateTime);
			}
			if (ctime != 0L)
			{
				this.CTime = Stat.UnixToDateTime(ctime);
			}
			else
			{
				this.CTime = default(DateTime);
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000D804 File Offset: 0x0000BA04
		[Obsolete("Use Mono.Unix.Native.NativeConvert.ToDateTime")]
		public static DateTime UnixToDateTime(long unix)
		{
			return Stat.UnixEpoch.Add(TimeSpan.FromSeconds((double)unix)).ToLocalTime();
		}

		// Token: 0x0400040C RID: 1036
		[Obsolete("Use Mono.Unix.Native.Stat.st_dev")]
		public readonly int Device;

		// Token: 0x0400040D RID: 1037
		[Obsolete("Use Mono.Unix.Native.Stat.st_ino")]
		public readonly int INode;

		// Token: 0x0400040E RID: 1038
		[Obsolete("Use Mono.Unix.Native.Stat.st_mode")]
		public readonly StatMode Mode;

		// Token: 0x0400040F RID: 1039
		[Obsolete("Use Mono.Unix.Native.Stat.st_nlink")]
		public readonly int NLinks;

		// Token: 0x04000410 RID: 1040
		[Obsolete("Use Mono.Unix.Native.Stat.st_uid")]
		public readonly int Uid;

		// Token: 0x04000411 RID: 1041
		[Obsolete("Use Mono.Unix.Native.Stat.st_gid")]
		public readonly int Gid;

		// Token: 0x04000412 RID: 1042
		[Obsolete("Use Mono.Unix.Native.Stat.st_rdev")]
		public readonly long DeviceType;

		// Token: 0x04000413 RID: 1043
		[Obsolete("Use Mono.Unix.Native.Stat.st_size")]
		public readonly long Size;

		// Token: 0x04000414 RID: 1044
		[Obsolete("Use Mono.Unix.Native.Stat.st_blksize")]
		public readonly long BlockSize;

		// Token: 0x04000415 RID: 1045
		[Obsolete("Use Mono.Unix.Native.Stat.st_blocks")]
		public readonly long Blocks;

		// Token: 0x04000416 RID: 1046
		[Obsolete("Use Mono.Unix.Native.Stat.st_atime")]
		public readonly DateTime ATime;

		// Token: 0x04000417 RID: 1047
		[Obsolete("Use Mono.Unix.Native.Stat.st_mtime")]
		public readonly DateTime MTime;

		// Token: 0x04000418 RID: 1048
		[Obsolete("Use Mono.Unix.Native.Stat.st_ctime")]
		public readonly DateTime CTime;

		// Token: 0x04000419 RID: 1049
		[Obsolete("Use Mono.Unix.Native.NativeConvert.LocalUnixEpoch")]
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
	}
}
