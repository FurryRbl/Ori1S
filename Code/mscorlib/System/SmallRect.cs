using System;

namespace System
{
	// Token: 0x0200019D RID: 413
	internal struct SmallRect
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x00053288 File Offset: 0x00051488
		public SmallRect(int left, int top, int right, int bottom)
		{
			this.Left = (short)left;
			this.Top = (short)top;
			this.Right = (short)right;
			this.Bottom = (short)bottom;
		}

		// Token: 0x04000837 RID: 2103
		public short Left;

		// Token: 0x04000838 RID: 2104
		public short Top;

		// Token: 0x04000839 RID: 2105
		public short Right;

		// Token: 0x0400083A RID: 2106
		public short Bottom;
	}
}
