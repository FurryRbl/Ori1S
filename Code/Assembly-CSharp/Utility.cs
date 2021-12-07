using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class Utility
{
	// Token: 0x06000261 RID: 609 RVA: 0x0000A29A File Offset: 0x0000849A
	public static void DisableLate(MonoBehaviour target)
	{
		target.gameObject.SetActive(false);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000A2A8 File Offset: 0x000084A8
	private static IEnumerator DisableLater(MonoBehaviour target)
	{
		yield return new WaitForEndOfFrame();
		target.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000A2CA File Offset: 0x000084CA
	public static string LowercaseFirstLetter(string s)
	{
		return char.ToLower(s[0]) + s.Substring(1);
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000A2EC File Offset: 0x000084EC
	public static Rect RectFromBounds(Bounds bounds)
	{
		return new Rect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000A33C File Offset: 0x0000853C
	public static bool LineInBox(Rect rect, Vector3 origin, Vector3 delta)
	{
		if ((double)delta.magnitude > 0.5)
		{
			int num = Mathf.CeilToInt(delta.magnitude);
			for (int i = 0; i <= num; i++)
			{
				float d = (float)i / (float)num;
				if (rect.Contains(origin + delta * d))
				{
					return true;
				}
			}
			return false;
		}
		return rect.Contains(origin);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000A3AC File Offset: 0x000085AC
	public static Bounds BoundsFromPoints(Vector3 p1, Vector3 p2)
	{
		Bounds result = new Bounds(p1, Vector3.zero);
		result.Encapsulate(p2);
		return result;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000A3D0 File Offset: 0x000085D0
	public static Bounds BoundsFromPoints(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		Bounds result = new Bounds(p1, Vector3.zero);
		result.Encapsulate(p2);
		result.Encapsulate(p3);
		return result;
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000A3FC File Offset: 0x000085FC
	public static Bounds BoundsFromPoints(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
	{
		Bounds result = new Bounds(p1, Vector3.zero);
		result.Encapsulate(p2);
		result.Encapsulate(p3);
		result.Encapsulate(p4);
		return result;
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000A430 File Offset: 0x00008630
	public static Bounds BoundsFromTransform(Transform transform)
	{
		Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
		return Utility.BoundsFromPoints(localToWorldMatrix.MultiplyPoint3x4(new Vector3(0.5f, -0.5f)), localToWorldMatrix.MultiplyPoint3x4(new Vector3(0.5f, 0.5f)), localToWorldMatrix.MultiplyPoint3x4(new Vector3(-0.5f, -0.5f)), localToWorldMatrix.MultiplyPoint3x4(new Vector3(-0.5f, 0.5f)));
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000A4A4 File Offset: 0x000086A4
	public static float ColorDiff(Color a, Color b)
	{
		return Mathf.Max(Mathf.Max(Mathf.Abs(a.r - b.r), Mathf.Abs(a.g - b.g)), Mathf.Abs(a.b - b.b));
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000A4F8 File Offset: 0x000086F8
	public static Rect CombineRects(Rect a, Rect b)
	{
		return new Rect
		{
			xMin = Mathf.Min(a.xMin, b.xMin),
			yMin = Mathf.Min(a.yMin, b.yMin),
			xMax = Mathf.Max(a.xMax, b.xMax),
			yMax = Mathf.Max(a.yMax, b.yMax)
		};
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000A578 File Offset: 0x00008778
	public static Rect CombineRects(params Rect[] rects)
	{
		Rect rect = rects[0];
		foreach (Rect b in rects)
		{
			rect = Utility.CombineRects(rect, b);
		}
		return rect;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000A5C0 File Offset: 0x000087C0
	public static Rect CombineRects(List<Rect> rects)
	{
		Rect rect = rects[0];
		for (int i = 0; i < rects.Count; i++)
		{
			Rect b = rects[i];
			rect = Utility.CombineRects(rect, b);
		}
		return rect;
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000A5FD File Offset: 0x000087FD
	public static float Normalize(float x)
	{
		if (x == 0f)
		{
			return x;
		}
		return Mathf.Sign(x);
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000A612 File Offset: 0x00008812
	public static float Angle(float angle)
	{
		while (angle >= 360f)
		{
			angle -= 360f;
		}
		while (angle < 0f)
		{
			angle += 360f;
		}
		return angle;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000A647 File Offset: 0x00008847
	public static float AngleDifference(float value1, float value2)
	{
		return Mathf.Min(Mathf.Abs(value1 - value2), 360f - Mathf.Abs(value1 - value2));
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000A664 File Offset: 0x00008864
	public static float RotateTowards(float value, float target, float distance)
	{
		distance = Mathf.Min(distance, Utility.AngleDifference(value, target));
		if (Utility.AngleDifference(Utility.Angle(value + distance), target) < Utility.AngleDifference(Utility.Angle(value - distance), target))
		{
			return Utility.Angle(value + distance);
		}
		return Utility.Angle(value - distance);
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000A6B2 File Offset: 0x000088B2
	public static float MoveNumberTowards(float value, float target, float distance)
	{
		if (target > value)
		{
			return Mathf.Min(target, value + distance);
		}
		return Mathf.Max(target, value - distance);
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000A6CE File Offset: 0x000088CE
	public static float ClampedAdd(float value, float offset, float min, float max)
	{
		if (offset > 0f && value < max)
		{
			return Mathf.Min(max, value + offset);
		}
		if (offset < 0f && value > min)
		{
			return Mathf.Max(min, value + offset);
		}
		return value;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000A709 File Offset: 0x00008909
	public static float ClampedSubtract(float value, float offset, float min, float max)
	{
		if (value < min)
		{
			return Mathf.Min(min, value - offset);
		}
		if (value > max)
		{
			return Mathf.Max(max, value - offset);
		}
		return value;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000A72E File Offset: 0x0000892E
	public static Vector3 Rotate(Vector3 v, float angle)
	{
		return Quaternion.Euler(0f, 0f, angle) * v;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000A746 File Offset: 0x00008946
	public static Vector3 Unrotate(Vector3 v, float angle)
	{
		return Quaternion.Euler(0f, 0f, -angle) * v;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000A760 File Offset: 0x00008960
	public static string PathGoBack(string path)
	{
		int num = path.Length - 1;
		while (num > 0 && path[num] != '/')
		{
			num--;
		}
		return path.Remove(num);
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000A79C File Offset: 0x0000899C
	public static Vector3 DirectionToVector(Utility.MoveDirection direction)
	{
		switch (direction)
		{
		case Utility.MoveDirection.Left:
			return Vector3.left;
		case Utility.MoveDirection.Right:
			return Vector3.right;
		case Utility.MoveDirection.Up:
			return Vector3.up;
		case Utility.MoveDirection.Down:
			return Vector3.down;
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000A7E4 File Offset: 0x000089E4
	public static string NumberToString(int number, int digits)
	{
		string text = number.ToString();
		return text.PadLeft(digits, '0');
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000A804 File Offset: 0x00008A04
	public static int Wrap(int value, int min, int max)
	{
		if (value < min)
		{
			return value + (max - min);
		}
		if (value >= max)
		{
			return value - (max - min);
		}
		return value;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000A824 File Offset: 0x00008A24
	public static T GetComponentUpwards<T>(Transform transform) where T : Component
	{
		T component = transform.GetComponent<T>();
		if (component != null)
		{
			return component;
		}
		return (!(transform.parent == null)) ? Utility.GetComponentUpwards<T>(transform.parent) : ((T)((object)null));
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000A874 File Offset: 0x00008A74
	public static Bounds LocalHierarchyBoundingBox(GameObject gameObject)
	{
		MeshFilter[] componentsInChildren = gameObject.GetComponentsInChildren<MeshFilter>();
		if (componentsInChildren.Length == 0)
		{
			return default(Bounds);
		}
		Bounds result = default(Bounds);
		bool flag = true;
		foreach (MeshFilter meshFilter in componentsInChildren)
		{
			if (!(meshFilter.sharedMesh == null))
			{
				Matrix4x4 matrix = gameObject.transform.worldToLocalMatrix * meshFilter.transform.localToWorldMatrix;
				Bounds bounds = Utility.BoundsOfBounds(matrix, meshFilter.sharedMesh.bounds);
				if (flag)
				{
					result = bounds;
					flag = false;
				}
				else
				{
					result.Encapsulate(bounds);
				}
			}
		}
		return result;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000A928 File Offset: 0x00008B28
	public static Bounds BoundsOfBounds(Matrix4x4 matrix, Bounds bounds)
	{
		Vector3[] array = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 1f),
			new Vector3(1f, 0f, 0f),
			new Vector3(1f, 0f, 1f),
			new Vector3(1f, 1f, 0f),
			new Vector3(1f, 1f, 1f)
		};
		Bounds result = new Bounds(matrix.MultiplyPoint(Utility.LerpVector3(bounds.min, bounds.max, array[0])), Vector3.zero);
		foreach (Vector3 r in array)
		{
			result.Encapsulate(matrix.MultiplyPoint(Utility.LerpVector3(bounds.min, bounds.max, r)));
		}
		return result;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000AABC File Offset: 0x00008CBC
	public static Vector3 LerpVector3(Vector3 start, Vector3 end, Vector3 r)
	{
		return new Vector3(Mathf.Lerp(start.x, end.x, r.x), Mathf.Lerp(start.y, end.y, r.y), Mathf.Lerp(start.z, end.z, r.z));
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000AB1C File Offset: 0x00008D1C
	private static Bounds EncapsulateChildren(Transform trans, ref Bounds current, ref bool hasBounds)
	{
		Renderer component = trans.GetComponent<Renderer>();
		if (component != null && !(component is ParticleRenderer))
		{
			if (hasBounds)
			{
				current.Encapsulate(component.bounds);
			}
			else
			{
				current = component.bounds;
				hasBounds = true;
			}
		}
		for (int i = 0; i < trans.childCount; i++)
		{
			Transform child = trans.GetChild(i);
			Utility.EncapsulateChildren(child, ref current, ref hasBounds);
		}
		return current;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000AB9C File Offset: 0x00008D9C
	public static Bounds WorldSpaceHierarchyBoundingBox(GameObject gameObject)
	{
		Bounds bounds = default(Bounds);
		bool flag = false;
		return Utility.EncapsulateChildren(gameObject.transform, ref bounds, ref flag);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000ABC2 File Offset: 0x00008DC2
	public static float Round(float value, float grid)
	{
		if (grid == 0f)
		{
			return value;
		}
		return Mathf.Round(value / grid) * grid;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000ABDC File Offset: 0x00008DDC
	public static Rect Round(Rect rect, float grid)
	{
		rect.xMin = Utility.Round(rect.xMin, grid);
		rect.xMax = Utility.Round(rect.xMax, grid);
		rect.yMin = Utility.Round(rect.yMin, grid);
		rect.yMax = Utility.Round(rect.yMax, grid);
		return rect;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000AC3C File Offset: 0x00008E3C
	public static Vector3 Round(Vector3 vector, float grid)
	{
		return new Vector3(Utility.Round(vector.x, grid), Utility.Round(vector.y, grid), Utility.Round(vector.z, grid));
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000AC75 File Offset: 0x00008E75
	public static Vector3 Round(Vector3 vector)
	{
		return Utility.Round(vector, 1f);
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000AC82 File Offset: 0x00008E82
	public static Rect Round(Rect rect)
	{
		return Utility.Round(rect, 1f);
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0000AC8F File Offset: 0x00008E8F
	public static float Floor(float value, float grid)
	{
		if (grid == 0f)
		{
			return value;
		}
		return Mathf.Floor(value / grid) * grid;
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000ACA8 File Offset: 0x00008EA8
	public static float Ceil(float value, float grid)
	{
		if (grid == 0f)
		{
			return value;
		}
		return Mathf.Ceil(value / grid) * grid;
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000ACC1 File Offset: 0x00008EC1
	public static void DontAssociateWithAnyScene(GameObject go)
	{
	}

	// Token: 0x020008F8 RID: 2296
	public enum MoveDirection
	{
		// Token: 0x04002E19 RID: 11801
		Left,
		// Token: 0x04002E1A RID: 11802
		Right,
		// Token: 0x04002E1B RID: 11803
		Up,
		// Token: 0x04002E1C RID: 11804
		Down
	}
}
