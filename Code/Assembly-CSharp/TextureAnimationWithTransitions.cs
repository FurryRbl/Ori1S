using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class TextureAnimationWithTransitions : ScriptableObject
{
	// Token: 0x0600010C RID: 268 RVA: 0x00005760 File Offset: 0x00003960
	public TextureAnimationWithTransitions.TextureAnimationPair GetTransition(int currentFrame, TextureAnimationWithTransitions from, TextureAnimation fromAnimation, bool flip)
	{
		for (int i = 0; i < this.Transitions.Count; i++)
		{
			TextureAnimationWithTransitions.TextureAnimationPair textureAnimationPair = this.Transitions[i];
			if (!textureAnimationPair.From || !(textureAnimationPair.From != from))
			{
				if (!textureAnimationPair.FromAnimation || !(textureAnimationPair.FromAnimation != fromAnimation))
				{
					if (textureAnimationPair.MinFrame == textureAnimationPair.MaxFrame || (textureAnimationPair.MinFrame <= currentFrame && textureAnimationPair.MaxFrame > currentFrame))
					{
						if (!textureAnimationPair.Flip || flip)
						{
							return textureAnimationPair;
						}
					}
				}
			}
		}
		if (this.Parent)
		{
			return this.Parent.GetTransition(currentFrame, from, fromAnimation, flip);
		}
		return null;
	}

	// Token: 0x040000DA RID: 218
	public TextureAnimation Animation;

	// Token: 0x040000DB RID: 219
	public List<TextureAnimationWithTransitions.TextureAnimationPair> Transitions = new List<TextureAnimationWithTransitions.TextureAnimationPair>();

	// Token: 0x040000DC RID: 220
	public TextureAnimationWithTransitions Parent;

	// Token: 0x0200038F RID: 911
	[Serializable]
	public class TextureAnimationPair
	{
		// Token: 0x0400161E RID: 5662
		public TextureAnimation TransitionAnimation;

		// Token: 0x0400161F RID: 5663
		public TextureAnimationWithTransitions From;

		// Token: 0x04001620 RID: 5664
		public TextureAnimation FromAnimation;

		// Token: 0x04001621 RID: 5665
		public bool Flip;

		// Token: 0x04001622 RID: 5666
		public int CrossoverFrame;

		// Token: 0x04001623 RID: 5667
		public int MinFrame;

		// Token: 0x04001624 RID: 5668
		public int MaxFrame;

		// Token: 0x04001625 RID: 5669
		public int TransitionStart;

		// Token: 0x04001626 RID: 5670
		public int TransitionEnd = 6;
	}
}
