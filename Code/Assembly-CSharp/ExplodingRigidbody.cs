using System;
using UnityEngine;

// Token: 0x020006D0 RID: 1744
public class ExplodingRigidbody : MonoBehaviour, IContextReciever, IDamageReciever
{
	// Token: 0x060029E0 RID: 10720 RVA: 0x000B42B0 File Offset: 0x000B24B0
	public void Start()
	{
		if (this.ExplodeOnStart)
		{
			Damage damage = new Damage(100f, this.ExplodeOnStartDirection, base.transform.position, DamageType.Explosion, base.gameObject);
			this.OnRecieveDamage(damage);
		}
	}

	// Token: 0x060029E1 RID: 10721 RVA: 0x000B42F8 File Offset: 0x000B24F8
	public void OnRecieveDamage(Damage damage)
	{
		int num = 0;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			float magnitude = (transform.position - damage.Position).magnitude;
			float d = this.Force * this.ForceOverDistanceCurve.Evaluate(magnitude);
			Vector3 v = damage.Force * d + new Vector2(FixedRandom.Values[(4 + num) % 10] - 0.5f, FixedRandom.Values[(7 + num) % 10] - 0.5f) * this.ForceRandom;
			Rigidbody component = transform.GetComponent<Rigidbody>();
			if (component)
			{
				component.AddForceSafe(v);
				component.AddRelativeTorque(0f, 0f, this.TorqueRandom * (FixedRandom.Values[(5 + num) % 10] - 0.5f));
			}
			num++;
		}
	}

	// Token: 0x060029E2 RID: 10722 RVA: 0x000B4428 File Offset: 0x000B2628
	public void OnReceiveContext(IContext context)
	{
		if (context is DamageContext)
		{
			Damage damage = ((DamageContext)context).Damage;
			this.OnRecieveDamage(damage);
		}
	}

	// Token: 0x04002546 RID: 9542
	public AnimationCurve ForceOverDistanceCurve;

	// Token: 0x04002547 RID: 9543
	public float Force;

	// Token: 0x04002548 RID: 9544
	public float ForceRandom;

	// Token: 0x04002549 RID: 9545
	public float TorqueRandom;

	// Token: 0x0400254A RID: 9546
	public bool ExplodeOnStart;

	// Token: 0x0400254B RID: 9547
	public Vector3 ExplodeOnStartDirection;
}
