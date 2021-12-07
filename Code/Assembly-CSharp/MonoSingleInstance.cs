using System;
using UnityEngine;

// Token: 0x0200093F RID: 2367
public class MonoSingleInstance<T> : MonoBehaviour where T : MonoBehaviour, new()
{
	// Token: 0x1700083D RID: 2109
	// (get) Token: 0x06003444 RID: 13380 RVA: 0x000DBC20 File Offset: 0x000D9E20
	// (set) Token: 0x06003445 RID: 13381 RVA: 0x000DBC5F File Offset: 0x000D9E5F
	public static T Instance
	{
		get
		{
			if (!MonoSingleInstance<T>.m_initialize || !Application.isPlaying)
			{
				MonoSingleInstance<T>.m_initialize = true;
				MonoSingleInstance<T>.m_instance = (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
			}
			return MonoSingleInstance<T>.m_instance;
		}
		set
		{
			MonoSingleInstance<T>.m_instance = value;
		}
	}

	// Token: 0x06003446 RID: 13382 RVA: 0x000DBC67 File Offset: 0x000D9E67
	public void Awake()
	{
		MonoSingleInstance<T>.m_instance = (this as T);
		MonoSingleInstance<T>.m_initialize = true;
	}

	// Token: 0x04002F33 RID: 12083
	private static T m_instance;

	// Token: 0x04002F34 RID: 12084
	private static bool m_initialize;
}
