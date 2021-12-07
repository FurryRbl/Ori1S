using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200005E RID: 94
	[UsedByNativeCode]
	[IL2CPPStructAlignment(Align = 4)]
	public struct Color32
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00006254 File Offset: 0x00004454
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00006274 File Offset: 0x00004474
		public override string ToString()
		{
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r,
				this.g,
				this.b,
				this.a
			});
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000062CC File Offset: 0x000044CC
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

		// Token: 0x0600054C RID: 1356 RVA: 0x00006328 File Offset: 0x00004528
		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000063B4 File Offset: 0x000045B4
		public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
		{
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00006438 File Offset: 0x00004638
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)(Mathf.Clamp01(c.r) * 255f), (byte)(Mathf.Clamp01(c.g) * 255f), (byte)(Mathf.Clamp01(c.b) * 255f), (byte)(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00006498 File Offset: 0x00004698
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x040000DB RID: 219
		public byte r;

		// Token: 0x040000DC RID: 220
		public byte g;

		// Token: 0x040000DD RID: 221
		public byte b;

		// Token: 0x040000DE RID: 222
		public byte a;
	}
}
