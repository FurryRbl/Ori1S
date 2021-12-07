using System;

namespace System
{
	// Token: 0x0200019C RID: 412
	internal struct Coord
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x00053274 File Offset: 0x00051474
		public Coord(int x, int y)
		{
			this.X = (short)x;
			this.Y = (short)y;
		}

		// Token: 0x04000835 RID: 2101
		public short X;

		// Token: 0x04000836 RID: 2102
		public short Y;
	}
}
