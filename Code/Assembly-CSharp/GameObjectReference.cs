using System;
using UnityEngine;

// Token: 0x02000489 RID: 1161
public class GameObjectReference
{
	// Token: 0x06001FA3 RID: 8099 RVA: 0x0008B279 File Offset: 0x00089479
	public GameObjectReference(GameObject gameObject)
	{
		this.GameObject = gameObject;
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x0008B288 File Offset: 0x00089488
	public static GameObject GetReference(GameObjectReference gameObjectReference)
	{
		if (gameObjectReference == null)
		{
			return null;
		}
		return gameObjectReference.GameObject;
	}

	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x0008B2A1 File Offset: 0x000894A1
	// (set) Token: 0x06001FA5 RID: 8101 RVA: 0x0008B298 File Offset: 0x00089498
	public GameObject GameObject { get; private set; }

	// Token: 0x06001FA7 RID: 8103 RVA: 0x0008B2AC File Offset: 0x000894AC
	public static GameObjectReference MakeReference(GameObject gameObject)
	{
		GameObjectReferenceComponent gameObjectReferenceComponent = gameObject.GetComponent<GameObjectReferenceComponent>();
		if (gameObjectReferenceComponent)
		{
			return gameObjectReferenceComponent.GameObjectReference;
		}
		gameObjectReferenceComponent = gameObject.AddComponent<GameObjectReferenceComponent>();
		gameObjectReferenceComponent.GameObjectReference = new GameObjectReference(gameObject);
		return gameObjectReferenceComponent.GameObjectReference;
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x0008B2EC File Offset: 0x000894EC
	public static void ChangeReference(GameObject oldGameObject, GameObject newGameObject)
	{
		GameObjectReference gameObjectReference = GameObjectReference.MakeReference(oldGameObject);
		gameObjectReference.GameObject = newGameObject;
		newGameObject.AddComponent<GameObjectReferenceComponent>().GameObjectReference = gameObjectReference;
	}
}
