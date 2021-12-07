using System;
using Game;

// Token: 0x02000620 RID: 1568
public class SeinHealthVisualMinProvider : FloatValueProvider
{
	// Token: 0x060026C6 RID: 9926 RVA: 0x000A99F0 File Offset: 0x000A7BF0
	public override float GetFloatValue()
	{
		return Characters.Sein.Mortality.Health.VisualMinAmount / this.DivideBy;
	}

	// Token: 0x04002165 RID: 8549
	public float DivideBy = 1f;
}
