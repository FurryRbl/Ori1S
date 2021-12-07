using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003BE RID: 958
public class DamageDealer : MonoBehaviour
{
	// Token: 0x06001A9A RID: 6810 RVA: 0x000729F1 File Offset: 0x00070BF1
	public virtual float AmountOfDamage(GameObject target)
	{
		return this.Damage;
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x000729F9 File Offset: 0x00070BF9
	public void Start()
	{
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x000729FC File Offset: 0x00070BFC
	public void OnTriggerStay(Collider collider)
	{
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		Rigidbody attachedRigidbody = collider.attachedRigidbody;
		if (attachedRigidbody)
		{
			this.OnCollision(attachedRigidbody.gameObject);
		}
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x00072A34 File Offset: 0x00070C34
	public void OnCollisionStay(Collision collision)
	{
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		Rigidbody attachedRigidbody = collision.collider.attachedRigidbody;
		if (attachedRigidbody)
		{
			this.OnCollision(attachedRigidbody.gameObject);
		}
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x00072A70 File Offset: 0x00070C70
	public void OnCollision(GameObject collided)
	{
		if (!this.Activated)
		{
			return;
		}
		if (!collided.activeInHierarchy)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.PlayerOnly)
		{
			if (DamageDealer.s_oriMask == -1)
			{
				DamageDealer.s_oriMask = LayerMask.NameToLayer("character");
			}
			if (collided.layer != DamageDealer.s_oriMask || (!collided.GetComponent<SeinDamageReciever>() && !collided.GetComponent<SpiritGrenadeDamageDealer>()))
			{
				return;
			}
		}
		this.DealDamage(collided);
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x00072B04 File Offset: 0x00070D04
	public virtual void DealDamage(GameObject target)
	{
		if (base.GetComponent<Collider>() && !base.GetComponent<Collider>().enabled)
		{
			return;
		}
		if (InstantiateUtility.IsDestroyed(target))
		{
			return;
		}
		if (this.Condition && !this.Condition.Validate(null))
		{
			return;
		}
		if (this.ShouldDealDamage != null && !this.ShouldDealDamage(target))
		{
			return;
		}
		Vector2 vector = target.transform.position - base.transform.position;
		Damage damage = new Damage(this.AmountOfDamage(target), vector.normalized, base.transform.position, this.DamageType, base.gameObject);
		bool flag = false;
		IDamageReciever damageReciever = target.FindComponent<IDamageReciever>();
		if (damageReciever != null)
		{
			damageReciever.OnRecieveDamage(damage);
			flag = true;
		}
		if (flag)
		{
			this.OnDamageDealtEvent(target, damage);
		}
	}

	// Token: 0x04001713 RID: 5907
	public float Damage;

	// Token: 0x04001714 RID: 5908
	public DamageType DamageType;

	// Token: 0x04001715 RID: 5909
	public bool Activated = true;

	// Token: 0x04001716 RID: 5910
	public bool PlayerOnly;

	// Token: 0x04001717 RID: 5911
	public Action<GameObject, Damage> OnDamageDealtEvent = delegate(GameObject A_0, Damage A_1)
	{
	};

	// Token: 0x04001718 RID: 5912
	public Func<GameObject, bool> ShouldDealDamage;

	// Token: 0x04001719 RID: 5913
	public Condition Condition;

	// Token: 0x0400171A RID: 5914
	private static int s_oriMask = -1;

	// Token: 0x0400171B RID: 5915
	private List<Collider> m_colliders = new List<Collider>();
}
