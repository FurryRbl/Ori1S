using System;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000007 RID: 7
	public struct IntRect
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002883 File Offset: 0x00000A83
		public IntRect(int l, int t, int r, int b)
		{
			this.left = l;
			this.top = t;
			this.right = r;
			this.bottom = b;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028A2 File Offset: 0x00000AA2
		public IntRect(IntRect ir)
		{
			this.left = ir.left;
			this.top = ir.top;
			this.right = ir.right;
			this.bottom = ir.bottom;
		}

		// Token: 0x0400000F RID: 15
		public int left;

		// Token: 0x04000010 RID: 16
		public int top;

		// Token: 0x04000011 RID: 17
		public int right;

		// Token: 0x04000012 RID: 18
		public int bottom;
	}
}
