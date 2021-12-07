using System;
using Game;
using UnityEngine;

// Token: 0x02000618 RID: 1560
public class SeinExperienceForNextLevelValueProvider : FloatValueProvider
{
	// Token: 0x060026B6 RID: 9910 RVA: 0x000A98DE File Offset: 0x000A7ADE
	public override float GetFloatValue()
	{
		return Mathf.Round((float)Characters.Sein.Level.ExperienceForNextLevel);
	}
}
