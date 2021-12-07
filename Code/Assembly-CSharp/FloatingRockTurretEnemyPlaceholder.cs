using System;
using UnityEngine;

// Token: 0x020005A8 RID: 1448
public class FloatingRockTurretEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600250C RID: 9484 RVA: 0x000A1A00 File Offset: 0x0009FC00
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.FloatingRockTurretEnemy, base.transform.position, base.transform.rotation);
		FloatingRockTurretEnemy component = gameObject.GetComponent<FloatingRockTurretEnemy>();
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		component.Settings.ChargeDuration = this.Settings.ChargeDuration;
		component.Settings.ShootingDuration = this.Settings.ShootingDuration;
		component.Settings.ShootingForce = this.Settings.ShootingForce;
		component.Settings.SpringForce = this.Settings.SpringForce;
		component.Settings.Drag = this.Settings.Drag;
		component.Settings.DisolveDistance = this.Settings.DisolveDistance;
		component.Settings.ProjectileSpeed = this.Settings.ProjectileSpeed;
		component.Settings.ProjectileDamage = this.Settings.ProjectileDamage;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.FloatingRockTurretEnemy.name;
		component.GetComponentInChildren<SeeSeinTrigger>().TriggerDistance = this.Settings.TriggerDistance;
		return component;
	}

	// Token: 0x04001F7D RID: 8061
	public FloatingRockTurretEnemyPlaceholder.FloatingRockLaserEnemySettings Settings;

	// Token: 0x04001F7E RID: 8062
	[AssetReferenceChoice("Floating Rock Type", new string[]
	{
		"Assets\\frameworks\\enemies\\rockEnemy\\floatingRockTurretEnemy\\prefabs\\general\\floatTurretEnemy.prefab",
		"Assets\\frameworks\\enemies\\rockEnemy\\floatingRockTurretEnemy\\textures\\floatingRockTurretEnemyPlaceholder.png",
		"Regular Floating Rock",
		"Assets\\frameworks\\enemies\\rockEnemy\\floatingRockTurretEnemy\\prefabs\\fire\\floatTurretEnemyFire.prefab",
		"Assets\\frameworks\\enemies\\rockEnemy\\floatingRockTurretEnemy\\textures\\floatingRockTurretEnemyFirePlaceholder.png",
		"Fire Floating Rock",
		"Assets\\frameworks\\enemies\\rockEnemy\\floatingRockTurretEnemy\\prefabs\\wood\\floatTurretEnemyWood.prefab",
		"Assets\\frameworks\\enemies\\rockEnemy\\floatingRockTurretEnemy\\textures\\floatingRockTurretEnemyWoodPlaceholder.png",
		"Wooden Floating Rock"
	})]
	public GameObject FloatingRockTurretEnemy;

	// Token: 0x020005A9 RID: 1449
	[Serializable]
	public class FloatingRockLaserEnemySettings
	{
		// Token: 0x04001F7F RID: 8063
		public float Health = 5f;

		// Token: 0x04001F80 RID: 8064
		public float DamageOnTouch = 4f;

		// Token: 0x04001F81 RID: 8065
		public float ProjectileDamage = 5f;

		// Token: 0x04001F82 RID: 8066
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001F83 RID: 8067
		public DropLootSettings LootSettings;

		// Token: 0x04001F84 RID: 8068
		public float ChargeDuration;

		// Token: 0x04001F85 RID: 8069
		public float ShootingDuration;

		// Token: 0x04001F86 RID: 8070
		public float ShootingForce;

		// Token: 0x04001F87 RID: 8071
		public float SpringForce;

		// Token: 0x04001F88 RID: 8072
		public float Drag;

		// Token: 0x04001F89 RID: 8073
		public float DisolveDistance;

		// Token: 0x04001F8A RID: 8074
		public float TriggerDistance = 15f;

		// Token: 0x04001F8B RID: 8075
		public float ProjectileSpeed;
	}
}
