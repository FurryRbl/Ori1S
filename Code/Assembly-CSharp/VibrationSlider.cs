using System;

// Token: 0x0200013D RID: 317
public class VibrationSlider : CleverValueSlider
{
	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00038ED5 File Offset: 0x000370D5
	// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00038EE1 File Offset: 0x000370E1
	public override float Value
	{
		get
		{
			return GameSettings.Instance.VibrationStrength;
		}
		set
		{
			GameSettings.Instance.VibrationStrength = value;
			SettingsScreen.Instance.SetDirty();
		}
	}
}
