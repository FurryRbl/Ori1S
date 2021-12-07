using System;
using UnityEngine;

// Token: 0x02000237 RID: 567
public sealed class CCTextRangeColorer : CCTextModifier
{
	// Token: 0x060012E5 RID: 4837 RVA: 0x00057600 File Offset: 0x00055800
	public override void Modify(CCText text)
	{
		char c = this.rangeBeginSymbol[0];
		char c2 = this.rangeEndSymbol[0];
		Color color = text.Color;
		Color[] colors = text.colors;
		int i = 0;
		int num = 0;
		int length = text.Length;
		while (i < length)
		{
			char c3 = text[i];
			if (c3 > ' ')
			{
				if (c <= c3 && c3 <= c2)
				{
					colors[num] = this.color;
					colors[num + 1] = this.color;
					colors[num + 2] = this.color;
					colors[num + 3] = this.color;
				}
				else
				{
					colors[num] = color;
					colors[num + 1] = color;
					colors[num + 2] = color;
					colors[num + 3] = color;
				}
				num += 4;
			}
			i++;
		}
		text.mesh.colors = colors;
	}

	// Token: 0x0400109C RID: 4252
	public string rangeBeginSymbol = "A";

	// Token: 0x0400109D RID: 4253
	public string rangeEndSymbol = "Z";

	// Token: 0x0400109E RID: 4254
	public Color color;
}
