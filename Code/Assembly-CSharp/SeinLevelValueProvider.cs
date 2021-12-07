using System;
using Game;

// Token: 0x02000621 RID: 1569
public class SeinLevelValueProvider : FloatValueProvider
{
	// Token: 0x060026C8 RID: 9928 RVA: 0x000A9A15 File Offset: 0x000A7C15
	public override float GetFloatValue()
	{
		return (float)(Characters.Sein.Level.Current + 1);
	}
}
