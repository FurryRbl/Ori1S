using System;
using Game;
using UnityEngine;

// Token: 0x020009C3 RID: 2499
public class PondController : MonoBehaviour
{
	// Token: 0x06003680 RID: 13952 RVA: 0x000E50D8 File Offset: 0x000E32D8
	private void FixedUpdate()
	{
		this.m_wasDeathEffect = (this.m_deathEffectTimer > 0f);
		this.m_deathEffectTimer -= Time.fixedDeltaTime;
		if (this.m_wasDeathEffect && this.m_deathEffectTimer <= 0f)
		{
			Characters.BabySein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft = false;
			UI.Cameras.Current.Target = this.EscapePondSpriteAnimator.transform;
			this.EscapePondSpriteAnimator.gameObject.SetActive(true);
			this.EscapePondSpriteAnimator.SetAnimation(this.EscapePondAnimation, true);
			this.EscapePondSpriteAnimator.OnAnimationEndEvent += this.OnAnimationEnd;
		}
	}

	// Token: 0x06003681 RID: 13953 RVA: 0x000E5190 File Offset: 0x000E3390
	private void OnTriggerEnter(Collider collider)
	{
		if (Characters.BabySein && collider.transform == Characters.BabySein.transform)
		{
			this.m_deathEffect = (UnityEngine.Object.Instantiate(this.BabySeinDeathEffect, collider.transform.position, collider.transform.rotation) as GameObject);
			UI.Cameras.Current.Target = this.m_deathEffect.transform;
			Characters.BabySein.PlatformBehaviour.PlatformMovement.LocalSpeed = Vector2.zero;
			Characters.BabySein.gameObject.SetActive(false);
			this.m_deathEffectTimer = this.DeathEffectDuration;
		}
	}

	// Token: 0x06003682 RID: 13954 RVA: 0x000E523C File Offset: 0x000E343C
	private void OnAnimationEnd()
	{
		Characters.BabySein.transform.position = this.EscapePondSpriteAnimator.transform.position;
		InstantiateUtility.Destroy(Characters.BabySein.gameObject);
		UnityEngine.Object.Instantiate(this.BabySein, this.EscapePondSpriteAnimator.transform.position, this.EscapePondSpriteAnimator.transform.rotation);
		Characters.BabySein.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
		this.EscapePondSpriteAnimator.gameObject.SetActive(false);
		UI.Cameras.Current.Target = Characters.BabySein.transform;
	}

	// Token: 0x0400315F RID: 12639
	public SpriteAnimator EscapePondSpriteAnimator;

	// Token: 0x04003160 RID: 12640
	public TextureAnimation EscapePondAnimation;

	// Token: 0x04003161 RID: 12641
	public GameObject BabySein;

	// Token: 0x04003162 RID: 12642
	public GameObject BabySeinDeathEffect;

	// Token: 0x04003163 RID: 12643
	public float DeathEffectDuration = 1f;

	// Token: 0x04003164 RID: 12644
	private float m_deathEffectTimer = -1f;

	// Token: 0x04003165 RID: 12645
	private GameObject m_deathEffect;

	// Token: 0x04003166 RID: 12646
	private bool m_wasDeathEffect;
}
