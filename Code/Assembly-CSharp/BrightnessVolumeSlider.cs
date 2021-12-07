using System;

// Token: 0x02000104 RID: 260
public class BrightnessVolumeSlider : CleverValueSlider
{
	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002BD58 File Offset: 0x00029F58
	// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0002BD64 File Offset: 0x00029F64
	public override float Value
	{
		get
		{
			return GameSettings.Instance.Brightness;
		}
		set
		{
			GameSettings.Instance.Brightness = value;
			SettingsScreen.Instance.SetDirty();
		}
	}
}
