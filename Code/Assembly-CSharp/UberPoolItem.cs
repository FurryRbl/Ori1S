using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006ED RID: 1773
public class UberPoolItem
{
	// Token: 0x170006C2 RID: 1730
	// (get) Token: 0x06002A66 RID: 10854 RVA: 0x000B62E1 File Offset: 0x000B44E1
	public bool IsFree
	{
		get
		{
			return !this.Target.activeInHierarchy;
		}
	}

	// Token: 0x170006C3 RID: 1731
	// (get) Token: 0x06002A67 RID: 10855 RVA: 0x000B62F1 File Offset: 0x000B44F1
	public bool IsDestroyed
	{
		get
		{
			return this.m_isDestroyed;
		}
	}

	// Token: 0x06002A68 RID: 10856 RVA: 0x000B62FC File Offset: 0x000B44FC
	public virtual void OnSpawned()
	{
		this.m_isDestroyed = false;
		this.InactiveTime = 0f;
		this.Target.transform.localScale = this.m_startPos[this.Target.transform.GetInstanceID()].StartScale;
		for (int i = 0; i < this.m_children.Count; i++)
		{
			Transform trans = this.m_children[i];
			this.ResetPosition(trans);
		}
	}

	// Token: 0x06002A69 RID: 10857 RVA: 0x000B6380 File Offset: 0x000B4580
	public void SetActive()
	{
		if (this.m_activeAtStart == null)
		{
			return;
		}
		for (int i = 0; i < this.m_activeAtStart.Count; i++)
		{
			GameObject gameObject = this.m_activeAtStart[i];
			if (gameObject != null && !gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06002A6A RID: 10858 RVA: 0x000B63E0 File Offset: 0x000B45E0
	public virtual void OnDespawn(GameObject go)
	{
		if (go == null)
		{
			return;
		}
		if (go == this.Target)
		{
			Transform group = this.Group;
			this.Target.transform.parent = group;
			this.m_isDestroyed = true;
		}
		else
		{
			go.transform.parent = this.m_startParent[go.transform.GetInstanceID()];
		}
		this.ResetPosition(go.transform);
	}

	// Token: 0x06002A6B RID: 10859 RVA: 0x000B645C File Offset: 0x000B465C
	private void ResetPosition(Transform trans)
	{
		if (trans == null)
		{
			return;
		}
		int instanceID = trans.GetInstanceID();
		if (trans.gameObject != this.Target && this.m_startParent.ContainsKey(instanceID))
		{
			trans.parent = this.m_startParent[instanceID];
		}
		if (this.m_startPos.ContainsKey(instanceID))
		{
			PoolTransformPosition poolTransformPosition = this.m_startPos[instanceID];
			trans.localPosition = poolTransformPosition.Position;
			trans.localRotation = poolTransformPosition.Rotation;
			trans.localScale = poolTransformPosition.StartScale;
		}
	}

	// Token: 0x06002A6C RID: 10860 RVA: 0x000B64FC File Offset: 0x000B46FC
	public void OnNewlyCreated()
	{
		int childCount = this.Target.transform.childCount;
		if (childCount > 0)
		{
			this.m_activeAtStart = new List<GameObject>();
			this.m_children = new List<Transform>();
		}
		this.AddStateRecurs(this.Target.transform, this.m_activeAtStart, this.m_children);
		this.InactiveTime = 0f;
	}

	// Token: 0x06002A6D RID: 10861 RVA: 0x000B6560 File Offset: 0x000B4760
	public void OnDecease()
	{
		this.m_startParent.Clear();
		this.m_startPos.Clear();
		if (this.m_activeAtStart != null)
		{
			this.m_activeAtStart.Clear();
		}
	}

	// Token: 0x06002A6E RID: 10862 RVA: 0x000B659C File Offset: 0x000B479C
	private void AddStateRecurs(Transform trans, List<GameObject> activeAtStart, List<Transform> children)
	{
		if (trans.gameObject.activeSelf && trans != this.Target.transform)
		{
			activeAtStart.Add(trans.gameObject);
		}
		PoolTransformPosition value = new PoolTransformPosition
		{
			Position = trans.localPosition,
			Rotation = trans.localRotation,
			StartScale = trans.localScale
		};
		int instanceID = trans.GetInstanceID();
		this.m_startPos.Add(instanceID, value);
		this.m_startParent.Add(instanceID, trans.parent);
		if (trans.gameObject != this.Target)
		{
			children.Add(trans);
		}
		for (int i = 0; i < trans.childCount; i++)
		{
			Transform child = trans.GetChild(i);
			this.AddStateRecurs(child, activeAtStart, children);
		}
	}

	// Token: 0x06002A6F RID: 10863 RVA: 0x000B667C File Offset: 0x000B487C
	private void AddEntryRecurs(Dictionary<int, UberPoolItem> add, GameObject go)
	{
		add.Add(go.GetInstanceID(), this);
		for (int i = 0; i < go.transform.childCount; i++)
		{
			Transform child = go.transform.GetChild(i);
			this.AddEntryRecurs(add, child.gameObject);
		}
	}

	// Token: 0x06002A70 RID: 10864 RVA: 0x000B66CC File Offset: 0x000B48CC
	public void AddEntries(Dictionary<int, UberPoolItem> spawnedToItem)
	{
		this.AddEntryRecurs(spawnedToItem, this.Target);
	}

	// Token: 0x06002A71 RID: 10865 RVA: 0x000B66DB File Offset: 0x000B48DB
	public void SetExplicitDestroy(bool destroyed)
	{
		this.m_isDestroyed = destroyed;
	}

	// Token: 0x040025BC RID: 9660
	public GameObject Target;

	// Token: 0x040025BD RID: 9661
	public Transform Group;

	// Token: 0x040025BE RID: 9662
	private List<GameObject> m_activeAtStart;

	// Token: 0x040025BF RID: 9663
	private Dictionary<int, PoolTransformPosition> m_startPos = new Dictionary<int, PoolTransformPosition>();

	// Token: 0x040025C0 RID: 9664
	private Dictionary<int, Transform> m_startParent = new Dictionary<int, Transform>();

	// Token: 0x040025C1 RID: 9665
	private List<Transform> m_children = new List<Transform>();

	// Token: 0x040025C2 RID: 9666
	[NonSerialized]
	public float InactiveTime;

	// Token: 0x040025C3 RID: 9667
	private bool m_isDestroyed;
}
