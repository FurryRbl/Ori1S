using System;

namespace LibTessDotNet
{
	// Token: 0x02000005 RID: 5
	public struct Vec3
	{
		// Token: 0x17000001 RID: 1
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.X;
				}
				if (index == 1)
				{
					return this.Y;
				}
				if (index == 2)
				{
					return this.Z;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index == 0)
				{
					this.X = value;
					return;
				}
				if (index == 1)
				{
					this.Y = value;
					return;
				}
				if (index == 2)
				{
					this.Z = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EA6 File Offset: 0x000010A6
		public static void Sub(ref Vec3 lhs, ref Vec3 rhs, out Vec3 result)
		{
			result.X = lhs.X - rhs.X;
			result.Y = lhs.Y - rhs.Y;
			result.Z = lhs.Z - rhs.Z;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002EE1 File Offset: 0x000010E1
		public static void Neg(ref Vec3 v)
		{
			v.X = -v.X;
			v.Y = -v.Y;
			v.Z = -v.Z;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002F0A File Offset: 0x0000110A
		public static void Dot(ref Vec3 u, ref Vec3 v, out float dot)
		{
			dot = u.X * v.X + u.Y * v.Y + u.Z * v.Z;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F38 File Offset: 0x00001138
		public static void Normalize(ref Vec3 v)
		{
			float num = v.X * v.X + v.Y * v.Y + v.Z * v.Z;
			num = 1f / (float)Math.Sqrt((double)num);
			v.X *= num;
			v.Y *= num;
			v.Z *= num;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002FA0 File Offset: 0x000011A0
		public static int LongAxis(ref Vec3 v)
		{
			int num = 0;
			if (Math.Abs(v.Y) > Math.Abs(v.X))
			{
				num = 1;
			}
			if (Math.Abs(v.Z) > Math.Abs((num == 0) ? v.X : v.Y))
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002FEF File Offset: 0x000011EF
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.X, this.Y, this.Z);
		}

		// Token: 0x04000007 RID: 7
		public static readonly Vec3 Zero;

		// Token: 0x04000008 RID: 8
		public float X;

		// Token: 0x04000009 RID: 9
		public float Y;

		// Token: 0x0400000A RID: 10
		public float Z;
	}
}
