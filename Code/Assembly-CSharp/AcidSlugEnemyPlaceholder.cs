using System;
using UnityEngine;

// Token: 0x020005BE RID: 1470
public class AcidSlugEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600254C RID: 9548 RVA: 0x000A2BBC File Offset: 0x000A0DBC
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.AcidSlugEnemy, base.transform.position, base.transform.rotation);
		AcidSlugEnemy component = gameObject.GetComponent<AcidSlugEnemy>();
		component.Settings.WalkSpeed = this.Settings.WalkSpeed;
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		component.Settings.AcidDripRate = this.Settings.AcidDripRate;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.AcidSlugEnemy.name;
		return component;
	}

	// Token: 0x04001FD8 RID: 8152
	public AcidSlugEnemyPlaceholder.AcidSlugEnemyPlaceholderSettings Settings;

	// Token: 0x04001FD9 RID: 8153
	[AssetReferenceChoice("Acid Slug Type", new string[]
	{
		"Assets\\frameworks\\enemies\\slug\\prefabs\\acid\\acidSlugEnemy.prefab",
		"Assets\\frameworks\\enemies\\slug\\textures\\acidSlugEnemyPlaceholder.png",
		"Regular Slug",
		"Assets\\frameworks\\enemies\\slug\\prefabs\\fire\\fireSlugEnemy.prefab",
		"Assets\\frameworks\\enemies\\slug\\textures\\fireSlugEnemyPlaceholder.png",
		"Fire Slug",
		"Assets\\frameworks\\enemies\\slug\\prefabs\\ice\\iceSlugEnemy.prefab",
		"Assets\\frameworks\\enemies\\slug\\textures\\iceSlugEnemyPlaceholder.png",
		"Ice Slug"
	})]
	public GameObject AcidSlugEnemy;

	// Token: 0x020005BF RID: 1471
	[Serializable]
	public class AcidSlugEnemyPlaceholderSettings
	{
		// Token: 0x04001FDA RID: 8154
		public float Health = 20f;

		// Token: 0x04001FDB RID: 8155
		public float DamageOnTouch = 2f;

		// Token: 0x04001FDC RID: 8156
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001FDD RID: 8157
		public float WalkSpeed = 4f;

		// Token: 0x04001FDE RID: 8158
		public DropLootSettings LootSettings;

		// Token: 0x04001FDF RID: 8159
		public float AcidDripRate = 0.7f;
	}
}
