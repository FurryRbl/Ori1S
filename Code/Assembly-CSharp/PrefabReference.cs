using System;
using UnityEngine;

// Token: 0x0200098D RID: 2445
[ExecuteInEditMode]
[AddComponentMenu("Generic/Prefab reference")]
public class PrefabReference : MonoBehaviour
{
	// Token: 0x06003575 RID: 13685 RVA: 0x000E0086 File Offset: 0x000DE286
	private void Awake()
	{
		if (Application.isPlaying)
		{
			this.RuntimeAwake();
		}
	}

	// Token: 0x06003576 RID: 13686 RVA: 0x000E0098 File Offset: 0x000DE298
	private void Update()
	{
		if (Application.isEditor && this.referencedPrefab != null && this.m_prefab == null)
		{
			this.EditorAwake();
		}
	}

	// Token: 0x06003577 RID: 13687 RVA: 0x000E00D8 File Offset: 0x000DE2D8
	private void EditorAwake()
	{
		if (this.m_prefab)
		{
			InstantiateUtility.Destroy(this.m_prefab);
		}
		this.m_prefab = (InstantiateUtility.Instantiate(this.referencedPrefab, base.transform.position, base.transform.rotation) as GameObject);
		Vector3 localPosition = this.m_prefab.transform.localPosition;
		Vector3 localScale = this.m_prefab.transform.localScale;
		Quaternion localRotation = this.m_prefab.transform.localRotation;
		this.m_prefab.transform.parent = base.transform;
		this.m_prefab.transform.localPosition = localPosition;
		this.m_prefab.transform.localScale = localScale;
		this.m_prefab.transform.localRotation = localRotation;
		this.HideRecursively(this.m_prefab);
		this.m_prefab.SetActive(false);
		this.m_prefab.SetActive(true);
	}

	// Token: 0x06003578 RID: 13688 RVA: 0x000E01CC File Offset: 0x000DE3CC
	private void HideRecursively(GameObject o)
	{
		o.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
		foreach (object obj in o.transform)
		{
			Transform transform = (Transform)obj;
			this.HideRecursively(transform.gameObject);
		}
	}

	// Token: 0x06003579 RID: 13689 RVA: 0x000E023C File Offset: 0x000DE43C
	private void RuntimeAwake()
	{
		GameObject gameObject = InstantiateUtility.Instantiate(this.referencedPrefab, base.transform.position, base.transform.rotation) as GameObject;
		Vector3 localPosition = gameObject.transform.localPosition;
		Vector3 localScale = gameObject.transform.localScale;
		Quaternion localRotation = gameObject.transform.localRotation;
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localScale = localScale;
		gameObject.transform.localRotation = localRotation;
		gameObject.transform.parent = base.transform.parent;
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x04003004 RID: 12292
	public GameObject referencedPrefab;

	// Token: 0x04003005 RID: 12293
	private GameObject m_prefab;
}
