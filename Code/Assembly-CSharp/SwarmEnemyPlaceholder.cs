using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005F0 RID: 1520
public class SwarmEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x06002627 RID: 9767 RVA: 0x000A77F4 File Offset: 0x000A59F4
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.SwarmEnemy, base.transform.position, base.transform.rotation);
		SwarmEnemy component = gameObject.GetComponent<SwarmEnemy>();
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		component.Owner = this;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.TotalExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = component.name;
		return component;
	}

	// Token: 0x06002628 RID: 9768 RVA: 0x000A78C1 File Offset: 0x000A5AC1
	public void OnChildComponentSpawned(SwarmEnemy swarmEnemy)
	{
		this.m_childComponentEnemies.Add(swarmEnemy.GetInstanceID());
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x000A78D4 File Offset: 0x000A5AD4
	public void OnChildComponentDestroy(SwarmEnemy swarmEnemy)
	{
		this.m_childComponentEnemies.Remove(swarmEnemy.GetInstanceID());
	}

	// Token: 0x17000617 RID: 1559
	// (get) Token: 0x0600262A RID: 9770 RVA: 0x000A78E8 File Offset: 0x000A5AE8
	public override bool NeedsToRespawn
	{
		get
		{
			return base.NeedsToRespawn && this.m_childComponentEnemies.Count == 0;
		}
	}

	// Token: 0x040020C4 RID: 8388
	public SwarmEnemyPlaceholder.SwarmEnemyPlaceholderSettings Settings;

	// Token: 0x040020C5 RID: 8389
	[AssetReferenceChoice("Swarm Type", new string[]
	{
		"Assets\\frameworks\\enemies\\swarm\\prefabs\\normal\\swarmEnemyLarge.prefab",
		"Assets\\frameworks\\enemies\\swarm\\textures\\swarmEnemyPlaceholder.png",
		"Regular Swarm",
		"Assets\\frameworks\\enemies\\swarm\\prefabs\\fire\\swarmEnemyLargeFire.prefab",
		"Assets\\frameworks\\enemies\\swarm\\textures\\swarmEnemyFirePlaceholder.png",
		"Fire Swarm"
	})]
	public GameObject SwarmEnemy;

	// Token: 0x040020C6 RID: 8390
	private List<int> m_childComponentEnemies = new List<int>();

	// Token: 0x020005F4 RID: 1524
	[Serializable]
	public class SwarmEnemyPlaceholderSettings
	{
		// Token: 0x040020D5 RID: 8405
		public float Health = 20f;

		// Token: 0x040020D6 RID: 8406
		public int TotalExpOrbsToDrop = 30;

		// Token: 0x040020D7 RID: 8407
		public float DamageOnTouch = 5f;

		// Token: 0x040020D8 RID: 8408
		public DropLootSettings LootSettings;
	}
}
