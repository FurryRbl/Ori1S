using System;
using UnityEngine;

// Token: 0x0200057C RID: 1404
public class JumperEnemyPlaceholder : RespawningPlaceholder
{
	// Token: 0x0600244D RID: 9293 RVA: 0x0009E584 File Offset: 0x0009C784
	public override Entity Instantiate()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.JumpingSootEnemy, base.transform.position, base.transform.rotation);
		JumperEnemy component = gameObject.GetComponent<JumperEnemy>();
		component.Settings.ChargeRange = this.Settings.ChargeRange;
		component.Settings.ShortJumpHeight = this.Settings.ShortJumpHeight;
		component.Settings.JumpHeight = this.Settings.JumpHeight;
		component.Settings.JumpDistance = this.Settings.JumpDistance;
		component.Settings.Gravity = this.Settings.Gravity;
		component.Settings.ChargingDuration = this.Settings.ChargingDuration;
		component.Settings.ExplosionDamage = this.Settings.ExplosionDamage;
		component.Settings.GroundStompDamage = this.Settings.GroundStompDamage;
		component.Settings.HasStompExplosion = this.Settings.HasStompExplosion;
		component.FaceLeft = (base.transform.localScale.x < 0f);
		float num = Enemy.ScaleHealth(this.Settings.Health);
		component.DamageReciever.SetHealth(num);
		component.DamageReciever.SetMaxHealth(num);
		component.DamageDealer.Damage = this.Settings.DamageOnTouch;
		OrbSpawner componentInChildren = component.GetComponentInChildren<OrbSpawner>();
		componentInChildren.SetNumberOfExpOrbs(this.Settings.NumberOfExpOrbsToDrop);
		componentInChildren.LootSettings = this.Settings.LootSettings;
		gameObject.transform.parent = base.transform.parent;
		gameObject.name = this.JumpingSootEnemy.name;
		component.JumpingZone = this.JumpingZone;
		return component;
	}

	// Token: 0x04001E81 RID: 7809
	public JumperEnemyPlaceholder.JumpingSootEnemyPlaceholderSettings Settings;

	// Token: 0x04001E82 RID: 7810
	public GameObject JumpingSootEnemy;

	// Token: 0x04001E83 RID: 7811
	public Transform JumpingZone;

	// Token: 0x0200057D RID: 1405
	[Serializable]
	public class JumpingSootEnemyPlaceholderSettings
	{
		// Token: 0x04001E84 RID: 7812
		public float Health = 5f;

		// Token: 0x04001E85 RID: 7813
		public float DamageOnTouch = 5f;

		// Token: 0x04001E86 RID: 7814
		public int NumberOfExpOrbsToDrop = 2;

		// Token: 0x04001E87 RID: 7815
		public DropLootSettings LootSettings;

		// Token: 0x04001E88 RID: 7816
		public float ChargeRange = 20f;

		// Token: 0x04001E89 RID: 7817
		public float JumpHeight = 10f;

		// Token: 0x04001E8A RID: 7818
		public float ShortJumpHeight = 5f;

		// Token: 0x04001E8B RID: 7819
		public float JumpDistance = 20f;

		// Token: 0x04001E8C RID: 7820
		public float Gravity = 30f;

		// Token: 0x04001E8D RID: 7821
		public float ChargingDuration = 0.7f;

		// Token: 0x04001E8E RID: 7822
		public float ExplosionDamage = 5f;

		// Token: 0x04001E8F RID: 7823
		public int GroundStompDamage = 10;

		// Token: 0x04001E90 RID: 7824
		public bool HasStompExplosion = true;
	}
}
