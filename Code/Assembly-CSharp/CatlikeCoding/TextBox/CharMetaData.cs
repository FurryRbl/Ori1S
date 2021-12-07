using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000689 RID: 1673
	public struct CharMetaData
	{
		// Token: 0x06002891 RID: 10385 RVA: 0x000B05A8 File Offset: 0x000AE7A8
		public void EraseIfVisible()
		{
			if (this.type == CharType.Visible)
			{
				this.renderer.renderedCharCount--;
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000B05CC File Offset: 0x000AE7CC
		public void RenderIfVisible(Vector2 offset)
		{
			if (this.type == CharType.Visible)
			{
				BitmapFontChar bitmapFontChar = this.font[this.id];
				if (bitmapFontChar.height == 0f)
				{
					this.id = '□';
				}
				this.renderer.Add(this, offset);
			}
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000B0624 File Offset: 0x000AE824
		public void AdjustPositionInBox(Vector2 delta)
		{
			this.positionInBox.x = this.positionInBox.x + delta.x;
			this.positionInBox.y = this.positionInBox.y + delta.y;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000B0664 File Offset: 0x000AE864
		public void AdjustPositionInBox(float xDelta, float yDelta)
		{
			this.positionInBox.x = this.positionInBox.x + xDelta;
			this.positionInBox.y = this.positionInBox.y + yDelta;
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000B0697 File Offset: 0x000AE897
		public char MarkAsStyleStatement(int unstyledIndex, Vector2 position)
		{
			this.unstyledIndex = unstyledIndex;
			this.type = CharType.Style;
			this.positionInBox = position;
			return this.id;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000B06B4 File Offset: 0x000AE8B4
		public void MarkAsWhitespace(int unstyledIndex, Vector2 position, ref AppliedTextStyle style)
		{
			this.unstyledIndex = unstyledIndex;
			this.type = CharType.Whitespace;
			this.positionInBox = position;
			this.font = style.font;
			this.scale = style.size;
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000B06E4 File Offset: 0x000AE8E4
		public BitmapFontChar MarkAsVisible(int unstyledIndex, Vector2 position, ref AppliedTextStyle style)
		{
			this.unstyledIndex = unstyledIndex;
			this.type = CharType.Visible;
			this.positionInBox = position;
			this.color = style.color;
			this.font = style.font;
			this.scale = style.size;
			this.renderer = style.renderer;
			this.renderer.renderedCharCount++;
			return this.font[this.id];
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000B075C File Offset: 0x000AE95C
		public float After
		{
			get
			{
				if (this.type == CharType.Visible)
				{
					return this.positionInBox.x + this.font[this.id].advance * this.scale;
				}
				if (this.type == CharType.Whitespace)
				{
					return this.positionInBox.x + this.font.spaceAdvance * this.scale;
				}
				return this.positionInBox.x;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x000B07D5 File Offset: 0x000AE9D5
		public float HorizontalMiddle
		{
			get
			{
				return (this.positionInBox.x + this.After) * 0.5f;
			}
		}

		// Token: 0x04002420 RID: 9248
		public char id;

		// Token: 0x04002421 RID: 9249
		public int unstyledIndex;

		// Token: 0x04002422 RID: 9250
		public CharType type;

		// Token: 0x04002423 RID: 9251
		public Color32 color;

		// Token: 0x04002424 RID: 9252
		public float scale;

		// Token: 0x04002425 RID: 9253
		public Vector2 positionInBox;

		// Token: 0x04002426 RID: 9254
		public BitmapFont font;

		// Token: 0x04002427 RID: 9255
		public TextRenderer renderer;
	}
}
