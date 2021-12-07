using System;
using UnityEngine;

// Token: 0x02000991 RID: 2449
public class RandomAnimationPlayer : MonoBehaviour
{
	// Token: 0x06003583 RID: 13699 RVA: 0x000E0704 File Offset: 0x000DE904
	public void Awake()
	{
		this.m_spriteAnimator = base.GetComponent<SpriteAnimator>();
		foreach (RandomAnimationPlayer.WeightedAnimation weightedAnimation in this.Animations)
		{
			this.m_totalWeight += weightedAnimation.Weight;
		}
	}

	// Token: 0x06003584 RID: 13700 RVA: 0x000E0750 File Offset: 0x000DE950
	public void FixedUpdate()
	{
		float time = this.m_spriteAnimator.TextureAnimator.Time;
		if (this.m_time > time || this.m_spriteAnimator.AnimationEnded)
		{
			this.m_time = 0f;
			this.OnAnimationLooped();
		}
		else
		{
			this.m_time = time;
		}
	}

	// Token: 0x06003585 RID: 13701 RVA: 0x000E07A8 File Offset: 0x000DE9A8
	public void OnAnimationLooped()
	{
		float num = FixedRandom.Range(0f, this.m_totalWeight, FixedRandom.IndexFromPosition(base.transform.position));
		foreach (RandomAnimationPlayer.WeightedAnimation weightedAnimation in this.Animations)
		{
			num -= weightedAnimation.Weight;
			if (num <= 0f)
			{
				this.m_spriteAnimator.SetAnimation(weightedAnimation.Animation, true);
				this.m_spriteAnimator.AnimatorDriver.Restart();
				return;
			}
		}
	}

	// Token: 0x0400301A RID: 12314
	private SpriteAnimator m_spriteAnimator;

	// Token: 0x0400301B RID: 12315
	private float m_time;

	// Token: 0x0400301C RID: 12316
	public RandomAnimationPlayer.WeightedAnimation[] Animations;

	// Token: 0x0400301D RID: 12317
	private float m_totalWeight;

	// Token: 0x02000992 RID: 2450
	[Serializable]
	public class WeightedAnimation
	{
		// Token: 0x0400301E RID: 12318
		public TextureAnimation Animation;

		// Token: 0x0400301F RID: 12319
		public float Weight;
	}
}
