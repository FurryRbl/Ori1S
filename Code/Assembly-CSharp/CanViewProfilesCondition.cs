using System;

// Token: 0x0200027D RID: 637
public class CanViewProfilesCondition : Condition
{
	// Token: 0x06001507 RID: 5383 RVA: 0x0005E407 File Offset: 0x0005C607
	public override bool Validate(IContext context)
	{
		return XboxOneUsers.CanViewProfiles;
	}
}
