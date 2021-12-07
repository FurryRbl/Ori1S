using System;
using Game;
using UnityEngine;

// Token: 0x020009BF RID: 2495
public class DayFourBushSetupController : MonoBehaviour
{
	// Token: 0x0600366F RID: 13935 RVA: 0x000E4A2C File Offset: 0x000E2C2C
	public void FixedUpdate()
	{
		if (!this.m_branchAnimationWasPlayed && this.SeinSpriteAnimator.gameObject.activeInHierarchy && this.SeinSpriteAnimator.TextureAnimator.Frame > 140f)
		{
			this.BranchSpriteAnimator.SetAnimation(this.BranchBreakAnimation, true);
			this.m_branchAnimationWasPlayed = true;
		}
		if (!this.m_berriesAnimationWasPlayed && this.SeinSpriteAnimator.gameObject.activeInHierarchy && this.SeinSpriteAnimator.TextureAnimator.Frame > 191f)
		{
			this.BerriesSpriteAnimator.gameObject.SetActive(true);
			this.BerriesSpriteAnimator.SetAnimation(this.BerriesFallAnimation, true);
			this.m_berriesAnimationWasPlayed = true;
		}
	}

	// Token: 0x06003670 RID: 13936 RVA: 0x000E4AF0 File Offset: 0x000E2CF0
	public void PlayerCollisionTrigger()
	{
		this.SeinSpriteAnimator.gameObject.SetActive(true);
		this.SeinSpriteAnimator.SetAnimation(this.SeinFloatAnimation, true);
		this.SeinSpriteAnimator.OnAnimationEndEvent += this.OnAnimationEnd;
		this.LeafSpriteAnimator.gameObject.SetActive(false);
		UI.Cameras.Current.Target = this.CameraTargetDuringSequence;
		InstantiateUtility.Destroy(Characters.BabySein.gameObject);
	}

	// Token: 0x06003671 RID: 13937 RVA: 0x000E4B67 File Offset: 0x000E2D67
	private void EnterCaveTrigger()
	{
		this.MovieTextureController.StartMovieSequence();
	}

	// Token: 0x06003672 RID: 13938 RVA: 0x000E4B74 File Offset: 0x000E2D74
	public void OnAnimationEnd()
	{
		UnityEngine.Object.Instantiate(this.BabySeinWithBerries, this.SeinSpawnPisition.position, this.SeinSpriteAnimator.transform.rotation);
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
		InstantiateUtility.Destroy(this.SeinSpriteAnimator.gameObject);
		InstantiateUtility.Destroy(this.BerriesSpriteAnimator.gameObject);
	}

	// Token: 0x0400312D RID: 12589
	public SpriteAnimator SeinSpriteAnimator;

	// Token: 0x0400312E RID: 12590
	public SpriteAnimator LeafSpriteAnimator;

	// Token: 0x0400312F RID: 12591
	public SpriteAnimator BranchSpriteAnimator;

	// Token: 0x04003130 RID: 12592
	public SpriteAnimator BerriesSpriteAnimator;

	// Token: 0x04003131 RID: 12593
	public TextureAnimation BerriesFallAnimation;

	// Token: 0x04003132 RID: 12594
	public TextureAnimation BranchBreakAnimation;

	// Token: 0x04003133 RID: 12595
	public TextureAnimation BranchIdleAnimation;

	// Token: 0x04003134 RID: 12596
	public TextureAnimation LeafIdleAnimation;

	// Token: 0x04003135 RID: 12597
	public TextureAnimation SeinFloatAnimation;

	// Token: 0x04003136 RID: 12598
	public Transform CameraTargetDuringSequence;

	// Token: 0x04003137 RID: 12599
	public BabySein BabySeinWithBerries;

	// Token: 0x04003138 RID: 12600
	public MovieTextureController MovieTextureController;

	// Token: 0x04003139 RID: 12601
	public Transform SeinSpawnPisition;

	// Token: 0x0400313A RID: 12602
	private bool m_branchAnimationWasPlayed;

	// Token: 0x0400313B RID: 12603
	private bool m_berriesAnimationWasPlayed;
}
