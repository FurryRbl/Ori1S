using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009C1 RID: 2497
public class DayThreeTreeSetupController : MonoBehaviour
{
	// Token: 0x06003677 RID: 13943 RVA: 0x000E4CFE File Offset: 0x000E2EFE
	public void Start()
	{
		this.ChangeState(DayThreeTreeSetupController.State.None);
	}

	// Token: 0x06003678 RID: 13944 RVA: 0x000E4D07 File Offset: 0x000E2F07
	public void FixedUpdate()
	{
		this.UpdateState();
	}

	// Token: 0x06003679 RID: 13945 RVA: 0x000E4D10 File Offset: 0x000E2F10
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case DayThreeTreeSetupController.State.BranchIdle:
			if (Core.Input.Left.IsPressed)
			{
				this.ChangeState(DayThreeTreeSetupController.State.BranchBackwards);
			}
			if (Core.Input.Right.IsPressed)
			{
				this.ChangeState(DayThreeTreeSetupController.State.BranchReach);
			}
			if (Core.Input.Jump.OnPressed)
			{
				this.ChangeState(DayThreeTreeSetupController.State.Jump);
			}
			break;
		case DayThreeTreeSetupController.State.BranchBackwards:
			if (!Core.Input.Left.IsPressed)
			{
				this.ChangeState(DayThreeTreeSetupController.State.BranchIdle);
			}
			break;
		case DayThreeTreeSetupController.State.BranchReach:
			if (Core.Input.Jump.OnPressed)
			{
				this.ChangeState(DayThreeTreeSetupController.State.JumpForward);
			}
			if (!Core.Input.Right.IsPressed)
			{
				this.ChangeState(DayThreeTreeSetupController.State.BranchIdle);
			}
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x0600367A RID: 13946 RVA: 0x000E4E00 File Offset: 0x000E3000
	public void ChangeState(DayThreeTreeSetupController.State state)
	{
		switch (this.CurrentState)
		{
		}
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		switch (this.CurrentState)
		{
		case DayThreeTreeSetupController.State.BranchIdle:
			this.NaruBranchAnimationSystem.Play(this.NaruBranchIdleAnimation, 0, null);
			break;
		case DayThreeTreeSetupController.State.BranchBackwards:
			this.NaruBranchAnimationSystem.Play(this.NaruBranchBackwardsAnimation, 0, null);
			break;
		case DayThreeTreeSetupController.State.BranchReach:
			this.NaruBranchAnimationSystem.Play(this.NaruBranchReachAnimation, 0, null);
			break;
		case DayThreeTreeSetupController.State.Jump:
			this.m_jumpsCount++;
			this.NaruBranchAnimationSystem.Play(this.NaruBranchJumpIdleAnimation, 0, null);
			this.NaruBranchAnimationSystem.Animator.OnAnimationEndEvent += this.OnJumpAnimationEnd;
			break;
		case DayThreeTreeSetupController.State.Fall:
			this.NaruBranchAnimationSystem.Play(this.NaruBranchJumpIdleCollapseAnimation, 0, null);
			this.NaruBranchAnimationSystem.Animator.OnAnimationEndEvent += this.NaruBranchJumpOnAnimationEnd;
			break;
		case DayThreeTreeSetupController.State.JumpForward:
			this.NaruBranchAnimationSystem.Play(this.NaruBranchJumpAnimation, 0, null);
			this.TreeSpriteAnimator.AnimatorDriver.Resume();
			this.NaruBranchAnimationSystem.Animator.OnAnimationEndEvent += this.NaruBranchJumpOnAnimationEnd;
			break;
		}
	}

	// Token: 0x0600367B RID: 13947 RVA: 0x000E4FAB File Offset: 0x000E31AB
	private void OnJumpAnimationEnd(TextureAnimation textureAnimation)
	{
		this.NaruBranchAnimationSystem.Animator.OnAnimationEndEvent -= this.OnJumpAnimationEnd;
		this.ChangeState(DayThreeTreeSetupController.State.BranchIdle);
	}

	// Token: 0x0600367C RID: 13948 RVA: 0x000E4FD0 File Offset: 0x000E31D0
	private void NaruBranchJumpOnAnimationEnd(TextureAnimation textureAnimation)
	{
		this.NaruBranchAnimationSystem.Animator.OnAnimationEndEvent -= this.NaruBranchJumpOnAnimationEnd;
		this.MovieTextureController.StartMovieSequence();
	}

	// Token: 0x0600367D RID: 13949 RVA: 0x000E4FFC File Offset: 0x000E31FC
	private void NaruClimbTrigger()
	{
		this.NaruClimbSpriteAnimator.gameObject.SetActive(true);
		this.NaruClimbSpriteAnimator.SetAnimation(this.NaruBranchGrabAnimation, true);
		this.NaruClimbSpriteAnimator.OnAnimationEndEvent += this.OnNaruClimbAnimationFinished;
		UI.Cameras.Current.Target = this.NaruClimbSpriteAnimator.transform.parent;
		InstantiateUtility.Destroy(Characters.Naru.gameObject);
	}

	// Token: 0x0600367E RID: 13950 RVA: 0x000E506C File Offset: 0x000E326C
	private void OnNaruClimbAnimationFinished()
	{
		this.NaruClimbSpriteAnimator.OnAnimationEndEvent -= this.OnNaruClimbAnimationFinished;
		this.NaruBranchSpriteAnimatorWithTransitions.gameObject.SetActive(true);
		this.NaruClimbSpriteAnimator.gameObject.SetActive(false);
		this.ChangeState(DayThreeTreeSetupController.State.BranchIdle);
	}

	// Token: 0x04003147 RID: 12615
	public SpriteAnimator NaruClimbSpriteAnimator;

	// Token: 0x04003148 RID: 12616
	public SpriteAnimatorWithTransitions NaruBranchSpriteAnimatorWithTransitions;

	// Token: 0x04003149 RID: 12617
	public SpriteAnimator TreeSpriteAnimator;

	// Token: 0x0400314A RID: 12618
	public TextureAnimation NaruBranchGrabAnimation;

	// Token: 0x0400314B RID: 12619
	public TextureAnimationWithTransitions NaruBranchIdleAnimation;

	// Token: 0x0400314C RID: 12620
	public TextureAnimationWithTransitions NaruBranchReachAnimation;

	// Token: 0x0400314D RID: 12621
	public TextureAnimationWithTransitions NaruBranchBackwardsAnimation;

	// Token: 0x0400314E RID: 12622
	public TextureAnimationWithTransitions NaruBranchJumpAnimation;

	// Token: 0x0400314F RID: 12623
	public TextureAnimationWithTransitions NaruBranchJumpIdleAnimation;

	// Token: 0x04003150 RID: 12624
	public TextureAnimationWithTransitions NaruBranchJumpIdleCollapseAnimation;

	// Token: 0x04003151 RID: 12625
	public CharacterAnimationSystem NaruBranchAnimationSystem;

	// Token: 0x04003152 RID: 12626
	public DayThreeTreeSetupController.State CurrentState;

	// Token: 0x04003153 RID: 12627
	private float m_stateCurrentTime;

	// Token: 0x04003154 RID: 12628
	private int m_jumpsCount;

	// Token: 0x04003155 RID: 12629
	public MovieTextureController MovieTextureController;

	// Token: 0x04003156 RID: 12630
	public TextureAnimationWithTransitions TextureAnimationWithTransitions;

	// Token: 0x020009C2 RID: 2498
	public enum State
	{
		// Token: 0x04003158 RID: 12632
		None,
		// Token: 0x04003159 RID: 12633
		BranchIdle,
		// Token: 0x0400315A RID: 12634
		BranchBackwards,
		// Token: 0x0400315B RID: 12635
		BranchReach,
		// Token: 0x0400315C RID: 12636
		Jump,
		// Token: 0x0400315D RID: 12637
		Fall,
		// Token: 0x0400315E RID: 12638
		JumpForward
	}
}
