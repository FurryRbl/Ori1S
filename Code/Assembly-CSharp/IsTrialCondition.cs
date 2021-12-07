using System;

// Token: 0x020008AB RID: 2219
public class IsTrialCondition : Condition
{
	// Token: 0x0600318B RID: 12683 RVA: 0x000D3238 File Offset: 0x000D1438
	public override bool Validate(IContext context)
	{
		return GameController.Instance.IsTrial;
	}
}
