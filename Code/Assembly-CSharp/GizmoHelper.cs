using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public static class GizmoHelper
{
	// Token: 0x0600104B RID: 4171 RVA: 0x0004A6D8 File Offset: 0x000488D8
	public static void DrawTextFilled(Transform transform, string title, Color fillColor, Color outlineColor, bool bound = false)
	{
		Vector3 position = transform.position;
		Vector3 size = transform.lossyScale;
		if (bound)
		{
			Bounds bounds = Utility.BoundsFromTransform(transform);
			position = bounds.center;
			size = bounds.size;
		}
		GizmoHelper.SelectableBox(position, size);
		GizmoHelper.DrawRectangle(position, size, fillColor, outlineColor);
		GizmoHelper.DrawCenteredText(title, position, GizmoHelper.CenteredWhiteBoldText, GizmoHelper.ShouldTextBeSideways(size));
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x0004A733 File Offset: 0x00048933
	public static void DrawTextFilled(Transform transform, string title, bool bounds = false)
	{
		GizmoHelper.DrawTextFilled(transform, title, GizmoHelper.RectangleFillColor, GizmoHelper.RectangleOutlineColor, bounds);
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x0004A748 File Offset: 0x00048948
	public static void DrawTextNoFill(Transform transform, string title, bool bounds = false)
	{
		GizmoHelper.DrawTextFilled(transform, title, new Color(0f, 0f, 0f, 0f), GizmoHelper.RectangleOutlineColor, bounds);
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0004A77C File Offset: 0x0004897C
	public static void DrawSelectedTextFilled(Transform transform, string title, Color fillColor, Color outlineColor, bool bound)
	{
		Vector3 position = transform.position;
		Vector3 size = transform.lossyScale;
		if (bound)
		{
			Bounds bounds = Utility.BoundsFromTransform(transform);
			position = bounds.center;
			size = bounds.size;
		}
		GizmoHelper.SelectableBox(position, size);
		GizmoHelper.DrawRectangle(position, size, fillColor, outlineColor);
		GizmoHelper.DrawCenteredText(title, position, GizmoHelper.CenteredWhiteBoldText, GizmoHelper.ShouldTextBeSideways(size));
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0004A7D7 File Offset: 0x000489D7
	public static void DrawSelectedTextFilled(Transform transform, string title, Color fillColor, Color outlineColor)
	{
		GizmoHelper.DrawSelectedTextFilled(transform, title, fillColor, outlineColor);
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0004A7E2 File Offset: 0x000489E2
	public static void DrawSelectedTextFilled(Transform transform, string title, bool bounds = false)
	{
		GizmoHelper.DrawSelectedTextFilled(transform, title, GizmoHelper.RectangleFillColor, GizmoHelper.RectangleOutlineColor, bounds);
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0004A7F8 File Offset: 0x000489F8
	public static void SelectableBox(Vector3 position, Vector3 size)
	{
		Color color = Gizmos.color;
		Gizmos.color = new Color(0f, 0f, 0f, 0f);
		Gizmos.DrawCube(position, size);
		Gizmos.color = Color.white;
		Gizmos.color = color;
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x0004A840 File Offset: 0x00048A40
	public static void DrawRectangle(Vector3 position, Vector3 size, Color fill, Color outline)
	{
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x0004A842 File Offset: 0x00048A42
	public static void DrawOutline(Vector3 position, Vector3 size, Color outline)
	{
		GizmoHelper.DrawRectangle(position, size, GizmoHelper.m_color, outline);
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06001054 RID: 4180 RVA: 0x0004A851 File Offset: 0x00048A51
	public static GUIStyle CenteredWhiteBoldText
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0004A854 File Offset: 0x00048A54
	public static void DrawCenteredText(string text, Vector3 position, GUIStyle style, bool sideways)
	{
		GizmoHelper.DrawCenteredText(new GUIContent(text), position, style, sideways);
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0004A864 File Offset: 0x00048A64
	public static void DrawCenteredText(GUIContent content, Vector3 position, GUIStyle style, bool sideways)
	{
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0004A866 File Offset: 0x00048A66
	public static void DrawLine(Vector3 start, Vector3 end, Color fill)
	{
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x0004A868 File Offset: 0x00048A68
	public static bool ShouldTextBeSideways(Vector3 size)
	{
		return Mathf.Abs(size.y) > Mathf.Abs(size.x);
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0004A884 File Offset: 0x00048A84
	public static bool IsOnCamera(Vector3 position, Vector3 scale)
	{
		if (GizmoHelper.m_editorCamera == null)
		{
			GameObject gameObject = GameObject.Find("SceneCamera");
			if (!gameObject)
			{
				return true;
			}
			GizmoHelper.m_editorCamera = gameObject.GetComponent<Camera>();
		}
		if (Vector3.Distance(GizmoHelper.m_editorCamera.transform.position, GizmoHelper.m_lastCameraPosition) > 0.4f)
		{
			GizmoHelper.m_transformsCache.Clear();
		}
		GizmoHelper.m_lastCameraPosition = GizmoHelper.m_editorCamera.transform.position;
		Bounds bounds = new Bounds(position, scale);
		if (GizmoHelper.m_transformsCache.ContainsKey(bounds))
		{
			return GizmoHelper.m_transformsCache[bounds];
		}
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(GizmoHelper.m_editorCamera);
		bounds.extents = new Vector3(Mathf.Abs(bounds.extents.x), Mathf.Abs(bounds.extents.y), Mathf.Abs(bounds.extents.z));
		GizmoHelper.m_transformsCache[bounds] = GeometryUtility.TestPlanesAABB(planes, bounds);
		return GizmoHelper.m_transformsCache[bounds];
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0004A99C File Offset: 0x00048B9C
	public static Vector2 GetCameraDistance(Transform transform)
	{
		if (GizmoHelper.m_editorCamera == null || GizmoHelper.m_previousCameraPosition != GizmoHelper.m_lastCameraPosition)
		{
			Vector3 a = GizmoHelper.ScreenToWorld(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), transform);
			Vector3 world = a - Vector2.one * 0.5f;
			Vector3 world2 = a + Vector2.one * 0.5f;
			Vector2 previousDelta = GizmoHelper.WorldToScreen(world) - GizmoHelper.WorldToScreen(world2);
			GizmoHelper.m_previousDelta = previousDelta;
			GizmoHelper.m_previousCameraPosition = GizmoHelper.m_lastCameraPosition;
		}
		return GizmoHelper.m_previousDelta;
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x0004AA46 File Offset: 0x00048C46
	private static Vector3 ScreenToWorld(Vector2 screen, Transform transform)
	{
		return Vector3.zero;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x0004AA4D File Offset: 0x00048C4D
	private static Vector2 WorldToScreen(Vector3 world)
	{
		return Vector3.zero;
	}

	// Token: 0x04000D8A RID: 3466
	public static Color RectangleFillColor = new Color(0f, 0f, 0.35f, 0.35f);

	// Token: 0x04000D8B RID: 3467
	public static Color RectangleOutlineColor = new Color(1f, 1f, 1f, 0.35f);

	// Token: 0x04000D8C RID: 3468
	public static Color RectangleSelectedFillColor = new Color(0f, 0f, 0.35f, 0.5f);

	// Token: 0x04000D8D RID: 3469
	public static Color RectangleSelectedOutlineColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04000D8E RID: 3470
	private static Color m_color = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000D8F RID: 3471
	private static GUIStyle m_centeredWhiteBoldText;

	// Token: 0x04000D90 RID: 3472
	private static Dictionary<Bounds, bool> m_transformsCache = new Dictionary<Bounds, bool>();

	// Token: 0x04000D91 RID: 3473
	private static Camera m_editorCamera = null;

	// Token: 0x04000D92 RID: 3474
	private static Vector3 m_lastCameraPosition = Vector3.zero;

	// Token: 0x04000D93 RID: 3475
	private static Vector2 m_previousDelta = Vector2.zero;

	// Token: 0x04000D94 RID: 3476
	private static Vector3 m_previousCameraPosition = Vector3.zero;
}
