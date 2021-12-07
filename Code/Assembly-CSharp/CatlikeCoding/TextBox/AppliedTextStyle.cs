using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x0200076F RID: 1903
	public struct AppliedTextStyle
	{
		// Token: 0x06002C44 RID: 11332 RVA: 0x000BE628 File Offset: 0x000BC828
		public void Apply(TextStyle style, TextRenderer renderer)
		{
			this.color = style.color;
			this.font = style.font;
			this.letterSpacing = style.letterSpacing;
			this.size = style.fontScale;
			this.lineHeight = this.size * style.lineScale;
			this.lineDescent.baseline = this.font.baseline * this.lineHeight;
			this.lineDescent.baselineToBottom = this.font.baselineToBottom * this.lineHeight;
			this.renderer = renderer;
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000BE6BC File Offset: 0x000BC8BC
		public void ApplyOnTop(TextStyle style)
		{
			bool flag = false;
			if (style.hasColor)
			{
				this.color = style.color;
			}
			if (style.font != null)
			{
				this.font = style.font;
				flag = true;
			}
			if (style.hasLetterSpacing)
			{
				this.letterSpacing = style.letterSpacing;
			}
			if (style.hasFontScale)
			{
				if (style.absoluteFontScale)
				{
					this.size = style.fontScale;
				}
				else
				{
					this.size *= style.fontScale;
				}
			}
			if (style.hasLineScale)
			{
				this.lineHeight = this.size * style.lineScale;
				flag = true;
			}
			if (flag)
			{
				this.lineDescent.baseline = this.font.baseline * this.lineHeight;
				this.lineDescent.baselineToBottom = this.font.baselineToBottom * this.lineHeight;
			}
		}

		// Token: 0x04002819 RID: 10265
		public Color32 color;

		// Token: 0x0400281A RID: 10266
		public BitmapFont font;

		// Token: 0x0400281B RID: 10267
		public TextRenderer renderer;

		// Token: 0x0400281C RID: 10268
		public float size;

		// Token: 0x0400281D RID: 10269
		public float letterSpacing;

		// Token: 0x0400281E RID: 10270
		public float lineHeight;

		// Token: 0x0400281F RID: 10271
		public LineDescent lineDescent;
	}
}
