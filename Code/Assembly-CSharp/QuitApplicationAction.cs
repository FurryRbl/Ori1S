using System;

// Token: 0x02000318 RID: 792
public class QuitApplicationAction : ActionMethod
{
	// Token: 0x0600175C RID: 5980 RVA: 0x00064DA6 File Offset: 0x00062FA6
	public override void Perform(IContext context)
	{
		GameController.Instance.QuitApplication();
	}
}
