using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006F8 RID: 1784
public class VaryingPrefabProvider : PrefabProvider
{
	// Token: 0x06002A84 RID: 10884 RVA: 0x000B6883 File Offset: 0x000B4A83
	public override GameObject Prefab(IContext context)
	{
		return this.Prefabs[0];
	}

	// Token: 0x040025DE RID: 9694
	public List<GameObject> Prefabs;
}
