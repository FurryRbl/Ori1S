using System;
using Game;
using UnityEngine;

// Token: 0x02000334 RID: 820
[Category("General")]
public class SetCharacterPosition : ActionMethod
{
	// Token: 0x060017C4 RID: 6084 RVA: 0x000660B4 File Offset: 0x000642B4
	public override void Perform(IContext context)
	{
		Characters.Current.Position = this.Position.position;
		if (this.PlaceOnGround)
		{
			Characters.Current.PlaceOnGround();
		}
		if (Characters.Ori)
		{
			Characters.Ori.MoveOriBackToPlayer();
		}
	}

	// Token: 0x0400146F RID: 5231
	public Transform Position;

	// Token: 0x04001470 RID: 5232
	public bool PlaceOnGround = true;
}
