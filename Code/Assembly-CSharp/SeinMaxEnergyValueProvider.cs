using System;
using Game;

// Token: 0x02000622 RID: 1570
public class SeinMaxEnergyValueProvider : FloatValueProvider
{
	// Token: 0x060026CA RID: 9930 RVA: 0x000A9A3C File Offset: 0x000A7C3C
	public override float GetFloatValue()
	{
		return Characters.Sein.Energy.Max / this.DivideBy;
	}

	// Token: 0x04002166 RID: 8550
	public float DivideBy = 1f;
}
