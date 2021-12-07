using System;
using Game;

// Token: 0x02000619 RID: 1561
public class SeinExperienceValueNormalizedProvider : FloatValueProvider
{
	// Token: 0x060026B8 RID: 9912 RVA: 0x000A98FD File Offset: 0x000A7AFD
	public override float GetFloatValue()
	{
		return (float)Characters.Sein.Level.Experience / (float)Characters.Sein.Level.ExperienceForNextLevel;
	}
}
