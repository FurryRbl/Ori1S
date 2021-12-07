using System;
using System.Collections.Generic;
using Game;

// Token: 0x0200039A RID: 922
public class AnimationMetaDataTrigger : LegacyAnimator
{
	// Token: 0x060019ED RID: 6637 RVA: 0x0006F57C File Offset: 0x0006D77C
	protected override void AnimateIt(float value)
	{
		if (this.UseSeinSpriteAnimation)
		{
			this.SpriteAnimatorWithTransitions = Characters.Sein.PlatformBehaviour.Visuals.Sprite.GetComponent<SpriteAnimatorWithTransitions>();
		}
		if (this.SpriteAnimatorWithTransitions.CurrentAnimation == null || this.SpriteAnimatorWithTransitions.CurrentAnimation.AnimationMetaData == null)
		{
			return;
		}
		if (this.m_previousAnimation != this.SpriteAnimatorWithTransitions.CurrentAnimation.name)
		{
			this.m_previousAnimation = this.SpriteAnimatorWithTransitions.CurrentAnimation.name;
			base.CurrentTime = 0f;
		}
		int num = (int)(this.SpriteAnimatorWithTransitions.CurrentAnimation.AnimationMetaData.FrameRate * base.CurrentTime);
		if (num >= this.SpriteAnimatorWithTransitions.CurrentAnimation.AnimationMetaData.Camera.PositionX.Values.Count)
		{
			return;
		}
		for (int i = 0; i < this.NodeActionPairs.Count; i++)
		{
			foreach (AnimationMetaData.AnimationData animationData in this.SpriteAnimatorWithTransitions.CurrentAnimation.AnimationMetaData.Data)
			{
				if (!(animationData.Name != this.NodeActionPairs[i].NodeNade))
				{
					if (this.ShouldPerformAction(animationData, num))
					{
						this.NodeActionPairs[i].Action.Perform(null);
					}
				}
			}
		}
		this.m_previousframe = num;
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x0006F734 File Offset: 0x0006D934
	private bool ShouldPerformAction(AnimationMetaData.AnimationData animationData, int frame)
	{
		return this.m_previousframe != frame && animationData.ScaleY.Values[frame] > 1.5f;
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x0006F75D File Offset: 0x0006D95D
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x0400164B RID: 5707
	public AnimationMetaData AnimationMetaData;

	// Token: 0x0400164C RID: 5708
	public SpriteAnimatorWithTransitions SpriteAnimatorWithTransitions;

	// Token: 0x0400164D RID: 5709
	public bool UseSeinSpriteAnimation;

	// Token: 0x0400164E RID: 5710
	public List<NodeActionPair> NodeActionPairs = new List<NodeActionPair>();

	// Token: 0x0400164F RID: 5711
	private int m_previousframe;

	// Token: 0x04001650 RID: 5712
	private string m_previousAnimation = string.Empty;
}
