using System;
using UnityEngine;

// Token: 0x0200098C RID: 2444
public class PrefabPlaceholder : MonoBehaviour
{
	// Token: 0x06003573 RID: 13683 RVA: 0x000DFFFC File Offset: 0x000DE1FC
	public void Awake()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab);
		Transform transform = gameObject.transform;
		transform.parent = base.transform.parent;
		transform.localPosition = base.transform.localPosition;
		transform.localRotation = base.transform.localRotation;
		InstantiateUtility.Destroy(base.gameObject);
		SaveSceneManager saveSceneManager = SaveSceneManager.FromTransform(base.transform);
		if (saveSceneManager != null)
		{
			saveSceneManager.RegisterGameObject(gameObject);
		}
	}

	// Token: 0x04003003 RID: 12291
	public GameObject Prefab;
}
