using System;

// Token: 0x020002D8 RID: 728
[Category("Debug")]
public class DebugLogAction : ActionMethod
{
	// Token: 0x06001662 RID: 5730 RVA: 0x000629EC File Offset: 0x00060BEC
	public override void Perform(IContext context)
	{
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x000629EE File Offset: 0x00060BEE
	public override string GetNiceName()
	{
		return "Print \"" + this.Message + "\"";
	}

	// Token: 0x04001357 RID: 4951
	public string Message;
}
