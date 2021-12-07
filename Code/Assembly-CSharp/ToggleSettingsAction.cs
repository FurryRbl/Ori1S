using System;
using Core;

// Token: 0x0200013A RID: 314
public class ToggleSettingsAction : ActionMethod
{
	// Token: 0x06000C73 RID: 3187 RVA: 0x00038CFC File Offset: 0x00036EFC
	public override void Perform(IContext context)
	{
		switch (this.Setting)
		{
		case ToggleSettingsAction.SettingType.Fullscreen:
			GameSettings.Instance.Fullscreen = !GameSettings.Instance.Fullscreen;
			this.PlaySound(GameSettings.Instance.Fullscreen);
			break;
		case ToggleSettingsAction.SettingType.DamageText:
			GameSettings.Instance.DamageTextEnabled = !GameSettings.Instance.DamageTextEnabled;
			this.PlaySound(GameSettings.Instance.DamageTextEnabled);
			break;
		case ToggleSettingsAction.SettingType.MotionBlur:
			GameSettings.Instance.MotionBlurEnabled = !GameSettings.Instance.MotionBlurEnabled;
			this.PlaySound(GameSettings.Instance.MotionBlurEnabled);
			break;
		case ToggleSettingsAction.SettingType.VSync:
			GameSettings.Instance.Vsync = !GameSettings.Instance.Vsync;
			this.PlaySound(GameSettings.Instance.Vsync);
			break;
		}
		SettingsScreen.Instance.SetDirty();
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x00038DEC File Offset: 0x00036FEC
	private void PlaySound(bool on)
	{
		if (on && this.OnSound)
		{
			Sound.Play(this.OnSound.GetSound(null), base.transform.position, null);
		}
		else if (this.OffSound && !on)
		{
			Sound.Play(this.OffSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x04000A4B RID: 2635
	public ToggleSettingsAction.SettingType Setting;

	// Token: 0x04000A4C RID: 2636
	public SoundProvider OnSound;

	// Token: 0x04000A4D RID: 2637
	public SoundProvider OffSound;

	// Token: 0x0200013B RID: 315
	public enum SettingType
	{
		// Token: 0x04000A4F RID: 2639
		Fullscreen,
		// Token: 0x04000A50 RID: 2640
		Vibration,
		// Token: 0x04000A51 RID: 2641
		DamageText,
		// Token: 0x04000A52 RID: 2642
		MotionBlur,
		// Token: 0x04000A53 RID: 2643
		VSync
	}
}
