using System;
using UnityEngine;

// Token: 0x0200093A RID: 2362
public class DestroyOnStartUnlessDebug : MonoBehaviour
{
	// Token: 0x06003438 RID: 13368 RVA: 0x000DB9E0 File Offset: 0x000D9BE0
	private void Start()
	{
		if (!Application.isEditor)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}
}
