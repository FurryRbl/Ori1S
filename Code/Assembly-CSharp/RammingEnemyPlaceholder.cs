using System;
using UnityEngine;

// Token: 0x02000598 RID: 1432
public class RammingEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x060024B4 RID: 9396 RVA: 0x0009FFE8 File Offset: 0x0009E1E8
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.RammingEnemy, base.transform.position, base.transform.rotation);
		RammingEnemy component = gameObject.GetComponent<RammingEnemy>();
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		component.Settings.AlertRange = this.Settings.AlertRange;
		component.Settings.CanDieToLevelUpBlast = this.Settings.CanDieToLevelUpBlast;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.RammingEnemy.name;
		component.Zones = this.Zones;
		ArmouredRammingEnemy component2 = gameObject.GetComponent<ArmouredRammingEnemy>();
		if (component2)
		{
			ArmouredRammingEnemy armouredRammingEnemy = component2;
			armouredRammingEnemy.OnReplacedAction = (Action<Entity>)Delegate.Combine(armouredRammingEnemy.OnReplacedAction, new Action<Entity>(this.OnRammingEnemyReplaced));
		}
		return component;
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x000A0122 File Offset: 0x0009E322
	public void OnRammingEnemyReplaced(Entity entity)
	{
		base.SetCurrentEntity(entity);
	}

	// Token: 0x04001EF8 RID: 7928
	public RammingEnemyPlaceholder.RammingEnemyPlaceholderSettings Settings;

	// Token: 0x04001EF9 RID: 7929
	public GameObject RammingEnemy;

	// Token: 0x04001EFA RID: 7930
	public Transform[] Zones;

	// Token: 0x02000599 RID: 1433
	[Serializable]
	public class RammingEnemyPlaceholderSettings
	{
		// Token: 0x04001EFB RID: 7931
		public float Health = 20f;

		// Token: 0x04001EFC RID: 7932
		public float DamageOnTouch = 5f;

		// Token: 0x04001EFD RID: 7933
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001EFE RID: 7934
		public float AlertRange = 32f;

		// Token: 0x04001EFF RID: 7935
		public bool CanDieToLevelUpBlast = true;

		// Token: 0x04001F00 RID: 7936
		public DropLootSettings LootSettings;
	}
}
