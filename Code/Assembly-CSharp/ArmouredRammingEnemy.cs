using System;
using UnityEngine;

// Token: 0x0200058B RID: 1419
public class ArmouredRammingEnemy : RammingEnemy
{
	// Token: 0x0600248C RID: 9356 RVA: 0x0009F650 File Offset: 0x0009D850
	public override void OnModifyDamage(Damage damage)
	{
		if (damage.Type == DamageType.Crush)
		{
			AchievementsLogic.Instance.OnCrushRamWithStomper();
		}
		if (damage.Type == DamageType.SpiritFlame)
		{
			base.PlaySound(this.Sounds.Deflected);
			damage.SetAmount(0f);
		}
		else if (damage.Type == DamageType.Bash)
		{
			this.Controller.StateMachine.ChangeState(this.State.KnockBack);
			base.PlaySound(this.Sounds.Deflected);
			damage.SetAmount(0f);
			base.FaceLeft = (damage.Force.x > 0f);
		}
		else
		{
			if (damage.Type != DamageType.Enemy)
			{
				base.PlaySound(this.Sounds.Hurt);
			}
			if (damage.Type == DamageType.Stomp || damage.Type == DamageType.StompBlast || damage.Type == DamageType.ChargeFlame || damage.Type == DamageType.Grenade || (damage.Type == DamageType.LevelUp && this.Settings.CanDieToLevelUpBlast))
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.RammingEnemy, base.Position, Quaternion.identity);
				RammingEnemy component = gameObject.GetComponent<RammingEnemy>();
				component.FaceLeft = base.FaceLeft;
				component.transform.parent = base.transform.parent;
				component.Zones = this.Zones;
				component.DamageReciever.SetHealth(this.DamageReciever.Health);
				component.DamageReciever.SetMaxHealth(this.DamageReciever.MaxHealth);
				component.DamageDealer.Damage = this.DamageDealer.Damage;
				component.GetComponentInChildren<OrbSpawner>().CopySettings(base.GetComponentInChildren<OrbSpawner>());
				gameObject.AddComponent<DestroyOnRestoreCheckpoint>();
				base.gameObject.SetActive(false);
				InstantiateUtility.Destroy(base.gameObject);
				component.DamageReciever.OnDeathEvent = this.DamageReciever.OnDeathEvent;
				if (damage.Type == DamageType.ChargeFlame)
				{
					ChargeFlameBurst.IgnoreOnLastInstance(component.Targetting);
				}
				if (damage.Type == DamageType.Grenade)
				{
					GrenadeBurst.IgnoreOnLastInstance(component.Targetting);
				}
				if (this.ShatterEffect)
				{
					this.ShatterEffect.Spawn(new DamageContext(damage));
				}
				this.OnReplacedAction(component);
			}
			else if (damage.Type == DamageType.LevelUp && !this.Settings.CanDieToLevelUpBlast)
			{
				base.PlaySound(this.Sounds.Deflected);
				damage.SetAmount(0f);
			}
		}
	}

	// Token: 0x04001EC6 RID: 7878
	public GameObject RammingEnemy;

	// Token: 0x04001EC7 RID: 7879
	public Action<Entity> OnReplacedAction = delegate(Entity A_0)
	{
	};

	// Token: 0x04001EC8 RID: 7880
	public PrefabSpawner ShatterEffect;
}
