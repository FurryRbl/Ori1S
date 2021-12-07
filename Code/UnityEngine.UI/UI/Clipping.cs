using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000080 RID: 128
	public static class Clipping
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00015C3C File Offset: 0x00013E3C
		public static Rect FindCullAndClipWorldRect(List<RectMask2D> rectMaskParents, out bool validRect)
		{
			if (rectMaskParents.Count == 0)
			{
				validRect = false;
				return default(Rect);
			}
			Rect a = rectMaskParents[0].canvasRect;
			for (int i = 0; i < rectMaskParents.Count; i++)
			{
				a = Clipping.RectIntersect(a, rectMaskParents[i].canvasRect);
			}
			bool flag = a.width <= 0f || a.height <= 0f;
			if (flag)
			{
				validRect = false;
				return default(Rect);
			}
			Vector3 vector = new Vector3(a.x, a.y, 0f);
			Vector3 vector2 = new Vector3(a.x + a.width, a.y + a.height, 0f);
			validRect = true;
			return new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00015D4C File Offset: 0x00013F4C
		private static Rect RectIntersect(Rect a, Rect b)
		{
			float num = Mathf.Max(a.x, b.x);
			float num2 = Mathf.Min(a.x + a.width, b.x + b.width);
			float num3 = Mathf.Max(a.y, b.y);
			float num4 = Mathf.Min(a.y + a.height, b.y + b.height);
			if (num2 >= num && num4 >= num3)
			{
				return new Rect(num, num3, num2 - num, num4 - num3);
			}
			return new Rect(0f, 0f, 0f, 0f);
		}
	}
}
