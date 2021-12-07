using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000096 RID: 150
[ExecuteInEditMode]
public class GamePlaceholder : MonoBehaviour, IDynamicGraphic
{
	// Token: 0x06000630 RID: 1584 RVA: 0x0001829B File Offset: 0x0001649B
	public void Start()
	{
		if (!Application.isPlaying)
		{
			return;
		}
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x000182A8 File Offset: 0x000164A8
	public void FixedUpdate()
	{
		if (this.SceneRoot == null)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x000182C8 File Offset: 0x000164C8
	public void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.SceneRoot == null)
		{
			this.SceneRoot = SceneRoot.FindFromTransform(base.transform);
		}
		base.GetComponent<Renderer>().enabled = false;
		if (GoToSceneController.CheckStartInScene(this.SceneRoot.MetaData.SceneMoonGuid) && GameController.Instance == null && this.GameControllerPrefab != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GameControllerPrefab);
			gameObject.name = this.GameControllerPrefab.name;
			SaveSceneManager.Master.RegisterGameObject(gameObject);
			Vector3 position = base.transform.position;
			Scenes.Manager.SetTargetPositions(position);
			UI.Cameras.Current.CameraTarget.SetTargetPosition(position);
			gameObject.GetComponent<GameController>().AtlasPlatform = this.AtlasPlatform;
		}
	}

	// Token: 0x040004C0 RID: 1216
	[HideInInspector]
	public UberAtlassingPlatform AtlasPlatform;

	// Token: 0x040004C1 RID: 1217
	[HideInInspector]
	public SceneRoot SceneRoot;

	// Token: 0x040004C2 RID: 1218
	public GameObject GameControllerPrefab;
}
