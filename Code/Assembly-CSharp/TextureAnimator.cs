using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
[Serializable]
public class TextureAnimator
{
	// Token: 0x06000326 RID: 806 RVA: 0x0000D3AC File Offset: 0x0000B5AC
	public TextureAnimator()
	{
		this.SpeedMultiplier = 1f;
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000327 RID: 807 RVA: 0x0000D3ED File Offset: 0x0000B5ED
	// (set) Token: 0x06000328 RID: 808 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
	public float SpeedMultiplier { get; set; }

	// Token: 0x06000329 RID: 809 RVA: 0x0000D400 File Offset: 0x0000B600
	public void Advance(float timeDelta)
	{
		this.Time += timeDelta * (this.Animation.Speed / 30f) * this.SpeedMultiplier;
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x0600032A RID: 810 RVA: 0x0000D438 File Offset: 0x0000B638
	public float AnimationDuration
	{
		get
		{
			if (this.m_animation == null)
			{
				return 0f;
			}
			return this.m_animation.Duration;
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0000D468 File Offset: 0x0000B668
	public void ConstrainTime()
	{
		float animationDuration = this.AnimationDuration;
		if (!this.m_animation)
		{
			return;
		}
		if (this.m_animation.Loop)
		{
			int num = (this.m_time <= 0f) ? 0 : Mathf.FloorToInt(this.m_time / animationDuration);
			this.m_time = ((this.m_time <= 0f) ? 0f : Mathf.Repeat(this.m_time, animationDuration));
			if (num > this.m_currentLoop)
			{
				this.m_currentLoop = num;
				this.OnAnimationStart();
			}
		}
		else if (this.m_time >= animationDuration && animationDuration > 0f)
		{
			this.m_time = animationDuration;
		}
		this.m_time = Mathf.Max(this.m_time, 0f);
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0000D548 File Offset: 0x0000B748
	public void SetAnimation(TextureAnimation animation, bool resetTime = true)
	{
		if (!animation)
		{
			return;
		}
		this.m_animation = animation;
		if (resetTime)
		{
			this.Time = 0f;
			this.OnAnimationStart();
		}
		else
		{
			this.ConstrainTime();
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x0600032D RID: 813 RVA: 0x0000D58F File Offset: 0x0000B78F
	public TextureAnimation Animation
	{
		get
		{
			return this.m_animation;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x0600032E RID: 814 RVA: 0x0000D598 File Offset: 0x0000B798
	public AtlasSpriteTexture Texture
	{
		get
		{
			Atlas atlas;
			return this.m_animation.GetTextureAtTime(this.m_time, out atlas);
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x0600032F RID: 815 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
	// (set) Token: 0x06000330 RID: 816 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
	public float Time
	{
		get
		{
			return this.m_time;
		}
		set
		{
			this.m_time = value;
			this.ConstrainTime();
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000331 RID: 817 RVA: 0x0000D5CF File Offset: 0x0000B7CF
	// (set) Token: 0x06000332 RID: 818 RVA: 0x0000D5E2 File Offset: 0x0000B7E2
	public float Frame
	{
		get
		{
			return this.m_animation.TimeToFrame(this.m_time);
		}
		set
		{
			this.Time = this.m_animation.FrameToTime(value);
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000333 RID: 819 RVA: 0x0000D5F6 File Offset: 0x0000B7F6
	public bool AnimationEnded
	{
		get
		{
			return !(this.m_animation == null) && this.m_time >= this.AnimationDuration - 0.001f && !this.m_animation.Loop;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0000D634 File Offset: 0x0000B834
	public float GetRotationFromMetaData(string name)
	{
		AnimationMetaData animationMetaData = this.m_animation.AnimationMetaData;
		if (!animationMetaData)
		{
			return 0f;
		}
		AnimationMetaData.AnimationData animationData = animationMetaData.FindData(name);
		if (animationData == null)
		{
			return 0f;
		}
		return animationData.RotationZ.GetValueAtTime(this.m_time);
	}

	// Token: 0x04000253 RID: 595
	public Action OnAnimationStart = delegate()
	{
	};

	// Token: 0x04000254 RID: 596
	private int m_currentLoop;

	// Token: 0x04000255 RID: 597
	private TextureAnimation m_animation;

	// Token: 0x04000256 RID: 598
	private float m_time;
}
