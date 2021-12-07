using System;
using Core;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x020008F4 RID: 2292
public class MistTorchPlaceholder : MonoBehaviour
{
	// Token: 0x0600330A RID: 13066 RVA: 0x000D74BC File Offset: 0x000D56BC
	public void FixedUpdate()
	{
		if (Sein.World.Events.MistLifted)
		{
			return;
		}
		if (Items.MistTorch == null)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.MistTorchPrefab, base.transform.position, Quaternion.identity);
			SaveSceneManager.Master.RegisterGameObject(gameObject);
			gameObject.AddComponent<LoadFromMasterAtStart>();
		}
		else
		{
			this.m_remainingTime -= Time.deltaTime;
			MistTorch mistTorch = Items.MistTorch;
			if (this.m_remainingTime <= 0f)
			{
				this.m_remainingTime = ((!mistTorch.gameObject.activeSelf) ? 0.2f : 0f);
				mistTorch.gameObject.SetActive(Scenes.Manager.IsInsideActiveSceneBoundary(mistTorch.transform.position));
			}
		}
	}

	// Token: 0x0600330B RID: 13067 RVA: 0x000D7589 File Offset: 0x000D5789
	public void OnDisable()
	{
		if (Items.MistTorch)
		{
			InstantiateUtility.Destroy(Items.MistTorch.gameObject);
		}
	}

	// Token: 0x0600330C RID: 13068 RVA: 0x000D75A9 File Offset: 0x000D57A9
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawTextFilled(base.transform, "MistTorch Placeholder", false);
	}

	// Token: 0x0600330D RID: 13069 RVA: 0x000D75BC File Offset: 0x000D57BC
	public void OnDrawGizmosSelected()
	{
		GizmoHelper.DrawSelectedTextFilled(base.transform, "MistTorch Placeholder", false);
	}

	// Token: 0x04002E01 RID: 11777
	public GameObject MistTorchPrefab;

	// Token: 0x04002E02 RID: 11778
	private float m_remainingTime;
}
