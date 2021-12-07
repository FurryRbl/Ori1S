using System;

// Token: 0x020006AD RID: 1709
public class VibrationStrengthProvider : FloatValueProvider
{
	// Token: 0x06002938 RID: 10552 RVA: 0x000B1F5F File Offset: 0x000B015F
	public override float GetFloatValue()
	{
		return GameSettings.Instance.VibrationStrength;
	}
}
