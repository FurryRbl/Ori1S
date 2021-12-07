using System;
using Game;

// Token: 0x02000615 RID: 1557
public class SeinEnergyMaxVisualProvider : FloatValueProvider
{
	// Token: 0x060026B0 RID: 9904 RVA: 0x000A987A File Offset: 0x000A7A7A
	public override float GetFloatValue()
	{
		return Characters.Sein.Energy.MaxVisual / this.DivideBy;
	}

	// Token: 0x04002162 RID: 8546
	public float DivideBy = 1f;
}
