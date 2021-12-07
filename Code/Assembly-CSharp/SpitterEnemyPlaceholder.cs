using System;
using UnityEngine;

// Token: 0x020005E9 RID: 1513
public class SpitterEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600260B RID: 9739 RVA: 0x000A6CA4 File Offset: 0x000A4EA4
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.ChargingSootEnemy, base.transform.position, base.transform.rotation);
		SpitterEnemy component = gameObject.GetComponent<SpitterEnemy>();
		component.Settings.WalkSpeed = this.Settings.WalkSpeed;
		component.Settings.RunSpeed = this.Settings.RunSpeed;
		component.Settings.IdleDuration = this.Settings.IdleDuration;
		component.Settings.WalkDuration = this.Settings.WalkDuration;
		component.Settings.SeePlayerDistance = this.Settings.SeePlayerDistance;
		component.Settings.MinChargeDistance = this.Settings.MinChargeDistance;
		component.Settings.ProjectileSpeed = this.Settings.ProjectileSpeed;
		component.Settings.Gravity = this.Settings.Gravity;
		component.Settings.ProjectileGravity = this.Settings.ProjectileGravity;
		component.Settings.ProjectileDamage = this.Settings.ProjectileDamage;
		component.FaceLeft = (base.transform.localScale.x < 0f);
		component.Settings.SpreadShot = this.Settings.SpreadShot;
		component.ActionZones = this.Settings.ActionZones;
		component.EnterZoneAction = this.Settings.EnterZoneAction;
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.ChargingSootEnemy.name;
		return component;
	}

	// Token: 0x0400208B RID: 8331
	public SpitterEnemyPlaceholder.ChargingSootEnemyPlaceholderSettings Settings;

	// Token: 0x0400208C RID: 8332
	[AssetReferenceChoice("Spitter Type", new string[]
	{
		"Assets\\frameworks\\enemies\\spitter\\prefabs\\regular\\spitterEnemy.prefab",
		"Assets\\frameworks\\enemies\\spitter\\textures\\spitterEnemyPlaceholder.png",
		"Regular Spitter",
		"Assets\\frameworks\\enemies\\spitter\\prefabs\\fast\\fastSpitterEnemy.prefab",
		"Assets\\frameworks\\enemies\\spitter\\textures\\fastSpitterEnemyPlaceholder.png",
		"Fast Spitter"
	})]
	public GameObject ChargingSootEnemy;

	// Token: 0x020005EA RID: 1514
	[Serializable]
	public class ChargingSootEnemyPlaceholderSettings
	{
		// Token: 0x0400208D RID: 8333
		public float Health = 20f;

		// Token: 0x0400208E RID: 8334
		public float DamageOnTouch = 5f;

		// Token: 0x0400208F RID: 8335
		public float ProjectileDamage = 20f;

		// Token: 0x04002090 RID: 8336
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04002091 RID: 8337
		public DropLootSettings LootSettings;

		// Token: 0x04002092 RID: 8338
		public float WalkSpeed = 3f;

		// Token: 0x04002093 RID: 8339
		public float RunSpeed = 10f;

		// Token: 0x04002094 RID: 8340
		public float IdleDuration = 1f;

		// Token: 0x04002095 RID: 8341
		public float WalkDuration = 5f;

		// Token: 0x04002096 RID: 8342
		public float SeePlayerDistance = 10f;

		// Token: 0x04002097 RID: 8343
		public float MinChargeDistance = 8f;

		// Token: 0x04002098 RID: 8344
		public float ProjectileSpeed = 10f;

		// Token: 0x04002099 RID: 8345
		public float Gravity = 30f;

		// Token: 0x0400209A RID: 8346
		public float ProjectileGravity;

		// Token: 0x0400209B RID: 8347
		public bool SpreadShot;

		// Token: 0x0400209C RID: 8348
		public Transform[] ActionZones;

		// Token: 0x0400209D RID: 8349
		public ActionMethod EnterZoneAction;
	}
}
