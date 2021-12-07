using System;

// Token: 0x0200030C RID: 780
public class PerformSkipAction : ActionMethod
{
	// Token: 0x06001730 RID: 5936 RVA: 0x0006445B File Offset: 0x0006265B
	public override void Perform(IContext context)
	{
		SkipCutsceneController.Instance.SkipCutscene();
	}
}
