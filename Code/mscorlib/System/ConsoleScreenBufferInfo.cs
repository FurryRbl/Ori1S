using System;

namespace System
{
	// Token: 0x0200019E RID: 414
	internal struct ConsoleScreenBufferInfo
	{
		// Token: 0x0400083B RID: 2107
		public Coord Size;

		// Token: 0x0400083C RID: 2108
		public Coord CursorPosition;

		// Token: 0x0400083D RID: 2109
		public short Attribute;

		// Token: 0x0400083E RID: 2110
		public SmallRect Window;

		// Token: 0x0400083F RID: 2111
		public Coord MaxWindowSize;
	}
}
