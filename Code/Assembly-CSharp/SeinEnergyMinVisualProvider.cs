using System;
using Game;

// Token: 0x02000617 RID: 1559
public class SeinEnergyMinVisualProvider : FloatValueProvider
{
	// Token: 0x060026B4 RID: 9908 RVA: 0x000A98BE File Offset: 0x000A7ABE
	public override float GetFloatValue()
	{
		return Characters.Sein.Energy.MinVisual / this.DivideBy;
	}

	// Token: 0x04002163 RID: 8547
	public float DivideBy = 1f;
}
