using System;

// Token: 0x02000634 RID: 1588
public class ConfirmChangingDifficulty : ActionMethod
{
	// Token: 0x0600270E RID: 9998 RVA: 0x000AABEA File Offset: 0x000A8DEA
	public override void Perform(IContext context)
	{
		ChangeDifficultyScreen.Instance.Confirm();
	}
}
