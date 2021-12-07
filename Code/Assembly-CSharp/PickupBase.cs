using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020008D0 RID: 2256
public abstract class PickupBase : SaveSerialize, IFrustumOptimizable, IPooled, IDynamicGraphicHierarchy
{
	// Token: 0x06003233 RID: 12851 RVA: 0x000D4B50 File Offset: 0x000D2D50
	public void OnValidate()
	{
		this.m_onKillRecievers = base.GetComponentsInChildren(typeof(IKillReciever));
		if (this.DestroyTarget == null)
		{
			this.DestroyTarget = base.gameObject;
		}
		this.m_transform = base.transform;
	}

	// Token: 0x06003234 RID: 12852 RVA: 0x000D4B9C File Offset: 0x000D2D9C
	public void OnPoolSpawned()
	{
		this.OnCollectedEvent = delegate()
		{
		};
		this.IsCollected = false;
		this.m_currentTime = 0f;
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x000D4BD4 File Offset: 0x000D2DD4
	public override void Awake()
	{
		base.Awake();
		this.m_bounds = new Bounds(base.transform.position, Vector3.one * 4f);
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x000D4C0C File Offset: 0x000D2E0C
	public void FixedUpdate()
	{
		if (this.FrustrumOptimized && !this.m_insideFrustum)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.m_currentTime += Time.deltaTime;
		if (this.m_currentTime < this.DelayBeforeCollectable)
		{
			return;
		}
		if (!this.IsCollected && Characters.Sein && Vector3.Distance(this.m_transform.position, Characters.Sein.Position) < this.Radius)
		{
			this.OnCollectorCandidateTouch(Characters.Sein.gameObject);
		}
	}

	// Token: 0x06003237 RID: 12855
	public abstract void OnCollectorCandidateTouch(GameObject collector);

	// Token: 0x06003238 RID: 12856 RVA: 0x000D4CAF File Offset: 0x000D2EAF
	public void SpawnCollectedEffect()
	{
		if (this.CollectedEffect)
		{
			InstantiateUtility.Instantiate(this.CollectedEffect, this.m_transform.position, Quaternion.identity);
		}
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x000D4CE0 File Offset: 0x000D2EE0
	public virtual void Collected()
	{
		this.IsCollected = true;
		this.SpawnCollectedEffect();
		if (this.CollectedSoundProvider != null)
		{
			Sound.Play(this.CollectedSoundProvider.GetSound(null), this.m_transform.position, null);
		}
		for (int i = 0; i < this.m_onKillRecievers.Length; i++)
		{
			if (this.m_onKillRecievers[i])
			{
				((IKillReciever)this.m_onKillRecievers[i]).OnKill();
			}
		}
		if (this.OnCollectedAction != null)
		{
			this.OnCollectedAction.Perform(null);
		}
		this.OnCollectedEvent();
		if (this.DestroyOnCollect)
		{
			InstantiateUtility.Destroy(this.DestroyTarget);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x000D4DBC File Offset: 0x000D2FBC
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_currentTime);
		ar.Serialize(ref this.IsCollected);
		if (ar.Reading)
		{
			base.gameObject.SetActive(!this.IsCollected);
		}
	}

	// Token: 0x170007F6 RID: 2038
	// (get) Token: 0x0600323B RID: 12859 RVA: 0x000D4E00 File Offset: 0x000D3000
	public Bounds Bounds
	{
		get
		{
			this.m_bounds.center = this.m_transform.position;
			return this.m_bounds;
		}
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x000D4E1E File Offset: 0x000D301E
	public void OnFrustumEnter()
	{
		this.m_insideFrustum = true;
		if (!this.IsCollected)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x000D4E3E File Offset: 0x000D303E
	public void OnFrustumExit()
	{
		this.m_insideFrustum = false;
	}

	// Token: 0x170007F7 RID: 2039
	// (get) Token: 0x0600323E RID: 12862 RVA: 0x000D4E47 File Offset: 0x000D3047
	public bool InsideFrustum
	{
		get
		{
			return this.m_insideFrustum;
		}
	}

	// Token: 0x04002D36 RID: 11574
	public bool IsCollected;

	// Token: 0x04002D37 RID: 11575
	public SoundProvider CollectedSoundProvider;

	// Token: 0x04002D38 RID: 11576
	public Action OnCollectedEvent = delegate()
	{
	};

	// Token: 0x04002D39 RID: 11577
	public ActionMethod OnCollectedAction;

	// Token: 0x04002D3A RID: 11578
	public float DelayBeforeCollectable;

	// Token: 0x04002D3B RID: 11579
	public bool DestroyOnCollect;

	// Token: 0x04002D3C RID: 11580
	public GameObject DestroyTarget;

	// Token: 0x04002D3D RID: 11581
	public GameObject CollectedEffect;

	// Token: 0x04002D3E RID: 11582
	public float Radius = 2f;

	// Token: 0x04002D3F RID: 11583
	public bool FrustrumOptimized;

	// Token: 0x04002D40 RID: 11584
	[HideInInspector]
	[SerializeField]
	private Component[] m_onKillRecievers;

	// Token: 0x04002D41 RID: 11585
	[SerializeField]
	[HideInInspector]
	private Transform m_transform;

	// Token: 0x04002D42 RID: 11586
	private float m_currentTime;

	// Token: 0x04002D43 RID: 11587
	private Bounds m_bounds;

	// Token: 0x04002D44 RID: 11588
	private bool m_insideFrustum = true;
}
