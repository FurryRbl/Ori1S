using System;

// Token: 0x02000668 RID: 1640
public class RegisterSkipCutsceneAction : ActionMethod
{
	// Token: 0x060027FE RID: 10238 RVA: 0x000ADB98 File Offset: 0x000ABD98
	public override void Perform(IContext context)
	{
		SkipCutsceneController.Instance.RegisterSkipCutscene(new Action(this.MasterTimelineSequence.OnSkipCutscene));
	}

	// Token: 0x0400228C RID: 8844
	public MasterTimelineSequence MasterTimelineSequence;
}
