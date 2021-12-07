using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class TranslatedMessageProvider : MessageProvider
{
	// Token: 0x06000C27 RID: 3111 RVA: 0x0003646C File Offset: 0x0003466C
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		if (Application.isPlaying)
		{
			foreach (TranslatedMessageProvider.MessageItem message in this.Messages)
			{
				yield return message.GetDescriptor(GameSettings.Instance.Language);
			}
		}
		else
		{
			foreach (TranslatedMessageProvider.MessageItem message2 in this.Messages)
			{
				yield return message2.GetDescriptor(Language.English);
			}
		}
		yield break;
	}

	// Token: 0x040009F9 RID: 2553
	public List<TranslatedMessageProvider.MessageItem> Messages = new List<TranslatedMessageProvider.MessageItem>();

	// Token: 0x0200067A RID: 1658
	[Serializable]
	public class MessageItem
	{
		// Token: 0x06002850 RID: 10320 RVA: 0x000AED08 File Offset: 0x000ACF08
		public string Message(Language language)
		{
			switch (language)
			{
			case Language.English:
				return this.English;
			case Language.French:
				return this.French;
			case Language.Italian:
				return this.Italian;
			case Language.German:
				return this.German;
			case Language.Spanish:
				return this.Spanish;
			case Language.Japanese:
				return this.Japanese;
			case Language.Portuguese:
				return this.Portuguese;
			case Language.Chinese:
				return this.Chinese;
			case Language.Russian:
				return this.Russian;
			default:
				return this.English;
			}
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000AED8C File Offset: 0x000ACF8C
		public MessageDescriptor GetDescriptor(Language language)
		{
			string text = this.Message(language);
			if (text != string.Empty)
			{
				return new MessageDescriptor(text, this.Emotion, this.Sound);
			}
			return new MessageDescriptor("_Empty_пусто_空_空の_", this.Emotion, this.Sound);
		}

		// Token: 0x040023C7 RID: 9159
		public string English;

		// Token: 0x040023C8 RID: 9160
		public string French;

		// Token: 0x040023C9 RID: 9161
		public string Italian;

		// Token: 0x040023CA RID: 9162
		public string German;

		// Token: 0x040023CB RID: 9163
		public string Spanish;

		// Token: 0x040023CC RID: 9164
		public string Japanese;

		// Token: 0x040023CD RID: 9165
		public string Portuguese;

		// Token: 0x040023CE RID: 9166
		public string Chinese;

		// Token: 0x040023CF RID: 9167
		public string Russian;

		// Token: 0x040023D0 RID: 9168
		public EmotionType Emotion;

		// Token: 0x040023D1 RID: 9169
		public SoundProvider Sound;
	}
}
