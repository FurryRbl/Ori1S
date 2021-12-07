using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000029 RID: 41
	public struct RealTimeSignum : IEquatable<RealTimeSignum>
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00009CA4 File Offset: 0x00007EA4
		public RealTimeSignum(int offset)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("Offset cannot be negative");
			}
			if (offset > RealTimeSignum.MaxOffset)
			{
				throw new ArgumentOutOfRangeException("Offset greater than maximum supported SIGRT");
			}
			this.rt_offset = offset;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009D14 File Offset: 0x00007F14
		public int Offset
		{
			get
			{
				return this.rt_offset;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009D1C File Offset: 0x00007F1C
		public override int GetHashCode()
		{
			return this.rt_offset.GetHashCode();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009D2C File Offset: 0x00007F2C
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && this.Equals((RealTimeSignum)obj);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009D60 File Offset: 0x00007F60
		public bool Equals(RealTimeSignum value)
		{
			return this.Offset == value.Offset;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009D74 File Offset: 0x00007F74
		public static bool operator ==(RealTimeSignum lhs, RealTimeSignum rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00009D80 File Offset: 0x00007F80
		public static bool operator !=(RealTimeSignum lhs, RealTimeSignum rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000099 RID: 153
		private int rt_offset;

		// Token: 0x0400009A RID: 154
		private static readonly int MaxOffset = UnixSignal.GetSIGRTMAX() - UnixSignal.GetSIGRTMIN() - 1;

		// Token: 0x0400009B RID: 155
		public static readonly RealTimeSignum MinValue = new RealTimeSignum(0);

		// Token: 0x0400009C RID: 156
		public static readonly RealTimeSignum MaxValue = new RealTimeSignum(RealTimeSignum.MaxOffset);
	}
}
