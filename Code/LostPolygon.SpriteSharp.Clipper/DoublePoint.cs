using System;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000002 RID: 2
	public struct DoublePoint
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public DoublePoint(float x = 0f, float y = 0f)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		public DoublePoint(DoublePoint dp)
		{
			this.X = dp.X;
			this.Y = dp.Y;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000207A File Offset: 0x0000027A
		public DoublePoint(IntPoint ip)
		{
			this.X = (float)ip.X;
			this.Y = (float)ip.Y;
		}

		// Token: 0x04000001 RID: 1
		public float X;

		// Token: 0x04000002 RID: 2
		public float Y;
	}
}
