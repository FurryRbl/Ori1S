using System;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class KamikazeSootEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x06002485 RID: 9349 RVA: 0x0009F484 File Offset: 0x0009D684
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab, base.transform.position, base.transform.rotation);
		KamikazeSootEnemy component = gameObject.GetComponent<KamikazeSootEnemy>();
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		component.DamageDealer.Damage = (float)component.Settings.ExplosionDamage;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.Prefab.name;
		return component;
	}

	// Token: 0x04001EC1 RID: 7873
	public KamikazeSootEnemyPlaceholder.KamikazeSootEnemyPlaceholderSettings Settings;

	// Token: 0x04001EC2 RID: 7874
	public GameObject Prefab;

	// Token: 0x02000589 RID: 1417
	[Serializable]
	public class KamikazeSootEnemyPlaceholderSettings
	{
		// Token: 0x04001EC3 RID: 7875
		public float Health = 5f;

		// Token: 0x04001EC4 RID: 7876
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001EC5 RID: 7877
		public DropLootSettings LootSettings;
	}
}
