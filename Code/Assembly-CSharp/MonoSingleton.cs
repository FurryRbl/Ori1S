using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour, new()
{
	// Token: 0x060009E9 RID: 2537 RVA: 0x0002B4DC File Offset: 0x000296DC
	public void Awake()
	{
		if (MonoSingleton<T>.m_instance)
		{
			if (MonoSingleton<T>.m_instance != this)
			{
				InstantiateUtility.Destroy(base.gameObject);
			}
		}
		else
		{
			MonoSingleton<T>.m_instance = (this as T);
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002B538 File Offset: 0x00029738
	public static T Instance
	{
		get
		{
			if (MonoSingleton<T>.m_instance == null)
			{
				MonoSingleton<T>.m_instance = (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
				if (GameController.IsClosing)
				{
					return MonoSingleton<T>.m_instance;
				}
				if (MonoSingleton<T>.m_instance == null)
				{
					GameObject gameObject = new GameObject(typeof(T).ToString());
					MonoSingleton<T>.m_instance = (gameObject.AddComponent(typeof(T)) as T);
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
				}
				else
				{
					MonoSingleton<T>.m_instance.transform.parent = null;
					UnityEngine.Object.DontDestroyOnLoad(MonoSingleton<T>.m_instance.gameObject);
				}
			}
			return MonoSingleton<T>.m_instance;
		}
	}

	// Token: 0x04000835 RID: 2101
	protected static T m_instance;
}
