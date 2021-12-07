using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000969 RID: 2409
public class ExistOnceByName : MonoBehaviour
{
	// Token: 0x060034E8 RID: 13544 RVA: 0x000DE138 File Offset: 0x000DC338
	public void Awake()
	{
		if (ExistOnceByName.m_instances.ContainsKey(base.name))
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
		else
		{
			ExistOnceByName.m_instances.Add(base.name, this);
		}
	}

	// Token: 0x060034E9 RID: 13545 RVA: 0x000DE17B File Offset: 0x000DC37B
	public void OnDestroy()
	{
		ExistOnceByName.m_instances.Remove(base.name);
	}

	// Token: 0x04002FA0 RID: 12192
	private static Dictionary<string, ExistOnceByName> m_instances = new Dictionary<string, ExistOnceByName>();
}
