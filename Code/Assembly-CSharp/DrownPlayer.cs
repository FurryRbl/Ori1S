using System;
using Game;
using UnityEngine;

// Token: 0x020008CB RID: 2251
public class DrownPlayer : MonoBehaviour
{
	// Token: 0x06003223 RID: 12835 RVA: 0x000D4774 File Offset: 0x000D2974
	private void OnTriggerStay(Collider other)
	{
		SeinDamageReciever seinDamageReciever = other.gameObject.FindComponentInChildren<SeinDamageReciever>();
		if (seinDamageReciever)
		{
			Vector3 position = Characters.Sein.Controller.Transform.position;
			position.y += Characters.Sein.Controller.Transform.localScale.y / 2f;
			if (base.GetComponent<Collider>().bounds.Contains(position))
			{
				Vector2 force = (other.transform.position - base.transform.position).normalized;
				Damage damage = new Damage(10000f, force, base.transform.position, DamageType.Water, base.gameObject);
				damage.DealToComponents(seinDamageReciever.gameObject);
			}
		}
	}
}
