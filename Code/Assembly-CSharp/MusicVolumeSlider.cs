using System;

// Token: 0x02000126 RID: 294
public class MusicVolumeSlider : CleverValueSlider
{
	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00034FF3 File Offset: 0x000331F3
	// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00034FFF File Offset: 0x000331FF
	public override float Value
	{
		get
		{
			return GameSettings.Instance.MusicVolume;
		}
		set
		{
			GameSettings.Instance.MusicVolume = value;
			SettingsScreen.Instance.SetDirty();
		}
	}
}
