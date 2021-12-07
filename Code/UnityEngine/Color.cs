using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000280 RID: 640
	[UsedByNativeCode]
	public struct Color
	{
		// Token: 0x06002570 RID: 9584 RVA: 0x000337BC File Offset: 0x000319BC
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000337DC File Offset: 0x000319DC
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0003380C File Offset: 0x00031A0C
		public override string ToString()
		{
			return UnityString.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", new object[]
			{
				this.r,
				this.g,
				this.b,
				this.a
			});
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00033864 File Offset: 0x00031A64
		public string ToString(string format)
		{
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format),
				this.g.ToString(format),
				this.b.ToString(format),
				this.a.ToString(format)
			});
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000338C0 File Offset: 0x00031AC0
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000338E0 File Offset: 0x00031AE0
		public override bool Equals(object other)
		{
			if (!(other is Color))
			{
				return false;
			}
			Color color = (Color)other;
			return this.r.Equals(color.r) && this.g.Equals(color.g) && this.b.Equals(color.b) && this.a.Equals(color.a);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x0003395C File Offset: 0x00031B5C
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000339DC File Offset: 0x00031BDC
		public static Color LerpUnclamped(Color a, Color b, float t)
		{
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00033A54 File Offset: 0x00031C54
		internal Color RGBMultiplied(float multiplier)
		{
			return new Color(this.r * multiplier, this.g * multiplier, this.b * multiplier, this.a);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00033A7C File Offset: 0x00031C7C
		internal Color AlphaMultiplied(float multiplier)
		{
			return new Color(this.r, this.g, this.b, this.a * multiplier);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00033AA0 File Offset: 0x00031CA0
		internal Color RGBMultiplied(Color multiplier)
		{
			return new Color(this.r * multiplier.r, this.g * multiplier.g, this.b * multiplier.b, this.a);
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x00033AD8 File Offset: 0x00031CD8
		public static Color red
		{
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00033AF4 File Offset: 0x00031CF4
		public static Color green
		{
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x00033B10 File Offset: 0x00031D10
		public static Color blue
		{
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00033B2C File Offset: 0x00031D2C
		public static Color white
		{
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00033B48 File Offset: 0x00031D48
		public static Color black
		{
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x00033B64 File Offset: 0x00031D64
		public static Color yellow
		{
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x00033B80 File Offset: 0x00031D80
		public static Color cyan
		{
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x00033B9C File Offset: 0x00031D9C
		public static Color magenta
		{
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x00033BB8 File Offset: 0x00031DB8
		public static Color gray
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x00033BD4 File Offset: 0x00031DD4
		public static Color grey
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x00033BF0 File Offset: 0x00031DF0
		public static Color clear
		{
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x00033C0C File Offset: 0x00031E0C
		public float grayscale
		{
			get
			{
				return 0.299f * this.r + 0.587f * this.g + 0.114f * this.b;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x00033C40 File Offset: 0x00031E40
		public Color linear
		{
			get
			{
				return new Color(Mathf.GammaToLinearSpace(this.r), Mathf.GammaToLinearSpace(this.g), Mathf.GammaToLinearSpace(this.b), this.a);
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x00033C7C File Offset: 0x00031E7C
		public Color gamma
		{
			get
			{
				return new Color(Mathf.LinearToGammaSpace(this.r), Mathf.LinearToGammaSpace(this.g), Mathf.LinearToGammaSpace(this.b), this.a);
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x00033CB8 File Offset: 0x00031EB8
		public float maxColorComponent
		{
			get
			{
				return Mathf.Max(Mathf.Max(this.r, this.g), this.b);
			}
		}

		// Token: 0x17000943 RID: 2371
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.r;
				case 1:
					return this.g;
				case 2:
					return this.b;
				case 3:
					return this.a;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.r = value;
					break;
				case 1:
					this.g = value;
					break;
				case 2:
					this.b = value;
					break;
				case 3:
					this.a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00033D90 File Offset: 0x00031F90
		public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V)
		{
			if (rgbColor.b > rgbColor.g && rgbColor.b > rgbColor.r)
			{
				Color.RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
			}
			else if (rgbColor.g > rgbColor.r)
			{
				Color.RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
			}
			else
			{
				Color.RGBToHSVHelper(0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00033E48 File Offset: 0x00032048
		private static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V)
		{
			V = dominantcolor;
			if (V != 0f)
			{
				float num;
				if (colorone > colortwo)
				{
					num = colortwo;
				}
				else
				{
					num = colorone;
				}
				float num2 = V - num;
				if (num2 != 0f)
				{
					S = num2 / V;
					H = offset + (colorone - colortwo) / num2;
				}
				else
				{
					S = 0f;
					H = offset + (colorone - colortwo);
				}
				H /= 6f;
				if (H < 0f)
				{
					H += 1f;
				}
			}
			else
			{
				S = 0f;
				H = 0f;
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00033EF0 File Offset: 0x000320F0
		public static Color HSVToRGB(float H, float S, float V)
		{
			return Color.HSVToRGB(H, S, V, true);
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00033EFC File Offset: 0x000320FC
		public static Color HSVToRGB(float H, float S, float V, bool hdr)
		{
			Color white = Color.white;
			if (S == 0f)
			{
				white.r = V;
				white.g = V;
				white.b = V;
			}
			else if (V == 0f)
			{
				white.r = 0f;
				white.g = 0f;
				white.b = 0f;
			}
			else
			{
				white.r = 0f;
				white.g = 0f;
				white.b = 0f;
				float num = H * 6f;
				int num2 = (int)Mathf.Floor(num);
				float num3 = num - (float)num2;
				float num4 = V * (1f - S);
				float num5 = V * (1f - S * num3);
				float num6 = V * (1f - S * (1f - num3));
				int num7 = num2;
				switch (num7 + 1)
				{
				case 0:
					white.r = V;
					white.g = num4;
					white.b = num5;
					break;
				case 1:
					white.r = V;
					white.g = num6;
					white.b = num4;
					break;
				case 2:
					white.r = num5;
					white.g = V;
					white.b = num4;
					break;
				case 3:
					white.r = num4;
					white.g = V;
					white.b = num6;
					break;
				case 4:
					white.r = num4;
					white.g = num5;
					white.b = V;
					break;
				case 5:
					white.r = num6;
					white.g = num4;
					white.b = V;
					break;
				case 6:
					white.r = V;
					white.g = num4;
					white.b = num5;
					break;
				case 7:
					white.r = V;
					white.g = num6;
					white.b = num4;
					break;
				}
				if (!hdr)
				{
					white.r = Mathf.Clamp(white.r, 0f, 1f);
					white.g = Mathf.Clamp(white.g, 0f, 1f);
					white.b = Mathf.Clamp(white.b, 0f, 1f);
				}
			}
			return white;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00034160 File Offset: 0x00032360
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000341B0 File Offset: 0x000323B0
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00034200 File Offset: 0x00032400
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x00034250 File Offset: 0x00032450
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0003427C File Offset: 0x0003247C
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000342A8 File Offset: 0x000324A8
		public static Color operator /(Color a, float b)
		{
			return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000342D4 File Offset: 0x000324D4
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs == rhs;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000342E8 File Offset: 0x000324E8
		public static bool operator !=(Color lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000342F4 File Offset: 0x000324F4
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x00034318 File Offset: 0x00032518
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}

		// Token: 0x040009DF RID: 2527
		public float r;

		// Token: 0x040009E0 RID: 2528
		public float g;

		// Token: 0x040009E1 RID: 2529
		public float b;

		// Token: 0x040009E2 RID: 2530
		public float a;
	}
}
