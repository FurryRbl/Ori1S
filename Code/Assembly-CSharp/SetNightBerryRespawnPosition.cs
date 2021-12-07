using System;
using Game;
using UnityEngine;

// Token: 0x02000901 RID: 2305
public class SetNightBerryRespawnPosition : ActionMethod
{
	// Token: 0x0600333A RID: 13114 RVA: 0x000D7E4C File Offset: 0x000D604C
	public override void Perform(IContext context)
	{
		if (Items.NightBerry)
		{
			Items.NightBerry.SetRespawnPosition(this.Target.position);
		}
	}

	// Token: 0x04002E2D RID: 11821
	public Transform Target;
}
