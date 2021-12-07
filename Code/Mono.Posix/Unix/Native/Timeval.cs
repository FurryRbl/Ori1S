using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004D RID: 77
	[Map("struct timeval")]
	public struct Timeval : IEquatable<Timeval>
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000B03C File Offset: 0x0000923C
		public override int GetHashCode()
		{
			return this.tv_sec.GetHashCode() ^ this.tv_usec.GetHashCode();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000B058 File Offset: 0x00009258
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Timeval timeval = (Timeval)obj;
			return timeval.tv_sec == this.tv_sec && timeval.tv_usec == this.tv_usec;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public bool Equals(Timeval value)
		{
			return value.tv_sec == this.tv_sec && value.tv_usec == this.tv_usec;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000B0E8 File Offset: 0x000092E8
		public static bool operator ==(Timeval lhs, Timeval rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public static bool operator !=(Timeval lhs, Timeval rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400035A RID: 858
		[time_t]
		public long tv_sec;

		// Token: 0x0400035B RID: 859
		[suseconds_t]
		public long tv_usec;
	}
}
