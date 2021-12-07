using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000049 RID: 73
	[Map("struct flock")]
	public struct Flock : IEquatable<Flock>
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000A890 File Offset: 0x00008A90
		public override int GetHashCode()
		{
			return this.l_type.GetHashCode() ^ this.l_whence.GetHashCode() ^ this.l_start.GetHashCode() ^ this.l_len.GetHashCode() ^ this.l_pid.GetHashCode();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Flock flock = (Flock)obj;
			return this.l_type == flock.l_type && this.l_whence == flock.l_whence && this.l_start == flock.l_start && this.l_len == flock.l_len && this.l_pid == flock.l_pid;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A978 File Offset: 0x00008B78
		public bool Equals(Flock value)
		{
			return this.l_type == value.l_type && this.l_whence == value.l_whence && this.l_start == value.l_start && this.l_len == value.l_len && this.l_pid == value.l_pid;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public static bool operator ==(Flock lhs, Flock rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A9EC File Offset: 0x00008BEC
		public static bool operator !=(Flock lhs, Flock rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000339 RID: 825
		[CLSCompliant(false)]
		public LockType l_type;

		// Token: 0x0400033A RID: 826
		[CLSCompliant(false)]
		public SeekFlags l_whence;

		// Token: 0x0400033B RID: 827
		[off_t]
		public long l_start;

		// Token: 0x0400033C RID: 828
		[off_t]
		public long l_len;

		// Token: 0x0400033D RID: 829
		[pid_t]
		public int l_pid;
	}
}
