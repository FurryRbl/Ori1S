using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000090 RID: 144
public static class GameObjectExtender
{
	// Token: 0x06000611 RID: 1553 RVA: 0x00017EBC File Offset: 0x000160BC
	public static bool HasComponent<T>(this GameObject gameObject) where T : class
	{
		return gameObject.FindComponent<T>() != null;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00017ECF File Offset: 0x000160CF
	public static T FindComponent<T>(this GameObject gameObject) where T : class
	{
		return gameObject.GetComponent(typeof(T)) as T;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00017EF0 File Offset: 0x000160F0
	public static void GetComponents<T>(this GameObject gameObject, List<T> list) where T : class
	{
		gameObject.GetComponents<T>(list);
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00017EF9 File Offset: 0x000160F9
	public static void GetComponentsInChildren<T>(this GameObject gameObject, List<T> list) where T : class
	{
		gameObject.GetComponentsInChildren<T>(list);
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00017F02 File Offset: 0x00016102
	public static void GetComponentsInChildren<T>(this GameObject gameObject, bool inactive, List<T> list) where T : class
	{
		gameObject.GetComponentsInChildren<T>(inactive, list);
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00017F0C File Offset: 0x0001610C
	public static T FindComponentUpwards<T>(this Transform transform) where T : class
	{
		Type typeFromHandle = typeof(T);
		while (transform)
		{
			Component component = transform.GetComponent(typeFromHandle);
			if (component)
			{
				return component as T;
			}
			transform = transform.parent;
		}
		return (T)((object)null);
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00017F66 File Offset: 0x00016166
	public static Component[] FindComponents<T>(this GameObject gameObject) where T : class
	{
		return gameObject.GetComponents(typeof(T));
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x00017F78 File Offset: 0x00016178
	public static T FindComponentInChildren<T>(this GameObject gameObject) where T : class
	{
		return gameObject.GetComponentInChildren(typeof(T)) as T;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x00017F99 File Offset: 0x00016199
	public static Component[] FindComponentsInChildren<T>(this GameObject gameObject) where T : class
	{
		return gameObject.GetComponentsInChildren(typeof(T));
	}
}
