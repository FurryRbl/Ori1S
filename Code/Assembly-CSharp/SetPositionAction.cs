using System;
using Game;
using UnityEngine;

// Token: 0x02000338 RID: 824
[Category("General")]
public class SetPositionAction : ActionMethod
{
	// Token: 0x060017CB RID: 6091 RVA: 0x000661B8 File Offset: 0x000643B8
	public override void Perform(IContext context)
	{
		if (this.TargetPlayer)
		{
			this.Target = Characters.Current.Transform;
		}
		if (this.Target)
		{
			Vector3 position = this.Target.position;
			if (this.UseX)
			{
				position.x = this.Position.position.x;
			}
			if (this.UseY)
			{
				position.y = this.Position.position.y;
			}
			this.Target.position = position;
		}
		if (this.TargetPlayer && Characters.Ori)
		{
			Characters.Ori.MoveOriBackToPlayer();
		}
	}

	// Token: 0x04001477 RID: 5239
	public Transform Target;

	// Token: 0x04001478 RID: 5240
	public Transform Position;

	// Token: 0x04001479 RID: 5241
	public bool TargetPlayer;

	// Token: 0x0400147A RID: 5242
	public bool UseX = true;

	// Token: 0x0400147B RID: 5243
	public bool UseY = true;
}
