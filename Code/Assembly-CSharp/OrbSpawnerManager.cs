using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200062E RID: 1582
public class OrbSpawnerManager : MonoBehaviour
{
	// Token: 0x060026F9 RID: 9977 RVA: 0x000AA531 File Offset: 0x000A8731
	public void OnGameReset()
	{
		this.Items.Clear();
	}

	// Token: 0x060026FA RID: 9978 RVA: 0x000AA53E File Offset: 0x000A873E
	public void Spawn(OrbSpawnerManager.ItemType itemType, Vector2 position, Vector2 velocity, DropPickup.State initialState)
	{
		this.Items.Enqueue(new OrbSpawnerManager.ItemToSpawn(itemType, position, velocity, initialState));
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x000AA555 File Offset: 0x000A8755
	public void Awake()
	{
		OrbSpawnerManager.Instance = this;
		Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnGameSerializeLoad));
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x000AA594 File Offset: 0x000A8794
	public void OnDestroy()
	{
		if (OrbSpawnerManager.Instance == this)
		{
			OrbSpawnerManager.Instance = null;
		}
		Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnGameSerializeLoad));
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x000AA5ED File Offset: 0x000A87ED
	public void OnGameSerializeLoad()
	{
		this.Items.Clear();
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x000AA5FC File Offset: 0x000A87FC
	public void FixedUpdate()
	{
		UberSpawnManager instance = UberSpawnManager.Instance;
		while (this.Items.Count > 0 && instance.GetSpawnOk())
		{
			instance.StartSpawn();
			OrbSpawnerManager.ItemToSpawn itemToSpawn = this.Items.Dequeue();
			GameObject gameObject = null;
			switch (itemToSpawn.ItemType)
			{
			case OrbSpawnerManager.ItemType.GreenExpOrb:
				gameObject = this.GreenExpOrb;
				break;
			case OrbSpawnerManager.ItemType.BlueExpOrb:
				gameObject = this.BlueExpOrb;
				break;
			case OrbSpawnerManager.ItemType.RedExpOrb:
				gameObject = this.RedExpOrb;
				break;
			case OrbSpawnerManager.ItemType.YellowExpOrb:
				gameObject = this.YellowExpOrb;
				break;
			case OrbSpawnerManager.ItemType.Energy:
				gameObject = this.EnergyPickup;
				break;
			case OrbSpawnerManager.ItemType.Health:
				gameObject = this.HealthPickup;
				break;
			}
			if (gameObject)
			{
				GameObject gameObject2 = (GameObject)InstantiateUtility.Instantiate(gameObject, itemToSpawn.Position, Quaternion.identity);
				gameObject2.GetComponent<Rigidbody>().velocity = itemToSpawn.Velocity;
				gameObject2.GetComponent<DropPickup>().CurrentState = itemToSpawn.InitialState;
			}
			instance.StopSpawn();
		}
	}

	// Token: 0x04002192 RID: 8594
	public static OrbSpawnerManager Instance;

	// Token: 0x04002193 RID: 8595
	public GameObject GreenExpOrb;

	// Token: 0x04002194 RID: 8596
	public GameObject BlueExpOrb;

	// Token: 0x04002195 RID: 8597
	public GameObject YellowExpOrb;

	// Token: 0x04002196 RID: 8598
	public GameObject RedExpOrb;

	// Token: 0x04002197 RID: 8599
	public GameObject EnergyPickup;

	// Token: 0x04002198 RID: 8600
	public GameObject HealthPickup;

	// Token: 0x04002199 RID: 8601
	public Queue<OrbSpawnerManager.ItemToSpawn> Items = new Queue<OrbSpawnerManager.ItemToSpawn>(50);

	// Token: 0x0200062F RID: 1583
	public enum ItemType
	{
		// Token: 0x0400219B RID: 8603
		GreenExpOrb,
		// Token: 0x0400219C RID: 8604
		BlueExpOrb,
		// Token: 0x0400219D RID: 8605
		RedExpOrb,
		// Token: 0x0400219E RID: 8606
		YellowExpOrb,
		// Token: 0x0400219F RID: 8607
		Energy,
		// Token: 0x040021A0 RID: 8608
		Health
	}

	// Token: 0x02000630 RID: 1584
	public struct ItemToSpawn
	{
		// Token: 0x060026FF RID: 9983 RVA: 0x000AA710 File Offset: 0x000A8910
		public ItemToSpawn(OrbSpawnerManager.ItemType itemType, Vector2 position, Vector2 velocity, DropPickup.State initialState)
		{
			this.ItemType = itemType;
			this.Position = position;
			this.Velocity = velocity;
			this.InitialState = initialState;
		}

		// Token: 0x040021A1 RID: 8609
		public OrbSpawnerManager.ItemType ItemType;

		// Token: 0x040021A2 RID: 8610
		public Vector2 Position;

		// Token: 0x040021A3 RID: 8611
		public Vector2 Velocity;

		// Token: 0x040021A4 RID: 8612
		public DropPickup.State InitialState;
	}
}
