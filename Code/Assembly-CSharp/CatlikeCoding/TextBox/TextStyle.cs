using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000771 RID: 1905
	[Serializable]
	public sealed class TextStyle
	{
		// Token: 0x04002822 RID: 10274
		public string name;

		// Token: 0x04002823 RID: 10275
		public Color32 color;

		// Token: 0x04002824 RID: 10276
		public BitmapFont font;

		// Token: 0x04002825 RID: 10277
		public TextRenderer renderer;

		// Token: 0x04002826 RID: 10278
		public float letterSpacing;

		// Token: 0x04002827 RID: 10279
		public float fontScale;

		// Token: 0x04002828 RID: 10280
		public bool absoluteFontScale;

		// Token: 0x04002829 RID: 10281
		public float lineScale;

		// Token: 0x0400282A RID: 10282
		public bool hasColor;

		// Token: 0x0400282B RID: 10283
		public bool hasLetterSpacing;

		// Token: 0x0400282C RID: 10284
		public bool hasFontScale;

		// Token: 0x0400282D RID: 10285
		public bool hasLineScale;

		// Token: 0x0400282E RID: 10286
		[NonSerialized]
		public int rendererId;
	}
}
