using System;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public static class TransformUtility
{
	// Token: 0x0600118D RID: 4493 RVA: 0x00050D54 File Offset: 0x0004EF54
	public static void SetParentMaintainingLocalTransform(this Transform transform, Transform parent)
	{
		Vector3 localScale = transform.localScale;
		Vector3 localPosition = transform.localPosition;
		Quaternion localRotation = transform.localRotation;
		transform.parent = parent;
		transform.localScale = localScale;
		transform.localRotation = localRotation;
		transform.localPosition = localPosition;
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x00050D94 File Offset: 0x0004EF94
	public static void SetParentMaintainingLocalPosition(this Transform transform, Transform parent)
	{
		Vector3 localPosition = transform.localPosition;
		transform.parent = parent;
		transform.localPosition = localPosition;
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x00050DB8 File Offset: 0x0004EFB8
	public static void SetParentMaintainingLocalPositionAndRotation(this Transform transform, Transform parent)
	{
		Vector3 localPosition = transform.localPosition;
		Quaternion localRotation = transform.localRotation;
		transform.parent = parent;
		transform.localPosition = localPosition;
		transform.localRotation = localRotation;
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x00050DE8 File Offset: 0x0004EFE8
	public static void SetParentMaintainingRotationAndScale(this Transform transform, Transform parent)
	{
		Vector3 localScale = transform.localScale;
		Quaternion localRotation = transform.localRotation;
		transform.parent = parent;
		transform.localScale = localScale;
		transform.localRotation = localRotation;
	}
}
