using System;
using Game;

// Token: 0x0200061B RID: 1563
public class SeinExperienceVisualMaxNormalizedProvider : FloatValueProvider
{
	// Token: 0x060026BC RID: 9916 RVA: 0x000A9947 File Offset: 0x000A7B47
	public override float GetFloatValue()
	{
		return Characters.Sein.Level.ExperienceVisualMaxNormalized;
	}
}
