using System;
using UnityEngine;

// Token: 0x020008CF RID: 2255
public class KillPlayer : MonoBehaviour
{
	// Token: 0x0600322E RID: 12846 RVA: 0x000D4A53 File Offset: 0x000D2C53
	public void Start()
	{
	}

	// Token: 0x0600322F RID: 12847 RVA: 0x000D4A55 File Offset: 0x000D2C55
	private void OnTriggerEnter(Collider other)
	{
		this.DealDamage(other.gameObject);
	}

	// Token: 0x06003230 RID: 12848 RVA: 0x000D4A63 File Offset: 0x000D2C63
	private void OnCollisionEnter(Collision other)
	{
		this.DealDamage(other.gameObject);
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x000D4A74 File Offset: 0x000D2C74
	public void DealDamage(GameObject gameObject)
	{
		IDamageReciever damageReciever = gameObject.FindComponent<IDamageReciever>();
		if (damageReciever != null)
		{
			if (!this.KillEnemiesToo && gameObject.name != "Grenade (Clone)" && gameObject.GetComponent<PickupBase>() == null && !(damageReciever is SeinDamageReciever))
			{
				return;
			}
			Damage damage = new Damage(10000f, (gameObject.transform.position - base.transform.position).normalized, base.transform.position, DamageType.Lava, gameObject);
			damageReciever.OnRecieveDamage(damage);
		}
	}

	// Token: 0x04002D35 RID: 11573
	public bool KillEnemiesToo;
}
