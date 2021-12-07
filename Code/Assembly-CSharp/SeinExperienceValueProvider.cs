using System;
using Game;
using UnityEngine;

// Token: 0x0200061A RID: 1562
public class SeinExperienceValueProvider : FloatValueProvider
{
	// Token: 0x060026BA RID: 9914 RVA: 0x000A9928 File Offset: 0x000A7B28
	public override float GetFloatValue()
	{
		return Mathf.Round((float)Characters.Sein.Level.Experience);
	}
}
