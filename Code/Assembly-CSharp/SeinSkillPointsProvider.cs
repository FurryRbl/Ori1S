using System;
using Game;

// Token: 0x02000624 RID: 1572
public class SeinSkillPointsProvider : FloatValueProvider
{
	// Token: 0x060026CE RID: 9934 RVA: 0x000A9A8D File Offset: 0x000A7C8D
	public override float GetFloatValue()
	{
		return (float)Characters.Sein.Level.SkillPoints;
	}
}
