using System;
using Game;

// Token: 0x02000312 RID: 786
[Category("Sein")]
internal class PlayFallAnimation : ActionMethod
{
	// Token: 0x06001749 RID: 5961 RVA: 0x00064A38 File Offset: 0x00062C38
	public override void Perform(IContext context)
	{
		Characters.Sein.Animation.PlayLoop(this.Animation, 210, new Func<bool>(this.ShouldFallAnimationKeepPlaying), false);
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x00064A6D File Offset: 0x00062C6D
	public bool ShouldFallAnimationKeepPlaying()
	{
		return Characters.Sein && Characters.Sein.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x04001404 RID: 5124
	[NotNull]
	public TextureAnimationWithTransitions Animation;
}
