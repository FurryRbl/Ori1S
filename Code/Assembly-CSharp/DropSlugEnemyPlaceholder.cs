using System;
using UnityEngine;

// Token: 0x0200051C RID: 1308
public class DropSlugEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x060022D1 RID: 8913 RVA: 0x00098844 File Offset: 0x00096A44
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.DropOwlEnemy, base.transform.position, base.transform.rotation);
		DropSlugEnemy component = gameObject.GetComponent<DropSlugEnemy>();
		component.Settings.AlertRange = this.Settings.AlertRange;
		component.Settings.FallRange = this.Settings.FallRange;
		component.Settings.Speed = this.Settings.Speed;
		component.Settings.ExplosionDamage = this.Settings.ExplosionDamage;
		component.DamageDealer.Damage = this.Settings.ExplosionDamage;
		component.GetComponent<SeeSeinTrigger>().TriggerDistance = this.Settings.DropDistance;
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.DropOwlEnemy.name;
		return component;
	}

	// Token: 0x04001D47 RID: 7495
	public DropSlugEnemyPlaceholder.DropOwlPlaceholderSettings Settings;

	// Token: 0x04001D48 RID: 7496
	public GameObject DropOwlEnemy;

	// Token: 0x0200051D RID: 1309
	[Serializable]
	public class DropOwlPlaceholderSettings
	{
		// Token: 0x04001D49 RID: 7497
		public float Health = 20f;

		// Token: 0x04001D4A RID: 7498
		public float DamageOnTouch = 5f;

		// Token: 0x04001D4B RID: 7499
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001D4C RID: 7500
		public DropLootSettings LootSettings;

		// Token: 0x04001D4D RID: 7501
		public float AlertRange = 10f;

		// Token: 0x04001D4E RID: 7502
		public float FallRange = 3f;

		// Token: 0x04001D4F RID: 7503
		public float DropDistance = 20f;

		// Token: 0x04001D50 RID: 7504
		public float Speed = 40f;

		// Token: 0x04001D51 RID: 7505
		public float ExplosionDamage = 10f;
	}
}
