using System;
using Game;

// Token: 0x02000613 RID: 1555
public class SeinBreathRemainingValueProvider : FloatValueProvider
{
	// Token: 0x060026AC RID: 9900 RVA: 0x000A9823 File Offset: 0x000A7A23
	public override float GetFloatValue()
	{
		return Characters.Sein.Abilities.Swimming.RemainingBreath / Characters.Sein.Abilities.Swimming.Breath;
	}
}
