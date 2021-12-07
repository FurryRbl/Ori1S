using System;

// Token: 0x02000131 RID: 305
public class ResetSettingsToDefaultAction : ActionMethod
{
	// Token: 0x06000C59 RID: 3161 RVA: 0x00038630 File Offset: 0x00036830
	public override void Perform(IContext context)
	{
		GameSettings instance = GameSettings.Instance;
		instance.DamageTextEnabled = true;
		instance.MotionBlurEnabled = true;
		instance.VibrationStrength = 1f;
		instance.Vsync = true;
	}
}
