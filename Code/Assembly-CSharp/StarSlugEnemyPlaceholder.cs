using System;
using UnityEngine;

// Token: 0x020005CC RID: 1484
public class StarSlugEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600256D RID: 9581 RVA: 0x000A3508 File Offset: 0x000A1708
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.StarSlugEnemy, base.transform.position, base.transform.rotation);
		StarSlugEnemy component = gameObject.GetComponent<StarSlugEnemy>();
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.StarSlugEnemy.name;
		return component;
	}

	// Token: 0x04001FFC RID: 8188
	public StarSlugEnemyPlaceholder.StarSlugEnemyPlaceholderSettings Settings;

	// Token: 0x04001FFD RID: 8189
	public GameObject StarSlugEnemy;

	// Token: 0x020005CD RID: 1485
	[Serializable]
	public class StarSlugEnemyPlaceholderSettings
	{
		// Token: 0x04001FFE RID: 8190
		public float Health = 20f;

		// Token: 0x04001FFF RID: 8191
		public float DamageOnTouch = 2f;

		// Token: 0x04002000 RID: 8192
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04002001 RID: 8193
		public DropLootSettings LootSettings;

		// Token: 0x04002002 RID: 8194
		public float WalkSpeed = 4f;

		// Token: 0x04002003 RID: 8195
		public float ProjectileSpeed = 10f;

		// Token: 0x04002004 RID: 8196
		public GameObject Projectile;
	}
}
