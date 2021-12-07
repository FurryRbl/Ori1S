using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class DamageBasedPrefabProvider : PrefabProvider
{
	// Token: 0x06001E27 RID: 7719 RVA: 0x00084A58 File Offset: 0x00082C58
	public override GameObject Prefab(IContext context)
	{
		if (context is IDamageContext)
		{
			IDamageContext damageContext = (IDamageContext)context;
			Damage damage = damageContext.Damage;
			foreach (DamageTypePrefabPair damageTypePrefabPair in this.Prefabs)
			{
				if (damageTypePrefabPair.DamageType == damage.Type)
				{
					return damageTypePrefabPair.PrefabProvider.Prefab(context);
				}
			}
		}
		return (!(this.Default == null)) ? this.Default.Prefab(context) : null;
	}

	// Token: 0x040019F7 RID: 6647
	public List<DamageTypePrefabPair> Prefabs;

	// Token: 0x040019F8 RID: 6648
	public PrefabProvider Default;
}
