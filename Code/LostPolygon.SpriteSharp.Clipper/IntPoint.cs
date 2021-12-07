using System;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000006 RID: 6
	public struct IntPoint
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000027AF File Offset: 0x000009AF
		public IntPoint(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027BF File Offset: 0x000009BF
		public IntPoint(float x, float y)
		{
			this.X = (int)x;
			this.Y = (int)y;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027D1 File Offset: 0x000009D1
		public IntPoint(IntPoint pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027EB File Offset: 0x000009EB
		public static bool operator ==(IntPoint a, IntPoint b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000280B File Offset: 0x00000A0B
		public static bool operator !=(IntPoint a, IntPoint b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002830 File Offset: 0x00000A30
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is IntPoint)
			{
				IntPoint intPoint = (IntPoint)obj;
				return this.X == intPoint.X && this.Y == intPoint.Y;
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002871 File Offset: 0x00000A71
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400000D RID: 13
		public int X;

		// Token: 0x0400000E RID: 14
		public int Y;
	}
}
