using System;
using Game;
using UnityEngine;

// Token: 0x02000327 RID: 807
[Category("Sein")]
public class SeinLocalSpeedAction : ActionMethod
{
	// Token: 0x06001794 RID: 6036 RVA: 0x000654FA File Offset: 0x000636FA
	public override void Perform(IContext context)
	{
		Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed = this.Speed;
	}

	// Token: 0x04001432 RID: 5170
	public Vector2 Speed = Vector2.zero;
}
