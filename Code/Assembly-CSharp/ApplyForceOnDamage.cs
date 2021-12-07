using System;
using UnityEngine;

// Token: 0x020006CC RID: 1740
public class ApplyForceOnDamage : MonoBehaviour, IContextReciever, IDamageReciever
{
	// Token: 0x060029C2 RID: 10690 RVA: 0x000B3D00 File Offset: 0x000B1F00
	public void OnRecieveDamage(Damage damage)
	{
		Vector3 vector = damage.Force * this.ForceMagnitude;
		if (this.Target == null)
		{
			base.GetComponent<Rigidbody>().AddForceSafe(vector);
			base.SendMessage("OnApplyForce", vector, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			this.Target.GetComponent<Rigidbody>().AddForceSafe(vector);
			this.Target.SendMessage("OnApplyForce", vector, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060029C3 RID: 10691 RVA: 0x000B3D8C File Offset: 0x000B1F8C
	public void OnReceiveContext(IContext context)
	{
		if (context is DamageReciever)
		{
			Damage damage = (context as IDamageContext).Damage;
			this.OnRecieveDamage(damage);
		}
	}

	// Token: 0x04002536 RID: 9526
	public float ForceMagnitude;

	// Token: 0x04002537 RID: 9527
	public GameObject Target;
}
