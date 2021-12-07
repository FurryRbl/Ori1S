using System;
using Game;
using UnityEngine;

// Token: 0x020009BA RID: 2490
public class BerryCelebrationController : MonoBehaviour
{
	// Token: 0x06003654 RID: 13908 RVA: 0x000E41A0 File Offset: 0x000E23A0
	private void OnNaruCollided()
	{
		this.BerryCelebrationSpriteAnimator.gameObject.SetActive(true);
		this.BerryCelebrationSpriteAnimator.SetAnimation(this.BerryCelebrationAnimation, true);
		this.BerryCelebrationSpriteAnimator.OnAnimationEndEvent += this.OnBerryCelebrationAnimationEnd;
		UI.Cameras.Current.Target = this.BerryCelebrationSpriteAnimator.transform;
		InstantiateUtility.Destroy(Characters.Naru.gameObject);
	}

	// Token: 0x06003655 RID: 13909 RVA: 0x000E420C File Offset: 0x000E240C
	public void Update()
	{
		this.m_introLength -= Time.deltaTime;
		if (this.BerryCelebrationSpriteAnimator.TextureAnimator != null && this.BerryCelebrationSpriteAnimator.TextureAnimator.Animation != null && this.BerryCelebrationSpriteAnimator.TextureAnimator.Frame > 34f)
		{
			UnityEngine.Object.DestroyObject(this.Rope);
		}
	}

	// Token: 0x06003656 RID: 13910 RVA: 0x000E427B File Offset: 0x000E247B
	private void OnDestroy()
	{
	}

	// Token: 0x06003657 RID: 13911 RVA: 0x000E4280 File Offset: 0x000E2480
	private void OnBerryCelebrationAnimationEnd()
	{
		this.BerryCelebrationSpriteAnimator.OnAnimationEndEvent -= this.OnBerryCelebrationAnimationEnd;
		this.MovieTextureController.StartMovieSequence();
	}

	// Token: 0x040030F6 RID: 12534
	public SpriteAnimator BerryCelebrationSpriteAnimator;

	// Token: 0x040030F7 RID: 12535
	public TextureAnimation BerryCelebrationAnimation;

	// Token: 0x040030F8 RID: 12536
	public MovieTextureController MovieTextureController;

	// Token: 0x040030F9 RID: 12537
	public GameObject Rope;

	// Token: 0x040030FA RID: 12538
	public SoundProvider IntroSoundProvider;

	// Token: 0x040030FB RID: 12539
	public SoundProvider LoopSoundProvider;

	// Token: 0x040030FC RID: 12540
	public float IntroFadeInDuration = 3f;

	// Token: 0x040030FD RID: 12541
	public float LoopFadeInDuration = 3f;

	// Token: 0x040030FE RID: 12542
	private float m_introLength;
}
