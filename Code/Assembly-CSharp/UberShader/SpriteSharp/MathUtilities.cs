using System;
using UnityEngine;

namespace UberShader.SpriteSharp
{
	// Token: 0x02000823 RID: 2083
	public static class MathUtilities
	{
		// Token: 0x06002FB5 RID: 12213 RVA: 0x000CA1D4 File Offset: 0x000C83D4
		public static bool AreLineSegmentsIntersecting(Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End)
		{
			Vector2 vector;
			vector.x = line1End.x - line1Start.x;
			vector.y = line1End.y - line1Start.y;
			Vector2 vector2;
			vector2.x = line2End.x - line2Start.x;
			vector2.y = line2End.y - line2Start.y;
			Vector2 vector3;
			vector3.x = line1Start.x - line2Start.x;
			vector3.y = line1Start.y - line2Start.y;
			float num = 1f / (-vector2.x * vector.y + vector.x * vector2.y);
			float num2 = (-vector.y * vector3.x + vector.x * vector3.y) * num;
			float num3 = (vector2.x * vector3.y - vector2.y * vector3.x) * num;
			return num2 > 0f && num2 < 1f && num3 > 0f && num3 < 1f;
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000CA304 File Offset: 0x000C8504
		public static float CalculateTriangleArea(Vector2 point1, Vector2 point2, Vector2 point3)
		{
			float f = (point2.x - point1.x) * (point3.y - point1.y) - (point3.x - point1.x) * (point2.y - point1.y);
			return Mathf.Abs(f) * 0.5f;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000CA360 File Offset: 0x000C8560
		public static float CalculatePolygonArea(Vector2[] polygon)
		{
			int num = polygon.Length;
			if (num < 3)
			{
				return 0f;
			}
			float num2 = 0f;
			int i = 0;
			int num3 = num - 1;
			while (i < num)
			{
				num2 += (polygon[num3].x + polygon[i].x) * (polygon[num3].y - polygon[i].y);
				num3 = i;
				i++;
			}
			return Mathf.Abs(num2) * 0.5f;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000CA3E4 File Offset: 0x000C85E4
		public static bool IsPointInsidePolygon(Vector2 point, Vector2[] polygon)
		{
			int num = 0;
			int i = 0;
			int num2 = polygon.Length;
			while (i < num2)
			{
				Vector2 vector = (i != num2 - 1) ? polygon[i + 1] : polygon[0];
				if ((polygon[i].y <= point.y && vector.y > point.y) || (polygon[i].y > point.y && vector.y <= point.y))
				{
					float num3 = (point.y - polygon[i].y) / (vector.y - polygon[i].y);
					if (point.x < polygon[i].x + num3 * (vector.x - polygon[i].x))
					{
						num++;
					}
				}
				i++;
			}
			return (num & 1) == 1;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000CA4F4 File Offset: 0x000C86F4
		public static bool IsPolygonInsidePolygon(Vector2[] innerPolygon, Rect innerPolygonRect, Vector2[] outerPolygon, Rect outerPolygonRect)
		{
			if (!MathUtilities.AreRectsIntersecting(innerPolygonRect, outerPolygonRect))
			{
				return MathUtilities.IsPointInsidePolygon(innerPolygon[0], outerPolygon);
			}
			int i = 0;
			int num = innerPolygon.Length;
			while (i < num)
			{
				Vector2 line1End = (i != num - 1) ? innerPolygon[i + 1] : innerPolygon[0];
				int j = 0;
				int num2 = outerPolygon.Length;
				while (j < num2)
				{
					Vector2 line2End = (j != num2 - 1) ? outerPolygon[j + 1] : outerPolygon[0];
					if (MathUtilities.AreLineSegmentsIntersecting(innerPolygon[i], line1End, outerPolygon[j], line2End))
					{
						return false;
					}
					j++;
				}
				i++;
			}
			return MathUtilities.IsPointInsidePolygon(innerPolygon[0], outerPolygon);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000CA5DC File Offset: 0x000C87DC
		public static bool IsPolygonInsidePolygon(Vector2[] innerPolygon, Vector2[] outerPolygon)
		{
			int i = 0;
			int num = innerPolygon.Length;
			while (i < num)
			{
				Vector2 line1End = (i != num - 1) ? innerPolygon[i + 1] : innerPolygon[0];
				int j = 0;
				int num2 = outerPolygon.Length;
				while (j < num2)
				{
					Vector2 line2End = (j != num2 - 1) ? outerPolygon[j + 1] : outerPolygon[0];
					if (MathUtilities.AreLineSegmentsIntersecting(innerPolygon[i], line1End, outerPolygon[j], line2End))
					{
						return false;
					}
					j++;
				}
				i++;
			}
			return MathUtilities.IsPointInsidePolygon(innerPolygon[0], outerPolygon);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000CA6A4 File Offset: 0x000C88A4
		public static bool AreRectsIntersecting(Rect a, Rect b)
		{
			return a.xMin < b.xMax && a.xMax > b.xMin && a.yMin < b.yMax && a.yMax > b.yMin;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000CA700 File Offset: 0x000C8900
		public static float CalculateMeshArea(ushort[] triangles, Vector2[] vertices)
		{
			float num = 0f;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				num += MathUtilities.CalculateTriangleArea(vertices[(int)triangles[i]], vertices[(int)triangles[i + 1]], vertices[(int)triangles[i + 2]]);
			}
			return num;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000CA760 File Offset: 0x000C8960
		public static float CalculateMeshAreaAndBoundingBox(ushort[] triangles, Vector2[] vertices, out Vector2 min, out Vector2 max)
		{
			min = new Vector2(float.MaxValue, float.MaxValue);
			max = new Vector2(float.MinValue, float.MinValue);
			float num = 0f;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				Vector2 point = vertices[(int)triangles[i]];
				Vector2 point2 = vertices[(int)triangles[i + 1]];
				Vector2 point3 = vertices[(int)triangles[i + 2]];
				num += MathUtilities.CalculateTriangleArea(point, point2, point3);
				if (point.x < min.x)
				{
					min.x = point.x;
				}
				if (point2.x < min.x)
				{
					min.x = point2.x;
				}
				if (point3.x < min.x)
				{
					min.x = point3.x;
				}
				if (point.x > max.x)
				{
					max.x = point.x;
				}
				if (point2.x > max.x)
				{
					max.x = point2.x;
				}
				if (point3.x > max.x)
				{
					max.x = point3.x;
				}
				if (point.y < min.y)
				{
					min.y = point.y;
				}
				if (point2.y < min.y)
				{
					min.y = point2.y;
				}
				if (point3.y < min.y)
				{
					min.y = point3.y;
				}
				if (point.y > max.y)
				{
					max.y = point.y;
				}
				if (point2.y > max.y)
				{
					max.y = point2.y;
				}
				if (point3.y > max.y)
				{
					max.y = point3.y;
				}
			}
			return num;
		}
	}
}
