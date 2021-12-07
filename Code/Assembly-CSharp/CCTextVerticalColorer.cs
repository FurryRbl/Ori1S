using System;
using UnityEngine;

// Token: 0x0200023A RID: 570
public sealed class CCTextVerticalColorer : CCTextModifier
{
	// Token: 0x060012EC RID: 4844 RVA: 0x00057BE0 File Offset: 0x00055DE0
	public override void Modify(CCText text)
	{
		float y = text.minBounds.y;
		float num = 1f / (text.maxBounds.y - y);
		Vector3[] vertices = text.vertices;
		Color[] colors = text.colors;
		int i = 0;
		int num2 = 0;
		int length = text.Length;
		while (i < length)
		{
			char c = text[i];
			if (c > ' ')
			{
				Color color = Color.Lerp(this.bottomColor, this.topColor, (vertices[num2].y - y) * num);
				Color color2 = Color.Lerp(this.bottomColor, this.topColor, (vertices[num2 + 2].y - y) * num);
				colors[num2] = color;
				colors[num2 + 1] = color;
				colors[num2 + 2] = color2;
				colors[num2 + 3] = color2;
				num2 += 4;
			}
			i++;
		}
		text.mesh.colors = colors;
	}

	// Token: 0x040010A7 RID: 4263
	public Color topColor = Color.white;

	// Token: 0x040010A8 RID: 4264
	public Color bottomColor = Color.red;
}
