using System;
using Game;
using UnityEngine;

// Token: 0x020009C4 RID: 2500
public class TheGiftSetupController : MonoBehaviour
{
	// Token: 0x06003684 RID: 13956 RVA: 0x000E52EE File Offset: 0x000E34EE
	public void Start()
	{
	}

	// Token: 0x06003685 RID: 13957 RVA: 0x000E52F0 File Offset: 0x000E34F0
	private void OnNaruCollided()
	{
		UnityEngine.Object.DestroyObject(this.Leaf);
		this.FindSeinSpriteAnimator.gameObject.SetActive(true);
		this.FindSeinSpriteAnimator.SetAnimation(this.FindSeinTextureAnimation, true);
		this.FindSeinSpriteAnimator.OnAnimationEndEvent += this.OnFindSeinAnimationEnd;
		this.GlowSpriteAnimator.gameObject.SetActive(true);
		this.GlowSpriteAnimator.SetAnimation(this.GlowTextureAnimation, true);
		UI.Cameras.Current.Target = this.CameraTarget;
		InstantiateUtility.Destroy(Characters.Naru.gameObject);
		this.OnNaruCollidedAction.Perform(null);
	}

	// Token: 0x06003686 RID: 13958 RVA: 0x000E5390 File Offset: 0x000E3590
	public void FixedUpdate()
	{
	}

	// Token: 0x06003687 RID: 13959 RVA: 0x000E5392 File Offset: 0x000E3592
	private void OnFindSeinAnimationEnd()
	{
		this.FindSeinSpriteAnimator.OnAnimationEndEvent -= this.OnFindSeinAnimationEnd;
		this.OnFinishedAction.Perform(null);
	}

	// Token: 0x04003167 RID: 12647
	public SpriteAnimator FindSeinSpriteAnimator;

	// Token: 0x04003168 RID: 12648
	public SpriteAnimator GlowSpriteAnimator;

	// Token: 0x04003169 RID: 12649
	public TextureAnimation FindSeinTextureAnimation;

	// Token: 0x0400316A RID: 12650
	public TextureAnimation GlowTextureAnimation;

	// Token: 0x0400316B RID: 12651
	public ActionMethod OnNaruCollidedAction;

	// Token: 0x0400316C RID: 12652
	public ActionMethod OnFinishedAction;

	// Token: 0x0400316D RID: 12653
	public GameObject Leaf;

	// Token: 0x0400316E RID: 12654
	public Transform CameraTarget;
}
