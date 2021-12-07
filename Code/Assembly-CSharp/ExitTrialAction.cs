using System;

// Token: 0x020002E2 RID: 738
public class ExitTrialAction : ActionMethod
{
	// Token: 0x06001678 RID: 5752 RVA: 0x00062C0F File Offset: 0x00060E0F
	public override void Perform(IContext context)
	{
		GameController.Instance.ExitTrial();
	}
}
