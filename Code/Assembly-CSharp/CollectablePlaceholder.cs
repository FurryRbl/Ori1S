using System;
using Game;
using UnityEngine;

// Token: 0x0200090D RID: 2317
public class CollectablePlaceholder : SaveSerialize, ISuspendable, IDynamicGraphic
{
	// Token: 0x06003374 RID: 13172 RVA: 0x000D8FD8 File Offset: 0x000D71D8
	public override void Awake()
	{
		CollectablePlaceholder.All.Add(this);
		if (this.Prefab == null)
		{
			InstantiateUtility.Destroy(base.gameObject);
			return;
		}
		base.Awake();
		base.GetComponent<Renderer>().enabled = false;
		SuspensionManager.Register(this);
	}

	// Token: 0x06003375 RID: 13173 RVA: 0x000D9025 File Offset: 0x000D7225
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		base.OnDestroy();
		CollectablePlaceholder.All.Remove(this);
	}

	// Token: 0x06003376 RID: 13174 RVA: 0x000D9040 File Offset: 0x000D7240
	public void Spawn()
	{
		if (!InstantiateUtility.IsDestroyed(this.m_instance))
		{
			InstantiateUtility.Destroy(this.m_instance);
			this.m_instance = null;
		}
		this.Instantiate();
	}

	// Token: 0x06003377 RID: 13175 RVA: 0x000D9075 File Offset: 0x000D7275
	public void OnCollect()
	{
		this.m_collected = true;
		this.m_remainingRespawnTime = this.RespawnTime;
	}

	// Token: 0x06003378 RID: 13176 RVA: 0x000D908C File Offset: 0x000D728C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_remainingRespawnTime > 0f)
		{
			this.m_remainingRespawnTime -= Time.deltaTime;
			this.m_collected = false;
		}
		if (this.m_instance == null && !this.m_collected && UI.Cameras.Current.IsOnScreenPadded(base.transform.position, 5f))
		{
			this.Instantiate();
		}
	}

	// Token: 0x06003379 RID: 13177 RVA: 0x000D9110 File Offset: 0x000D7310
	public void Instantiate()
	{
		this.m_instance = (InstantiateUtility.Instantiate(this.Prefab, base.transform.position, base.transform.rotation) as GameObject);
		UberPoolManager.Instance.AddOnDestroyed(this.m_instance, delegate
		{
			this.m_instance = null;
		});
		PickupBase componentInChildren = this.m_instance.GetComponentInChildren<PickupBase>();
		PickupBase pickupBase = componentInChildren;
		pickupBase.OnCollectedEvent = (Action)Delegate.Combine(pickupBase.OnCollectedEvent, new Action(this.OnCollect));
		if (this.m_instance.GetComponent<DestroyOnRestoreCheckpoint>() == null)
		{
			this.m_instance.AddComponent<DestroyOnRestoreCheckpoint>();
		}
		if (base.GetComponent<VisibleOnWorldMap>() && this.m_instance.GetComponent<VisibleOnWorldMap>())
		{
			this.m_instance.GetComponent<VisibleOnWorldMap>().MoonGuid = base.GetComponent<VisibleOnWorldMap>().MoonGuid;
		}
		this.m_instance.transform.parent = base.transform.parent;
		this.m_instance.name = this.Prefab.name;
	}

	// Token: 0x0600337A RID: 13178 RVA: 0x000D9226 File Offset: 0x000D7426
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_collected);
		ar.Serialize(ref this.m_remainingRespawnTime);
	}

	// Token: 0x1700081F RID: 2079
	// (get) Token: 0x0600337B RID: 13179 RVA: 0x000D9240 File Offset: 0x000D7440
	public bool Collected
	{
		get
		{
			return this.m_collected;
		}
	}

	// Token: 0x17000820 RID: 2080
	// (get) Token: 0x0600337C RID: 13180 RVA: 0x000D9248 File Offset: 0x000D7448
	// (set) Token: 0x0600337D RID: 13181 RVA: 0x000D9250 File Offset: 0x000D7450
	public bool IsSuspended { get; set; }

	// Token: 0x04002E7A RID: 11898
	public float RespawnTime;

	// Token: 0x04002E7B RID: 11899
	public GameObject Prefab;

	// Token: 0x04002E7C RID: 11900
	public static AllContainer<CollectablePlaceholder> All = new AllContainer<CollectablePlaceholder>();

	// Token: 0x04002E7D RID: 11901
	public bool UseDebug;

	// Token: 0x04002E7E RID: 11902
	private float m_remainingRespawnTime;

	// Token: 0x04002E7F RID: 11903
	private GameObject m_instance;

	// Token: 0x04002E80 RID: 11904
	private bool m_collected;
}
