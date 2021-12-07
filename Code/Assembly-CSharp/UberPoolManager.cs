using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class UberPoolManager : MonoBehaviour
{
	// Token: 0x060005FD RID: 1533 RVA: 0x000179C0 File Offset: 0x00015BC0
	[ContextMenu("Look up")]
	public void DoLookUp()
	{
		foreach (UberPoolPrefabSetting uberPoolPrefabSetting in this.Settings)
		{
			foreach (WeakPrefab weakPrefab in uberPoolPrefabSetting.PrefabRefs)
			{
				if (weakPrefab.Name == this.LookUp)
				{
					Debug.Log("Found it in " + uberPoolPrefabSetting.Name);
				}
			}
		}
	}

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x060005FE RID: 1534 RVA: 0x00017A80 File Offset: 0x00015C80
	// (set) Token: 0x060005FF RID: 1535 RVA: 0x00017A88 File Offset: 0x00015C88
	public bool DoPool
	{
		get
		{
			return this.m_doPool;
		}
		set
		{
			this.m_doPool = value;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000600 RID: 1536 RVA: 0x00017A91 File Offset: 0x00015C91
	// (set) Token: 0x06000601 RID: 1537 RVA: 0x00017A99 File Offset: 0x00015C99
	public bool DoPoolAnalysis
	{
		get
		{
			return this.m_doAnalysis;
		}
		set
		{
			this.m_doAnalysis = value;
		}
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x00017AA4 File Offset: 0x00015CA4
	private void Awake()
	{
		if (UberPoolManager.Instance != null)
		{
			return;
		}
		UberPoolManager.Instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00017AD3 File Offset: 0x00015CD3
	public GameObject Spawn(GameObject o)
	{
		return this.Spawn(o, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00017AE8 File Offset: 0x00015CE8
	public GameObject Spawn(GameObject o, Vector3 position, Quaternion rotation)
	{
		string name = o.name;
		return this.SpawnWithName(name, position, rotation, o);
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00017B08 File Offset: 0x00015D08
	public void PrewarmInstance(GameObject prefab)
	{
		string name = prefab.name;
		int groupIndex = this.GetGroupIndex(name);
		if (groupIndex == -1)
		{
			return;
		}
		UberPoolGroup uberPoolGroup = this.m_groups[groupIndex];
		uberPoolGroup.PrewarmInstance(prefab, this.m_spawnedToItem);
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x00017B48 File Offset: 0x00015D48
	private GameObject SpawnWithName(string getName, Vector3 position, Quaternion rotation, GameObject original)
	{
		if (!this.DoPool)
		{
			return null;
		}
		int groupIndex = this.GetGroupIndex(getName);
		if (groupIndex == -1)
		{
			return null;
		}
		UberPoolGroup uberPoolGroup = this.m_groups[groupIndex];
		bool flag;
		UberPoolItem uberPoolItem = uberPoolGroup.RequestObject(original, position, rotation, out flag);
		if (flag)
		{
			uberPoolItem.AddEntries(this.m_spawnedToItem);
		}
		if (this.DoPoolAnalysis && original != null)
		{
			UberPoolAnalyze.Analyze(uberPoolItem.Target, original);
		}
		return uberPoolItem.Target;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00017BCC File Offset: 0x00015DCC
	private int GetGroupIndex(string getName)
	{
		int num;
		if (!this.m_objectToPool.TryGetValue(getName, out num))
		{
			num = -1;
			for (int i = 0; i < this.Settings.Count; i++)
			{
				UberPoolPrefabSetting uberPoolPrefabSetting = this.Settings[i];
				for (int j = 0; j < uberPoolPrefabSetting.PrefabRefs.Count; j++)
				{
					WeakPrefab weakPrefab = uberPoolPrefabSetting.PrefabRefs[j];
					if (weakPrefab.Name == getName)
					{
						num = this.CreateNewGroup(getName, uberPoolPrefabSetting.Setting);
					}
				}
			}
			this.m_objectToPool[getName] = num;
		}
		return num;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00017C70 File Offset: 0x00015E70
	private int CreateNewGroup(string groupName, UberPoolSettings setting)
	{
		GameObject gameObject = new GameObject();
		UberPoolGroup uberPoolGroup = gameObject.AddComponent<UberPoolGroup>();
		uberPoolGroup.Init(groupName, setting);
		int count = this.m_groups.Count;
		this.m_groups.Add(uberPoolGroup);
		return count;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00017CAC File Offset: 0x00015EAC
	public bool Destroy(GameObject go)
	{
		int instanceID = go.GetInstanceID();
		UberPoolItem uberPoolItem;
		if (this.m_spawnedToItem.TryGetValue(instanceID, out uberPoolItem))
		{
			go.BroadcastMessage("OnPoolDespawned", SendMessageOptions.DontRequireReceiver);
			go.gameObject.SetActive(false);
			uberPoolItem.OnDespawn(go);
			List<Action> list;
			if (this.m_releaseActions.TryGetValue(instanceID, out list) && list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					Action action = list[i];
					action();
				}
				this.m_releaseActions[instanceID].Clear();
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00017D4C File Offset: 0x00015F4C
	public void Decease(GameObject poolObject)
	{
		int instanceID = poolObject.GetInstanceID();
		UberPoolItem uberPoolItem = this.m_spawnedToItem[instanceID];
		uberPoolItem.OnDecease();
		this.m_spawnedToItem.Remove(instanceID);
		if (this.m_releaseActions.ContainsKey(instanceID))
		{
			this.m_releaseActions.Remove(instanceID);
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00017DA0 File Offset: 0x00015FA0
	public bool IsDestroyed(GameObject go)
	{
		if (go.activeInHierarchy)
		{
			return false;
		}
		int instanceID = go.GetInstanceID();
		UberPoolItem uberPoolItem;
		return this.m_spawnedToItem.TryGetValue(instanceID, out uberPoolItem) && uberPoolItem.IsFree;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00017DDC File Offset: 0x00015FDC
	public bool IsDestroyed(Component comp)
	{
		return this.IsDestroyed(comp.gameObject);
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00017DEC File Offset: 0x00015FEC
	public void RemoveOnDestroyed(GameObject go)
	{
		int instanceID = go.GetInstanceID();
		if (this.m_releaseActions.ContainsKey(instanceID))
		{
			this.m_releaseActions.Remove(instanceID);
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x00017E20 File Offset: 0x00016020
	public void AddOnDestroyed(GameObject go, Action set)
	{
		int instanceID = go.GetInstanceID();
		UberPoolItem uberPoolItem;
		if (this.m_spawnedToItem.TryGetValue(instanceID, out uberPoolItem))
		{
			if (!this.m_releaseActions.ContainsKey(instanceID))
			{
				this.m_releaseActions.Add(instanceID, new List<Action>());
			}
			this.m_releaseActions[instanceID].Add(set);
		}
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x00017E7B File Offset: 0x0001607B
	public void RunDestroyDelayed(float time, Action run)
	{
		base.StartCoroutine(this.RunDelayed(time, run));
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00017E8C File Offset: 0x0001608C
	private IEnumerator RunDelayed(float time, Action run)
	{
		yield return new WaitForSeconds(time);
		run();
		yield break;
	}

	// Token: 0x040004AA RID: 1194
	private const string c_broadcastCallback = "OnPoolDespawned";

	// Token: 0x040004AB RID: 1195
	public static UberPoolManager Instance;

	// Token: 0x040004AC RID: 1196
	public string LookUp;

	// Token: 0x040004AD RID: 1197
	public List<UberPoolPrefabSetting> Settings = new List<UberPoolPrefabSetting>();

	// Token: 0x040004AE RID: 1198
	private List<UberPoolGroup> m_groups = new List<UberPoolGroup>();

	// Token: 0x040004AF RID: 1199
	private Dictionary<string, int> m_objectToPool = new Dictionary<string, int>();

	// Token: 0x040004B0 RID: 1200
	private Dictionary<int, UberPoolItem> m_spawnedToItem = new Dictionary<int, UberPoolItem>();

	// Token: 0x040004B1 RID: 1201
	private Dictionary<int, List<Action>> m_releaseActions = new Dictionary<int, List<Action>>();

	// Token: 0x040004B2 RID: 1202
	private bool m_doPool = true;

	// Token: 0x040004B3 RID: 1203
	private bool m_doAnalysis;
}
