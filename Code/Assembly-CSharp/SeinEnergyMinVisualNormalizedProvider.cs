using System;
using Game;

// Token: 0x02000616 RID: 1558
public class SeinEnergyMinVisualNormalizedProvider : FloatValueProvider
{
	// Token: 0x060026B2 RID: 9906 RVA: 0x000A989A File Offset: 0x000A7A9A
	public override float GetFloatValue()
	{
		return Characters.Sein.Energy.VisualMinNormalized;
	}
}
