using System;
using Game;
using UnityEngine;

// Token: 0x020009C0 RID: 2496
public class DayThreeBridgeSetupController : MonoBehaviour
{
	// Token: 0x06003674 RID: 13940 RVA: 0x000E4BDC File Offset: 0x000E2DDC
	private void NaruTrigger()
	{
		this.Bridge.SetActive(false);
		this.SurfaceColliderBeforeCrash.enabled = false;
		this.BridgeCollapseASpriteAnimator.gameObject.SetActive(true);
		this.BridgeCollapseBSpriteAnimator.gameObject.SetActive(true);
		this.NaruCollapseSpriteAnimator.gameObject.SetActive(true);
		this.NaruCollapseSpriteAnimator.SetAnimation(this.BridgeCollapseNaruAnimation, true);
		this.NaruCollapseSpriteAnimator.OnAnimationEndEvent += this.BridgeCollapseNaruOnAnimationEnded;
		UI.Cameras.Current.Target = this.NaruCollapseSpriteAnimator.transform;
		InstantiateUtility.Destroy(Characters.Naru.gameObject);
	}

	// Token: 0x06003675 RID: 13941 RVA: 0x000E4C84 File Offset: 0x000E2E84
	private void BridgeCollapseNaruOnAnimationEnded()
	{
		this.NaruCollapseSpriteAnimator.OnAnimationEndEvent -= this.BridgeCollapseNaruOnAnimationEnded;
		UnityEngine.Object.Instantiate(this.Naru, this.NaruSpawnPisition.position, this.NaruSpawnPisition.transform.rotation);
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
		this.NaruCollapseSpriteAnimator.gameObject.SetActive(false);
		this.SurfaceColliderAfterCrash.enabled = true;
	}

	// Token: 0x0400313C RID: 12604
	public SpriteAnimator NaruCollapseSpriteAnimator;

	// Token: 0x0400313D RID: 12605
	public SpriteAnimator BridgeCollapseASpriteAnimator;

	// Token: 0x0400313E RID: 12606
	public SpriteAnimator BridgeCollapseBSpriteAnimator;

	// Token: 0x0400313F RID: 12607
	public TextureAnimation BridgeCollapseAAnimation;

	// Token: 0x04003140 RID: 12608
	public TextureAnimation BridgeCollapseBAnimation;

	// Token: 0x04003141 RID: 12609
	public TextureAnimation BridgeCollapseNaruAnimation;

	// Token: 0x04003142 RID: 12610
	public GameObject Bridge;

	// Token: 0x04003143 RID: 12611
	public Collider SurfaceColliderBeforeCrash;

	// Token: 0x04003144 RID: 12612
	public Collider SurfaceColliderAfterCrash;

	// Token: 0x04003145 RID: 12613
	public Naru Naru;

	// Token: 0x04003146 RID: 12614
	public Transform NaruSpawnPisition;
}
