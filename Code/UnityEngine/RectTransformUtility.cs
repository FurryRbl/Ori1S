using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001E9 RID: 489
	public sealed class RectTransformUtility
	{
		// Token: 0x06001DA7 RID: 7591 RVA: 0x0001BE3C File Offset: 0x0001A03C
		private RectTransformUtility()
		{
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0001BE54 File Offset: 0x0001A054
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, null);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0001BE60 File Offset: 0x0001A060
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			return RectTransformUtility.INTERNAL_CALL_RectangleContainsScreenPoint(rect, ref screenPoint, cam);
		}

		// Token: 0x06001DAB RID: 7595
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_RectangleContainsScreenPoint(RectTransform rect, ref Vector2 screenPoint, Camera cam);

		// Token: 0x06001DAC RID: 7596 RVA: 0x0001BE6C File Offset: 0x0001A06C
		public static Vector2 PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas)
		{
			Vector2 result;
			RectTransformUtility.PixelAdjustPoint(point, elementTransform, canvas, out result);
			return result;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0001BE84 File Offset: 0x0001A084
		private static void PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 output)
		{
			RectTransformUtility.INTERNAL_CALL_PixelAdjustPoint(ref point, elementTransform, canvas, out output);
		}

		// Token: 0x06001DAE RID: 7598
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_PixelAdjustPoint(ref Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 output);

		// Token: 0x06001DAF RID: 7599 RVA: 0x0001BE90 File Offset: 0x0001A090
		public static Rect PixelAdjustRect(RectTransform rectTransform, Canvas canvas)
		{
			Rect result;
			RectTransformUtility.INTERNAL_CALL_PixelAdjustRect(rectTransform, canvas, out result);
			return result;
		}

		// Token: 0x06001DB0 RID: 7600
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_PixelAdjustRect(RectTransform rectTransform, Canvas canvas, out Rect value);

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		public static bool ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
			float distance;
			if (!plane.Raycast(ray, out distance))
			{
				return false;
			}
			worldPoint = ray.GetPoint(distance);
			return true;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0001BF0C File Offset: 0x0001A10C
		public static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
		{
			localPoint = Vector2.zero;
			Vector3 position;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out position))
			{
				localPoint = rect.InverseTransformPoint(position);
				return true;
			}
			return false;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0001BF48 File Offset: 0x0001A148
		public static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
		{
			if (cam != null)
			{
				return cam.ScreenPointToRay(screenPos);
			}
			Vector3 origin = screenPos;
			origin.z -= 100f;
			return new Ray(origin, Vector3.forward);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0001BF94 File Offset: 0x0001A194
		public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
		{
			if (cam == null)
			{
				return new Vector2(worldPoint.x, worldPoint.y);
			}
			return cam.WorldToScreenPoint(worldPoint);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
		public static Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
		{
			RectTransform[] componentsInChildren = child.GetComponentsInChildren<RectTransform>(false);
			if (componentsInChildren.Length > 0)
			{
				Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					componentsInChildren[i].GetWorldCorners(RectTransformUtility.s_Corners);
					for (int j = 0; j < 4; j++)
					{
						Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(RectTransformUtility.s_Corners[j]);
						vector = Vector3.Min(lhs, vector);
						vector2 = Vector3.Max(lhs, vector2);
					}
					i++;
				}
				Bounds result = new Bounds(vector, Vector3.zero);
				result.Encapsulate(vector2);
				return result;
			}
			return new Bounds(Vector3.zero, Vector3.zero);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
		public static Bounds CalculateRelativeRectTransformBounds(Transform trans)
		{
			return RectTransformUtility.CalculateRelativeRectTransformBounds(trans, trans);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		public static void FlipLayoutOnAxis(RectTransform rect, int axis, bool keepPositioning, bool recursive)
		{
			if (rect == null)
			{
				return;
			}
			if (recursive)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					RectTransform rectTransform = rect.GetChild(i) as RectTransform;
					if (rectTransform != null)
					{
						RectTransformUtility.FlipLayoutOnAxis(rectTransform, axis, false, true);
					}
				}
			}
			Vector2 pivot = rect.pivot;
			pivot[axis] = 1f - pivot[axis];
			rect.pivot = pivot;
			if (keepPositioning)
			{
				return;
			}
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = -anchoredPosition[axis];
			rect.anchoredPosition = anchoredPosition;
			Vector2 anchorMin = rect.anchorMin;
			Vector2 anchorMax = rect.anchorMax;
			float num = anchorMin[axis];
			anchorMin[axis] = 1f - anchorMax[axis];
			anchorMax[axis] = 1f - num;
			rect.anchorMin = anchorMin;
			rect.anchorMax = anchorMax;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0001C1B4 File Offset: 0x0001A3B4
		public static void FlipLayoutAxes(RectTransform rect, bool keepPositioning, bool recursive)
		{
			if (rect == null)
			{
				return;
			}
			if (recursive)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					RectTransform rectTransform = rect.GetChild(i) as RectTransform;
					if (rectTransform != null)
					{
						RectTransformUtility.FlipLayoutAxes(rectTransform, false, true);
					}
				}
			}
			rect.pivot = RectTransformUtility.GetTransposed(rect.pivot);
			rect.sizeDelta = RectTransformUtility.GetTransposed(rect.sizeDelta);
			if (keepPositioning)
			{
				return;
			}
			rect.anchoredPosition = RectTransformUtility.GetTransposed(rect.anchoredPosition);
			rect.anchorMin = RectTransformUtility.GetTransposed(rect.anchorMin);
			rect.anchorMax = RectTransformUtility.GetTransposed(rect.anchorMax);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0001C268 File Offset: 0x0001A468
		private static Vector2 GetTransposed(Vector2 input)
		{
			return new Vector2(input.y, input.x);
		}

		// Token: 0x0400060A RID: 1546
		private static Vector3[] s_Corners = new Vector3[4];
	}
}
