using System;
using Game;

// Token: 0x020001BB RID: 443
public class SkillPointsFloatProvider : FloatValueProvider
{
	// Token: 0x06001087 RID: 4231 RVA: 0x0004B806 File Offset: 0x00049A06
	public override float GetFloatValue()
	{
		return (float)Characters.Sein.Level.SkillPoints;
	}
}
