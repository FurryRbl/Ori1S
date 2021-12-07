using System;
using System.Collections;
using Game;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class GameplaySystemsCreator : MonoBehaviour
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x0001C50A File Offset: 0x0001A70A
	public void Awake()
	{
		base.StartCoroutine(this.CreateSetups());
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0001C51C File Offset: 0x0001A71C
	public IEnumerator CreateSetups()
	{
		if (Characters.Ori == null)
		{
			GameObject oriGameObject = (GameObject)UnityEngine.Object.Instantiate(this.OriPrefab, Vector3.zero, Quaternion.identity);
			Utility.DontAssociateWithAnyScene(oriGameObject);
			oriGameObject.name = this.OriPrefab.name;
			SaveSceneManager.Master.RegisterGameObject(oriGameObject);
			oriGameObject.AddComponent<LoadFromMasterAtStart>();
		}
		if (UI.SeinUI == null)
		{
			GameObject go = (GameObject)UnityEngine.Object.Instantiate(this.SeinUIPrefab, Vector3.zero, Quaternion.identity);
			Utility.DontAssociateWithAnyScene(go);
			go.name = this.SeinUIPrefab.name;
		}
		if (WorldMapLogic.Instance == null)
		{
			GameObject worldMapLogicGameObject = (GameObject)UnityEngine.Object.Instantiate(this.WorldMapLogicPrefab, Vector3.zero, Quaternion.identity);
			Utility.DontAssociateWithAnyScene(worldMapLogicGameObject);
			worldMapLogicGameObject.name = this.WorldMapLogicPrefab.name;
			SaveSceneManager.Master.RegisterGameObject(worldMapLogicGameObject);
			worldMapLogicGameObject.AddComponent<LoadFromMasterAtStart>();
		}
		yield return new WaitForEndOfFrame();
		if (AreaMapUI.Instance == null)
		{
			UI.Menu.SetupWorldMapUI(this.WorldMapUIPrefab);
			WorldMapUI.Initialize();
			yield return new WaitForEndOfFrame();
		}
		if (SkillTreeManager.Instance == null)
		{
			UI.Menu.SetupSkillTreeUI(this.SkillTreeUIPrefab);
			yield return new WaitForEndOfFrame();
		}
		if (InventoryManager.Instance == null)
		{
			UI.Menu.SetupInventoryUI(this.InventoryPrefab);
			yield return new WaitForEndOfFrame();
		}
		InstantiateUtility.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0400051B RID: 1307
	public GameObject OriPrefab;

	// Token: 0x0400051C RID: 1308
	public GameObject WorldMapLogicPrefab;

	// Token: 0x0400051D RID: 1309
	public GameObject WorldMapUIPrefab;

	// Token: 0x0400051E RID: 1310
	public GameObject SkillTreeUIPrefab;

	// Token: 0x0400051F RID: 1311
	public GameObject InventoryPrefab;

	// Token: 0x04000520 RID: 1312
	public GameObject SeinUIPrefab;
}
