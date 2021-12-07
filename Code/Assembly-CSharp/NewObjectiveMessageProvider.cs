using System;
using System.Collections.Generic;

// Token: 0x0200088F RID: 2191
public class NewObjectiveMessageProvider : MessageProvider
{
	// Token: 0x0600313E RID: 12606 RVA: 0x000D1CB0 File Offset: 0x000CFEB0
	public void SetMessage(MessageProvider message)
	{
		this.m_messageProvider = message;
	}

	// Token: 0x0600313F RID: 12607 RVA: 0x000D1CBC File Offset: 0x000CFEBC
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		yield return new MessageDescriptor(this.NewObjective + ": " + this.m_messageProvider);
		yield break;
	}

	// Token: 0x04002C94 RID: 11412
	public MessageProvider NewObjective;

	// Token: 0x04002C95 RID: 11413
	private MessageProvider m_messageProvider;
}
