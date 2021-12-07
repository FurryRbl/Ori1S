using System;
using Game;
using UnityEngine;

// Token: 0x020003E2 RID: 994
public class CameraScrollLockConstraint : MonoBehaviour
{
	// Token: 0x06001B2A RID: 6954 RVA: 0x00074685 File Offset: 0x00072885
	public void Awake()
	{
		CameraScrollLockConstraint.Current = this;
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x00074690 File Offset: 0x00072890
	public static Bounds BoundsFromPoints(Vector3[] pts)
	{
		Bounds result = new Bounds
		{
			center = pts[0]
		};
		foreach (Vector3 point in pts)
		{
			result.Encapsulate(point);
		}
		return result;
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x000746E8 File Offset: 0x000728E8
	public bool HasPassedThroughScrollLock(Vector3 oldPosition, Vector3 position, out CameraScrollLock scrollLockPassedThrough)
	{
		for (int i = 0; i < ScrollLocks.All.Count; i++)
		{
			CameraScrollLock cameraScrollLock = ScrollLocks.All[i];
			if (cameraScrollLock.gameObject.activeInHierarchy)
			{
				Vector3 scrollCenter = cameraScrollLock.ScrollCenter;
				Vector3 halfScrollSize = cameraScrollLock.HalfScrollSize;
				if (cameraScrollLock.ScrollType == CameraScrollLock.Type.Horizontal)
				{
					if (scrollCenter.y + halfScrollSize.y >= position.y && scrollCenter.y - halfScrollSize.y <= position.y)
					{
						if (oldPosition.x < scrollCenter.x && position.x > scrollCenter.x)
						{
							scrollLockPassedThrough = cameraScrollLock;
							return true;
						}
						if (oldPosition.x > scrollCenter.x && position.x < scrollCenter.x)
						{
							scrollLockPassedThrough = cameraScrollLock;
							return true;
						}
					}
				}
				else if (scrollCenter.x + halfScrollSize.x >= position.x && scrollCenter.x - halfScrollSize.x <= position.x)
				{
					if (oldPosition.y < scrollCenter.y && position.y > scrollCenter.y)
					{
						scrollLockPassedThrough = cameraScrollLock;
						return true;
					}
					if (oldPosition.y > scrollCenter.y && position.y < scrollCenter.y)
					{
						scrollLockPassedThrough = cameraScrollLock;
						return true;
					}
				}
			}
		}
		scrollLockPassedThrough = null;
		return false;
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x00074878 File Offset: 0x00072A78
	public static Bounds CalculateCameraBounds(Camera camera)
	{
		Plane plane = new Plane(Vector3.forward, Vector3.zero);
		CameraScrollLockConstraint.s_rays[0] = camera.ViewportPointToRay(new Vector3(0f, 0f));
		CameraScrollLockConstraint.s_rays[1] = camera.ViewportPointToRay(new Vector3(0f, 1f));
		CameraScrollLockConstraint.s_rays[2] = camera.ViewportPointToRay(new Vector3(1f, 0f));
		CameraScrollLockConstraint.s_rays[3] = camera.ViewportPointToRay(new Vector3(1f, 1f));
		CameraScrollLockConstraint.s_rays[4] = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		for (int i = 0; i < 5; i++)
		{
			Ray ray = CameraScrollLockConstraint.s_rays[i];
			float d;
			plane.Raycast(ray, out d);
			CameraScrollLockConstraint.s_pts[i] = ray.origin + ray.direction * d;
		}
		Bounds result = CameraScrollLockConstraint.BoundsFromPoints(CameraScrollLockConstraint.s_pts);
		result.extents += new Vector3(0f, 0f, 0.5f);
		return result;
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x000749DC File Offset: 0x00072BDC
	public Vector3 CalculateConstraintOffset(Vector3 position)
	{
		float num = -UI.Cameras.Current.OffsetController.transform.position.z;
		float num2 = Mathf.Tan(0.5235988f) * num * 2f;
		float a = 1.7777778f * num2;
		float b = AspectRatioManager.AspectRatio * num2;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		Vector3 zero4 = Vector3.zero;
		Vector3 vector = UI.Cameras.Current.OffsetController.Offset;
		for (int i = 0; i < ScrollLocks.All.Count; i++)
		{
			CameraScrollLock cameraScrollLock = ScrollLocks.All[i];
			if (cameraScrollLock.enabled)
			{
				if (cameraScrollLock.gameObject.activeInHierarchy)
				{
					Vector3 scrollCenter = cameraScrollLock.ScrollCenter;
					Vector3 halfScrollSize = cameraScrollLock.HalfScrollSize;
					AnimationCurve scrollLockSmooth = this.ScrollLockSmooth;
					if (cameraScrollLock.UseScrollLockSmooth)
					{
						scrollLockSmooth = cameraScrollLock.ScrollLockSmooth;
					}
					float num3 = Mathf.Lerp(a, b, cameraScrollLock.WideScreenAdjustment);
					float num4 = num2;
					if (cameraScrollLock.ScrollType == CameraScrollLock.Type.Horizontal)
					{
						if (scrollCenter.y + halfScrollSize.y >= position.y && scrollCenter.y - halfScrollSize.y <= position.y)
						{
							if (position.x < scrollCenter.x && position.x > scrollCenter.x - num3 * 2f && cameraScrollLock.LockMode != CameraScrollLock.ScrollLockMode.LeftOrBottom)
							{
								float a2 = 1f - Mathf.Clamp01(-(position.x + vector.x - scrollCenter.x) / num3 * 2f);
								float num5 = 1f - Mathf.Clamp01(-(position.x + vector.x - scrollCenter.x) / num3 * 1f);
								float t = scrollLockSmooth.Evaluate(Mathf.InverseLerp(0f, 0.5f, num5));
								num5 *= num5;
								float b2 = -num3 * 0.5f * Mathf.Lerp(a2, num5, t);
								zero4.x = Mathf.Min(zero4.x, b2);
							}
							if (position.x > scrollCenter.x && position.x < scrollCenter.x + num3 * 2f && cameraScrollLock.LockMode != CameraScrollLock.ScrollLockMode.RightOrTop)
							{
								float a3 = 1f - Mathf.Clamp01((position.x + vector.x - scrollCenter.x) / num3 * 2f);
								float num6 = 1f - Mathf.Clamp01((position.x + vector.x - scrollCenter.x) / num3 * 1f);
								float t2 = scrollLockSmooth.Evaluate(Mathf.InverseLerp(0f, 0.5f, num6));
								num6 *= num6;
								float b3 = num3 * 0.5f * Mathf.Lerp(a3, num6, t2);
								zero3.x = Mathf.Max(zero3.x, b3);
							}
						}
					}
					else if (scrollCenter.x + halfScrollSize.x >= position.x && scrollCenter.x - halfScrollSize.x <= position.x)
					{
						if (position.y < scrollCenter.y && position.y > scrollCenter.y - num4 * 2f && cameraScrollLock.LockMode != CameraScrollLock.ScrollLockMode.LeftOrBottom)
						{
							float a4 = 1f - Mathf.Clamp01(-(position.y + vector.y - scrollCenter.y) / num4 * 2f);
							float num7 = 1f - Mathf.Clamp01(-(position.y + vector.y - scrollCenter.y) / num4 * 1f);
							float t3 = scrollLockSmooth.Evaluate(Mathf.InverseLerp(0f, 0.5f, num7));
							num7 *= num7;
							float b4 = -num4 * 0.5f * Mathf.Lerp(a4, num7, t3);
							zero4.y = Mathf.Min(zero4.y, b4);
						}
						if (position.y > scrollCenter.y && position.y < scrollCenter.y + num4 * 2f && cameraScrollLock.LockMode != CameraScrollLock.ScrollLockMode.RightOrTop)
						{
							float a5 = 1f - Mathf.Clamp01((position.y + vector.y - scrollCenter.y) / num4 * 2f);
							float num8 = 1f - Mathf.Clamp01((position.y + vector.y - scrollCenter.y) / num4 * 1f);
							float t4 = scrollLockSmooth.Evaluate(Mathf.InverseLerp(0f, 0.5f, num8));
							num8 *= num8;
							float b5 = num4 * 0.5f * Mathf.Lerp(a5, num8, t4);
							zero3.y = Mathf.Max(zero3.y, b5);
						}
					}
				}
			}
		}
		zero.x = zero3.x + zero4.x;
		zero.y = zero3.y + zero4.y;
		return zero2 + new Vector3(zero.x, zero.y);
	}

	// Token: 0x040017A8 RID: 6056
	public static CameraScrollLockConstraint Current;

	// Token: 0x040017A9 RID: 6057
	public AnimationCurve ScrollLockSmooth;

	// Token: 0x040017AA RID: 6058
	public float SmoothDistance;

	// Token: 0x040017AB RID: 6059
	private static Ray[] s_rays = new Ray[5];

	// Token: 0x040017AC RID: 6060
	private static Vector3[] s_pts = new Vector3[5];
}
