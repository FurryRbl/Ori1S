using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004A RID: 74
	[Map("struct pollfd")]
	public struct Pollfd : IEquatable<Pollfd>
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000A9FC File Offset: 0x00008BFC
		public override int GetHashCode()
		{
			return this.events.GetHashCode() ^ this.revents.GetHashCode();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000AA20 File Offset: 0x00008C20
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Pollfd pollfd = (Pollfd)obj;
			return pollfd.events == this.events && pollfd.revents == this.revents;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public bool Equals(Pollfd value)
		{
			return value.events == this.events && value.revents == this.revents;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public static bool operator ==(Pollfd lhs, Pollfd rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000AABC File Offset: 0x00008CBC
		public static bool operator !=(Pollfd lhs, Pollfd rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400033E RID: 830
		public int fd;

		// Token: 0x0400033F RID: 831
		[CLSCompliant(false)]
		public PollEvents events;

		// Token: 0x04000340 RID: 832
		[CLSCompliant(false)]
		public PollEvents revents;
	}
}
