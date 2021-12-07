using System;

namespace UnityEngine
{
	// Token: 0x020001D7 RID: 471
	public struct TextGenerationSettings
	{
		// Token: 0x06001CAF RID: 7343 RVA: 0x0001AD60 File Offset: 0x00018F60
		private bool CompareColors(Color left, Color right)
		{
			return Mathf.Approximately(left.r, right.r) && Mathf.Approximately(left.g, right.g) && Mathf.Approximately(left.b, right.b) && Mathf.Approximately(left.a, right.a);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0001ADCC File Offset: 0x00018FCC
		private bool CompareVector2(Vector2 left, Vector2 right)
		{
			return Mathf.Approximately(left.x, right.x) && Mathf.Approximately(left.y, right.y);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0001AE08 File Offset: 0x00019008
		public bool Equals(TextGenerationSettings other)
		{
			return this.CompareColors(this.color, other.color) && this.fontSize == other.fontSize && Mathf.Approximately(this.scaleFactor, other.scaleFactor) && this.resizeTextMinSize == other.resizeTextMinSize && this.resizeTextMaxSize == other.resizeTextMaxSize && Mathf.Approximately(this.lineSpacing, other.lineSpacing) && this.fontStyle == other.fontStyle && this.richText == other.richText && this.textAnchor == other.textAnchor && this.alignByGeometry == other.alignByGeometry && this.resizeTextForBestFit == other.resizeTextForBestFit && this.resizeTextMinSize == other.resizeTextMinSize && this.resizeTextMaxSize == other.resizeTextMaxSize && this.resizeTextForBestFit == other.resizeTextForBestFit && this.updateBounds == other.updateBounds && this.horizontalOverflow == other.horizontalOverflow && this.verticalOverflow == other.verticalOverflow && this.CompareVector2(this.generationExtents, other.generationExtents) && this.CompareVector2(this.pivot, other.pivot) && this.font == other.font;
		}

		// Token: 0x040005BD RID: 1469
		public Font font;

		// Token: 0x040005BE RID: 1470
		public Color color;

		// Token: 0x040005BF RID: 1471
		public int fontSize;

		// Token: 0x040005C0 RID: 1472
		public float lineSpacing;

		// Token: 0x040005C1 RID: 1473
		public bool richText;

		// Token: 0x040005C2 RID: 1474
		public float scaleFactor;

		// Token: 0x040005C3 RID: 1475
		public FontStyle fontStyle;

		// Token: 0x040005C4 RID: 1476
		public TextAnchor textAnchor;

		// Token: 0x040005C5 RID: 1477
		public bool alignByGeometry;

		// Token: 0x040005C6 RID: 1478
		public bool resizeTextForBestFit;

		// Token: 0x040005C7 RID: 1479
		public int resizeTextMinSize;

		// Token: 0x040005C8 RID: 1480
		public int resizeTextMaxSize;

		// Token: 0x040005C9 RID: 1481
		public bool updateBounds;

		// Token: 0x040005CA RID: 1482
		public VerticalWrapMode verticalOverflow;

		// Token: 0x040005CB RID: 1483
		public HorizontalWrapMode horizontalOverflow;

		// Token: 0x040005CC RID: 1484
		public Vector2 generationExtents;

		// Token: 0x040005CD RID: 1485
		public Vector2 pivot;

		// Token: 0x040005CE RID: 1486
		public bool generateOutOfBounds;
	}
}
