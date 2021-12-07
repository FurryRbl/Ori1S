using System;
using Game;
using UnityEngine;

// Token: 0x0200064F RID: 1615
[ExecuteInEditMode]
public class LightTorchPlaceholder : MonoBehaviour
{
	// Token: 0x06002786 RID: 10118 RVA: 0x000AC058 File Offset: 0x000AA258
	public void FixedUpdate()
	{
		if (Items.LightTorch == null)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.LightTorchPrefab, base.transform.position, Quaternion.identity);
			SaveSceneManager.Master.RegisterGameObject(gameObject);
			gameObject.AddComponent<LoadFromMasterAtStart>();
		}
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x000AC0A8 File Offset: 0x000AA2A8
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawTextFilled(base.transform, "Light Torch Placeholder", false);
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x000AC0BB File Offset: 0x000AA2BB
	public void OnDrawGizmosSelected()
	{
		GizmoHelper.DrawSelectedTextFilled(base.transform, "Light Torch Placeholder", false);
	}

	// Token: 0x0400221A RID: 8730
	public GameObject LightTorchPrefab;
}
