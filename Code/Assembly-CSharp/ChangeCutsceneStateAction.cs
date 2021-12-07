using System;

// Token: 0x020002C6 RID: 710
[Category("Cutscene")]
public class ChangeCutsceneStateAction : ActionMethod
{
	// Token: 0x06001607 RID: 5639 RVA: 0x0006184A File Offset: 0x0005FA4A
	public override void Perform(IContext context)
	{
		this.Cutscene.ChangeState(this.State);
	}

	// Token: 0x040012F9 RID: 4857
	[NotNull]
	public CutsceneController Cutscene;

	// Token: 0x040012FA RID: 4858
	public CutsceneState State;
}
