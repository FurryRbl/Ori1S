using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F0 RID: 1264
public class Triangulate
{
	// Token: 0x06002217 RID: 8727 RVA: 0x000959E0 File Offset: 0x00093BE0
	public static float Area(ref List<Vector2> contour)
	{
		int count = contour.Count;
		float num = 0f;
		int index = count - 1;
		int i = 0;
		while (i < count)
		{
			num += contour[index].x * contour[i].y - contour[i].x * contour[index].y;
			index = i++;
		}
		return num * 0.5f;
	}

	// Token: 0x06002218 RID: 8728 RVA: 0x00095A64 File Offset: 0x00093C64
	public static bool InsideTriangle(float Ax, float Ay, float Bx, float By, float Cx, float Cy, float Px, float Py)
	{
		float num = Cx - Bx;
		float num2 = Cy - By;
		float num3 = Ax - Cx;
		float num4 = Ay - Cy;
		float num5 = Bx - Ax;
		float num6 = By - Ay;
		float num7 = Px - Ax;
		float num8 = Py - Ay;
		float num9 = Px - Bx;
		float num10 = Py - By;
		float num11 = Px - Cx;
		float num12 = Py - Cy;
		float num13 = num * num10 - num2 * num9;
		float num14 = num5 * num8 - num6 * num7;
		float num15 = num3 * num12 - num4 * num11;
		return num13 >= 0f && num15 >= 0f && num14 >= 0f;
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x00095B00 File Offset: 0x00093D00
	public static bool Snip(ref List<Vector2> contour, int u, int v, int w, int n, ref List<int> V)
	{
		float x = contour[V[u]].x;
		float y = contour[V[u]].y;
		float x2 = contour[V[v]].x;
		float y2 = contour[V[v]].y;
		float x3 = contour[V[w]].x;
		float y3 = contour[V[w]].y;
		if (Triangulate.EPSILON > (x2 - x) * (y3 - y) - (y2 - y) * (x3 - x))
		{
			return false;
		}
		for (int i = 0; i < n; i++)
		{
			if (i != u && i != v && i != w)
			{
				float x4 = contour[V[i]].x;
				float y4 = contour[V[i]].y;
				if (Triangulate.InsideTriangle(x, y, x2, y2, x3, y3, x4, y4))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600221A RID: 8730 RVA: 0x00095C48 File Offset: 0x00093E48
	public static bool Process(ref List<Vector2> contour, ref List<int> result, out bool counterClockwise)
	{
		counterClockwise = false;
		int count = contour.Count;
		if (count < 3)
		{
			return false;
		}
		List<int> list = new List<int>();
		if (0f < Triangulate.Area(ref contour))
		{
			counterClockwise = true;
			for (int i = 0; i < count; i++)
			{
				list.Add(i);
			}
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				list.Add(count - 1 - j);
			}
		}
		int k = count;
		int num = 2 * k;
		int num2 = 0;
		int num3 = k - 1;
		while (k > 2)
		{
			if (0 >= num--)
			{
				return false;
			}
			int num4 = num3;
			if (k <= num4)
			{
				num4 = 0;
			}
			num3 = num4 + 1;
			if (k <= num3)
			{
				num3 = 0;
			}
			int num5 = num3 + 1;
			if (k <= num5)
			{
				num5 = 0;
			}
			if (Triangulate.Snip(ref contour, num4, num3, num5, k, ref list))
			{
				int item = list[num4];
				int item2 = list[num3];
				int item3 = list[num5];
				result.Add(item3);
				result.Add(item2);
				result.Add(item);
				num2++;
				int num6 = num3;
				for (int l = num3 + 1; l < k; l++)
				{
					list[num6] = list[l];
					num6++;
				}
				k--;
				num = 2 * k;
			}
		}
		return true;
	}

	// Token: 0x04001CAC RID: 7340
	private static float EPSILON = 1E-10f;
}
