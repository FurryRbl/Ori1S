using System;

// Token: 0x020002EB RID: 747
public class GoToTrialEndAction : ActionMethod
{
	// Token: 0x0600169E RID: 5790 RVA: 0x00062EB0 File Offset: 0x000610B0
	public override void Perform(IContext context)
	{
		GameController.Instance.GoToEndTrialScreen();
	}
}
