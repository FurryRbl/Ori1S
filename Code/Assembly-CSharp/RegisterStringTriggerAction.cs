using System;

// Token: 0x0200031C RID: 796
public class RegisterStringTriggerAction : ActionMethod
{
	// Token: 0x06001763 RID: 5987 RVA: 0x00064E4E File Offset: 0x0006304E
	public override void Perform(IContext context)
	{
		TriggerByString.Register(this.String);
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x00064E5B File Offset: 0x0006305B
	public override string GetNiceName()
	{
		return StringUtility.AddSpaces("Register " + this.String + " Trigger");
	}

	// Token: 0x04001415 RID: 5141
	public string String = string.Empty;
}
