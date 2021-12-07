using System;
using System.Collections.Generic;
using UnityEngine;
using UWPCompat.Extensions;

// Token: 0x02000952 RID: 2386
public class CacheComponent : MonoBehaviour
{
	// Token: 0x06003484 RID: 13444 RVA: 0x000DC760 File Offset: 0x000DA960
	public void Start()
	{
		foreach (Component component in base.GetComponents<Component>())
		{
			foreach (Type key in CacheComponent.FindAllClasses(component.GetType()))
			{
				HashSet<Component> hashSet;
				if (!this.m_cachedComponents.TryGetValue(key, out hashSet))
				{
					hashSet = new HashSet<Component>();
					this.m_cachedComponents.Add(key, hashSet);
				}
				hashSet.Add(component);
			}
		}
		foreach (Component component2 in base.GetComponentsInChildren<Component>())
		{
			foreach (Type key2 in CacheComponent.FindAllClasses(component2.GetType()))
			{
				HashSet<Component> hashSet2;
				if (!this.m_cachedComponents.TryGetValue(key2, out hashSet2))
				{
					hashSet2 = new HashSet<Component>();
					this.m_cachedComponentsInChildren.Add(key2, hashSet2);
				}
				hashSet2.Add(component2);
			}
		}
	}

	// Token: 0x06003485 RID: 13445 RVA: 0x000DC8B4 File Offset: 0x000DAAB4
	public IEnumerable<T> Find<T>() where T : class
	{
		HashSet<Component> hashSet;
		if (this.m_cachedComponents.TryGetValue(typeof(T), out hashSet))
		{
			foreach (Component component in hashSet)
			{
				yield return component as T;
			}
		}
		yield break;
	}

	// Token: 0x06003486 RID: 13446 RVA: 0x000DC8D8 File Offset: 0x000DAAD8
	public IEnumerable<T> FindInChildren<T>() where T : class
	{
		HashSet<Component> hashSet;
		if (this.m_cachedComponentsInChildren.TryGetValue(typeof(T), out hashSet))
		{
			foreach (Component component in hashSet)
			{
				yield return component as T;
			}
		}
		yield break;
	}

	// Token: 0x06003487 RID: 13447 RVA: 0x000DC8FC File Offset: 0x000DAAFC
	public static IEnumerable<Type> FindAllClasses(Type type)
	{
		while (type != null)
		{
			yield return type;
			foreach (Type i in type.GetInterfaces())
			{
				yield return i;
			}
			type = type.BaseType();
		}
		yield break;
	}

	// Token: 0x04002F5E RID: 12126
	private readonly Dictionary<Type, HashSet<Component>> m_cachedComponents = new Dictionary<Type, HashSet<Component>>();

	// Token: 0x04002F5F RID: 12127
	private readonly Dictionary<Type, HashSet<Component>> m_cachedComponentsInChildren = new Dictionary<Type, HashSet<Component>>();
}
