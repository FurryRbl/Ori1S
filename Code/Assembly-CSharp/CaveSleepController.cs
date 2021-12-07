using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009BE RID: 2494
public class CaveSleepController : MonoBehaviour
{
	// Token: 0x0600366A RID: 13930 RVA: 0x000E48F5 File Offset: 0x000E2AF5
	public void Start()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.Fader).GetComponent<Fader>().FadeOutTime = this.FadeOutDuration;
	}

	// Token: 0x0600366B RID: 13931 RVA: 0x000E4912 File Offset: 0x000E2B12
	public void OnDestroy()
	{
	}

	// Token: 0x0600366C RID: 13932 RVA: 0x000E4914 File Offset: 0x000E2B14
	public void Update()
	{
		if (Core.Input.OnAnyButtonPressed && this.BabySeinSpriteAnimator.TextureAnimator.Animation.name != this.WakeUpAnimation.name)
		{
			this.BabySeinSpriteAnimator.SetAnimation(this.WakeUpAnimation, true);
			this.BabySeinSpriteAnimator.OnAnimationEndEvent += this.OnAnimationEnd;
		}
		this.m_introLength -= Time.deltaTime;
	}

	// Token: 0x0600366D RID: 13933 RVA: 0x000E4990 File Offset: 0x000E2B90
	public void OnAnimationEnd()
	{
		this.BabySeinSpriteAnimator.OnAnimationEndEvent -= this.OnAnimationEnd;
		BabySein babySein = UnityEngine.Object.Instantiate(this.BabySein, UI.Cameras.Current.Target.position, UI.Cameras.Current.Target.rotation) as BabySein;
		Transform target = UI.Cameras.Current.Target;
		UI.Cameras.Current.Target = babySein.transform;
		InstantiateUtility.Destroy(target.gameObject);
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
		if (PlayerInput.Instance)
		{
		}
	}

	// Token: 0x04003123 RID: 12579
	public BabySein BabySein;

	// Token: 0x04003124 RID: 12580
	public SpriteAnimator BabySeinSpriteAnimator;

	// Token: 0x04003125 RID: 12581
	public TextureAnimation WakeUpAnimation;

	// Token: 0x04003126 RID: 12582
	public GameObject Fader;

	// Token: 0x04003127 RID: 12583
	public float FadeOutDuration;

	// Token: 0x04003128 RID: 12584
	public SoundProvider IntroSoundProvider;

	// Token: 0x04003129 RID: 12585
	public SoundProvider LoopSoundProvider;

	// Token: 0x0400312A RID: 12586
	public float IntroFadeInDuration = 3f;

	// Token: 0x0400312B RID: 12587
	public float LoopFadeInDuration = 3f;

	// Token: 0x0400312C RID: 12588
	private float m_introLength;
}
