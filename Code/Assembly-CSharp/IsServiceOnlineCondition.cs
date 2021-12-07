using System;

// Token: 0x02000292 RID: 658
public class IsServiceOnlineCondition : Condition
{
	// Token: 0x0600155B RID: 5467 RVA: 0x0005F07A File Offset: 0x0005D27A
	public override bool Validate(IContext context)
	{
		return XboxOneLive.LiveOnline || Steamworks.IsLoggedIn;
	}
}
