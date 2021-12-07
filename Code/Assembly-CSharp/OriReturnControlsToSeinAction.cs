using System;
using Game;

// Token: 0x02000309 RID: 777
[Category("Ori")]
public class OriReturnControlsToSeinAction : ActionMethod
{
	// Token: 0x0600171D RID: 5917 RVA: 0x000641A1 File Offset: 0x000623A1
	public override void Perform(IContext context)
	{
		Characters.Ori.BackToPlayerController();
	}
}
