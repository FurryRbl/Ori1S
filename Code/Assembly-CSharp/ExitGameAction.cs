using System;

// Token: 0x020002E1 RID: 737
public class ExitGameAction : ActionMethod
{
	// Token: 0x06001676 RID: 5750 RVA: 0x00062BFB File Offset: 0x00060DFB
	public override void Perform(IContext context)
	{
		GameController.Instance.ExitGame();
	}
}
