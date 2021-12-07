using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002DC RID: 732
	internal struct ILExceptionBlock
	{
		// Token: 0x06002574 RID: 9588 RVA: 0x00083AC4 File Offset: 0x00081CC4
		internal void Debug()
		{
		}

		// Token: 0x04000E05 RID: 3589
		public const int CATCH = 0;

		// Token: 0x04000E06 RID: 3590
		public const int FILTER = 1;

		// Token: 0x04000E07 RID: 3591
		public const int FINALLY = 2;

		// Token: 0x04000E08 RID: 3592
		public const int FAULT = 4;

		// Token: 0x04000E09 RID: 3593
		public const int FILTER_START = -1;

		// Token: 0x04000E0A RID: 3594
		internal Type extype;

		// Token: 0x04000E0B RID: 3595
		internal int type;

		// Token: 0x04000E0C RID: 3596
		internal int start;

		// Token: 0x04000E0D RID: 3597
		internal int len;

		// Token: 0x04000E0E RID: 3598
		internal int filter_offset;
	}
}
