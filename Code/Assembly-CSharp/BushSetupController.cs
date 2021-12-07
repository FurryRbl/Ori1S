using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009BC RID: 2492
public class BushSetupController : MonoBehaviour
{
	// Token: 0x0600365E RID: 13918 RVA: 0x000E44FD File Offset: 0x000E26FD
	public void FixedUpdate()
	{
		this.UpdateState();
	}

	// Token: 0x0600365F RID: 13919 RVA: 0x000E4508 File Offset: 0x000E2708
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case BushSetupController.State.LiftSein:
			if (Core.Input.Left.Pressed)
			{
				this.ChangeState(BushSetupController.State.LiftSeinToRockReach);
			}
			break;
		case BushSetupController.State.LiftSeinToRockReach:
			if (Core.Input.Left.Released)
			{
				this.ChangeState(BushSetupController.State.RockReachToLiftSein);
			}
			break;
		case BushSetupController.State.RockReach:
			if (Core.Input.Left.Released)
			{
				this.ChangeState(BushSetupController.State.RockReachToLiftSein);
			}
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x06003660 RID: 13920 RVA: 0x000E4598 File Offset: 0x000E2798
	public void ChangeState(BushSetupController.State state)
	{
		switch (this.CurrentState)
		{
		case BushSetupController.State.KneelingRockToLiftSein:
			this.NaruSpriteAnimator.OnAnimationEndEvent -= this.KneelingRockToLiftSeinOnAnimatoinEnd;
			break;
		case BushSetupController.State.LiftSeinToRockReach:
			this.NaruSpriteAnimator.OnAnimationEndEvent -= this.LiftSeinToRockReachOnAnimatoinEnd;
			break;
		case BushSetupController.State.RockReach:
			this.NaruSpriteAnimator.OnAnimationEndEvent -= this.RockReachOnAnimatoinEnd;
			break;
		case BushSetupController.State.RockFall:
			this.NaruSpriteAnimator.OnAnimationEndEvent -= this.RockFallOnAnimatoinEnd;
			break;
		case BushSetupController.State.RockReachToLiftSein:
			this.NaruSpriteAnimator.OnAnimationEndEvent -= this.RockReachToLiftSeinOnAnimatoinEnd;
			break;
		}
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		switch (this.CurrentState)
		{
		case BushSetupController.State.KneelingRock:
			this.NaruSpriteAnimator.SetAnimation(this.KneelingRockAnimation, true);
			break;
		case BushSetupController.State.KneelingRockToLiftSein:
			this.NaruSpriteAnimator.SetAnimation(this.KneelingRockToLiftSeinAnimation, true);
			this.NaruSpriteAnimator.OnAnimationEndEvent += this.KneelingRockToLiftSeinOnAnimatoinEnd;
			break;
		case BushSetupController.State.LiftSein:
			this.NaruSpriteAnimator.SetAnimation(this.LiftSeinAnimation, true);
			break;
		case BushSetupController.State.LiftSeinToRockReach:
			this.NaruSpriteAnimator.SetAnimation(this.LiftSeinToRockReachAnimation, true);
			this.NaruSpriteAnimator.OnAnimationEndEvent += this.LiftSeinToRockReachOnAnimatoinEnd;
			break;
		case BushSetupController.State.RockReach:
			this.NaruSpriteAnimator.SetAnimation(this.RockReachAnimation, true);
			this.NaruSpriteAnimator.OnAnimationEndEvent += this.RockReachOnAnimatoinEnd;
			break;
		case BushSetupController.State.RockFall:
			InstantiateUtility.Instantiate(this.LightCeremonyEffects);
			this.SeinSpriteAnimator.gameObject.SetActive(true);
			this.NaruSpriteAnimator.SetAnimation(this.RockFallNaruAnimation, true);
			this.NaruSpriteAnimator.OnAnimationEndEvent += this.RockFallOnAnimatoinEnd;
			break;
		case BushSetupController.State.RockReachToLiftSein:
			this.NaruSpriteAnimator.SetAnimation(this.RockReachToLiftSeinAnimation, true);
			this.NaruSpriteAnimator.OnAnimationEndEvent += this.RockReachToLiftSeinOnAnimatoinEnd;
			break;
		}
	}

	// Token: 0x06003661 RID: 13921 RVA: 0x000E47E3 File Offset: 0x000E29E3
	public void NoticeSeinOnAnimationEnd()
	{
		this.NaruSpriteAnimator.OnAnimationEndEvent -= this.NoticeSeinOnAnimationEnd;
		this.ChangeState(BushSetupController.State.KneelingRock);
	}

	// Token: 0x06003662 RID: 13922 RVA: 0x000E4804 File Offset: 0x000E2A04
	public void RockFallOnAnimatoinEnd()
	{
		this.NaruSpriteAnimator.OnAnimationEndEvent -= this.RockFallOnAnimatoinEnd;
		this.MovieTextureController.StartMovieSequence();
	}

	// Token: 0x06003663 RID: 13923 RVA: 0x000E4833 File Offset: 0x000E2A33
	public void RockReachOnAnimatoinEnd()
	{
		this.ChangeState(BushSetupController.State.RockFall);
	}

	// Token: 0x06003664 RID: 13924 RVA: 0x000E483C File Offset: 0x000E2A3C
	public void RockReachToLiftSeinOnAnimatoinEnd()
	{
		this.ChangeState(BushSetupController.State.LiftSein);
	}

	// Token: 0x06003665 RID: 13925 RVA: 0x000E4845 File Offset: 0x000E2A45
	public void LiftSeinToRockReachOnAnimatoinEnd()
	{
		this.ChangeState(BushSetupController.State.RockReach);
	}

	// Token: 0x06003666 RID: 13926 RVA: 0x000E484E File Offset: 0x000E2A4E
	public void KneelingRockToLiftSeinOnAnimatoinEnd()
	{
		this.ChangeState(BushSetupController.State.LiftSein);
	}

	// Token: 0x06003667 RID: 13927 RVA: 0x000E4858 File Offset: 0x000E2A58
	public void PlayerCollisionTrigger()
	{
		this.ChangeState(BushSetupController.State.KneelingRockToLiftSein);
		UI.Cameras.Current.Target = this.NaruSpriteAnimator.transform;
		InstantiateUtility.Destroy(Characters.BabySein.gameObject);
	}

	// Token: 0x06003668 RID: 13928 RVA: 0x000E4890 File Offset: 0x000E2A90
	public void NoticeSeinTrigger()
	{
		this.NaruSpriteAnimator.gameObject.SetActive(true);
		this.NaruSpriteAnimator.SetAnimation(this.NoticeSeinAnimation, true);
		this.NaruSpriteAnimator.OnAnimationEndEvent += this.NoticeSeinOnAnimationEnd;
	}

	// Token: 0x0400310B RID: 12555
	public SpriteAnimator NaruSpriteAnimator;

	// Token: 0x0400310C RID: 12556
	public SpriteAnimator SeinSpriteAnimator;

	// Token: 0x0400310D RID: 12557
	public TextureAnimation KneelingRockAnimation;

	// Token: 0x0400310E RID: 12558
	public TextureAnimation KneelingRockToLiftSeinAnimation;

	// Token: 0x0400310F RID: 12559
	public TextureAnimation LiftSeinAnimation;

	// Token: 0x04003110 RID: 12560
	public TextureAnimation LiftSeinToRockReachAnimation;

	// Token: 0x04003111 RID: 12561
	public TextureAnimation RockReachAnimation;

	// Token: 0x04003112 RID: 12562
	public TextureAnimation RockFallSeinAnimation;

	// Token: 0x04003113 RID: 12563
	public TextureAnimation RockFallNaruAnimation;

	// Token: 0x04003114 RID: 12564
	public TextureAnimation RockReachToLiftSeinAnimation;

	// Token: 0x04003115 RID: 12565
	public TextureAnimation NoticeSeinAnimation;

	// Token: 0x04003116 RID: 12566
	public GameObject LightCeremonyEffects;

	// Token: 0x04003117 RID: 12567
	public MovieTextureController MovieTextureController;

	// Token: 0x04003118 RID: 12568
	public BushSetupController.State CurrentState;

	// Token: 0x04003119 RID: 12569
	private float m_stateCurrentTime;

	// Token: 0x020009BD RID: 2493
	public enum State
	{
		// Token: 0x0400311B RID: 12571
		KneelingRock,
		// Token: 0x0400311C RID: 12572
		KneelingRockToLiftSein,
		// Token: 0x0400311D RID: 12573
		LiftSein,
		// Token: 0x0400311E RID: 12574
		LiftSeinToRockReach,
		// Token: 0x0400311F RID: 12575
		RockReach,
		// Token: 0x04003120 RID: 12576
		RockFall,
		// Token: 0x04003121 RID: 12577
		RockReachToLiftSein,
		// Token: 0x04003122 RID: 12578
		NoticeSein
	}
}
