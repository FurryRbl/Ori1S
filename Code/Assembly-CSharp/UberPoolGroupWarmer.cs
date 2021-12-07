using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006EF RID: 1775
public class UberPoolGroupWarmer : MonoBehaviour
{
	// Token: 0x06002A74 RID: 10868 RVA: 0x000B671C File Offset: 0x000B491C
	private void Update()
	{
		if (UberPoolManager.Instance != null && this.m_prefabIndex < this.Prefabs.Count)
		{
			GameObject prefab = this.Prefabs[this.m_prefabIndex];
			for (int i = 0; i < this.AmountToWarm[this.m_prefabIndex]; i++)
			{
				UberPoolManager.Instance.PrewarmInstance(prefab);
			}
			this.m_prefabIndex++;
		}
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x000B679C File Offset: 0x000B499C
	private void Awake()
	{
		if (!this.KeepLoaded)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040025C5 RID: 9669
	public List<GameObject> Prefabs = new List<GameObject>();

	// Token: 0x040025C6 RID: 9670
	public List<int> AmountToWarm = new List<int>();

	// Token: 0x040025C7 RID: 9671
	public bool KeepLoaded = true;

	// Token: 0x040025C8 RID: 9672
	private int m_prefabIndex;
}
