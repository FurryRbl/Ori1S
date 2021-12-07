using System;
using Game;

// Token: 0x02000614 RID: 1556
public class SeinEnergyMaxVisualNormalizedProvider : FloatValueProvider
{
	// Token: 0x060026AE RID: 9902 RVA: 0x000A9856 File Offset: 0x000A7A56
	public override float GetFloatValue()
	{
		return Characters.Sein.Energy.VisualMaxNormalized;
	}
}
