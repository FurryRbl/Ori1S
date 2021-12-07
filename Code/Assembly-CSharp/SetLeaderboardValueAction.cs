using System;

// Token: 0x020008B8 RID: 2232
[Category("SPAConfig")]
public class SetLeaderboardValueAction : ActionMethod
{
	// Token: 0x060031B6 RID: 12726 RVA: 0x000D35B9 File Offset: 0x000D17B9
	public override void Perform(IContext context)
	{
		if (GameController.Instance.IsTrial)
		{
			return;
		}
	}

	// Token: 0x04002CEB RID: 11499
	public Leaderboards.Views View;

	// Token: 0x04002CEC RID: 11500
	public Leaderboards.Properties Property;

	// Token: 0x04002CED RID: 11501
	public int Value;
}
