using System;

// Token: 0x02000290 RID: 656
public class IsOnlineCondition : Condition
{
	// Token: 0x06001557 RID: 5463 RVA: 0x0005F05C File Offset: 0x0005D25C
	public override bool Validate(IContext context)
	{
		return Steamworks.IsLoggedIn;
	}
}
