using System;
using System.Collections.Generic;

// Token: 0x02000675 RID: 1653
public class CombinedMessageProvider : MessageProvider
{
	// Token: 0x0600283C RID: 10300 RVA: 0x000AE9F8 File Offset: 0x000ACBF8
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		yield return new MessageDescriptor(((!this.FirstMessageProvider) ? string.Empty : this.FirstMessageProvider.ToString()) + this.CombiningString + ((!this.SecondMessageProvider) ? string.Empty : this.SecondMessageProvider.ToString()));
		yield break;
	}

	// Token: 0x040023B5 RID: 9141
	public MessageProvider FirstMessageProvider;

	// Token: 0x040023B6 RID: 9142
	public string CombiningString;

	// Token: 0x040023B7 RID: 9143
	public MessageProvider SecondMessageProvider;
}
