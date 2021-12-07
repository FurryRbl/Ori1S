using System;
using UnityEngine;

// Token: 0x02000236 RID: 566
public sealed class CCTextMarkedColorer : CCTextModifier
{
	// Token: 0x060012E3 RID: 4835 RVA: 0x0005747C File Offset: 0x0005567C
	public override void Modify(CCText text)
	{
		char c = this.beginSymbol[0];
		char c2 = this.endSymbol[0];
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
				if (c3 == c)
				{
					colors[num] = color;
					colors[num + 1] = color;
					colors[num + 2] = color;
					colors[num + 3] = color;
					color = this.color;
				}
				else if (c3 == c2)
				{
					color = text.Color;
					colors[num] = color;
					colors[num + 1] = color;
					colors[num + 2] = color;
					colors[num + 3] = color;
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

	// Token: 0x04001099 RID: 4249
	public string beginSymbol = "[";

	// Token: 0x0400109A RID: 4250
	public string endSymbol = "]";

	// Token: 0x0400109B RID: 4251
	public Color color;
}
