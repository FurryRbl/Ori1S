using System;
using UnityEngine;

// Token: 0x0200052D RID: 1325
public class FishEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600231B RID: 8987 RVA: 0x00099DB8 File Offset: 0x00097FB8
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.FishEnemy, base.transform.position, base.transform.rotation);
		FishEnemy component = gameObject.GetComponent<FishEnemy>();
		component.WanderTarget = this.WanderTarget;
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		component.transform.parent = base.transform.parent;
		component.name = this.FishEnemy.name;
		return component;
	}

	// Token: 0x04001D93 RID: 7571
	public Transform WanderTarget;

	// Token: 0x04001D94 RID: 7572
	public FishEnemyPlaceholder.FishEnemyPlaceholderSettings Settings;

	// Token: 0x04001D95 RID: 7573
	public GameObject FishEnemy;

	// Token: 0x0200052E RID: 1326
	[Serializable]
	public class FishEnemyPlaceholderSettings
	{
		// Token: 0x04001D96 RID: 7574
		public float Health = 20f;

		// Token: 0x04001D97 RID: 7575
		public float DamageOnTouch = 5f;

		// Token: 0x04001D98 RID: 7576
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001D99 RID: 7577
		public DropLootSettings LootSettings;
	}
}
