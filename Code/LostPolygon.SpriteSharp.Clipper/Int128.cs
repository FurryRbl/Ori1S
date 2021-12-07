using System;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000005 RID: 5
	internal struct Int128
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000227C File Offset: 0x0000047C
		public Int128(long _lo)
		{
			this.lo = (ulong)_lo;
			if (_lo < 0L)
			{
				this.hi = -1L;
				return;
			}
			this.hi = 0L;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000229B File Offset: 0x0000049B
		public Int128(long _hi, ulong _lo)
		{
			this.lo = _lo;
			this.hi = _hi;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022AB File Offset: 0x000004AB
		public Int128(Int128 val)
		{
			this.hi = val.hi;
			this.lo = val.lo;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022C5 File Offset: 0x000004C5
		public bool IsNegative()
		{
			return this.hi < 0L;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D4 File Offset: 0x000004D4
		public static bool operator ==(Int128 val1, Int128 val2)
		{
			return val1 == val2 || (val1 != null && val2 != null && val1.hi == val2.hi && val1.lo == val2.lo);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002321 File Offset: 0x00000521
		public static bool operator !=(Int128 val1, Int128 val2)
		{
			return !(val1 == val2);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002330 File Offset: 0x00000530
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Int128))
			{
				return false;
			}
			Int128 @int = (Int128)obj;
			return @int.hi == this.hi && @int.lo == this.lo;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000236F File Offset: 0x0000056F
		public override int GetHashCode()
		{
			return this.hi.GetHashCode() ^ this.lo.GetHashCode();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002388 File Offset: 0x00000588
		public static bool operator >(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi > val2.hi;
			}
			return val1.lo > val2.lo;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023B5 File Offset: 0x000005B5
		public static bool operator <(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi < val2.hi;
			}
			return val1.lo < val2.lo;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023E2 File Offset: 0x000005E2
		public static Int128 operator +(Int128 lhs, Int128 rhs)
		{
			lhs.hi += rhs.hi;
			lhs.lo += rhs.lo;
			if (lhs.lo < rhs.lo)
			{
				lhs.hi += 1L;
			}
			return lhs;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002422 File Offset: 0x00000622
		public static Int128 operator -(Int128 lhs, Int128 rhs)
		{
			return lhs + -rhs;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002430 File Offset: 0x00000630
		public static Int128 operator -(Int128 val)
		{
			if (val.lo == 0UL)
			{
				return new Int128(-val.hi, 0UL);
			}
			return new Int128(~val.hi, ~val.lo + 1UL);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002460 File Offset: 0x00000660
		public static Int128 Int128Mul(long lhs, long rhs)
		{
			bool flag = lhs < 0L != rhs < 0L;
			if (lhs < 0L)
			{
				lhs = -lhs;
			}
			if (rhs < 0L)
			{
				rhs = -rhs;
			}
			ulong num = (ulong)lhs >> 32;
			ulong num2 = (ulong)(lhs & (long)((ulong)-1));
			ulong num3 = (ulong)rhs >> 32;
			ulong num4 = (ulong)(rhs & (long)((ulong)-1));
			ulong num5 = num * num3;
			ulong num6 = num2 * num4;
			ulong num7 = num * num4 + num2 * num3;
			long num8 = (long)(num5 + (num7 >> 32));
			ulong num9 = (num7 << 32) + num6;
			if (num9 < num6)
			{
				num8 += 1L;
			}
			Int128 @int = new Int128(num8, num9);
			if (!flag)
			{
				return @int;
			}
			return -@int;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024EC File Offset: 0x000006EC
		public static Int128 operator /(Int128 lhs, Int128 rhs)
		{
			if (rhs.lo == 0UL && rhs.hi == 0L)
			{
				throw new ClipperException("Int128: divide by zero");
			}
			bool flag = rhs.hi < 0L != lhs.hi < 0L;
			if (lhs.hi < 0L)
			{
				lhs = -lhs;
			}
			if (rhs.hi < 0L)
			{
				rhs = -rhs;
			}
			if (rhs < lhs)
			{
				Int128 @int = new Int128(0L);
				Int128 int2 = new Int128(1L);
				while (rhs.hi >= 0L && !(rhs > lhs))
				{
					rhs.hi <<= 1;
					if (rhs.lo < 0UL)
					{
						rhs.hi += 1L;
					}
					rhs.lo <<= 1;
					int2.hi <<= 1;
					if (int2.lo < 0UL)
					{
						int2.hi += 1L;
					}
					int2.lo <<= 1;
				}
				rhs.lo >>= 1;
				if ((rhs.hi & 1L) == 1L)
				{
					rhs.lo |= 9223372036854775808UL;
				}
				rhs.hi = (long)((ulong)rhs.hi >> 1);
				int2.lo >>= 1;
				if ((int2.hi & 1L) == 1L)
				{
					int2.lo |= 9223372036854775808UL;
				}
				int2.hi >>= 1;
				while (int2.hi != 0L || int2.lo != 0UL)
				{
					if (!(lhs < rhs))
					{
						lhs -= rhs;
						@int.hi |= int2.hi;
						@int.lo |= int2.lo;
					}
					rhs.lo >>= 1;
					if ((rhs.hi & 1L) == 1L)
					{
						rhs.lo |= 9223372036854775808UL;
					}
					rhs.hi >>= 1;
					int2.lo >>= 1;
					if ((int2.hi & 1L) == 1L)
					{
						int2.lo |= 9223372036854775808UL;
					}
					int2.hi >>= 1;
				}
				if (!flag)
				{
					return @int;
				}
				return -@int;
			}
			else
			{
				if (rhs == lhs)
				{
					return new Int128(flag ? -1L : 1L);
				}
				return new Int128(0L);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002744 File Offset: 0x00000944
		public double ToDouble()
		{
			if (this.hi >= 0L)
			{
				return this.lo + (double)this.hi * 1.8446744073709552E+19;
			}
			ulong num = ~this.lo + 1UL;
			if (num == 0UL)
			{
				return (double)this.hi * 1.8446744073709552E+19;
			}
			return -(num + (double)(~(double)this.hi) * 1.8446744073709552E+19);
		}

		// Token: 0x0400000B RID: 11
		private long hi;

		// Token: 0x0400000C RID: 12
		private ulong lo;
	}
}
