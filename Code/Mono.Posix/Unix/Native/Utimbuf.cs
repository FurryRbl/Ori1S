using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004F RID: 79
	[Map("struct utimbuf")]
	public struct Utimbuf : IEquatable<Utimbuf>
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000B18C File Offset: 0x0000938C
		public override int GetHashCode()
		{
			return this.actime.GetHashCode() ^ this.modtime.GetHashCode();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B1A8 File Offset: 0x000093A8
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Utimbuf utimbuf = (Utimbuf)obj;
			return utimbuf.actime == this.actime && utimbuf.modtime == this.modtime;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B204 File Offset: 0x00009404
		public bool Equals(Utimbuf value)
		{
			return value.actime == this.actime && value.modtime == this.modtime;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B238 File Offset: 0x00009438
		public static bool operator ==(Utimbuf lhs, Utimbuf rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B244 File Offset: 0x00009444
		public static bool operator !=(Utimbuf lhs, Utimbuf rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400035E RID: 862
		[time_t]
		public long actime;

		// Token: 0x0400035F RID: 863
		[time_t]
		public long modtime;
	}
}
