using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class CCMoonTextColorer : CCTextModifier
{
	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06001149 RID: 4425 RVA: 0x0004F5B2 File Offset: 0x0004D7B2
	// (set) Token: 0x0600114A RID: 4426 RVA: 0x0004F5BA File Offset: 0x0004D7BA
	public Color[] Colors { get; set; }

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004F5C3 File Offset: 0x0004D7C3
	// (set) Token: 0x0600114C RID: 4428 RVA: 0x0004F5CB File Offset: 0x0004D7CB
	public float Count { get; set; }

	// Token: 0x0600114D RID: 4429 RVA: 0x0004F5D4 File Offset: 0x0004D7D4
	public override void Modify(CCText text)
	{
		if (this.Colors == null)
		{
			return;
		}
		Color[] colors = text.colors;
		int num = 0;
		for (int i = 0; i < text.Text.Length; i++)
		{
			if (text.Text[i] > ' ')
			{
				if (i < this.Colors.Length)
				{
					colors[num] = this.Colors[i];
					colors[num + 1] = this.Colors[i];
					colors[num + 2] = this.Colors[i];
					colors[num + 3] = this.Colors[i];
				}
				Color color = colors[num];
				color.a = this.OpacityCurve.Evaluate(this.Count - (float)i);
				colors[num] = color;
				colors[num + 1] = color;
				colors[num + 2] = color;
				colors[num + 3] = color;
				num += 4;
			}
		}
	}

	// Token: 0x04000EE9 RID: 3817
	public AnimationCurve OpacityCurve;

	// Token: 0x04000EEA RID: 3818
	public AnimationCurve ScaleCurve;
}
