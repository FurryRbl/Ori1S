using System;
using Game;
using UnityEngine;

// Token: 0x020009BB RID: 2491
public class BerryTreeController : MonoBehaviour
{
	// Token: 0x06003659 RID: 13913 RVA: 0x000E42B7 File Offset: 0x000E24B7
	public void Start()
	{
		this.NaruSpriteAnimator.SetAnimation(this.NaruAnimationA, true);
		this.TreeSpriteAnimator.SetAnimation(this.TreeAnimationA, true);
		this.NaruSpriteAnimator.OnAnimationEndEvent += this.OnAnimationEnd;
	}

	// Token: 0x0600365A RID: 13914 RVA: 0x000E42F4 File Offset: 0x000E24F4
	public void OnAnimationEnd()
	{
		switch (UnityEngine.Random.Range(0, 4))
		{
		case 0:
			this.NaruSpriteAnimator.SetAnimation(this.NaruAnimationA, true);
			this.TreeSpriteAnimator.SetAnimation(this.TreeAnimationA, true);
			break;
		case 1:
			this.NaruSpriteAnimator.SetAnimation(this.NaruAnimationB, true);
			this.TreeSpriteAnimator.SetAnimation(this.TreeAnimationB, true);
			break;
		case 2:
			this.NaruSpriteAnimator.SetAnimation(this.NaruAnimationC, true);
			this.TreeSpriteAnimator.SetAnimation(this.TreeAnimationC, true);
			break;
		case 3:
			this.NaruSpriteAnimator.SetAnimation(this.NaruAnimationD, true);
			this.TreeSpriteAnimator.SetAnimation(this.TreeAnimationD, true);
			break;
		}
	}

	// Token: 0x0600365B RID: 13915 RVA: 0x000E43C8 File Offset: 0x000E25C8
	public void PlayerCollisionTrigger()
	{
		this.NaruSpriteAnimator.OnAnimationEndEvent -= this.OnAnimationEnd;
		this.NaruSpriteAnimator.OnAnimationEndEvent += this.SwapCharacters;
		Transform target = UI.Cameras.Current.Target;
		UI.Cameras.Current.Target = this.NaruSpriteAnimator.transform;
		InstantiateUtility.Destroy(target.gameObject);
		this.NaruSpriteAnimator.SetAnimation(this.SeinClimbOnNaruAnimation, true);
		this.TreeSpriteAnimator.SetAnimation(this.TreeAnimationA, true);
	}

	// Token: 0x0600365C RID: 13916 RVA: 0x000E4454 File Offset: 0x000E2654
	public void SwapCharacters()
	{
		this.NaruSpriteAnimator.OnAnimationEndEvent -= this.OnAnimationEnd;
		Naru naru = InstantiateUtility.Instantiate(this.Naru) as Naru;
		naru.SeinNaruComboEnabled = true;
		naru.transform.position = this.NaruSpriteAnimator.transform.position;
		naru.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
		Transform target = UI.Cameras.Current.Target;
		UI.Cameras.Current.Target = naru.transform;
		InstantiateUtility.Destroy(target.gameObject);
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
	}

	// Token: 0x040030FF RID: 12543
	public SpriteAnimator NaruSpriteAnimator;

	// Token: 0x04003100 RID: 12544
	public SpriteAnimator TreeSpriteAnimator;

	// Token: 0x04003101 RID: 12545
	public TextureAnimation NaruAnimationA;

	// Token: 0x04003102 RID: 12546
	public TextureAnimation NaruAnimationB;

	// Token: 0x04003103 RID: 12547
	public TextureAnimation NaruAnimationC;

	// Token: 0x04003104 RID: 12548
	public TextureAnimation NaruAnimationD;

	// Token: 0x04003105 RID: 12549
	public TextureAnimation TreeAnimationA;

	// Token: 0x04003106 RID: 12550
	public TextureAnimation TreeAnimationB;

	// Token: 0x04003107 RID: 12551
	public TextureAnimation TreeAnimationC;

	// Token: 0x04003108 RID: 12552
	public TextureAnimation TreeAnimationD;

	// Token: 0x04003109 RID: 12553
	public TextureAnimation SeinClimbOnNaruAnimation;

	// Token: 0x0400310A RID: 12554
	public Naru Naru;
}
