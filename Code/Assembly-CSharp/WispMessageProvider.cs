using System;
using System.Collections.Generic;

// Token: 0x02000690 RID: 1680
public class WispMessageProvider : MessageProvider
{
	// Token: 0x060028A9 RID: 10409 RVA: 0x000B0BE0 File Offset: 0x000AEDE0
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		DeathInformation deathInformation = DeathWispsManager.Instance.Collected;
		int totalSeconds = deathInformation.TimeOfDeath;
		int hours = totalSeconds / 3600;
		int minutes = totalSeconds / 60 - hours * 60;
		int seconds = totalSeconds - minutes * 60 - hours * 3600;
		string message = this.Message.ToString().Replace("[HOURS]", hours.ToString()).Replace("[MINUTES]", minutes.ToString());
		yield return new MessageDescriptor(message);
		yield break;
	}

	// Token: 0x04002453 RID: 9299
	public MessageProvider Message;
}
