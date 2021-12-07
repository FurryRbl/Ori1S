using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class AnimationMetaDataBasedSoundPlayer : MonoBehaviour
{
	// Token: 0x06001648 RID: 5704 RVA: 0x00062460 File Offset: 0x00060660
	private void FixedUpdate()
	{
		if (this.Animator.CurrentAnimation.AnimationMetaData == null)
		{
			return;
		}
		int num = (int)this.Animator.TextureAnimator.Frame;
		for (int i = 0; i < this.NodeSoundPlayerPairs.Count; i++)
		{
			foreach (AnimationMetaData.AnimationData animationData in this.Animator.CurrentAnimation.AnimationMetaData.Data)
			{
				if (!(animationData.Name != this.NodeSoundPlayerPairs[i].NodeName))
				{
					if (num < animationData.PositionX.Values.Count)
					{
						if (this.ShouldPlaySound(animationData, num))
						{
							Sound.Play(this.NodeSoundPlayerPairs[i].SoundProvider.GetSound(null), this.Animator.transform.position + new Vector3(animationData.PositionX.Values[num] * this.Animator.transform.localScale.x, animationData.PositionY.Values[num] * this.Animator.transform.localScale.y, 0f), null);
						}
					}
				}
			}
		}
		this.m_previousframe = (int)this.Animator.TextureAnimator.Frame;
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x00062604 File Offset: 0x00060804
	private bool ShouldPlaySound(AnimationMetaData.AnimationData animationData, int frame)
	{
		return this.m_previousframe != frame && this.ShuoldPlaySoundInFrame(animationData, frame);
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x0006261C File Offset: 0x0006081C
	private bool ShuoldPlaySoundInFrame(AnimationMetaData.AnimationData animationData, int frame)
	{
		return animationData.ScaleY.Values[frame] > 1.5f;
	}

	// Token: 0x0400134C RID: 4940
	public SpriteAnimatorWithTransitions Animator;

	// Token: 0x0400134D RID: 4941
	public List<NodeSoundPlayerPair> NodeSoundPlayerPairs;

	// Token: 0x0400134E RID: 4942
	private int m_previousframe;
}
