using System;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000014 RID: 20
	internal class OutRec
	{
		// Token: 0x0400004B RID: 75
		internal int Idx;

		// Token: 0x0400004C RID: 76
		internal bool IsHole;

		// Token: 0x0400004D RID: 77
		internal bool IsOpen;

		// Token: 0x0400004E RID: 78
		internal OutRec FirstLeft;

		// Token: 0x0400004F RID: 79
		internal OutPt Pts;

		// Token: 0x04000050 RID: 80
		internal OutPt BottomPt;

		// Token: 0x04000051 RID: 81
		internal PolyNode PolyNode;
	}
}
