using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020005F1 RID: 1521
public class SwarmEnemyManager : MonoBehaviour
{
	// Token: 0x0600262C RID: 9772 RVA: 0x000A7919 File Offset: 0x000A5B19
	private void Awake()
	{
		SwarmEnemyManager.Instance = this;
		Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnGameSerializeLoad));
	}

	// Token: 0x0600262D RID: 9773 RVA: 0x000A793C File Offset: 0x000A5B3C
	private void OnDestroy()
	{
		Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnGameSerializeLoad));
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x000A7959 File Offset: 0x000A5B59
	public void OnGameSerializeLoad()
	{
		this.m_spawnQueue.Clear();
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x000A7968 File Offset: 0x000A5B68
	public void QueueSpawn(Vector3 pos, Vector3 velocity, int lootAmount, OrbSpawner spawner, float damageOnTouch, GameObject child, MoonGuid sceneRoot, SwarmEnemyPlaceholder owner)
	{
		SwarmEnemyManager.SpawnInfo item = new SwarmEnemyManager.SpawnInfo
		{
			Pos = pos,
			Velocity = velocity,
			SceneRoot = sceneRoot,
			LootAmount = lootAmount,
			DamageOnTouch = damageOnTouch,
			OrbSpawner = spawner,
			Child = child,
			Owner = owner
		};
		this.m_spawnQueue.Enqueue(item);
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x000A79D4 File Offset: 0x000A5BD4
	private void Update()
	{
		UberSpawnManager instance = UberSpawnManager.Instance;
		while (this.m_spawnQueue.Count > 0 && instance.GetSpawnOk())
		{
			instance.StartSpawn();
			SwarmEnemyManager.SpawnInfo spawnInfo = this.m_spawnQueue.Dequeue();
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(spawnInfo.Child, spawnInfo.Pos, Quaternion.identity);
			SwarmEnemy component = gameObject.GetComponent<SwarmEnemy>();
			component.SetSceneRoot(spawnInfo.SceneRoot);
			component.PlatformMovement.LocalSpeed = spawnInfo.Velocity;
			component.SetModeToSpawned();
			component.Loot.LootAmount = (float)spawnInfo.LootAmount;
			component.DamageDealer.Damage = spawnInfo.DamageOnTouch;
			component.OrbSpawner.SetNumberOfExpOrbs(spawnInfo.LootAmount);
			component.OrbSpawner.LimitNumberOfOrbs(1);
			component.Owner = spawnInfo.Owner;
			spawnInfo.Owner.OnChildComponentSpawned(component);
			gameObject.GetComponent<DestroyOnRestoreCheckpoint>().enabled = true;
			gameObject.GetComponent<DestroyWhenOutsideActiveBoundaries>().enabled = true;
			instance.StopSpawn();
		}
	}

	// Token: 0x040020C7 RID: 8391
	public static SwarmEnemyManager Instance;

	// Token: 0x040020C8 RID: 8392
	private Queue<SwarmEnemyManager.SpawnInfo> m_spawnQueue = new Queue<SwarmEnemyManager.SpawnInfo>();

	// Token: 0x020005F2 RID: 1522
	private struct SpawnInfo
	{
		// Token: 0x040020C9 RID: 8393
		public Vector3 Pos;

		// Token: 0x040020CA RID: 8394
		public Vector3 Velocity;

		// Token: 0x040020CB RID: 8395
		public MoonGuid SceneRoot;

		// Token: 0x040020CC RID: 8396
		public int LootAmount;

		// Token: 0x040020CD RID: 8397
		public OrbSpawner OrbSpawner;

		// Token: 0x040020CE RID: 8398
		public GameObject Child;

		// Token: 0x040020CF RID: 8399
		public float DamageOnTouch;

		// Token: 0x040020D0 RID: 8400
		public SwarmEnemyPlaceholder Owner;
	}
}
