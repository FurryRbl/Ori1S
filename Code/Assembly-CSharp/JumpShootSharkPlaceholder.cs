using System;
using UnityEngine;

// Token: 0x020005B1 RID: 1457
public class JumpShootSharkPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600252A RID: 9514 RVA: 0x000A22B8 File Offset: 0x000A04B8
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab, base.transform.position, base.transform.rotation);
		JumpShootShark component = gameObject.GetComponent<JumpShootShark>();
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		component.Settings.JumpHeight = this.Settings.JumpHeight;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.Prefab.name;
		return component;
	}

	// Token: 0x04001FB1 RID: 8113
	public JumpShootSharkPlaceholder.JumpShootSharkPlaceholderSettings Settings;

	// Token: 0x04001FB2 RID: 8114
	[AssetReferenceChoice("Shark Type", new string[]
	{
		"Assets\\frameworks\\enemies\\shark\\prefabs\\jumpShootSharkEnemy.prefab",
		"Assets\\frameworks\\enemies\\shark\\texture\\jumpShootSharkPlaceholder.png",
		"Regular Shark",
		"Assets\\frameworks\\enemies\\shark\\prefabs\\jumpShootSharkEnemyFire.prefab",
		"Assets\\frameworks\\enemies\\shark\\texture\\jumpShootSharkFirePlaceholder.png",
		"Fire Shark"
	})]
	public GameObject Prefab;

	// Token: 0x020005B2 RID: 1458
	[Serializable]
	public class JumpShootSharkPlaceholderSettings
	{
		// Token: 0x04001FB3 RID: 8115
		public float Health = 20f;

		// Token: 0x04001FB4 RID: 8116
		public float DamageOnTouch = 5f;

		// Token: 0x04001FB5 RID: 8117
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001FB6 RID: 8118
		public DropLootSettings LootSettings;

		// Token: 0x04001FB7 RID: 8119
		public float JumpHeight = 12f;
	}
}
