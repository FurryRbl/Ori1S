using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200009A RID: 154
	public static class LayoutUtility
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x00017F7C File Offset: 0x0001617C
		public static float GetMinSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetMinWidth(rect);
			}
			return LayoutUtility.GetMinHeight(rect);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00017F94 File Offset: 0x00016194
		public static float GetPreferredSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetPreferredWidth(rect);
			}
			return LayoutUtility.GetPreferredHeight(rect);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00017FAC File Offset: 0x000161AC
		public static float GetFlexibleSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetFlexibleWidth(rect);
			}
			return LayoutUtility.GetFlexibleHeight(rect);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00017FC4 File Offset: 0x000161C4
		public static float GetMinWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017FFC File Offset: 0x000161FC
		public static float GetPreferredWidth(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredWidth, 0f));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00018060 File Offset: 0x00016260
		public static float GetFlexibleWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleWidth, 0f);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00018098 File Offset: 0x00016298
		public static float GetMinHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000180D0 File Offset: 0x000162D0
		public static float GetPreferredHeight(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredHeight, 0f));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00018134 File Offset: 0x00016334
		public static float GetFlexibleHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleHeight, 0f);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001816C File Offset: 0x0001636C
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue)
		{
			ILayoutElement layoutElement;
			return LayoutUtility.GetLayoutProperty(rect, property, defaultValue, out layoutElement);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00018184 File Offset: 0x00016384
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue, out ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float num = defaultValue;
			int num2 = int.MinValue;
			List<Component> list = ListPool<Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			for (int i = 0; i < list.Count; i++)
			{
				ILayoutElement layoutElement = list[i] as ILayoutElement;
				if (!(layoutElement is Behaviour) || ((Behaviour)layoutElement).isActiveAndEnabled)
				{
					int layoutPriority = layoutElement.layoutPriority;
					if (layoutPriority >= num2)
					{
						float num3 = property(layoutElement);
						if (num3 >= 0f)
						{
							if (layoutPriority > num2)
							{
								num = num3;
								num2 = layoutPriority;
								source = layoutElement;
							}
							else if (num3 > num)
							{
								num = num3;
								source = layoutElement;
							}
						}
					}
				}
			}
			ListPool<Component>.Release(list);
			return num;
		}
	}
}
