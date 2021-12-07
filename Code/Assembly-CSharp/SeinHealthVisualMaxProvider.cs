using System;
using Game;

// Token: 0x0200061E RID: 1566
public class SeinHealthVisualMaxProvider : FloatValueProvider
{
	// Token: 0x060026C2 RID: 9922 RVA: 0x000A99A2 File Offset: 0x000A7BA2
	public override float GetFloatValue()
	{
		return Characters.Sein.Mortality.Health.VisualMaxAmount / this.DivideBy;
	}

	// Token: 0x04002164 RID: 8548
	public float DivideBy = 1f;
}
