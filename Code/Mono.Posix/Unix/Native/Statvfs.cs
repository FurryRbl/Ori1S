using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004C RID: 76
	[Map]
	[CLSCompliant(false)]
	public struct Statvfs : IEquatable<Statvfs>
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public override int GetHashCode()
		{
			return this.f_bsize.GetHashCode() ^ this.f_frsize.GetHashCode() ^ this.f_blocks.GetHashCode() ^ this.f_bfree.GetHashCode() ^ this.f_bavail.GetHashCode() ^ this.f_files.GetHashCode() ^ this.f_ffree.GetHashCode() ^ this.f_favail.GetHashCode() ^ this.f_fsid.GetHashCode() ^ this.f_flag.GetHashCode() ^ this.f_namemax.GetHashCode();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000AE4C File Offset: 0x0000904C
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Statvfs statvfs = (Statvfs)obj;
			return statvfs.f_bsize == this.f_bsize && statvfs.f_frsize == this.f_frsize && statvfs.f_blocks == this.f_blocks && statvfs.f_bfree == this.f_bfree && statvfs.f_bavail == this.f_bavail && statvfs.f_files == this.f_files && statvfs.f_ffree == this.f_ffree && statvfs.f_favail == this.f_favail && statvfs.f_fsid == this.f_fsid && statvfs.f_flag == this.f_flag && statvfs.f_namemax == this.f_namemax;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000AF4C File Offset: 0x0000914C
		public bool Equals(Statvfs value)
		{
			return value.f_bsize == this.f_bsize && value.f_frsize == this.f_frsize && value.f_blocks == this.f_blocks && value.f_bfree == this.f_bfree && value.f_bavail == this.f_bavail && value.f_files == this.f_files && value.f_ffree == this.f_ffree && value.f_favail == this.f_favail && value.f_fsid == this.f_fsid && value.f_flag == this.f_flag && value.f_namemax == this.f_namemax;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000B020 File Offset: 0x00009220
		public static bool operator ==(Statvfs lhs, Statvfs rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000B02C File Offset: 0x0000922C
		public static bool operator !=(Statvfs lhs, Statvfs rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400034F RID: 847
		public ulong f_bsize;

		// Token: 0x04000350 RID: 848
		public ulong f_frsize;

		// Token: 0x04000351 RID: 849
		[fsblkcnt_t]
		public ulong f_blocks;

		// Token: 0x04000352 RID: 850
		[fsblkcnt_t]
		public ulong f_bfree;

		// Token: 0x04000353 RID: 851
		[fsblkcnt_t]
		public ulong f_bavail;

		// Token: 0x04000354 RID: 852
		[fsfilcnt_t]
		public ulong f_files;

		// Token: 0x04000355 RID: 853
		[fsfilcnt_t]
		public ulong f_ffree;

		// Token: 0x04000356 RID: 854
		[fsfilcnt_t]
		public ulong f_favail;

		// Token: 0x04000357 RID: 855
		public ulong f_fsid;

		// Token: 0x04000358 RID: 856
		public MountFlags f_flag;

		// Token: 0x04000359 RID: 857
		public ulong f_namemax;
	}
}
