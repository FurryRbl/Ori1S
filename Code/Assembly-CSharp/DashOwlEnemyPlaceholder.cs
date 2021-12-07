using System;
using UnityEngine;

// Token: 0x0200050A RID: 1290
public class DashOwlEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600228D RID: 8845 RVA: 0x0009745C File Offset: 0x0009565C
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.DashOwlEnemy, base.transform.position, base.transform.rotation);
		DashOwlEnemy component = gameObject.GetComponent<DashOwlEnemy>();
		component.Settings.DashRange = this.Settings.DashRange;
		component.Settings.MaxDistanceFromStartPosition = this.Settings.MaxDistanceFromStartPosition;
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.DashOwlEnemy.name;
		component.Settings.Perched = this.Settings.Perched;
		return component;
	}

	// Token: 0x04001CF0 RID: 7408
	public DashOwlEnemyPlaceholder.DashOwlEnemyPlaceholderSettings Settings;

	// Token: 0x04001CF1 RID: 7409
	public GameObject DashOwlEnemy;

	// Token: 0x0200050B RID: 1291
	[Serializable]
	public class DashOwlEnemyPlaceholderSettings
	{
		// Token: 0x04001CF2 RID: 7410
		public float Health = 20f;

		// Token: 0x04001CF3 RID: 7411
		public float DamageOnTouch = 7f;

		// Token: 0x04001CF4 RID: 7412
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001CF5 RID: 7413
		public DropLootSettings LootSettings;

		// Token: 0x04001CF6 RID: 7414
		public float DashRange = 20f;

		// Token: 0x04001CF7 RID: 7415
		public float MaxDistanceFromStartPosition = 15f;

		// Token: 0x04001CF8 RID: 7416
		public bool Perched = true;
	}
}
