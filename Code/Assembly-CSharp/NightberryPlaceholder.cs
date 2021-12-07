using System;
using System.Collections;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x020008FE RID: 2302
[ExecuteInEditMode]
public class NightberryPlaceholder : MonoBehaviour
{
	// Token: 0x0600332B RID: 13099 RVA: 0x000D7B54 File Offset: 0x000D5D54
	public void FixedUpdate()
	{
		if (Sein.World.Events.GravityActivated)
		{
			return;
		}
		if (Items.NightBerry == null)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.NightberryPrefab, base.transform.position, Quaternion.identity);
			SaveSceneManager.Master.RegisterGameObject(gameObject);
			gameObject.AddComponent<LoadFromMasterAtStart>();
			base.StartCoroutine(this.HackForFixingNightberry());
		}
	}

	// Token: 0x0600332C RID: 13100 RVA: 0x000D7BBC File Offset: 0x000D5DBC
	public IEnumerator HackForFixingNightberry()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		if (Items.NightBerry && Vector3.Distance(Items.NightBerry.Position, new Vector2(-792f, -357f)) < 3f && !base.transform.root.Find("*storySetups").Find("storyTextWithTrigger").Find("triggerCollider").GetComponent<PlayerCollisionTrigger>().Active)
		{
			Items.NightBerry.Position = new Vector2(-756f, -406f);
		}
		yield break;
	}

	// Token: 0x0600332D RID: 13101 RVA: 0x000D7BD7 File Offset: 0x000D5DD7
	public void OnDrawGizmos()
	{
		GizmoHelper.DrawTextFilled(base.transform, "Nightberry Placeholder", false);
	}

	// Token: 0x0600332E RID: 13102 RVA: 0x000D7BEA File Offset: 0x000D5DEA
	public void OnDrawGizmosSelected()
	{
		GizmoHelper.DrawSelectedTextFilled(base.transform, "Nightberry Placeholder", false);
	}

	// Token: 0x0600332F RID: 13103 RVA: 0x000D7C00 File Offset: 0x000D5E00
	public void Update()
	{
		if (!Application.isPlaying)
		{
			Shader.SetGlobalVector("_NightberryPosition", new Vector4(base.transform.position.x, base.transform.position.y, 4f, 6f));
		}
	}

	// Token: 0x04002E27 RID: 11815
	public GameObject NightberryPrefab;
}
