using System;
using Game;

// Token: 0x0200061F RID: 1567
public class SeinHealthVisualMinNormalizedProvider : FloatValueProvider
{
	// Token: 0x060026C4 RID: 9924 RVA: 0x000A99C7 File Offset: 0x000A7BC7
	public override float GetFloatValue()
	{
		return Characters.Sein.Mortality.Health.VisualMinAmountNormalized;
	}
}
