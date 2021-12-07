using System;
using UnityEngine;

// Token: 0x0200095A RID: 2394
public struct ColorHSV
{
	// Token: 0x060034B5 RID: 13493 RVA: 0x000DD2E4 File Offset: 0x000DB4E4
	public ColorHSV(float h, float s, float v)
	{
		this.h = h;
		this.s = s;
		this.v = v;
		this.a = 1f;
	}

	// Token: 0x060034B6 RID: 13494 RVA: 0x000DD311 File Offset: 0x000DB511
	public ColorHSV(float h, float s, float v, float a)
	{
		this.h = h;
		this.s = s;
		this.v = v;
		this.a = a;
	}

	// Token: 0x060034B7 RID: 13495 RVA: 0x000DD330 File Offset: 0x000DB530
	public ColorHSV(Color color)
	{
		float num = Mathf.Min(Mathf.Min(color.r, color.g), color.b);
		float num2 = Mathf.Max(Mathf.Max(color.r, color.g), color.b);
		float num3 = num2 - num;
		this.a = color.a;
		this.v = num2;
		if (Mathf.Approximately(num2, 0f))
		{
			this.s = 0f;
			this.h = -1f;
			return;
		}
		this.s = num3 / num2;
		if (Mathf.Approximately(num, num2))
		{
			this.v = num2;
			this.s = 0f;
			this.h = -1f;
			return;
		}
		if (color.r == num2)
		{
			this.h = (color.g - color.b) / num3;
		}
		else if (color.g == num2)
		{
			this.h = 2f + (color.b - color.r) / num3;
		}
		else
		{
			this.h = 4f + (color.r - color.g) / num3;
		}
		this.h *= 60f;
		if (this.h < 0f)
		{
			this.h += 360f;
		}
	}

	// Token: 0x060034B8 RID: 13496 RVA: 0x000DD4A0 File Offset: 0x000DB6A0
	public Color ToColor()
	{
		if (this.s == 0f)
		{
			return new Color(this.v, this.v, this.v, this.a);
		}
		float num = this.h / 60f;
		int num2 = Mathf.FloorToInt(num);
		float num3 = num - (float)num2;
		float num4 = this.v;
		float num5 = num4 * (1f - this.s);
		float num6 = num4 * (1f - this.s * num3);
		float num7 = num4 * (1f - this.s * (1f - num3));
		Color result = new Color(0f, 0f, 0f, this.a);
		switch (num2)
		{
		case 0:
			result.r = num4;
			result.g = num7;
			result.b = num5;
			break;
		case 1:
			result.r = num6;
			result.g = num4;
			result.b = num5;
			break;
		case 2:
			result.r = num5;
			result.g = num4;
			result.b = num7;
			break;
		case 3:
			result.r = num5;
			result.g = num6;
			result.b = num4;
			break;
		case 4:
			result.r = num7;
			result.g = num5;
			result.b = num4;
			break;
		default:
			result.r = num4;
			result.g = num5;
			result.b = num6;
			break;
		}
		return result;
	}

	// Token: 0x060034B9 RID: 13497 RVA: 0x000DD630 File Offset: 0x000DB830
	public new string ToString()
	{
		return string.Format("h: {0:0.00}, s: {1:0.00}, v: {2:0.00}, a: {3:0.00}", new object[]
		{
			this.h,
			this.s,
			this.v,
			this.a
		});
	}

	// Token: 0x04002F85 RID: 12165
	public float h;

	// Token: 0x04002F86 RID: 12166
	public float s;

	// Token: 0x04002F87 RID: 12167
	public float v;

	// Token: 0x04002F88 RID: 12168
	public float a;
}
