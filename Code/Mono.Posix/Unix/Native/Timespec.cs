using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000050 RID: 80
	[Map("struct timespec")]
	public struct Timespec : IEquatable<Timespec>
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0000B254 File Offset: 0x00009454
		public override int GetHashCode()
		{
			return this.tv_sec.GetHashCode() ^ this.tv_nsec.GetHashCode();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B270 File Offset: 0x00009470
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Timespec timespec = (Timespec)obj;
			return timespec.tv_sec == this.tv_sec && timespec.tv_nsec == this.tv_nsec;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B2CC File Offset: 0x000094CC
		public bool Equals(Timespec value)
		{
			return value.tv_sec == this.tv_sec && value.tv_nsec == this.tv_nsec;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B300 File Offset: 0x00009500
		public static bool operator ==(Timespec lhs, Timespec rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B30C File Offset: 0x0000950C
		public static bool operator !=(Timespec lhs, Timespec rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000360 RID: 864
		[time_t]
		public long tv_sec;

		// Token: 0x04000361 RID: 865
		public long tv_nsec;
	}
}
