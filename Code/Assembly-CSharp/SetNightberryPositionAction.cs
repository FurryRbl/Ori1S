using System;
using Game;
using UnityEngine;

// Token: 0x02000337 RID: 823
[Category("General")]
public class SetNightberryPositionAction : ActionMethod
{
	// Token: 0x060017C9 RID: 6089 RVA: 0x00066188 File Offset: 0x00064388
	public override void Perform(IContext context)
	{
		Items.NightBerry.Position = this.Target.position;
	}

	// Token: 0x04001476 RID: 5238
	public Transform Target;
}
