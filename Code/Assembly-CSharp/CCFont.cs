using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
public sealed class CCFont : ScriptableObject
{
	// Token: 0x060012BC RID: 4796 RVA: 0x0005603A File Offset: 0x0005423A
	private CCFont()
	{
	}

	// Token: 0x17000350 RID: 848
	public CCFont.Char this[char c]
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
				this.missing = new CCFont.Char();
				this.missing.kerningIds = new int[0];
			}
			return this.missing;
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x060012BE RID: 4798 RVA: 0x000560F0 File Offset: 0x000542F0
	public bool IsValid
	{
		get
		{
			return this.pixelLineHeight != 0;
		}
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x00056100 File Offset: 0x00054300
	public void UpdateAllCCText()
	{
		CCText[] array = (CCText[])UnityEngine.Object.FindObjectsOfType(typeof(CCText));
		for (int i = 0; i < array.Length; i++)
		{
			array[i].UpdateText();
		}
	}

	// Token: 0x04001029 RID: 4137
	public CCFont.Char[] asciiChars;

	// Token: 0x0400102A RID: 4138
	public CCFont.Char[] otherChars;

	// Token: 0x0400102B RID: 4139
	public int pixelLineHeight;

	// Token: 0x0400102C RID: 4140
	public float pixelScale;

	// Token: 0x0400102D RID: 4141
	public float baseline;

	// Token: 0x0400102E RID: 4142
	public float spaceAdvance;

	// Token: 0x0400102F RID: 4143
	public bool supportsKerning;

	// Token: 0x04001030 RID: 4144
	public float leftMargin;

	// Token: 0x04001031 RID: 4145
	public float rightMargin;

	// Token: 0x04001032 RID: 4146
	public float topMargin;

	// Token: 0x04001033 RID: 4147
	public float bottomMargin;

	// Token: 0x04001034 RID: 4148
	private CCFont.Char missing;

	// Token: 0x02000228 RID: 552
	[Serializable]
	public class Char
	{
		// Token: 0x060012C1 RID: 4801 RVA: 0x00056148 File Offset: 0x00054348
		public float AdvanceWithKerning(char nextChar)
		{
			if (this.kerningIds.Length == 0)
			{
				return this.advance;
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
						return this.advance + this.kernings[num2];
					}
					num = num2 + 1;
				}
			}
			return this.advance;
		}

		// Token: 0x04001035 RID: 4149
		public int id;

		// Token: 0x04001036 RID: 4150
		public float uMin;

		// Token: 0x04001037 RID: 4151
		public float uMax;

		// Token: 0x04001038 RID: 4152
		public float vMin;

		// Token: 0x04001039 RID: 4153
		public float vMax;

		// Token: 0x0400103A RID: 4154
		public float xOffset;

		// Token: 0x0400103B RID: 4155
		public float yOffset;

		// Token: 0x0400103C RID: 4156
		public float width;

		// Token: 0x0400103D RID: 4157
		public float height;

		// Token: 0x0400103E RID: 4158
		public float advance;

		// Token: 0x0400103F RID: 4159
		public int[] kerningIds;

		// Token: 0x04001040 RID: 4160
		public float[] kernings;
	}
}
