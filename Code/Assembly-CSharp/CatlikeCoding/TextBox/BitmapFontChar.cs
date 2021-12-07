using System;

namespace CatlikeCoding.TextBox
{
	// Token: 0x0200068A RID: 1674
	[Serializable]
	public class BitmapFontChar
	{
		// Token: 0x0600289B RID: 10395 RVA: 0x000B07F8 File Offset: 0x000AE9F8
		public float GetKerning(char nextChar)
		{
			if (this.kerningIds.Length == 0)
			{
				return 0f;
			}
			int num = 0;
			int i = this.kerningIds.Length - 1;
			while (i >= num)
			{
				int num2 = num + i >> 1;
				int num3 = this.kerningIds[num2];
				if (num3 > (int)nextChar)
				{
					i = num2 - 1;
				}
				else
				{
					if (num3 >= (int)nextChar)
					{
						return this.kernings[num2];
					}
					num = num2 + 1;
				}
			}
			return 0f;
		}

		// Token: 0x04002428 RID: 9256
		public int id;

		// Token: 0x04002429 RID: 9257
		public float uMin;

		// Token: 0x0400242A RID: 9258
		public float uMax;

		// Token: 0x0400242B RID: 9259
		public float vMin;

		// Token: 0x0400242C RID: 9260
		public float vMax;

		// Token: 0x0400242D RID: 9261
		public float xOffset;

		// Token: 0x0400242E RID: 9262
		public float yOffset;

		// Token: 0x0400242F RID: 9263
		public float width;

		// Token: 0x04002430 RID: 9264
		public float height;

		// Token: 0x04002431 RID: 9265
		public float advance;

		// Token: 0x04002432 RID: 9266
		public int[] kerningIds;

		// Token: 0x04002433 RID: 9267
		public float[] kernings;
	}
}
