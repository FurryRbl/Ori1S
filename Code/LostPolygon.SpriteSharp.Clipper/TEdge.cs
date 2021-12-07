using System;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x0200000F RID: 15
	internal class TEdge
	{
		// Token: 0x04000030 RID: 48
		internal IntPoint Bot;

		// Token: 0x04000031 RID: 49
		internal IntPoint Curr;

		// Token: 0x04000032 RID: 50
		internal IntPoint Top;

		// Token: 0x04000033 RID: 51
		internal IntPoint Delta;

		// Token: 0x04000034 RID: 52
		internal float Dx;

		// Token: 0x04000035 RID: 53
		internal PolyType PolyTyp;

		// Token: 0x04000036 RID: 54
		internal EdgeSide Side;

		// Token: 0x04000037 RID: 55
		internal int WindDelta;

		// Token: 0x04000038 RID: 56
		internal int WindCnt;

		// Token: 0x04000039 RID: 57
		internal int WindCnt2;

		// Token: 0x0400003A RID: 58
		internal int OutIdx;

		// Token: 0x0400003B RID: 59
		internal TEdge Next;

		// Token: 0x0400003C RID: 60
		internal TEdge Prev;

		// Token: 0x0400003D RID: 61
		internal TEdge NextInLML;

		// Token: 0x0400003E RID: 62
		internal TEdge NextInAEL;

		// Token: 0x0400003F RID: 63
		internal TEdge PrevInAEL;

		// Token: 0x04000040 RID: 64
		internal TEdge NextInSEL;

		// Token: 0x04000041 RID: 65
		internal TEdge PrevInSEL;
	}
}
