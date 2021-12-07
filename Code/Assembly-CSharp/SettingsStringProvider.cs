using System;

// Token: 0x02000134 RID: 308
public class SettingsStringProvider : StringValueProvider
{
	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00038A2C File Offset: 0x00036C2C
	private string On
	{
		get
		{
			return "ON";
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00038A33 File Offset: 0x00036C33
	private string Off
	{
		get
		{
			return "OFF";
		}
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00038A3C File Offset: 0x00036C3C
	public override string GetStringValue()
	{
		switch (this.Setting)
		{
		case SettingsStringProvider.SettingType.Resolution:
			return string.Format("{0}x{1}", GameSettings.Instance.Resolution.x, GameSettings.Instance.Resolution.y);
		case SettingsStringProvider.SettingType.Fullscreen:
			return (!GameSettings.Instance.Fullscreen) ? this.Off : this.On;
		case SettingsStringProvider.SettingType.DamageText:
			return (!GameSettings.Instance.DamageTextEnabled) ? this.Off : this.On;
		case SettingsStringProvider.SettingType.MotionBlur:
			return (!GameSettings.Instance.MotionBlurEnabled) ? this.Off : this.On;
		case SettingsStringProvider.SettingType.Language:
			return GameSettings.Instance.Language.ToString();
		case SettingsStringProvider.SettingType.VSync:
			return (!GameSettings.Instance.Vsync) ? this.Off : this.On;
		}
		return string.Empty;
	}

	// Token: 0x04000A3E RID: 2622
	public SettingsStringProvider.SettingType Setting;

	// Token: 0x02000136 RID: 310
	public enum SettingType
	{
		// Token: 0x04000A40 RID: 2624
		Resolution,
		// Token: 0x04000A41 RID: 2625
		Fullscreen,
		// Token: 0x04000A42 RID: 2626
		Vibration,
		// Token: 0x04000A43 RID: 2627
		DamageText,
		// Token: 0x04000A44 RID: 2628
		MotionBlur,
		// Token: 0x04000A45 RID: 2629
		Language,
		// Token: 0x04000A46 RID: 2630
		VSync
	}
}
