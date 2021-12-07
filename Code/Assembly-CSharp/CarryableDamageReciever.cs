using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F2 RID: 2290
public class CarryableDamageReciever : MonoBehaviour, IDamageReciever
{
	// Token: 0x06003303 RID: 13059 RVA: 0x000D740C File Offset: 0x000D560C
	public void Awake()
	{
		this.m_carryable = base.transform.parent.GetComponent<CarryableRigidBody>();
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x000D7424 File Offset: 0x000D5624
	public void OnRecieveDamage(Damage damage)
	{
		if (this.m_carryable.IsCarried)
		{
			return;
		}
		if (this.m_damageTypes.Contains(damage.Type))
		{
			this.m_carryable.ExplodeAndRespawn();
		}
	}

	// Token: 0x04002DFF RID: 11775
	private CarryableRigidBody m_carryable;

	// Token: 0x04002E00 RID: 11776
	private readonly HashSet<DamageType> m_damageTypes = new HashSet<DamageType>
	{
		DamageType.Spikes,
		DamageType.Lava
	};
}
