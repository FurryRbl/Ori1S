using System;
using Game;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class DamageTextSpawner : MonoBehaviour
{
	// Token: 0x06001660 RID: 5728 RVA: 0x000629B8 File Offset: 0x00060BB8
	public void SpawnDamageText(IContext context)
	{
		IDamageContext damageContext = context as IDamageContext;
		if (damageContext != null)
		{
			Attacking.DamageDisplayText.Create(damageContext.Damage, base.transform);
		}
	}
}
