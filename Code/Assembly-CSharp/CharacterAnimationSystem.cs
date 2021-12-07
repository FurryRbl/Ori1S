using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class CharacterAnimationSystem : MonoBehaviour
{
	// Token: 0x06000085 RID: 133 RVA: 0x00003F08 File Offset: 0x00002108
	public CharacterAnimationSystem.CharacterAnimationState Play(TextureAnimationWithTransitions animationToPlay, int layer = 0, Func<bool> condition = null)
	{
		int index = this.m_states.Count;
		for (int i = 0; i < this.m_states.Count; i++)
		{
			if (this.m_states[i].Layer == layer)
			{
				index = i;
				this.m_states.RemoveAt(index);
				break;
			}
			if (layer > this.m_states[i].Layer)
			{
				index = i;
				break;
			}
		}
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = new CharacterAnimationSystem.CharacterAnimationState
		{
			Animation = animationToPlay,
			ConditionFunction = condition,
			Layer = layer,
			PlayOnce = true
		};
		this.m_states.Insert(index, characterAnimationState);
		return characterAnimationState;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003FBC File Offset: 0x000021BC
	public CharacterAnimationSystem.CharacterAnimationState RestartLoop(TextureAnimationWithTransitions animationToPlay, int layer = 0, Func<bool> condition = null)
	{
		int index = this.m_states.Count;
		for (int i = 0; i < this.m_states.Count; i++)
		{
			if (this.m_states[i].Layer == layer)
			{
				index = i;
				this.m_states.RemoveAt(index);
				break;
			}
			if (layer > this.m_states[i].Layer)
			{
				index = i;
				break;
			}
		}
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = new CharacterAnimationSystem.CharacterAnimationState
		{
			Animation = animationToPlay,
			ConditionFunction = condition,
			Layer = layer
		};
		this.m_states.Insert(index, characterAnimationState);
		return characterAnimationState;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00004068 File Offset: 0x00002268
	public CharacterAnimationSystem.CharacterAnimationState PlayLoop(TextureAnimationWithTransitions animationToPlay, int layer = 0, Func<bool> condition = null, bool keepFrame = false)
	{
		int index = this.m_states.Count;
		int i = 0;
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState;
		while (i < this.m_states.Count)
		{
			if (this.m_states[i].Layer == layer)
			{
				characterAnimationState = this.m_states[i];
				if (characterAnimationState.Animation == animationToPlay)
				{
					characterAnimationState.Animation = animationToPlay;
					characterAnimationState.ConditionFunction = condition;
					characterAnimationState.Layer = layer;
					characterAnimationState.OnStopPlaying = null;
					characterAnimationState.OnStartPlaying = null;
					characterAnimationState.PlayOnce = false;
					return characterAnimationState;
				}
				index = i;
				this.m_states.RemoveAt(index);
				break;
			}
			else
			{
				if (layer > this.m_states[i].Layer)
				{
					index = i;
					break;
				}
				i++;
			}
		}
		characterAnimationState = new CharacterAnimationSystem.CharacterAnimationState
		{
			Animation = animationToPlay,
			ConditionFunction = condition,
			Layer = layer,
			KeepFrame = keepFrame
		};
		this.m_states.Insert(index, characterAnimationState);
		return characterAnimationState;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004164 File Offset: 0x00002364
	public CharacterAnimationSystem.CharacterAnimationState PlayRandom(TextureAnimationWithTransitions[] animationsToPlay, int layer, Func<bool> condition)
	{
		return this.Play(FixedRandom.GetRandomArrayItem<TextureAnimationWithTransitions>(animationsToPlay, 0), layer, condition);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00004175 File Offset: 0x00002375
	public CharacterAnimationSystem.CharacterAnimationState PlayLoopRandom(TextureAnimationWithTransitions[] animationsToPlay, int layer, Func<bool> condition)
	{
		return this.PlayLoop(FixedRandom.GetRandomArrayItem<TextureAnimationWithTransitions>(animationsToPlay, 0), layer, condition, false);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004187 File Offset: 0x00002387
	public void Start()
	{
		this.UpdateStates();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004190 File Offset: 0x00002390
	public void UpdateStates()
	{
		for (int i = 0; i < this.m_states.Count; i++)
		{
			if (!this.m_states[i].Condition || (i > 0 && this.m_states[i].PlayOnce))
			{
				this.m_states.RemoveAt(i);
				i--;
			}
		}
		if (this.m_states.Count == 0)
		{
			return;
		}
		if (this.m_lastPlayingAnimationState != this.m_states[0] || this.m_states[0].Animation != this.Animator.CurrentTextureAnimationTransitions)
		{
			bool flag = false;
			if (this.m_lastPlayingAnimationState != null)
			{
				flag = this.m_lastPlayingAnimationState.KeepFrame;
			}
			if (this.m_lastPlayingAnimationState != null && this.m_lastPlayingAnimationState.OnStopPlaying != null)
			{
				this.m_lastPlayingAnimationState.OnStopPlaying();
			}
			float currentAnimationTime = this.Animator.CurrentAnimationTime;
			this.Animator.SetAnimation(this.m_states[0].Animation, false);
			if (flag && this.m_states[0].KeepFrame && this.m_lastPlayingAnimationState.Animation.Animation.FrameGuids.Count == this.m_states[0].Animation.Animation.FrameGuids.Count)
			{
				this.Animator.CurrentAnimationTime = currentAnimationTime;
			}
			this.m_lastPlayingAnimationState = this.m_states[0];
			if (this.m_lastPlayingAnimationState != null && this.m_lastPlayingAnimationState.OnStartPlaying != null)
			{
				this.m_lastPlayingAnimationState.OnStartPlaying();
			}
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000435C File Offset: 0x0000255C
	public void FixedUpdate()
	{
		this.Animator.Flip = false;
		if (this.SpriteMirror && this.m_wasFacingLeft != this.SpriteMirror.FaceLeft)
		{
			this.m_wasFacingLeft = this.SpriteMirror.FaceLeft;
			this.Animator.Flip = true;
			this.m_flipTime = 0.3f;
			this.Animator.SetAnimation(this.Animator.CurrentTextureAnimationTransitions, true);
		}
		if (this.m_flipTime > 0f)
		{
			this.m_flipTime -= Time.deltaTime;
			if (this.m_flipTime < 0f)
			{
				this.m_flipTime = 0f;
				this.Animator.Flip = false;
			}
		}
		this.UpdateStates();
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004429 File Offset: 0x00002629
	public void Awake()
	{
		this.Animator.OnAnimationEndEvent += this.OnAnimationEnd;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00004442 File Offset: 0x00002642
	public void OnDestroy()
	{
		this.Animator.OnAnimationEndEvent -= this.OnAnimationEnd;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000445C File Offset: 0x0000265C
	public void OnAnimationEnd(TextureAnimation animation)
	{
		if (this.m_states.Count == 0)
		{
			return;
		}
		CharacterAnimationSystem.CharacterAnimationState characterAnimationState = this.m_states[0];
		if (characterAnimationState.Animation.Animation == animation && characterAnimationState.PlayOnce)
		{
			this.m_states.RemoveAt(0);
		}
	}

	// Token: 0x04000092 RID: 146
	public CharacterSpriteMirror SpriteMirror;

	// Token: 0x04000093 RID: 147
	private readonly List<CharacterAnimationSystem.CharacterAnimationState> m_states = new List<CharacterAnimationSystem.CharacterAnimationState>();

	// Token: 0x04000094 RID: 148
	public SpriteAnimatorWithTransitions Animator;

	// Token: 0x04000095 RID: 149
	private CharacterAnimationSystem.CharacterAnimationState m_lastPlayingAnimationState;

	// Token: 0x04000096 RID: 150
	private bool m_wasFacingLeft;

	// Token: 0x04000097 RID: 151
	public float m_flipTime;

	// Token: 0x0200002B RID: 43
	[Serializable]
	public class CharacterAnimationState
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000833C File Offset: 0x0000653C
		public bool Condition
		{
			get
			{
				return this.ConditionFunction == null || this.ConditionFunction();
			}
		}

		// Token: 0x0400018C RID: 396
		public int Layer;

		// Token: 0x0400018D RID: 397
		public TextureAnimationWithTransitions Animation;

		// Token: 0x0400018E RID: 398
		public Func<bool> ConditionFunction;

		// Token: 0x0400018F RID: 399
		public bool PlayOnce;

		// Token: 0x04000190 RID: 400
		public Action OnStopPlaying;

		// Token: 0x04000191 RID: 401
		public Action OnStartPlaying;

		// Token: 0x04000192 RID: 402
		public bool KeepFrame;
	}
}
