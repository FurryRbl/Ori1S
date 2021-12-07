using System;

// Token: 0x0200028E RID: 654
public class IsFullyInstalledCondition : Condition
{
	// Token: 0x06001553 RID: 5459 RVA: 0x0005EFB3 File Offset: 0x0005D1B3
	public override bool Validate(IContext context)
	{
		return GameController.Instance.IsPackageFullyInstalled;
	}
}
