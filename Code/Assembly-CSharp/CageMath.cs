using System;
using UnityEngine;

// Token: 0x020007B4 RID: 1972
public class CageMath : MonoBehaviour
{
	// Token: 0x020007B5 RID: 1973
	public static class Vector
	{
		// Token: 0x06002D9F RID: 11679 RVA: 0x000C2D24 File Offset: 0x000C0F24
		private static float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000C2D70 File Offset: 0x000C0F70
		public static bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
		{
			bool flag = CageMath.Vector.Sign(pt, v1, v2) < 0f;
			bool flag2 = CageMath.Vector.Sign(pt, v2, v3) < 0f;
			bool flag3 = CageMath.Vector.Sign(pt, v3, v1) < 0f;
			return flag == flag2 && flag2 == flag3;
		}
	}

	// Token: 0x020007B6 RID: 1974
	public static class Line
	{
		// Token: 0x06002DA1 RID: 11681 RVA: 0x000C2DBC File Offset: 0x000C0FBC
		public static Vector3 ClosestPointOnLineSegmentToPoint(Vector3 p1, Vector3 p2, Vector3 p)
		{
			float num = ((p.x - p1.x) * (p2.x - p1.x) + (p.y - p1.y) * (p2.y - p1.y)) / Vector3.SqrMagnitude(p2 - p1);
			num = Mathf.Clamp01(num);
			return Vector3.Lerp(p1, p2, num);
		}
	}

	// Token: 0x020007B7 RID: 1975
	public static class Rectangle
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000C2E28 File Offset: 0x000C1028
		public static Rect Absolute(Rect rect)
		{
			return Rect.MinMaxRect(Mathf.Min(rect.xMin, rect.xMax), Mathf.Min(rect.yMin, rect.yMax), Mathf.Max(rect.xMin, rect.xMax), Mathf.Max(rect.yMin, rect.yMax));
		}
	}
}
