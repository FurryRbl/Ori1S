using System;

// Token: 0x0200065F RID: 1631
public class CanSkipCutsceneCondition : Condition
{
	// Token: 0x060027C8 RID: 10184 RVA: 0x000ACEB1 File Offset: 0x000AB0B1
	public override bool Validate(IContext context)
	{
		return !(SkipCutsceneController.Instance == null) && SkipCutsceneController.Instance.SkippingAvailable;
	}
}
