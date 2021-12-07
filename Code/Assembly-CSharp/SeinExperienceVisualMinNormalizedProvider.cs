using System;
using Game;

// Token: 0x0200061C RID: 1564
public class SeinExperienceVisualMinNormalizedProvider : FloatValueProvider
{
	// Token: 0x060026BE RID: 9918 RVA: 0x000A9960 File Offset: 0x000A7B60
	public override float GetFloatValue()
	{
		return Characters.Sein.Level.ExperienceVisualMinNormalized;
	}
}
