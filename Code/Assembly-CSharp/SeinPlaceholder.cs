using System;
using Game;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class SeinPlaceholder : MonoBehaviour, IDynamicGraphic
{
	// Token: 0x06000778 RID: 1912 RVA: 0x0001F18C File Offset: 0x0001D38C
	public void Awake()
	{
		Events.Scheduler.OnSceneRootPreEnabled.Add(new Action<SceneRoot>(this.OnSceneRootPreEnabled));
		if (this.SceneRoot == null)
		{
			this.SceneRoot = SceneRoot.FindFromTransform(base.transform);
		}
		if (base.GetComponent<Renderer>())
		{
			base.GetComponent<Renderer>().enabled = false;
		}
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0001F1F2 File Offset: 0x0001D3F2
	public void OnDestroy()
	{
		Events.Scheduler.OnSceneRootPreEnabled.Remove(new Action<SceneRoot>(this.OnSceneRootPreEnabled));
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0001F20F File Offset: 0x0001D40F
	public void OnSceneRootPreEnabled(SceneRoot sceneRoot)
	{
		if (sceneRoot == this.SceneRoot)
		{
			this.Spawn();
		}
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0001F228 File Offset: 0x0001D428
	private void Spawn()
	{
		if (GoToSceneController.CheckStartInScene(this.SceneRoot.MetaData.SceneMoonGuid))
		{
			if (this.SceneRoot.name != "introLogos")
			{
				GameController.Instance.MainMenuCanBeOpened = true;
			}
			if (Characters.Current as Component == null)
			{
				CharacterFactory.Instance.SpawnCharacter(this.Character, this.CharacterPrefab, base.transform.position, new Action(this.AfterLoadingFromMasterFinishedAfterInstantiation));
				UI.Cameras.Current.Target = Characters.Current.Transform;
				UI.Cameras.Current.MoveCameraToTargetInstantly(true);
				Characters.Current.FaceLeft = (base.transform.localScale.x < 0f);
			}
			if (this.GameplaySystems)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.GameplaySystems);
			}
		}
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0001F318 File Offset: 0x0001D518
	private void AfterLoadingFromMasterFinishedAfterInstantiation()
	{
		if (Characters.Current != null)
		{
			Characters.Current.Activate(true);
			if (!GameController.Instance.IsLoadingGame)
			{
				Characters.Current.Position = base.transform.position;
			}
			Rect rect = new Rect(-1202f, -195f, 584f, 254f);
			if (!rect.Contains(Characters.Current.Position))
			{
				Characters.Current.PlaceOnGround();
			}
		}
		if (Characters.Sein && Characters.Sein.Prefabs)
		{
			Characters.Sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
		}
		if (GameController.Instance.RequireInitialValues)
		{
			GameController.Instance.RequireInitialValues = false;
			if (!GameController.Instance.IsLoadingGame)
			{
				GameController.Instance.SetupGameplay(this.SceneRoot, base.GetComponent<WorldEventsOnAwake>());
			}
		}
	}

	// Token: 0x040005A0 RID: 1440
	public CharacterFactory.Characters Character;

	// Token: 0x040005A1 RID: 1441
	public GameObject GameplaySystems;

	// Token: 0x040005A2 RID: 1442
	[HideInInspector]
	public SceneRoot SceneRoot;

	// Token: 0x040005A3 RID: 1443
	public GameObject CharacterPrefab;
}
