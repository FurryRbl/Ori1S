using System;

// Token: 0x02000137 RID: 311
public class SoundVolumeSlider : CleverValueSlider
{
	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00038B5E File Offset: 0x00036D5E
	// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00038B6A File Offset: 0x00036D6A
	public override float Value
	{
		get
		{
			return GameSettings.Instance.SoundEffectsVolume;
		}
		set
		{
			GameSettings.Instance.SoundEffectsVolume = value;
			SettingsScreen.Instance.SetDirty();
		}
	}
}
