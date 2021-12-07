using System;
using Game;

// Token: 0x020009D0 RID: 2512
public class DestroyNightBerryAction : ActionMethod
{
	// Token: 0x060036BE RID: 14014 RVA: 0x000E5DC8 File Offset: 0x000E3FC8
	public override void Perform(IContext context)
	{
		InstantiateUtility.Destroy(Items.NightBerry.gameObject);
	}
}
