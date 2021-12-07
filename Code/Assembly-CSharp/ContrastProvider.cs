using System;

// Token: 0x020006A3 RID: 1699
public class ContrastProvider : FloatValueProvider
{
	// Token: 0x0600291E RID: 10526 RVA: 0x000B1A65 File Offset: 0x000AFC65
	public override float GetFloatValue()
	{
		return GameSettings.Instance.Contrast;
	}
}
