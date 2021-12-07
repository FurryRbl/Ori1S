using System;
using UnityEngine;

// Token: 0x02000481 RID: 1153
public class DealDamageSphere : MonoBehaviour
{
	// Token: 0x06001F8F RID: 8079 RVA: 0x0008AE40 File Offset: 0x00089040
	public void Awake()
	{
		this.m_transform = base.transform;
		if (this.Delay == 0f)
		{
			this.DealDamageSphereMethod();
		}
		else
		{
			base.Invoke("DealDamageSphereMethod", this.Delay);
		}
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x0008AE7C File Offset: 0x0008907C
	public void DealDamageSphereMethod()
	{
		foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.Radius))
		{
			foreach (IDamageReciever damageReciever in collider.gameObject.FindComponentsInChildren<IDamageReciever>())
			{
				Vector2 force = (collider.transform.position - base.transform.position).normalized;
				if (collider.gameObject)
				{
					Damage damage = new Damage((float)this.Damage, force, this.m_transform.position, this.DamageType, base.gameObject);
					damageReciever.OnRecieveDamage(damage);
				}
			}
		}
	}

	// Token: 0x04001B2B RID: 6955
	public float Radius;

	// Token: 0x04001B2C RID: 6956
	public float Delay;

	// Token: 0x04001B2D RID: 6957
	public DamageType DamageType = DamageType.Explosion;

	// Token: 0x04001B2E RID: 6958
	public int Damage = 1000;

	// Token: 0x04001B2F RID: 6959
	private Transform m_transform;
}
