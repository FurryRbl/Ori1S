using System;

// Token: 0x02000124 RID: 292
public class LanguageOptions : CleverMenuOptionsList
{
	// Token: 0x06000BD9 RID: 3033 RVA: 0x00034E74 File Offset: 0x00033074
	public new void OnEnable()
	{
		base.OnEnable();
		base.ClearItems();
		base.AddItem(Language.English, this.English.ToString(), delegate()
		{
			this.SetLanguage(Language.English);
		});
		base.AddItem(Language.Chinese, this.Chinese.ToString(), delegate()
		{
			this.SetLanguage(Language.Chinese);
		});
		base.AddItem(Language.Italian, this.Italian.ToString(), delegate()
		{
			this.SetLanguage(Language.Italian);
		});
		base.AddItem(Language.German, this.German.ToString(), delegate()
		{
			this.SetLanguage(Language.German);
		});
		base.AddItem(Language.Spanish, this.Spanish.ToString(), delegate()
		{
			this.SetLanguage(Language.Spanish);
		});
		base.AddItem(Language.Japanese, this.Japanese.ToString(), delegate()
		{
			this.SetLanguage(Language.Japanese);
		});
		base.AddItem(Language.Portuguese, this.Portuguese.ToString(), delegate()
		{
			this.SetLanguage(Language.Portuguese);
		});
		base.AddItem(Language.Russian, this.Russian.ToString(), delegate()
		{
			this.SetLanguage(Language.Russian);
		});
		base.SetSelection(0);
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00034F84 File Offset: 0x00033184
	public void SetLanguage(Language language)
	{
		GameSettings.Instance.Language = language;
		SettingsScreen.Instance.SetDirty();
	}

	// Token: 0x040009A0 RID: 2464
	public MessageProvider English;

	// Token: 0x040009A1 RID: 2465
	public MessageProvider French;

	// Token: 0x040009A2 RID: 2466
	public MessageProvider Italian;

	// Token: 0x040009A3 RID: 2467
	public MessageProvider German;

	// Token: 0x040009A4 RID: 2468
	public MessageProvider Spanish;

	// Token: 0x040009A5 RID: 2469
	public MessageProvider Japanese;

	// Token: 0x040009A6 RID: 2470
	public MessageProvider Portuguese;

	// Token: 0x040009A7 RID: 2471
	public MessageProvider Chinese;

	// Token: 0x040009A8 RID: 2472
	public MessageProvider Russian;
}
