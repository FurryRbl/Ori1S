using System;
using Game;

// Token: 0x02000355 RID: 853
[Category("Sein")]
internal class StopCharacterAnimation : ActionMethod
{
	// Token: 0x06001858 RID: 6232 RVA: 0x00068707 File Offset: 0x00066907
	public override void Perform(IContext context)
	{
		Characters.Sein.Controller.StopAnimation();
		Characters.Ori.StopListening();
	}
}
