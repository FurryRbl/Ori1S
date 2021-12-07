using System;

// Token: 0x020007AC RID: 1964
public class UberShaderPrewarmerProgress : FloatValueProvider
{
	// Token: 0x06002D78 RID: 11640 RVA: 0x000C22CF File Offset: 0x000C04CF
	public override float GetFloatValue()
	{
		return UberShaderPrewarmer.WarmProgress;
	}
}
