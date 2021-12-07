using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006EB RID: 1771
public class UberPoolGroup : MonoBehaviour
{
	// Token: 0x06002A5A RID: 10842 RVA: 0x000B5FB0 File Offset: 0x000B41B0
	private void Awake()
	{
	}

	// Token: 0x06002A5B RID: 10843 RVA: 0x000B5FB2 File Offset: 0x000B41B2
	public void Init(string poolName, UberPoolSettings settings)
	{
		this.m_setting = settings;
		this.m_poolName = poolName;
		base.name = "uberPoolFor " + poolName;
	}

	// Token: 0x06002A5C RID: 10844 RVA: 0x000B5FD4 File Offset: 0x000B41D4
	public void PrewarmInstance(GameObject prefab, Dictionary<int, UberPoolItem> entries)
	{
		GameObject newObject = this.GetNewObject(prefab, Vector3.zero, Quaternion.identity);
		UberPoolItem uberPoolItem = this.CreatePoolItemForGO(newObject);
		uberPoolItem.AddEntries(entries);
		newObject.SetActive(false);
		uberPoolItem.SetExplicitDestroy(true);
		this.m_minNumber++;
	}

	// Token: 0x06002A5D RID: 10845 RVA: 0x000B6020 File Offset: 0x000B4220
	public UberPoolItem RequestObject(GameObject prototype, Vector3 position, Quaternion rotation, out bool newObject)
	{
		for (int i = 0; i < this.m_poolObjects.Count; i++)
		{
			UberPoolItem poolObject = this.m_poolObjects[i];
			if (poolObject.IsDestroyed)
			{
				poolObject.Target.transform.position = position;
				poolObject.Target.transform.rotation = rotation;
				poolObject.OnSpawned();
				poolObject.SetActive();
				poolObject.Target.SetActive(true);
				poolObject.Target.BroadcastMessage("OnPoolSpawned", SendMessageOptions.DontRequireReceiver);
				this.m_lateStartMethod = delegate()
				{
					UberPoolGroup.BroadcastCallbacks(poolObject);
				};
				LateStartHook.AddLateStartMethod(this.m_lateStartMethod);
				newObject = false;
				return poolObject;
			}
		}
		GameObject newObject2 = this.GetNewObject(prototype, position, rotation);
		UberPoolItem result = this.CreatePoolItemForGO(newObject2);
		newObject = true;
		return result;
	}

	// Token: 0x06002A5E RID: 10846 RVA: 0x000B611C File Offset: 0x000B431C
	private UberPoolItem CreatePoolItemForGO(GameObject newObj)
	{
		UberPoolItem uberPoolItem = new UberPoolItem();
		uberPoolItem.Group = base.transform;
		uberPoolItem.Target = newObj;
		newObj.transform.parent = base.transform;
		uberPoolItem.OnNewlyCreated();
		uberPoolItem.OnSpawned();
		this.m_poolObjects.Add(uberPoolItem);
		return uberPoolItem;
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x000B616C File Offset: 0x000B436C
	private static void BroadcastCallbacks(UberPoolItem poolObject)
	{
		poolObject.Target.BroadcastMessage("Start", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x000B6180 File Offset: 0x000B4380
	private GameObject GetNewObject(GameObject prototype, Vector3 position, Quaternion rotation)
	{
		if (prototype != null)
		{
			return UnityEngine.Object.Instantiate(prototype, position, rotation) as GameObject;
		}
		GameObject gameObject = new GameObject(this.m_poolName);
		if (position != Vector3.zero)
		{
			gameObject.transform.position = position;
		}
		if (rotation != Quaternion.identity)
		{
			gameObject.transform.rotation = rotation;
		}
		return gameObject;
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x000B61EC File Offset: 0x000B43EC
	private void Update()
	{
		this.PruneObjects();
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x000B61F4 File Offset: 0x000B43F4
	private void PruneObjects()
	{
		if (this.m_poolObjects.Count > this.m_minNumber)
		{
			for (int i = this.m_poolObjects.Count - 1; i >= this.m_minNumber; i--)
			{
				UberPoolItem uberPoolItem = this.m_poolObjects[i];
				bool isDestroyed = uberPoolItem.IsDestroyed;
				if (isDestroyed)
				{
					uberPoolItem.InactiveTime += Time.deltaTime;
					if (uberPoolItem.InactiveTime > this.m_setting.InactiveDeleteTime)
					{
						UberPoolManager.Instance.Decease(uberPoolItem.Target);
						UnityEngine.Object.Destroy(uberPoolItem.Target);
						this.m_poolObjects.RemoveAt(i);
					}
				}
			}
		}
	}

	// Token: 0x040025B6 RID: 9654
	private UberPoolSettings m_setting;

	// Token: 0x040025B7 RID: 9655
	private List<UberPoolItem> m_poolObjects = new List<UberPoolItem>();

	// Token: 0x040025B8 RID: 9656
	private string m_poolName;

	// Token: 0x040025B9 RID: 9657
	private Action m_lateStartMethod;

	// Token: 0x040025BA RID: 9658
	private int m_minNumber;
}
