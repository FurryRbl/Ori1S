using System;
using System.Collections.Generic;

// Token: 0x020004E5 RID: 1253
public class DifficultyModeMessageProvider : MessageProvider
{
	// Token: 0x060021CF RID: 8655 RVA: 0x00093BE0 File Offset: 0x00091DE0
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		MessageProvider modeMessage = this.Easy;
		switch (DifficultyController.Instance.Difficulty)
		{
		case DifficultyMode.Easy:
			modeMessage = this.Easy;
			break;
		case DifficultyMode.Normal:
			modeMessage = this.Normal;
			break;
		case DifficultyMode.Hard:
			modeMessage = this.Hard;
			break;
		case DifficultyMode.OneLife:
			modeMessage = this.OneLife;
			break;
		}
		yield return new MessageDescriptor(this.Difficulty.ToString() + ": " + modeMessage.ToString());
		yield break;
	}

	// Token: 0x04001C65 RID: 7269
	public MessageProvider Easy;

	// Token: 0x04001C66 RID: 7270
	public MessageProvider Normal;

	// Token: 0x04001C67 RID: 7271
	public MessageProvider Hard;

	// Token: 0x04001C68 RID: 7272
	public MessageProvider OneLife;

	// Token: 0x04001C69 RID: 7273
	public MessageProvider Difficulty;
}
