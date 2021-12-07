using System;
using Game;

// Token: 0x0200033B RID: 827
public class ShowAreaMessageAction : ActionMethod
{
	// Token: 0x060017D1 RID: 6097 RVA: 0x00066322 File Offset: 0x00064522
	public override void Perform(IContext context)
	{
		UI.MessageController.ShowAreaMessage(this.Message);
	}

	// Token: 0x04001481 RID: 5249
	public MessageProvider Message;
}
