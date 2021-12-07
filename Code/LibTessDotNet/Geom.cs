using System;

namespace LibTessDotNet
{
	// Token: 0x02000003 RID: 3
	internal static class Geom
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002190 File Offset: 0x00000390
		public static bool IsWindingInside(WindingRule rule, int n)
		{
			switch (rule)
			{
			case WindingRule.EvenOdd:
				return (n & 1) == 1;
			case WindingRule.NonZero:
				return n != 0;
			case WindingRule.Positive:
				return n > 0;
			case WindingRule.Negative:
				return n < 0;
			case WindingRule.AbsGeqTwo:
				return n >= 2 || n <= -2;
			default:
				throw new Exception("Wrong winding rule");
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E8 File Offset: 0x000003E8
		public static bool VertCCW(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			return u._s * (v._t - w._t) + v._s * (w._t - u._t) + w._s * (u._t - v._t) >= 0f;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000223D File Offset: 0x0000043D
		public static bool VertEq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._s == rhs._s && lhs._t == rhs._t;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000225D File Offset: 0x0000045D
		public static bool VertLeq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._s < rhs._s || (lhs._s == rhs._s && lhs._t <= rhs._t);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002290 File Offset: 0x00000490
		public static float EdgeEval(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._s - u._s;
			float num2 = w._s - v._s;
			if (num + num2 <= 0f)
			{
				return 0f;
			}
			if (num < num2)
			{
				return v._t - u._t + (u._t - w._t) * (num / (num + num2));
			}
			return v._t - w._t + (w._t - u._t) * (num2 / (num + num2));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002310 File Offset: 0x00000510
		public static float EdgeSign(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._s - u._s;
			float num2 = w._s - v._s;
			if (num + num2 > 0f)
			{
				return (v._t - w._t) * num + (v._t - u._t) * num2;
			}
			return 0f;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002368 File Offset: 0x00000568
		public static bool TransLeq(MeshUtils.Vertex lhs, MeshUtils.Vertex rhs)
		{
			return lhs._t < rhs._t || (lhs._t == rhs._t && lhs._s <= rhs._s);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000239C File Offset: 0x0000059C
		public static float TransEval(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._t - u._t;
			float num2 = w._t - v._t;
			if (num + num2 <= 0f)
			{
				return 0f;
			}
			if (num < num2)
			{
				return v._s - u._s + (u._s - w._s) * (num / (num + num2));
			}
			return v._s - w._s + (w._s - u._s) * (num2 / (num + num2));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000241C File Offset: 0x0000061C
		public static float TransSign(MeshUtils.Vertex u, MeshUtils.Vertex v, MeshUtils.Vertex w)
		{
			float num = v._t - u._t;
			float num2 = w._t - v._t;
			if (num + num2 > 0f)
			{
				return (v._s - w._s) * num + (v._s - u._s) * num2;
			}
			return 0f;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002474 File Offset: 0x00000674
		public static bool EdgeGoesLeft(MeshUtils.Edge e)
		{
			return Geom.VertLeq(e._Dst, e._Org);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002487 File Offset: 0x00000687
		public static bool EdgeGoesRight(MeshUtils.Edge e)
		{
			return Geom.VertLeq(e._Org, e._Dst);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000249A File Offset: 0x0000069A
		public static float VertL1dist(MeshUtils.Vertex u, MeshUtils.Vertex v)
		{
			return Math.Abs(u._s - v._s) + Math.Abs(u._t - v._t);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024C1 File Offset: 0x000006C1
		public static void AddWinding(MeshUtils.Edge eDst, MeshUtils.Edge eSrc)
		{
			eDst._winding += eSrc._winding;
			eDst._Sym._winding += eSrc._Sym._winding;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024F4 File Offset: 0x000006F4
		public static float Interpolate(float a, float x, float b, float y)
		{
			a = ((a < 0f) ? 0f : a);
			b = ((b < 0f) ? 0f : b);
			if (a > b)
			{
				return y + (x - y) * (b / (a + b));
			}
			if (b != 0f)
			{
				return x + (y - x) * (a / (a + b));
			}
			return (x + y) / 2f;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002554 File Offset: 0x00000754
		private static void Swap(ref MeshUtils.Vertex a, ref MeshUtils.Vertex b)
		{
			MeshUtils.Vertex vertex = a;
			a = b;
			b = vertex;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000256C File Offset: 0x0000076C
		public static void EdgeIntersect(MeshUtils.Vertex o1, MeshUtils.Vertex d1, MeshUtils.Vertex o2, MeshUtils.Vertex d2, MeshUtils.Vertex v)
		{
			if (!Geom.VertLeq(o1, d1))
			{
				Geom.Swap(ref o1, ref d1);
			}
			if (!Geom.VertLeq(o2, d2))
			{
				Geom.Swap(ref o2, ref d2);
			}
			if (!Geom.VertLeq(o1, o2))
			{
				Geom.Swap(ref o1, ref o2);
				Geom.Swap(ref d1, ref d2);
			}
			float num;
			float num2;
			if (!Geom.VertLeq(o2, d1))
			{
				v._s = (o2._s + d1._s) / 2f;
			}
			else if (Geom.VertLeq(d1, d2))
			{
				num = Geom.EdgeEval(o1, o2, d1);
				num2 = Geom.EdgeEval(o2, d1, d2);
				if (num + num2 < 0f)
				{
					num = -num;
					num2 = -num2;
				}
				v._s = Geom.Interpolate(num, o2._s, num2, d1._s);
			}
			else
			{
				num = Geom.EdgeSign(o1, o2, d1);
				num2 = -Geom.EdgeSign(o1, d2, d1);
				if (num + num2 < 0f)
				{
					num = -num;
					num2 = -num2;
				}
				v._s = Geom.Interpolate(num, o2._s, num2, d2._s);
			}
			if (!Geom.TransLeq(o1, d1))
			{
				Geom.Swap(ref o1, ref d1);
			}
			if (!Geom.TransLeq(o2, d2))
			{
				Geom.Swap(ref o2, ref d2);
			}
			if (!Geom.TransLeq(o1, o2))
			{
				Geom.Swap(ref o1, ref o2);
				Geom.Swap(ref d1, ref d2);
			}
			if (!Geom.TransLeq(o2, d1))
			{
				v._t = (o2._t + d1._t) / 2f;
				return;
			}
			if (Geom.TransLeq(d1, d2))
			{
				num = Geom.TransEval(o1, o2, d1);
				num2 = Geom.TransEval(o2, d1, d2);
				if (num + num2 < 0f)
				{
					num = -num;
					num2 = -num2;
				}
				v._t = Geom.Interpolate(num, o2._t, num2, d1._t);
				return;
			}
			num = Geom.TransSign(o1, o2, d1);
			num2 = -Geom.TransSign(o1, d2, d1);
			if (num + num2 < 0f)
			{
				num = -num;
				num2 = -num2;
			}
			v._t = Geom.Interpolate(num, o2._t, num2, d2._t);
		}
	}
}
