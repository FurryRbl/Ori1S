using System;

// Token: 0x02000119 RID: 281
public class ContrastVolumeSlider : CleverValueSlider
{
	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00030185 File Offset: 0x0002E385
	// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00030191 File Offset: 0x0002E391
	public override float Value
	{
		get
		{
			return GameSettings.Instance.Contrast;
		}
		set
		{
			GameSettings.Instance.Contrast = value;
			SettingsScreen.Instance.SetDirty();
		}
	}
}
