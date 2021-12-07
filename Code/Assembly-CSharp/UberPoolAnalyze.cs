using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UWPCompat.Extensions;

// Token: 0x020006E8 RID: 1768
public static class UberPoolAnalyze
{
	// Token: 0x06002A2A RID: 10794 RVA: 0x000B534C File Offset: 0x000B354C
	private static FieldInfo[] GetFields(Type t)
	{
		FieldInfo[] fields;
		if (!UberPoolAnalyze.m_fieldOfTypes.TryGetValue(t, out fields))
		{
			fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			UberPoolAnalyze.m_fieldOfTypes.Add(t, fields);
		}
		return fields;
	}

	// Token: 0x06002A2B RID: 10795 RVA: 0x000B5384 File Offset: 0x000B3584
	private static bool FieldIsSafe(FieldInfo field)
	{
		bool flag = false;
		if (!UberPoolAnalyze.m_poolSafe.TryGetValue(field, out flag))
		{
			flag = Attribute.IsDefined(field, typeof(PooledSafeAttribute));
			UberPoolAnalyze.m_poolSafe[field] = flag;
		}
		return flag;
	}

	// Token: 0x06002A2C RID: 10796 RVA: 0x000B53C4 File Offset: 0x000B35C4
	private static bool TypeIsSafe(Type t)
	{
		bool flag = false;
		if (!UberPoolAnalyze.m_poolSafeType.TryGetValue(t, out flag))
		{
			flag = t.IsDefined(typeof(PooledSafeAttribute), false);
			UberPoolAnalyze.m_poolSafeType[t] = flag;
		}
		return flag;
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x000B5404 File Offset: 0x000B3604
	private static void CompareObject(object o1, object o2, string path, HashSet<object> visited, HashSet<GameObject> total, int depth)
	{
		if (depth > 10)
		{
			return;
		}
		if (path.EndsWith("m_Ptr") || path.EndsWith("_version"))
		{
			return;
		}
		if (o1 == null || o2 == null)
		{
			return;
		}
		if (o1.GetType() != o2.GetType())
		{
		}
		if (UberPoolAnalyze.TypeIsSafe(o1.GetType()))
		{
			return;
		}
		if (visited.Contains(o1))
		{
			return;
		}
		visited.Add(o1);
		if (o1.GetType().IsValueType())
		{
			bool flag = false;
			if (o1 is float)
			{
				if ((float)o1 - (float)o2 > Mathf.Epsilon)
				{
					flag = true;
				}
			}
			else if (!o1.Equals(o2))
			{
				flag = true;
			}
			if (flag)
			{
			}
		}
		else
		{
			FieldInfo[] fields = UberPoolAnalyze.GetFields(o1.GetType());
			foreach (FieldInfo fieldInfo in fields)
			{
				if (!UberPoolAnalyze.FieldIsSafe(fieldInfo))
				{
					object value = fieldInfo.GetValue(o1);
					object value2 = fieldInfo.GetValue(o2);
					string text = path + "/" + fieldInfo.Name;
					if (value == null || value2 == null || value.Equals(null) || value2.Equals(null))
					{
						if ((value != null && !value.Equals(null) && value2 != null && !value2.Equals(null)) || (value2 != null && !value2.Equals(null) && value != null && !value.Equals(null)))
						{
							Debug.LogError("Object value mismatch: " + text);
							Debug.LogError(value);
							Debug.LogError(value2);
						}
					}
					else
					{
						GameObject gameObject = null;
						Component component = value2 as Component;
						if (component != null && !(component is ParticleSystem))
						{
							gameObject = component.gameObject;
						}
						else
						{
							GameObject gameObject2 = value2 as GameObject;
							if (gameObject2 != null)
							{
								gameObject = gameObject2;
							}
						}
						if (!(gameObject != null) || total.Contains(gameObject))
						{
							if (fieldInfo.FieldType.IsArray)
							{
								Array array2 = (Array)value;
								Array array3 = (Array)value2;
								if (array2.Length == array3.Length)
								{
									for (int j = 0; j < array2.Length; j++)
									{
										UberPoolAnalyze.CompareObject(array2.GetValue(j), array3.GetValue(j), text, visited, total, depth + 1);
									}
								}
							}
							else if (value is IList)
							{
								IList list = (IList)value;
								IList list2 = (IList)value2;
								if (list.Count == list2.Count)
								{
									for (int k = 0; k < list.Count; k++)
									{
										UberPoolAnalyze.CompareObject(list[k], list2[k], text, visited, total, depth + 1);
									}
								}
							}
							else
							{
								UberPoolAnalyze.CompareObject(value, value2, text, visited, total, depth + 1);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002A2E RID: 10798 RVA: 0x000B5738 File Offset: 0x000B3938
	private static void CompareGameObject(GameObject go1, GameObject go2, HashSet<object> visited, HashSet<GameObject> total, string path)
	{
		Component[] array = go1.GetComponents<Component>().ToArray<Component>();
		Component[] array2 = go2.GetComponents<Component>().ToArray<Component>();
		string arg = path + "/" + go1.name;
		if (array.Length == array2.Length)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i] is UberShaderComponent) && !(array[i] is UberShaderModifier) && !(array[i] is UberShaderBlock))
				{
					if (array[i].GetType() != array2[i].GetType())
					{
						Debug.LogError("Differently ordered components on " + go1.name + " and " + go2.name);
					}
					else
					{
						UberPoolAnalyze.CompareObject(array[i], array2[i], arg + "/" + array[i].GetType(), visited, total, 1);
					}
				}
			}
		}
	}

	// Token: 0x06002A2F RID: 10799 RVA: 0x000B581C File Offset: 0x000B3A1C
	public static void CompareFullHierarchies(GameObject a, GameObject b, HashSet<object> visited, string path)
	{
		if (a.transform.childCount != b.transform.childCount)
		{
			return;
		}
		if (a.activeInHierarchy != b.activeInHierarchy)
		{
		}
		HashSet<GameObject> total = new HashSet<GameObject>();
		UberPoolAnalyze.AddRecursive(b, total);
		UberPoolAnalyze.CompareGameObject(a, b, visited, total, path);
		int childCount = a.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			UberPoolAnalyze.CompareFullHierarchies(a.transform.GetChild(i).gameObject, b.transform.GetChild(i).gameObject, visited, path + "/" + a.transform.GetChild(i).name);
		}
	}

	// Token: 0x06002A30 RID: 10800 RVA: 0x000B58D0 File Offset: 0x000B3AD0
	private static void AddRecursive(GameObject gameObject, HashSet<GameObject> total)
	{
		total.Add(gameObject);
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			UberPoolAnalyze.AddRecursive(transform.gameObject, total);
		}
	}

	// Token: 0x06002A31 RID: 10801 RVA: 0x000B5940 File Offset: 0x000B3B40
	public static void Analyze(GameObject pooledObj, GameObject original)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(original, pooledObj.transform.position, pooledObj.transform.rotation) as GameObject;
		UberPoolAnalyze.CompareFullHierarchies(pooledObj, gameObject, new HashSet<object>(), original.name);
		UnityEngine.Object.Destroy(gameObject);
	}

	// Token: 0x040025A0 RID: 9632
	private static Dictionary<Type, FieldInfo[]> m_fieldOfTypes = new Dictionary<Type, FieldInfo[]>();

	// Token: 0x040025A1 RID: 9633
	private static Dictionary<FieldInfo, bool> m_poolSafe = new Dictionary<FieldInfo, bool>();

	// Token: 0x040025A2 RID: 9634
	private static Dictionary<Type, bool> m_poolSafeType = new Dictionary<Type, bool>();
}
