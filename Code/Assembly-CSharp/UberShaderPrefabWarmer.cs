using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007AA RID: 1962
public class UberShaderPrefabWarmer : MonoBehaviour
{
	// Token: 0x06002D73 RID: 11635 RVA: 0x000C22AE File Offset: 0x000C04AE
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x04002902 RID: 10498
	public List<GameObject> PrefabsToWarm;

	// Token: 0x04002903 RID: 10499
	public List<Texture2D> BaseAtlases;
}
