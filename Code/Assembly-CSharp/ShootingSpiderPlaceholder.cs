using System;
using UnityEngine;

// Token: 0x020005D7 RID: 1495
public class ShootingSpiderPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600258E RID: 9614 RVA: 0x000A3EF0 File Offset: 0x000A20F0
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.ShootingSpider, base.transform.position, base.transform.rotation);
		ShootingSpider component = gameObject.GetComponent<ShootingSpider>();
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		component.Settings.ProjectileDamage = this.Settings.ProjectileDamage;
		component.Settings.ProjectileSpeed = this.Settings.ProjectileSpeed;
		component.Settings.SpreadShot = this.Settings.SpreadShot;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.NumberOfExpOrbs = this.Settings.NumberOfExpOrbsToDrop;
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.ShootingSpider.name;
		if (this.Settings.PhysicalSystem != null)
		{
			this.Settings.PhysicalSystem.RegisterInstanciatedRigidbody(gameObject.GetComponent<Rigidbody>());
			this.Settings.PhysicalSystem.ResetPhysicalSystemToTheOriginalState();
		}
		else
		{
			this.Settings.Rope.ResetRopeToOriginalPosition();
		}
		AttachToRope component2 = component.GetComponent<AttachToRope>();
		component2.RopeToAttachTo = this.Settings.Rope;
		return component;
	}

	// Token: 0x04002030 RID: 8240
	public ShootingSpiderPlaceholder.ShootingSpiderPlaceholderSettings Settings;

	// Token: 0x04002031 RID: 8241
	[AssetReferenceChoice("Spider Type", new string[]
	{
		"Assets\\frameworks\\enemies\\spider\\shootingSpider\\prefabs\\shootingSpiderEnemy.prefab",
		"Assets\\frameworks\\enemies\\spider\\shootingSpider\\textures\\shootingSpiderPlaceholder.png",
		"Regular Spider",
		"Assets\\frameworks\\enemies\\spider\\shootingSpider\\prefabs\\spreadshotSpiderEnemy.prefab",
		"Assets\\frameworks\\enemies\\spider\\shootingSpider\\textures\\spreadshotSpiderPlaceholder.png",
		"Spreadshot Spider"
	})]
	public GameObject ShootingSpider;

	// Token: 0x020005D8 RID: 1496
	[Serializable]
	public class ShootingSpiderPlaceholderSettings
	{
		// Token: 0x04002032 RID: 8242
		public float Health = 5f;

		// Token: 0x04002033 RID: 8243
		public float DamageOnTouch = 1f;

		// Token: 0x04002034 RID: 8244
		public float ProjectileDamage = 6f;

		// Token: 0x04002035 RID: 8245
		public float ProjectileSpeed = 15f;

		// Token: 0x04002036 RID: 8246
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04002037 RID: 8247
		public DropLootSettings LootSettings;

		// Token: 0x04002038 RID: 8248
		public Rope Rope;

		// Token: 0x04002039 RID: 8249
		public PhysicalSystemManager PhysicalSystem;

		// Token: 0x0400203A RID: 8250
		public bool SpreadShot;
	}
}
