using System;

namespace System.Resources
{
	// Token: 0x0200031B RID: 795
	internal class ICONDIRENTRY
	{
		// Token: 0x0600288A RID: 10378 RVA: 0x00091A70 File Offset: 0x0008FC70
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ICONDIRENTRY (",
				this.bWidth,
				"x",
				this.bHeight,
				" ",
				this.wBitCount,
				" bpp)"
			});
		}

		// Token: 0x04001075 RID: 4213
		public byte bWidth;

		// Token: 0x04001076 RID: 4214
		public byte bHeight;

		// Token: 0x04001077 RID: 4215
		public byte bColorCount;

		// Token: 0x04001078 RID: 4216
		public byte bReserved;

		// Token: 0x04001079 RID: 4217
		public short wPlanes;

		// Token: 0x0400107A RID: 4218
		public short wBitCount;

		// Token: 0x0400107B RID: 4219
		public int dwBytesInRes;

		// Token: 0x0400107C RID: 4220
		public int dwImageOffset;

		// Token: 0x0400107D RID: 4221
		public byte[] image;
	}
}
