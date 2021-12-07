using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
[Category("Messaging")]
public class SendMessageAction : ActionMethod
{
	// Token: 0x060017AD RID: 6061 RVA: 0x00065D28 File Offset: 0x00063F28
	public override void Perform(IContext context)
	{
		switch (this.TargetMessageType)
		{
		case SendMessageAction.MessageType.Send:
			this.Target.SendMessage(this.Message);
			break;
		case SendMessageAction.MessageType.Broadcast:
			this.Target.BroadcastMessage(this.Message);
			break;
		case SendMessageAction.MessageType.SendUpwards:
			this.Target.SendMessageUpwards(this.Message);
			break;
		}
	}

	// Token: 0x0400144B RID: 5195
	[NotNull]
	public GameObject Target;

	// Token: 0x0400144C RID: 5196
	public string Message;

	// Token: 0x0400144D RID: 5197
	public SendMessageAction.MessageType TargetMessageType = SendMessageAction.MessageType.Broadcast;

	// Token: 0x0200032C RID: 812
	public enum MessageType
	{
		// Token: 0x0400144F RID: 5199
		Send,
		// Token: 0x04001450 RID: 5200
		Broadcast,
		// Token: 0x04001451 RID: 5201
		SendUpwards
	}
}
