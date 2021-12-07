using System;

// Token: 0x02000352 RID: 850
public class StartExitingAction : ActionMethod
{
	// Token: 0x0600184E RID: 6222 RVA: 0x000686AA File Offset: 0x000668AA
	public override void Perform(IContext context)
	{
		GameController.Instance.PreventFocusPause = true;
	}
}
