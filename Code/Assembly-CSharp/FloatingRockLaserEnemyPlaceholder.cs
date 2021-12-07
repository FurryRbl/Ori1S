using System;
using UnityEngine;

// Token: 0x020005A1 RID: 1441
public class FloatingRockLaserEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x060024EE RID: 9454 RVA: 0x000A0EF0 File Offset: 0x0009F0F0
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.FloatingRockLaserEnemy, base.transform.position, base.transform.rotation);
		FloatingRockLaserEnemy component = gameObject.GetComponent<FloatingRockLaserEnemy>();
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		component.Settings.ChargeDuration = this.Settings.ChargeDuration;
		component.Settings.ShootingDuration = this.Settings.ShootingDuration;
		component.Settings.ShootingForce = this.Settings.ShootingForce;
		component.Settings.LaserForce = this.Settings.LaserForce;
		component.Settings.SpringForce = this.Settings.SpringForce;
		component.Settings.Drag = this.Settings.Drag;
		component.Settings.ProjectileSpeed = this.Settings.ProjectileSpeed;
		component.Settings.LaserDuration = this.Settings.LaserDuration;
		component.Settings.LaserChaseSpeed = this.Settings.LaserChaseSpeed;
		component.Settings.LaserChaseSpeedDistance = this.Settings.LaserChaseSpeedDistance;
		component.Settings.LaserLength = this.Settings.LaserLength;
		component.Settings.LaserDamage = this.Settings.LaserDamage;
		component.Settings.CloseDistance = this.Settings.CloseDistance;
		component.Settings.LaserAngularOffset = this.Settings.LaserAngularOffset;
		component.Settings.ProjectileDamage = this.Settings.ProjectileDamage;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.FloatingRockLaserEnemy.name;
		gameObject.GetComponentInChildren<SeeSeinTrigger>().TriggerDistance = this.Settings.TriggerDistance;
		return component;
	}

	// Token: 0x04001F40 RID: 8000
	public FloatingRockLaserEnemyPlaceholder.FloatingRockLaserEnemySettings Settings;

	// Token: 0x04001F41 RID: 8001
	public GameObject FloatingRockLaserEnemy;

	// Token: 0x020005A2 RID: 1442
	[Serializable]
	public class FloatingRockLaserEnemySettings
	{
		// Token: 0x04001F42 RID: 8002
		public float Health = 5f;

		// Token: 0x04001F43 RID: 8003
		public float DamageOnTouch;

		// Token: 0x04001F44 RID: 8004
		public float ProjectileDamage = 6f;

		// Token: 0x04001F45 RID: 8005
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001F46 RID: 8006
		public DropLootSettings LootSettings;

		// Token: 0x04001F47 RID: 8007
		public float ChargeDuration;

		// Token: 0x04001F48 RID: 8008
		public float ShootingDuration;

		// Token: 0x04001F49 RID: 8009
		public float ShootingForce;

		// Token: 0x04001F4A RID: 8010
		public float LaserForce;

		// Token: 0x04001F4B RID: 8011
		public float SpringForce;

		// Token: 0x04001F4C RID: 8012
		public float Drag;

		// Token: 0x04001F4D RID: 8013
		public float ProjectileSpeed;

		// Token: 0x04001F4E RID: 8014
		public float LaserDuration;

		// Token: 0x04001F4F RID: 8015
		public float LaserChaseSpeed;

		// Token: 0x04001F50 RID: 8016
		public float LaserChaseSpeedDistance = 10f;

		// Token: 0x04001F51 RID: 8017
		public float LaserLength;

		// Token: 0x04001F52 RID: 8018
		public float LaserDamage;

		// Token: 0x04001F53 RID: 8019
		public float CloseDistance = 5f;

		// Token: 0x04001F54 RID: 8020
		public float TriggerDistance = 20f;

		// Token: 0x04001F55 RID: 8021
		public float LaserAngularOffset = 20f;
	}
}
