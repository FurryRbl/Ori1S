using System;
using System.Collections.Generic;

// Token: 0x02000678 RID: 1656
public class ForceLanguageTranslatedMessageProvider : TranslatedMessageProvider
{
	// Token: 0x06002846 RID: 10310 RVA: 0x000AEB4C File Offset: 0x000ACD4C
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		foreach (TranslatedMessageProvider.MessageItem message in this.Messages)
		{
			yield return message.GetDescriptor(this.Language);
		}
		yield break;
	}

	// Token: 0x040023C1 RID: 9153
	public Language Language;
}
