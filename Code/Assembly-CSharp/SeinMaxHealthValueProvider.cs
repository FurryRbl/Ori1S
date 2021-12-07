using System;
using Game;

// Token: 0x02000623 RID: 1571
public class SeinMaxHealthValueProvider : FloatValueProvider
{
	// Token: 0x060026CC RID: 9932 RVA: 0x000A9A67 File Offset: 0x000A7C67
	public override float GetFloatValue()
	{
		return (float)Characters.Sein.Mortality.Health.MaxHealth / this.DivideBy;
	}

	// Token: 0x04002167 RID: 8551
	public float DivideBy = 1f;
}
