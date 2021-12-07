using System;

// Token: 0x0200026D RID: 621
public class ShowHelpAction : ActionMethod
{
	// Token: 0x060014CD RID: 5325 RVA: 0x0005D9BB File Offset: 0x0005BBBB
	public override void Perform(IContext context)
	{
		XboxOne.Help();
	}
}
