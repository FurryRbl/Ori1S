using System;
using Core;

// Token: 0x020006B5 RID: 1717
public class SetSoundCompositionAction : ActionMethod
{
	// Token: 0x06002957 RID: 10583 RVA: 0x000B2699 File Offset: 0x000B0899
	public override void Perform(IContext context)
	{
		Core.SoundComposition.Manager.PlaySound(this.SoundComposition, this.Transition);
	}

	// Token: 0x040024E0 RID: 9440
	public global::SoundComposition SoundComposition;

	// Token: 0x040024E1 RID: 9441
	public SoundCompositionTransition Transition;
}
