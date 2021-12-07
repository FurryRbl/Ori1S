using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020004A5 RID: 1189
public class DestroyManager
{
	// Token: 0x06002076 RID: 8310 RVA: 0x0008D53C File Offset: 0x0008B73C
	public void DestroyResourcesAsync(List<UnityEngine.Object> resources)
	{
		int count = resources.Count;
		for (int i = 0; i < count; i++)
		{
			UnityEngine.Object exists = resources[i];
			if (exists)
			{
				this.m_destroyResources.Push(resources[i]);
			}
		}
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x0008D588 File Offset: 0x0008B788
	public void DestroyGameObjectAsync(GameObject target)
	{
		this.AddTransform(target.transform);
		if (target.activeSelf)
		{
			target.SetActive(false);
		}
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x0008D5B4 File Offset: 0x0008B7B4
	public void UnloadResourcesAsync(List<UnityEngine.Object> runtimeResources)
	{
		for (int i = 0; i < runtimeResources.Count; i++)
		{
			this.m_unloadResources.Push(runtimeResources[i]);
		}
	}

	// Token: 0x06002079 RID: 8313 RVA: 0x0008D5EC File Offset: 0x0008B7EC
	private void AddTransform(Transform transform)
	{
		this.m_destroy.Push(transform);
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.AddTransform(transform.GetChild(i));
		}
	}

	// Token: 0x1700058B RID: 1419
	// (get) Token: 0x0600207A RID: 8314 RVA: 0x0008D62B File Offset: 0x0008B82B
	public bool IsDestroying
	{
		get
		{
			return this.m_destroy.Count > 0;
		}
	}

	// Token: 0x0600207B RID: 8315 RVA: 0x0008D63C File Offset: 0x0008B83C
	public void DestroyAll()
	{
		while (this.m_destroy.Count > 0)
		{
			Transform transform = this.m_destroy.Pop();
			if (transform)
			{
				InstantiateUtility.Destroy(transform.gameObject);
			}
		}
		Scenes.Manager.ReleaseUnusedResources();
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x0008D68C File Offset: 0x0008B88C
	public void Update()
	{
		if (this.m_destroy.Count == 0 && this.m_destroyResources.Count == 0)
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		int num = 0;
		while (Time.realtimeSinceStartup - realtimeSinceStartup < 0.001f && num < 50)
		{
			num++;
			if (this.m_destroy.Count > 0)
			{
				Transform transform = this.m_destroy.Pop();
				if (transform)
				{
					InstantiateUtility.Destroy(transform.gameObject);
				}
			}
			if (this.m_destroyResources.Count > 0)
			{
				UnityEngine.Object @object = this.m_destroyResources.Pop();
				if (@object)
				{
					UnityEngine.Object.Destroy(@object);
				}
			}
		}
		if (this.m_destroy.Count == 0 && this.m_destroyResources.Count == 0)
		{
			Scenes.Manager.ReleaseUnusedResources();
		}
	}

	// Token: 0x04001B9E RID: 7070
	private readonly Stack<Transform> m_destroy = new Stack<Transform>(4096);

	// Token: 0x04001B9F RID: 7071
	private readonly Stack<UnityEngine.Object> m_destroyResources = new Stack<UnityEngine.Object>(4096);

	// Token: 0x04001BA0 RID: 7072
	private readonly Stack<UnityEngine.Object> m_unloadResources = new Stack<UnityEngine.Object>(4096);
}
