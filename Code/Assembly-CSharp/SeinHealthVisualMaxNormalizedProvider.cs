using System;
using Game;

// Token: 0x0200061D RID: 1565
public class SeinHealthVisualMaxNormalizedProvider : FloatValueProvider
{
	// Token: 0x060026C0 RID: 9920 RVA: 0x000A9979 File Offset: 0x000A7B79
	public override float GetFloatValue()
	{
		return Characters.Sein.Mortality.Health.VisualMaxAmountNormalized;
	}
}
