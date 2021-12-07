using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public static class ObjectExtender
{
	// Token: 0x06000C6C RID: 3180 RVA: 0x00038C22 File Offset: 0x00036E22
	public static T GetComponentInParents<T>(this GameObject gameObject) where T : class
	{
		return ObjectExtender.GetComponentInParents<T>(gameObject.transform);
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00038C2F File Offset: 0x00036E2F
	public static T GetComponentInParents<T>(this Component component) where T : class
	{
		return ObjectExtender.GetComponentInParents<T>(component.transform);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00038C3C File Offset: 0x00036E3C
	public static T GetComponentInParents<T>(Transform transform) where T : class
	{
		T t = transform.gameObject.FindComponent<T>();
		if (t != null)
		{
			return t;
		}
		return (!(transform.parent == null)) ? ObjectExtender.GetComponentInParents<T>(transform.parent) : ((T)((object)null));
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x00038C89 File Offset: 0x00036E89
	public static T GetComponentInChildrenAndParents<T>(this GameObject gameObject) where T : Component
	{
		return ObjectExtender.GetComponentInChildrenAndParents<T>(gameObject.transform);
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x00038C96 File Offset: 0x00036E96
	public static T GetComponentInChildrenAndParents<T>(this Component component) where T : Component
	{
		return ObjectExtender.GetComponentInChildrenAndParents<T>(component.transform);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00038CA4 File Offset: 0x00036EA4
	public static T GetComponentInChildrenAndParents<T>(Transform transform) where T : Component
	{
		T componentInChildren = transform.GetComponentInChildren<T>();
		if (componentInChildren != null)
		{
			return componentInChildren;
		}
		return (!(transform.parent == null)) ? ObjectExtender.GetComponentInParents<T>(transform.parent) : ((T)((object)null));
	}
}
