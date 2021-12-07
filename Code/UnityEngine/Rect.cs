using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000060 RID: 96
	[UsedByNativeCode]
	public struct Rect
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x00006D50 File Offset: 0x00004F50
		public Rect(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00006D70 File Offset: 0x00004F70
		public Rect(Vector2 position, Vector2 size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00006DB4 File Offset: 0x00004FB4
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public static Rect MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00006E08 File Offset: 0x00005008
		public void Set(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00006E28 File Offset: 0x00005028
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00006E30 File Offset: 0x00005030
		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00006E3C File Offset: 0x0000503C
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00006E44 File Offset: 0x00005044
		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00006E50 File Offset: 0x00005050
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x00006E64 File Offset: 0x00005064
		public Vector2 position
		{
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00006E80 File Offset: 0x00005080
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00006EB8 File Offset: 0x000050B8
		public Vector2 center
		{
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			set
			{
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00006EFC File Offset: 0x000050FC
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00006F10 File Offset: 0x00005110
		public Vector2 min
		{
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00006F2C File Offset: 0x0000512C
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00006F40 File Offset: 0x00005140
		public Vector2 max
		{
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00006F5C File Offset: 0x0000515C
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00006F64 File Offset: 0x00005164
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00006F70 File Offset: 0x00005170
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00006F78 File Offset: 0x00005178
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00006F84 File Offset: 0x00005184
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00006F98 File Offset: 0x00005198
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00006FB4 File Offset: 0x000051B4
		[Obsolete("use xMin")]
		public float left
		{
			get
			{
				return this.m_XMin;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00006FBC File Offset: 0x000051BC
		[Obsolete("use xMax")]
		public float right
		{
			get
			{
				return this.m_XMin + this.m_Width;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00006FCC File Offset: 0x000051CC
		[Obsolete("use yMin")]
		public float top
		{
			get
			{
				return this.m_YMin;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00006FD4 File Offset: 0x000051D4
		[Obsolete("use yMax")]
		public float bottom
		{
			get
			{
				return this.m_YMin + this.m_Height;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00006FE4 File Offset: 0x000051E4
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00006FEC File Offset: 0x000051EC
		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00007018 File Offset: 0x00005218
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00007020 File Offset: 0x00005220
		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000704C File Offset: 0x0000524C
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000705C File Offset: 0x0000525C
		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000706C File Offset: 0x0000526C
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000707C File Offset: 0x0000527C
		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000708C File Offset: 0x0000528C
		public override string ToString()
		{
			return UnityString.Format("(x:{0:F2}, y:{1:F2}, width:{2:F2}, height:{3:F2})", new object[]
			{
				this.x,
				this.y,
				this.width,
				this.height
			});
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000070E4 File Offset: 0x000052E4
		public string ToString(string format)
		{
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.width.ToString(format),
				this.height.ToString(format)
			});
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000714C File Offset: 0x0000534C
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000071A4 File Offset: 0x000053A4
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000071FC File Offset: 0x000053FC
		public bool Contains(Vector3 point, bool allowInverse)
		{
			if (!allowInverse)
			{
				return this.Contains(point);
			}
			bool flag = false;
			if ((this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax))
			{
				flag = true;
			}
			return flag && ((this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax));
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000072F4 File Offset: 0x000054F4
		private static Rect OrderMinMax(Rect rect)
		{
			if (rect.xMin > rect.xMax)
			{
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			if (rect.yMin > rect.yMax)
			{
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00007364 File Offset: 0x00005564
		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000073BC File Offset: 0x000055BC
		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse)
			{
				rect = Rect.OrderMinMax(rect);
				other = Rect.OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000073F0 File Offset: 0x000055F0
		public static Vector2 NormalizedToPoint(Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2(Mathf.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00007438 File Offset: 0x00005638
		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			return new Vector2(Mathf.InverseLerp(rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp(rectangle.y, rectangle.yMax, point.y));
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00007480 File Offset: 0x00005680
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.width.GetHashCode() << 2 ^ this.y.GetHashCode() >> 2 ^ this.height.GetHashCode() >> 1;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000074D0 File Offset: 0x000056D0
		public override bool Equals(object other)
		{
			if (!(other is Rect))
			{
				return false;
			}
			Rect rect = (Rect)other;
			return this.x.Equals(rect.x) && this.y.Equals(rect.y) && this.width.Equals(rect.width) && this.height.Equals(rect.height);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00007558 File Offset: 0x00005758
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.width != rhs.width || lhs.height != rhs.height;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000075B4 File Offset: 0x000057B4
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x040000E4 RID: 228
		private float m_XMin;

		// Token: 0x040000E5 RID: 229
		private float m_YMin;

		// Token: 0x040000E6 RID: 230
		private float m_Width;

		// Token: 0x040000E7 RID: 231
		private float m_Height;
	}
}
