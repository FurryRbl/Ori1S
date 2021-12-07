using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class OptionsScreenTextsManager : MonoBehaviour
{
	// Token: 0x06000C22 RID: 3106 RVA: 0x000360FC File Offset: 0x000342FC
	public void UpdateItems()
	{
		string aspectRatio = ResolutionOptions.GetAspectRatio(GameSettings.Instance.Resolution, 0.015f);
		this.ResolutionMessageBox.SetMessage(new MessageDescriptor(string.Empty + string.Format("{0}x{1} " + aspectRatio, GameSettings.Instance.Resolution.x, GameSettings.Instance.Resolution.y)));
		this.FullScreenMessageBox.SetMessage(new MessageDescriptor(string.Empty + ((!GameSettings.Instance.Fullscreen) ? this.OffMessageProvider : this.OnMessageProvider)));
		this.SoundVolumeMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.SoundVolumeMessageProvider));
		this.MusicVolumeMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.MusicVolumeMessageProvider));
		switch (GameSettings.Instance.CurrentControlScheme)
		{
		case ControlScheme.Controller:
			this.ControlStyleMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.ControlStyleControllerMessageProvider));
			break;
		case ControlScheme.KeyboardAndMouse:
			this.ControlStyleMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.ControlStyleKeyboardAndMouseMessageProvider));
			break;
		case ControlScheme.Keyboard:
			this.ControlStyleMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.ControlStyleKeyboardMessageProvider));
			break;
		}
		KeyboardLayout keyboardLayout = GameSettings.Instance.KeyboardLayout;
		if (keyboardLayout != KeyboardLayout.QWERTY)
		{
			if (keyboardLayout == KeyboardLayout.AZERTY)
			{
				this.KeyboardLayoutMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.KeyboardLayoutAzertyMessageProvider));
			}
		}
		else
		{
			this.KeyboardLayoutMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.KeyboardLayoutQwertyMessageProvider));
		}
		this.DamageTextsMessageBox.SetMessage(new MessageDescriptor(string.Empty + ((!GameSettings.Instance.DamageTextEnabled) ? this.OffMessageProvider : this.OnMessageProvider)));
		this.MotionBlurMessageBox.SetMessage(new MessageDescriptor(string.Empty + ((!GameSettings.Instance.MotionBlurEnabled) ? this.OffMessageProvider : this.OnMessageProvider)));
		this.LanguageMessageBox.SetMessage(new MessageDescriptor(string.Empty + this.GetLanguageMessageProvider(GameSettings.Instance.Language)));
		this.VSyncMessageBox.SetMessage(new MessageDescriptor(string.Empty + ((!GameSettings.Instance.Vsync) ? this.OffMessageProvider : this.OnMessageProvider)));
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x000363C3 File Offset: 0x000345C3
	public void FixedUpdate()
	{
		this.UpdateItems();
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x000363CB File Offset: 0x000345CB
	public void Start()
	{
		this.UpdateItems();
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x000363D4 File Offset: 0x000345D4
	public TranslatedMessageProvider GetLanguageMessageProvider(Language lang)
	{
		switch (lang)
		{
		case Language.English:
			return this.EnglishMessageProvider;
		case Language.French:
			return this.FrenchMessageProvider;
		case Language.Italian:
			return this.ItalianMessageProvider;
		case Language.German:
			return this.GermanMessageProvider;
		case Language.Spanish:
			return this.SpanishMessageProvider;
		case Language.Japanese:
			return this.JapaneseMessageProvider;
		case Language.Portuguese:
			return this.PortugueseMessageProvider;
		case Language.Chinese:
			return this.ChineseMessageProvider;
		case Language.Russian:
			return this.RussianMessageProvider;
		default:
			return this.EnglishMessageProvider;
		}
	}

	// Token: 0x040009DC RID: 2524
	public MessageBox ResolutionMessageBox;

	// Token: 0x040009DD RID: 2525
	public MessageBox FullScreenMessageBox;

	// Token: 0x040009DE RID: 2526
	public MessageBox SoundVolumeMessageBox;

	// Token: 0x040009DF RID: 2527
	public MessageBox MusicVolumeMessageBox;

	// Token: 0x040009E0 RID: 2528
	public MessageBox ControlStyleMessageBox;

	// Token: 0x040009E1 RID: 2529
	public MessageBox KeyboardLayoutMessageBox;

	// Token: 0x040009E2 RID: 2530
	public MessageBox VibrationMessageBox;

	// Token: 0x040009E3 RID: 2531
	public MessageBox DamageTextsMessageBox;

	// Token: 0x040009E4 RID: 2532
	public MessageBox MotionBlurMessageBox;

	// Token: 0x040009E5 RID: 2533
	public MessageBox LanguageMessageBox;

	// Token: 0x040009E6 RID: 2534
	public MessageBox VSyncMessageBox;

	// Token: 0x040009E7 RID: 2535
	public TranslatedMessageProvider OnMessageProvider;

	// Token: 0x040009E8 RID: 2536
	public TranslatedMessageProvider OffMessageProvider;

	// Token: 0x040009E9 RID: 2537
	public TranslatedMessageProvider SoundVolumeMessageProvider;

	// Token: 0x040009EA RID: 2538
	public TranslatedMessageProvider MusicVolumeMessageProvider;

	// Token: 0x040009EB RID: 2539
	public TranslatedMessageProvider ControlStyleControllerMessageProvider;

	// Token: 0x040009EC RID: 2540
	public TranslatedMessageProvider ControlStyleKeyboardMessageProvider;

	// Token: 0x040009ED RID: 2541
	public TranslatedMessageProvider ControlStyleKeyboardAndMouseMessageProvider;

	// Token: 0x040009EE RID: 2542
	public TranslatedMessageProvider KeyboardLayoutQwertyMessageProvider;

	// Token: 0x040009EF RID: 2543
	public TranslatedMessageProvider KeyboardLayoutAzertyMessageProvider;

	// Token: 0x040009F0 RID: 2544
	public TranslatedMessageProvider EnglishMessageProvider;

	// Token: 0x040009F1 RID: 2545
	public TranslatedMessageProvider FrenchMessageProvider;

	// Token: 0x040009F2 RID: 2546
	public TranslatedMessageProvider ItalianMessageProvider;

	// Token: 0x040009F3 RID: 2547
	public TranslatedMessageProvider GermanMessageProvider;

	// Token: 0x040009F4 RID: 2548
	public TranslatedMessageProvider SpanishMessageProvider;

	// Token: 0x040009F5 RID: 2549
	public TranslatedMessageProvider JapaneseMessageProvider;

	// Token: 0x040009F6 RID: 2550
	public TranslatedMessageProvider PortugueseMessageProvider;

	// Token: 0x040009F7 RID: 2551
	public TranslatedMessageProvider ChineseMessageProvider;

	// Token: 0x040009F8 RID: 2552
	public TranslatedMessageProvider RussianMessageProvider;
}
