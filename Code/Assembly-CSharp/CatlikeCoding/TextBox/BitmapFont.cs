using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x0200068C RID: 1676
	public sealed class BitmapFont : ScriptableObject
	{
		// Token: 0x0600289C RID: 10396 RVA: 0x000B086E File Offset: 0x000AEA6E
		private BitmapFont()
		{
		}

		// Token: 0x17000674 RID: 1652
		public BitmapFontChar this[char c]
		{
			get
			{
				if ('!' <= c && c <= '~')
				{
					return this.asciiChars[(int)(c - '!')];
				}
				int num = 0;
				int i = this.otherChars.Length - 1;
				while (i >= num)
				{
					int num2 = num + i >> 1;
					int id = this.otherChars[num2].id;
					if (id > (int)c)
					{
						i = num2 - 1;
					}
					else
					{
						if (id >= (int)c)
						{
							return this.otherChars[num2];
						}
						num = num2 + 1;
					}
				}
				if (this.missing == null)
				{
					this.missing = new BitmapFontChar();
					this.missing.kerningIds = new int[0];
				}
				return this.missing;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x000B0924 File Offset: 0x000AEB24
		public bool IsValid
		{
			get
			{
				return this.pixelLineHeight != 0f;
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000B0938 File Offset: 0x000AEB38
		public void UpdateAllTextBoxes()
		{
			TextBox[] array = UnityEngine.Object.FindObjectsOfType(typeof(TextBox)) as TextBox[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RenderText();
			}
		}

		// Token: 0x04002438 RID: 9272
		public BitmapFontChar[] asciiChars;

		// Token: 0x04002439 RID: 9273
		public BitmapFontChar[] otherChars;

		// Token: 0x0400243A RID: 9274
		public float pixelLineHeight;

		// Token: 0x0400243B RID: 9275
		public float baseline;

		// Token: 0x0400243C RID: 9276
		public float baselineToBottom;

		// Token: 0x0400243D RID: 9277
		public float spaceAdvance;

		// Token: 0x0400243E RID: 9278
		public bool hasImportedKerningData;

		// Token: 0x0400243F RID: 9279
		public float importOffsetX;

		// Token: 0x04002440 RID: 9280
		public float importOffsetY;

		// Token: 0x04002441 RID: 9281
		public float importOffsetAdvance;

		// Token: 0x04002442 RID: 9282
		public float importLineHeightAdjustment;

		// Token: 0x04002443 RID: 9283
		private BitmapFontChar missing;
	}
}
