using System;

// Token: 0x02000704 RID: 1796
public class CancelDifficultyScreenAction : ActionMethod
{
	// Token: 0x06002AAD RID: 10925 RVA: 0x000B6F08 File Offset: 0x000B5108
	public override void Perform(IContext context)
	{
		SaveSlotsUI.Instance.CancelDifficultyScreen();
	}
}
