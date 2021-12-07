using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C6 RID: 966
public class RigidbodyReactToDamage : MonoBehaviour, IDamageReciever
{
	// Token: 0x06001AC3 RID: 6851 RVA: 0x000730B0 File Offset: 0x000712B0
	public void Start()
	{
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x000730B4 File Offset: 0x000712B4
	public void OnRecieveDamage(Damage damage)
	{
		for (int i = 0; i < this.DamageReactionSettings.Count; i++)
		{
			DamageReactionSettings damageReactionSettings = this.DamageReactionSettings[i];
			if (damageReactionSettings.DamageType == damage.Type && !base.GetComponent<Rigidbody>().isKinematic)
			{
				base.GetComponent<Rigidbody>().velocity = damage.Force.normalized * damageReactionSettings.ForceMultiplier;
			}
		}
	}

	// Token: 0x0400172D RID: 5933
	public List<DamageReactionSettings> DamageReactionSettings = new List<DamageReactionSettings>();
}
