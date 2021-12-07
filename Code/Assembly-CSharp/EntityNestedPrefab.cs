using System;
using UnityEngine;

// Token: 0x02000548 RID: 1352
public class EntityNestedPrefab : MonoBehaviour
{
	// Token: 0x06002370 RID: 9072 RVA: 0x0009AF53 File Offset: 0x00099153
	public void OnValidate()
	{
		this.Entity = base.transform.FindComponentUpwards<Entity>();
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x0009AF68 File Offset: 0x00099168
	public void InstantiatePrefab()
	{
		this.Prefab.SetActive(false);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab);
		this.Prefab.SetActive(true);
		gameObject.transform.parent = base.transform.parent;
		gameObject.transform.localPosition = base.transform.localPosition;
		gameObject.name = base.name;
		gameObject.SetActive(true);
		if (Application.isPlaying)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x0009AFFD File Offset: 0x000991FD
	public void Start()
	{
		if (this.Entity == null)
		{
			this.OnValidate();
		}
		this.InstantiatePrefab();
	}

	// Token: 0x04001DBD RID: 7613
	public Entity Entity;

	// Token: 0x04001DBE RID: 7614
	public GameObject Prefab;
}
