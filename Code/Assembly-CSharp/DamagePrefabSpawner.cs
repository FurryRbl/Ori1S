using System;
using Game;
using UnityEngine;

// Token: 0x0200053A RID: 1338
public class DamagePrefabSpawner : MonoBehaviour, IDamageReciever
{
	// Token: 0x0600233B RID: 9019 RVA: 0x0009A3AC File Offset: 0x000985AC
	public void OnRecieveDamage(Damage damage)
	{
		if (this.DamagePrefab)
		{
			GameObject gameObject = Attacking.DamageEffect.Create(damage, base.transform, this.DamagePrefab);
			if (this.UseRotation)
			{
				gameObject.transform.rotation = base.transform.rotation;
			}
		}
	}

	// Token: 0x04001DA4 RID: 7588
	public DamageBasedPrefabProvider DamagePrefab;

	// Token: 0x04001DA5 RID: 7589
	public bool UseRotation;
}
