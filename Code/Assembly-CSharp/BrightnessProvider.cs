using System;

// Token: 0x020006A2 RID: 1698
public class BrightnessProvider : FloatValueProvider
{
	// Token: 0x0600291C RID: 10524 RVA: 0x000B1A51 File Offset: 0x000AFC51
	public override float GetFloatValue()
	{
		return GameSettings.Instance.Brightness;
	}
}
