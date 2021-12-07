using System;
using UnityEngine;

// Token: 0x0200052C RID: 1324
[Serializable]
public class TwistAnimationSet
{
	// Token: 0x06002319 RID: 8985 RVA: 0x00099D7C File Offset: 0x00097F7C
	public TextureAnimationWithTransitions GetAnimation(float a)
	{
		int num = this.Animations.Length - 1;
		int num2 = Mathf.Clamp(Mathf.RoundToInt(a * (float)num), 0, num);
		return this.Animations[num2];
	}

	// Token: 0x04001D92 RID: 7570
	public TextureAnimationWithTransitions[] Animations;
}
