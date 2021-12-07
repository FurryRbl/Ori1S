using System;
using System.Collections.Generic;

// Token: 0x0200024C RID: 588
public class AchievementsTestMessageProvider : MessageProvider
{
	// Token: 0x060013F8 RID: 5112 RVA: 0x0005B236 File Offset: 0x00059436
	public void SetText(string text)
	{
		this.MessageDescriptor.Message = text;
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x0005B244 File Offset: 0x00059444
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		yield return this.MessageDescriptor;
		yield break;
	}

	// Token: 0x04001193 RID: 4499
	public MessageDescriptor MessageDescriptor;
}
