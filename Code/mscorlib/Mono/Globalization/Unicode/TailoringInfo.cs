using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200007A RID: 122
	internal class TailoringInfo
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x00017FB4 File Offset: 0x000161B4
		public TailoringInfo(int lcid, int tailoringIndex, int tailoringCount, bool frenchSort)
		{
			this.LCID = lcid;
			this.TailoringIndex = tailoringIndex;
			this.TailoringCount = tailoringCount;
			this.FrenchSort = frenchSort;
		}

		// Token: 0x0400011E RID: 286
		public readonly int LCID;

		// Token: 0x0400011F RID: 287
		public readonly int TailoringIndex;

		// Token: 0x04000120 RID: 288
		public readonly int TailoringCount;

		// Token: 0x04000121 RID: 289
		public readonly bool FrenchSort;
	}
}
