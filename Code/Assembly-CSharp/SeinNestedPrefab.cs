using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class SeinNestedPrefab
{
	// Token: 0x060008F6 RID: 2294 RVA: 0x00026A0D File Offset: 0x00024C0D
	public SeinNestedPrefab(SeinCharacter sein, GameObject prefab)
	{
		this.Sein = sein;
		this.m_prefab = prefab;
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00026A55 File Offset: 0x00024C55
	// (set) Token: 0x060008F7 RID: 2295 RVA: 0x00026A24 File Offset: 0x00024C24
	public bool IsInstantiated
	{
		get
		{
			return this.m_gameObject;
		}
		set
		{
			if (value == this.IsInstantiated)
			{
				return;
			}
			if (value)
			{
				this.Instantiate();
			}
			else
			{
				this.Destroy();
			}
		}
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00026A64 File Offset: 0x00024C64
	private void Instantiate()
	{
		this.m_gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_prefab);
		this.m_gameObject.transform.parent = this.Sein.PlayerAbilities.transform;
		this.m_gameObject.transform.localPosition = Vector3.zero;
		this.m_gameObject.transform.localScale = Vector3.one;
		this.Sein.MakeBelongToSein(this.m_gameObject);
		SaveSceneManager.Master.RegisterGameObject(this.m_gameObject);
		this.m_gameObject.AddComponent<LoadFromMasterAtStart>();
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00026AFC File Offset: 0x00024CFC
	private void Destroy()
	{
		SaveSceneManager.Master.UnregisterGameObject(this.m_gameObject);
		InstantiateUtility.Destroy(this.m_gameObject);
		this.m_gameObject = null;
	}

	// Token: 0x04000736 RID: 1846
	public SeinCharacter Sein;

	// Token: 0x04000737 RID: 1847
	private GameObject m_prefab;

	// Token: 0x04000738 RID: 1848
	private GameObject m_gameObject;
}
