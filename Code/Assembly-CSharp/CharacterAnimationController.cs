using System;
using System.Collections.Generic;

// Token: 0x0200041B RID: 1051
public class CharacterAnimationController : Suspendable
{
	// Token: 0x06001D6D RID: 7533 RVA: 0x000817D4 File Offset: 0x0007F9D4
	public void Start()
	{
		this.SpriteAnimator.OnAnimationEndEvent += this.OnAnimationEnd;
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x000817ED File Offset: 0x0007F9ED
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.SpriteAnimator.OnAnimationEndEvent -= this.OnAnimationEnd;
	}

	// Token: 0x170004F6 RID: 1270
	// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0008180C File Offset: 0x0007FA0C
	// (set) Token: 0x06001D70 RID: 7536 RVA: 0x00081814 File Offset: 0x0007FA14
	public override bool IsSuspended { get; set; }

	// Token: 0x06001D71 RID: 7537 RVA: 0x0008181D File Offset: 0x0007FA1D
	public void OnAnimationEnd(TextureAnimation animation)
	{
		if (this.CurrentState)
		{
			this.CurrentState.OnAnimationEnd(animation);
		}
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x0008183C File Offset: 0x0007FA3C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.SpriteAnimator.Flip = false;
		if (this.SpriteMirror && this.m_wasFacingLeft != this.SpriteMirror.FaceLeft)
		{
			this.m_wasFacingLeft = this.SpriteMirror.FaceLeft;
			this.SpriteAnimator.Flip = true;
		}
		for (int i = 0; i < this.States.Count; i++)
		{
			CharacterAnimationStateBase characterAnimationStateBase = this.States[i];
			if (!(characterAnimationStateBase == null))
			{
				if (characterAnimationStateBase.CanEnter)
				{
					this.ChangeState(characterAnimationStateBase);
					break;
				}
			}
		}
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x000818F8 File Offset: 0x0007FAF8
	public void ResetState()
	{
		if (this.CurrentState)
		{
			this.CurrentState.OnExit();
		}
		this.CurrentState = null;
	}

	// Token: 0x06001D74 RID: 7540 RVA: 0x00081928 File Offset: 0x0007FB28
	public void ChangeState(CharacterAnimationStateBase state)
	{
		TextureAnimationWithTransitions animationToPlay = state.AnimationToPlay;
		if (this.CurrentState == state && animationToPlay == this.SpriteAnimator.CurrentTextureAnimationTransitions && !this.SpriteAnimator.Flip)
		{
			return;
		}
		if (animationToPlay == null)
		{
			return;
		}
		if (animationToPlay.Animation.FrameGuids.Count == 0)
		{
			return;
		}
		this.SpriteAnimator.SetAnimation(animationToPlay, this.CurrentState == state && animationToPlay == this.SpriteAnimator.CurrentTextureAnimationTransitions);
		if (this.CurrentState)
		{
			this.CurrentState.OnExit();
		}
		this.CurrentState = state;
		if (this.CurrentState)
		{
			this.CurrentState.OnEnter();
		}
	}

	// Token: 0x04001975 RID: 6517
	public List<CharacterAnimationStateBase> States;

	// Token: 0x04001976 RID: 6518
	public CharacterAnimationStateBase CurrentState;

	// Token: 0x04001977 RID: 6519
	public SpriteAnimatorWithTransitions SpriteAnimator;

	// Token: 0x04001978 RID: 6520
	public CharacterSpriteMirror SpriteMirror;

	// Token: 0x04001979 RID: 6521
	private bool m_wasFacingLeft;
}
