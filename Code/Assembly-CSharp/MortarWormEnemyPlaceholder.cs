using System;
using UnityEngine;

// Token: 0x0200060A RID: 1546
public class MortarWormEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x17000621 RID: 1569
	// (get) Token: 0x06002692 RID: 9874 RVA: 0x000A9353 File Offset: 0x000A7553
	public override bool CheckForOverlap
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x000A9358 File Offset: 0x000A7558
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.MortarWormEnemy, base.transform.position, base.transform.rotation);
		MortarWormEnemy component = gameObject.GetComponent<MortarWormEnemy>();
		component.Settings.HideDistance = this.Settings.HideDistance;
		component.Settings.ProjectileSpeed = this.Settings.ProjectileSpeed;
		component.Settings.ProjectileGravity = this.Settings.ProjectileGravity;
		component.Settings.CanTurnAround = this.Settings.CanTurnAround;
		component.Settings.WaitBetweenShots = this.Settings.WaitBetweenShots;
		component.Settings.ProjectileDamage = this.Settings.ProjectileDamage;
		component.FaceLeft = (base.transform.localScale.x < 0f);
		component.GetComponentInChildren<NearSeinTrigger>().TriggerDistance = this.Settings.Range;
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.MortarWormEnemy.name;
		return component;
	}

	// Token: 0x04002141 RID: 8513
	public MortarWormEnemyPlaceholder.MortarWormEnemyPlaceholderSettings Settings;

	// Token: 0x04002142 RID: 8514
	[AssetReferenceChoice("Mortar Type", new string[]
	{
		"Assets\\frameworks\\enemies\\worm\\prefabs\\mortarWorm\\mortarWormEnemy.prefab",
		"Assets\\frameworks\\enemies\\worm\\textures\\mortarWormEnemyPlaceholder.png",
		"Regular Mortar",
		"Assets\\frameworks\\enemies\\worm\\prefabs\\mortarWorm\\mortarWormFireEnemy.prefab",
		"Assets\\frameworks\\enemies\\worm\\textures\\mortarWormEnemyFirePlaceholder.png",
		"Fire Mortar"
	})]
	public GameObject MortarWormEnemy;

	// Token: 0x0200060B RID: 1547
	[Serializable]
	public class MortarWormEnemyPlaceholderSettings
	{
		// Token: 0x04002143 RID: 8515
		public float Health = 5f;

		// Token: 0x04002144 RID: 8516
		public float DamageOnTouch = 2f;

		// Token: 0x04002145 RID: 8517
		public float ProjectileDamage = 10f;

		// Token: 0x04002146 RID: 8518
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04002147 RID: 8519
		public DropLootSettings LootSettings;

		// Token: 0x04002148 RID: 8520
		public float HideDistance = 5f;

		// Token: 0x04002149 RID: 8521
		public float ProjectileSpeed = 20f;

		// Token: 0x0400214A RID: 8522
		public float ProjectileGravity = 30f;

		// Token: 0x0400214B RID: 8523
		public bool CanTurnAround;

		// Token: 0x0400214C RID: 8524
		public float WaitBetweenShots = 2f;

		// Token: 0x0400214D RID: 8525
		public float Range = 20f;
	}
}
