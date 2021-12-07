using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004B RID: 75
	[Map("struct stat")]
	public struct Stat : IEquatable<Stat>
	{
		// Token: 0x0600037A RID: 890 RVA: 0x0000AACC File Offset: 0x00008CCC
		public override int GetHashCode()
		{
			return this.st_dev.GetHashCode() ^ this.st_ino.GetHashCode() ^ this.st_mode.GetHashCode() ^ this.st_nlink.GetHashCode() ^ this.st_uid.GetHashCode() ^ this.st_gid.GetHashCode() ^ this.st_rdev.GetHashCode() ^ this.st_size.GetHashCode() ^ this.st_blksize.GetHashCode() ^ this.st_blocks.GetHashCode() ^ this.st_atime.GetHashCode() ^ this.st_mtime.GetHashCode() ^ this.st_ctime.GetHashCode();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Stat stat = (Stat)obj;
			return stat.st_dev == this.st_dev && stat.st_ino == this.st_ino && stat.st_mode == this.st_mode && stat.st_nlink == this.st_nlink && stat.st_uid == this.st_uid && stat.st_gid == this.st_gid && stat.st_rdev == this.st_rdev && stat.st_size == this.st_size && stat.st_blksize == this.st_blksize && stat.st_blocks == this.st_blocks && stat.st_atime == this.st_atime && stat.st_mtime == this.st_mtime && stat.st_ctime == this.st_ctime;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		public bool Equals(Stat value)
		{
			return value.st_dev == this.st_dev && value.st_ino == this.st_ino && value.st_mode == this.st_mode && value.st_nlink == this.st_nlink && value.st_uid == this.st_uid && value.st_gid == this.st_gid && value.st_rdev == this.st_rdev && value.st_size == this.st_size && value.st_blksize == this.st_blksize && value.st_blocks == this.st_blocks && value.st_atime == this.st_atime && value.st_mtime == this.st_mtime && value.st_ctime == this.st_ctime;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000AD98 File Offset: 0x00008F98
		public static bool operator ==(Stat lhs, Stat rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		public static bool operator !=(Stat lhs, Stat rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000341 RID: 833
		[dev_t]
		[CLSCompliant(false)]
		public ulong st_dev;

		// Token: 0x04000342 RID: 834
		[ino_t]
		[CLSCompliant(false)]
		public ulong st_ino;

		// Token: 0x04000343 RID: 835
		[CLSCompliant(false)]
		public FilePermissions st_mode;

		// Token: 0x04000344 RID: 836
		[NonSerialized]
		private uint _padding_;

		// Token: 0x04000345 RID: 837
		[CLSCompliant(false)]
		[nlink_t]
		public ulong st_nlink;

		// Token: 0x04000346 RID: 838
		[CLSCompliant(false)]
		[uid_t]
		public uint st_uid;

		// Token: 0x04000347 RID: 839
		[gid_t]
		[CLSCompliant(false)]
		public uint st_gid;

		// Token: 0x04000348 RID: 840
		[dev_t]
		[CLSCompliant(false)]
		public ulong st_rdev;

		// Token: 0x04000349 RID: 841
		[off_t]
		public long st_size;

		// Token: 0x0400034A RID: 842
		[blksize_t]
		public long st_blksize;

		// Token: 0x0400034B RID: 843
		[blkcnt_t]
		public long st_blocks;

		// Token: 0x0400034C RID: 844
		[time_t]
		public long st_atime;

		// Token: 0x0400034D RID: 845
		[time_t]
		public long st_mtime;

		// Token: 0x0400034E RID: 846
		[time_t]
		public long st_ctime;
	}
}
