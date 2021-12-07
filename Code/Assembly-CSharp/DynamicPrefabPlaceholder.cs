using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000966 RID: 2406
public class DynamicPrefabPlaceholder : MonoBehaviour
{
	// Token: 0x060034DC RID: 13532 RVA: 0x000DDFC4 File Offset: 0x000DC1C4
	public void Awake()
	{
		GameObject exists;
		if (DynamicPrefabPlaceholder.InstantiatedPrefabs.TryGetValue(this.UniqueName, out exists) && exists)
		{
			return;
		}
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab);
		Transform transform = gameObject.transform;
		transform.position = base.transform.position;
		transform.rotation = base.transform.rotation;
		InstantiateUtility.Destroy(base.gameObject);
		DynamicPrefabPlaceholder.InstantiatedPrefabs.Add(this.UniqueName, gameObject);
	}

	// Token: 0x04002F9A RID: 12186
	public static Dictionary<string, GameObject> InstantiatedPrefabs = new Dictionary<string, GameObject>();

	// Token: 0x04002F9B RID: 12187
	public GameObject Prefab;

	// Token: 0x04002F9C RID: 12188
	public string UniqueName;
}
