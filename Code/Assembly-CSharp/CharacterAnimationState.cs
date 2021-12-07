using System;

// Token: 0x0200041D RID: 1053
public abstract class CharacterAnimationState : CharacterAnimationStateBase
{
	// Token: 0x170004F9 RID: 1273
	// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00081A1B File Offset: 0x0007FC1B
	public override TextureAnimationWithTransitions AnimationToPlay
	{
		get
		{
			return this.Animation;
		}
	}

	// Token: 0x0400197B RID: 6523
	public TextureAnimationWithTransitions Animation;
}
